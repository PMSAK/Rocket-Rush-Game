using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacleMovement : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 targetPosition;

    [SerializeField] float speed = 5f;
    [SerializeField] float radius = 5f;

    void Start()
    {
        startPosition = transform.position;
        PickNewTarget();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 direction = targetPosition - transform.position;

        //Face the direction of movement
        if (direction.sqrMagnitude > 0.001f)
        {
            transform.forward = direction.normalized;
        }

        //When reached the target, pick a new target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            PickNewTarget();
        }
    }

    void PickNewTarget()
    {
        Vector2 randomOffSet = Random.insideUnitCircle * radius;

        targetPosition = startPosition + new Vector3(randomOffSet.x, randomOffSet.y, 0f);
    }
}
