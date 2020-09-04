﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOrbit : MonoBehaviour {

	public Transform target ;
	public float  distance = 10.0f;

	public float  xSpeed = 250.0f;
	public float  ySpeed = 120.0f;

	public float  yMinLimit = -20f;
	public float  yMaxLimit = 80f;

	public float  rotatespeed = 5f;

	private float  x = 0.0f;
	private float  y = 0.0f;

	bool AutoRotate = true;

	[HideInInspector]public bool canDrag;

	void Start () {
		var angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

	void LateUpdate () {
		if (target) 
		{
			if (AutoRotate == false && canDrag)
			{
				x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
				y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			}

			y = ClampAngle(y, yMinLimit, yMaxLimit);

			Quaternion rotation = Quaternion.Euler(y, x, 0);
			Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

			transform.rotation = rotation;
			transform.position = position;
		}
		if (!canDrag || AutoRotate == true) 
		{
			x += rotatespeed;
		}
	}


	static float  ClampAngle ( float angle ,   float min ,   float max  ){

		if (angle < -360)

			angle += 360;

		if (angle > 360)

			angle -= 360;

		return Mathf.Clamp (angle, min, max);

	}

	public void SetCanDrag(bool state)
	{
		canDrag = state; 
	}

}
