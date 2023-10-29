using System.Collections;
using Cooking;
using Inventory;
using UnityEngine;
using BaseClasses;
using Items;
using UnityEngine.UI;
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

		private IEnumerator Fill(SoRecipes recipes, Recipe recipe, Image fillingImage)
		{
			WaitForFixedUpdate fixedUpdate = new();

			int index = recipes.Recipes.IndexOf(recipe);

			if (!recipe.IsCooking)
			{
				Create(recipe);
				recipe.EndCookingTime = Time.time + recipe.CookingTime;
				recipe.StartCookingTime = Time.time;
			}
			
			recipe.IsCooking = true;
			while (Time.time < recipe.EndCookingTime)
			{
				fillingImage.fillAmount = (Time.time - recipe.StartCookingTime) /
				                          (recipe.EndCookingTime - recipe.StartCookingTime);
				
				recipes.Recipes[index] = recipe;
				yield return fixedUpdate;
			}
			recipe.IsCooking = false;				
			recipes.Recipes[index] = recipe;
		}
		
		public void Fill(SoRecipes recipes)
		{
			Clear();
			for (int i = 0; i < recipes.Recipes.Count; i++)
			{
				Recipe recipe = recipes.Recipes[i];
				GameObject newDish = Instantiate(dishContentElem, dishContentTransform);

				DishElem dishElem = newDish.GetComponent<DishElem>();
				if (recipe.IsCooking)
				{
					DishCooking(recipes, recipe, dishElem.BackgroundFillingImage);
					dishElem.createButton.enabled = false;
				}
				
				if(!recipe.IsCooking)
				{
					dishElem.createButton.enabled = true;
					dishElem.createButton.onClick.AddListener( () =>
					{
						StartCoroutine(Fill(recipes, recipe, dishElem.BackgroundFillingImage));
						dishElem.createButton.onClick.RemoveAllListeners();
					});
				}

				dishElem.DishName.text = recipe.DishName;
				dishElem.DishImage.sprite = recipe.FinishDishSprite;
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

		private void DishCooking(SoRecipes soRecipes, Recipe recipe, Image dishElemBackgroundFillingImage)
		{
			StartCoroutine(Fill(soRecipes, recipe, dishElemBackgroundFillingImage));
		}
		
		private void Clear()
		{
			foreach (Transform child in dishContentTransform.transform)
				Destroy(child.gameObject);
		}
	}
}