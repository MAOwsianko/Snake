using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeGame
{
    public class GameFieldController : MonoBehaviour
    {
        [SerializeField] private PlayFieldData playfieldData = null;
        
        [SerializeField] private int powerupsPerSpawn = 3;
        [SerializeField] private float spawnInterval = 2.0f;
        [SerializeField] private float lastSpawnTime = -1.0f;
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
            transform.position = playfieldData.GridCenter;
        }

        public void OnGameTick()
        {
            float spawnElapsedTime =Time.time - lastSpawnTime;
            if (lastSpawnTime < 0  || spawnElapsedTime>= spawnInterval)
            {
                lastSpawnTime = Time.time;
                SpawnPowerUps();
            }
            
        }

        private void SpawnPowerUps()
        {
            for(int i = 0;i < powerupsPerSpawn; i++)
            {
               
                playfieldData.SpawnCollectible(transform);
            }
        }

    }
}
