using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public bool isFullScreen = false;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		FullScreenMode();
	}

	void FullScreenMode()
	{
		if (isFullScreen == false)
		{
			Screen.fullScreen = true;
		}
		else Screen.fullScreen = false;
	}
}
