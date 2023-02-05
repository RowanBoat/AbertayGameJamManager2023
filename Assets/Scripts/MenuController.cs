using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MenuController : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_howToPlayScreen;
    public GameObject m_creditsScreen;
    public GameObject m_background;
    private DifficultyManager[] difficultyManager;

    // Start is called before the first frame update
    void Start()
    {
        difficultyManager = GameObject.FindObjectsOfType<DifficultyManager>();
        m_mainMenu.SetActive(true);
        m_background.SetActive(true);
        m_howToPlayScreen.SetActive(false);
        m_creditsScreen.SetActive(false);

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
        difficultyManager[0].SetDifficulty(difficulty);
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
        DontDestroyOnLoad(difficultyManager[0]);
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
