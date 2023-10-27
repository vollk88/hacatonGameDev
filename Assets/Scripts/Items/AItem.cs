using System;
using BaseClasses;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items
{
	public abstract class AItem : CustomBehaviour, IItem
	{
		public Sprite Icon { get; set; }
		public void Take(InputAction.CallbackContext obj)
		{
			throw new NotImplementedException();
		}

		public void Drop()
		{
			throw new System.NotImplementedException();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				InputManager.PlayerActions.Take.started += Take;
			}	
		}


		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				InputManager.PlayerActions.Take.started -= Take;
			}
		}
	}
}