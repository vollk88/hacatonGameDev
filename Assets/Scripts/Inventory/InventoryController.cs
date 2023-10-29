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
		private static Dictionary<Item, uint> _items = new();
		public static Action InventoryChanged;
		public static Item CurrentItem;
		private static readonly SOItemPrefabs ItemPrefabs;
		private static SOItemIcons _itemIcons;

		static InventoryController()
		{
			ItemPrefabs = Resources.Load<SOItemPrefabs>("SOItemPrefabs");
			_itemIcons = Resources.Load<SOItemIcons>("SOItemIcons");
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

			if (IsContains(ref item))
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
			if (!IsContains(ref item)) return;
			
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

		private static bool IsContains(ref Item item)
		{
			foreach (Item variable in _items.Keys)
			{
				if (variable.Type == item.Type)
				{
					item = variable;
					return true;
				}
			}

			return false;
		}

		public static Sprite GetIconByType(EItems eItems)
		{
			return _itemIcons.ItemSpritesList.First(type => type.ItemType == eItems).Sprite;
		}
		
		public static uint GetItemCountByType(EItems eItems)
		{
			foreach (Item variable in _items.Keys)
			{
				if (variable.Type == eItems)
					return _items[variable];
			}

			return 0;
		}

		public static Item GetItemByType(EItems eItems)
		{
			foreach (Item variable in _items.Keys)
			{
				if (variable.Type == eItems)
					return variable;
			}

			return null;
		}

		public static void SetItems(Dictionary<Item, uint> newItems) => _items = newItems;
		public static Dictionary<Item, uint> GetItems() => _items;
		
		public static GameObject GetItemFromType(EItems type) => ItemPrefabs.Get(type);
		
		public static void Debug()
		{ 
			foreach (var variable in _items)
			{
				UnityEngine.Debug.Log("Type - " + 
					variable.Key.Type + " Key - " +
				           variable.Key + " Value - " +
				           variable.Value);
			}
		}
	}
}