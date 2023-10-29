using BaseClasses;
using UnityEngine.UI;

namespace UI
{
    public class SliderController : CustomBehaviour
    {
        [GetOnObject]
        private Slider _slider;

        protected override void Awake()
        {
            base.Awake();
            _slider.value = 1f;
        }

        public void SetSliderValue(float health, float maxHealth)
        {
            _slider.value = health / maxHealth;
        }
    }
}