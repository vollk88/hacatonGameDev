using UnityEngine;

namespace Items
{
	public class Ingredient : AItem
	{
		[Tooltip("Тип")][SerializeField]
		private EIngredients type;
		public EIngredients Type => type;
	}
}