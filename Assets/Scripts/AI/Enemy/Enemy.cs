﻿using AI.Enemy.State;
using AI.State;
using Audio;
using UnityEngine;

namespace AI.Enemy
{
	[RequireComponent(typeof(SoundManager))]
	public class Enemy : AUnit
	{
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
		[SerializeField]
		private float attackDelay = 0.4f;
		[SerializeField]
		private int attackDamage = 10;

		[GetOnObject] 
		private SoundManager _soundManager;
		
		private	EnemyStateMachine _stateMachine;
		private Perception.Perception _perception;
		
		
		public float AttackDistance => attackDistance;
		public float AttackDelay => attackDelay;
		public int AttackDamage => attackDamage;
		public Perception.Perception Perception => _perception;
		public GameObject Target => _perception.Target;
		public override AStateMachine StateMachine => _stateMachine ??= new EnemyStateMachine(this);
		public SoundManager SoundManager => _soundManager;

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
			
			if (_perception.Target is not null && 
			    StateMachine.CurrentState is not AttackState &&
			    StateMachine.CurrentState is not ChaseState)
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