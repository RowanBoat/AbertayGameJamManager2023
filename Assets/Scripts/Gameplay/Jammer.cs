using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
    [Range(0, 10)] public int m_hungry = 0;
    [Range(0, 10)] public int m_motivated = 0;
    [SerializeField] public Trait m_trait = 0;

    public SpriteRenderer m_spriteRenderer;

    private Vector2 m_targetLocation;
    private bool m_shouldBeMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise
        m_sleepy = Random.Range(0, 5);
        m_hungry = Random.Range(0, 5);
        m_motivated = Random.Range(5, 10);
        m_trait = (Trait)Random.Range(1, 5);

        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
            transform.position = Vector2.MoveTowards(transform.position, m_targetLocation, 5.0f * Time.deltaTime);
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
