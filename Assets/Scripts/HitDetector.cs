using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
	int currentDroneCount = 0;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Drone"))
		{
			currentDroneCount++;
			Debug.Log(currentDroneCount);
		}
	}
}
