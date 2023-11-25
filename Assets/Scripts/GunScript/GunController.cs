using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
	[SerializeField] GunEffects rightControllerShootScript;
	[SerializeField] GunEffects leftControllerShootScript;

	[SerializeField] GameObject bulletProjectile;

	[SerializeField] float projectileSpeed;
	[SerializeField] float fireRate = 0.5f;
	[SerializeField] float spreadAngle = 5f;

	[Header("Left and right references/Attributes")]
	[SerializeField] private TextMeshProUGUI leftDebugTextBox;
	[SerializeField] private TextMeshProUGUI rightDebugTextBox;
	public int leftMagazineSize;
	public int rightMagazineSize;

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

		leftDebugTextBox.text = leftBulletsInMagazine.ToString();
		rightDebugTextBox.text = rightBulletsInMagazine.ToString();
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

		AutoReload(leftMagazineSize ,leftBulletsInMagazine, leftCanShoot);
		AutoReload(rightMagazineSize, rightBulletsInMagazine, rightCanShoot);
	}

	private void PlayerShoot(GameObject socket)
	{
		GameObject shootProjectile = ObjectPooler.instance.GetPooledObject(bulletProjectile);
		if(shootProjectile != null ) 
		{
			shootProjectile.transform.position = socket.transform.position;

			//Angle deviation for bullets
			Quaternion randomRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0f);
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

	private void AutoReload(float directionalMaxMagSize, float bulletsInMagazine, bool directionalCanShoot)
	{
		if(bulletsInMagazine <= directionalMaxMagSize)
		{
			//Play reload sound effect
			//Play smoke effect
			StartCoroutine(ReloadAfter(2f, directionalMaxMagSize, bulletsInMagazine, directionalCanShoot));
		}
	}

	private IEnumerator ReloadAfter(float delay,float directionalMagazineSize, float bulletsInMagazine, bool directionalCanShoot)
	{
		directionalCanShoot = false;
		yield return new WaitForSeconds(delay);
		bulletsInMagazine = directionalMagazineSize;
		directionalCanShoot = true;
	}

}
