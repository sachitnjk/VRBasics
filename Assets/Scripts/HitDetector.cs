using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
	private HealthController healthController;

	private void Start()
	{
		healthController = GetComponent<HealthController>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Drone"))
		{
			healthController.DamageHealth(1);
		}
	}
}
