using UnityEngine;
using System.Collections;

public class Pausenscripte : MonoBehaviour {
    public GameObject Charakter;
    public GameObject CharakterCamera;
    public GameObject Camera;
    public Canvas PauseCanvas;
    public float charakterHight;
    public Canvas reallyEndC;

    // Use this for initialization
    void Start () {
        PauseCanvas.enabled = false;
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = false;
        reallyEndC.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            PauseCanvas.enabled = !PauseCanvas.enabled;
            if (PauseCanvas.enabled)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Time.timeScale = 0;
                Camera.transform.position = CharakterCamera.transform.position;
                Camera.transform.rotation = CharakterCamera.transform.rotation;
                Charakter.SetActive(false);
                
                Camera.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Charakter.SetActive(true);
                Camera.SetActive(false);
                Cursor.visible = false;
            }
            

        }
	}

    public void  continueGame()
    {
        PauseCanvas.enabled = !PauseCanvas.enabled;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Charakter.SetActive(true);
        Camera.SetActive(false);
    }
    public void reallyEnd()
    {
        reallyEndC.enabled = true;
    }
    public void notEnd()
    {
        reallyEndC.enabled = false;
    }

}
