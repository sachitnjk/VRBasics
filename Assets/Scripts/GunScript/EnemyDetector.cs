using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
	private Vector3 direction;
	private string directionKey;

	[SerializeField] private TextMeshProUGUI directionTextBox;

	private Dictionary<string, int> directionCount = new Dictionary<string, int>();

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Drone"))
		{
			direction = other.transform.position - transform.position;
			directionKey = GetDirectionKey(direction);

			if(directionKey != null)
			{
				if(!directionCount.ContainsKey(directionKey))
				{
					//Checking if drone from that direction does not exist then setting that droen as the first from that direction
					directionCount[directionKey] = 1;
				}
				else
				{
					//If drone from that direction already exists, then just add to existing number
					directionCount[directionKey]++;
				}

				//Checking if more than 3 drones arfrom the same direction
				if (directionCount[directionKey] >= 1)
				{
					Debug.Log(directionKey);
				}
			}

		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("Drone"))
		{
			directionKey = "";
		}
	}

	private string GetDirectionKey(Vector3 direction)
	{
		if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
		{
			return (direction.x > 0) ? "right" : "left";
		}
		else
		{
			return (direction.z > 0) ? "front" : "back";
		}
	}
}
