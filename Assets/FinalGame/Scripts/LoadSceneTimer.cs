using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTimer : MonoBehaviour {
    public string NewScene;
    public float Delay;

    private bool TimerStarted;
    // Use this for initialization
    public void StartTimer()
    {
        if (!TimerStarted)
        {
            TimerStarted = true;
            StartCoroutine(timer(Delay));
        }
    }

    private IEnumerator timer(float Delay)
    {
        yield return new WaitForSeconds(Delay);

        SceneManager.LoadScene(NewScene);
    }
}
