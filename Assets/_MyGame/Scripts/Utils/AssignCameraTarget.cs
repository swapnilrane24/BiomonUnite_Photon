using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace Swapnil.Gameplay
{
    public class AssignCameraTarget : MonoBehaviour
    {
        [SerializeField] private Transform cameraTarget;

        private void Start()
        {
            NetworkObject networkObject = GetComponent<NetworkObject>();

            //here we check the NetworkObject component assigned to this gameobject has
            //authority over this gameobject
            if (networkObject && networkObject.HasInputAuthority)
            {
                //if yes we do following logic
                PlayerCameraFollow playerCameraFollow = FindObjectOfType<PlayerCameraFollow>();

                if (playerCameraFollow)
                {
                    playerCameraFollow.SetFollowTarget(cameraTarget);
                }
            }
        }
    }
}