using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    [CreateAssetMenu(menuName = "Snake/PowerUps/PowerUpSpeedChange")]
    public class PowerUpSpeedChange : PowerUp
    {
        [SerializeField] private float speedMultiplier = 2.0f;
        [SerializeField] private  float effectDuration = 5.0f;
        [SerializeField] private SnakeData snakeData;

        public override void ApplyPowerUp(SnakeSegmentController snakeHead)
        {
            snakeData.SpeedChange(speedMultiplier, effectDuration);
        }
    }
}
