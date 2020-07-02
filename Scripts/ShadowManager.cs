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

    Rigidbody2D rb;
    int i;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        waterManager = GetComponentInParent<WaterManager>();
    }

    public void Update()
    {
        rb.velocity = Vector2.up * Input.GetAxisRaw("Vertical") * _Speed * Time.deltaTime;

        //transform.Translate(Input.GetAxis("Vertical") * Vector3.up * _Speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Space) && overPool)
        {
            Vacuum();
        }
        if (Input.GetKey(KeyCode.Space) && overGarden)
        {
            Release();
        }
    }

    void Vacuum()
    {
        waterManager.RefillCloud();
    }

    void Release()
    {
        waterManager.EmptyCloud();
        for (int i = 0; i < _waterDropsSpawner.Length; i++)
        {
            GameObject tempSpawn = _waterDropsSpawner[i].Spawn(_dropletsPrefab.gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Pool"))
        {
            overPool = true;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Garden"))
        {
            overGarden = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Pool"))
        {
            overPool = false;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Garden"))
        {
            overGarden = false;
        }
    }
}
