using BaseClasses;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIManager : CustomBehaviour
    {
        [SerializeField] private SliderController healthSlider;
        [SerializeField] private SliderController staminaSlider;
        [SerializeField] private TextMeshProUGUI itemNameText;

        public SliderController HealthSlider => healthSlider;
        public SliderController StaminaSlider => staminaSlider;

        public void ShowInteractionText(string itemName)
        {
            itemNameText.text = $"{itemName} [F]";
            itemNameText.gameObject.SetActive(true);
        }
        
        public void HideInteractionText()
        {
            itemNameText.gameObject.SetActive(false);
        }
    }
}