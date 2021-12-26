using UnityEngine;

public class BattleFadeManager : MonoBehaviour
{
    [SerializeField]
    Fade fade = null;

    void Start()
    {
        fade.cutoutRange = 1;
        fade.FadeOut(0.7f);
    }
}
