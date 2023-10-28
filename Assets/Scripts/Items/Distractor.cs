using UnityEngine;

namespace Items
{
	public class Distractor : AItem, IUsable
	{
		[Tooltip("Тип")][SerializeField]
		private EDistractors type;
		public EDistractors Type => type;
		
		public void Use()
		{
			//TODO: Implement
			throw new System.NotImplementedException();
		}
	}
}