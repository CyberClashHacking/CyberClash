using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        Move,
        Attack
    }

    public State state { get; set; }

    public static Player Instance { get { return instance; } }
    public Rigidbody2D pRigidbody { get; private set; }
    public Animator animator { get; private set; }
    public PlayerController controller { get; private set; }

    private static Player instance;

    [Header("Player Statement")]
    [SerializeField] public float maxHp;
    [SerializeField] public float currentHp;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpPower;
    [SerializeField] public int maxJumpCount;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            animator = GetComponent<Animator>();
            pRigidbody = GetComponent<Rigidbody2D>();
            controller = GetComponent<PlayerController>();

            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);
    }

    private void Start()
    {
        InitStateController();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        // 플레이어 속도에 입력받은 x 값과 원래 가지고 있던 y값을 넣어줌
        Instance.pRigidbody.velocity = new Vector2(controller.inputVec.x, Instance.pRigidbody.velocity.y);
    }

    public void UpdateStat(float _maxHp, float _currentHp, float _moveSpeed, float _jumpPower, int _maxJumpCount)
    {
        this.maxHp = _maxHp;
        this.currentHp = _currentHp;
        this.moveSpeed = _moveSpeed;
        this.jumpPower = _jumpPower;
        this.maxJumpCount = _maxJumpCount;
    }

    void InitStateController()
    {

    }
}
