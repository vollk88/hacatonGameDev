using Items;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Cooking
{
	[Serializable]
	public struct Items
	{
		public string ItemName;
		public EItems ItemType;
		public uint ItemCount;
	}

	[Serializable]
	public struct Recipe
	{
		public string DishName;
		public Sprite FinishDishSprite;
		public List<Items> Ingredients;
		public float CookingTime;

		public float StartCookingTime;
		public float EndCookingTime;

		public bool IsCooking;
	}
	
	[CreateAssetMenu(fileName = "SORecipes", menuName = "Items/SORecipes")]
	public class SoRecipes : ScriptableObject
	{
		[SerializeField] private List<Recipe> recipes;

		public List<Recipe> Recipes => recipes;
	}
}