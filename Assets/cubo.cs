using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubo : MonoBehaviour {

    public Rigidbody m_Rig;

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log(collision.other.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Debug.Log(other.name);
    }

    // Update is called once per frame
    void Update () {
        m_Rig.AddTorque(transform.up * 3);
	}
}
