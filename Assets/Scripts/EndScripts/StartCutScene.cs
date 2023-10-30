using System;
using System.Collections;
using System.Collections.Generic;
using BaseClasses;
using Input;
using UI;
using UnityEngine;
using UnityEngine.Playables;

public class StartCutScene : MonoBehaviour
{
    [SerializeField] private GameObject leg;
    [SerializeField] private GameObject mainvirt;


    private void OnEnable()
    {
        GameStateEvents.GameWin += StartPlay;
    }


    private void Start()
    {
        
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space)) StartPlay();
    }

    private void StartPlay()
    {
        InputManager.DisableActions();
        mainvirt.gameObject.SetActive(false);
        CustomBehaviour.GetCharacterController().gameObject.SetActive(false);
        leg?.SetActive(true);
        CustomBehaviour.GetCharacterController().gameObject.SetActive(false);
        FindObjectOfType<UIManager>().gameObject.SetActive(false);
        PlayableDirector play = GetComponent<PlayableDirector>();
        Debug.Log("play:" +play);
        play.Play();
    }
}
