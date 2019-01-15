using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
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
        menuCam = GameObject.Find("MenuCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            mainMenu.gameObject.SetActive(false);
            menuCam.enabled = false;
            playerController.GetComponent<FirstPersonController>().enabled = true;
            mainCam.enabled = true;
        }
    }
}
