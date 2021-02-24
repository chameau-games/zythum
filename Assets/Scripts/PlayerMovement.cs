using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float speed;
    public float mouseSensitivityX;
    public float mouseSensitivityY;
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
    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = h * _transform.right + v * _transform.forward;
        _characterController.Move(movement*speed);

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 yaw = new Vector3(0, x, 0);
        transform.Rotate(yaw*mouseSensitivityX);

        Vector3 pitch = new Vector3(-y, 0, 0);
        _camera.transform.Rotate(pitch*mouseSensitivityY);
    }
}