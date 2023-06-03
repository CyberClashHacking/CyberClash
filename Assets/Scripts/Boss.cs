using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private readonly Vector3 RIGHT = new Vector3(0f, 180f, 0f);
    private readonly Vector3 LEFT = new Vector3(0f, 0f, 0f);

    enum BossType { Idle, Walk, Dash, Attack}
    BossType bossType;

    public static Boss Instance { get { return instance; } }
    public Rigidbody2D bRigidbody { get; private set; }
    public Animator animator { get; private set; }

    private static Boss instance;
    public int leftOrRight { get; private set; }
    

    [SerializeField] public float maxHp;
    [SerializeField] public float currentHp;
    [SerializeField] public float moveSpeed;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            bRigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }

    void FixedUpdate()
    {
        Move();
    }

    void LookAtPlayer()
    {
        if(Player.Instance.pRigidbody.position.x < Instance.bRigidbody.position.x)
        {
            Instance.transform.eulerAngles = LEFT;
            leftOrRight = -1;
        }
        else
        {
            Instance.transform.eulerAngles = RIGHT;
            leftOrRight = 1;
        }
    }

    void Move()
    {
        Instance.animator.SetBool("isWalk", true);
        Instance.bRigidbody.velocity = new Vector2(leftOrRight * moveSpeed, Instance.bRigidbody.velocity.y);
    }

    void Stop()
    {

    } 
}
