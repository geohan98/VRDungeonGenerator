using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [Header("HealthBarController Options")]
    public Transform m_Bar;
    public Enemy m_Enemy;

    private void Update()
    {
        m_Bar.transform.localScale = new Vector3(m_Enemy.GetHealthPercent(), 1, 1);
    }
}
