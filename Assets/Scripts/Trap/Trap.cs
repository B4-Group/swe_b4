using System.Collections;
using System.Collections.Generic;
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
    public IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        GameObject cGO = collision.gameObject;
        if (cGO.name == "Player") 
        {
            if (cGO.GetComponent<PlayerHealth>() != null)
            {
                yield return new WaitForSeconds(0.3f);
                cGO.GetComponent<PlayerHealth>().SendDamage(1); // Give damage to player from trap
                
            }
        }
    }
}