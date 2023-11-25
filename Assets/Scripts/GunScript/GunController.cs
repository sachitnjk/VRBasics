using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
	[Header("Attributes")]
	[SerializeField] float projectileSpeed;
	[SerializeField] float fireRate = 0.5f;
	[SerializeField] float spreadAngle = 5f;
	[Header("Mag sizes")]
	public int leftMagazineSize;
	public int rightMagazineSize;

	[Header("References")]
	[SerializeField] GameObject bulletProjectile;
	[SerializeField] GunEffects rightControllerShootScript;
	[SerializeField] GunEffects leftControllerShootScript;
	[SerializeField] private TextMeshProUGUI leftDebugTextBox;
	[SerializeField] private TextMeshProUGUI rightDebugTextBox;

	private GameObject rightProjSocket;
	private GameObject leftProjSocket;

	private float rightNextFireTime = 0f;
	private float leftNextFireTime = 0f;

	private int leftBulletsInMagazine;
	private int rightBulletsInMagazine;

	private bool leftCanShoot;
	private bool rightCanShoot;

	private void Start()
	{
		rightProjSocket = rightControllerShootScript.barrelLocation.gameObject;
		leftProjSocket = leftControllerShootScript.barrelLocation.gameObject;

		leftCanShoot = true;
		rightCanShoot = true;
		leftBulletsInMagazine = leftMagazineSize;
		rightBulletsInMagazine = rightMagazineSize;

	}

	private void Update()
	{
		if(VrController_Inputs.Instance.rightTrigger.IsPressed() && rightCanShoot)
		{
			TryShoot(rightProjSocket, rightControllerShootScript, ref rightNextFireTime);

		}
		if(VrController_Inputs.Instance.leftTrigger.IsPressed() && leftCanShoot)
		{
			TryShoot(leftProjSocket, leftControllerShootScript, ref leftNextFireTime);
		}

		if(leftBulletsInMagazine <= 0)
		{
			leftCanShoot = false;
			if(!leftCanShoot)
			{
				StartReload(leftBulletsInMagazine, leftMagazineSize, leftCanShoot, (result) => leftBulletsInMagazine = result, (result) => leftCanShoot = result);
			}
		}
		if(rightBulletsInMagazine <= 0)
		{
			rightCanShoot = false;
			if(!rightCanShoot)
			{
				StartReload(rightBulletsInMagazine, rightMagazineSize, rightCanShoot, (result) => rightBulletsInMagazine = result, (result) => rightCanShoot = result);
			}
		}

		leftDebugTextBox.text = leftBulletsInMagazine.ToString();
		rightDebugTextBox.text = rightBulletsInMagazine.ToString();
	}

	private void StartReload(int bulletsInMagazine, int magazineSize, bool directionalCanShoot, Action<int> callback, Action<bool> boolCallback)
	{
		StartCoroutine(ReloadAfterDelay(bulletsInMagazine, magazineSize, directionalCanShoot, callback, boolCallback));
	}

	private IEnumerator ReloadAfterDelay(int bulletsInMagazine, int magazineSize, bool directionalCanShoot, Action<int> callback, Action<bool> boolCallback)
	{
		yield return new WaitForSeconds(3f);
		bulletsInMagazine += magazineSize;
		directionalCanShoot = true;

		callback(bulletsInMagazine);
		boolCallback(directionalCanShoot);
	}

	private void TryShoot(GameObject socket, GunEffects gunEffects, ref float nextFireTime)
	{
		if(Time.time >= nextFireTime)
		{
			PlayerShoot(socket);
			gunEffects.Shoot();

			if (socket == leftProjSocket && leftCanShoot)
			{
				leftBulletsInMagazine--;
			}
			else if (socket == rightProjSocket && rightCanShoot)
			{
				rightBulletsInMagazine--;
			}

			nextFireTime = Time.time + 1f/fireRate;
		}
	}

	private void PlayerShoot(GameObject socket)
	{
		GameObject shootProjectile = ObjectPooler.instance.GetPooledObject(bulletProjectile);
		if(shootProjectile != null ) 
		{
			shootProjectile.transform.position = socket.transform.position;

			//Angle deviation for bullets
			Quaternion randomRotation = Quaternion.Euler(UnityEngine.Random.Range(-spreadAngle, spreadAngle), UnityEngine.Random.Range(-spreadAngle, spreadAngle), 0f);
			shootProjectile.transform.rotation = socket.transform.rotation * randomRotation;

			shootProjectile.SetActive(true);

			Rigidbody projectileRigidbody = shootProjectile.GetComponent<Rigidbody>();
			if(projectileRigidbody != null) 
			{
				Vector3 shootDirection = shootProjectile.transform.forward;
				projectileRigidbody.velocity = shootDirection * projectileSpeed;
			}
		}
	}
}
