using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 *edit: 29.11 Nam
 */
public class EnemyController : MonoBehaviour
{
    public Animator animator;

    public IEnumerator OnCollisionEnter2D(Collision2D collision) //Methode von Unity
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.name == "Player")
        {
            if (collisionGameObject.GetComponent<PlayerHealth>() != null)
            {
                animator.SetTrigger("hitting");
                yield return new WaitForSeconds(1);
                collisionGameObject.GetComponent<PlayerHealth>().SendDamage(1); // damage uebergabe an den Player

            }
        }
    }

}
