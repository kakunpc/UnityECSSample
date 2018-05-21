using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {
	
	// for fps calculation.
	private int frameCount;
	private float elapsedTime;
	private double frameRate;

	[SerializeField]
	private Text _uiText;

	private void Start()
	{
		Application.targetFrameRate = 120;
		_uiText.text = "FPS:0.00";
	}

	private void Update()
	{
		frameCount++;
		elapsedTime += Time.deltaTime;
		if (elapsedTime > 0.5f)
		{
			frameRate = System.Math.Round(frameCount / elapsedTime, 1, System.MidpointRounding.AwayFromZero);
			frameCount = 0;
			elapsedTime = 0;
			_uiText.text = "FPS:" + frameRate.ToString("0.00");
		}

	}

}