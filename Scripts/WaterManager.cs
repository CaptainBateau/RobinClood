using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public float _maxCapacity;
    public float _currentCapacity;
    
    public bool _isRefilling;
    public float _refillPerTick;
    public int _refillTickRate;
    float _lastRefillTickTime;
    

    public bool _isEmptying;
    public float _emptyPerTick;
    public int _emptyTickRate;
    float _lastEmptyTickTime;

    float _firstStageValue;
    bool _firstStage = false;
    float _secondStageValue;
    bool _secondStage = false;
    float _thirdStageValue;
    bool _thirdStage = false;

    bool _defaultStage;
    SpriteRenderer _spriteRenderer;
    public Sprite[] _faceSprites;

    private void Awake()
    {
        TryGetComponent<SpriteRenderer>(out _spriteRenderer);
        float stageValue = _maxCapacity / _faceSprites.Length;
        _firstStageValue = stageValue;
        _secondStageValue = stageValue * 2;
        _thirdStageValue = stageValue * 3;

    }
    private void Update()
    {
        if (_isRefilling && _lastRefillTickTime + (1 / (float)_refillTickRate) < Time.time)
            RefillCloud();
        if (_isEmptying && _lastEmptyTickTime + (1 / (float)_emptyTickRate) < Time.time)
            EmptyCloud();
    }

    public void RefillCloud()
    {
        _lastRefillTickTime = Time.time;
        _currentCapacity += _refillPerTick;
        _currentCapacity = Mathf.Clamp(_currentCapacity, 0, _maxCapacity);
        if (_firstStageValue <= _currentCapacity && !_firstStage)
        {
            _firstStage = true;
            ChangeStage(0);
        }
        if (_secondStageValue <= _currentCapacity && !_secondStage)
        {
            _secondStage = true;
            ChangeStage(1);
        }
        if (_thirdStageValue <= _currentCapacity && !_thirdStage)
        {
            _thirdStage = true;
            ChangeStage(2);
        }
        if(_currentCapacity < _firstStageValue && !_defaultStage)
        {
            _defaultStage = true;
            ChangeStage(-1);
        }
    }

    public void EmptyCloud()
    {
        _lastEmptyTickTime = Time.time;
        if (_currentCapacity >= _emptyPerTick) 
        {
            DropWaterPower(_emptyPerTick);
            _currentCapacity -= _emptyPerTick;
            _currentCapacity = Mathf.Clamp(_currentCapacity, 0, _maxCapacity); 
        }
        else if(_currentCapacity > 0 && _currentCapacity < _emptyPerTick)
        {
            DropWaterPower(_emptyPerTick/(float)_currentCapacity);
            _currentCapacity = 0;
        }

        if (_firstStageValue > _currentCapacity && _firstStage)
        {
            _firstStage = false;
            ChangeStage(-1);
        }
        if (_secondStageValue > _currentCapacity && _secondStage)
        {
            _secondStage = false;
            ChangeStage(0);
        }
        if (_thirdStageValue > _currentCapacity && _thirdStage)
        {
            _thirdStage = false;
            ChangeStage(1);
        }
    }

    public void LoseWater(float waterLost = 0) 
    {
        if (_currentCapacity < waterLost)
        {
            //Lose the game
            Destroy(gameObject);
        }
        _currentCapacity -= waterLost;
        _currentCapacity = Mathf.Clamp(_currentCapacity, 0, _maxCapacity);
    }


    public void DropWaterPower(float power = 1)
    {
        Debug.Log("POWERRRRRR " + power);
    }

    //Change Sprite + wooble
    public void ChangeStage(int stage)
    {
        switch (stage)
        {
            case 0:
                _spriteRenderer.sprite = _faceSprites[1];
                transform.localScale *= _woobleMultiplier;
                _woobleStartTimer = Time.time;
                break;
            case 1:

                _spriteRenderer.sprite = _faceSprites[2];
                transform.localScale *= _woobleMultiplier;
                _woobleStartTimer = Time.time;
                break;
            case 2:

                _spriteRenderer.sprite = _faceSprites[3];
                transform.localScale *= _woobleMultiplier;
                _woobleStartTimer = Time.time;
                break;
            default:

                _spriteRenderer.sprite = _faceSprites[0];
                transform.localScale *= _woobleMultiplier;
                _woobleStartTimer = Time.time;
                break;
               
        }
    }

    public void WoobleCloud()
    {

    }

    public float _woobleMultiplier = 1.2f;
    public float _woobleMaxTimer;
    public float _woobleStartTimer;
}