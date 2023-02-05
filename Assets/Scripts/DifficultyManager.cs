using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public float m_time { get; set; }
    public int m_health { get; set; }

    public void SetDifficulty(int diff)
    {
        switch (diff)
        {
            case 1:
                m_time = 180;
                m_health = 5;
                break;
            case 2:
                m_time = 360;
                m_health = 4;
                break;
            case 3:
                m_time = 720;
                m_health = 3;
                break;

            default:
                m_time = 0;
                m_health = 0;
                break;
        }

    }
}
