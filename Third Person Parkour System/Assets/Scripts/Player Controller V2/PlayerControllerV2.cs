using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    [Header("Move Value")] 
    public float moveSpeed = 5f;
    public float turnSpeed = 500f;
    [SerializeField] private CharacterController characterController;
    
    //private value
    private Quaternion targetRotation;
    private float yVelocity;
    
    [Header("GroundCheck")]
    private bool isGrounded;
    public float groundCheckRadius;
    public Vector3 groundCheckOffset;
    public LayerMask whatIsGround;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    [Header("Camera")] 
    [SerializeField] private CameraV2 _cameraV2;
    

    private void Update()
    {
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float moveAmmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        Vector3 moveInput = (new Vector3(horizontal, 0, vertical)).normalized;
        
        GroundChecker();
        Debug.Log(isGrounded);

        if (isGrounded)
        {
            yVelocity = -0.5f;
        }
        else
        {
            yVelocity += Physics.gravity.y * Time.deltaTime;
        }

        Vector3 moveDir = _cameraV2.PlannarRotation * moveInput;
        
        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = yVelocity;

        characterController.Move(velocity * Time.deltaTime);
        
        if (moveAmmount > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        
        anim.SetFloat("moveAmmount", moveAmmount, 0.2f, Time.deltaTime);
    }

    void GroundChecker()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }
}
