using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
    
{
    public Animator soldierAnimation;
    public static bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        soldierAnimation = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            soldierAnimation.SetBool("death", true);
        }
    }
}
