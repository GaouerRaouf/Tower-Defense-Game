using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float PlayerSpeed = 1.9f;
    [SerializeField] float PlayerSprint = 2f;
    [SerializeField] Transform playerCamera;

    [Header("Player Animator and Gravity")]
    [SerializeField] CharacterController cc;
    [SerializeField] float gravity = -9.81f;
    public Animator PlayerAnimator;

    [Header("Player Jumping and velocity")]
    [SerializeField] float turnCalmTime = 1f;
    float turnCalmVelocity;
    [SerializeField] Transform surfaceCheck;
    Vector3 velocity;
    bool onSurface;
    [SerializeField] float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;
    float jumpRange = 2;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
        PlayerMove();
        Jump();
        Sprint();
        Attack();
    }


    void PlayerMove()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        if (direction.magnitude > 0.1f)
        {
            PlayerAnimator.SetBool("Walk", true);
            PlayerAnimator.SetBool("Running", false);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cc.Move(moveDirection * PlayerSpeed * Time.deltaTime);

        }
        else
        {
            PlayerAnimator.SetBool("Walk", false);
            PlayerAnimator.SetBool("Running", false);
        }

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            PlayerAnimator.SetBool("Idle", false);
            PlayerAnimator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            PlayerAnimator.SetBool("Idle", true);
            PlayerAnimator.ResetTrigger("Jump");
        }

    }
    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            StartCoroutine(Attacking());
        }
    }
    void Sprint()
    {
        if (Input.GetButton("Sprint") && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && onSurface)
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

            if (direction.magnitude > 0.1f)
            {
                PlayerAnimator.SetBool("Walk", false);
                PlayerAnimator.SetBool("Running", true);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);


                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cc.Move(moveDirection * PlayerSprint * Time.deltaTime);

            }
            else
            {
                PlayerAnimator.SetBool("Walk", true);
                PlayerAnimator.SetBool("Running", false);
            }
        }
    }
    IEnumerator Attacking()
    {
        PlayerAnimator.SetTrigger("Attack");
        PlayerAnimator.SetBool("AttackBool", true);
        yield return new WaitForSeconds(1f);
        PlayerAnimator.ResetTrigger("Attack");
        PlayerAnimator.SetBool("AttackBool", false);
    }
}