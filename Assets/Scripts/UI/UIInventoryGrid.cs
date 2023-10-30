using System.Collections.Generic;
using BaseClasses;
using Input;
using Items;
using TMPro;
using UnityEngine;
using Inventory;
using UnityEngine.InputSystem;

namespace UI
{
	public class UIInventoryGrid : CustomBehaviour
	{
		[Tooltip("Элементы сетки.")][SerializeField]
		private List<ElementGrid> elements = new();
		[Tooltip("Объект с текстом названия текущего предмета.")][SerializeField]
		private TextMeshProUGUI currentItemName;
		
		private Dictionary<Item, uint> _items = new();
		private List<Item> _orderedItems = new();
		private int _currentItemIndex;
		

		protected override void OnEnable()
		{
			// Подписываемся на событие изменения инвентаря
			InventoryController.InventoryChanged += Refill;
			InputManager.PlayerActions.NextItem.started += Next;
			InputManager.PlayerActions.PrevItem.started += Prev;
		}

		protected override void OnDisable()
		{
			// Отписываемся от события при выключении
			InventoryController.InventoryChanged -= Refill;
			InputManager.PlayerActions.NextItem.started -= Next;
			InputManager.PlayerActions.PrevItem.started -= Prev;
		}

		private void Start()
		{
			// Начальное заполнение сетки
			Refill();
		}

		private void Refill()
		{
			// Обновляем словарь предметов
			_items = InventoryController.GetItems();
			_orderedItems = new List<Item>(_items.Keys);

			// На случай, если инвентарь пуст, сбрасываем текущий выбранный предмет
			if (_orderedItems.Count == 0)
			{
				foreach (var elem in elements)
					elem.Hide();
				_currentItemIndex = 0;
				currentItemName.text = "";
			}

			// Обновляем отображение
			UpdateGrid();
		}

		private void UpdateGrid()
		{
			if (_items.Count == 0) return;
			
			int itemCount = _orderedItems.Count;

			for (int i = 0; i < elements.Count; i++)
			{
				int itemIndex = (_currentItemIndex - elements.Count / 2 + i + itemCount) % itemCount;
				Item item = _orderedItems[itemIndex];

				// Установите предмет в элемент сетки
				elements[i].SetItem(item, _items[item]);

				// Установите текст текущего предмета, если это центральный элемент
				if (i != elements.Count / 2) continue;
				
				currentItemName.text = item.GetName();
				InventoryController.CurrentItem = item;
			}
		}

		private void Next(InputAction.CallbackContext obj)
		{
			if (_orderedItems.Count <= 1) return;
				
			_currentItemIndex = (_currentItemIndex + 1) % _orderedItems.Count;
				
			UpdateGrid();
		}

		private void Prev(InputAction.CallbackContext obj)
		{
			if (_orderedItems.Count <= 1) return;
				
			_currentItemIndex = (_currentItemIndex - 1 + _orderedItems.Count) % _orderedItems.Count;
				
			UpdateGrid();
		}
	}
}