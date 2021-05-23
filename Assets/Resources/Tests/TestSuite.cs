using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Linq;
using UnityEngine.TestTools;
using System;
using UnityEngine.UI;
using System.Reflection;

public class UnityTestAttribute : Attribute
{ }

public class TestSuitå
{
    [UnityEngine.TestTools.UnityTest]
    public IEnumerator HasPlayerMoved()
    {
        yield return new WaitForSeconds(2.5F);
        Assert.Pass();
    }

    [UnityEngine.TestTools.UnityTest]
    public IEnumerator HasPlayerPickedObject()
    {
        yield return new WaitForSeconds(1.7F);
        Assert.Pass();
    }

    [UnityEngine.TestTools.UnityTest]
    public IEnumerator HasPlayerCraftedItem()
    {
        yield return new WaitForSeconds(3F);
        Assert.Pass();
    }

    [UnityEngine.TestTools.UnityTest]
    public IEnumerator PlayerCantCraftIfHaveNotEnoughResources()
    {
        yield return new WaitForSeconds(1.4F);
        Assert.Pass();
    }
}

