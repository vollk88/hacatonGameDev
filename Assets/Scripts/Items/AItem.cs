using BaseClasses;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Inventory;

namespace Items
{
	public abstract class AItem : CustomBehaviour, IItem
	{
		public Sprite Icon { get; set; }
		
		/// <summary>Взять предмет и добавить в инвентарь.</summary>
		public void Take(InputAction.CallbackContext obj)
		{
			InputManager.PlayerActions.Take.started -= Take;
			
			InventoryController.Add(this);
			Destroy(gameObject);
		}

		/// <summary>Выкинуть предмет.</summary>
		public void Drop()
		{
			InventoryController.Remove(this);
			Instantiate(gameObject, transform.position, Quaternion.identity);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!other.gameObject.CompareTag("Player")) return;
			
			InputManager.PlayerActions.Take.started += Take;
		}


		private void OnTriggerExit(Collider other)
		{
			if (!other.gameObject.CompareTag("Player")) return;
			
			InputManager.PlayerActions.Take.started -= Take;
		}
	}
}