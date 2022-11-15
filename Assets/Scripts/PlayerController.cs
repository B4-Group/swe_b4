using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Autor: Nam
 *  Dieser Script kann reintheoretisch gelöscht werden, Onfire kann man in Shooting implementieren ~ Nam
 */

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Camera cam;


    public Rigidbody2D rb;
    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
   
    }
    private void FixedUpdate()
    {
       
    }
    void OnFire() // nur animation
    {
        animator.SetTrigger("interact");
        print("fire![Controller]");
    }
}
