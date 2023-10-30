using System.Collections;
using System.Collections.Generic;
using BaseClasses;
using UI;
using UnityEngine;
using UnityEngine.Playables;

public class StartCutScene : MonoBehaviour
{
    [SerializeField] private GameObject leg;




    private void Start()
    {
        CustomBehaviour.GetCharacterController().gameObject.SetActive(false);
        leg?.SetActive(true);
    }

    private void StartPlay()
    {
        CustomBehaviour.GetCharacterController().gameObject.SetActive(false);
        FindObjectOfType<UIManager>().gameObject.SetActive(false);
        PlayableDirector play = GetComponent<PlayableDirector>();
        play.Play();
    }
}
