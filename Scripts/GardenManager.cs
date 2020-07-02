﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public float _startingGrowMeter = 0f;
    public float _fullyGrownMeter = 45f;

    SpriteRenderer _spriteRenderer;
    public Sprite[] _plantStagesSprites;

    private void Awake()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void Grow(float waterReceived)
    {
        float _currentGrownMeter;
        _currentGrownMeter = _startingGrowMeter + (waterReceived*Time.deltaTime);
        if (_currentGrownMeter >= _fullyGrownMeter / 3)
        {
            _spriteRenderer.sprite = _plantStagesSprites[0];
        }

        if (_currentGrownMeter >= _fullyGrownMeter *2 / 3)
        {
            _spriteRenderer.sprite = _plantStagesSprites[1];
        }

        if (_currentGrownMeter >= _fullyGrownMeter)
        {
            _spriteRenderer.sprite = _plantStagesSprites[2];
            Destroy(this);
        }

        _startingGrowMeter = _currentGrownMeter;
    }
}