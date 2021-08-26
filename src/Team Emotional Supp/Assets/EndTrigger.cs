using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public Canvas canvas;

    private bool isInArea = false;

    private void Start()
    {
        canvas.GetComponent<Canvas>().enabled = false;
        canvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInArea)
            SceneManager.LoadScene(3);
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
