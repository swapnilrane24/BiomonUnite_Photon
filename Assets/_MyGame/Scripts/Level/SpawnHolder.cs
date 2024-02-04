using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Swapnil.Gameplay
{
    public class SpawnHolder : MonoBehaviour
    {
        [SerializeField] private SpawnPoint[] spawnPointA;
        [SerializeField] private SpawnPoint[] spawnPointB;

        public Vector3 GetATeamSpawnPoint()
        {
            return spawnPointA[Random.Range(0, spawnPointA.Length)].transform.position;
        }


        public Vector3 GetBTeamSpawnPoint()
        {
            return spawnPointB[Random.Range(0, spawnPointB.Length)].transform.position;
        }

    }
}