using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayTracker : MonoBehaviour
{
    private float m_timeLeft;
    public bool m_timerOn;

    // Start is called before the first frame update
    void Start()
    {
        m_timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timerOn)
        {
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
}