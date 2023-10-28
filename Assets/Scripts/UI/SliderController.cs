using BaseClasses;
using UnityEngine.UI;

namespace UI
{
    public class SliderController : CustomBehaviour
    {
        [GetOnObject]
        private Image _healthSlider;

        protected override void Awake()
        {
            base.Awake();
            _healthSlider.fillAmount = 1f;
        }

        public void SetSliderValue(float health, float maxHealth)
        {
            _healthSlider.fillAmount = health / maxHealth;
        }
    }
}