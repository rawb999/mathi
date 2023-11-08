using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBarrier : MonoBehaviour
{
    public bool isCollected = false;

    //public GameObject eventSystem;
    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected)
        {
            isCollected = true;
            // Disable this barrier.
            this.gameObject.SetActive(false);
            collectableControl.scoreCount -= 30;
            fight.startFight();
            
        }
    }
    
}
