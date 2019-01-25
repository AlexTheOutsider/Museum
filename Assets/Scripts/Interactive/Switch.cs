using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Switch : Interactive
{
    public ProjectorController projector;

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
        dictLocked.Add("Text","This switch has already turned on.");
    }
    
    protected override void ContextMenuUpdate()
    {
        base.ContextMenuUpdate();
        itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
        itemMenuManager.UpdateMenu(ItemMenuManager.ItemMenuType.LockedPanel, dictLocked);
		
        if (isActivated == false)
        {
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel, true);
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel, false);
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel, false);
        }
        else
        {
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel, false);
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel, true);
            itemMenuManager.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel, false);
        }
    }

    private void Activate()
    {
        if (Input.GetKey(KeyCode.E) && (isActivated == false))
        {
            //projector.ActivateSwitch();
            EventManager.Instance.TriggerEvent("Switch");
			
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
            transform.GetChild(0).GetComponent<Light>().color = Color.blue;
            isActivated = true;
        }
    }
}