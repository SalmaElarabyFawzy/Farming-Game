using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public static class PlayerEvents
    {
        public static Action<Vector2> OnMove;
        public static Action OnJump;
        public static Action OnInteract;
    }
}
