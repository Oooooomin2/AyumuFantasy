using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    [SerializeField]
    Fade fade = null;

    public void MoveToSampleScene()
    {
        FieldManager.PlayerFieldLocation = new Vector3(100, 0, 100);
        fade.FadeIn(1.0f, () =>
        {
            SceneManager.LoadScene("SampleScene");
        });
    }
}
