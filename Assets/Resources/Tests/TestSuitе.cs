using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using NUnit.Framework;

public class TestSuite
{
    [UnityTest]
    public IEnumerator HasPlayerMoved()
    {
        var player = UnityEngine.Object.Instantiate(Resources.Load<Player>("Prefabs/Player"));
        var playerStartXCoord = player.gameObject.transform.position.x;
        player.Move(false, 1);
        yield return new WaitForSeconds(0.5F);
        var playerNewXCoord = player.gameObject.transform.position.x;
        Assert.Less(playerStartXCoord, playerNewXCoord);
    }

    [UnityTest]
    public IEnumerator HasPlayerPickedObject()
    {
        var player = UnityEngine.Object.Instantiate(Resources.Load<Player>("Prefabs/Player"));
        var log =  UnityEngine.Object.Instantiate(Resources.Load<Collectable>("Prefabs/Log"));

        var playerStartLogAmount = player.GetAmountOfItem("Log");
        player.CurrentInteractable = log;
        log.Interact();
        Assert.IsNull(log);
        var playerNewLogAmount = player.GetAmountOfItem("Log");
        Assert.Equals(playerStartLogAmount, 0);
        Assert.Equals(playerNewLogAmount, 1);
        yield return null;
    }

    [UnityTest]
    public IEnumerator HasPlayerCraftedItem()
    {
        var player = UnityEngine.Object.Instantiate(Resources.Load<Player>("Prefabs/Player"));
        player.AddDeltaItems("Log", 3);
        var playerStartLogAmount = player.GetAmountOfItem("Log");
        var playerStartAxeAmount = player.GetAmountOfItem("Axe");

        Assert.Equals(playerStartLogAmount, 3);
        Assert.Equals(playerStartAxeAmount, 0);

        var axeRecipe = Resources.Load<CraftingRecipe>("Prefabs/Crafting recipes/AxeRecipe");
        axeRecipe.Craft();

        var playerNewLogAmount = player.GetAmountOfItem("Log");
        var playerNewAxeAmount = player.GetAmountOfItem("Axe");

        Assert.Equals(playerNewLogAmount, 0);
        Assert.Equals(playerNewAxeAmount, 1);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerCantCraftIfHaveNotEnoughResources()
    {
        var player = UnityEngine.Object.Instantiate(Resources.Load<Player>("Prefabs/Player"));

        var playerStartLogAmount = player.GetAmountOfItem("Log");

        var axeRecipe = Resources.Load<CraftingRecipe>("Prefabs/Crafting recipes/AxeRecipe");

        Assert.IsFalse(axeRecipe.CanCraft());

        var playerNewLogAmount = player.GetAmountOfItem("Log");

        Assert.AreEqual(playerStartLogAmount, playerNewLogAmount);
        Assert.AreEqual(playerNewLogAmount, 0);
        yield return null;
    }
}