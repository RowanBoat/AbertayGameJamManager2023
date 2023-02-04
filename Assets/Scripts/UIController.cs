using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject m_pauseMenu;
    private GameplayTracker gameController;

    // Start is called before the first frame update
    void Start()
    {
        m_pauseMenu.SetActive(false);
        gameController = GameObject.FindObjectOfType<GameplayTracker>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResumeGame()
    {
        gameController.SetTimerPaused(false);
    }

    public void RestartLevel()
    {

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenus", LoadSceneMode.Single);
    }

    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Closing Game");
    }
}
