﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Smash()
    {
        animator.SetBool("Smash", true);
        StartCoroutine(BreakCoroutine());
    }

    private IEnumerator BreakCoroutine()
    {
        yield return new WaitForSeconds(0.55f);
        this.gameObject.SetActive(false);
        Debug.Log("I am now broken and disabled");
    }

}