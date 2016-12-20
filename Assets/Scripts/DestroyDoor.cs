using UnityEngine;
using System.Collections;

public class DestroyDoor : MonoBehaviour {

    void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.tag == "door")
        {
            //Destroy(hit.gameObject);
            WheelData.Instance.doorMoving = false;
            hit.gameObject.transform.Translate(Vector3.down * 0.0f, Space.World);
        }
    }
}
