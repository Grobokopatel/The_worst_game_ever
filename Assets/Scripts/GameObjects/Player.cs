using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3F;

    public float currentReload
    {
        get;
        set;
    }
    private readonly float reload = 6;
    private Animator animator;
    private SpriteRenderer sprite;
    public static Player player;
    public readonly static float ConstantYPosition = -6.283F;
    public Boat Boat
    {
        get;
        set;
    }

    [SerializeField] private GameObject fishingGamePrefab;
    [SerializeField] private GameObject bobber;
    [SerializeField] private GameObject boatPrefab;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameObject inventorySlotPrefab;
    public Interactable CurrentInteractable { get; set; }

    public PlayerState State
    {
        get => (PlayerState)animator.GetInteger("State");
        set
        {
            switch (value)
            {
                case PlayerState.Run:
                case PlayerState.Idle:
                    if (!PauseMenu.pauseMenu.IsGamePaused)
                        enabled = true;
                    sprite.sortingOrder = 5;
                    var newPosition = player.transform.position;
                    newPosition.y = ConstantYPosition;
                    transform.position = newPosition;
                    break;

                case PlayerState.InBoat:
                    sprite.sortingOrder = 12;
                    break;

                case PlayerState.Fishing:
                    sprite.sortingOrder = 11;
                    enabled = false;
                    Instantiate(fishingGamePrefab);
                    break;
            }

            animator.SetInteger("State", (int)value);
        }
    }


    private void Awake()
    {
        AddDeltaItems("Key");
        AddDeltaItems("Dynamite");
        AddDeltaItems("Shovel");
        AddDeltaItems("Axe");
        AddDeltaItems("Log", 3);
        AddDeltaItems("Pickaxe");

        player = this;
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(Technical.WaitThenInvokeMethod(3, () =>
        {
            GetComponent<AudioSource>().Play();
        }));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (player.GetAmountOfItem("FishingRod") >= 1)
            {
                if (State != PlayerState.InBoat)
                {
                    State = PlayerState.Fishing;
                    return;
                }
            }
            else
            {
                PopUpTextCreator.QueueText($"Мне нечем рыбачить");
            }
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            if (GetAmountOfItem("Shovel") >= 1)
            {
                if (State == PlayerState.InBoat)
                {
                    PopUpTextCreator.QueueText($"Воду довольно проблематично копать");
                }
                else
                if (currentReload <= 0)
                {
                    AudioManager.PlayAudio(AudioManager.DigSound);
                    var closestDiggable =
                        Technical.GetCollidersInPosition(transform.position)
                        .Select(collider => collider.GetComponent<Diggable>())
                        .FirstOrDefault(diggable => diggable != null);

                    if (closestDiggable != null)
                    {
                        closestDiggable.TryDig();
                        PopUpTextCreator.QueueText($"Так, я что-то нашёл");
                    }
                    else
                    {
                        PopUpTextCreator.QueueText($"Я ничего не откопал");
                        currentReload = reload;
                    }
                }
                else
                {
                    PopUpTextCreator.QueueText($"Я устал, мне бы передохнуть ещё секунды {Mathf.CeilToInt(currentReload)}");
                }
            }
            else
            {
                if (State == PlayerState.InBoat)
                    PopUpTextCreator.QueueText($"Мне нечем копать, да и в воде довольно проблематично что-то откопать");
                else
                    PopUpTextCreator.QueueText($"Мне нечем копать");
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (State == PlayerState.InBoat)
            {
                if (Enumerable.Range(0, 3)
                    .All(number => Technical.GetCollidersInPosition(transform.position + transform.up + (0.75F - number * 0.75F) * transform.right).Length >= 1))
                {
                    Instantiate(boatPrefab, transform.position, transform.rotation).GetComponentInChildren<SpriteRenderer>().flipX = !sprite.flipX;

                    if (Input.GetButton("Horizontal"))
                        State = PlayerState.Run;
                    else
                        State = PlayerState.Idle;
                }
            }
            else if (Boat != null)
            {
                State = PlayerState.InBoat;
                transform.position = Boat.transform.position;
                Destroy(Boat.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
            CurrentInteractable?.Interact();

        if (State != PlayerState.InBoat)
        {
            if (Input.GetButtonDown("Horizontal"))
                State = PlayerState.Run;
            if ((Input.GetButtonUp("Horizontal") && !Input.GetButton("Horizontal")) || !Input.GetButton("Horizontal"))
                State = PlayerState.Idle;
        }

        if (State == PlayerState.Run)
        {
            Walk();
        }
        else if (Input.GetButton("Horizontal") && State == PlayerState.InBoat)
        {
            Swim();
        }
    }

    public void Swim()
    {
        Move(false, Input.GetAxis("Horizontal"));
    }

    public void Walk()
    {
        Move(true, Input.GetAxis("Horizontal"));
    }

    public void Move(bool shouldCheckOnCollidersAhead, float scale)
    {
        var deltaMovement = scale * transform.right;
        sprite.flipX = deltaMovement.x < 0;

        var bobberCoords = bobber.transform.localPosition;
        bobberCoords.x = Math.Abs(bobberCoords.x) * (sprite.flipX ? -1 : 1);
        bobber.transform.localPosition = bobberCoords;

        if (!shouldCheckOnCollidersAhead ||
            Technical.GetCollidersInPosition(transform.position + 0.7F * deltaMovement.normalized +
                                             (-0.5F) * transform.up).Length >= 1)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + deltaMovement,
                speed * Time.deltaTime);
    }

    private readonly Dictionary<Item, (int quantity, GameObject itemSlot)> inventory =
        new Dictionary<Item, (int, GameObject)>();

    public void AddDeltaItems(Item item, int deltaAmount = 1)
    {
        if (deltaAmount != 0)
            PopUpTextCreator.QueueText($"{(deltaAmount > 0 ? "+" : "")}{deltaAmount} {item.ItemName}",
                deltaAmount > 0 ? Color.white : Color.red);
        int newQuantity;
        if (item.ItemName == "Лодка")
        {
            var playerPosition = transform.position;
            playerPosition.x += 2;
            playerPosition.y = -7.316F;
            Instantiate(boatPrefab, playerPosition, transform.rotation);
            return;
        }

        if (item.ItemName == "Вторая часть ключа" && !Shop.WasCanvasOpenedBefore)
        {
            CraftingMenu.craftingMenu.AddRecipeOnCanvas(
                Resources.Load<CraftingRecipe>("Prefabs/Crafting recipes/KeyRecipe"));
        }

        if (inventory.ContainsKey(item))
        {
            newQuantity = inventory[item].quantity + deltaAmount;
        }
        else
        {
            newQuantity = deltaAmount;
            inventory[item] = (deltaAmount, null);
        }

        if (newQuantity == 0)
        {
            if (inventory[item].itemSlot != null)
                Destroy(inventory[item].itemSlot);
            inventory[item] = (0, null);
            return;
        }
        else
        {
            if (inventory[item].itemSlot == null)
            {
                inventory[item] = (newQuantity, Instantiate(inventorySlotPrefab, inventoryCanvas.transform));
                inventory[item].itemSlot.GetComponentsInChildren<Image>()[1].sprite = item.Icon;
            }
            else
                inventory[item] = (newQuantity, inventory[item].itemSlot);
        }

        inventory[item].itemSlot.GetComponentInChildren<Text>().text = newQuantity == 1 ? "" : newQuantity.ToString();
    }

    public void AddDeltaItems(string itemName, int deltaAmount = 1)
    {
        AddDeltaItems(itemName.GetItemWithThisName(), deltaAmount);
    }

    public int GetAmountOfItem(Item item)
    {
        if (inventory.ContainsKey(item))
            return inventory[item].quantity;

        inventory[item] = (0, null);
        return 0;
    }

    public int GetAmountOfItem(string itemName)
    {
        return GetAmountOfItem(itemName.GetItemWithThisName());
    }
}