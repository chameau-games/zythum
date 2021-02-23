using System;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 0.05f;
    private CharacterController _characterController;

    private void Start()
    {
        if (isLocalPlayer)
        {
            _characterController = GetComponent<CharacterController>();
        }
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        _characterController.Move(dir*speed);
    }
}