using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Missle", menuName = "Spells/Missle")]
public class Missle : Spell
{
    [Header("Missle Attributes")]
    public GameObject m_Missle;
    public float m_Damage;
    public float m_Speed;
    public float m_Lifetime;
    public float m_CastRate;
    float m_LastCastTime;

    public override void onPress() { }
    public override void onHold() { }
    public override void onRelease() { }
    public override void onEquip() { }
    public override void onUnequip() { }
}

