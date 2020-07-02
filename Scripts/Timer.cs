using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float _targetTime = 60.0f;
    public Text _timerText;
    public GameObject _gameOverScreen;
    public GameObject _victoryScreen;
    public CloudManager _cloud;
    public ShadowManager _shadow;

    public static List<GardenManager> _gardensToWater;

    void Update()
    {
        _targetTime -= Time.deltaTime;

        string minutes = ((int)_targetTime / 60).ToString();
        string seconds = ((int)_targetTime % 60).ToString();
        string floatPart = ((int)(_targetTime % 1 * 100)).ToString();

        if ((_targetTime / 60) < 10)
        {
            minutes = "0" + minutes.ToString();
        }
        if ((_targetTime % 60) < 10)
        {
            seconds = "0" + seconds.ToString();
        }
        _timerText.text = minutes + ":" + seconds + "." + floatPart.ToString();

        if (_targetTime <= 0.0f)
        {
            GameOver();
        }

        if (_targetTime >= 0f && _gardensToWater.Count == 0)
        {
            Victory();
        }
    }

    void GameOver()
    {
        _gameOverScreen.SetActive(true);
        _cloud.enabled = false;
        _shadow.enabled = false;
    }

    void Victory()
    {
        _victoryScreen.SetActive(true);
        _cloud.enabled = false;
        _shadow.enabled = false;
    }


}

