using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    Animator animator;
    public string _currentState;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void ChangeAnimationState(string newState)
    {
        if (_currentState == newState) return;

        animator.Play(newState);

        _currentState = newState;
    }
}
