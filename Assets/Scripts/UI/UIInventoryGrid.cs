using System.Collections.Generic;
using BaseClasses;
using Items;
using TMPro;
using UnityEngine;
using Inventory;

namespace UI
{
	public class UIInventoryGrid : CustomBehaviour
	{
		[Tooltip("Элементы сетки.")][SerializeField]
		private List<ElementGrid> elements = new();
		[Tooltip("Объект с текстом названия текущего предмета.")][SerializeField]
		private TextMeshProUGUI currentItemName;
		
		private Dictionary<AItem, uint> _items = new Dictionary<AItem, uint>();
		private List<AItem> _orderedItems = new List<AItem>();
		private int _currentItemIndex = 0;

		private void OnEnable()
		{
			// Подписываемся на событие изменения инвентаря
			Inventory.InventoryController.InventoryChanged += Refill;
		}

		private void OnDisable()
		{
			// Отписываемся от события при выключении
			Inventory.InventoryController.InventoryChanged -= Refill;
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
			_orderedItems = new List<AItem>(_items.Keys);

			// На случай, если инвентарь пуст, сбрасываем текущий выбранный предмет
			if (_orderedItems.Count == 0)
			{
				_currentItemIndex = 0;
			}

			// Обновляем отображение
			UpdateGrid();
		}

		private void UpdateGrid()
		{
			int itemCount = _orderedItems.Count;

			for (int i = 0; i < elements.Count; i++)
			{
				int itemIndex = (_currentItemIndex - elements.Count / 2 + i + itemCount) % itemCount;
				AItem item = _orderedItems[itemIndex];

				// Установите предмет в элемент сетки
				elements[i].SetItem(item, _items[item]);

				// Установите текст текущего предмета, если это центральный элемент
				if (i == elements.Count / 2)
				{
					//currentItemName.text = item.Name;
				}
			}
		}

		private void Next()
		{
			if (_orderedItems.Count > 1)
			{
				_currentItemIndex = (_currentItemIndex + 1) % _orderedItems.Count;
				UpdateGrid();
			}
		}

		private void Prev()
		{
			if (_orderedItems.Count > 1)
			{
				_currentItemIndex = (_currentItemIndex - 1 + _orderedItems.Count) % _orderedItems.Count;
				UpdateGrid();
			}
		}
	}
}