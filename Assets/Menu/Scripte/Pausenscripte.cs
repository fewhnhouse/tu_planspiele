using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;


public class Pausenscripte : MonoBehaviour
{
    // Charakter as well as the CharakterCamera
    public GameObject Charakter;
    public GameObject CharakterCamera;
    // The Camera that looks at the Menu
    public GameObject Camera;
    // The canvas of the Paused game
    public Canvas PauseCanvas;
    // The Canvas that opens if you press end game
    public Canvas reallyEndC;
    // Opens if the game was saved
    public Canvas savedC;

    // The Scripte handels the pausing and continuing of the scene as well as saving and loading

    // Use this for initialization


    void Start()
    {
        PauseCanvas.enabled = false;
        savedC.enabled = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        reallyEndC.enabled = false;
        Camera.SetActive(false);
        Charakter.SetActive(true);
        Time.timeScale = 1;
        setInteractiable(true,PauseCanvas.GetComponentsInChildren<Button>());

        // Laden teil
        if (Loadscripte.load.getLoaded())
        {
            Charakter.transform.position = Loadscripte.load.getPos();
            Gamemaster gm = new Gamemaster();
            if((bool)Loadscripte.load.finishedLevels[0])
            {
             //   gm.Finished();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (!PauseCanvas.enabled)
            {
                PauseCanvas.enabled = !PauseCanvas.enabled;
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
                continueGame();
            }
        }

    }

    public void continueGame()
    {
        setInteractiable(true, PauseCanvas.GetComponentsInChildren<Button>());
        PauseCanvas.enabled = !PauseCanvas.enabled;
        Time.timeScale = 1;
        //Cursor.lockState = CursorLockMode.Locked;
        Charakter.SetActive(true);
        Camera.SetActive(false);
        reallyEndC.enabled = false;
        savedC.enabled = false;
        
        
    }
    // Is Called if you end the game to check if you really want to end
    public void reallyEnd()
    {
        reallyEndC.enabled = true;
        setInteractiable(false, PauseCanvas.GetComponentsInChildren<Button>());

    }
    // Is Called if you don´t really want to end
    public void notEnd()
    {
        setInteractiable(true, PauseCanvas.GetComponentsInChildren<Button>());
        reallyEndC.enabled = false;
    }
    // Goes back to the Menu after pressing yes
    public void backToMainMenu()
    {
        Loadscripte.load.setLoaded(false);
        SceneManager.LoadScene("Start Menu");
    }

    private void setInteractiable(bool b, Button[] Button)
    {
        for(int i =0; i<Button.Length;i++)
        {
            Button[i].interactable = b;
        }
    }

    // Save part

    // Saves the players position and rotation into a file called playerProgress.dat
    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerProgress.dat");
        playerProgress plPro = new playerProgress();
        plPro.pos = vecToArray(Charakter.transform.position);
        plPro.progress = listToArray(Loadscripte.load.finishedLevels);
        bf.Serialize(file, plPro);
        file.Close();

    }
    private bool[] listToArray(ArrayList a)
    {
        bool[] b = new bool[3];
        for (int i = 0; i < 3; i++)
        {
            b[i] =(bool) a[i];
        }
        return b;
    }
    

    // Because only primary datatypes can be serialysed, dies Methodes convert Vec to an array an back
    private float[] vecToArray(Vector3 vec)
    {
        float[] f = new float[3];
        f[0] = vec.x;
        f[1] = vec.y;
        f[2] = vec.z;
        return f;
    }
   
    // Saves the game and opens the Field to press ok
    public void openSave()
    {
        Save();
        savedC.enabled = true;
        setInteractiable(false, PauseCanvas.GetComponentsInChildren<Button>());
    }

    public void continueAfterSave()
    {
        savedC.enabled = false;
        setInteractiable(true, PauseCanvas.GetComponentsInChildren<Button>());
    }

    // Because only primary datatypes can be serialysed, dies Methodes convert Quaternion to an array an back
    /*
        public float[] quadToArray(Quaternion Quat)
        {
            float[] f = new float[4];
            f[0] = Quat.x;
            f[1] = Quat.y;
            f[2] = Quat.z;
            f[3] = Quat.w;
            return f;
        }
        public Quaternion arrayToQuat(float[] f)
        {
            return new Quaternion(f[0], f[1], f[2],f[3]);
        }*/
}
[System.Serializable]
class playerProgress
{
    public float[] pos = new float[3];
    // Saves which levels have been finished
    public bool[] progress = new bool[3];
    // public float[] rot = new float[3];
}

