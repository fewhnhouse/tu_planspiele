using UnityEngine;

public class SkullActivatable : MonoBehaviour
{
    public bool Active;
    public float yMoveDistance;

    private Vector3 startPosition;
    //TODO fix bug with more skull where the startposition is set to 0 before Start()
    void Awake()
    {
        startPosition = transform.position;
    }

    public void SetActive(bool active)
    {

        if (active)
        {
            transform.position = startPosition + new Vector3(0, yMoveDistance, 0);
        }
        else
        {
            transform.position = startPosition;
        }

        Active = active;
    }
}
