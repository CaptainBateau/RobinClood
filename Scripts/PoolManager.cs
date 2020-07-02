using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
	public SpriteRenderer _poolRenderer;
	public Sprite[] _sprites;
	public float _refillPerSecond = 10;
	public int _refreshPerSecond;
	public float _maxCapacity;
	public float _currentCapacity;
	float _refillTimer;

	float _tickTime;

	public float _topOfThePool;
	public float _bottonOfThePool;



	private void Awake()
	{
		_currentCapacity = _maxCapacity;
		_tickTime = 1 / (float)_refillPerSecond;
	}
	void Update()
	{
		if (Time.time > _refillTimer + _tickTime) 
			RefillPool();
		MoveSprite();
	}
	public void RemoveWater(float toRemove)
	{
		_currentCapacity -= toRemove;
		_currentCapacity = Mathf.Clamp(_currentCapacity, 0, _maxCapacity);
	}

	public void RefillPool()
	{
		_refillTimer = Time.time;
		_currentCapacity += _refillPerSecond/(float)_refreshPerSecond;
		_currentCapacity = Mathf.Clamp(_currentCapacity, 0, _maxCapacity);
	}

	public void MoveSprite()
	{
		_poolRenderer.transform.localPosition = new Vector3(_poolRenderer.transform.localPosition.x, 
												Mathf.Lerp(_bottonOfThePool, _topOfThePool, _currentCapacity / (float)_maxCapacity),
												_poolRenderer.transform.localPosition.z);
		if (_currentCapacity <= _maxCapacity / 10f)
		{
			_poolRenderer.enabled = false;
		}
		else
			_poolRenderer.enabled = true;
	}
}
