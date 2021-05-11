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
    public static Player player;

    [SerializeField]
    private GameObject boat;
    public PlayerState State
    {
        get => (PlayerState)animator.GetInteger("State");
        set
        {
            animator.SetInteger("State", (int)value);
        }
    }

    private InteractableObject currentInteractable;
    public InteractableObject CurrentInteractable
    {
        get => currentInteractable;
        set
        {
            currentInteractable = value;
        }
    }

    private void Awake()
    {
        player = this;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (player.State != PlayerState.InBoat)
            State = PlayerState.Idle;
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && GetCollidersInPosition(transform.position + 1.5F * transform.up).Length >= 1)
            {
                Instantiate(boat, transform.position, transform.rotation);
                player.transform.position += 1.5F * transform.up;
                player.State = PlayerState.Idle;
            }
        }

        if (CurrentInteractable != null && Input.GetKeyDown(KeyCode.E))
            CurrentInteractable.Interact(this);

        if (Input.GetButton("Horizontal"))
        {
            if (State == PlayerState.InBoat)
                Swim();
            else
                Move();
        }
    }

    private void Swim()
    {
        var deltaMovement = transform.right * Input.GetAxis("Horizontal");
        sprite.flipX = deltaMovement.x < 0;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + deltaMovement, speed * Time.deltaTime);
    }

    private void Move()
    {
        State = PlayerState.Run;
        var deltaMovement = transform.right * Input.GetAxis("Horizontal");
        var collidersAmount = GetCollidersInPosition(transform.position + deltaMovement.normalized * 0.5F + transform.up * (-0.5F)).Length;
        sprite.flipX = deltaMovement.x < 0;
        if (collidersAmount >= 1)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + deltaMovement, speed * Time.deltaTime);
    }

    private Collider2D[] GetCollidersInPosition(Vector3 position)
    {
        return Physics2D.OverlapCircleAll(position, 0.1F);
    }

    private readonly Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    public void AddDeltaItems(Item item, int deltaAmount)
    {
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

    //public float GetPositionAfterMovement(float currentPosition, float nextPosition)
    //{
    //    var greaterCord = Math.Max(currentPosition, nextPosition);
    //    var lesserCord = Math.Min(currentPosition, nextPosition);
    //
    //    return currentSubWorld.Barriers.Any(barrierCord => lesserCord <= barrierCord && barrierCord <= greaterCord)
    //        ? currentPosition : nextPosition;
    //}
}