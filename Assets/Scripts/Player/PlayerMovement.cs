using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviourPun
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

        private bool _isGamePaused = false;

        public new Transform transform;
        public GameObject playerCamera;
        private Rigidbody _rigidbody;

        private bool _isGrounded = true;
        private bool _canControl = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
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

        private void OnCollisionEnter(Collision other)
        {
            foreach (ContactPoint contact in other.contacts)
            {
                if (contact.otherCollider.gameObject.layer == 8)
                    _isGrounded = true;
            }
        }
        
        private void OnDisable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void OnEnable()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
       

        private void Update()
        {
            if (photonView.IsMine && _canControl)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    _isGamePaused = true;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    _isGamePaused = false;
                }
            }
        }

        private void FixedUpdate()
        {
            if (!photonView.IsMine || _isGamePaused) return;

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
            movement.y = _rigidbody.velocity.y;

            _rigidbody.velocity = movement;

            if (_isGrounded && Input.GetAxisRaw("Jump") > .9)
            {
                _isGrounded = false;
                _rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
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
}