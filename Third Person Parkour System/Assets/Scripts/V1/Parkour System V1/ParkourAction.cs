using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parkour System/New Parkour Action")]
public class ParkourAction : ScriptableObject
{
    public string animName;
    public float minHeight;
    public float maxHeight;
    
    public bool CheckIfPossible(ObstacleHiData hitData, Transform player)
    {
        float height = hitData.heightHit.point.y - player.position.y;

        if (height < minHeight || height > maxHeight) return false; 
        
        return true;
    }
}
