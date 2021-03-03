using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _gravity;
    void Start()
    {
        _gravity = Vector3.zero;
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        _gravity += Physics.gravity * Time.deltaTime;
        if (!_characterController.isGrounded)
        {
            _characterController.Move(_gravity);
        }
        else
        {
            _gravity = Vector3.zero;
        }
    }
}
