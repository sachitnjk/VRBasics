using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Go to Target")]
public class GoToTarget : FlockBehaviour
{
	public float desiredDistance = 5f;

	private float distanceToTarget;

	private Vector3 moveDirection;
	private Vector3 desiredPosition;
	public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, GameObject target)
	{
		if(target == null)
		{
			return Vector3.zero;
		}

		moveDirection = target.transform.position - agent.transform.position;
		distanceToTarget = moveDirection.magnitude;

		if(distanceToTarget < desiredDistance) 
		{
			return Vector3.zero;
		}

		desiredPosition = target.transform.position - moveDirection.normalized * desiredDistance;
		moveDirection = desiredPosition - agent.transform.position;

		return moveDirection.normalized;
	}
}
