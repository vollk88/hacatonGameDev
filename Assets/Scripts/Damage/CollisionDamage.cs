using System;
using System.Collections;
using System.Collections.Generic;
using BaseClasses;
using UnityEngine;
using UnityEngine.Serialization;
using CharacterController = Unit.Character.CharacterController;


public class CollisionDamage : CustomBehaviour
{
    [GetOnObject]
    private Collider _collider;
    
    private static CharacterController _player;
    
    public int Damage { get; set; } = 10;
    // Start is called before the first frame update
    void Start()
    {
        _collider.isTrigger = true;

        if (!Instances.ContainsKey(typeof(CharacterController)))
        {
            Debug.LogError("CharacterController not found");
            _player = null;
            return;
        }
        
        _player ??= (CharacterController) Instances[typeof(CharacterController)][0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _player.gameObject)
            return;
        
        _player.GetDamage(Damage);
    }
}
