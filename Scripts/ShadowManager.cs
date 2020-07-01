using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    public float _Speed;

    public LayerMask _poolLayerMask;
    public LayerMask _gardenLayerMask;

    bool overPool = false;
    bool overGarden = false;

    public Spawner[] _waterDropsSpawner;
    public GameObject _dropletsPrefab;

    WaterManager waterManager;

    GameObject _cloud;

    public void Update()
    {
        transform.Translate(Input.GetAxis("Vertical") * Vector3.up * _Speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Space) && overPool)
        {
            Vacuum();
        }
        if (Input.GetKey(KeyCode.Space) && overPool)
        {
            Release();
        }
    }

    void Vacuum()
    {
        if (_cloud.TryGetComponent<WaterManager>(out waterManager))
        {
            waterManager.RefillCloud();
        }
    }

    void Release()
    {
        if (_cloud.TryGetComponent<WaterManager>(out waterManager))
        {
            waterManager.EmptyCloud();
            for (int i = 0; i < _waterDropsSpawner.Length; i++)
            {
                GameObject tempSpawn = _waterDropsSpawner[i].Spawn(_dropletsPrefab.gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == _poolLayerMask)
        {
            overPool = true;
        }
        if (collider.gameObject.layer == _gardenLayerMask)
        {
            overGarden = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        overPool = false;
        overGarden = false;
    }
}
