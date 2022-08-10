using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 500f;

    private Quaternion targetRotation;
    private CamerController _camerController;

    private void Awake()
    {
        _camerController = Camera.main.GetComponent<CamerController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float moveAmmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

        Vector3 moveInput = (new Vector3(horizontal, 0, vertical)).normalized;
        Vector3 moveDir = _camerController.PlannarRotation * moveInput;

        if (moveAmmount > 0)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


    }
}
