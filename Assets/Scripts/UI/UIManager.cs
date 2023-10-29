using System.Collections.Generic;
using BaseClasses;
using TMPro;
using UI.Cooking;
using UnityEngine;

namespace UI
{
    public class UIManager : CustomBehaviour
    {
        public enum EuiTabs
        {
            HUD,
            MainMenu,
            PauseMenu,
            DeadTab
        }
        
        [SerializeField] private SliderController healthSlider;
        [SerializeField] private SliderController staminaSlider;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private List<GameObject> uiTabs;
        [SerializeField] private CookingUI cookingUI;

        public SliderController HealthSlider => healthSlider;
        public SliderController StaminaSlider => staminaSlider;
        public CookingUI CookingUI => cookingUI;

        public void ShowInteractionText(string itemName)
        {
            itemNameText.text = $"{itemName} [F]";
            itemNameText.gameObject.SetActive(true);
        }
        
        public void HideInteractionText()
        {
            itemNameText.gameObject.SetActive(false);
        }
        
        public void OpenTab(EuiTabs tab)
        {
            foreach (var uiTab in uiTabs)
            {
                uiTab.SetActive(false);
            }
            uiTabs[(int) tab].SetActive(true);
        }
        
    }
}