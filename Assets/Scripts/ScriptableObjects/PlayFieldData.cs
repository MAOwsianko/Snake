using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    [CreateAssetMenu(menuName = "Snake/PlayFieldData")]
    public class PlayFieldData : ScriptableObject
    {
        [SerializeField] private float tileSize = 0.25f;
        [Tooltip("Tte tile grid size has to have odd number in both witdht and height, so we have a defined middle element")]
        [SerializeField] private Vector2Int tilesGridSize = new Vector2Int(57, 33);

        public float TileSize
        {
            get
            {
                return tileSize;
            }

        }

        public Vector2Int TilesGridSize
        {
            get
            {
                return tilesGridSize;
            }

        }

        public Vector2Int GetMaxTilePosition()
        {
            if (tilesGridSize.x % 2 != 1 || tilesGridSize.y % 2 !=1)
            {
                Debug.LogAssertion("PlayFieldData.GetMaxTilePosition Error tilesGridSize is not odd tilesGridSize");
            }
            Vector2Int maxTilePos = Vector2Int.zero;
            maxTilePos.x = (TilesGridSize.x - 1) / 2;
            maxTilePos.y = (TilesGridSize.y - 1) / 2;

            return maxTilePos;
        }
        public Vector2Int GetMinTilePosition()
        {
            return -1 * GetMaxTilePosition();
        }
    }
}
