using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{

	public Sprite[] _sprites;
	public float _maxValue;
	public float _refillPerSecond;
	public float _maxCapacity;
	public float _currentCapacity;



	private void Awake()
	{
		_currentCapacity = _maxCapacity;
	}

	public void RemoveWater(float toRemove)
	{

	}
}
