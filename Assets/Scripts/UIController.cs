using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject m_pauseMenu;
    public GameObject m_pausePage;
    public GameObject m_quitPage;
    public GameObject m_security;
    public GameObject m_gameUI;  
    public GameObject m_winMenu;
    public GameObject m_loseMenu;
    public TextMeshProUGUI m_lives;
    public TextMeshProUGUI m_timer;
    private GameplayTracker gameController;
    public Camera m_camera;
    private Vector3 m_camPosition;
    private int temp = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_gameUI.SetActive(true);
        m_pauseMenu.SetActive(false);
        m_quitPage.SetActive(false);
        gameController = GameObject.FindObjectOfType<GameplayTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.m_playerHealth != temp)
        {
            m_lives.text = "Lives: " + gameController.m_playerHealth.ToString();
            temp = gameController.m_playerHealth;
        }

        m_timer.text = "Time Left: " + gameController.GetTimerText();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(true);
        }
    }

    public void MoveCamera(int val)
    {
        m_camPosition = m_camera.transform.position;
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01)
        {
            m_camPosition.x += val * 3;
            m_camera.transform.position = m_camPosition;
        }
    }

    public void PauseGame(bool val)
    {
        m_gameUI.SetActive(!val);
        gameController.SetTimerPaused(!val);
        if (val) 
            Time.timeScale = 0; 
        else 
            Time.timeScale = 1;
        m_pauseMenu.SetActive(val);
    }

    public void RestartLevel()
    {
        // Insert Reset Code
        GameObject restartDiff = gameController.GetRestartDifficulty();
        DontDestroyOnLoad(restartDiff);
        Time.timeScale = 1;
        gameController.SetTimerPaused(false);
        PauseGame(false);
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }

    public void QuitMenu(bool val)
    {
        m_pausePage.SetActive(!val);
        m_quitPage.SetActive(val);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        gameController.SetTimerPaused(false);
        SceneManager.LoadScene("MainMenus", LoadSceneMode.Single);
        // Maybe Reset Code?
    }

    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Closing Game");
    }
    public void SecurityPopup()
    {
        StartCoroutine(SecurityPopup(1));
    }
    private IEnumerator SecurityPopup(float interval)
    {
        m_security.SetActive(true);
        yield return new WaitForSeconds(interval);
        m_security.SetActive(false);
    }

    public void EndMenu(bool val)
    {
        m_winMenu.SetActive(val);
        m_loseMenu.SetActive(!val);
    }
}
