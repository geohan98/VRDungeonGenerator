using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Missle", menuName = "Spells/Missle")]
public class Missle : Spell
{
    [Header("Missle Attributes")]
    public GameObject m_missle;
    public float m_damage;
    public float m_speed;
    public float m_lifetime;


    public override void onPress() { }
    public override void onHold()
    {
        Debug.Log(m_name + "Hold");
        if (Time.time - m_lastCastTime >= m_castRate)
        {
            GameObject tmp = Instantiate(m_missle, m_origin.position, m_origin.rotation, null);
            m_lastCastTime = Time.time;
        }
    }
    public override void onRelease()
    {

    }
    public override void onEquip() { }
    public override void onUnequip() { }
}

