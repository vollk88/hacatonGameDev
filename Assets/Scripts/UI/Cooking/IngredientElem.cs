using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Cooking
{
    public class IngredientElem : MonoBehaviour
    {
        public Image IngredientImage;
        public TextMeshProUGUI IngredientName;
        
        [SerializeField] private TextMeshProUGUI ingredientCount;

        public void SetIngredientCount(uint weHaveCount, uint neededCount)
        {
            ingredientCount.text = $"{weHaveCount}/{neededCount}";
        }
    }
}
