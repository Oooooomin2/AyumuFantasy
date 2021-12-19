using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    Slider playerSlider;
    public GameObject playerPanel;

    void Start()
    {
        playerSlider = GetComponent<Slider>();
        playerSlider.value = Random.Range(0.0f, 1.0f);
        playerPanel.SetActive(false);
    }

    
    void Update()
    {
        playerSlider.value += 0.002f;
        if(playerSlider.value == 1 && !AttackButtonController.hasPushButton)
        {
            playerPanel.SetActive(true);
        }
    }
}
