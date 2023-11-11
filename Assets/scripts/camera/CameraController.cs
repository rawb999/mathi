using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public static bool inFight = false;
    public static int row = 0;
    private int recentRow = 0;
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

        if (row != recentRow) // if the row got updated
        {
            float rowDiff = (float)row - (float)recentRow;
            transform.position += new Vector3(0, 0, -rowDiff);
            recentRow = row;
        }

    }
}
