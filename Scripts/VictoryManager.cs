using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    float _maximumTimeValue;
    public float _targetTime = 60.0f;
    float _endTime = 0f;
    float t = 0f;

    public float _lerpedValue;

    EventSystem ES;


    //public Text _timerText;
    public GameObject _gameOverScreen;
    public GameObject _victoryScreen;
    public CloudManager _cloud;
    public ShadowManager _shadow;
    public static List<GardenManager> _gardensToWater = new List<GardenManager>();
    private void Awake()
    {
        _maximumTimeValue = _targetTime;
        ES = FindObjectOfType<EventSystem>();
    }
    void Update()
    {
        t += Time.deltaTime;
        _targetTime -= Time.deltaTime;

        LerpingTime();

        //string minutes = ((int)_targetTime / 60).ToString();
        //string seconds = ((int)_targetTime % 60).ToString();
        //string floatPart = ((int)(_targetTime % 1 * 100)).ToString();
        //
        //if ((_targetTime / 60) < 10)
        //{
        //    minutes = "0" + minutes.ToString();
        //}
        //if ((_targetTime % 60) < 10)
        //{
        //    seconds = "0" + seconds.ToString();
        //}
        //_timerText.text = minutes + ":" + seconds + "." + floatPart.ToString();

        if (_targetTime <= _endTime)
        {
            GameOver();
        }

        if (_gardensToWater.Count == 0)
        {
            Victory();
        }
    }

    public void GameOver()
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

    void LerpingTime()
    {
        _lerpedValue = Mathf.Lerp(_endTime, 1, Mathf.Clamp01(t / _maximumTimeValue));
    }
}

