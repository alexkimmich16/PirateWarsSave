using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 15f;
    public Transform explosionPrefab;
    public GameObject cook2_sp;
    // Start is called before the first frame update
    void Start()
    {
        cook2_sp = GameObject.Find("cook2_sp");
    }
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        print("enter");
        //ContactPoint contact = collision.contacts[0];
        //Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = new Vector3(gameObject.transform.position.x, cook2_sp.transform.position.y, gameObject.transform.position.z);
        Instantiate(explosionPrefab, position, cook2_sp.transform.rotation);
        Destroy(gameObject);
    }
}
