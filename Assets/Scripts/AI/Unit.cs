using AI.State;
using BaseClasses;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public abstract class Unit : CustomBehaviour
    {
        [GetOnObject] private NavMeshAgent _agent;
        [GetOnObject] private Collider _collider;
        [GetOnObject] private Rigidbody _rigidbody;


#region properties

		public NavMeshAgent Agent => _agent;
		public Collider Collider => _collider;
		public Rigidbody Rigidbody => _rigidbody;
		public abstract AStateMachine StateMachine { get; }

#endregion
        void Start()
        {
			StateMachine.Init();
        }

        // Update is called once per frame
        void Update()
        {
	        StateMachine.Update();
        
        }
    }
}
