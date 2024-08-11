using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldController : MonoBehaviour
{
    [SerializeField] private float tileSize = 0.25f;
    [Tooltip("Tte tile grid size has to have odd number in both witdht and height, so we have a defined middle element")]
    [SerializeField] private Vector2Int tilesGridSize = new Vector2Int(57,33);

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


    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer spriteRenderer
    {
        get
        {
            if(_spriteRenderer == null)
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
        float fieldWidth = tileSize * (float)TilesGridSize.x;
        float fieldHeight = tileSize * (float )TilesGridSize.y;
        spriteRenderer.size = new Vector2(fieldWidth, fieldHeight);
    }


}
