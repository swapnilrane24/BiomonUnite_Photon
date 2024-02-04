using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace Swapnil.Gameplay
{
    public class ActorFireScript : NetworkBehaviour
    {
        [SerializeField] private ProjectileController projectilePrefab;

        [Networked] private TickTimer delay { get; set; }

        private Vector3 _forward;

        public void ShootProjectile(Vector3 spawnPosition, Vector3 forwardDirection)
        {
            //we check if this entity has authority to spawn and delay is complete
            if (HasStateAuthority && delay.ExpiredOrNotRunning(Runner))
            {
                _forward = forwardDirection;
                //reset delay
                delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
                //spawn the projectile
               Runner.Spawn(projectilePrefab, spawnPosition, Quaternion.LookRotation(forwardDirection),
                    Object.InputAuthority, InitializeProjectileBeforeSpawn);
                
            }

        }

        private void InitializeProjectileBeforeSpawn(NetworkRunner runner, NetworkObject networkObject)
        {
            networkObject.GetComponent<ProjectileController>().Init(_forward);
        }
    }
}