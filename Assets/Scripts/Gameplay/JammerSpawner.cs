using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_jammerPrefab;
    [Range(5,20)] public float m_spawnerInterval;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newJammer = Instantiate(m_jammerPrefab, new Vector2(Random.Range(-2, 2) + transform.position.x, 0.1f), Quaternion.identity);
        StartCoroutine(SpawnJammer(m_spawnerInterval, m_jammerPrefab));
    }

     private IEnumerator SpawnJammer(float interval, GameObject prefab)
    {
        yield return new WaitForSeconds(interval);
        GameObject newJammer = Instantiate(prefab, new Vector2(Random.Range(-2, 2) + transform.position.x, 0.1f), Quaternion.identity);
        StartCoroutine(SpawnJammer(interval, prefab));
    }
}
