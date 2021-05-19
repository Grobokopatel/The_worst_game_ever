using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Technical
{
    public static IEnumerator WaitThenInvokeMethod(float seconds, Action whatToDoAfterDelay)
    {
        yield return new WaitForSeconds(seconds);
        whatToDoAfterDelay();
    }

    public static Item GetItem(this string itemName)
    {
        return Resources.Load<Item>($"Prefabs/Inventory items/{itemName}");
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