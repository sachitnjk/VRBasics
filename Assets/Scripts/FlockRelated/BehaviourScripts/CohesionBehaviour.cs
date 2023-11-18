using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
	public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, GameObject target)
	{
		//if no neighbours, return no adjustment
		if(context.Count == 0) 
		{
			return Vector3.zero;
		}

		//add all points together and average
		Vector3 cohesionMove = Vector3.zero;
		List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
		foreach (Transform item in filteredContext)
		{
			cohesionMove += (Vector3)item.position;
		}
		cohesionMove /= context.Count;

		//create offset from agent position
		cohesionMove -= (Vector3)agent.transform.position;

		//finds middle point between all neighbours and tries to move there
		return cohesionMove;
	}
}
