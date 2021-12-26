using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolveGameOver : MonoBehaviour
{
    public void BackToGameStart() => SceneManager.LoadScene("GameStart");
}
