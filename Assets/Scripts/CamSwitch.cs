using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CamSwitch : MonoBehaviour
{
    private Transform playerController;
    private Camera mainCam;
    private Transform mainMenu;
    private Camera menuCam;
    
    private void Start()
    {
        playerController = GameObject.Find("FPSController").transform;
        mainCam = playerController.GetChild(0).GetComponent<Camera>();
        mainMenu = GameObject.Find("MainMenu").transform;
        menuCam = GameObject.Find("GameManager").transform.GetChild(1).GetComponent<Camera>();
        
        playerController.GetComponent<FirstPersonController>().enabled = true;
        mainCam.enabled = true;
        
        mainMenu.gameObject.SetActive(false);
        menuCam.enabled = false;
    }
}