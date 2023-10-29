using System;
using BaseClasses;
using Cinemachine;
using Input;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cooking
{
    public class CookingTable : CustomBehaviour
    {
        public Action CookingTableClosed;
        private SoRecipes _recipes;
        private UIManager _uiManager;
        private CinemachineVirtualCamera _virtualCamera;

        protected override void Awake()
        {
            base.Awake();
            _uiManager = FindObjectOfType<UIManager>();
            _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            _recipes = Resources.Load<SoRecipes>("SORecipes");
        }

        public void OpenTable()
        {
            SwitchInput(false);
        
            InputManager.DisableActions();
            InputManager.UIActions.Enable();
            _uiManager.CookingUI.gameObject.SetActive(true);
            _uiManager.CookingUI.Fill(_recipes);
            InputManager.UIActions.Cancel.started += CloseTable;
        }

        private void CloseTable(InputAction.CallbackContext _)
        {
            CloseTable();
        }

        public void CloseTable()
        {
            InputManager.EnableActions();
            _uiManager.CookingUI.gameObject.SetActive(false);
            CookingTableClosed?.Invoke();
            SwitchInput(true);
            InputManager.UIActions.Cancel.started -= CloseTable;
        }

        private void SwitchInput(bool closeTable)
        {
            if (closeTable)
            {
                _virtualCamera.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                _virtualCamera.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
