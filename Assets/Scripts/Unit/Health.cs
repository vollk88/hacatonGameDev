using System;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unit
{
    [Serializable]
    public class Health
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float health;

        private UIManager _uiManager;
        public bool IsDeath { get; private set; }
        public float MaxHealth => maxHealth;
        public float CurrentHealth => health;

        public void Init(UIManager uiManager)
        {
            _uiManager = uiManager;
            health = maxHealth;
        }

        public void GetDamage(float damage)
        {
            health -= damage;
            _uiManager.HealthSlider.SetSliderValue(health, maxHealth);
            if (health <= 0)
                IsDeath = true;
            
        }

    }
}