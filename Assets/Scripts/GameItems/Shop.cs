using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    [SerializeField]
    private TradingMenu tradingMenu;
    [SerializeField]
    private CameraController cameraController;

    public override void Interact(Player player)
    {
        tradingMenu.gameObject.SetActive(true);
        tradingMenu.UpdateItemsAmount();
        Player.player.enabled = false;
        cameraController.XOffset = 3.5F;
        
        #region
        /*GetComponent<FirstPersonController>().enabled = !canvas.active;
        if (canvas.active)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(holder.GetComponent<RectTransform>());
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }*/

        /*for (int i = 0; i < GlobalItemsList.gl.allItem1sList.Count; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                AddItem1(GlobalItemsList.gl.allItem1sList[i].Clone());
            }
        }*/
        #endregion
    }
}
