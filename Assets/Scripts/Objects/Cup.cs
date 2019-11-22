using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, Interactable {
    public float drinkAmount = 0;

    public enum Status {
        Free,
        Handled,
        OnMachine,
        Filling
    }

    public Status currentState = Status.Free;
    bool stateChanged = false;

    void LateUpdate () {
        switch (currentState) {
            case Status.Handled:
            if (stateChanged) break;

            if(Input.GetButtonDown("Fire1")) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

                if (Physics.Raycast(ray, out hit, 2)) {
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
