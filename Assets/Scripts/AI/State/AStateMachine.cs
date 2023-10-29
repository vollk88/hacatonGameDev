using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.State
{
	public abstract class AStateMachine
	{
		public float TimeToStay { get; set; }
		public PatrolPoint NextPatrolPoint { get; private set; }

		protected readonly AUnit AUnit;
		public IState CurrentState { get; private set; }
		private readonly Dictionary<Type, IState> _states = new ();

		protected AStateMachine(AUnit aUnit)
		{
			AUnit = aUnit;
		}
		
		public void AddState(IState state)
		{
			var type = state.GetType();
			if (_states.ContainsKey(type))
			{
				Debug.LogError($"State {type} already added");
				return;
			}
			_states.Add(type, state);
		}
		
		public void SetState<T>() where T : IState
		{
			if (CurrentState?.GetType() == typeof(T))
				return;
			
			CurrentState?.Exit();
			CurrentState = _states[typeof(T)];
			CurrentState.Enter();
		}
		
		public virtual void Update()
		{
			CurrentState?.Update();
		}

		public T GetState <T>() where T : IState
		{
			return (T) _states[typeof(T)];
		}

		public void SetWaitTime(float timeToStay)
		{
			TimeToStay = timeToStay;
		}

		public virtual void SetPatrolPoint(PatrolPoint point)
		{
			NextPatrolPoint = point;
		}
	}
}