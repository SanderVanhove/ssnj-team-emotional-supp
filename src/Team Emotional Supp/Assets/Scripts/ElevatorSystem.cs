using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSystem : MonoBehaviour
{
    public Transform rightElevatorTarget;
    public Transform leftElevatorTarget;

    public bool rightElevator;
    public bool leftElevator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && rightElevator == true)
        {
            transform.position = rightElevatorTarget.position;
        }

        if (Input.GetKeyDown(KeyCode.E) && leftElevator == true)
        {
            transform.position = leftElevatorTarget.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Right Elevator"))
        {
            rightElevator = true;
        }

        if (collision.CompareTag("Left Elevator"))
        {
            leftElevator = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Right Elevator"))
        {
            rightElevator = false;
        }

        if (collision.CompareTag("Left Elevator"))
        {
            leftElevator = false;
        }


    }
}
