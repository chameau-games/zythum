using System;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float maxWalkSpeed;
    public float maxSprintSpeed;
    public float maxCrouchSpeed;
    public float mouseSensitivityX;
    public float mouseSensitivityY;
    public float minPitch;
    public float maxPitch;

    private float _pitchAngle;
    
    [HideInInspector]
    public bool isGamePaused = false;
    
    private CharacterController _characterController;
    private Transform _transform;
    private Camera _camera;

    private void Start()
    {
        if (isLocalPlayer)
        {
            _characterController = GetComponent<CharacterController>();
            _transform = transform;
            _camera = GetComponentInChildren<Camera>();
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        if (isGamePaused) return;

        float speedH = Input.GetAxis("Horizontal");
        float absSpeedH = Math.Abs(speedH);
        float speedV = Input.GetAxis("Vertical");
        float absSpeedV = Math.Abs(speedV);
        
        Vector3 movement = (speedH * _transform.right + speedV * _transform.forward).normalized;

        float speed;
        switch (Input.GetAxisRaw("Speed"))
        {
            case -1:
                speed = maxCrouchSpeed;
                break;
            case 1:
                speed = maxSprintSpeed;
                break;
            default:
                speed = maxWalkSpeed;
                break;
        }
        speed *= Math.Max(absSpeedH, absSpeedV);

        _characterController.Move(movement * (speed * Time.deltaTime));

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 yaw = new Vector3(0, x, 0);
        transform.Rotate(yaw * (mouseSensitivityX * Time.deltaTime * 10));

        _pitchAngle += -y * mouseSensitivityY * Time.deltaTime * 10;
        _pitchAngle = Mathf.Clamp(_pitchAngle, minPitch, maxPitch);
        _camera.transform.localEulerAngles = new Vector3(_pitchAngle, 0, 0);
    }
}