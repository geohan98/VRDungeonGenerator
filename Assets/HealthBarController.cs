using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [Header("HealthBarController Options")]
    public Transform m_Bar;
    //public Pawn m_Pawn;

    private void Update()
    {
        //m_Bar.transform.localScale = new Vector3(m_Pawn.GetHealthPercent(), 1, 1);
    }
}
