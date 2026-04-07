using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{

    [SerializeField] float timeToCompleate = 120f;
    [SerializeField] TextMeshProUGUI timerText;
    
    CollisionHandler collisionHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collisionHandler = FindFirstObjectByType<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToCompleate -= Time.deltaTime;
        timerText.text = (timeToCompleate).ToString("0");
        if (timeToCompleate < 0)
        {
            collisionHandler.ReloadLevel();
        }
    }
}
