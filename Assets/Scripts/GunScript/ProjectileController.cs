using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	[SerializeField] float bulletDamage;
	[SerializeField] float disableTime;
	[SerializeField] Transform effectPoint;
	[SerializeField] GameObject hitEffectPrefab;

	private HealthController droneHealthComponent;

	private void Update()
	{
		StartCoroutine(DestroyOverTime(disableTime));
	}
	private IEnumerator DestroyOverTime(float time)
	{
		yield return new WaitForSeconds(time);
		this.gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Drone") || other.gameObject.CompareTag("Surface"))
		{
			droneHealthComponent = other.gameObject.GetComponent<HealthController>();
			droneHealthComponent?.DamageHealth(bulletDamage);

			GameObject hitEffect = ObjectPooler.instance.GetPooledObject(hitEffectPrefab);

			if (hitEffect != null)
			{
				// Set the hit effect position to the effect point
				hitEffect.transform.position = effectPoint.position;
				// Activate the hit effect
				hitEffect.SetActive(true);
			}

			this.gameObject.SetActive(false);
		}
	}
}
