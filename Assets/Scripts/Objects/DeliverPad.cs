using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverPad : MonoBehaviour, Interactable
{
    ClientManager clients;

    public enum Status {
        Empty,
        Cup
    }

    public Status currentState = Status.Empty;

    // Start is called before the first frame update
    void Start()
    {
        clients = GameObject.Find("Level Manager").GetComponent<ClientManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract () {
        switch (currentState) {
            case Status.Empty:
            GameObject obj = GameObject.Find("Player").GetComponent<PlayerBehaviour>().GetHeldObject();
            clients.posOrder[0].GetComponent<Client>().ReceiveOrder(obj.GetComponent<Cup>());
            GameObject.Find("Level Manager").GetComponent<ClientManager>().posOrder.RemoveAt(0);
            clients.UpdateQueuesPositions();
            break;
        }
    }
}
