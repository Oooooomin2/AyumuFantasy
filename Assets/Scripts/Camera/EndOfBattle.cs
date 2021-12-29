using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfBattle : MonoBehaviour
{
    public GameObject Target;

    [SerializeField]
    Fade fade = null;
    private bool isEnd;

    void Start()
    {
        isEnd = false;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position + new Vector3(0.0f, 0.5f, 1.0f), 0.02f);
        float distance = (transform.position - Target.transform.position).sqrMagnitude;
        if (distance < 1.3f && !isEnd)
        {
            isEnd = true;
            StartCoroutine(DelayCoroutine(0.5f, () =>
            {
                BackToField();
            }));
        }
    }

    private void BackToField()
    {
        fade.FadeIn(0.8f, () =>
        {
            SceneManager.LoadScene("SampleScene");
        });
    }

    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}