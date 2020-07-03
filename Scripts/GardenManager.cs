using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public float _startingGrowMeter = 0f;
    public float _fullyGrownMeter = 45f;
    float _currentGrownMeter;

    SpriteRenderer _spriteRenderer;
    public Sprite[] _plantStagesSprites;
    public ParticleSystem _particles;

    private void Awake()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _currentGrownMeter = _startingGrowMeter;
    }
    private void Start()
    {
        VictoryManager._gardensToWater.Add(this);
    }

    public void Grow(float waterReceived)
    {
        _currentGrownMeter += waterReceived;
        if (_currentGrownMeter >= _fullyGrownMeter / 4)
        {
            _spriteRenderer.sprite = _plantStagesSprites[0];
        }

        if (_currentGrownMeter >= _fullyGrownMeter *2 / 4)
        {
            _spriteRenderer.sprite = _plantStagesSprites[1];
        }

        if (_currentGrownMeter >= _fullyGrownMeter*3/4)
        {
            _spriteRenderer.sprite = _plantStagesSprites[2];
        }
        if(_currentGrownMeter >= _fullyGrownMeter)
        {
            _spriteRenderer.sprite = _plantStagesSprites[3];
            if(_particles!=null)
                _particles.gameObject.SetActive(true);
            DestroyMe();

        }

    }

    public void DestroyMe()
    {
        VictoryManager._gardensToWater.Remove(this);
        Destroy(this);
    }
}
