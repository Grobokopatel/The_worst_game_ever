using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3F;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider2D collider;
    public static Player player;
    public readonly static float ConstantYPosition = -6.283F;
    [SerializeField] private GameObject fishingGamePrefab;
    [SerializeField]
    private GameObject bobber;
    [SerializeField] private GameObject boatPrefab;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameObject itemPrefab;

    public PlayerState State
    {
        get => (PlayerState)animator.GetInteger("State");
        set
        {
            switch (value)
            {
                case PlayerState.Idle:
                    sprite.sortingOrder = 5;
                    enabled = true;
                    var newPosition = player.transform.position;
                    newPosition.y = ConstantYPosition;
                    transform.position = newPosition;
                    break;

                case PlayerState.Run:
                    sprite.sortingOrder = 5;
                    enabled = true;
                    break;

                case PlayerState.InBoat:
                    sprite.sortingOrder = 11;
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

    public Interactable CurrentInteractable { get; set; }

    private void Awake()
    {

        //AddDeltaItems("ShovelRecipe", 1);
        player = this;
        collider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
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
                PopUpTextCreator.TextsToPopUp.Enqueue(($"Мне нечем рыбачить", Color.white));
            }

        if (State == PlayerState.InBoat && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                 && Enumerable.Range(0, 3)
                     .All(number =>
                         Technical.GetCollidersInPosition(transform.position + transform.up +
                                                          (0.75F - number * 0.75F) * transform.right).Length >= 1))
        {
            Instantiate(boatPrefab, transform.position, transform.rotation).GetComponentInChildren<SpriteRenderer>().flipX = !sprite.flipX;
            if (Input.GetButton("Horizontal"))
            {
                State = PlayerState.Idle;
                State = PlayerState.Run;
            }
            else
                State = PlayerState.Idle;
            sprite.sortingOrder = 5;
        }

        if (CurrentInteractable != null && Input.GetKeyDown(KeyCode.E))
            CurrentInteractable.Interact();

        if (State != PlayerState.InBoat)
        {
            if (Input.GetButtonDown("Horizontal"))
                State = PlayerState.Run;
            if (Input.GetButtonUp("Horizontal") && !Input.GetButton("Horizontal"))
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
        State = PlayerState.Run;
        Move(true, Input.GetAxis("Horizontal"));
    }

    public void Move(bool shouldCheckOnCollidersAhead, float scale)
    {
        var deltaMovement = scale * transform.right;
        sprite.flipX = deltaMovement.x < 0;

        /*var colliderOffset = collider.offset;
        colliderOffset.x = (float)Math.Abs(colliderOffset.x) * (sprite.flipX ? 1 : -1);
        collider.offset = colliderOffset;*/

        var bobberCoords = bobber.transform.localPosition;
        bobberCoords.x = Math.Abs(bobberCoords.x) * (sprite.flipX ? -1 : 1);
        bobber.transform.localPosition = bobberCoords;

        if (!shouldCheckOnCollidersAhead ||
            Technical.GetCollidersInPosition(transform.position + 0.7F * deltaMovement.normalized +
                                             (-0.5F) * transform.up).Length >= 1)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + deltaMovement,
                speed * Time.deltaTime);
    }

    private readonly Dictionary<Item, (int quantity, GameObject itemObject)> inventory =
        new Dictionary<Item, (int, GameObject)>();

    public void AddDeltaItems(Item item, int deltaAmount)
    {
        if (deltaAmount != 0)
            PopUpTextCreator.TextsToPopUp.Enqueue(($"{(deltaAmount > 0 ? "+" : "")}{deltaAmount} {item.ItemName}", Color.white));
        int newQuantity;
        if (item.ItemName == "�����")
        {
            var playerPosition = transform.position;
            playerPosition.x += 2;
            playerPosition.y = -7.316F;
            Instantiate(boatPrefab, playerPosition, transform.rotation);
            return;
        }

        if (item.ItemName == "������ ����� �����" && !Shop.WasCanvasOpenedBefore)
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
            if (inventory[item].itemObject != null)
                Destroy(inventory[item].itemObject);
            inventory[item] = (0, null);
            return;
        }
        else
        {
            if (inventory[item].itemObject == null)
            {
                inventory[item] = (newQuantity, Instantiate(itemPrefab, inventoryCanvas.transform));
                inventory[item].itemObject.GetComponentsInChildren<Image>()[1].sprite = item.Icon;
            }
            else
                inventory[item] = (newQuantity, inventory[item].itemObject);
        }

        inventory[item].itemObject.GetComponentInChildren<Text>().text = newQuantity == 1 ? "" : newQuantity.ToString();
    }

    public void AddDeltaItems(string itemName, int deltaAmount)
    {
        AddDeltaItems(Technical.GetItem(itemName), deltaAmount);
    }

    public int GetAmountOfItem(Item item)
    {
        if (inventory.ContainsKey(item))
            return inventory[item].quantity;

        AddDeltaItems(item, 0);
        return 0;
    }

    public int GetAmountOfItem(string itemName)
    {
        var item = Technical.GetItem(itemName);
        if (inventory.ContainsKey(item))
            return inventory[item].quantity;

        AddDeltaItems(item, 0);
        return 0;
    }


}