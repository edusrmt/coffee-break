using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, Interactable {
    public float coffeeAmount = 0;
    public float milkAmount = 0;  
    GameObject liquid;  

    public enum Status {
        Free,
        Handled,
        OnMachine,
        Filling
    }

    public Status currentState = Status.Free;
    bool stateChanged = false;
    public Color coffeeColor = new Color(43 / 255f, 34 / 255f, 34 / 255f);
    public Color milkColor = new Color(255 / 255f, 247 / 255f, 217 / 255f);

    void Start () {
        liquid = transform.Find("Liquid").gameObject;
    }

    void LateUpdate () {
        switch (currentState) {
            case Status.Handled:
            if (stateChanged) break;

            if(Input.GetButtonDown("Fire1")) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

                if (Physics.Raycast(ray, out hit, 2)) {
                    if (hit.transform.name != "Computer")
                        Replace(hit.point);
                }
            }

            break;
        }

        if (stateChanged)
            stateChanged = false;
    }

    public void Replace (Vector3 pos) {
        transform.parent = null;
        transform.position = new Vector3(pos.x, pos.y + 0.05f, pos.z);
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        GameObject.Find("Player").GetComponent<PlayerBehaviour>().holdingObject = false;
        currentState = Status.Free;
    }

    public float Fill (string drink, float amount) {
        if (drink == "Coffee")
            coffeeAmount += amount;
        else if (drink == "Milk")
            milkAmount += amount;

        float drinkAmount = coffeeAmount + milkAmount;

        Color liquidColor;

        if (coffeeAmount == 0)
            liquidColor = Color.white;
        else
            liquidColor = Color.Lerp(coffeeColor, milkColor, milkAmount / coffeeAmount / 4f);

        Debug.Log("Ratio: " + milkAmount / coffeeAmount / 2);
        liquid.GetComponent<Renderer>().material.SetColor("_Color", liquidColor);        

        if (drinkAmount < 1) {
            liquid.transform.localPosition = new Vector3(0, 0, 0.075f * drinkAmount);
            liquid.transform.localScale = new Vector3(1 + (0.25f * drinkAmount), 1 + (0.25f * drinkAmount), 1);
        } else if (drinkAmount > 1) {
            drinkAmount = 1;
        }

        return drinkAmount;
    }

    public void OnInteract () {
        switch (currentState) {
            case Status.Free:
                if (GameObject.Find("Player").GetComponent<PlayerBehaviour>().PickUpObject(gameObject)) {
                    currentState = Status.Handled;
                    stateChanged = true;
                }
            break;

            case Status.OnMachine:
            GameObject.Find("Player").GetComponent<PlayerBehaviour>().PickUpObject(gameObject);
            GameObject.Find("Coffee Machine").GetComponent<CoffeeMachine>().currentState = CoffeeMachine.Status.Empty;
            currentState = Status.Handled;
            stateChanged = true;
            break;
        }        
    }
}
