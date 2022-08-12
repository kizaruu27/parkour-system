using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraV2 : MonoBehaviour
{
    [Header("Target Component")]
    [SerializeField] private Transform followTarget;
    
    public float distance = 5f;
    public float sensivity = 2f;
    public float minXValue = -20f;
    public float maxXValue = 45f;
    public Vector2 cameraOffsetPos;

    private float rotationY;
    private float rotationX;

    private float xInvertValue;
    private float yInvertValue;

    public bool invertX;
    public bool invertY;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ThirdPersonCamera();
    }

    void ThirdPersonCamera()
    {
        xInvertValue = invertX ? -1 : 1;
        yInvertValue = invertY ? -1 : 1;
        
        rotationY += Input.GetAxisRaw("Camera X") * sensivity * xInvertValue;
        rotationX += Input.GetAxisRaw("Camera Y") * sensivity * yInvertValue;

        rotationX = Mathf.Clamp(rotationX, minXValue, maxXValue);

        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 cameraFocusPoint = followTarget.position + new Vector3(cameraOffsetPos.x, cameraOffsetPos.y);
        
        transform.position = cameraFocusPoint - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }
    
    public Quaternion PlannarRotation => Quaternion.Euler(0, rotationY, 0);
}
