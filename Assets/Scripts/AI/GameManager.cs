using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float popularity = 100;
    public int daysCount = 1;
    public float clientBalance = 3;
    public float extraFactor = 0.1f;

    void Awake () {
        DontDestroyOnLoad(gameObject);
        // SceneManager.LoadScene("Gameplay");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
