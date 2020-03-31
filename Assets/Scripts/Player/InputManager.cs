using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InputManager : MonoBehaviour
{
    #region Public Variables
    [Header("Input Manager Options")]
    public bool m_Debug = true;
    public static InputManager s_Instance;
    [Header("Input Data")]
    public bool m_touchpadTouchLeft = false;
    public bool m_touchpadTouchRight = false;

    public bool m_touchpadPressDownLeft = false;
    public bool m_touchpadPressDownRight = false;

    public bool m_touchpadPressUpLeft = false;
    public bool m_touchpadPressUpRight = false;

    public Vector2 m_touchpadPositionLeft = Vector2.zero;
    public Vector2 m_touchpadPositionRight = Vector2.zero;

    public float m_triggerPositionLeft = 0;
    public float m_triggerPositionRight = 0;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (!s_Instance) s_Instance = this;
    }
    private void Update()
    {
        m_touchpadTouchLeft = SteamVR_Actions.default_TouchpadTouch[SteamVR_Input_Sources.LeftHand].state;
        m_touchpadTouchRight = SteamVR_Actions.default_TouchpadTouch[SteamVR_Input_Sources.RightHand].state;

        m_touchpadPressDownLeft = SteamVR_Actions.default_TouchpadPress[SteamVR_Input_Sources.LeftHand].stateDown;
        m_touchpadPressDownRight = SteamVR_Actions.default_TouchpadPress[SteamVR_Input_Sources.RightHand].stateDown;

        m_touchpadPressUpLeft = SteamVR_Actions.default_TouchpadPress[SteamVR_Input_Sources.LeftHand].stateUp;
        m_touchpadPressUpRight = SteamVR_Actions.default_TouchpadPress[SteamVR_Input_Sources.RightHand].stateUp;

        m_touchpadPositionLeft = SteamVR_Actions.default_TouchpadPosition[SteamVR_Input_Sources.LeftHand].axis;
        m_touchpadPositionRight = SteamVR_Actions.default_TouchpadPosition[SteamVR_Input_Sources.RightHand].axis;

        m_triggerPositionLeft = SteamVR_Actions.default_TriggerPosition[SteamVR_Input_Sources.LeftHand].axis;
        m_triggerPositionRight = SteamVR_Actions.default_TriggerPosition[SteamVR_Input_Sources.RightHand].axis;
    }
    #endregion

    #region Private Functions
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[InputManager][" + _msg + "]");
        }
    }
    #endregion
}
