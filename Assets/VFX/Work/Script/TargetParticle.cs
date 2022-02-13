using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetParticle : MonoBehaviour
{
    public float speed = 15f;
    public GameObject hit;
    public GameObject flash;
    public bool LocalRotation = false;
    public Transform target;
    private Vector3 targetOffset;
    public string target_name;
    public float sideAngle = 0;
    public float upAngle = 0;

    void Start()
    {
        FlashEffect();
        target = GameObject.Find(target_name).GetComponent<Transform>();
        UpdateTarget(target.transform, Vector3.zero);
    }

    public void UpdateTarget(Transform targetPosition , Vector3 Offset)
    {
        target = targetPosition;
        targetOffset = Offset;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 forward = ((target.position + targetOffset) - transform.position);
        Vector3 crossDirection = Vector3.Cross(forward, Vector3.up);
        Quaternion randomDeltaRotation = Quaternion.Euler(0, 0, 0) * Quaternion.AngleAxis(upAngle, crossDirection);
        Vector3 direction = randomDeltaRotation * ((target.position + targetOffset) - transform.position);

        float distanceThisFrame = Time.deltaTime * speed;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            Destroy(gameObject);
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void FlashEffect()
    {
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
    }

    void HitTarget()
    {
        if (hit != null)
        {
            var hitInstance = Instantiate(hit, transform.position, transform.rotation);
            // var hitRotation = transform.rotation;
            // if (LocalRotation == true)
            // {
            //     hitRotation = Quaternion.Euler(0, 0, 0);
            // }
            // var hitInstance = Instantiate(hit, transform.position, transform.rotation);
            // var hitPs = hitInstance.GetComponent<ParticleSystem>();
            // if (hitPs != null)
            // {
            //     Destroy(hitInstance, hitPs.main.duration);
            // }
            // else
            // {
            //     var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
            //     Destroy(hitInstance, hitPsParts.main.duration);
            // }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        //print(other.name + "i am particle");
        target = null;
        HitTarget();
    }
}
