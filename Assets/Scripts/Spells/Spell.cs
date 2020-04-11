using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    [Header("Spell Attributes")]
    public float m_castRate;
    protected float m_lastCastTime;
    protected Transform m_origin;
    public virtual void onPress() { }
    public virtual void onHold() { }
    public virtual void onRelease() { }
    public virtual void onEquip() { }
    public virtual void onUnequip() { }
    public void setTransform(Transform _transform)
    {
        m_origin = _transform;
    }
}
