using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    [Header("Spell Attributes")]
    public string m_name;
    public Texture2D m_sprite;
    public string m_description;
    public float m_castRate;
    protected float m_lastCastTime;
    protected Transform m_origin;

    public virtual void onPress() { }
    public virtual void onHold() { }
    public virtual void onRelease() { }
    public virtual void onEquip() { }
    public virtual void onUnequip() { }

    public void setTransform(Transform t)
    {
        m_origin = t;
    }
}
