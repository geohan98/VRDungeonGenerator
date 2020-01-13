using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    [Header("Spell Attributes")]
    public string m_Name;
    public Texture2D m_Sprite;
    public string m_Description;
    public float m_CastRate;
    protected float m_LastCastTime;

    public virtual void onPress() { }
    public virtual void onHold() { }
    public virtual void onRelease() { }
    public virtual void onEquip() { }
    public virtual void onUnequip() { }
}
