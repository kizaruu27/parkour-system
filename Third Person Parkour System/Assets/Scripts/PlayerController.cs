using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 500f;
    
    private CamerController cameraController;
    private Quaternion targetRotation;
    
    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CamerController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float moveAmmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

        Vector3 moveInput = (new Vector3(horizontal, 0, vertical)).normalized;
        Vector3 moveDir = cameraController.PlannarRotation * moveInput;


        if (moveAmmount > 0)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            targetRotation = Quaternion.LookRotation(moveDir);
        }
        transform.rotation = 
            Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
