using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject uiMenu;
    [SerializeField] private GameObject uiGame;

    [SerializeField] private TextMeshProUGUI scoreInGame;
    [SerializeField] private TextMeshProUGUI scoreInMenu;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI live;

    public void UpdateTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        time.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }

    public void ShowUiGame(bool value)
    {
        if (value)
        {
            uiGame.SetActive(value);
            uiMenu.SetActive(false);
        }
        else
        {
            uiGame.SetActive(value);
            uiMenu.SetActive(true);
        }
    }

    public void UpdateLive(int live)
    {
        this.live.text = live.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreInGame.text = score.ToString();
    }

    public void UpdateScoreInMenu(int score)
    {
        scoreInMenu.text = score.ToString();
    }
}
