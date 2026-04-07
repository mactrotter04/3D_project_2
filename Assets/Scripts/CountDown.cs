using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{

    [SerializeField] float timeToCompleate = 120f;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] AudioClip fail;

    CollisionHandler collisionHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collisionHandler = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToCompleate -= Time.deltaTime;
        timeToCompleate = Mathf.Clamp(timeToCompleate, 0, float.MaxValue);

        timerText.text = (timeToCompleate).ToString("0");

        if (timeToCompleate <= 5)
        {
            timerText.color = Color.red;
        }

        if (timeToCompleate <= 0)
        {
            Invoke(nameof(TimerFailSequence), 1f);
        }
    }

    void TimerFailSequence()
    {
        collisionHandler.StartFailSequence(fail);
    }
}
