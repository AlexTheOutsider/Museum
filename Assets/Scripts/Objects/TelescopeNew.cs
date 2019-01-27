using UnityEngine;

public class TelescopeNew : Interactive
{
    public Transform playerCam;
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
        
        ItemMenuManager.Instance.UpdateMenu(ItemMenuManager.ItemMenuType.PickupPanel, dictPickup);
        ItemMenuManager.Instance.UpdateMenu(ItemMenuManager.ItemMenuType.UsePanel, dictUse);

        if (isActivated == false)
        {
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel, true);
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel, false);
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel, false);
        }
        else
        {
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.PickupPanel, false);
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.LockedPanel, false);
            ItemMenuManager.Instance.ToggleDisplay(ItemMenuManager.ItemMenuType.UsePanel, true);
        }
    }

    private void Activate()
    {
        if (Input.GetKeyDown(KeyCode.E) && (isActivated == false))
        {
            playerCam.parent.gameObject.SetActive(false);
            telescopeCam.gameObject.SetActive(true);
            isActivated = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && (isActivated == true))
        {
            playerCam.parent.gameObject.SetActive(true);
            telescopeCam.gameObject.SetActive(false);
            isActivated = false;
        }
    }
}