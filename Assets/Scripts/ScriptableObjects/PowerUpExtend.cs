
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeGame
{
    [CreateAssetMenu(menuName = "Snake/PowerUps/PowerUpExtend")]
    public class PowerUpExtend : PowerUp
    {
        public override void ApplyPowerUp(SnakeSegmentController snakeHead)
        {
            snakeHead.GrowSnake();
        }
    }
}