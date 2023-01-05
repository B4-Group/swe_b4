using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/*Trapsystem: 
 * If Collision with Object "Player" send damage
 * 
 * Author: Amier
 * Status: in progress
 * Date: 27.12
 */
public class Trap : MonoBehaviour
{
       
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject cGO = collision.gameObject;
        if (cGO.name == "Player") 
        {
            if (cGO.GetComponent<PlayerHealth>() != null)
            {
                cGO.GetComponent<PlayerHealth>().SendDamage(1); // Give damage to player from trap
                print("hit![Trap]");                                            //Console debug
            }
        }
    }
}
