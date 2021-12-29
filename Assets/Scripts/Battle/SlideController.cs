using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    Slider playerSlider;
    public GameObject PlayerPanel;

    void Start()
    {
        playerSlider = GetComponent<Slider>();
        playerSlider.value = Random.Range(0.0f, 1.0f);
        PlayerPanel.SetActive(false);
    }

    
    private void FixedUpdate()
    {
        if (BattleManager.IsDuringMotion) return;

        playerSlider.value += 0.004f;
        if(playerSlider.value == 1 && !AttackButtonController.HasPushButton)
        {
            PlayerPanel.SetActive(true);
        }
    }
}
