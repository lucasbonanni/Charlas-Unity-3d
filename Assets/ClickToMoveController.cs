using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMoveController : MonoBehaviour {

    private NavMeshAgent navMeshAgent;
    private Vector3 m_Pos;
    public Animation m_anim;

    public NavMeshAgent m_navMesh
    {
        get
        {
            if(this.navMeshAgent == null)
                navMeshAgent = GetComponent<NavMeshAgent>();
            return navMeshAgent;
        }
        set { navMeshAgent = value; }
    }


    // Use this for initialization
    void Start () {
        m_Pos = transform.position;
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
                m_anim.Play("run");
                navMeshAgent.SetDestination(m_Pos);
                navMeshAgent.Resume();
            }
            Debug.Log("Click");
            Debug.Log(sRay);
        }
        Corre();
	}

    public void Corre()
    {
        if(Vector3.Distance(transform.position,m_Pos) <= 1)
        {
            m_anim.Play("idle");
            navMeshAgent.Stop();
        }
        else
        {
            m_anim.Play("run");
            navMeshAgent.Resume();
        }
    }
}
