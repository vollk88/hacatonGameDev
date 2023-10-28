using System;
using AI.State;
using BaseClasses;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace AI
{
    public abstract class AUnit : CustomBehaviour, IPatrolActor
    {
	    public abstract AStateMachine StateMachine { get; }
		public bool OnPatrol { get; set; }

		[Header("Скорость")]
		[SerializeField]
		private float speed = 2f;
		[SerializeField]
		private float runSpeed = 3.5f;
		
        [GetOnObject] 
        private NavMeshAgent _agent;
        [GetOnObject]
        private Collider _collider;
        [GetOnObject]
        private Rigidbody _rigidbody;
        
        #if UNITY_EDITOR
	    private string _currentState;
	    #endif


		#region properties

		public float Speed => speed;
		public float RunSpeed => runSpeed;
		protected NavMeshAgent Agent => _agent;
		protected Collider Collider => _collider;
		protected Rigidbody Rigidbody => _rigidbody;

		#endregion

		protected virtual void InitStates()
		{
			StateMachine.AddState(new IdleState(this));
			StateMachine.AddState(new PatrolState(this));
		}


        protected virtual void Start()
        {
	        InitStates();
	        Agent.speed = speed;
        }

        protected override void OnEnable()
        {
	        base.OnEnable();
	        PatrolPull.PatrolActors.Add(this);
        }

        protected virtual void Update()
        {
	        StateMachine.Update();
		#if	UNITY_EDITOR
	        _currentState = StateMachine.CurrentState.ToString();	        
		#endif
        }

        protected override void OnDisable()
        {
	        base.OnDisable();
	        PatrolPull.PatrolActors.Remove(this);
        }

        public void SetPatrolPoint(PatrolPoint point, float timeToStay = 0f)
        {
	        StateMachine.SetPatrolPoint(point);
	        StateMachine.SetWaitTime(timeToStay);
        }

        public void MoveTo(Vector3 transformPosition)
        {
	        Agent.SetDestination(transformPosition);
        }

        public void StopMove()
        {
	        Agent.isStopped = true;
        }
        public void StartMove()
		{
	        Agent.isStopped = false;
		}

        public void SetSpeed(float newSpeed)
        {
	        Agent.speed = newSpeed;
        }
    }
}
