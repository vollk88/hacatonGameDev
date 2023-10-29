using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace UI.Cooking
{
	[CreateAssetMenu(fileName = "SOItemSprites", menuName = "Items/SOItemSprites")]
	public class SOItemIcons : ScriptableObject
	{
		[Serializable]
		public struct ItemSprites
		{
			public Sprite Sprite;
			public EItems ItemType;
		}

		[SerializeField] private List<ItemSprites> itemSprites;

		public List<ItemSprites> ItemSpritesList => itemSprites;
	}
}