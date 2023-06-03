using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputVec { get; private set; } // 입력받은 Vector2 값

    private readonly float MAX_ATTACK_DELAY = 0.8f;
    private readonly float MIN_ATTACK_DELAY = 0.4f;
    private readonly Vector3 LEFT = new Vector3(0f, 180f, 0f);
    private readonly Vector3 RIGHT = new Vector3(0f, 0f, 0f);

    //private enum _BasicAttackComboType { combo1, combo2, combo3 }
    //private _BasicAttackComboType _currentBasicCombo = _BasicAttackComboType.combo1;

    //protected enum PlayerState { move, attack }
    //protected PlayerState playerState;

    private int _jumpCount = 0; // 점프 카운트

    //private float _attackDelayCount = 0f;

    private bool _isJump = false; // Jump Animation 유무 확인
    private bool _isRun = false; // Run Animation 유무 확인
    private bool _isAttack = false;
    private bool _isAttackCall = false;

    public GameObject p_attack1_prefab;



    //private void Update()
    //{
    //    _attackDelayCount += Time.deltaTime;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트의 태그가 Platform일 경우..
        if (collision.gameObject.tag == "Platform")
        {
            //Debug.Log("enter");
            // 충돌한 오브젝트의 노말벡터 값이 0.7 이상인 경우 윗면이라고 판단
            if (collision.contacts[0].normal.y > 0.7f)
            {
                //Debug.Log("enter_top");
                _isJump = false;
                _jumpCount = 0;
                Player.Instance.animator.SetBool("isJump", _isJump);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            Debug.Log("exit");
            _isJump = true;
            Player.Instance.animator.SetBool("isJump", _isJump);
        }
    }

    void OnMove(InputValue value)
    {
        // 입력받은 Vector 값 가져옴
        inputVec = value.Get<Vector2>() * Player.Instance.moveSpeed;

        // 입력받은 Vector2의 x 값이 0보다 작으면 Player Object의 y 축을 180도로 변환
        if (inputVec.x < 0)
        {
            transform.eulerAngles = LEFT;
        }
        else if (inputVec.x > 0)
        {
            transform.eulerAngles = RIGHT;
        }

        if (inputVec.x != 0) _isRun = true;
        else _isRun = false;

        // isRun 값에 따라 Animation Setting
        Player.Instance.animator.SetBool("isRun", _isRun);
    }

    void OnJump()
    {
        // Player가 jump 상태 일 경우에만 점프 카운트를 셈
        if(_isJump && _jumpCount < Player.Instance.maxJumpCount)
        {
            _jumpCount++;
        }

        if(_jumpCount < Player.Instance.maxJumpCount)
        {
            Player.Instance.pRigidbody.velocity = new Vector2(Player.Instance.pRigidbody.velocity.x, 0);
            // Jump키 입력 시 순간적으로 y축에 힘을 가함
            Player.Instance.pRigidbody.AddForce(Vector2.up * Player.Instance.jumpPower, ForceMode2D.Impulse);
        }

    }

    void OnBasicAttack()
    {
        if (!_isAttack)
        {
            _isAttack = true;
            StopCoroutine(BasicAttack());
            StartCoroutine(BasicAttack());
        }

    }

    IEnumerator BasicAttack()
    {
        Player.Instance.animator.SetTrigger("BasicAttack1");
        float xPlus = (transform.rotation.y < 0) ? -1 : 1;
        Vector3 p_attack1_position = new Vector3(transform.position.x+xPlus, transform.position.y-0.4f);
        GameObject p_attack1 = Instantiate(p_attack1_prefab, p_attack1_position, transform.rotation);
        yield return new WaitForSeconds(MIN_ATTACK_DELAY);
        Destroy(p_attack1);
        _isAttack = false;
    }
}
