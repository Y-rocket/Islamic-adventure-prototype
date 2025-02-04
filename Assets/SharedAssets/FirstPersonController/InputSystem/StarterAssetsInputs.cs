using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssets : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        [Header("Jump Settings")]
        public float jumpForce = 7f;             // Jump strength
        public LayerMask groundLayer;           // Layer for detecting ground
        public Transform groundCheck;           // Transform for ground detection
        public float groundCheckRadius = 0.3f;  // Radius for ground check sphere

        private Rigidbody _rigidbody;           // Rigidbody reference
        private bool _isGrounded;               // Check if the player is grounded

        private void Awake()
        {
            // Get the Rigidbody component
            _rigidbody = GetComponent<Rigidbody>();

            // Ensure ground check transform exists
            if (!groundCheck)
            {
                Debug.LogWarning("GroundCheck transform is not assigned. Please assign it in the inspector.");
            }

            // Configure Rigidbody constraints
            if (_rigidbody)
            {
                _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        private void Update()
        {
            // Check if the player is grounded
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

            // Handle jump logic
            if (jump && _isGrounded)
            {
                PerformJump();
                jump = false; // Reset jump flag
            }
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }
#endif

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void PerformJump()
        {
            if (_rigidbody == null) return;

            // Reset vertical velocity before applying jump force
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = 0f;
            _rigidbody.velocity = velocity;

            // Apply jump force
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump performed!");
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        private void OnDrawGizmosSelected()
        {
            // Visualize the ground check sphere in the Scene view
            if (groundCheck)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}
