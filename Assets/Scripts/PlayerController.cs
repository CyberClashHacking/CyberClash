using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputVec { get; private set; } // �Է¹��� Vector2 ��

    private readonly float MAX_ATTACK_DELAY = 0.8f;
    private readonly float MIN_ATTACK_DELAY = 0.4f;
    private readonly Vector3 LEFT = new Vector3(0f, 180f, 0f);
    private readonly Vector3 RIGHT = new Vector3(0f, 0f, 0f);

    //private enum _BasicAttackComboType { combo1, combo2, combo3 }
    //private _BasicAttackComboType _currentBasicCombo = _BasicAttackComboType.combo1;

    //protected enum PlayerState { move, attack }
    //protected PlayerState playerState;

    private int _jumpCount = 0; // ���� ī��Ʈ

    //private float _attackDelayCount = 0f;

    private bool _isJump = false; // Jump Animation ���� Ȯ��
    private bool _isRun = false; // Run Animation ���� Ȯ��
    private bool _isAttack = false;
    private bool _isAttackCall = false;

    public GameObject p_attack1_prefab;



    //private void Update()
    //{
    //    _attackDelayCount += Time.deltaTime;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� Platform�� ���..
        if (collision.gameObject.tag == "Platform")
        {
            //Debug.Log("enter");
            // �浹�� ������Ʈ�� �븻���� ���� 0.7 �̻��� ��� �����̶�� �Ǵ�
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
        // �Է¹��� Vector �� ������
        inputVec = value.Get<Vector2>() * Player.Instance.moveSpeed;

        // �Է¹��� Vector2�� x ���� 0���� ������ Player Object�� y ���� 180���� ��ȯ
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

        // isRun ���� ���� Animation Setting
        Player.Instance.animator.SetBool("isRun", _isRun);
    }

    void OnJump()
    {
        // Player�� jump ���� �� ��쿡�� ���� ī��Ʈ�� ��
        if(_isJump && _jumpCount < Player.Instance.maxJumpCount)
        {
            _jumpCount++;
        }

        if(_jumpCount < Player.Instance.maxJumpCount)
        {
            Player.Instance.pRigidbody.velocity = new Vector2(Player.Instance.pRigidbody.velocity.x, 0);
            // JumpŰ �Է� �� ���������� y�࿡ ���� ����
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
