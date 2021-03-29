using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPos : MonoBehaviour
{
    public Transform player;


    public bool startPos = true;

    public float y;
    Vector3 offset;

    public TrailRenderer trailRenderer;

    void Start()
    {
        offset = transform.position;
    }

    private void Update()
    {
        Vector3 desiredPosition = player.position + offset;
        if (startPos)
            desiredPosition.y = player.transform.position.y - 1.3f;
        else
            desiredPosition.y = y;
        transform.position = desiredPosition;

        trailRenderer.startWidth = player.transform.localScale.x / 2.5f;
    }
}
