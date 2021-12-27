using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolveGameStart : MonoBehaviour
{
    public void GameStart() => SceneManager.LoadScene("Opening");
}
