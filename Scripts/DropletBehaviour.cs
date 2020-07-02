using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DropletBehaviour : MonoBehaviour
{
    public int _waterPower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Garden"))
        {
            collision.GetComponent<GardenManager>().Grow(_waterPower);
            Destroy(gameObject);
        }
    }
}
