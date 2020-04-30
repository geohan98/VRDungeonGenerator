using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController s_Instance;
    public int m_EnemyCount;

    private void Awake()
    {
        if (!s_Instance) s_Instance = this;
        m_EnemyCount = FindObjectsOfType<Enemy>().Length;
    }

    private void Update()
    {
        if (PlayerManager.s_Instance.m_Alive == false)
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.Die();
            }
            StartCoroutine(LoadMainScene());
        }
        if (m_EnemyCount <= 0)
        {
            StartCoroutine(LoadMainScene());
        }
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MenuScene");
    }
}
