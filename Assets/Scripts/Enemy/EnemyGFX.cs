using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{

    public float interval = 3.0f;
    public float trackedTime = 0.0f;
    public AIPath aiPath;
    private float distanceToPlayer;
    // Update is called once per frame
    void Update()
    {
        trackedTime += Time.deltaTime;
        if(trackedTime >= interval)
        {
            float sound_nr = Random.Range(0, 2);
            distanceToPlayer = Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position);
            if(distanceToPlayer < 7)
            {
                switch (sound_nr)
                {
                    case 0:
                        FindObjectOfType<AudioManager>().Play("mummy1");
                        break;
                    case 1:
                        FindObjectOfType<AudioManager>().Play("mummy2");
                        break;
                    case 2:
                        FindObjectOfType<AudioManager>().Play("mummy3");
                        break;
                }
            }
            trackedTime = 0.0f;
            interval = Random.Range(3.0f, 8.0f);
        }
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

        }else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
         
        
    
    }
    
    
    
}
