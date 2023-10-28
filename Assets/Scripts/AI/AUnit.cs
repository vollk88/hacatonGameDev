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

		[SerializeField]
		private int speed = 15;
		
        [GetOnObject] 
        private NavMeshAgent _agent;
        [GetOnObject]
        private Collider _collider;
        [GetOnObject]
        private Rigidbody _rigidbody;


		#region properties

		protected NavMeshAgent Agent => _agent;
		protected Collider Collider => _collider;
		protected Rigidbody Rigidbody => _rigidbody;

		#endregion

		protected abstract void InitStates();


        protected virtual void Start()
        {
	        InitStates();
	        // Agent.speed = speed;
        }

        protected override void OnEnable()
        {
	        base.OnEnable();
	        PatrolPull.PatrolActors.Add(this);
        }

        protected virtual void Update()
        {
	        StateMachine.Update();
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

        public void SetWaitTime(float timeToStay)
        {
	        
        }
    }
}
