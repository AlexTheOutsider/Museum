using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody rb;
    private CharacterController characterController;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    [SerializeField] private Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            //rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
            //transform.Translate(velocity * Time.fixedDeltaTime,Space.World);
            characterController.Move(velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        if (rotation != Vector3.zero)
        {
            //rb.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
            transform.Rotate(rotation);
        }

        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
}