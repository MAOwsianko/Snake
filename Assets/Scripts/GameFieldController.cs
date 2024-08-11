using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeGame
{
    public class GameFieldController : MonoBehaviour
    {
        [SerializeField] private PlayFieldData playfieldData = null;
    
        private SpriteRenderer _spriteRenderer;
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


        void Start()
        {
            SetupField();
        }
        //We setup the field so that it is always a multiple of the tile size, so that it forms a nice neat grid
        private void SetupField()
        {
            float fieldWidth = playfieldData.TileSize * (float)playfieldData.TilesGridSize.x;
            float fieldHeight = playfieldData.TileSize * (float)playfieldData.TilesGridSize.y;
            spriteRenderer.size = new Vector2(fieldWidth, fieldHeight);
        }


    }
}
