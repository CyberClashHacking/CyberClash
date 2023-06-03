using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if(Boss.Instance.currentHp - 20 <= 0)
                Boss.Instance.gameObject.SetActive(false);
            else
                Boss.Instance.currentHp -= 20;
            Debug.Log("¼º°ø");
        }
    }
}
