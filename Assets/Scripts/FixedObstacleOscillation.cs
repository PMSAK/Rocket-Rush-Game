using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedObstacleOscillation : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    float movementFactor;

    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}
