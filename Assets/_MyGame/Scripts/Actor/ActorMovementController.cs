using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace Swapnil.Gameplay
{

    public class ActorMovementController : NetworkBehaviour
    {
        [SerializeField] private Transform projectileSpawnPosition;
        [SerializeField] private Transform cameraFollowTransform;
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private ActorFireScript actorFireScript;

        private float folloTargetYPos;
        private Vector3 directionWithGravity;

        private void Start()
        {
            if (cameraFollowTransform)
            {
                folloTargetYPos = cameraFollowTransform.position.y;
                cameraFollowTransform.parent = null;
            }
        }

        //its called every Fusion simulation tick
        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData input))
            {
  
                if (characterController.isGrounded == false)
                {
                    directionWithGravity = input.direction.normalized;
                    directionWithGravity += 9f * Vector3.down;

                    characterController.Move(moveSpeed * directionWithGravity * Runner.DeltaTime);
                }
                else
                {
                    //here we use Runner.DeltaTime instead of Time.DeltaTime
                    characterController.Move(moveSpeed * input.direction.normalized * Runner.DeltaTime);
                }
                if (input.direction.sqrMagnitude > 0)
                {
                    transform.rotation = Quaternion.LookRotation(input.direction);

                    cameraFollowTransform.position = transform.position + Vector3.up * folloTargetYPos;
                }

                if (input.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
                {
                    actorFireScript.ShootProjectile(projectileSpawnPosition.position,
                        transform.forward);
                }
            }


        }



    }
}