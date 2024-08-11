using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SnakeGame
{
    public class SnakeSegmentController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer segmentFill = null;
        [SerializeField] private float fillToSizeRatio = 0.8f;
        [SerializeField] private PlayFieldData playfieldData = null;

        private SnakeSegmentController segmentFront = null;
        private SnakeSegmentController segmentBack = null;
        private Vector2Int segmentPosition = Vector2Int.zero;
        [SerializeField]  private Vector2Int segmentDirection = Vector2Int.right;



        public Vector2Int SegmentDirection
        {
            get
            {
                return segmentDirection;
            }
            set
            {
                segmentDirection = value;
            }
        }

        public void SetupSnakeSegment(Vector2Int newPostion, Vector2Int newSegmentDirection)
        {
            SetupSnakeSegment(null, newPostion, newSegmentDirection);
        }
        public void SetupSnakeSegment( SnakeSegmentController newSegmentFront, Vector2Int newPostion, Vector2Int newSegmentDirection)
        {
           
            segmentPosition = newPostion;
            segmentFront = newSegmentFront;
            segmentBack = null;
            segmentDirection = newSegmentDirection;
            SeupSegmentFill();
            SetupSegmentPosition();
        }

        public void GrowSnake()
        {
            if (segmentBack == null)
            {
                segmentBack = Instantiate(this, transform.parent);
             
                segmentBack.SetupSnakeSegment( this, segmentPosition - segmentDirection, segmentDirection);
            }
            else
            {
                segmentBack.GrowSnake();
            }
        }

    

        private void SeupSegmentFill()
        {
            segmentFill.size = Vector2.one * playfieldData.TileSize * 10.0f * fillToSizeRatio;
        }
        private void SetupSegmentPosition()
        {
            Vector2 elementPosition = (Vector2)segmentPosition * playfieldData.TileSize;
            transform.position = elementPosition;
        }
        public void SegmentAdvance()
        {     
            if (segmentBack != null)
            {
                segmentBack.SegmentAdvance();
            }
            MoveSegment();
            if(segmentFront != null)
            {
                segmentDirection = segmentFront.segmentDirection;
            }
        }

        private void MoveSegment()
        {
            segmentPosition += segmentDirection;
            Vector2Int maxTilePos = playfieldData.GetMaxTilePosition();
            Vector2Int minTilePos = playfieldData.GetMinTilePosition();

            if(segmentPosition.x>maxTilePos.x) 
            {
          
                segmentPosition.x = minTilePos.x;
            }
            if (segmentPosition.y > maxTilePos.y)
            {
        
                segmentPosition.y = minTilePos.y;
            }


            if (segmentPosition.x < minTilePos.x)
            {
              
                segmentPosition.x = maxTilePos.x;
            }
            if (segmentPosition.y < minTilePos.y)
            {
               
                segmentPosition.y = maxTilePos.y;
            }

            SetupSegmentPosition();
        }

      

    }
}