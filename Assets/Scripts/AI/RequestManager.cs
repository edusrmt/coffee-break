using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestManager : MonoBehaviour {
    public Text requestText;
    public List<Request> requests = new List<Request>();
    int requestIndex = 0;
    int requestCount = 0;

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
        Request req = new Request(requestCount, dose);
        requestCount++;
        requestIndex = requests.Count;
        requests.Add(req);
        RenderRequest(requestIndex);
    }

    public void NextRequest () {
        requestIndex++;

        if (requestIndex >= requests.Count)
            requestIndex = 0;   

        RenderRequest(requestIndex);
    }

    public void DeliverRequest ()
    {
        requests.RemoveAt(requestIndex);
        RenderRequest(requestIndex);
    }

    void RenderRequest (int index) {
        if (requests.Count > 0 && index >= 0 && index < requests.Count)
        {
            Request req = requests[index];
            requestText.text = "ID: " + req.id + "\nCOFFEE AMOUNT: " + req.coffeeAmount + "/3";
        } else if (requests.Count == 0)
        {
            requestText.text = "NO REQUEST";
        } else if (index >= requests.Count)
        {
            requestIndex = requests.Count - 1;
            RenderRequest(requestIndex);
        } else
        {
            requestText.text = "ERROR";
        }        
    }
}
