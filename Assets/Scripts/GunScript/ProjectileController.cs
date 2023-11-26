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
	private HealthController playerHealthController;

	private void Start()
	{
		playerHealthController = GameManager.instance.playerHealthController;
	}

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
				hitEffect.transform.position = effectPoint.position;
				hitEffect.SetActive(true);
			}

			if(other.gameObject.CompareTag("Drone"))
			{
				playerHealthController.HealHealth(0.5f);
			}

			this.gameObject.SetActive(false);
		}
	}
}
