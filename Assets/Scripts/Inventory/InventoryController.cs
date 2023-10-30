using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using UI.Cooking;
using UnityEngine;

namespace Inventory
{
	public static class InventoryController
	{
		private static Dictionary<EItems, uint> _items = new();
		public static Action InventoryChanged;
		public static EItems CurrentItem;
		private static readonly SOItemPrefabs ItemPrefabs;
		private static SOItemIcons _itemIcons;

		static InventoryController()
		{
			ItemPrefabs = Resources.Load<SOItemPrefabs>("SOItemPrefabs");
			_itemIcons = Resources.Load<SOItemIcons>("SOItemIcons");
		}

		public static GameObject GetCurrentItemPrefab()
		{
			return CurrentItem is EItems.Null ? null : ItemPrefabs.Get(CurrentItem);
		}
		/// <summary>Добавляет предмет в инвентарь.</summary>
		/// <param name="item">Предмет, унаследованный от AItem.</param>
		public static void Add(EItems item)
		{
			if (_items.Count == 0)
			{
				CurrentItem = item;
			}

			if (_items.ContainsKey(item))
			{
				_items[item]++;
			}
			else
			{
				_items.Add(item, 1);
			}
			
			InventoryChanged?.Invoke();
		}
		
		/// <summary>Удаляет предмет из инвентаря.</summary>
		/// <param name="item">Предмет, унаследованный от AItem.</param>
		public static void Remove(EItems item)
		{
			if (!_items.ContainsKey(item)) return;
			
			if (_items[item] > 1)
			{
				_items[item]--;
			}
			else
			{
				if (CurrentItem == item)
				{
					CurrentItem = _items.Count > 1 ? _items.Keys.GetEnumerator().Current : EItems.Null;
				}
				_items.Remove(item);
			}
			InventoryChanged?.Invoke();
		}

		public static Sprite GetIconByType(EItems eItems)
		{
			return _itemIcons.ItemSpritesList.First(type => type.ItemType == eItems).Sprite;
		}
		
		public static uint GetItemCountByType(EItems eItems)
		{
			foreach (EItems variable in _items.Keys)
			{
				if (variable == eItems)
					return _items[variable];
			}

			return 0;
		}

		public static void SetItems(Dictionary<EItems, uint> newItems) => _items = newItems;
		public static Dictionary<EItems, uint> GetItems() => _items;
		
		public static GameObject GetItemFromType(EItems type) => ItemPrefabs.Get(type);
		
		public static void Debug()
		{ 
			foreach (var variable in _items)
			{
				UnityEngine.Debug.Log("Type - " + 
					variable.Key + " Key - " +
				           variable.Key + " Value - " +
				           variable.Value);
			}
		}
	}
}