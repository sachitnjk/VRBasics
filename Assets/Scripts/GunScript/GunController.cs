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

	private GameObject rightProjSocket;
	private GameObject leftProjSocket;

	private void Start()
	{
		rightProjSocket = rightControllerShootScript.barrelLocation.gameObject;
		leftProjSocket = leftControllerShootScript.barrelLocation.gameObject;
	}

	private void Update()
	{
		if(VrController_Inputs.Instance.rightTrigger.IsPressed())
		{
			PlayerShoot(rightProjSocket);
			rightControllerShootScript.Shoot();
		}
		if(VrController_Inputs.Instance.leftTrigger.IsPressed())
		{
			PlayerShoot(leftProjSocket);
			leftControllerShootScript.Shoot();
		}
	}

	private void PlayerShoot(GameObject socket)
	{
		GameObject shootProjectile = ObjectPooler.instance.GetPooledObject(bulletProjectile);
		if(shootProjectile != null ) 
		{
			shootProjectile.transform.position = socket.transform.position;
			shootProjectile.transform.rotation = socket.transform.rotation;

			shootProjectile.SetActive(true);

			Rigidbody projectileRigidbody = shootProjectile.GetComponent<Rigidbody>();
			if(projectileRigidbody != null) 
			{
				Vector3 shootDirection = socket.transform.forward;
				projectileRigidbody.velocity = shootDirection * projectileSpeed;
			}
		}
	}

}
