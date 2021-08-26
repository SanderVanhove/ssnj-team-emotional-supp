using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    public GameObject creditsImage;
    private bool creditsImageEnabled;

    // Start is called before the first frame update
    void Start()
    {
        creditsImage.SetActive(false);

    }
    public void EnableCredits()
    {
        creditsImage.SetActive(true);
    }

    public void DisableCredits()
    {
        creditsImage.SetActive(false);
    }
}
