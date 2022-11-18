using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) //Methode von Unity
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.name == "Player")
        {
            //if (collisionGameObject.GetComponent<PlayerHeath>() != null)
            //{
            //    collisionGameObject.GetComponent<PlayerHeath>().SendDamage(10); // damage uebergabe an den Player 
             print("hit![Enemy]"); //consol debug
            //}
        }
    }
}
