using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float m_stop_distance = 2f;

    private Animation m_Anim;

    private Animator m_Animator;

    private ClickToMoveController m_Player;

    public Animator characterAnimator
    {
        get
        {
            if (m_Animator == null)
            {
                m_Animator = GetComponent<Animator>();
            }
            return m_Animator;
        }
    }


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
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<ClickToMoveController>();
        if (SkeletonAnimation != null)
        {
            SkeletonAnimation.Play("idle");
        }
        if(characterAnimator != null)
        {
            characterAnimator.SetInteger("Movimiento", 0);
        }
    }
	
    public void Run()
    {
        SetMovimiento(1);
    }

    public void Idle()
    {
        SetMovimiento(1);
    }


    void SetMovimiento(int xValor)
    {
        if (characterAnimator != null)
            characterAnimator.SetInteger("Movimiento", xValor);
    }

    void Ataca(bool valor)
    {
        if(characterAnimator != null)
        {
            characterAnimator.SetBool("Ataca", valor);
        }
    }

    void OnAttack()
    {
        Debug.Log("Ataca");
    }

	// Update is called once per frame
	void Update () {
        
        if (m_Player != null)
        {
            Vector3 sPosPlayer = GameObject.FindGameObjectWithTag("Player").transform.position;
            if (Vector3.Distance(transform.position, sPosPlayer) <= m_stop_distance)
            {
                
                if (m_Anim != null)
                    SkeletonAnimation.Play("attack");
                Idle();
                Ataca(true);
                m_Player.Damage();
                transform.LookAt(sPosPlayer);
                m_Nav.Stop();
            }
            else
            {
                m_Nav.SetDestination(sPosPlayer);
                if (SkeletonAnimation != null)
                    SkeletonAnimation.Play("run");
                Ataca(false);
                Run();
                m_NavMeshAgent.Resume();
            }

            m_Nav.Resume();
        }



    }
}
