using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_script : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed;
    public float jumpSpeed;
    public float gravity;
    public float turnSmoothTime;
    float turnSmoothVelocity;
    public InputAction playerControls;

    private Vector3 velocity;

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        MovePlayer();

        // Gravitace
        velocity.y -= gravity * Time.deltaTime;

        // Použití řízeného pohybu na kontroleru postavy
        controller.Move(velocity * Time.deltaTime);
    }

    private void MovePlayer()
    {
        Vector3 direction = playerControls.ReadValue<Vector3>();

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = jumpSpeed;
        }
    }
}