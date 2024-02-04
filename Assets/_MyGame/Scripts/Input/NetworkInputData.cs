using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace Swapnil.Gameplay
{
    public struct NetworkInputData : INetworkInput
    {
        public const byte MOUSEBUTTON0 = 1;

        public Vector3 direction;
        public NetworkButtons buttons;

    }
}