using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	[SerializeField] float bulletDamage;
	[SerializeField] float disableTime;
	private HealthController droneHealthComponent;

	private void Update()
	{
		StartCoroutine(DestroyOverTime(disableTime));
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Drone") || other.gameObject.CompareTag("Surface"))
		{
			droneHealthComponent = other.gameObject.GetComponent<HealthController>();
			droneHealthComponent?.DamageHealth(bulletDamage);
			this.gameObject.SetActive(false);
		}
	}

	private IEnumerator DestroyOverTime(float time)
	{
		yield return new WaitForSeconds(time);
		this.gameObject.SetActive(false);
	}
}
