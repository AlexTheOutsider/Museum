using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TelescopeNew : Interactive
{
    public Transform mainCam;
    public Transform telescopeCam;
	
    private bool isActivated = false;

    protected override void Function()
    {
        base.Function();
        Activate();
    }
	
    protected override void InitializeItemMenu()
    {
        base.InitializeItemMenu();
        dictPickup.Add("Text","Activate");
        dictUse.Add("Function 1","Move");
        dictUse.Add("Function 2","Zoom");
        dictUse.Add("Exit","Exit");
    }
    
    protected override void ContextMenuUpdate()
    {
        base.ContextMenuUpdate();
        itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
        itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.UsePanel, dictUse);

        if (isActivated == false)
        {
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel, true);
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel, false);
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel, false);
        }
        else
        {
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel, false);
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel, false);
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel, true);
        }
    }

    private void Activate()
    {
        if (Input.GetKeyDown(KeyCode.E) && (isActivated == false))
        {
            mainCam.parent.gameObject.SetActive(false);
            telescopeCam.gameObject.SetActive(true);
            isActivated = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && (isActivated == true))
        {
            mainCam.parent.gameObject.SetActive(true);
            telescopeCam.gameObject.SetActive(false);
            isActivated = false;
        }
    }
}