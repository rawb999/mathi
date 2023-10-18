using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3;
    public float strafeSpeed = 4;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World); //moves the character forward

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //if player is pressing left key, move left
        {
            if (this.gameObject.transform.position.x > levelBoundary.leftSide) //if player is not heading outside the boundary
            {
                transform.Translate(Vector3.left * Time.deltaTime * strafeSpeed);
            }
            
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //if player is pressing right key, move right
        {
            if (this.gameObject.transform.position.x < levelBoundary.rightSide) //if player is not heading outside the boundary
            {
                transform.Translate(Vector3.left * Time.deltaTime * strafeSpeed * -1);
            }
        }
    }
}
