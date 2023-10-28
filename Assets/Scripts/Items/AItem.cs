using BaseClasses;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Inventory;
using UnityEngine.Serialization;

namespace Items
{
	public abstract class AItem : CustomBehaviour, IItem
	{
		[Tooltip("UI иконка для предмета.")][SerializeField]
		private Sprite icon;
		
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
		
		public Sprite GetIcon() => icon;
	}
}