using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Tools
{
    Shovel = 0,
    Axe = 1,
    Pickaxe = 2,
    Bomb = 3,
    Saw = 4
}

public enum InGameResources
{
    Log = 0,
    Stone = 1,
    Fish = 2,
}


public enum Directions
{
    Left = -1,
    Right = 1,
}

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3F;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer sprite;

    InteractableObject currentInteractable;
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
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (CurrentInteractable != null && Input.GetKeyDown(KeyCode.E))
            CurrentInteractable.Interact(this);

        if (Input.GetButton("Horizontal"))
            Move();
    }

    private void Move()
    {
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

    private readonly HashSet<int> tools = new HashSet<int>();

    private readonly Dictionary<int, int> resources = new Dictionary<int, int>();

    private float currentPosition;
    public float CurrentPosition
    {
        get => currentPosition;
        private set
        {
            currentPosition = value;
        }
    }

    public void AddTool(Tools tool)
    {
        tools.Add((int)tool);
    }

    public bool HasTool(Tools tool)
    {
        return tools.Contains((int)tool);
    }

    public void AddDeltaResources(InGameResources item, int deltaAmount)
    {
        var itemId = (int)item;
        if (resources.ContainsKey(itemId))
            resources[itemId] += deltaAmount;
        else
        {
            if (deltaAmount < 0)
                throw new InvalidOperationException();
            resources[itemId] = deltaAmount;
        }
    }

    public int GetAmountOfResource(InGameResources item)
    {
        var itemId = (int)item;
        if (resources.ContainsKey(itemId))
            return resources[itemId];

        AddDeltaResources(item, 0);
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



    public void TeleportTo(float coord)
    {
        currentPosition = coord;
    }
}