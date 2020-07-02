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

    public Sprite _pissingFace;
    public Sprite _fillingFace;
    public Sprite _fullFace;
    public Sprite _deadFace;
    public Sprite _hurtFace;
    public Sprite _neutralFace;

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
        if (_isEmptying && _lastEmptyTickTime + (1 / (float)_emptyTickRate) < Time.time && _currentCapacity > 0)
            EmptyCloud();
        if(_currentCapacity == 0)
        {
            _spriteRenderer.sprite = _hurtFace;
        }
        if(!_isRefilling && !_isEmptying && _currentCapacity != 0)
        {
            _spriteRenderer.sprite = _neutralFace;
            if (_currentCapacity == _maxCapacity)
            {
                _spriteRenderer.sprite = _fullFace;
            }
        }

        if (_isWoobling)
        {
            WoobleCloud();
        }
    }

    public void RefillCloud()
    {
        if(_isWoobling == false)
            _woobleStartTimer = Time.time;
        _lastRefillTickTime = Time.time;
        _currentCapacity += _refillPerTick;
        _currentCapacity = Mathf.Clamp(_currentCapacity, 0, _maxCapacity);
        _spriteRenderer.sprite = _fillingFace;
        _isGrowing = true;
        _isWoobling = true;
        if (_currentCapacity == _maxCapacity)
        {
            _spriteRenderer.sprite = _fullFace;
            _isGrowing = false;
            _isWoobling = false;
        }
    }

    public void EmptyCloud()
    {
        _spriteRenderer.sprite = _pissingFace;
        _lastEmptyTickTime = Time.time;
    }
    public void DropWater()
    {
        if (_currentCapacity >= _emptyPerTick)
        {
            DropWaterPower(_emptyPerTick);
            _currentCapacity -= _emptyPerTick;
            _currentCapacity = Mathf.Clamp(_currentCapacity, 0, _maxCapacity);
        }
        else if (_currentCapacity > 0 && _currentCapacity < _emptyPerTick)
        {
            DropWaterPower(_emptyPerTick / (float)_currentCapacity);
            _currentCapacity = 0;
        }
    }

    public void LoseWater(float waterLost = 0) 
    {
        if (_currentCapacity < waterLost)
        {
            //Lose the game
            _spriteRenderer.sprite = _deadFace;

        }
        _currentCapacity -= waterLost;
        _currentCapacity = Mathf.Clamp(_currentCapacity, 0, _maxCapacity);
    }


    public void DropWaterPower(float power = 1)
    {
        Debug.Log("POWERRRRRR " + power);
    }

    ////Change Sprite + wooble
    //public void ChangeStage(int stage, bool increasing = true)
    //{
    //    switch (stage)
    //    {
    //        case 1:
    //            _spriteRenderer.sprite = _faceSprites[1];
    //            _isGrowing = increasing;
    //            _woobleStartTimer = Time.time;
    //            _isWoobling = true;
    //            break;
    //        case 2:

    //            _spriteRenderer.sprite = _faceSprites[2];
    //            _isGrowing = increasing;
    //            _woobleStartTimer = Time.time;
    //            _isWoobling = true;
    //            break;
    //        case 3:

    //            _spriteRenderer.sprite = _faceSprites[3];
    //            _isGrowing = increasing;
    //            _woobleStartTimer = Time.time;
    //            _isWoobling = true;
    //            break;

    //        case 0:
    //            _spriteRenderer.sprite = _faceSprites[0];
    //            _isGrowing = increasing;
    //            _woobleStartTimer = Time.time; 
    //            _isWoobling = true;
    //            break;
    //        default:
    //            break;
               
    //    }
    //}

    public void WoobleCloud()
    {
        if(_isGrowing)
            transform.localScale =  Vector3.one * _growthCurve.Evaluate(((Time.time - _woobleStartTimer)%1) / _woobleMaxTimer);
        //transform.localScale =  Vector3.one * _growthCurve.Evaluate((Time.time - _woobleStartTimer) / _woobleMaxTimer);
        //transform.localScale = Vector3.one * (m_wantedCurved + m_idlt.Evaluate((Time.time % m_time) / m_time) * m_curveMultiplyEffect);
        //else
        //    transform.localScale = Vector3.one * _shrinkCurve.Evaluate((Time.time - _woobleStartTimer) / _woobleMaxTimer);
        if (Time.time - _woobleStartTimer > _woobleMaxTimer)
            _isWoobling = false;
    }

    bool _isWoobling;
    bool _isGrowing;
    public AnimationCurve _growthCurve;
    //public AnimationCurve _shrinkCurve;
    public float _woobleMaxTimer;
    float _woobleStartTimer;
}