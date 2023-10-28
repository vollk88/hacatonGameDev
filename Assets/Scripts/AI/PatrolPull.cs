using System.Collections.Generic;
using BaseClasses;
using UnityEngine;

namespace AI
{
	public static class PatrolPull
	{
		public static readonly List<IPatrolActor> PatrolActors;
		public static readonly List<PatrolPoint> PatrolPoints;

		static PatrolPull()
		{
			PatrolActors = new();
			PatrolPoints = new();
		}

		public static PatrolPoint GetClosestPoint(Transform transform)
		{
			PatrolPoint closestPoint = null;
			float minDistance = float.MaxValue;
			foreach (var patrolPoint in PatrolPoints)
			{
				float distance = Vector3.Distance(transform.position, patrolPoint.transform.position);
				if (distance < minDistance)
				{
					minDistance = distance;
					closestPoint = patrolPoint;
				}
			}

			return closestPoint;
		}
		
		public static void UpdatePatrolPoints()
		{
			PatrolPoints.AddRange(CustomBehaviour.Instances[typeof(PatrolPoint)]
				.ConvertAll(x => x as PatrolPoint));
		}
	}
}