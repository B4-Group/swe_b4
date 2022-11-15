using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Interact interact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnFire()
    {
        animator.SetTrigger("interact");
    }
    public void startInteract()
    {
        interact.doInteract();
        print("try to interact(PlayerController)");
    }
    public void endInteract()
    {
        interact.stopInteract();
    }
}
