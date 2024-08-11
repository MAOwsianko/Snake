using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeGame
{
    public class CollectibleController : MonoBehaviour
    {
        [SerializeField] private PowerUp powerUp;
        [SerializeField] private Vector2Int collectiblePosition;

        public PowerUp ConnectedPowerUp
        {
            get
            {
                return powerUp; 
            }
        }
        public Vector2Int CollectiblePosition
        {
            get
            {
                return collectiblePosition;
            }
        }
        public void SetupCollectible(Vector2Int newPosition, float collectibleSize, PowerUp newPowerUp )
        {
            powerUp = newPowerUp;
            collectiblePosition = newPosition;
            Vector2 elementPosition = (Vector2)collectiblePosition * collectibleSize;
            transform.position = elementPosition;
          
        }
        private void OnDestroy()
        {
            Destroy(gameObject);    
        }
    }
}