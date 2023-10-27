using Items;
using UnityEngine;

namespace Inventory
{
	public class Vegetable : AItem
	{
		[Tooltip("Тип")][SerializeField]
		private EVegetables type;
		public EVegetables Type => type;
		
		
	}
}