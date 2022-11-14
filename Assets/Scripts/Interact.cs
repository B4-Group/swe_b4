using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Collider2D interactCollider;
    
    // Start is called before the first frame update
    private void Start()
    {
       
    }
    public void doInteract()
    {
        interactCollider.enabled = true;
    }
    public void stopInteract()
    {
        interactCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("try to interact(Interact)");
        if (other.tag == "PuzzelObject")
        {
            print("try to enter puzzel(Interact)");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
