using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -12f)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (!ObstacleSpawner.instance.isGameOver)
        {
            rb.velocity = Vector2.left * moveSpeed;
        }
    }
}