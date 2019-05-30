using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Interactive Objects", order = 1)]
public class InteractiveObjectUI : ScriptableObject
{
    public string pickupText;
    public string lockText;
    public string useText;
    public string useText2;
    public string exitText;

    public bool directUse;
    public bool isUsing;
    public bool isLocked;
}