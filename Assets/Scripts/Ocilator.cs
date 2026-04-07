using UnityEngine;

public class Ocilator : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float distance = 10f;

    [SerializeField] Vector3 moveAxis = Vector3.up;

    Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newpos = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPos + moveAxis.normalized * newpos;
    }
}
