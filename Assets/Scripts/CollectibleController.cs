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
        public void SetupCollectible(Vector2Int newPosition, PowerUp newPowerUp )
        {
            spriteRenderer.size = Vector2.one* playFieldData.TileSize;
            spriteRenderer.color = newPowerUp.PowerUpColor;
            powerUp = newPowerUp;
            collectiblePosition = newPosition;
 
            transform.position = playFieldData.GridToRealPosition(collectiblePosition);

        }
        private void OnDestroy()
        {
            Destroy(gameObject);    
        }
    }
}