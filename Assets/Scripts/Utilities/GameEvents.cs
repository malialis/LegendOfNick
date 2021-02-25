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

    public event Action OnSubmit; //This is used for all listening objects of Chests

    public void DoSubmit() => this.OnSubmit?.Invoke();

    public event Action OnRead; //This is used for all listening objects of Signs

    public void DoRead() => this.OnRead?.Invoke();
    
    public event Action OnTalk; //This is used for all listening objects of Peoples

    public void DoTalk() => this.OnTalk?.Invoke();



}
