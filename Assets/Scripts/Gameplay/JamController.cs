using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class JamController : MonoBehaviour
{
    public Camera m_camera;
    private Vector3 m_camPosition;
    public RectTransform m_jammerMenu;
    public Slider m_hungerSlider;
    public Slider m_motivationSlider;
    public Slider m_tirednessSlider;
    private Jammer m_jammer;
    [SerializeField] public const float m_camSpeed = 7.5f;
    [SerializeField] public const float m_sprintSpeed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_camPosition = m_camera.transform.position;
        m_jammerMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        //RaycastHit[] hits;
        Collider2D[] hits;

        // LMB raycast interaction
        if (Input.GetMouseButtonDown(0))
        {
            //hits = Physics.RaycastAll(ray, 100, LayerMask.GetMask("UI"));
            hits = Physics2D.OverlapPointAll(m_camera.ScreenToWorldPoint(Input.mousePosition));
            //Debug.DrawLine(ray.origin, ray.direction * 100, Color.green, 1);
            if (hits.Length != 0)
            {
                foreach (Collider2D hit in hits)
                {
                    m_jammer = hit/*.collider*/.gameObject.GetComponent<Jammer>();
                    if (m_jammer != null)
                    {
                        // Open menu for Jammer stats
                        Vector2 viewportPosition = m_camera.WorldToViewportPoint(m_jammer.transform.position);

                        Vector2 finalPosition = new Vector2(viewportPosition.x * Screen.width, viewportPosition.y * Screen.height) - (m_jammerMenu.rect.size / 2);
                        m_jammerMenu.anchoredPosition = new Vector2(finalPosition.x, finalPosition.y);

                        m_jammerMenu.gameObject.SetActive(true);

                        m_jammer.SetStayStill(true);

                        break;
                    }
                }
            }
            else
            {
                if (!IsPointerOverUIElement(GetEventSystemRaycastResults()))
                    CloseJammerMenu(0);
            }
        }

        if (m_jammer != null)
        {
            m_hungerSlider.value = m_jammer.m_hungry / 10.0f;
            m_motivationSlider.value = m_jammer.m_motivated / 10.0f;
            m_tirednessSlider.value = m_jammer.m_sleepy / 10.0f;
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
        if (m_jammer)
        {
            m_jammer.SetStayStill(false);

            switch (val)
            {
                case 1: m_jammer.m_hungry -= 3; break;
                case 2: m_jammer.m_motivated += 5; break;
                case 3: m_jammer.m_sleepy -= 6; break;
                case 4: Destroy(m_jammer.gameObject); break;
                default: break;
            }
            m_jammer = null;
        }
        m_jammerMenu.gameObject.SetActive(false);
    }

    // https://forum.unity.com/threads/how-to-detect-if-mouse-is-over-ui.1025533/
    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}