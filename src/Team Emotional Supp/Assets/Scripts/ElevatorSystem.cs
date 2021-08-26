using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSystem : MonoBehaviour
{
    public Transform target;
    public Canvas canvas;

    private bool canTakeElevator = false;
    private GameObject player;

    void Start()
    {
        canvas.GetComponent<Canvas>().enabled = false;
        canvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canTakeElevator)
        {
            player.transform.position = target.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        canTakeElevator = true;
        player = collision.gameObject;

        canvas.GetComponent<Canvas>().enabled = true;
        canvas.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        canTakeElevator = false;

        canvas.GetComponent<Canvas>().enabled = false;
        canvas.gameObject.SetActive(false);
    }
}
