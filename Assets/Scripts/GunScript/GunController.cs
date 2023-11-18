using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
	[SerializeField] GameObject rightProjSocket;
	[SerializeField] GameObject leftProjSocket;
	[SerializeField] GameObject bulletProjectile;

	private void Update()
	{
		if(VrController_Inputs.Instance.rightTrigger.IsPressed())
		{
			Debug.Log("right shoot");
			PlayerShoot(rightProjSocket);
		}
		if(VrController_Inputs.Instance.leftTrigger.IsPressed())
		{
			Debug.Log("left shoot");
			PlayerShoot(leftProjSocket);
		}
	}

	private void PlayerShoot(GameObject socket)
	{
		GameObject shootProjectile = ObjectPooler.instance.GetPooledObject(bulletProjectile);
		if(shootProjectile != null ) 
		{
			shootProjectile.transform.position = socket.transform.position;
			shootProjectile.SetActive(true);
		}
	}

}
