using Unity.VisualScripting;
using UnityEngine;

public class Jammer : MonoBehaviour
{
    //TraitsEnum
    public enum Trait
    {
        Default = 0,
        Drunk = 1,
        Hungry = 2,
        Sad = 3,
        Sleepy = 4,
        Wanderer = 5
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
    [SerializeField] public Trait m_trait = 0;

    public SpriteRenderer m_spriteRenderer;

    private Vector2 m_targetLocation;
    private bool m_shouldBeMoving = false;

    private float m_eventChanceTimer;
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
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

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

        if (m_eventChanceTimer <= 0)
        {
            m_eventChanceTimer = 1.0f;

            if (m_sleepy >= 10 && Random.Range(1, 50) == 1)
            {
                Debug.Log("Student Fell Asleep");
                m_gameTracker.DamagePlayer();
                return;
            }
            if (m_hungry >= 10 && Random.Range(1, 50) == 1)
            {
                Debug.Log("Student Eating At Desk");
                m_gameTracker.DamagePlayer();
                return;
            }
            if (m_motivated <= 0 && Random.Range(1, 50) == 1)
            {
                Debug.Log("Student Is Being Rowdy");
                m_gameTracker.DamagePlayer();
                return;
            }

            if (m_trait == Trait.Drunk)
            {
                if (Random.Range(1, 60) == 1)
                {
                    Debug.Log("Student Is Drunk");
                    m_gameTracker.DamagePlayer();
                    return;
                }
            }
            else if (Random.Range(1, 240) == 1)
            {
                Debug.Log("Student Is Drunk");
                m_gameTracker.DamagePlayer();
                return;
            }

            if (m_trait == Trait.Wanderer)
            {
                if (Random.Range(1, 60) == 1)
                {
                    Debug.Log("Student Has Gone AWOL");
                    m_gameTracker.DamagePlayer();
                    return;
                }
            }
            else if (Random.Range(1, 240) == 1)
            {
                Debug.Log("Student Has Gone AWOL");
                m_gameTracker.DamagePlayer();
                return;
            }
        }

        // Movement
        Rigidbody2D jammerBody = gameObject.GetComponent<Rigidbody2D>();
        bool stationary = (Mathf.Abs(jammerBody.velocity.x) < 0.001f && Mathf.Abs(jammerBody.velocity.y) < 0.001f);

        if (stationary && !m_shouldBeMoving)
        {
            m_targetLocation = new Vector2(transform.position.x, transform.position.y);
            float targetX = 0;
            bool pathClear = false;

            while ((targetX > -2 && targetX < 2) || !pathClear)
            {
                targetX = Random.Range(-7.5f, 7.5f);
                m_targetLocation.x += targetX;

                Vector2 pos = gameObject.transform.position;
                Vector2 dir = m_targetLocation - pos;

                RaycastHit2D[] hits = Physics2D.RaycastAll(pos, dir.normalized, Mathf.Abs(targetX) * 1.2f);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.rigidbody != null && hit.rigidbody != jammerBody)
                    {
                        pathClear = false;
                        break;
                    }
                    else
                        pathClear = true;
                }
            }
            m_shouldBeMoving = true;
        }
        else if (m_shouldBeMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_targetLocation, 2.5f * dt);
            Vector2 vec2 = new Vector2(transform.position.x, transform.position.y);
            if ((m_targetLocation - vec2).magnitude < 0.1)
            {
                m_shouldBeMoving = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_shouldBeMoving = false;
    }
}
