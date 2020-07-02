using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
	public float _horizontalSpeed;

	void Update()
	{
		transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * _horizontalSpeed * Time.deltaTime);
	}
}
