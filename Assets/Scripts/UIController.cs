using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_howToPlayScreen;
    public GameObject m_creditsScreen;
    public GameObject m_background;
    public GameObject m_pauseMenu;
    public GameObject m_gameController;
    public DifficultyManager m_difficultyManager;
    private GameObject[] m_gameObjects;

    // Start is called before the first frame update
    void Start()
    {
        m_mainMenu.SetActive(true);
        m_background.SetActive(true);
        m_howToPlayScreen.SetActive(false);
        m_creditsScreen.SetActive(false);
        m_pauseMenu.SetActive(false);
        m_gameObjects = GameObject.FindGameObjectsWithTag("Game");
        foreach (GameObject m_gameObject in m_gameObjects)
        {
            m_gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void StartGame()
    {
        m_mainMenu.SetActive(false);
        m_howToPlayScreen.SetActive(true);
    }

    public void Credits()
    {
        m_mainMenu.SetActive(false);
        m_creditsScreen.SetActive(true);
    }

    public void DifficultySelection(int difficulty)
    {
        m_howToPlayScreen.SetActive(false);
        m_background.SetActive(false);
        m_difficultyManager.SetDifficulty(difficulty);
        m_gameController.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        m_howToPlayScreen.SetActive(false);
        m_creditsScreen.SetActive(false);
        m_mainMenu.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Closing Game");
    }
}
