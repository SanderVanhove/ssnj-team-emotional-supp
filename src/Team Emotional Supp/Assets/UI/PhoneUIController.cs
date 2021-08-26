using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class PhoneUIController : MonoBehaviour
{
    private VisualElement phoneUI;
    private VisualElement miniPhoneUI;
    private VisualElement whutsUp;
    private VisualElement whutsUpContacts;
    private VisualElement instaSnap;
    private VisualElement menu;
    private VisualElement miniVibrations;

    private ScrollView messageContainer;
    private ScrollView contactContainer;
    private ScrollView instaPostContainer;

    private Button whutsUpButton;
    private Button whutsUpBackButton;
    private Button whutsUpHomeButton;
    private Button instaSnapButton;
    private Button instaSnapBackButton;
    private Button closePhoneButton;
    private Button openPhoneButton;

    private VisualTreeAsset instaPostTemplate;
    private Label texterNameLabel;

    private Dictionary<string, List<string>> messages = new Dictionary<string, List<string>>();
    private bool hasNewMessage;
    private bool hasNewPost;

    private float vibrationDelta = .5f;

    // Start is called before the first frame update
    void Start()
    {
        instaPostTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/InstaPost.uxml");

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        phoneUI = root.Q<VisualElement>("phone-ui");
        miniPhoneUI = root.Q<VisualElement>("mini-phone-ui");
        whutsUp = root.Q<VisualElement>("text-app");
        whutsUpContacts = root.Q<VisualElement>("text-menu");
        instaSnap = root.Q<VisualElement>("picture-app");
        menu = root.Q<VisualElement>("phone-menu");
        miniVibrations = root.Q<VisualElement>("mini-vib"); 

        whutsUpButton = root.Q<Button>("whutsup-app-icon");
        whutsUpBackButton = root.Q<Button>("whutsup-back");
        whutsUpHomeButton = root.Q<Button>("whutsup-home");
        whutsUpButton.clicked += SwitchToWhutsUp;
        whutsUpHomeButton.clicked += SwitchToMenu;
        whutsUpBackButton.clicked += SwitchToWhutsUp;

        instaSnapButton = root.Q<Button>("instasnap-app-icon");
        instaSnapBackButton = root.Q<Button>("instasnap-home");
        instaSnapButton.clicked += SwitchToInstaSnap;
        instaSnapBackButton.clicked += SwitchToMenu;

        openPhoneButton = root.Q<Button>("mini-phone");
        closePhoneButton = root.Q<Button>("close-big-phone");
        openPhoneButton.clicked += OpenPhone;
        closePhoneButton.clicked += ClosePhone;

        messageContainer = root.Q<ScrollView>("message-container");
        contactContainer = root.Q<ScrollView>("name-container");
        instaPostContainer = root.Q<ScrollView>("post-container");

        texterNameLabel = root.Q<Label>("texter-name");

        SwitchToMenu();
        SendTestMessages();
    }

    private void SendTestMessages()
    {
        StartCoroutine(AddDelayedMessage("Hey sweety,", "Mom <3", 5));
        StartCoroutine(AddDelayedMessage("Good luck on your new job!", "Mom <3", 6));
        StartCoroutine(AddDelayedMessage("I know you wanted this for so long.", "Mom <3", 7.5f));
        StartCoroutine(AddDelayedMessage("So you have fun alright?", "Mom <3", 9));
        StartCoroutine(AddDelayedMessage("Love you!", "Mom <3", 10));

        StartCoroutine(AddDelayedMessage("You'll kill it today!", "BFF Laura", 13));
        StartCoroutine(AddDelayedMessage("Show those game nerds your awesome skills!", "BFF Laura", 15));

        StartCoroutine(AddDelayedInstaPost("Rose", "Starting my new job as game dev, wish me luck!!", "Assets/UI/placeholder_image.png", 5));
    }

    private void AddMessageToContainer(string text)
    {
        Label label = new Label(text);
        label.EnableInClassList("phone-message", true);
        messageContainer.Add(label);
    }

    public void AddMessage(string text, string userName)
    {
        if (!messages.ContainsKey(userName))
            messages[userName] = new List<string>();
        messages[userName].Add(text);

        hasNewMessage = true;
        StartCoroutine(RingPhone());

        if (whutsUp.style.display == DisplayStyle.Flex && texterNameLabel.text == userName)
            AddMessageToContainer(text);
        if (whutsUpContacts.style.display == DisplayStyle.Flex && messages[userName].Count == 1)
            AddContact(userName);
    }

    public IEnumerator AddDelayedMessage(string text, string userName, float delay)
    {
        yield return new WaitForSeconds(delay);
        AddMessage(text, userName);
    }

    public void AddInstaPost(string name, string text, string imagePath)
    {
        VisualElement newPost = instaPostTemplate.CloneTree();
        newPost.Q<Label>("post-name").text = name;
        newPost.Q<Label>("post-text").text = text;
        newPost.Q<VisualElement>("post-image").style.backgroundImage = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);

        instaPostContainer.Add(newPost);

        hasNewPost = true;
        StartCoroutine(RingPhone());
    }

    public IEnumerator AddDelayedInstaPost(string name, string text, string imagePath, float delay)
    {
        yield return new WaitForSeconds(delay);
        AddInstaPost(name, text, imagePath);
    }


    private void SwitchToInstaSnap()
    {
        menu.style.display = DisplayStyle.None;
        instaSnap.style.display = DisplayStyle.Flex;
        hasNewPost = false;
    }

    private void AddContact(string name)
    {
        Button contact = new Button();
        contact.text = name;
        contact.EnableInClassList("contact-name", true);
        contact.clicked += () => SwitchToWhutsUpContact(name);
        contactContainer.Add(contact);
    }

    private void SwitchToWhutsUp()
    {
        menu.style.display = DisplayStyle.None;
        whutsUp.style.display = DisplayStyle.None;
        whutsUpContacts.style.display = DisplayStyle.Flex;

        contactContainer.Clear();
        foreach (string key in messages.Keys)
            AddContact(key);

        hasNewMessage = false;
    }

    private void SwitchToWhutsUpContact(string name)
    {
        whutsUpContacts.style.display = DisplayStyle.None;
        whutsUp.style.display = DisplayStyle.Flex;

        messageContainer.Clear();
        texterNameLabel.text = name;
        foreach (string text in messages[name])
            AddMessageToContainer(text);
    }

    private void SwitchToMenu()
    {
        menu.style.display = DisplayStyle.Flex;
        instaSnap.style.display = DisplayStyle.None;
        whutsUp.style.display = DisplayStyle.None;
        whutsUpContacts.style.display = DisplayStyle.None;
    }

    private void OpenPhone()
    {
        miniPhoneUI.style.display = DisplayStyle.None;
        phoneUI.style.display = DisplayStyle.Flex;
    }

    private void ClosePhone()
    {
        miniPhoneUI.style.display = DisplayStyle.Flex;
        phoneUI.style.display = DisplayStyle.None;
    }

    public IEnumerator RingPhone()
    {
        miniVibrations.style.display = DisplayStyle.Flex;
        yield return new WaitForSeconds(vibrationDelta);
        miniVibrations.style.display = DisplayStyle.None;
        yield return new WaitForSeconds(vibrationDelta);

        if (hasNewMessage || hasNewPost) 
            StartCoroutine(RingPhone());
    }
}
