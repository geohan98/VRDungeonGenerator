using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR;

public class LazerPointer : MonoBehaviour
{
    [Header("LazerPointer Options")]
    #region Public Variables
    public LineRenderer m_Lazer;
    public bool m_LeftHand = false;
    public LayerMask m_LayerMask;
    public bool m_Debug = true;
    #endregion

    #region Private Variables
    SteamVR_Input_Sources m_InputSource;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (!m_LeftHand)
        {
            m_InputSource = SteamVR_Input_Sources.RightHand;
        }
        else
        {
            m_InputSource = SteamVR_Input_Sources.LeftHand;
        }
    }
    private void Update()
    {
        if (SteamVR_Actions.default_TriggerPosition[m_InputSource].axis > 0)
        {
            Vector3 endpoint = transform.position;
            RaycastHit ray;
            if (Physics.Raycast(transform.position, transform.forward, out ray, Mathf.Infinity, m_LayerMask))
            {
                endpoint = transform.position + transform.forward * ray.distance;
                Button button;
                if (button = ray.collider.gameObject.GetComponent<Button>())
                {
                    Debug.Log(ray.collider.gameObject.ToString());
                    button.Select();
                }
                else
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }
            else
            {
                endpoint = transform.position + transform.forward * 1000.0f;
                EventSystem.current.SetSelectedGameObject(null);
            }
            m_Lazer.SetPosition(0, transform.position);
            m_Lazer.SetPosition(1, endpoint);
        }
        else
        {
            m_Lazer.SetPosition(0, transform.position);
            m_Lazer.SetPosition(1, transform.position);
            EventSystem.current.SetSelectedGameObject(null);
        }
        if (SteamVR_Actions.default_ClickMenu[m_InputSource].stateDown)
        {
            RaycastHit ray;
            if (Physics.Raycast(transform.position, transform.forward, out ray, Mathf.Infinity, m_LayerMask))
            {
                Button button;
                if (button = ray.collider.gameObject.GetComponent<Button>())
                {
                    ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
                }
            }
        }
    }
    #endregion

    #region Public Functions

    #endregion

    #region Private Functions
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[LazerPointer][" + _msg + "]");
        }
    }
    #endregion
}
