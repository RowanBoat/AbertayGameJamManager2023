using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject m_pauseMenu;
    public GameObject m_pausePage;
    public GameObject m_quitPage;
    private GameplayTracker gameController;
    public Camera m_camera;
    private Vector3 m_camPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_pauseMenu.SetActive(false);
        m_quitPage.SetActive(false);
        gameController = GameObject.FindObjectOfType<GameplayTracker>();
    }

    // Update is called once per frame
    void Update()
    {
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
        gameController.SetTimerPaused(val);
        if (val) 
            Time.timeScale = 0; 
        else 
            Time.timeScale = 1;
        m_pauseMenu.SetActive(val);
        // Insert Resume Code
    }

    public void RestartLevel()
    {
        // Insert Reset Code
    }

    public void QuitMenu(bool val)
    {
        m_pausePage.SetActive(!val);
        m_quitPage.SetActive(val);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenus", LoadSceneMode.Single);
        // Maybe Reset Code?
    }

    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Closing Game");
    }
}
