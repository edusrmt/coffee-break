using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public Request myRequest;
    public float requirementLevel = 0.1f;

    public void ReceiveOrder (Cup order) {
        float satisfaction = 100;

        float coffeeDiff = Mathf.Abs(order.coffeeAmount - myRequest.coffeeAmount / 3f);
        float milkDiff = Mathf.Abs(order.milkAmount - myRequest.milkAmount / 3f);

        if (coffeeDiff > requirementLevel) {
            satisfaction -= coffeeDiff * 100;
        }

        if (milkDiff > requirementLevel) {
            satisfaction -= milkDiff * 100;
        }

        Debug.Log("Satisfaction of " + satisfaction);

        GameObject.Find("Level Manager").GetComponent<LevelManager>().OrderDelivered(satisfaction);
        Destroy(order.gameObject);
        Destroy(gameObject);
    }
}
