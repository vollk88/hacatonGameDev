using System;
using System.Collections.Generic;
using BaseClasses;
using Input;
using TMPro;
using UI.Cooking;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class UIManager : CustomBehaviour
    {
        public enum EuiTabs
        {
            HUD,
            MainMenu,
            PauseMenu,
            DeadTab, 
            CookingTab,
            InstructionsTab
        }
        
        [SerializeField] private SliderController healthSlider;
        [SerializeField] private SliderController staminaSlider;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private List<GameObject> uiTabs;
        [SerializeField] private CookingUI cookingUI;
        [SerializeField] private UISeeTasks seeTasks;

        public SliderController HealthSlider => healthSlider;
        public SliderController StaminaSlider => staminaSlider;
        public CookingUI CookingUI => cookingUI;
        
        private bool _isPaused;
        public bool IsPaused => _isPaused;
        public UISeeTasks SeeTasks => seeTasks;

        private void Start()
        {
            GameStateEvents.GameStarted += OnGameStarted;
            GameStateEvents.GamePaused += OnGamePaused;
            GameStateEvents.GameResumed += OnGameResumed;
            GameStateEvents.GameEnded += OnGameEnded;
        }

        public void ShowInteractionText(string itemName)
        {
            itemNameText.text = $"{itemName} [F]";
            itemNameText.gameObject.SetActive(true);
        }
        
        public void HideInteractionText()
        {
            itemNameText.gameObject.SetActive(false);
        }

        private void OnGameEnded()
        {
            InputManager.UIActions.Pause.started -= Pause;
        }

        private void OnGameResumed()
        {
            //throw new NotImplementedException();
        }

        private void OnGamePaused()
        {
            //throw new NotImplementedException();
        }

        private void OnGameStarted()
        {
            InputManager.UIActions.Pause.started += Pause;
            InputManager.UIActions.SeeTasks.started += SeeTasksOn;
            InputManager.UIActions.SeeTasks.canceled += SeeTasksOff;
            OpenTab(EuiTabs.HUD);
        }

        private void Pause(InputAction.CallbackContext callbackContext)
        {
            _isPaused = !_isPaused;
            OpenTab(_isPaused ? EuiTabs.PauseMenu : EuiTabs.HUD);
        }

        private void SeeTasksOn(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                seeTasks.gameObject.SetActive(true);
            }
        }

        private void SeeTasksOff(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.canceled)
            {
                seeTasks.gameObject.SetActive(false);
            }
        }
        
        public void OpenTab(EuiTabs tab)
        {
            foreach (var uiTab in uiTabs)
            {
                uiTab.SetActive(false);
            }
            uiTabs[(int) tab].SetActive(true);
            if (tab != EuiTabs.HUD)
            {
                if (_isPaused)
                {
                    if (tab == EuiTabs.InstructionsTab)
                    {
                        InputManager.UIActions.Pause.started -= Pause;
                    }
                    else if (tab == EuiTabs.PauseMenu)
                    {
                        InputManager.UIActions.Pause.started += Pause;
                    }
                } //простите за этот костыль, я закончился
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        
    }
}