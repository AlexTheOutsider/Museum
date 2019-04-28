using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

//invoked by timeline activation track
public class CamSwitch : MonoBehaviour
{

    
    private void Start()
    {
        Intro.Instance.gameState = Intro.State.Playing;
        EventManager.Instance.TriggerEvent("SwitchEvent");
    }
}