using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class VacuumedDropsBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform _cloud;
    public float _speedEvaporate;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _cloud.position, _speedEvaporate * Time.deltaTime);
        if (transform.position == _cloud.position)
            Destroy(this.gameObject);
    }
}
