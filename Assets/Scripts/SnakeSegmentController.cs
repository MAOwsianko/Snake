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
        [SerializeField] private Color gameOverColor = Color.red;
        [SerializeField] private SnakeSegmentController segmentFront = null;
        [SerializeField] private SnakeSegmentController segmentBack = null;
        [SerializeField] private Vector2Int segmentPosition = Vector2Int.zero;
        [SerializeField] private Vector2Int segmentDirection = Vector2Int.right;

        public Vector2Int SegmentPosition
        {
            get
            {
                return segmentPosition;
            }
          
        }

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
        public void TurnSnake(Vector2Int newSnakeDirection)
        {
            Vector2Int suicideDIrection = segmentDirection * -1;
            if(newSnakeDirection==suicideDIrection)
            {
                return;  // we do not let the player turn the snake 180 deg and eat itself , we only allow 90 deg turns
            }
            segmentDirection = newSnakeDirection;
        }

        public void SetupSnakeSegment(Vector2Int newPostion, Vector2Int newSegmentDirection)
        {
            SetupSnakeSegment(null, newPostion, newSegmentDirection);
        }
        public void SetupSnakeSegment(SnakeSegmentController newSegmentFront, Vector2Int newPostion, Vector2Int newSegmentDirection)
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

                segmentBack.SetupSnakeSegment(this, segmentPosition - segmentDirection, segmentDirection);
            }
            else
            {
                segmentBack.GrowSnake();
            }
        }
        public void SetGameover()
        {
            segmentFill.color = gameOverColor;
        }


        private void SeupSegmentFill()
        {
            segmentFill.size = Vector2.one * playfieldData.TileSize * 10.0f * fillToSizeRatio;
        }
        private void SetupSegmentPosition()
        {
            Vector2 elementPosition = playfieldData.GridToRealPosition(segmentPosition); 
            transform.position = elementPosition;
        }
        public void SegmentAdvance()
        {
            if (this == playfieldData.SnakeHead)
            {
                CollectPowerUps();
            }
            if (segmentBack != null)
            {
                segmentBack.SegmentAdvance();
            }
           
            MoveSegment();
           
        }

        private Vector2Int GenerateSegmentDirection()
        {
            Vector2Int newDirection = segmentFront.segmentPosition - segmentPosition;
            if(newDirection.x <-1)
            {
                newDirection.x = 1; // we crosseed the right edge
               
            }
            else if(newDirection.x>1)
            {
                newDirection.x = -1;// we crosseed the left edge
            }
            else if (newDirection.y < -1)
            {
                newDirection.y = 1; // we crosseed the top edge
            }
            else if (newDirection.y > 1)
            {
                newDirection.y = -1;// we crosseed the bottom edge
            }
            return newDirection;
        }



        public void CollectPowerUps()
        {
            var collectedPowerup = playfieldData.CollectPowerUp();
            if (collectedPowerup != null)
            {
                collectedPowerup.ApplyPowerUp(this);
            }
        }

        public int GetSnakeLenght()
        {
            return GetSegmentsLenght(0);
        }

        public bool IsPositionOnSnake(Vector2Int testedPosition)
        {
            if(segmentPosition== testedPosition)
            {
                return true; // the position is on this segment
            }
            if(segmentBack != null)
            {
                return segmentBack.IsPositionOnSnake(testedPosition);  // the position is on not on this segment lets chek the next one
            }
            return false; // snake finished position not found
        }

        private int GetSegmentsLenght(int segmentsAlreadyCounted)
        {
            int snakeLenght = segmentsAlreadyCounted+1;
            if (segmentBack == null)
            {
                return snakeLenght;
            }
            return segmentBack.GetSegmentsLenght(snakeLenght);
        }

        private void MoveSegment()
        {
            if (segmentFront != null)
            {
                segmentDirection = GenerateSegmentDirection();
            }
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
        private bool IsHeadIntersectSnake(SnakeSegmentController headSegment )
        {
            if (headSegment==null)
            {
                Debug.LogAssertion("SnakeSegmentController.IsHeadIntersectSnake headSegment is null");
                return false;
            }
            if(headSegment.segmentPosition == segmentPosition)
            {
                return true; // if the head segment has the same position as a different segment, there is an intersection, so gameover
            }
            if(segmentBack !=null)
            {
                return segmentBack.IsHeadIntersectSnake(headSegment);   // we check the next segment id there is one
            }
            return false; // if we reached the end of the snake with no intersections then the snake is fine
        }
        private void OnDestroy()
        {
            Destroy(gameObject);
            if(segmentBack!=null)
            {
                Destroy(segmentBack);
            }
        }

        public bool IsHeadIntersectSnake()
        {
            if (segmentBack == null)
            {
                return false;
            }
            return segmentBack.IsHeadIntersectSnake(this);

        }
        
     
        public void ReverseSnake()
        {
            var segmentFrontLast = segmentFront;
            var segmentBackLast = segmentBack;
            segmentBack = segmentFrontLast;
            segmentFront = segmentBackLast;

           
            if (segmentBackLast != null)
            {
                segmentDirection = GenerateSegmentDirection();
                segmentBackLast.ReverseSnake();
            }
            else
            {
                segmentDirection *= -1;
                playfieldData.SnakeHead = this;
            }


        }

    }
}