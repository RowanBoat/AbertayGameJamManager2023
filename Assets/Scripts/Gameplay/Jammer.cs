using TMPro;
using UnityEngine;

public class Jammer : MonoBehaviour
{
    //Enums
    public enum Trait
    {
        Default = 0,
        Drunk = 1,
        Hungry = 2,
        Sad = 3,
        Sleepy = 4,
        Wanderer = 5
    };
    public enum JammerState
    {
        Default = 0,
        Annoying = 1,
        Asleep = 2,
        Drunk = 3,
        Eating = 4,
        Wandering = 5
    };

    //Members
    [Range(0, 10)] public int m_sleepy = 0;
    public float m_sleepyTimeMax = 20.0f;
    float m_sleepyTime = 0;
    [Range(0, 10)] public int m_hungry = 0;
    public float m_hungryTimeMax = 25.0f;
    float m_hungryTime = 0;
    [Range(0, 10)] public int m_motivated = 0;
    public float m_motivatedTimeMax = 10.0f;
    float m_motivatedTime = 0;
    public Trait m_trait = 0;
    public JammerState m_jammerState {get;private set;}

    public SpriteRenderer m_spriteRenderer;
    public SpriteRenderer[] m_emotes;

    private Vector2 m_targetLocation;
    private bool m_shouldBeMoving = false;
    private bool m_stayStill = false;

    private float m_eventChanceTimer;
    public float m_lifeCountdownMax = 15.0f;
    float m_lifeCountdown = 0;
    public TextMeshProUGUI m_countdown;


    private GameplayTracker m_gameTracker;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise
        m_sleepy = Random.Range(0, 5);
        m_sleepyTime = m_sleepyTimeMax;
        m_hungry = Random.Range(0, 5);
        m_hungryTime = m_hungryTimeMax;
        m_motivated = Random.Range(5, 10);
        m_motivatedTime = m_motivatedTimeMax;
        m_trait = (Trait)Random.Range(1, 5);

        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        m_eventChanceTimer = 1.0f;
        GameplayTracker[] gameTracker = GameObject.FindObjectsOfType<GameplayTracker>();
        if (gameTracker.Length != 0)
        {
            m_gameTracker = gameTracker[0];
        }

