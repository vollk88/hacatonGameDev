using UnityEngine;

namespace Items
{
	public class Distractor : AItem, IUsable
	{
		[Tooltip("Тип")][SerializeField]
		private EIngredients type;
		public EIngredients Type => type;
		
		public void Use()
		{
			//TODO: Implement
			throw new System.NotImplementedException();
		}
	}
}