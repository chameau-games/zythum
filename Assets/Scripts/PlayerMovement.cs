using System;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float speed;
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

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 movement = h * _transform.right + v * _transform.forward;
        _characterController.Move(movement.normalized * (speed * Time.deltaTime));

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 yaw = new Vector3(0, x, 0);
        transform.Rotate(yaw * (mouseSensitivityX * Time.deltaTime * 10));

        _pitchAngle += -y * mouseSensitivityY * Time.deltaTime * 10;
        _pitchAngle = Mathf.Clamp(_pitchAngle, minPitch, maxPitch);
        _camera.transform.localEulerAngles = new Vector3(_pitchAngle, 0, 0);
    }
}