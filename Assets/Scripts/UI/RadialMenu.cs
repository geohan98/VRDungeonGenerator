using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadialMenu : MonoBehaviour
{
    #region Public Variables
    [Header("Radial Menu Scene")]
    public Transform m_Background;
    public Transform m_Selector;
    public Transform m_Cursor;
    [Header("RadialMenu Options")]
    public bool m_leftHand = false;
    public bool m_Debug = true;
    [Header("Radial Menu Events")]
    public UnityEvent m_Top = new UnityEvent(); //1
    public UnityEvent m_Right = new UnityEvent(); //0
    public UnityEvent m_Bottom = new UnityEvent(); //3
    public UnityEvent m_Left = new UnityEvent(); //2
    [Header("Feedback")]
    public AudioClip m_ChangeSound;
    #endregion

    #region Private Variables
    private Vector2 m_CursorPosition;
    private int m_lastIndex;
    #endregion

    #region Unity Functions
    private void Update()
    {
        if (m_leftHand)
        {
            if (InputManager.s_Instance.m_touchpadTouchLeft)
            {
                m_Cursor.gameObject.SetActive(true);
                m_CursorPosition = InputManager.s_Instance.m_touchpadPositionLeft / 2.0f;
                m_Background.gameObject.SetActive(true);
                m_Selector.gameObject.SetActive(true);
                float angle = Mathf.Rad2Deg * Mathf.Atan2(InputManager.s_Instance.m_touchpadPositionLeft.y, InputManager.s_Instance.m_touchpadPositionLeft.x);
                if (angle < 0) angle += 360;
                m_lastIndex = Mathf.RoundToInt(angle / 90.0f);
                m_Selector.localEulerAngles = new Vector3(0, 0, (m_lastIndex - 1) * 90.0f);
            }
            else
            {
                m_Cursor.gameObject.SetActive(false);
                m_Background.gameObject.SetActive(false);
                m_Selector.gameObject.SetActive(false);
            }
            if (InputManager.s_Instance.m_touchpadPressUpLeft)
            {
                PlayerManager.s_Instance.PlaySoundOneShot(m_ChangeSound);
                switch (m_lastIndex)
                {
                    case 0:
                        m_Right.Invoke();
                        break;
                    case 1:
                        m_Top.Invoke();
                        break;
                    case 2:
                        m_Left.Invoke();
                        break;
                    case 3:
                        m_Bottom.Invoke();
                        break;
                }
            }
        }
        else
        {
            if (InputManager.s_Instance.m_touchpadTouchRight)
            {
                m_Cursor.gameObject.SetActive(true);
                m_CursorPosition = InputManager.s_Instance.m_touchpadPositionRight / 2.0f;
                m_Background.gameObject.SetActive(true);
                m_Selector.gameObject.SetActive(true);
                float angle = Mathf.Rad2Deg * Mathf.Atan2(InputManager.s_Instance.m_touchpadPositionRight.y, InputManager.s_Instance.m_touchpadPositionRight.x);
                if (angle < 0) angle += 360;
                m_lastIndex = Mathf.RoundToInt(angle / 90.0f);
                m_Selector.localEulerAngles = new Vector3(0, 0, (m_lastIndex - 1) * 90.0f);
            }
            else
            {
                m_Cursor.gameObject.SetActive(false);
                m_Background.gameObject.SetActive(false);
                m_Selector.gameObject.SetActive(false);
            }
            if (InputManager.s_Instance.m_touchpadPressUpRight)
            {
                PlayerManager.s_Instance.PlaySoundOneShot(m_ChangeSound);
                switch (m_lastIndex)
                {
                    case 0:
                        m_Right.Invoke();
                        break;
                    case 1:
                        m_Top.Invoke();
                        break;
                    case 2:
                        m_Left.Invoke();
                        break;
                    case 3:
                        m_Bottom.Invoke();
                        break;
                }
            }
        }

        if (m_Cursor) m_Cursor.localPosition = m_CursorPosition;
    }
    #endregion

    #region Public Functions

    #endregion

    #region Private Functions
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[RadialMenu][" + _msg + "]");
        }
    }
    #endregion
}
