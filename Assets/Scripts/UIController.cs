using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject HowToPlayScreen;
    public GameObject CreditsScreen;
    public GameObject Background;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        Background.SetActive(true);
        HowToPlayScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        MainMenu.SetActive(false);
        HowToPlayScreen.SetActive(true);
    }

    public void Credits()
    {
        MainMenu.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        HowToPlayScreen.SetActive(false);
        CreditsScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Closing Game");
    }
}
