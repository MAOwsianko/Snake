using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeGame
{
    public abstract class PowerUp : ScriptableObject
    {
        [Header("The higher the avialbility the more common the power up")]
        [SerializeField] private int avialbility = 1;
        [SerializeField] protected Color powerUpColor = Color.white;

        public int Avialbility
        {
            get
            {
                return avialbility;
            }
        }
        public Color PowerUpColor
        {
            get 
            { 
                return powerUpColor; 
            }
        }
        public abstract void ApplyPowerUp(SnakeSegmentController snakeHead);

    }
}
