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
        [SerializeField] private CollectibleController collectiblePrefab = null;
        private List<CollectibleController> collectiblesOnField = new List<CollectibleController>();
        private SnakeSegmentController snakeHead = null;    

       

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

        public void SeupSnakeHead(SnakeSegmentController newSnakeHead)
        {
            snakeHead = newSnakeHead;   
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

        public void SpawnCollectible(PowerUp powerUp, Transform targetTransform)
        {
            if(snakeHead==null)
            {
                return;
            }
            //int snakeSegmenstCount = snakeHead.GetSnakeLenght();
            //int collectiblesOnFieldCount = collectiblesOnField.Count;
            //int fieldsTaken = snakeSegmenstCount + collectiblesOnFieldCount;
            //int fieldSize = tilesGridSize.x * tilesGridSize.y;
            //if(fieldsTaken>=fieldSize)
            //{
            //    return;
            //}
            var newCollectible = Instantiate(collectiblePrefab, targetTransform);
            Vector2Int minPosition = GetMinTilePosition();
            Vector2Int maxPosition = GetMaxTilePosition();
            List<Vector2Int> posibleLocations = new List<Vector2Int>(); 
            for(int x = minPosition.x; x<= maxPosition.x; x++)
            {
                for (int y = minPosition.y; y <= maxPosition.y; y++)
                {
                    Vector2Int possibleLocation = new Vector2Int(x, y);
                    if(snakeHead.IsPositionOnSnake(possibleLocation))
                    {
                        continue;
                    }
                    if(GetCollectibleOnPosition(possibleLocation)!=null)
                    {
                        continue;
                    }
                    posibleLocations.Add(possibleLocation);
                }
            }
            if(posibleLocations.Count==0)
            {
                return;
            }
            int locationIndex = Random.Range(0, posibleLocations.Count);
            newCollectible.SetupCollectible(posibleLocations[locationIndex], tileSize, powerUp);
            collectiblesOnField.Add(newCollectible);
        }
       
        

        private CollectibleController GetCollectibleOnPosition(Vector2Int testedPosition)
        {
            foreach(var collectible in collectiblesOnField)
            {
                if(collectible.CollectiblePosition == testedPosition)
                {
                    return collectible;
                }
            }
            return null;
        }

       public PowerUp CollectPowerUp(Vector2Int headPosition)
       {
            var collectible = GetCollectibleOnPosition(headPosition);   
            if(collectible != null)
            {
                collectiblesOnField.Remove(collectible);
            }
            if(collectible== null)
            {
                return null;
            }
            var powerUp = collectible.ConnectedPowerUp;
            Destroy(collectible);

            return powerUp;
       }


    }
}
