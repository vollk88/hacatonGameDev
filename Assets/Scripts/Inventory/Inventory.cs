using System.Collections.Generic;
using BaseClasses;
using Items;

namespace Inventory
{
	public class Inventory : CustomBehaviour
	{
		private Dictionary<AItem, uint> _items = new ();

		public void Add(AItem item)
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
		
		public void Remove(AItem item)
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