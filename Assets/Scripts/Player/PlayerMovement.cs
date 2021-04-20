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
    public float gravity;
    public float jump_height;
    

    private float _pitchAngle;
    
    [HideInInspector]
    public bool isGamePaused = false;

    private Animator _animator;
    private CharacterController _characterController;
    private Transform _transform;
    private GameObject _playerCamera;

    public void Init()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = transform;
        _playerCamera = _transform.Find("Camera").gameObject;
        _animator=GetComponent<Animator>();
    }

    private void Update()
    {
        //mouvement terrestre
        if (!isLocalPlayer || isGamePaused) return;

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
                _animator.SetBool("iscrouching",true);
                break;
            case 1:
                speed = maxSprintSpeed;
                _animator.SetBool("isrunning",true);
                break;
            default:
                speed = maxWalkSpeed;
                _animator.SetBool("iswalking",true);
                break;
        }
        speed *= Math.Max(absSpeedH, absSpeedV);
        
        //jump
        if (!(_characterController.isGrounded)&& movement.y<0)
        {
            movement.y = -0.2f;
        }
        if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            _animator.SetBool("isjumping",true);
            movement.y = Mathf.Sqrt(jump_height * -2f * gravity);
        }

        //methode move
        _characterController.Move(movement * (speed * Time.deltaTime));
        
        
        //camera
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 yaw = new Vector3(0f, x, 0f);
        transform.Rotate(yaw * (mouseSensitivityX * Time.deltaTime * 10f));

        _pitchAngle += -y * mouseSensitivityY * Time.deltaTime * 10f;
        _pitchAngle = Mathf.Clamp(_pitchAngle, minPitch, maxPitch);
        _playerCamera.transform.localEulerAngles = new Vector3(_pitchAngle, 0f, 0f);
    }
}