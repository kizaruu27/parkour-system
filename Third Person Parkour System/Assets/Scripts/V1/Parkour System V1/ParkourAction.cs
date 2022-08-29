using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parkour System/New Parkour Action")]
public class ParkourAction : ScriptableObject
{
    public string animName;
    public float minHeight;
    public float maxHeight;
    public bool rotateToObstacle;
    
    [Header("Target Matching")]
    public bool enableTargetMatching = true;
    public AvatarTarget matchBodyPart;
    public float matchStartTime;
    public float matchTargetTime;

    public Quaternion TargetRotation
    {
        get;
        set;
    }
    
    public Vector3 MatchPos { get; set; }
    
    public bool CheckIfPossible(ObstacleHiData hitData, Transform player)
    {
        float height = hitData.heightHit.point.y - player.position.y;

        if (height < minHeight || height > maxHeight) return false;

        if (rotateToObstacle) TargetRotation = Quaternion.LookRotation(-hitData.forwardHit.normal);

        if (enableTargetMatching) MatchPos = hitData.heightHit.point;
        
        return true;
    }
}
