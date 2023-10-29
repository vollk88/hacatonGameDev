using AI.Enemy.State;
using AI.State;
using Damage;
using UnityEngine;

namespace AI.Enemy
{
	public class Enemy : AUnit
	{
		#if UNITY_EDITOR 
		[SerializeField]
		private bool showGizmos;
		#endif
		
		[Header("Зоны обзора и слуха")]
		[SerializeField]
		private float fovAngle = 45f;
		[SerializeField]
		private float fovRadius = 5f;
		[SerializeField]
		private float hearingRadius = 10f;
		
		[SerializeField, Header("Голова")]
		Transform fovOrigin;
		
		[Header("Параметры атаки")]
		[SerializeField]
		private float attackDistance = 1f;
		// [SerializeField]
		// private float attackDelay = 0.4f;
		[SerializeField]
		private int attackDamage = 10;
		
		private	EnemyStateMachine _stateMachine;
		private Perception.Perception _perception;

		[GetOnObject]
		private IDamage _damageStrategy;
		
		#region	Properties
		public float AttackDistance => attackDistance;
		// public float AttackDelay => attackDelay;
		// public int AttackDamage => attackDamage;
		public Perception.Perception Perception => _perception;
		public IDamage DamageStrategy => _damageStrategy;
		public GameObject Target => _perception.Target;
		public override AStateMachine StateMachine => _stateMachine ??= new EnemyStateMachine(this);
		#endregion

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
			_damageStrategy.Damage = attackDamage;
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
			
			if (_perception.Target is not null && 
			    StateMachine.CurrentState is not AttackState &&
			    StateMachine.CurrentState is not ChaseState)
			{
				StateMachine.SetState<ChaseState>();
			}
		}
		
		

		#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (!showGizmos) return;
			
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
		#endif
	}
}