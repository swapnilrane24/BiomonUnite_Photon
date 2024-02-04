using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace Swapnil.Gameplay
{
    public class ProjectileController : NetworkBehaviour
    {
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float projectileLife = 5;//in seconds

        [Networked] private TickTimer Life { get; set; }

        public void Init(Vector3 forward)
        {
            //set the life projectileLife seconds in future
            Life = TickTimer.CreateFromSeconds(Runner, projectileLife);
            //GetComponent<Rigidbody>().velocity = forward;
        }

        public override void FixedUpdateNetwork()
        {
            transform.position += moveSpeed * transform.forward * Runner.DeltaTime;

            if (Life.Expired(Runner)) //if time is up
            {
                Runner.Despawn(Object);//we despawn the object
            }
        }


    }
}