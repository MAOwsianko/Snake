using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeGame
{
    public class CollectibleController : MonoBehaviour
    {
        [SerializeField] PlayFieldData playFieldData = null;
        [SerializeField] private PowerUp powerUp;
        [SerializeField] private Vector2Int collectiblePosition;
        private SpriteRenderer _spriteRenderer = null;
        private SpriteRenderer spriteRenderer
        {
            get 
            { 
                if (_spriteRenderer == null)
                {
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                }
                return _spriteRenderer; 
            }

        }
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
            spriteRenderer.size = Vector2.one* playFieldData.TileSize;
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