using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public float interactiveRange = 2f;
    
    protected Transform player;
    protected float distance;
    protected bool shouldMenuBeClosed = true;
    
    protected Transform itemMenu;
    protected ItemMenuManager itemMenuManager;
    protected Dictionary<String, String> dictPickup;
    protected Dictionary<String, String> dictLocked;
    protected Dictionary<String, String> dictUse;
    protected virtual void Start()
    {
	    player = GameObject.FindWithTag("Player").transform.GetChild(0);
	    itemMenu = GameObject.Find("ItemMenu").transform;
	    InitializeItemMenu();
    }

    protected virtual void Update ()
    {
	    distance = Vector3.Distance(transform.position, player.Find("Guide").position);
	    if (distance < interactiveRange)
	    {
		    OnInteractiveObjEnter();
	    }
	    else
	    {
		    OnInteractiveObjExit();
	    }
    }
    
    protected virtual void OnInteractiveObjEnter()
    {
	    ContextMenuUpdate();
	    Function();
    }
	
    protected virtual void OnInteractiveObjExit()
    {	
	    if (shouldMenuBeClosed)
	    {
		    itemMenu.gameObject.SetActive(false);
		    shouldMenuBeClosed = false;
	    }
    }

    protected virtual void ContextMenuUpdate()
    {
	    itemMenu.gameObject.SetActive(true);
	    shouldMenuBeClosed = true;
	    //Expanded by child class
    }
	
    protected virtual void InitializeItemMenu()
    {
	    itemMenuManager = itemMenu.GetComponent<ItemMenuManager>();
	    dictPickup = new Dictionary<String, String>();
	    dictLocked = new Dictionary<String, String>();
	    dictUse = new Dictionary<String, String>();
		//Expanded by child class
    }

    protected virtual void Function()
    {
	    //Expanded by child class
    }
}