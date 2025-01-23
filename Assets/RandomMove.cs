using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movSpeed = 2f;
    public float directionChangeInterval = 3f;
    public float boundaryRadius = 5f;

    private Vector2 movement;
    private Vector3 startingPosition;


    void Start()
    {
        startingPosition = transform.position;
        StartCoroutine(ChangeMovementDirection());

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement * movSpeed * Time.deltaTime);

        //keep annoyance at boundary
        Vector3 distanceFromStart = transform.position - startingPosition;
        if (distanceFromStart.magnitude > boundaryRadius)
        {
            Vector3 fromOriginToObject = transform.position - startingPosition;
            fromOriginToObject *= boundaryRadius / fromOriginToObject.magnitude;
            transform.position = startingPosition + fromOriginToObject;
        }
    }

    //Coroutine
    System.Collections.IEnumerator ChangeMovementDirection()
    {
        while (true)
        {
            float angle = Random.Range(0f, 360f);
            movement = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            //wait at interval value ro change direction
            //
            yield return new WaitForSeconds(directionChangeInterval);

        }
    }
}
