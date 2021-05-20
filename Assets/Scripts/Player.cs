using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3F;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider2D collider;
    public static Player player;
    public readonly static float ConstantYPosition = -6.283F;
    [SerializeField]
    private GameObject fishingGamePrefab;
    [SerializeField]
    private GameObject boat;
    private Bobber bobber;
    [SerializeField]
    private GameObject boatPrefab;
    public PlayerState State
    {
        get => (PlayerState)animator.GetInteger("State");
        set
        {
            switch (value)
            {
                case PlayerState.Idle:
                    var newPosition = player.transform.position;
                    newPosition.y = ConstantYPosition;
                    transform.position = newPosition;
                    break;

                case PlayerState.Run:

                    break;

                case PlayerState.InBoat:
                    sprite.sortingOrder = 11;
                    break;

                case PlayerState.Fishing:
                    Instantiate(fishingGamePrefab);
                    break;
            }
            animator.SetInteger("State", (int)value);
        }
    }

    public Interactable CurrentInteractable
    {
        get;
        set;
    }

    private void Awake()
    {
        AddDeltaItems("Log", 5);
        AddDeltaItems("Rock", 5);
        AddDeltaItems("FishingRod", 1);
        player = this;
        collider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bobber = GetComponentInChildren<Bobber>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.F) && player.GetAmountOfItem("FishingRod") >= 1 && State != PlayerState.InBoat) || State == PlayerState.Fishing)
        {
            if (State != PlayerState.Fishing)
            {
                State = PlayerState.Fishing;
            }

            return;
        }

        if (State != PlayerState.InBoat)
            State = PlayerState.Idle;
        else if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            && GetCollidersInPosition(transform.position + transform.up + 0.75F * transform.right).Length >= 1
            && GetCollidersInPosition(transform.position + transform.up - 0.75F * transform.right).Length >= 1
            && GetCollidersInPosition(transform.position + transform.up).Length >= 1
            )
        {
            Instantiate(boat, transform.position, transform.rotation);
            State = PlayerState.Idle;
            sprite.sortingOrder = 5;
        }

        if (CurrentInteractable != null && Input.GetKeyDown(KeyCode.E))
            CurrentInteractable.Interact(this);

        if (Input.GetButton("Horizontal"))
        {
            if (State == PlayerState.InBoat)
                Swim();
            else
                Walk();
        }
    }

    private void Swim()
    {
        Move(false);
    }

    private void Walk()
    {
        State = PlayerState.Run;
        Move(true);
    }

    private void Move(bool shouldCheckOnCollidersAhead)
    {
        var deltaMovement = transform.right * Input.GetAxis("Horizontal");
        sprite.flipX = deltaMovement.x < 0;

        var colliderOffset = collider.offset;
        colliderOffset.x = (float)Math.Abs(colliderOffset.x) * (sprite.flipX ? 1 : -1);
        collider.offset = colliderOffset;

        var bobberCoords = bobber.transform.localPosition;
        bobberCoords.x = (float)Math.Abs(bobberCoords.x) * (sprite.flipX ? -1 : 1);
        bobber.transform.localPosition = bobberCoords;

        if (!shouldCheckOnCollidersAhead || GetCollidersInPosition(transform.position + 0.5F * deltaMovement.normalized + (-0.5F) * transform.up).Length >= 1)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + deltaMovement, speed * Time.deltaTime);
    }


    public static Collider2D[] GetCollidersInPosition(Vector3 position)
    {
        return Physics2D.OverlapCircleAll(position, 0.1F);
    }

    private readonly Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    public void AddDeltaItems(Item item, int deltaAmount)
    {
        if (item.ItemName == "Лодка")
        {
            var playerPosition = transform.position;
            playerPosition.x += 2;
            playerPosition.y = -7.316F;
            Instantiate(boatPrefab, playerPosition, transform.rotation);
        }
        else if (inventory.ContainsKey(item))
            inventory[item] += deltaAmount;
        else
            inventory[item] = deltaAmount;
    }

    public void AddDeltaItems(string itemName, int deltaAmount)
    {
        var item = Technical.GetItem(itemName);
        if (inventory.ContainsKey(item))
            inventory[item] += deltaAmount;
        else
            inventory[item] = deltaAmount;
    }

    public int GetAmountOfItem(Item item)
    {
        if (inventory.ContainsKey(item))
            return inventory[item];

        AddDeltaItems(item, 0);
        return 0;
    }

    public int GetAmountOfItem(string itemName)
    {
        var item = Technical.GetItem(itemName);
        if (inventory.ContainsKey(item))
            return inventory[item];

        AddDeltaItems(item, 0);
        return 0;
    }
}