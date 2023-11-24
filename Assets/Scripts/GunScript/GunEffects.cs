using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEffects : MonoBehaviour
{
	[Header("Prefab Refrences")]
	public GameObject muzzleFlashPrefab;

	[Header("Location Refrences")]
	[SerializeField] public Transform barrelLocation;

	[Header("Settings")]
	[SerializeField] private float destroyTimer = 2f;


	void Start()
	{
		if (barrelLocation == null)
			barrelLocation = transform;
	}


	//This function creates the bullet behavior
	public void Shoot()
	{
		if (muzzleFlashPrefab)
		{
			//Create the muzzle flash
			GameObject tempFlash;
			tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

			//Destroy the muzzle flash effect
			Destroy(tempFlash, destroyTimer);
		}
	}
}
