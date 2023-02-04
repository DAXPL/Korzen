using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float speed = 1f;
    private int pointIndex = 0;
    [SerializeField] private float step = 0;
    Vector3 lastPos;
    [SerializeField] private GameEvent trigEvent;
    void Start()
    {
        lastPos = patrolPoints[0].position;
    }

    void Update()
    {
        step += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(lastPos, patrolPoints[pointIndex].position, step);

        if (step >= 1)
        {
            lastPos = patrolPoints[pointIndex].position;
            step = 0;
            pointIndex++;
            if (pointIndex >= patrolPoints.Length) { pointIndex = 0; }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(trigEvent!= null) { trigEvent.Raise(); }
        }
    }
}
