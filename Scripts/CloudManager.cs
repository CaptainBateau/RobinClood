using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
	public float _horizontalSpeed;
	Camera _camera;
	float resX;
	private void Awake()
	{
		_camera = Camera.main;
		resX = Screen.currentResolution.width/1000f;
		
	}
	private void Start()
	{
		
	}
	void Update()
	{
		transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * _horizontalSpeed * Time.deltaTime);
		if (transform.position.x < -_camera.orthographicSize * resX)
			transform.position = new Vector3(-_camera.orthographicSize* resX, transform.position.y, transform.position.z);
		if (transform.position.x > _camera.orthographicSize * resX)
			transform.position = new Vector3(_camera.orthographicSize*resX, transform.position.y, transform.position.z);
	}
}
