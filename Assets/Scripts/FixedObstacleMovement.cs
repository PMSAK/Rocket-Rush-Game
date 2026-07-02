using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedObstacleMovement : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    Vector3 lastPosition;
    float movementFactor;

    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
        lastPosition = transform.position;
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);

        Vector3 direction = transform.position - lastPosition;

        if (direction.sqrMagnitude > 0.0001f)
        {
            transform.forward = direction.normalized;
        }

        lastPosition = transform.position;
    }

}
