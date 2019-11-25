using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestManager : MonoBehaviour {
    public Text requestText;
    public List<Request> requests = new List<Request>();

    void Start () {

    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            NewRequest();
        }
    }

    void NewRequest() {
        // ESPRESSO
        int dose = (int) Random.Range(1, 4);
        Request req = new Request(requests.Count, dose);
        requests.Add(req);
        RenderRequest(req);
    }

    public void NextRequest () {
        Debug.Log("Thank u, nxt!");
    }

    void RenderRequest (Request req) {
        requestText.text = "COFFEE AMOUNT: " + req.coffeeAmount + "/3";
    }
}
