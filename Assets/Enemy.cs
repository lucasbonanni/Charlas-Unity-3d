using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float m_stop_distance = 2f;

    private Animation m_Anim;

    public Animation SkeletonAnimation
    {
        get
        {
            if(m_Anim == null)
            {
                m_Anim = GetComponent<Animation>();
            }
            return m_Anim;
        }
    }


    private NavMeshAgent m_NavMeshAgent;

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
        SkeletonAnimation.Play("idle");
    }
	
	// Update is called once per frame
	void Update () {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 sPosPlayer = GameObject.FindGameObjectWithTag("Player").transform.position;
            if (Vector3.Distance(transform.position, sPosPlayer) <= m_stop_distance)
            {
                transform.LookAt(sPosPlayer);
                SkeletonAnimation.Play("attack");
                m_Nav.Stop();
            }
            else
            {
                m_Nav.SetDestination(sPosPlayer);
                SkeletonAnimation.Play("run");
                m_NavMeshAgent.Resume();
            }

            m_Nav.Resume();
        }



    }
}
