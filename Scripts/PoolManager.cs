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


	private void Awake()
	{
		_currentCapacity = _maxCapacity;
		_tickTime = 1 / (float)_refillPerSecond;
	}
	void Update()
	{
		if (Time.time > _refillTimer + _tickTime) 
			RefillPool();
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
}
