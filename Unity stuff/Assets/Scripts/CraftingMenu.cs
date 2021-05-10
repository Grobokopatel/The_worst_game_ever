using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMenu : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private GameObject craftPrefab;

    [SerializeField]
    private GameObject materialPrefab;

    [SerializeField]
    private GameObject craftHolder;

    [SerializeField]
    private GameObject materialsHolder;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Player.player.enabled = true;
            cameraController.XOffset = 0;
        }
    }

    private void Awake()
    {
        var craftingRecipes = Resources.LoadAll<CraftingRecipe>("Crafting Recipes");
        foreach (var craftingRecipe in craftingRecipes)
        {
            var craft = Instantiate(craftPrefab, craftHolder.transform);
            var firstResult = craftingRecipe.Results[0].Item;
            craft.GetComponentsInChildren<Image>(true)[1].sprite = firstResult.Icon;
            craft.GetComponentInChildren<Text>(true).text = firstResult.ItemName;

            foreach(var craftingRecipeMaterial in craftingRecipe.Materials)
            { 
                var material = Instantiate(materialPrefab, craft.GetComponentsInChildren<RectTransform>(true)[3].transform);

                material.GetComponentInChildren<Image>(true).sprite = craftingRecipeMaterial.Item.Icon;
                material.GetComponentInChildren<Text>(true).text = $"0/{craftingRecipeMaterial.Amount}";
            }
        }
    }
}
