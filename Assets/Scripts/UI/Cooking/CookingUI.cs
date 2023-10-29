using Cooking;
using Inventory;
using UnityEngine;
using BaseClasses;
using Items;
using Button = UnityEngine.UI.Button;

namespace UI.Cooking
{
	public class CookingUI : CustomBehaviour
	{
		[SerializeField] private Transform dishContentTransform;
		[SerializeField] private GameObject dishContentElem;
		
		[SerializeField] private GameObject ingredientContentElem;
		[SerializeField] private Button closeButton;

		protected override void Awake()
		{
			base.Awake();
			closeButton.onClick.AddListener(FindObjectOfType<CookingTable>().CloseTable);
		}

		private void Create(Recipe recipe)
		{
			InventoryController.Debug();
			foreach (global::Cooking.Items items in recipe.Ingredients)
			{
				Item item = InventoryController.GetItemByType(items.ItemType);
				for(int i = 0; i < items.ItemCount; i++)
					InventoryController.Remove(item);
			}
			InventoryController.Debug();
		}

		public void Fill(SoRecipes recipes)
		{
			Clear();
			foreach (Recipe recipe in recipes.Recipes)
			{
				GameObject newDish = Instantiate(dishContentElem, dishContentTransform);

				DishElem dishElem = newDish.GetComponent<DishElem>();
				dishElem.createButton.enabled = true;
				dishElem.createButton.onClick.AddListener(() => Create(recipe));

				dishElem.DishName.text = recipe.DishName;
				dishElem.DishImage.sprite = recipe.FinishDishSprite;
				Debug.Log(recipe.Ingredients.Count);
				foreach (global::Cooking.Items ingredient in recipe.Ingredients)
				{
					GameObject newIngredient = Instantiate(ingredientContentElem, dishElem.IngredientContentTransform);
					
					IngredientElem ingredientElem = newIngredient.GetComponent<IngredientElem>();

					ingredientElem.IngredientImage.sprite =
						InventoryController.GetIconByType(ingredient.ItemType);
					
					ingredientElem.IngredientName.text = ingredient.ItemName;
					
					uint currentCount = InventoryController.GetItemCountByType(ingredient.ItemType);

					if ((int)currentCount - ingredient.ItemCount < 0)
						dishElem.createButton.enabled = false;
					
					ingredientElem.SetIngredientCount(currentCount, ingredient.ItemCount);
				}
			}
		}
		
		private void Clear()
		{
			foreach (Transform child in dishContentTransform.transform)
				Destroy(child.gameObject);
		}
	}
}