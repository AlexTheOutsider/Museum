using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuManager : Singleton<ItemMenuManager>
{
    private Transform pickupPanel;
    private Transform lockedPanel;
    private Transform usePanel;
    private List<Text> textList;

    public enum ItemMenuType
    {
        PickupPanel,
        LockedPanel,
        UsePanel
    };

    private void Start()
    {
        pickupPanel = transform.Find("PickupPanel");
        lockedPanel = transform.Find("LockedPanel");
        usePanel = transform.Find("UsePanel");

        textList = new List<Text>
        {
            pickupPanel.Find("Text").GetComponent<Text>(),
            lockedPanel.Find("Text").GetComponent<Text>(),
            usePanel.Find("Function 1").GetComponent<Text>(),
            usePanel.Find("Function 2").GetComponent<Text>(),
            usePanel.Find("Exit").GetComponent<Text>()
        };
    }

    public void ToggleDisplay(ItemMenuType type, bool turnOn)
    {
        switch (type)
        {
            case ItemMenuType.PickupPanel:
                pickupPanel.gameObject.SetActive(turnOn);
                break;
            
            case ItemMenuType.LockedPanel:
                lockedPanel.gameObject.SetActive(turnOn);
                break;
            
            case ItemMenuType.UsePanel:
                usePanel.gameObject.SetActive(turnOn);
                break;
        }
    }

    public void UpdateMenu(ItemMenuType type, Dictionary<String, String> functionList)
    {
        switch (type)
        {
            case ItemMenuType.PickupPanel:
                pickupPanel.Find("Text").GetComponent<Text>().text = functionList["Text"];
                break;
            
            case ItemMenuType.LockedPanel:
                lockedPanel.Find("Text").GetComponent<Text>().text = functionList["Text"];
                break;
            
            case ItemMenuType.UsePanel:
                usePanel.Find("Function 1").GetComponent<Text>().text = functionList["Function 1"];
                usePanel.Find("Exit").GetComponent<Text>().text = functionList["Exit"];
                
                if (!functionList.ContainsKey("Function 2")) 
                    usePanel.Find("Function 2").gameObject.SetActive(false);
                else
                {
                    usePanel.Find("Function 2").GetComponent<Text>().text = functionList["Function 2"];
                    usePanel.Find("Function 2").gameObject.SetActive(true);
                }
                break;
        }
    }

    public void UpdateMenu(InteractiveObjectUI ui)
    {
        pickupPanel.Find("Text").GetComponent<Text>().text = ui.pickupText;
        lockedPanel.Find("Text").GetComponent<Text>().text = ui.lockText;
        usePanel.Find("Function 1").GetComponent<Text>().text = ui.useText;
        usePanel.Find("Function 2").GetComponent<Text>().text = ui.useText2;
        usePanel.Find("Exit").GetComponent<Text>().text = ui.exitText;
        
        foreach (Text text in textList)
        {
            text.gameObject.SetActive(text.text != "");
        }

        if (!ui.directUse)
        {
            ToggleDisplay(ItemMenuType.PickupPanel, !ui.isLocked && !ui.isUsing);
            ToggleDisplay(ItemMenuType.UsePanel, ui.isUsing);
            ToggleDisplay(ItemMenuType.LockedPanel, ui.isLocked);
        }
        else
        {
            ToggleDisplay(ItemMenuType.PickupPanel, true);
            ToggleDisplay(ItemMenuType.LockedPanel, false);
            ToggleDisplay(ItemMenuType.UsePanel, false);
        }
    }
}