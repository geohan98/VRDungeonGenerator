using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    #region Public Variables
    [Header("Spell Manager Options")]
    public Spell m_Spell_1;
    public Spell m_Spell_2;
    public Spell m_Spell_3;
    public Spell m_Spell_4;
    public bool m_Debug = false;
    [Header("Spell Manager Scene")]
    public Transform m_RightHandTransform;
    public Transform m_LeftHandTransform;
    #endregion
    #region Private Variables
    [SerializeField] private Spell m_LeftHandSpell;
    [SerializeField] private Spell m_RightHandSpell;
    private float m_LeftHandLastUpdateValue = 0;
    private float m_RightHandLastUpdateValue = 0;
    #endregion
    #region Unity Functions
    private void Update()
    {
        if (m_LeftHandSpell)
        {
            //OnPress
            if (m_LeftHandLastUpdateValue == 0 && InputManager.s_Instance.m_triggerPositionLeft > 0)
            {
                m_LeftHandSpell.onPress();
            }
            //OnHold
            if (m_LeftHandLastUpdateValue > 0 && InputManager.s_Instance.m_triggerPositionLeft > 0)
            {
                Log("HOLD");
                m_LeftHandSpell.onHold();
            }
            //On Release
            if (m_LeftHandLastUpdateValue > 0 && InputManager.s_Instance.m_triggerPositionLeft == 0)
            {
                m_LeftHandSpell.onRelease();
            }
            m_LeftHandLastUpdateValue = InputManager.s_Instance.m_triggerPositionLeft;
        }
        if (m_RightHandSpell)
        {
            //OnPress
            if (m_RightHandLastUpdateValue == 0 && InputManager.s_Instance.m_triggerPositionRight > 0)
            {
                m_RightHandSpell.onPress();
            }
            //OnHold
            if (m_RightHandLastUpdateValue > 0 && InputManager.s_Instance.m_triggerPositionRight > 0)
            {
                Log("hold");
                m_RightHandSpell.onHold();
            }
            //On Release
            if (m_RightHandLastUpdateValue > 0 && InputManager.s_Instance.m_triggerPositionRight == 0)
            {
                m_RightHandSpell.onRelease();
            }
            m_RightHandLastUpdateValue = InputManager.s_Instance.m_triggerPositionRight;
        }
    }
    #endregion
    #region Public Functions
    public void EquipRight(int _index)
    {
        Log("Equip Right = " + _index.ToString());
        if (m_RightHandSpell)
        {
            m_RightHandSpell.onUnequip();
            Destroy(m_RightHandSpell);
        }
        if (!m_RightHandTransform)
        {
            Log("Missing Right Hand Transform");
            return;
        }
        switch (_index)
        {
            case 0:
                if (m_Spell_1)
                {
                    m_RightHandSpell = Instantiate(m_Spell_1);
                    m_RightHandSpell.onEquip();
                    m_RightHandSpell.setTransform(m_RightHandTransform);
                }
                break;
            case 1:
                if (m_Spell_2)
                {
                    m_RightHandSpell = Instantiate(m_Spell_2);
                    m_RightHandSpell.onEquip();
                    m_RightHandSpell.setTransform(m_RightHandTransform);
                }
                break;
            case 2:
                if (m_Spell_3)
                {
                    m_RightHandSpell = Instantiate(m_Spell_3);
                    m_RightHandSpell.onEquip();
                    m_RightHandSpell.setTransform(m_RightHandTransform);
                }
                break;
            case 3:
                if (m_Spell_4)
                {
                    m_RightHandSpell = Instantiate(m_Spell_4);
                    m_RightHandSpell.onEquip();
                    m_RightHandSpell.setTransform(m_RightHandTransform);
                }
                break;
        }
    }
    public void EquipLeft(int _index)
    {
        Log("Equip Left = " + _index.ToString());
        if (m_LeftHandSpell)
        {
            m_LeftHandSpell.onUnequip();
            Destroy(m_LeftHandSpell);
        }
        if (!m_LeftHandTransform)
        {
            Log("Missing Left Hand Transform");
            return;
        }
        switch (_index)
        {
            case 0:
                if (m_Spell_1)
                {
                    m_LeftHandSpell = Instantiate(m_Spell_1);
                    m_LeftHandSpell.onEquip();
                    m_LeftHandSpell.setTransform(m_LeftHandTransform);
                }
                break;
            case 1:
                if (m_Spell_2)
                {
                    m_LeftHandSpell = Instantiate(m_Spell_2);
                    m_LeftHandSpell.onEquip();
                    m_LeftHandSpell.setTransform(m_LeftHandTransform);
                }
                break;
            case 2:
                if (m_Spell_3)
                {
                    m_LeftHandSpell = Instantiate(m_Spell_3);
                    m_LeftHandSpell.onEquip();
                    m_LeftHandSpell.setTransform(m_LeftHandTransform);
                }
                break;
            case 3:
                if (m_Spell_4)
                {
                    m_LeftHandSpell = Instantiate(m_Spell_4);
                    m_LeftHandSpell.onEquip();
                    m_LeftHandSpell.setTransform(m_LeftHandTransform);
                }
                break;
        }
    }
    #endregion
    #region Private Functions
    private void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[Spell Manager][" + _msg + "]");
        }
    }
    #endregion
}
