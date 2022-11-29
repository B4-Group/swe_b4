using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    bool onof = false;
    public Camera c;

    void start()
    {
        c.enabled = true;
        c.orthographic = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            onof = !onof;
        }
        if (onof)
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
            c.orthographicSize = 7.5f;
        }
        else
        {

        }
    }
}
