using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTrigger : MonoBehaviour
{
    public DialogController dialogueController;
    public Canvas canvas;
    public TextAsset inkFile;

    private bool isInArea = false;

    private void Start()
    {
        canvas.GetComponent<Canvas>().enabled = false;
        canvas.gameObject.SetActive(false);
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && isInArea)
            dialogueController.Init(inkFile);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas.GetComponent<Canvas>().enabled = true;
        canvas.gameObject.SetActive(true);
        isInArea = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canvas.GetComponent<Canvas>().enabled = false;
        canvas.gameObject.SetActive(false);
        isInArea = false;
    }
}
