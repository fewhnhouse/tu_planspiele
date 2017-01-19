using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Scenenavigator : MonoBehaviour
{
    public Pausenscripte sPause;
    public Texture2D cursorTexture;

    // Update is called once per frame
    public void Awake()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void switchSzene(string szene)
    {
        SceneManager.LoadScene(szene);
    }
    public void leaveGame()
    {
        Application.Quit();
    }
    public void ContinueGame()
    {
        sPause.continueGame();
    }
    public void loading(string scene)
    {
        if (File.Exists(Application.persistentDataPath + "/playerProgress.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerProgress.dat", FileMode.Open);
            playerProgress data = (playerProgress)bf.Deserialize(file);
            Loadscripte.load.setPos(arrayToVec(data.pos));
            file.Close();
            Loadscripte.load.setLoaded(true);
            SceneManager.LoadScene(scene);
            
        }
    }
    private Vector3 arrayToVec(float[] f)
    {
        return new Vector3(f[0], f[1], f[2]);
    }


}