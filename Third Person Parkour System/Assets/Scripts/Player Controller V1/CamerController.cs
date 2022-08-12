using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float distance = 5f;

    [SerializeField] private float minAngleValue = -20f;
    [SerializeField] private float maxAngleValue = 45f;

    [SerializeField] private float rotationSpeed = 2f;

    [SerializeField] private Vector2 frammingOffset;

    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;

    private float invertedXValue;
    private float invertedYValue;

    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        invertedXValue = invertX ? -1 : 1;
        invertedYValue = invertY ? -1 : 1;
        
        rotationX += Input.GetAxisRaw("Camera Y") * invertedYValue * rotationSpeed;
        rotationY += Input.GetAxisRaw("Camera X") * invertedXValue * rotationSpeed;

        rotationX = Mathf.Clamp(rotationX, minAngleValue, maxAngleValue);

        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 focusPosition = followTarget.position + new Vector3(frammingOffset.x, frammingOffset.y);
        
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;

    }

    public Quaternion PlannarRotation => Quaternion.Euler(0, rotationY, 0);
}




















