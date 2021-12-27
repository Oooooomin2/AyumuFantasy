using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    [SerializeField]
    Fade fade = null;

    public void MoveToSampleScene()
    {
        fade.FadeIn(1.0f, () =>
        {
            SceneManager.LoadScene("SampleScene");
        });
    }
}
