using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour {
    public GameObject clientPrefab;
    public Vector3 preFirstPos;
    public Vector3 posFirstPos;
    public Vector3 queueSpacement; 
    public Queue<GameObject> preOrder = new Queue<GameObject>();
    public List<GameObject> posOrder = new List<GameObject>();

    int preQueueCount = 0;  
    int posQueueCount = 0; 

    public Client SpawnClient() {
        GameObject cli = GameObject.Instantiate(clientPrefab, Vector3.zero, Quaternion.Euler(-90, 0, 180));
        cli.GetComponent<Renderer>().material.SetColor("_Color", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        preQueueCount++;
        preOrder.Enqueue(cli);
        UpdateQueuesPositions();
        return cli.GetComponent<Client>();
    }

    public void OrderReceived () {
        preQueueCount--;
        posQueueCount++;
        posOrder.Add(preOrder.Dequeue());
        UpdateQueuesPositions();
    }

    public void GoToFront(Request req) {
        int ownerIndex = posOrder.IndexOf(req.owner.gameObject);
        GameObject owner = posOrder[ownerIndex];
        posOrder.RemoveAt(ownerIndex);
        posOrder.Insert(0, owner);
        UpdateQueuesPositions();
    }

    public void UpdateQueuesPositions () {
        if (preOrder.Count > 0) {
            for (int i = 0; i < preOrder.Count; i++) {
                Vector3 pos = preFirstPos + (i * queueSpacement);
                GameObject cur = preOrder.Dequeue();
                cur.transform.position = pos;
                preOrder.Enqueue(cur);
            }
        }

        if (posOrder.Count > 0) {
            for (int i = 0; i < posOrder.Count; i++) {
                if (posOrder[i] == null)
                    return;
                Vector3 pos = posFirstPos + (i * queueSpacement);
                GameObject cur = posOrder[0];
                posOrder.RemoveAt(0);
                cur.transform.position = pos;
                posOrder.Add(cur);
            }
        }
    }
}
