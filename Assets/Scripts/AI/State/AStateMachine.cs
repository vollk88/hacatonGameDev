using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.State
{
	public abstract class AStateMachine
	{
		public abstract AUnit AUnit { get; protected set; }
		
		protected IState CurrentState;
		protected Dictionary<Type, IState> States = new ();

		public virtual void Init(AUnit aUnit)
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
		
		public void SetState(IState state)
		{
			CurrentState?.Exit();
			CurrentState = state;
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
	}
}