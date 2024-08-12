using SnakeGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Snake/SnakeData")]
public class SnakeData : ScriptableObject
{
    [SerializeField] float speedMultiplier = 1f;
    [SerializeField] float effectDuration = 0.0f;
    private int points = 0;
    private float effectEnteredTime = -1.0f;

    private SnakeSegmentController snakeHead = null;
    public int Points
    {
        get
        {
            return points;  
        }
    }

    public SnakeSegmentController SnakeHead
    {
        get
        {
            return snakeHead;
        }
        set
        {
            snakeHead = value;
        }
    }
    public void SpeedChange(float newSpeedMultiplier,float newEffectDuration)
    {
        speedMultiplier = newSpeedMultiplier;
        effectDuration = newEffectDuration;
        effectEnteredTime = Time.time;
    }

    public float GetSpeedMultiplier()
    {
        if (effectEnteredTime >= 0)
        {
            float elapsedTIme = Time.time - effectEnteredTime;
            if (elapsedTIme < effectDuration)
            {
                return speedMultiplier;
            }
        }
        return 1.0f;
    } 
    
    public void ResetSnakeData(SnakeSegmentController newSnakeHead)
    { 
        snakeHead = newSnakeHead;
        speedMultiplier = 1f;
        effectDuration = 0.0f;
        effectEnteredTime = -1.0f;
        points = 0;
    }
    public bool IsBoostActive()
    {
        float speedMult = GetSpeedMultiplier();
        return speedMult > 1.0f;

    }
    public bool IsSlowdownActive()
    {
        float speedMult = GetSpeedMultiplier();
        return speedMult < 1.0f;

    }

    public void AddPoint()
    {
        points++;
    }
}
