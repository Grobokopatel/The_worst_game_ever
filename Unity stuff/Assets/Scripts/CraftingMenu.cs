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

    private readonly List<(Text, Item)> allMaterials = new List<(Text, Item)>();

    public static CraftingMenu craftingMenu;

    public void UpdateItemsAmount()
    {
        foreach (var material in allMaterials)
        {
            material.Item1.text = $"{Player.player.GetAmountOfItem(material.Item2)}/{material.Item1.text.Split('/')[1]}";
        }
    }

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
        gameObject.SetActive(false);
        craftingMenu = this;
        var craftingRecipes = Resources.LoadAll<CraftingRecipe>("Prefabs/Crafting recipes");
        foreach (var craftingRecipe in craftingRecipes)
        {
            var craft = Instantiate(craftPrefab, craftHolder.transform);
            var firstResult = craftingRecipe.Results[0].Item;
            craft.GetComponentsInChildren<Image>(true)[1].sprite = firstResult.Icon;
            craft.GetComponentInChildren<Text>(true).text = firstResult.ItemName;
            craft.GetComponent<CraftButton>().ThisButtonRecipe = craftingRecipe;

            foreach (var craftingRecipeMaterial in craftingRecipe.Materials)
            {
                var material = Instantiate(materialPrefab, craft.GetComponentsInChildren<RectTransform>(true)[3].transform);
                allMaterials.Add((material.GetComponentInChildren<Text>(true), craftingRecipeMaterial.Item));

                material.GetComponentInChildren<Image>(true).sprite = craftingRecipeMaterial.Item.Icon;
                material.GetComponentInChildren<Text>(true).text = $"0/{craftingRecipeMaterial.Amount}";
            }
        }
        Debug.Log("Инициализация меню крафта");
    }
}
