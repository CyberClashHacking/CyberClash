using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaged : MonoBehaviour
{
    Vector2 dam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            Debug.Log("À¸¾Ç");
            if (Boss.Instance.leftOrRight == -1)
                dam = new Vector2(-8f, 5f);
            else
                dam = new Vector2(8f, 5f);

            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        Player.Instance.state = Player.State.Damage;
        Player.Instance.pRigidbody.velocity = Vector2.zero;
        Player.Instance.pRigidbody.AddForce(dam, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        Player.Instance.state = Player.State.Move;
    }
}
