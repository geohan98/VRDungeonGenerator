﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Missle", menuName = "Spells/Missle")]
public class MissleSpell : Spell
{
    [Header("Missle Attributes")]
    public GameObject m_Missle;
    public float m_Speed;
    public float m_Range;

    public override void onHold()
    {
        if (Time.time - m_lastCastTime >= m_CooldownTime)
        {
            GameObject tmp = Instantiate(m_Missle, m_origin.position, m_origin.rotation, null);
            tmp.GetComponent<Projectile>().init(m_Range, m_Speed);
            m_lastCastTime = Time.time;
        }
    }

    public override void onEquip()
    {
        m_lastCastTime = Time.time;
    }
}

