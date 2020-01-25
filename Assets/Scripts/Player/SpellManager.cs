using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SpellManager : MonoBehaviour
{
    public static GameObject m_player;

    public Transform m_rightHand;
    public Transform m_leftHand;

    public Spell m_rightHandSpell;
    public Spell m_rightHandSpellInstance;
    public Spell m_leftHandSpell;
    public Spell m_leftHandSpellInstance;

    public SteamVR_ActionSet m_actionSet;

    private void Awake()
    {
        m_player = gameObject;
        m_actionSet.Activate(SteamVR_Input_Sources.Any, 0, false);
        m_leftHandSpellInstance = Instantiate(m_leftHandSpell);
        m_rightHandSpellInstance = Instantiate(m_rightHandSpell);
        m_rightHandSpellInstance.onEquip();
        m_leftHandSpellInstance.onEquip();

    }
    private void Update()
    {

        //<-----------------------------------><Left Controller><-------------------------------------------------->
        if (SteamVR_Actions.development_Project_LeftTriggerBool.GetState(SteamVR_Input_Sources.Any))
        {
            if (SteamVR_Actions.development_Project_LeftTriggerBool.GetLastState(SteamVR_Input_Sources.Any))
            {
                //Hold
                Debug.Log("Left Trigger Hold");
                if (m_leftHandSpellInstance != null)
                {
                    m_leftHandSpellInstance.setTransform(m_leftHand);
                    m_leftHandSpellInstance.onHold();
                }
            }
            else if (!SteamVR_Actions.development_Project_LeftTriggerBool.GetLastState(SteamVR_Input_Sources.Any))
            {
                //Press
                Debug.Log("Left Trigger Press");
                if (m_leftHandSpellInstance != null)
                {
                    m_leftHandSpellInstance.setTransform(m_leftHand);
                    m_leftHandSpellInstance.onPress();
                }
            }
        }
        else if (!SteamVR_Actions.development_Project_LeftTriggerBool.GetState(SteamVR_Input_Sources.Any))
        {
            if (SteamVR_Actions.development_Project_LeftTriggerBool.GetLastState(SteamVR_Input_Sources.Any))
            {
                //Release
                Debug.Log("Left Trigger Release");
                if (m_leftHandSpellInstance != null)
                {
                    m_leftHandSpellInstance.setTransform(m_leftHand);
                    m_leftHandSpellInstance.onRelease();
                }
            }
        }
        //<--------------------------------------------------------------------------------------------------------->

        //<-----------------------------------><Right Controller><-------------------------------------------------->
        if (SteamVR_Actions.development_Project_RightTriggerBool.GetState(SteamVR_Input_Sources.Any))
        {
            if (SteamVR_Actions.development_Project_RightTriggerBool.GetLastState(SteamVR_Input_Sources.Any))
            {
                //Hold
                Debug.Log("Right Trigger Hold");
                if (m_rightHandSpellInstance != null)
                {
                    m_rightHandSpellInstance.setTransform(m_rightHand);
                    m_rightHandSpellInstance.onHold();
                }
            }
            else if (!SteamVR_Actions.development_Project_RightTriggerBool.GetLastState(SteamVR_Input_Sources.Any))
            {
                //Press
                Debug.Log("Right Trigger Press");
                if (m_rightHandSpellInstance != null)
                {
                    m_rightHandSpellInstance.setTransform(m_rightHand);
                    m_rightHandSpellInstance.onPress();
                }
            }
        }
        else if (!SteamVR_Actions.development_Project_RightTriggerBool.GetState(SteamVR_Input_Sources.Any))
        {
            if (SteamVR_Actions.development_Project_RightTriggerBool.GetLastState(SteamVR_Input_Sources.Any))
            {
                //Release
                Debug.Log("Right Trigger Release");
                if (m_rightHandSpellInstance != null)
                {
                    m_rightHandSpellInstance.setTransform(m_rightHand);
                    m_rightHandSpellInstance.onRelease();
                }
            }
        }
        //<--------------------------------------------------------------------------------------------------------->
    }

}
