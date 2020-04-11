using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArrowEvents : MonoBehaviour
{
    [Header("HideArrowEvents Options")]
    public GameObject m_Arrow;
    public UnityEvent m_Fire = new UnityEvent();

    public void ShowArrow()
    {
        if (m_Arrow) m_Arrow.SetActive(true);
    }

    public void HideArrow()
    {
        if (m_Arrow) m_Arrow.SetActive(false);
        m_Fire.Invoke();
    }
}
