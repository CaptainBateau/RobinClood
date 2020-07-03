using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject _pauseMenu;
    public bool _current = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _current = !_current;
            Pausing(_current);
        }
    }

    public void Pausing(bool isActive)
    {
        _pauseMenu.SetActive(isActive);
        if (isActive)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

    }
    public void SetCurrent(bool isActive)
    {
        _current = isActive;
    }

    public void ResetTime()
    {
        Time.timeScale = 1;
    }
}
