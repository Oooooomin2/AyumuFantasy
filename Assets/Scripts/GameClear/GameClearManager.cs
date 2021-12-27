using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    [SerializeField]
    Fade fade = null;

    public void GameStart()
    {
        fade.FadeIn(1.0f, () =>
        {
            SceneManager.LoadScene("GameStart");
        });
    }
}
