using System;
using BaseClasses;
using Cooking;
using Input;
using UI;
using UnityEngine;

public class CookingTable : CustomBehaviour
{
    public Action CookingTableClosed;
    private SoRecipes _recipes;
    private UIManager _uiManager;
    
    protected override void Awake()
    {
        base.Awake();
        _uiManager = FindObjectOfType<UIManager>();
        _recipes = Resources.Load<SoRecipes>("SORecipes");
    }

    public void OpenTable()
    {
        Debug.Log("open");
        InputManager.DisableActions();
        _uiManager.CookingUI.gameObject.SetActive(true);
        _uiManager.CookingUI.Fill(_recipes);
    }

    public void CloseTable()
    {
        InputManager.EnableActions();
        _uiManager.CookingUI.gameObject.SetActive(false);
        CookingTableClosed?.Invoke();
    }
}