        foreach(SpriteRenderer sprite in m_emotes)
        {
            sprite.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (m_jammerState != JammerState.Default)
        {
            if (!m_countdown.gameObject.activeSelf)
                m_countdown.gameObject.SetActive(true);

            Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
            Vector2 finalPosition = new Vector2(viewportPosition.x * Screen.width, (viewportPosition.y + 0.15f) * Screen.height) - (m_countdown.gameObject.GetComponent<RectTransform>().rect.size / 2);
            m_countdown.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(finalPosition.x, finalPosition.y);

            m_lifeCountdown -= dt;
            m_countdown.text = Mathf.RoundToInt(m_lifeCountdown).ToString();
            if (m_lifeCountdown <= 0)
            {
                m_gameTracker.DamagePlayer();
                m_jammerState = JammerState.Default;
                m_countdown.gameObject.SetActive(false);

                foreach (SpriteRenderer sprite in m_emotes)
                {
                    sprite.gameObject.SetActive(false);
                }
            }
        }

        // Timers
        if (m_sleepyTime > 0)
            m_sleepyTime -= dt;
        if (m_hungryTime > 0)
            m_hungryTime -= dt;
        if (m_motivatedTime > 0)
            m_motivatedTime -= dt;
        if (m_eventChanceTimer > 0)
            m_eventChanceTimer -= dt;

        if (m_sleepyTime <= 0 && m_sleepy < 10)
        {
            m_sleepy++;
            m_sleepyTime = m_sleepyTimeMax;
            if (m_trait == Trait.Sleepy)
                m_sleepyTime -= 3;
        }
        if (m_hungryTime <= 0 && m_hungry < 10)
        {
            m_hungry++;
            m_hungryTime = m_hungryTimeMax;
            if (m_trait == Trait.Hungry)
                m_hungryTime -= 3;
        }
        if (m_motivatedTime <= 0 && m_motivated > 0)
        {
            m_motivated--;
            m_motivatedTime = m_motivatedTimeMax;
            if (m_trait == Trait.Sad)
                m_motivatedTime -= 3;
        }
        
        // Check to trigger events every second assuming this Jammer isn't already dealing with one
        if (m_eventChanceTimer <= 0 && m_jammerState == JammerState.Default)
        {
            m_eventChanceTimer = 1.0f;

            if (m_sleepy >= 10 && Random.Range(1, 50) == 1)
            {
                Debug.Log("Student Fell Asleep");
                m_jammerState = JammerState.Asleep;
                int i = (int)JammerState.Asleep;
                m_emotes[i - 1].gameObject.SetActive(true);
                m_lifeCountdown = m_lifeCountdownMax;
                return;
            }
            if (m_hungry >= 10 && Random.Range(1, 50) == 1)
            {
                Debug.Log("Student Eating At Desk");
                m_jammerState = JammerState.Eating;
                int i = (int)JammerState.Eating;
                m_emotes[i - 1].gameObject.SetActive(true);
                m_lifeCountdown = m_lifeCountdownMax;
                return;
            }
            if (m_motivated <= 0 && Random.Range(1, 50) == 1)
            {
                Debug.Log("Student Is Being Rowdy");
                m_jammerState = JammerState.Annoying;
                int i = (int)JammerState.Annoying;
                m_emotes[i - 1].gameObject.SetActive(true);
                m_lifeCountdown = m_lifeCountdownMax;
                return;
            }

            if (m_trait == Trait.Drunk)
            {
                if (Random.Range(1, 60) == 1)
                {
                    Debug.Log("Student Is Drunk");
                    m_jammerState = JammerState.Drunk;
                    int i = (int)JammerState.Drunk;
                    m_emotes[i - 1].gameObject.SetActive(true);
                    m_lifeCountdown = m_lifeCountdownMax;
                    return;
                }
            }
            else if (Random.Range(1, 240) == 1)
            {
                Debug.Log("Student Is Drunk");
                m_jammerState = JammerState.Drunk;
                int i = (int)JammerState.Drunk;
                m_emotes[i - 1].gameObject.SetActive(true);
                m_lifeCountdown = m_lifeCountdownMax;
                return;
            }

            if (m_trait == Trait.Wanderer)
            {
                if (Random.Range(1, 60) == 1)
                {
                    Debug.Log("Student Has Gone AWOL");
                    m_jammerState = JammerState.Wandering;
                    int i = (int)JammerState.Wandering;
                    m_emotes[i - 1].gameObject.SetActive(true);
                    m_lifeCountdown = m_lifeCountdownMax;
                    return;
                }
            }
            else if (Random.Range(1, 240) == 1)
            {
                Debug.Log("Student Has Gone AWOL");
                m_jammerState = JammerState.Wandering;
                int i = (int)JammerState.Wandering;
                m_emotes[i - 1].gameObject.SetActive(true);
                m_lifeCountdown = m_lifeCountdownMax;
                return;
            }
        }

        // Movement
        if (!m_stayStill)
        {
            Rigidbody2D jammerBody = gameObject.GetComponent<Rigidbody2D>();
            bool stationary = (Mathf.Abs(jammerBody.velocity.x) < 0.001f && Mathf.Abs(jammerBody.velocity.y) < 0.001f);

            if (stationary && !m_shouldBeMoving)
            {
                m_targetLocation = gameObject.transform.position;
                float targetX = 0;
                bool pathClear = false;

                while ((targetX > -2 && targetX < 2) || !pathClear)
                {
                    targetX = Random.Range(-7.5f, 7.5f);
                    m_targetLocation.x += targetX;

                    Vector2 pos = gameObject.transform.position;
                    Vector2 dir = (m_targetLocation - pos);

                    RaycastHit2D[] hits = Physics2D.RaycastAll(pos, dir.normalized, Mathf.Abs(targetX) * 1.2f);
                    Debug.DrawRay(pos, dir, Color.green, 1, false);
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.rigidbody != null && hit.rigidbody != jammerBody && hit.rigidbody.GetComponent<Jammer>() == null)
                        {
                            pathClear = false;
                            break;
                        }
                        else
                            pathClear = true;
                    }
                    GetComponentInChildren<SpriteRenderer>().transform.localScale = new(dir.normalized.x * 2, 2, 1);
                }
                m_shouldBeMoving = true;
            }
            else if (m_shouldBeMoving)
            {
                transform.position = Vector2.MoveTowards(transform.position, m_targetLocation, 2.5f * dt);
                Vector2 vec2 = new(transform.position.x, transform.position.y);
                if ((m_targetLocation - vec2).magnitude < 0.1)
                {
                    m_shouldBeMoving = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_shouldBeMoving = false;
    }

    public void SetStayStill(bool val)
    {
        m_stayStill = val;
        if (val)
            GetComponentInChildren<Animator>().StartPlayback();
        else
            GetComponentInChildren<Animator>().StopPlayback();
    }

    public void SetJammerState(JammerState val)
    {
        m_jammerState = val;
    }
}
