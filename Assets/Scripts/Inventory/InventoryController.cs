using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Inventory
{
	public static class InventoryController
	{
		private static Dictionary<Item, uint> _items = new();
		public static Action InventoryChanged;
		public static Item CurrentItem = null;
		private static readonly SOItemPrefabs ItemPrefabs;

		static InventoryController()
		{
			ItemPrefabs = Resources.Load<SOItemPrefabs>("SOItemPrefabs");
		}

		public static GameObject GetCurrentItemPrefab()
		{
			return CurrentItem is null ? null : ItemPrefabs.Get(CurrentItem.Type);
		}
		/// <summary>Добавляет предмет в инвентарь.</summary>
		/// <param name="item">Предмет, унаследованный от AItem.</param>
		public static void Add(Item item)
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
		public static void Remove(Item item)
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
					CurrentItem = _items.Count > 1 ? _items.Keys.GetEnumerator().Current : null;
				}
				_items.Remove(item);
			}
			InventoryChanged?.Invoke();
		}
		public static Dictionary<Item, uint> GetItems() => _items;
	}
}