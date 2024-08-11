using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    [CreateAssetMenu(menuName = "Snake/PowerUps/PowerUpReverse")]
    public class PowerUpReverse : PowerUp
    {
        public override void ApplyPowerUp(SnakeSegmentController snakeHead)
        {
            snakeHead.ReverseSnake();
        }
    }
}
