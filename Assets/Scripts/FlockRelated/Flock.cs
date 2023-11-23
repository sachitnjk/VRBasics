using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
	public FlockAgent agentPrefab;
	List<FlockAgent> agents = new List<FlockAgent>();
	public FlockBehaviour behaviour;
	public GameObject target;
	public GoToTarget goToTargetSO;

	[Range(10, 500)]
	public int startingCount = 250;
	const float AgentDensity = 0.08f;

	[Range(1f, 100f)]
	public float driveFactor = 10f;
	[Range(1f, 100f)]
	public float maxSpeed = 5f;

	[Range(1f, 10f)]
	public float neighborRadius = 1.5f;
	[Range(0f, 1f)]
	public float avoidanceRadiusMultiplier = 0.5f;

	//private float currentDesiredDistance;
	private bool isChangingDistance = false;
	private float timeToWait;
	[Header("Attributes for GoTotarget behaviour")]
	[SerializeField] private float initialTargetDistance = 10f;
	[SerializeReference] private float newTargetDistance = 3f;
	[Range(5f, 10f)]
	[SerializeField] private float minDistanceChangeTime;
	[Range(10f, 15f)]
	[SerializeField] private float maxDistanceChangeTime;

	//calculating squares and comparing squares instead of using roots everytime, saving some math calculation
	float squareMaxSpeed;
	float squareNeighborRadius;
	float squareAvoidanceRadius;
	public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

	private void Start()
	{
		if (goToTargetSO != null) 
		{
			goToTargetSO.desiredDistance = initialTargetDistance;
			Debug.Log(goToTargetSO.desiredDistance);
		}

		squareMaxSpeed = maxSpeed * maxSpeed;
		squareNeighborRadius = neighborRadius * neighborRadius;
		squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

		for (int i = 0; i < startingCount; i++)
		{
			FlockAgent newAgent = Instantiate(
				agentPrefab,
				Random.insideUnitSphere * startingCount * AgentDensity,
				Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
				transform
				);

			newAgent.name = "Agent " + i;
			newAgent.Initialize(this);
			agents.Add( newAgent );
		}
	}

	private void Update()
	{

		if(goToTargetSO != null)
		{
			//currentDesiredDistance = goToTargetSO.desiredDistance;
			DistanceToPlayerCheck();
		}

		foreach(FlockAgent agent in agents)
		{
			List<Transform> context = GetNearbyObjects(agent);

			//for testing neighbour detections

			Vector3 move = behaviour.CalculateMove(agent, context, this, target);
			//multiplying by drive factor to get speedier movement
			move *= driveFactor;
			if (move.sqrMagnitude > squareMaxSpeed)
			{
				//capping out speed if exceeding max speed
				move = move.normalized * maxSpeed;
			}
			agent.Move(move);
		}
		//Debug.Log(goToTargetSO.desiredDistance);
	}

	//getting list of nearby objects with exception of self collider
	List<Transform> GetNearbyObjects(FlockAgent agent)
	{
		List<Transform> context = new List<Transform>();
		Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
		foreach(Collider c in contextColliders)
		{
			if(c != agent.AgentCollider) 
			{
				context.Add(c.transform);
			}
		}
		return context;
	}

	private void DistanceToPlayerCheck()
	{
		if(!isChangingDistance && goToTargetSO.desiredDistance != newTargetDistance)
		{
			StartCoroutine(ChangeTargetDistance(minDistanceChangeTime, maxDistanceChangeTime));
		}
	}

	private IEnumerator ChangeTargetDistance(float minTime, float maxTime)
	{
		isChangingDistance = true;
		goToTargetSO.desiredDistance = newTargetDistance;

		timeToWait = Random.Range(minTime, maxTime);
		yield return new WaitForSeconds(timeToWait);

		goToTargetSO.desiredDistance = initialTargetDistance;

		timeToWait = Random.Range(minTime, maxTime);
		yield return new WaitForSeconds(timeToWait);

		isChangingDistance = false;
	}

}
