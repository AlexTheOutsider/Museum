using UnityEngine;

public class Switch : Interactive
{
    public Projector projector;

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
        ItemMenuManager.Instance.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
        ItemMenuManager.Instance.UpdateMenu(ItemMenuManager.ItemMenuType.LockedPanel, dictLocked);
		
        if (isActivated == false)
        {
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel, true);
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel, false);
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel, false);
        }
        else
        {
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel, false);
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel, true);
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel, false);
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