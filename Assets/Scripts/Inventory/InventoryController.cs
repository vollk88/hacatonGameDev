using System.Collections.Generic;
using Items;

namespace Inventory
{
	public static class InventoryController
	{
		private static Dictionary<AItem, uint> _items = new ();

		/// <summary>Добавляет предмет в инвентарь.</summary>
		/// <param name="item">Предмет, унаследованный от AItem.</param>
		public static void Add(AItem item)
		{
			if (_items.ContainsKey(item))
			{
				_items[item]++;
			}
			else
			{
				_items.Add(item, 1);
			}
		}
		
		/// <summary>Удаляет предмет из инвентаря.</summary>
		/// <param name="item">Предмет, унаследованный от AItem.</param>
		public static void Remove(AItem item)
		{
			if (!_items.ContainsKey(item)) return;
			
			if (_items[item] > 1)
			{
				_items[item]--;
			}
			else
			{
				_items.Remove(item);
			}
		}
	}
}