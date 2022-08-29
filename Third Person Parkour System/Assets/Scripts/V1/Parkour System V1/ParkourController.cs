using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
   private EnvironmentScanner _environmentScanner;
   private Animator _animator;
   private PlayerController _playerController;

   [SerializeField] private List<ParkourAction> parkourActions;

   private bool inAction;

   private void Awake()
   {
      _environmentScanner = GetComponent<EnvironmentScanner>();
      _playerController = GetComponent<PlayerController>();
      _animator = GetComponent<Animator>();
   }

   private void Update()
   {
      if (Input.GetButton("Jump") && !inAction)
      {
         var hitData = _environmentScanner.ObstacleChecker();
         if (hitData.forwardHitFound)
         {
            foreach (var action in parkourActions)
            {
               if (action.CheckIfPossible(hitData, transform))
               {
                  StartCoroutine(DoParkourAction(action));
                  break;
               }
            }
         }
      }
   }

   IEnumerator DoParkourAction(ParkourAction action)
   {
      inAction = true;
      _playerController.SetControl(false);
      _animator.CrossFade(action.animName, .2f);
      yield return null;

      var animState = _animator.GetNextAnimatorStateInfo(0);
      
      if (!animState.IsName(action.animName))
         Debug.LogError("Wrong Animation Name!!");

      yield return new WaitForSeconds(animState.length);
      
      _playerController.SetControl(true);

      inAction = false;
   }
}
