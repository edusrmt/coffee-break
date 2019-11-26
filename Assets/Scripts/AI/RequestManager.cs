using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestManager : MonoBehaviour {
    public Text requestText;
    public Text newText;
    public Button deliverButton;
    public Button createButton;
    public List<Request> requests = new List<Request>();
    public Queue<Request> preOrder = new Queue<Request>();
    int requestIndex = 0;
    int requestCount = 0;

    ClientManager clients;

    void Start () {
        clients = GetComponent<ClientManager>();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            NewRequest();
        }        
    }

    public void NewRequest() {
        int coffee = (int) Random.Range(1, 4);
        int milk = (int) Random.Range(0, 3 - coffee);

        // CREATE CLIENT
        Client cli = clients.SpawnClient();
        Request req = new Request(requestCount, cli, coffee, milk);
        requestCount++;
        cli.myRequest = req;
        newText.gameObject.SetActive(true);
        createButton.interactable = true;
        preOrder.Enqueue(req);   
    }

    public void AcceptRequest () {
        Request req = preOrder.Dequeue();
        requestIndex = requests.Count;
        requests.Add(req);
        clients.OrderReceived();
        newText.gameObject.SetActive(preOrder.Count > 0); 
        createButton.interactable = preOrder.Count > 0;
        deliverButton.interactable = true;
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
        Request req = requests[requestIndex];
        clients.GoToFront(req);
        requests.RemoveAt(requestIndex);
        deliverButton.interactable = requests.Count > 0;
        RenderRequest(requestIndex);
    }

    void RenderRequest (int index) {
        if (requests.Count > 0 && index >= 0 && index < requests.Count)
        {
            Request req = requests[index];
            requestText.text = "ID: " + req.id + "\nCOFFEE AMOUNT: " + req.coffeeAmount + "/3\nMILK AMOUNT: " + req.milkAmount + "/3";
        } else if (requests.Count == 0)
        {
            requestText.text = "NO ORDER";
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
