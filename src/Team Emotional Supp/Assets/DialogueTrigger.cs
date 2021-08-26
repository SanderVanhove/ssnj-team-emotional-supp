using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogController dialogueController;
    public TextAsset inkFile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogueController.Init(inkFile);
    }
}
