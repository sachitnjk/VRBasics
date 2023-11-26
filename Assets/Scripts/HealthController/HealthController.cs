using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
	[field: SerializeField] public float MaxHealth {get; private set;}
	[SerializeField] private GameObject destroyEffect;

	public float CurrentHealth{get; private set;}

	private void OnEnable()
	{
		CurrentHealth = MaxHealth;
	}
	private void Start()
	{
		CurrentHealth = MaxHealth;
	}

	private void Update()
	{
		CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);
		EntityDeadCheck();
	}

	public void DamageHealth(float damageTaken)
	{
		CurrentHealth -= damageTaken;
	}
	public void HealHealth(float healAmount)
	{
		if(CurrentHealth < MaxHealth)
		{
			CurrentHealth += healAmount;
		}
	}

	public void EntityDeadCheck()
	{
		if(CurrentHealth <= 0f) 
		{
			GameObject deathEffect = ObjectPooler.instance.GetPooledObject(destroyEffect);
			if(deathEffect != null) 
			{
				deathEffect.transform.position = this.gameObject.transform.position;
				deathEffect.SetActive(true);
			}

			this.gameObject.SetActive(false);
		}
		else
		{
			return;
		}
	}
}
