using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public GameObject clientPrefab;
    public Vector3 preFirstPos;
    public Vector3 posFirstPos;
    public Vector3 queueSpacement; 
    public Queue<GameObject> preOrder = new Queue<GameObject>();
    public Queue<GameObject> posOrder = new Queue<GameObject>();

    int preQueueCount = 0;  
    int posQueueCount = 0; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Client SpawnClient() {
        GameObject cli = GameObject.Instantiate(clientPrefab, Vector3.zero, Quaternion.identity);
        cli.GetComponent<Renderer>().material.SetColor("_Color", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        preQueueCount++;
        preOrder.Enqueue(cli);
        UpdateQueuesPositions();
        return cli.GetComponent<Client>();
    }

    public void OrderReceived () {
        preQueueCount--;
        posQueueCount++;
        posOrder.Enqueue(preOrder.Dequeue());
        UpdateQueuesPositions();
    }

    void UpdateQueuesPositions () {
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
                Vector3 pos = posFirstPos + (i * queueSpacement);
                GameObject cur = posOrder.Dequeue();
                cur.transform.position = pos;
                posOrder.Enqueue(cur);
            }
        }
    }
}
