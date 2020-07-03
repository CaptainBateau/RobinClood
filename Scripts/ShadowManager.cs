using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    public float _Speed;
    public float _waterDrunkBySecond;
    public float _waterReleasedBySecond;

    public Vector2 _minMaxShadowDistanceRange;

    public LayerMask _poolLayerMask;
    public LayerMask _gardenLayerMask;

    bool overPool = false;
    bool overGarden = false;

    public Spawner[] _waterDropsSpawner;
    public GameObject _dropletsPrefab;
    public Vector2 _minMaxWaterDropsRange = new Vector2 (0.1f,0.5f);
    float _dropletsTimer;
    float _tempTimer;
    float _vacuumTimer;
    public float _vacuumDelay;

    WaterManager waterManager;
    GardenManager gardenManager;
    PoolManager poolManager;

    GameObject gameObjectTempGarden;
    GameObject gameObjectTempPool;

    public Vector2 _minMaxScaleRange = new Vector2(0.8f, 1.2f);
    Vector3 _startingScale;
    private void Awake()
    {
        waterManager = GetComponentInParent<WaterManager>();
        _startingScale = transform.localScale;
    }

    public void Update()
    {
        
        transform.Translate(Input.GetAxis("Vertical") * Vector3.up * _Speed * Time.deltaTime);
        transform.localPosition = new Vector2(0, Mathf.Clamp(transform.localPosition.y, -_minMaxShadowDistanceRange.y, -_minMaxShadowDistanceRange.x));
        float temp = Mathf.Lerp(_minMaxScaleRange.y, _minMaxScaleRange.x, Mathf.InverseLerp(-_minMaxShadowDistanceRange.y, -_minMaxShadowDistanceRange.x, transform.localPosition.y));
        transform.localScale = new Vector3(_startingScale.x*temp, _startingScale.y* temp,1);
        if (Input.GetKey(KeyCode.Space) && overPool && Time.time>_vacuumTimer+_vacuumDelay)
        {
            _vacuumTimer = Time.time;
            Vacuum();
        }
        if (Input.GetKey(KeyCode.Space) && overGarden && waterManager._currentCapacity > 0)
        {
            Release();
            if (Time.time > _tempTimer + _dropletsTimer)
            {
                SpawnDrops();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            waterManager._isRefilling = false;
            waterManager._isEmptying = false;
        }
    }

    void Vacuum()
    {
        waterManager._isRefilling = true;
        if (gameObjectTempPool.TryGetComponent<PoolManager>(out poolManager))
        {
            if (poolManager._currentCapacity >= poolManager._maxCapacity / 10)
            {
                poolManager.RemoveWater(_waterDrunkBySecond * (float)1 / _vacuumDelay);
                poolManager.SpawnDrops();
            }
        }
    }

    void Release()
    {
        waterManager._isEmptying = true;
    }

    void SpawnDrops()
    {
        _tempTimer = Time.time;
        int i = Random.Range(0, _waterDropsSpawner.Length);
        _dropletsTimer = Random.Range(_minMaxWaterDropsRange.x, _minMaxWaterDropsRange.y);
        GameObject tempSpawn = _waterDropsSpawner[i].Spawn(_dropletsPrefab.gameObject);
        waterManager.DropWater();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.layer == LayerMask.NameToLayer("Pool"))
        {
            overPool = true;
            gameObjectTempPool = collider.gameObject;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Garden"))
        {
            overGarden = true;
            gameObjectTempGarden = collider.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Pool"))
        {
            overPool = false;
            gameObjectTempPool = null;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Garden"))
        {
            overGarden = false;
            gameObjectTempGarden = null;
        }
    }
}
