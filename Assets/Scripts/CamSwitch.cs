using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

//invoked by timeline activation track
public class CamSwitch : MonoBehaviour
{
    private Transform playerController;
    private Transform introMenu;
    
    private Camera playerCam;
    private Camera menuCam;
    
    private void Start()
    {
        playerController = GameObject.Find("FPSController").transform;
        playerCam = playerController.GetChild(0).GetComponent<Camera>();
        
        introMenu = GameObject.Find("IntroMenu").transform;
        menuCam = GameObject.Find("GameManager").transform.GetChild(0).GetComponent<Camera>();
        
        playerController.GetComponent<FirstPersonController>().enabled = true;
        playerCam.enabled = true;
        
        introMenu.gameObject.SetActive(false);
        menuCam.gameObject.SetActive(false);
    }
}