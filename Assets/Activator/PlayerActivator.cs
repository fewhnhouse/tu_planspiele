using UnityEngine;
using System.Collections;

public class PlayerActivator : MonoBehaviour {
    //Maximum distance the player is allowed to activate things
    public float MaxDistance = 5;

    //Reticle to use
    public bool CrosshairOn = true;
    public Texture2D crosshairTexture;

    void Update () {
        //When player left clicks
	    if(Input.GetMouseButtonDown(0))
        {
            //Raycast info 
            RaycastHit hit;
            //it is important that the camera of the fps controller is tagged as: "MainCamera"
            Vector3 origin = Camera.main.transform.position;
            //we take the blue axis arrow (z-axis) as the direction we shoot the raycast into
            Vector3 direction = Camera.main.transform.forward;

            //Shoot a ray from camera along its z-axis
            if(Physics.Raycast(origin, direction, out hit, MaxDistance))
            {
                //The gameobject that was hit
                GameObject g = hit.collider.gameObject;
                if(g)
                {
                    //The activatable script that is attached to this gameobject
                    Activatable a = g.GetComponent<Activatable>();
                    if(a != null)
                    {
                        //if the object has a Activatable script, execute this
                        a.Activate();
                    }
                }
            }
        }
	}

    private bool DebugMessage = false;
    void OnGUI()
    {
        if (CrosshairOn)
        {
            if (crosshairTexture != null)
            {
                //determine position for crosshair every frame
                Rect position = new Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height - crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
                GUI.DrawTexture(position, crosshairTexture);
            }
            else if (!DebugMessage)
            {
                Debug.LogError("CrosshairTexture in PlayerActivator has not been assigned!");
                DebugMessage = true;
            }
               
        }
    }
}
