using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnvironmentScanner : MonoBehaviour
{
    [SerializeField] private Vector3 rayOffset = new Vector3(0, 0.25f, 0);
    [SerializeField] private float forwardRayDistance = 0.8f;
    [SerializeField] private float heightRayDistance = 5f;
    [SerializeField] private LayerMask whatIsObstacle;
    
    public ObstacleHiData ObstacleChecker()
    {
        ObstacleHiData hitData = new ObstacleHiData();
        
        hitData.forwardHitFound = Physics.Raycast(transform.position + rayOffset, transform.forward, out hitData.forwardHit,
            forwardRayDistance, whatIsObstacle);
        
        Debug.DrawRay(transform.position + rayOffset, transform.forward * forwardRayDistance, hitData.forwardHitFound ? Color.red : Color.white);

        if (hitData.forwardHitFound)
        {
            hitData.heigtHitFound = Physics.Raycast(hitData.forwardHit.point + Vector3.up * heightRayDistance,
                Vector3.down, out hitData.heightHit, heightRayDistance, whatIsObstacle);
            
            Debug.DrawRay(hitData.forwardHit.point + Vector3.up * heightRayDistance,
                Vector3.down * heightRayDistance, hitData.heigtHitFound ? Color.red : Color.white);
        }
        
        return hitData;
    }
}

public struct ObstacleHiData
{
    public bool forwardHitFound;
    public RaycastHit forwardHit;
    public bool heigtHitFound;
    public RaycastHit heightHit;
}


