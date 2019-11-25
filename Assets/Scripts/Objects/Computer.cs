using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour, Interactable {
    public enum Status {
        Idle,
        Selected
    }

    public Status currentState = Status.Idle;

    MouseLook mouse;

    void Start()
    {
        mouse = GameObject.Find("Main Camera").GetComponent<MouseLook>();
    }

    void Update() {
        if (currentState == Status.Selected)
        {
            if (mouse.active) currentState = Status.Idle;
        }
    }

    public void OnInteract () {
        if (currentState == Status.Idle) {
            mouse.ChangeLockState();
            currentState = Status.Selected;
        }
    }
}
