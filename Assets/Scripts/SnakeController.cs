using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnakeGame
{
    public class SnakeController : MonoBehaviour
    {
        [SerializeField] private SnakeData snakeData = null;
        [SerializeField] private PlayFieldData playField = null;
        [SerializeField] private GameEvent gameTick = null;
        [SerializeField] private GameEvent gameOverEvent = null;
        [SerializeField] private int initialSnakeSegmentsCount = 3;
        [SerializeField] private Vector2Int initialPosition = Vector2Int.zero;
        [SerializeField] private Vector2Int initialDirection = Vector2Int.right;
        [SerializeField] private float initialMoveTime = 0.5f;
       
        [SerializeField] private SnakeSegmentController snakeSegmentPrefab = null;

        private bool gameStarted = false;
        private Coroutine snakeMoveCoroutine = null;
        private float moveTime = 0.5f;
      

        private GameFieldController _gameFieldController = null;
        private GameFieldController gameFieldController
        {
            get
            {
                if (_gameFieldController == null)
                {
                    _gameFieldController = FindAnyObjectByType<GameFieldController>();
                }
                return _gameFieldController;
            }
        }
        
    
     
        private void CleanupLastGame()
        {
            if(snakeData.SnakeHead != null)
            {
                Destroy(snakeData.SnakeHead); 
            }
        }
        public void ResetGame()
        {
            CleanupLastGame();
            moveTime = initialMoveTime;
            SpawnSnake();
            
            snakeMoveCoroutine = StartCoroutine(SnakeMove());
        }

        private void SpawnSnake()
        {
            var snakeHead = Instantiate(snakeSegmentPrefab, transform);
            snakeHead.SetupSnakeSegment(  initialPosition, initialDirection);
            for (int i = 0; i < initialSnakeSegmentsCount; i++)
            {
                snakeHead.GrowSnake();
            }
            playField.InitializeField();
            snakeData.ResetSnakeData(snakeHead);
        }

        private IEnumerator SnakeMove()
        {
            if (snakeData.SnakeHead == null)
            {
                yield break;
            }
            gameStarted = true;
            while (gameStarted)
            {
                yield return new WaitForSeconds(moveTime / snakeData.GetSpeedMultiplier());
                snakeData.SnakeHead.SegmentAdvance();
                if(snakeData.SnakeHead.IsHeadIntersectSnake())
                {
                    gameStarted = false;
                   
                    if(gameOverEvent!=null)
                    {
                        gameOverEvent.Rise();   
                    }
                }
                else
                {
                    
                    gameTick.Rise();
                }
            }
        }

    
        public void SetSnakeDirection(InputAction.CallbackContext givenValue)
        {
            if(!gameStarted)
            {
                return;
            }
            if (!givenValue.performed)
            {
                return;
            }
            Vector2 newHeaDirectionFloat = givenValue.ReadValue<Vector2>();

            Vector2Int newHeaDirection = Vector2Int.zero;
            newHeaDirection.x = Mathf.CeilToInt(newHeaDirectionFloat.x);
            newHeaDirection.y = Mathf.CeilToInt(newHeaDirectionFloat.y);
            snakeData.SnakeHead.TurnSnake(newHeaDirection);
        }
       
    }
}
