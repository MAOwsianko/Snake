using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    [CreateAssetMenu(menuName = "Snake/PlayFieldData")]
    public class PlayFieldData : ScriptableObject
    {
        [SerializeField] private SnakeData snakeData;
        [SerializeField] private float tileSize = 0.25f;
        [Header("Odd number in both witdht and height")]
        [Tooltip("Odd number in both witdht and height")]
        [SerializeField] private Vector2Int tilesGridSize = new Vector2Int(57, 33);
        [SerializeField] private Vector2 gridCenter = Vector2.right;
        [SerializeField] private CollectibleController collectiblePrefab = null;
        [SerializeField] private PowerUp[] powerUps = null;

        private List<CollectibleController> collectiblesOnField = new List<CollectibleController>();
      

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

        public Vector2 GridCenter
        {
            get
            {
                return gridCenter;
            }
        }

        public Vector2 GridToRealPosition(Vector2Int gridPosition)
        {
            return  (Vector2)gridPosition * tileSize + gridCenter;
        }

        public void InitializeField()
        {
          
            foreach(var collectible in collectiblesOnField)
            {
                if(collectible != null)
                {
                    Destroy(collectible);
                }
                
            }
            collectiblesOnField.Clear();
            Random.InitState(System.DateTime.Now.Millisecond);
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

        private PowerUp GetRandomPowerUp()
        {
            List<PowerUp> tmpPowerUpList = new List<PowerUp>(); 
            foreach(var newPowerUp in powerUps)
            {
                for(int i = 0;i<newPowerUp.Avialbility;i++)
                {
                    tmpPowerUpList.Add(newPowerUp);
                }
            }
            int powerUpTypeIndex = Random.Range(0, tmpPowerUpList.Count);
            return tmpPowerUpList[powerUpTypeIndex];
        }

        public void SpawnCollectible( Transform targetTransform)
        {
            if(snakeData.SnakeHead == null)
            {
                return;
            }
         
            PowerUp powerUp = GetRandomPowerUp();


            Vector2Int minPosition = GetMinTilePosition();
            Vector2Int maxPosition = GetMaxTilePosition();
            List<Vector2Int> posibleLocations = new List<Vector2Int>(); 

            for(int x = minPosition.x+1; x<= maxPosition.x-1; x++) // we do not want the spand to happen on the border, it does not look nice
            {
                for (int y = minPosition.y+1; y <= maxPosition.y-1; y++) // we do not want the spand to happen on the border, it does not look nice
                {
                    Vector2Int possibleLocation = new Vector2Int(x, y);
                    if(snakeData.SnakeHead.IsPositionOnSnake(possibleLocation))
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
            var newCollectible = Instantiate(collectiblePrefab, targetTransform);
            int locationIndex = Random.Range(0, posibleLocations.Count);
            Vector2Int gridPosition = posibleLocations[locationIndex];
          

            newCollectible.SetupCollectible(gridPosition, powerUp);
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

       public PowerUp CollectPowerUp()
       {
            if(snakeData.SnakeHead == null)
            {
                return null;
            }
            var collectible = GetCollectibleOnPosition(snakeData.SnakeHead.SegmentPosition);   
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
