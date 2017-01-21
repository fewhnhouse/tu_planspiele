using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBridgesUp : MonoBehaviour {
    public AnimationCurve MoveAnimation;
    public float AnimationTime;
    public GameObject[] ObjectsToMove;
    public Transform[] Targets;
	
    public void MoveBridges()
    {
        for (int i = 0; i < ObjectsToMove.Length; i++)
        {
            StartCoroutine(MoveObject(ObjectsToMove[i], Targets[i]));
        }
    }

    private IEnumerator MoveObject(GameObject bridge, Transform target)
    {
        bridge.SetActive(true);

        float elapsedTime = 0;
        Vector3 startPos = bridge.transform.position;


        while(elapsedTime < AnimationTime)
        {
            elapsedTime += Time.deltaTime;
            bridge.transform.position = Vector3.Lerp(startPos, target.position, MoveAnimation.Evaluate(elapsedTime / AnimationTime));

            yield return null;
        }
    }
}
