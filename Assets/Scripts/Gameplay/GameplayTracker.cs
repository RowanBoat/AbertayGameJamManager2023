using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayTracker : MonoBehaviour
{
    private float m_timeLeft;
    public bool m_timerOn;

    public int m_playerMaxHealth { get; private set; }
    public int m_playerHealth { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        // Test
        //SetStartingHealth(0);
        //StartTimer(0);

        DifficultyManager[] difficultyManager = GameObject.FindObjectsOfType<DifficultyManager>();
        SetStartingHealth(difficultyManager[0].m_health);
        StartTimer(difficultyManager[0].m_time);
        Destroy(difficultyManager[0].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timerOn)
        {
            // Lose Cond.
            if (m_playerHealth <= 0)
            {
                Debug.Log("YOU LOSE!");
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
                Debug.Log("YOU WIN!");
                m_timeLeft = 0;
                m_timerOn = false;
            }
        }
    }

    public string GetTimerText()
    {
        float currentTime = m_timeLeft + 1;

        float mins = Mathf.FloorToInt(currentTime / 60);
        float secs = Mathf.FloorToInt(currentTime % 60);

        return string.Format("{0:00} : {1:00}", mins, secs);
    }

    public void StartTimer(float startTimeSecs)
    {
        m_timeLeft = startTimeSecs;
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
}