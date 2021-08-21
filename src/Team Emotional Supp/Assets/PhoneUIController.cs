using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PhoneUIController : MonoBehaviour
{
    private ScrollView messageContainer;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        messageContainer = root.Q<ScrollView>("message-container");

        Label label = new Label("Hey there!");
        label.EnableInClassList("phone-message", true);
        messageContainer.Add(label);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
