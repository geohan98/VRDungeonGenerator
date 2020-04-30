using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchEvents : MonoBehaviour
{
    public MeleeEnemy m_Enemy;
    public void LandPunch()
    {
        if (m_Enemy) m_Enemy.LandPunch();
    }
}
