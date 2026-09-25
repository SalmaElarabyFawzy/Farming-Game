using VContainer;

using Farm.Core.DesignPatterns.EventSystem;
using UnityEngine;

namespace Farm.Player.Events
{
    public class PlayerMoveEvent : IEvent
    {
        public Vector2 MoveDirection { get; private set; }

        public PlayerMoveEvent(Vector2 moveDirection)
        {
            MoveDirection = moveDirection;
        }
    }
}