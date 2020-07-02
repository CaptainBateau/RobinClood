using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    public float _speed = 10;
    public float _missileLifeTime = 3;
    public float _waterLostOnHit = 10;

    private void Start()
    {
        Destroy(gameObject, _missileLifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        WaterManager waterManager;
        if(collider.gameObject.TryGetComponent<WaterManager>(out waterManager))
        {
            waterManager.LoseWater(_waterLostOnHit);
        }
        Destroy(this.gameObject);
    }
}
