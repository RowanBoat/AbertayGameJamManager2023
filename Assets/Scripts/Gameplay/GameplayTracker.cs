using UnityEngine;

public class GameplayTracker : MonoBehaviour
{
    private float m_initTime;
    private float m_timeLeft;
    public bool m_timerOn;
    public UIController m_ui;

    public int m_playerMaxHealth { get; private set; }
    public int m_playerHealth { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        // Set HP and Time based on difficulty
        DifficultyManager[] difficultyManager = GameObject.FindObjectsOfType<DifficultyManager>();
        if (difficultyManager.Length != 0)
        {         

            SetStartingHealth(difficultyManager[0].m_health);
            StartTimer(difficultyManager[0].m_time);
            Destroy(difficultyManager[0].gameObject);
        }
        // Test
        else
        {
            SetStartingHealth(5);
            StartTimer(180);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timerOn)
        {
            // Lose Conditions
            if (m_playerHealth <= 0)
            {
                Debug.Log("You were banned from holding another Jam!");
                m_ui.EndMenu(false);
                Time.timeScale = 0;
                m_timerOn = false;
                return;
            }

            Jammer[] allJammers = GameObject.FindObjectsOfType<Jammer>();
            if (allJammers.Length <= 0)
            {
                Debug.Log("You sent all your Jammers home!");
                m_ui.EndMenu(false);
                Time.timeScale = 0;
                m_timerOn = false;
                return;
            }
     
            // Win Cond.
            if (m_timeLeft > 0)
            {
                m_timeLeft -= Time.deltaTime;
            }
            else
            {
                Debug.Log("You Jam was a success!");
                m_ui.EndMenu(true);
                Time.timeScale = 0;
                m_timeLeft = 0;
                m_timerOn = false;
            }
        }
    }


    // Timer text for UI
    public string GetTimerText()
    {
        float currentTime = m_timeLeft + 1;

        float mins = Mathf.FloorToInt(currentTime / 60);
        float secs = Mathf.FloorToInt(currentTime % 60);

        return string.Format("{0:00}:{1:00}", mins, secs);
    }

    public void StartTimer(float startTimeSecs)
    {
        m_initTime = m_timeLeft = startTimeSecs;
        m_timerOn = true;
    }

    public void SetStartingHealth(int val)
    {
        m_playerMaxHealth = val;
        m_playerHealth = val;
    }

    public void SetTimerPaused(bool val)
    {
        m_timerOn = val;
    }

    public void DamagePlayer()
    {
        m_playerHealth--;
        m_ui.SecurityPopup();
    }

    public GameObject GetRestartDifficulty()
    {
        GameObject objToSpawn = new GameObject("DifficultyManager");
        DifficultyManager difficultyManager = objToSpawn.AddComponent<DifficultyManager>();
        difficultyManager.m_health = m_playerMaxHealth;
        difficultyManager.m_time = m_initTime;

        return objToSpawn;
    }
}