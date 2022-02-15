using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    // public ParticleSystem part;
    // public List<ParticleCollisionEvent> collisionEvents;
    public int life;
    int current_life;
    void Start()
    {
        //life = 10;
        m_Animator = gameObject.GetComponent<Animator>();
        current_life = life;
        // part = GetComponent<ParticleSystem>();
        // collisionEvents = new List<ParticleCollisionEvent>();
    }
    void OnParticleCollision(GameObject other)
    {
        print(other.name);
        m_Animator.SetTrigger("hit");
        current_life --;
        if(current_life == 0){
            m_Animator.SetTrigger("dead");
            current_life = life;
            gameObject.GetComponent<Collider>().enabled = false;
            //gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        // int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        // Rigidbody rb = other.GetComponent<Rigidbody>();
        // int i = 0;

        // while (i < numCollisionEvents)
        // {
        //     if (rb)
        //     {
        //         Vector3 pos = collisionEvents[i].intersection;
        //         Vector3 force = collisionEvents[i].velocity * 10;
        //         rb.AddForce(force);
        //     }
        //     i++;
        // }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
