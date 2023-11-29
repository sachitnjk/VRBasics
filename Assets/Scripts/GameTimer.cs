using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
	public float elapsedTime { get; private set; }
	public bool reachedHoldOutTime { get; private set; }
	[field: SerializeField] public float holdOutTime{get; private set; }
	private void Update()
	{
		elapsedTime = Time.time;

		HoldOutChecker();
	}

	private void HoldOutChecker()
	{
		if(elapsedTime >= holdOutTime)
		{
			elapsedTime = Mathf.Clamp(elapsedTime, 0f, holdOutTime);

			reachedHoldOutTime = true;
		}
		else
		{
			reachedHoldOutTime = false;
		}
	}
}
