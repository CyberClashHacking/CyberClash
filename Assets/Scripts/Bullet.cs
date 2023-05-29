using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    Vector3 dir;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        // target = playerObj.transform;
        dir = playerObj.transform.position - transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * speed;
        rb.gravityScale = 0;
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
}
