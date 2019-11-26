using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    GameManager manager;
    RequestManager request;

    public Text timeText;

    int clientsAmount = 0;
    float dayDuration = 5;
    float dayTime;
    float waitTime = 0;

    float timer = 0;

    float ordersDelivered = 0;
    float satisfactionSum = 0;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        request = GetComponent<RequestManager>();
        clientsAmount = (int) (manager.daysCount * manager.clientBalance * manager.popularity / 100);
        waitTime = dayDuration * 60 / clientsAmount;
        dayTime = dayDuration * 60;
        request.NewRequest();
    }

    // Update is called once per frame
    void Update() {
        if (timer > waitTime || (request.requests.Count == 0 && request.preOrder.Count == 0)) {
            request.NewRequest();
            timer = 0;
            clientsAmount--;

            if (clientsAmount < 0) {
                manager.clientBalance += manager.extraFactor;
            }
        }

        timer += Time.deltaTime;

        dayTime -= Time.deltaTime;
        int min = (int) (dayTime / 60);
        int s = (int) (dayTime - (min * 60));

        if (s > 9)
            timeText.text = min + ":" + s;
        else
            timeText.text = min + ":0" + s;
    }

    public void OrderDelivered (float satisfaction) {
        ordersDelivered++;
        satisfactionSum += satisfaction;
    } 
}
