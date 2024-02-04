using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace Swapnil.Gameplay
{
    //This is HostMode so only host will spawn actor.
    //Therefore none of its parameters need to be [Networked]
    //NetworkBehavious this class extends Unity MonoBehaviour
    public class ActorSpawner : NetworkBehaviour, IPlayerJoined, IPlayerLeft
    {
        [SerializeField] private ActorMovementController actorPrefab;
        [SerializeField] private SpawnHolder spawnHolder;

        public void PlayerJoined(PlayerRef player)
        {
            //we check if this player belongs to this client
            if (player == Runner.LocalPlayer)
            {
                //then we ask network runner to spawn player and replicated for other clients as well
                Runner.Spawn(actorPrefab, spawnHolder.GetATeamSpawnPoint(), Quaternion.identity);
            }
        }

        public void PlayerLeft(PlayerRef player)
        {
            
        }
    }
}