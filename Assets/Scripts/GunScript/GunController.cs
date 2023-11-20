using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
	[SerializeField] SimpleShoot rightControllerShootScript;
	[SerializeField] SimpleShoot leftControllerShootScript;

	[SerializeField] GameObject bulletProjectile;

	[SerializeField] float projectileSpeed;
	[SerializeField] float fireRate = 0.5f;
	[SerializeField] float spreadAngle = 5f;

	private GameObject rightProjSocket;
	private GameObject leftProjSocket;

	private float rightNextFireTime = 0f;
	private float leftNextFireTime = 0f;

	private void Start()
	{
		rightProjSocket = rightControllerShootScript.barrelLocation.gameObject;
		leftProjSocket = leftControllerShootScript.barrelLocation.gameObject;
	}

	private void Update()
	{
		if(VrController_Inputs.Instance.rightTrigger.IsPressed())
		{
			TryShoot(rightProjSocket, rightControllerShootScript, ref rightNextFireTime);

		}
		if(VrController_Inputs.Instance.leftTrigger.IsPressed())
		{
			TryShoot(leftProjSocket, leftControllerShootScript, ref leftNextFireTime);
		}
	}

	private void TryShoot(GameObject socket, SimpleShoot shootScript, ref float nextFireTime)
	{
		if(Time.time >= nextFireTime)
		{
			PlayerShoot(socket);
			shootScript.Shoot();
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

}
