using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvents : MonoBehaviour
{
    public void GameQuit() { Application.Quit(); }
    public void LoadMainScene() { SceneManager.LoadScene("MenuScene"); }
    public void LoadGameScene() { SceneManager.LoadScene("GameScene"); }
}
