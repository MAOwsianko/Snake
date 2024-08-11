using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeGame
{
    public abstract class PowerUp : ScriptableObject
    {
        public Color powerUpColor = Color.white;
        public abstract void ApplyPowerUp(SnakeSegmentController snakeHead);

    }
}
