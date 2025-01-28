using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2)]
public class Enemy : MonoBehaviour
{
    public float speed;

    private GameObject target;

    private void Start()
    {
        SearchForTarget();
    }

    private void SearchForTarget()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if(target == null) SearchForTarget();

        Movement();
    }

    private void Movement()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.01f)
        {
            Debug.Log("Reached the target!");
        }
    }
}
