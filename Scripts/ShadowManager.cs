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

    WaterManager waterManager;
    GardenManager gardenManager;
    PoolManager poolManager;

    GameObject gameObjectTemp;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        waterManager = GetComponentInParent<WaterManager>();
    }

    public void Update()
    {
        
        transform.Translate(Input.GetAxis("Vertical") * Vector3.up * _Speed * Time.deltaTime);
        transform.localPosition = new Vector2(0, Mathf.Clamp(transform.localPosition.y, -_minMaxShadowDistanceRange.y, -_minMaxShadowDistanceRange.x));

        if (Input.GetKey(KeyCode.Space) && overPool)
        {
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
        if (gameObjectTemp.TryGetComponent<PoolManager>(out poolManager))
        {
            poolManager.RemoveWater(_waterDrunkBySecond);
            poolManager.SpawnDrops();
        }
    }

    void Release()
    {
        waterManager._isEmptying = true;
        //if (gameObjectTemp.TryGetComponent<GardenManager>(out gardenManager))
        //{
        //    gardenManager.Grow(_waterReleasedBySecond);
        //}

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
            gameObjectTemp = collider.gameObject;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Garden"))
        {
            overGarden = true;
            gameObjectTemp = collider.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Pool"))
        {
            overPool = false;
            gameObjectTemp = null;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Garden"))
        {
            overGarden = false;
            gameObjectTemp = null;
        }
    }
}
