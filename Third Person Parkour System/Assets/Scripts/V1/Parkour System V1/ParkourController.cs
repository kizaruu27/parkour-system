using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
   private EnvironmentScanner _environmentScanner;

   private void Awake()
   {
      _environmentScanner = GetComponent<EnvironmentScanner>();
   }

   private void Update()
   {
      var hitData = _environmentScanner.ObstacleChecker();

      if (hitData.forwardHitFound)
      {
         Debug.Log("Hit " + hitData.forwardHit.transform.name);
      }
   }
}
