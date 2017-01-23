using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class LoadNewSceneTrigger : MonoBehaviour {
    public string NewScene;

    void OnTriggerEnter(Collider collider)
    {
        SceneManager.LoadScene(NewScene);
    }
}
