﻿using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
	public GameObject platform;
	public float moveSpeed;
	public Transform currentPoint;
	public Transform[] points;
	public int pointSelection;

	void Start ()
    {
        currentPoint = points[pointSelection];
	}

	void Update ()
    {
		platform.transform.position = Vector3.MoveTowards (platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

		if (platform.transform.position == currentPoint.position) 
		{
			pointSelection++;

			if (pointSelection == points.Length) 
			{
				pointSelection = 0;				
			}
			currentPoint = points [pointSelection];
		}

	}
}
