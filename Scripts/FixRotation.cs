using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    public Transform player;

    //float cameraFollowSpeed = 20f;

    public float y;

    Vector3 offset;

    void Start()
    {
        offset = transform.position;
    }

    private void Update()
    {
        Vector3 desiredPosition = player.position + offset;
        desiredPosition.z -= player.localScale.x / 2;
        desiredPosition.y = player.position.y;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraFollowSpeed * Time.deltaTime);
        transform.position = desiredPosition;
    }
}
