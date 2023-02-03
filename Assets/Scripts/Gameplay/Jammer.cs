using System.Collections;
using System.Collections.Generic;
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
    [Range(0, 10)] public int m_hungry = 0;
    [Range(0, 10)] public int m_motivated = 0;
    [SerializeField] public Trait m_trait = 0;

    public SpriteRenderer m_spriteRenderer;

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

    }
}
