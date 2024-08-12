using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiController : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private string scoreStringPrefix = "SCORE  ";
    [SerializeField] GameObject speedUpActive = null;
    [SerializeField] GameObject speedDownActive = null;
    [SerializeField] private SnakeData snakeData;
    private void Update()
    {
        scoreText.text = scoreStringPrefix + snakeData.Points.ToString();
        speedUpActive.SetActive(snakeData.IsBoostActive());
        speedDownActive.SetActive(snakeData.IsSlowdownActive());
    }
}
