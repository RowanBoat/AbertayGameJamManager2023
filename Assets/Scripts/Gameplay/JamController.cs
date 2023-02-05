using UnityEngine;
using UnityEngine.AI;

public class JamController : MonoBehaviour
{
    public Camera m_camera;
    private Vector3 m_camPosition;
    [SerializeField] private const float m_sprintSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_camPosition = m_camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // LMB raycast interaction
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Open menu for Jammer interaction
            }
        }
        // RMB raycast interaction
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Open menu for Jammer interaction
            }
        }

        // Movement Controls - Shift to speed up.
        m_camPosition = m_camera.transform.position;
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            m_camPosition.x += Input.GetAxis("Horizontal") * dt;
        }
        else
        {
            m_camPosition.x += Input.GetAxis("Horizontal") * m_sprintSpeed * dt;
        }

        if (m_camera.transform.position != m_camPosition)
            m_camera.transform.position = m_camPosition;
    }
}
