using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ClickToMoveController : MonoBehaviour
{

    private NavMeshAgent m_NavMeshAgent;
    private Vector3 m_Pos;
    public Animation m_anim;
    public int Coins = 0;
    public Text m_CoinsText;
    public Image Vida;
    public Slider healthSlider;

    public bool m_Die = false;

    public const int m_MaxLife = 10000;
    public int characterLife;


    public void Damage()
    {
        if(characterLife > 0)
        {
            characterLife -= 10;
            float sValor = 10;
            if(healthSlider.value - sValor >= 0)
            {
                healthSlider.value -= sValor;
            }
            if(characterLife <= 0)
            {
                if (!m_Die)
                {
                    m_Die = true;
                    healthSlider.value = 0;
                    m_PlayerAnimation.Play("die");
                }
            }
        }
    }

    public Animation m_PlayerAnimation
    {
        get
        {
            if (m_anim == null)
            {
                m_anim = GetComponent<Animation>();
            }
            return m_anim;
        }
    }


    private NavMeshAgent m_Nav
    {
        get
        {
            if (m_NavMeshAgent == null)
            {
                m_NavMeshAgent = GetComponent<NavMeshAgent>();
            }
            return m_NavMeshAgent;
        }
    }

    void Awake()
    {
        //characterLife = m_MaxLife;
        //healthSlider.maxValue = m_MaxLife;
    }


    // Use this for initialization
    void Start()
    {
        m_Pos = transform.position;
        Coins = PlayerPrefs.GetInt("Coins", 0);
        this.m_CoinsText.text = Coins.ToString();
        healthSlider.maxValue = m_MaxLife;
        healthSlider.value = m_MaxLife;
        characterLife = m_MaxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Die)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray sRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit castHit;
                if (Physics.Raycast(sRay, out castHit, 1000))
                {
                    m_Pos = castHit.point;
                    m_NavMeshAgent.SetDestination(m_Pos);
                }
                Debug.Log("Click");
                Debug.Log(sRay);
            }
            Corre();
        }


    }

    public void Corre()
    {
        if (Vector3.Distance(transform.position, m_Pos) <= 1)
        {
            m_PlayerAnimation.Play("idle");
            m_Nav.Stop();
        }
        else
        {
            m_PlayerAnimation.Play("run");
            m_NavMeshAgent.Resume();
        }
    }

    public void ActualizarMonedas()
    {
        Coins++;
        PlayerPrefs.SetInt("Coins", Coins);
        this.m_CoinsText.text = Coins.ToString();
    }
}
