﻿using System;
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
    public float jumpForce;

    private float _pitchAngle;

    [HideInInspector] public bool isGamePaused = false;

    public new Transform transform;
    public GameObject playerCamera;
    public new Rigidbody rigidbody;

    private bool _isGrounded = true;

    private void OnCollisionEnter(Collision other)
    {
        foreach (ContactPoint contact in other.contacts)
        {
            Debug.Log(contact.otherCollider.gameObject.name);
            if (contact.otherCollider.gameObject.layer == 8)
                _isGrounded = true;
        }
    }
    
    private void Start()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (o != gameObject)
            {
                foreach (CapsuleCollider c1 in GetComponents<CapsuleCollider>())
                {
                    foreach (CapsuleCollider c2 in o.GetComponents<CapsuleCollider>())
                        Physics.IgnoreCollision(c1, c2);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer || isGamePaused) return;

        //MOVEMENTS JOUEUR
        float speedH = Input.GetAxis("Horizontal");
        float speedV = Input.GetAxis("Vertical");

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

        Vector3 movement = speedH * transform.right + speedV * transform.forward;
        if (movement.magnitude > 1)
            movement /= movement.magnitude;

        movement *= (speed * Time.deltaTime);
        movement.y = rigidbody.velocity.y;

        rigidbody.velocity = movement;

        if (_isGrounded && Input.GetAxisRaw("Jump") > .9)
        {
            _isGrounded = false;
            rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        //MOUVEMENTS CAMERA
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 yaw = new Vector3(0f, x, 0f);
        transform.Rotate(yaw * (mouseSensitivityX * Time.deltaTime * 10f));

        _pitchAngle += -y * mouseSensitivityY * Time.deltaTime * 10f;
        _pitchAngle = Mathf.Clamp(_pitchAngle, minPitch, maxPitch);
        playerCamera.transform.localEulerAngles = new Vector3(_pitchAngle, 0f, 0f);
    }
}