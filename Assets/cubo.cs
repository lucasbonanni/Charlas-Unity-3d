using UnityEngine;

public class cubo : MonoBehaviour {

    public Rigidbody m_Rig;

	// Use this for initialization
	void Start () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        ClickToMoveController sPlayer = null;
        if (other.gameObject.tag.ToLower() == "player")
            sPlayer = other.gameObject.GetComponent<ClickToMoveController>();

        sPlayer.ActualizarMonedas();
        Destroy(gameObject);
        Debug.Log(other.name);
    }

    // Update is called once per frame
    void Update () {
        m_Rig.AddTorque(transform.up * 3);
	}
}
