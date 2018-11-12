using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TorpedoShot : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    public float speed = 5f;
    public float rotateSpeed;

    public string explosion = "hortExplosion";
    public string cometSound = "cometSound";

    public GameObject explosionEffect;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	
	void FixedUpdate ()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
        AudioManager1.instance.Play(cometSound);

	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        AudioManager1.instance.Play(explosion);
        Destroy(gameObject);
    }
}
