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
		private float fowAngle = 45f;
		[SerializeField]
		private float fowRadius = 5f;
		[SerializeField]
		private float hearingRadius = 10f;
		[SerializeField]
		Transform fowOrigin;
		
		private	EnemyStateMachine _stateMachine;
		private Perception.Perception _perception;
		
		
		public Perception.Perception Perception => _perception;
		public GameObject Target => _perception.Target;
		public override AStateMachine StateMachine => _stateMachine ??= new EnemyStateMachine(this);

		protected override void Awake()
		{
			base.Awake();
			_perception = new Perception.Perception(enemy:this, fowAngle: fowAngle,
				fowRadius : fowRadius, fowOrigin: fowOrigin, hearingRadius: hearingRadius);
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
			
			if (_perception.Target is null)
			{
				StateMachine.SetState<ChaseState>();
			}
		}
	}
}