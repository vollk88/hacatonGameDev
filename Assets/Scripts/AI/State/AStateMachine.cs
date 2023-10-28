using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.State
{
	public abstract class AStateMachine
	{
		public float TimeToStay { get; set; }
		
		protected readonly AUnit AUnit;
		protected IState CurrentState;
		protected Dictionary<Type, IState> States = new ();

		protected AStateMachine(AUnit aUnit)
		{
			AUnit = aUnit;
		}
		
		public void AddState(IState state)
		{
			var type = state.GetType();
			if (States.ContainsKey(type))
			{
				Debug.LogError($"State {type} already added");
				return;
			}
			States.Add(type, state);
		}
		
		public void SetState<T>() where T : IState
		{
			CurrentState?.Exit();
			CurrentState = States[typeof(T)];
			CurrentState.Enter();
		}
		
		public virtual void Update()
		{
			CurrentState?.Update();
		}

		public T GetState <T>() where T : IState
		{
			return (T) States[typeof(T)];
		}

		public abstract void SetPatrolPoint(PatrolPoint point);

		public void SetWaitTime(float timeToStay)
		{
			TimeToStay = timeToStay;
		}

	}
}