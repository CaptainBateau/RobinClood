using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Spawn(GameObject prefab)
    {
        return Instantiate(prefab, transform.position, transform.rotation);
    }
}
