using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensibility = 3f;
    public bool lockCursor = true;
    private bool m_lockCursor = true;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float yMov = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * yMov;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;
        motor.Move(velocity);

        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensibility;
        motor.Rotate(rotation);
        
        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensibility;
        motor.RotateCamera(cameraRotation);
        
        UpdateCursorLock();
    }

    private void UpdateCursorLock()
    {
        if(!lockCursor)
            return;
        
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            m_lockCursor = false;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            m_lockCursor = true;
        }

        if (m_lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}