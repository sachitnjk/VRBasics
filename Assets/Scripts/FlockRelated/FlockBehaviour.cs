using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class FlockBehaviour : ScriptableObject
{
	public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, GameObject target);
}
