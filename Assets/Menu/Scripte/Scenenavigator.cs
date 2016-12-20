using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scenenavigator : MonoBehaviour {
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


}
