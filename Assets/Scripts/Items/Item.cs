using System;
using BaseClasses;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Inventory;

namespace Items
{
	[Serializable]
	public class Item : CustomBehaviour, IItem
	{
		[Tooltip("Тип")][SerializeField]
		private EItems type;
		[Tooltip("Имя")] [SerializeField] 
		private string itemName;
		
		/// <summary>Взять предмет и добавить в инвентарь.</summary>
		public void Take(InputAction.CallbackContext obj)
		{
			InputManager.PlayerActions.Take.started -= Take;
			
			InventoryController.Add(type);
			Destroy(gameObject);
		}
		
		public string GetName() => itemName;
		public EItems Type => type;
	}
}