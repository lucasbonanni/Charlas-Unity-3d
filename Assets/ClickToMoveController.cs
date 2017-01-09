using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ClickToMoveController : MonoBehaviour {

    private NavMeshAgent m_NavMeshAgent;
    private Vector3 m_Pos;
    public Animation m_Anim;
    public int Coins = 0;
    public Text m_CoinsText;


    private NavMeshAgent m_Nav
    {
        get
        {
            if (m_NavMeshAgent == null)
                m_NavMeshAgent = GetComponent<NavMeshAgent>();
            return m_NavMeshAgent;
        }
    }


    // Use this for initialization
    void Start () {
        m_Pos = transform.position;
        Coins = PlayerPrefs.GetInt("Coins", 0);
        this.m_CoinsText.text = string.Format("Coins {0}", Coins);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            Ray sRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit castHit;
            if(Physics.Raycast(sRay,out castHit,1000))
            {
                m_Pos = castHit.point;
                m_NavMeshAgent.SetDestination(m_Pos);
            }
            Debug.Log("Click");
            Debug.Log(sRay);
        }
        Corre();
	}

    public void Corre()
    {
        if(Vector3.Distance(transform.position, m_Pos) <= 1)
        {
            m_Anim.Play("idle");
            m_Nav.Stop();
        }
        else
        {
            m_Anim.Play("run");
            m_NavMeshAgent.Resume();
        }
    }

    public void ActualizarMonedas()
    {
        Coins++;
        PlayerPrefs.SetInt("Coins", Coins);
        this.m_CoinsText.text = string.Format("Coins {0}", Coins);
    }
}
