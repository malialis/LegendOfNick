using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    
    private void Awake()
    {
        current = this;
    } 

    public event Action OnSubmit; //This is used for all listening objects

    public void DoSubmit() => this.OnSubmit?.Invoke();

    
}
