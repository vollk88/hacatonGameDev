using AI.Enemy.State;
using AI.State;
using BaseClasses;
using Unit.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace AI.Enemy
{
	public class Enemy : AUnit
	{
		[SerializeField]
		private float fovAngle = 45f;
		[SerializeField]
		private float fovRadius = 5f;
		[SerializeField]
		private float hearingRadius = 10f;
		[SerializeField]
		Transform fovOrigin;
		
		[SerializeField]
		private float attackDistance = 1f;
		
		private	EnemyStateMachine _stateMachine;
		private Perception.Perception _perception;
		
		
		public float AttackDistance => attackDistance;
		public Perception.Perception Perception => _perception;
		public GameObject Target => _perception.Target;
		public override AStateMachine StateMachine => _stateMachine ??= new EnemyStateMachine(this);

		protected override void Awake()
		{
			base.Awake();
			_perception = new Perception.Perception(enemy:this, fovAngle: fovAngle,
				fovRadius : fovRadius, fovOrigin: fovOrigin, hearingRadius: hearingRadius);
		}

		protected override void Start()
		{
			base.Start();
			StateMachine.SetPatrolPoint(PatrolPull.GetClosestPoint(transform));
			StateMachine.SetState<IdleState>();
		}
		protected override void InitStates()
		{
			base.InitStates();
			StateMachine.AddState(new ChaseState(this));
			StateMachine.AddState(new AttackState(this));
		}

		protected override void Update()
		{
			base.Update();
			_perception.Update();
			
			if (_perception.Target is not null)
			{
				StateMachine.SetState<ChaseState>();
			}
		}
		
		

		private void OnDrawGizmos()
		{
			// show all perception radius
			var position = transform.position;
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(position, hearingRadius);
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(position, fovRadius);
			var forward = transform.forward;
			Gizmos.DrawLine(position, position + Quaternion.Euler(0, fovAngle / 2f, 0) * forward * fovRadius);
			Gizmos.DrawLine(position, position + Quaternion.Euler(0, -fovAngle / 2f, 0) * forward * fovRadius);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(position, attackDistance);
		}
	}
}