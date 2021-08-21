using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PhoneUIController : MonoBehaviour
{
    private ScrollView messageContainer;
    private Label texterNameLabel;

    public string texterName;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        messageContainer = root.Q<ScrollView>("message-container");
        texterNameLabel = root.Q<Label>("texter-name");
        texterNameLabel.text = texterName.Length > 0 ? texterName : "Texter Name";


        StartCoroutine(AddDelayedMessage("Hey", 1));
        StartCoroutine(AddDelayedMessage("You ok?", 2));
        StartCoroutine(AddDelayedMessage("This happens every time...", 3.5f));
        StartCoroutine(AddDelayedMessage("Text me back if you can!", 5));
    }

    public void AddMessage(string text)
    {
        Label label = new Label(text);
        label.EnableInClassList("phone-message", true);
        messageContainer.Add(label);

        //label.alpha = 0;
        //label.LeanAlpha(1, 0.5f);
    }

    public IEnumerator AddDelayedMessage(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        AddMessage(text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
