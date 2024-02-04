using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Swapnil.Gameplay
{
    public class PlayerCameraFollow : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

        public void SetFollowTarget(Transform followTarget)
        {
            cinemachineVirtualCamera.Follow = followTarget;
        }

    }
}