using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x + 1f, transform.position.y);
        transform.position = new Vector3(transform.position.x - 1f, transform.position.y);
    }
}
