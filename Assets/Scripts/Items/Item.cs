using BaseClasses;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Inventory;

namespace Items
{
	public class Item : CustomBehaviour, IItem
	{
		[Tooltip("Тип")][SerializeField]
		private EItems type;
		[Tooltip("Имя")] [SerializeField] 
		private string itemName;
		[Tooltip("UI иконка для предмета.")][SerializeField]
		private Sprite icon;
		
		/// <summary>Взять предмет и добавить в инвентарь.</summary>
		public void Take(InputAction.CallbackContext obj)
		{
			InputManager.PlayerActions.Take.started -= Take;
			
			InventoryController.Add(this);
			Destroy(gameObject);
		}
		
		public Sprite GetIcon() => icon;
		public string GetName() => itemName;
		public EItems Type => type;
	}
}