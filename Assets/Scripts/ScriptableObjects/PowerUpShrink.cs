using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    [CreateAssetMenu(menuName = "Snake/PowerUps/PowerUpShrink")]
    public class PowerUpShrink : PowerUp
    {
        public override void ApplyPowerUp(SnakeSegmentController snakeHead)
        {
            snakeHead.ShrinkSnake();
        }
    }
}
