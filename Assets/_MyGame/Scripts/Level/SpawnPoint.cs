using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Swapnil.Gameplay
{
    public class SpawnPoint : MonoBehaviour
    {

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
#endif

    }
}