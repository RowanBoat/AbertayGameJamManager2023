using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class JamController : MonoBehaviour
{
    public Camera m_camera;
    private Vector3 m_camPosition;
    public GameObject m_jammerMenu;
    private Jammer m_jammer;
    [SerializeField] public const float m_camSpeed = 7.5f;
    [SerializeField] public const float m_sprintSpeed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_camPosition = m_camera.transform.position;
        m_jammerMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits;

        // LMB raycast interaction
        if (Input.GetMouseButtonDown(0))
        {
            hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity, LayerMask.GetMask("UI"));
            if (hits.Length != 0)
            {
                foreach (RaycastHit2D hit in hits)
                {
                    m_jammer = hit.collider.gameObject.GetComponent<Jammer>();
                    if (m_jammer != null)
                    {
                        // Open menu for Jammer stats
                        m_jammerMenu.transform.position = m_jammer.transform.position;
                        m_jammerMenu.SetActive(true);
                    }
                }
            }
            else
            {
                CloseJammerMenu(0);
            }
        }

        // Movement Controls - Shift to speed up.
        m_camPosition = m_camera.transform.position;
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            m_camPosition.x += Input.GetAxis("Horizontal") * m_camSpeed * dt;
        }
        else
        {
            m_camPosition.x += Input.GetAxis("Horizontal") * m_sprintSpeed * dt;
        }

        if (m_camera.transform.position != m_camPosition)
            m_camera.transform.position = m_camPosition;
    }

    public void CloseJammerMenu(int val)
    {
        switch(val)
        {
            case 1: if (m_jammer) m_jammer.m_hungry -= 3; break;
            case 2: if (m_jammer) m_jammer.m_motivated += 5; break;
            case 3: if (m_jammer) m_jammer.m_sleepy -= 6; break;
            case 4: if (m_jammer) Destroy(m_jammer); break;
            default: break;
        }
        m_jammerMenu.SetActive(false);
    }
}