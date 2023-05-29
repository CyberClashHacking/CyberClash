using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject player, bulletPrefab, attackPrefab;
    private Rigidbody2D rb;
    int i = 1;
    public int pattern_delay = 3;
    Boolean b;
    Boolean ud;
    Vector3 dir;

    public Transform pos;
    public Vector2 boxSize;

    void nextPattern()
    {
        int random = UnityEngine.Random.Range(0, 4);
        switch (random)
        {
            case 0: StartCoroutine(rush()); break;
            case 1: StartCoroutine(jump()); break;
            case 2: StartCoroutine(fire()); break;
            case 3: StartCoroutine(attack()); break;
        }
    }
    IEnumerator rush()
    {
        yield return new WaitForSeconds(pattern_delay);
        dir = player.transform.position - transform.position; 
        lookPlayer();
        yield return new WaitForSeconds(2);
        b = false;
        while (!b) {
            rb.velocity = new Vector2 (i, 0) * 10f;
            yield return null;
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach (Collider2D collider2D in collider2Ds)
            {
                if (collider2D.tag == "Ground" || collider2D.tag == "Player")
                {
                    b = true;
                    nextPattern();
                    break;
                }
            }
        }
    }
    IEnumerator jump()
    {
        yield return new WaitForSeconds(pattern_delay);
        while (true)
        {
            if (rb.position.y > 10)
            {
                ud = false;
            }
            if (ud)
            {
                rb.velocity = new Vector2(0, 1) * 30f;
            }
            else
            {
                yield return new WaitForSeconds(2);
                rb.velocity = new Vector2(0, -1) * 20f;
                if (rb.position.y < 5)
                {
                    nextPattern();
                    ud = true;
                    break;
                }
            }      
            yield return null;
        }
    }
    IEnumerator fire()
    {
        yield return new WaitForSeconds(pattern_delay);
        dir = player.transform.position - transform.position;
        lookPlayer();
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        nextPattern();
    }
    IEnumerator attack()
    {
        yield return new WaitForSeconds(pattern_delay);
        lookPlayer();
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach(var collider2D in collider2Ds)
        {
            Debug.Log(collider2D.tag);
        }
        nextPattern();
    }
    void lookPlayer()
    {
        i = (player.transform.position.x > transform.position.x) ? 1 : -1;
        transform.localScale = new Vector2(1f * i, 1f);
    }
    // Start is called before the first frame update
    void Start()
    {
        b = false;
        ud = true;
        rb = GetComponent<Rigidbody2D>();
        nextPattern();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}