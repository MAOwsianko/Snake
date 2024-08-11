using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSegmentController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer segmentFill= null;
    [SerializeField] private float fillToSizeRatio = 0.8f;

    private SnakeSegmentController segmentFront = null;
    private SnakeSegmentController segmentBack = null;
    private Vector2Int segmentPosition = Vector2Int.zero;
    private Vector2Int segmentDirection = Vector2Int.right;
    private SnakeController snakeController = null;
    

    public void SetupSnakeSegment(SnakeController parrentSnakeController,SnakeSegmentController newSegmentFront, Vector2Int newPostion, Vector2Int newSegmentDirection)
    {
        snakeController = parrentSnakeController;
        segmentPosition = newPostion;
        segmentFront = newSegmentFront;
        segmentBack = null;
        segmentDirection = newSegmentDirection;
        SeupSegmentFill();
        SetupSegmentPosition();
    }

    public void GrowSnake()
    {
        if(segmentBack == null)
        {
            segmentBack = Instantiate(this, transform.parent);
            segmentBack.SetupSnakeSegment(snakeController,this, segmentPosition-segmentDirection, segmentDirection);
        }
        else
        {
            segmentBack.GrowSnake();
        }
    }

    private void SeupSegmentFill()
    {
        segmentFill.size = Vector2.one * snakeController.SegmentSize * 10.0f * fillToSizeRatio;
    }
    private void SetupSegmentPosition()
    {
        Vector2 elementPosition =  (Vector2)segmentPosition * snakeController.SegmentSize;
        transform.position = elementPosition;
    }
    public void SegmentAdvance()
    {
        segmentPosition += segmentDirection;
        SetupSegmentPosition();
        if (segmentBack != null)
        {
            segmentBack.SegmentAdvance();
        }
    }

}
