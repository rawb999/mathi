using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public static bool inFight = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inFight)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        
    }
}
