using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Technical
{
    public static IEnumerator WaitThenInvokeMethod(float seconds, Action whatToDoAfterDelay)
    {
        yield return seconds == 0 ? null : new WaitForSeconds(seconds);
        whatToDoAfterDelay();
    }

    public static Item GetItem(this string itemName)
    {
        return Resources.Load<Item>($"Prefabs/Inventory items/{itemName}");
    }

    public static Collider2D[] GetCollidersInPosition(Vector3 position)
    {
        return Physics2D.OverlapCircleAll(position, 0.1F);
    }

    public static string GetItemNameWithoutAdditInfo(this string name)
    {
        return name.Split(new[] { ' ', '(' })[0];
    }
}

public class Timer
{
    private double leftTime;
    public double LeftTime
    {
        get
        {
            if (leftTime > 0)
                leftTime -= Time.deltaTime;
            return leftTime;
        }
    }

    public bool WasOverOnThisFrame()
    {
        if (leftTime + Time.deltaTime > 0)
        {
            leftTime -= 1000;
            return true;
        }
        return false;
    }

    public Timer(double leftTime)
    {
        this.leftTime = leftTime;
    }
}