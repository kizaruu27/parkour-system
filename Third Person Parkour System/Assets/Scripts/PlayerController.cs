using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 500f;
    
    private CamerController cameraController;
    private Quaternion targetRotation;
    private Animator anim;
    private CharacterController controller;

    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;

    private bool isGrounded;
    private float yVelocity;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CamerController>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float moveAmmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        Vector3 moveInput = (new Vector3(horizontal, 0, vertical)).normalized;
        Vector3 moveDir = cameraController.PlannarRotation * moveInput;
        
        GroundCheck();
        Debug.Log("Is Grounded: " + isGrounded);

        if (isGrounded)
        {
            yVelocity = -0.5f;
        }
        else
        {
            yVelocity += Physics.gravity.y * Time.deltaTime;
        }

        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = yVelocity;

        controller.Move( velocity * Time.deltaTime);
        
        if (moveAmmount > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }
        transform.rotation = 
            Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        
        anim.SetFloat("moveAmmount", moveAmmount, 0.2f, Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }
}
