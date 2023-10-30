﻿using System;
using BaseClasses;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unit
{
    [Serializable]
    public class Health
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int health = 100;

        private UIManager _uiManager;
        public bool IsDead { get; private set; }
        public int MaxHealth => maxHealth;

        public int CurrentHealth
        {
            get => health;
            set => health = value;
        }

        public void Init(UIManager uiManager)
        {
            _uiManager = uiManager;
            health = maxHealth;
        }

        public void GetDamage(int damage)
        {
            health -= damage;
            _uiManager.HealthSlider.SetSliderValue(health, maxHealth);
            if (health <= 0)
            {
                IsDead = true;
                _uiManager.OpenTab(UIManager.EuiTabs.DeadTab);
            }
            
        }

    }
}