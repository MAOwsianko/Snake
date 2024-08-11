using SnakeGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopupController : MonoBehaviour
{
    [SerializeField] private GameEvent restaertGameEvent = null;
    [SerializeField] private float gameoverPopupWaitTime = 1.0f;
    [SerializeField] private GameObject popupContent = null;
    public void ShowPopup()
    {
        StartCoroutine(WaitAndShowContent());
    }

    public void RestartGame()
    {
        popupContent.SetActive(false);
        if (restaertGameEvent!=null)
        {
            restaertGameEvent.Rise();
        }
    }

    private IEnumerator WaitAndShowContent()
    {
        yield return new WaitForSeconds(gameoverPopupWaitTime);
        popupContent.SetActive(true);   
    }
}
