using Cooking;
using UnityEngine;
using BaseClasses;
using Inventory;

namespace UI.Cooking
{
	public class CookingUI : CustomBehaviour
	{
		[SerializeField] private Transform dishContentTransform;
		[SerializeField] private GameObject dishContentElem;
		
		[SerializeField] private Transform ingredientContentTransform;
		[SerializeField] private GameObject ingredientContentElem;
		
		public void Fill(SoRecipes recipes)
		{
			Clear();
			foreach (Recipe recipe in recipes.Recipes)
			{
				GameObject newDish = Instantiate(dishContentElem, dishContentTransform);

				DishElem dishElem = newDish.GetComponent<DishElem>();

				dishElem.DishName.text = recipe.DishName;
				dishElem.DishImage.sprite = recipe.FinishDishSprite;
				
				foreach (global::Cooking.Items ingredient in recipe.Ingredients)
				{
					GameObject newIngredient = Instantiate(ingredientContentElem, ingredientContentTransform);
					
					IngredientElem ingredientElem = newIngredient.GetComponent<IngredientElem>();

					ingredientElem.IngredientImage.sprite =
						InventoryController.GetIconByType(ingredient.ItemType);
					
					ingredientElem.IngredientName.text = ingredient.ItemName;
					ingredientElem.SetIngredientCount(ingredient.ItemCount,
						InventoryController.GetItemCountByType(ingredient.ItemType));

				}
			}
		}
		
		private void Clear()
		{
			foreach (Transform child in dishContentTransform.transform)
				Destroy(child.gameObject);
			
			foreach (Transform child in ingredientContentTransform.transform)
				Destroy(child.gameObject);
			
		}
	}
}