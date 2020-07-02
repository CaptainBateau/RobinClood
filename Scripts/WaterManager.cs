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

    public SpriteRenderer _cloudSprite;
    Transform _cloudTransform;
    public Sprite[] _faceSprites;

    public Sprite _pissingFace;
    public Sprite _fillingFace;
    public Sprite _fullFace;
    public Sprite _deadFace;
    public Sprite _hurtFace;
    public Sprite _neutralFace;
    bool isDead = false;

    private void Awake()
    {
        _cloudTransform = _cloudSprite.transform;
    }
    private void Update()
    {
        if (_isRefilling && _lastRefillTickTime + (1 / (float)_refillTickRate) < Time.time)
            RefillCloud();
        if (_isEmptying && _lastEmptyTickTime + (1 / (float)_emptyTickRate) < Time.time && _currentCapacity > 0)
            EmptyCloud();
        if(_currentCapacity == 0)
        {
            _cloudSprite.sprite = _hurtFace;
        }
        if(!_isRefilling && !_isEmptying && _currentCapacity != 0)
        {
            _cloudSprite.sprite = _neutralFace;
            if (_currentCapacity == _maxCapacity)
            {
                _cloudSprite.sprite = _fullFace;
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
        _cloudSprite.sprite = _fillingFace;
        _isGrowing = true;
        _isWoobling = true;
        if (_currentCapacity == _maxCapacity)
        {
            _cloudTransform.transform.localScale = Vector3.one;
            _cloudSprite.sprite = _fullFace;
            _isGrowing = false;
            _isWoobling = false;
        }
    }

    public void EmptyCloud()
    {
        _cloudSprite.sprite = _pissingFace;
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
            _cloudSprite.sprite = _deadFace;
            isDead = true;

        }
        _currentCapacity -= waterLost;
        _currentCapacity = Mathf.Clamp(_currentCapacity, 0, _maxCapacity);
    }


    public void DropWaterPower(float power = 1)
    {
        Debug.Log("POWERRRRRR " + power);
    }

    public void WoobleCloud()
    {
        if(_isGrowing)
            _cloudTransform.transform.localScale =  Vector3.one * _growthCurve.Evaluate(((Time.time - _woobleStartTimer)%1) / _woobleMaxTimer);
        if (Time.time - _woobleStartTimer > _woobleMaxTimer)
        {
            _cloudTransform.transform.localScale = Vector3.one;
            _isWoobling = false;
        }
    }

    bool _isWoobling;
    bool _isGrowing;
    public AnimationCurve _growthCurve;
    public float _woobleMaxTimer;
    float _woobleStartTimer;
}