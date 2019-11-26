using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour, Interactable
{   
    public enum Status {
        Empty,
        Idle,
        Filling
    }

    public Status currentState = Status.Empty;

    Cup cup;

    [SerializeField]
    Transform cupPlacement;
    [SerializeField]
    float timeToFill = 30;

    public string myDrink = "Coffee";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) {
            case Status.Filling:
            if (cup.Fill(myDrink, 1 / timeToFill * Time.deltaTime) >= 1)
                currentState = Status.Idle;
            break;
        }
    }

    public void OnInteract () {
        switch (currentState) {
            case Status.Empty:
            GameObject obj = GameObject.Find("Player").GetComponent<PlayerBehaviour>().GetHeldObject();

            if (obj == null)
                return;

            obj.transform.parent = cupPlacement;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            cup = obj.GetComponent<Cup>();
            cup.currentState = Cup.Status.OnMachine;
            currentState = Status.Idle;
            break;

            case Status.Idle:
            currentState = Status.Filling;
            cup.currentState = Cup.Status.Filling;
            break;

            case Status.Filling:
            currentState = Status.Idle;
            cup.currentState = Cup.Status.OnMachine;
            break;
        }        
    }
}
