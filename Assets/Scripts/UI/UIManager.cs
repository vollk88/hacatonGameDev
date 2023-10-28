using BaseClasses;
using UnityEngine;

namespace UI
{
    public class UIManager : CustomBehaviour
    {
        [SerializeField] private SliderController healthSlider;
        [SerializeField] private SliderController staminaSlider;

        public SliderController HealthSlider => healthSlider;

        public SliderController StaminaSlider => staminaSlider;
    }
}