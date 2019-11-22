using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestGenerator : MonoBehaviour {

    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            NewRequest();
        }
    }

    void NewRequest() {
        // ESPRESSO
        int dose = (int) Random.Range(1, 4);
        Debug.Log(dose);
    }
}
