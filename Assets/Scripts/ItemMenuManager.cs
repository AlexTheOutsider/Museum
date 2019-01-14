using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuManager : MonoBehaviour
{
    private Transform pickupPanel;
    private Transform lockedPanel;
    private Transform usePanel;

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
}