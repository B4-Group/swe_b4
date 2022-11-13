using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;

    public Rigidbody2D rb;

    [SerializeField]
    private float testHello = 2f; 

    public Animator animator;

    bool facingRight = true;

    Vector2 movement;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(movement.x == -1) {
            facingRight = false;
        } else if(movement.x == 1) {
            facingRight = true;
        }

        transform.localScale = new Vector3((float)(movement.x != 0 ? movement.x * 0.25 : facingRight ? 0.25 : -0.25), transform.localScale.y, transform.localScale.z);
        
    }

    private void FixedUpdate() {
        // Movement     
        rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * movement);
    }
}
