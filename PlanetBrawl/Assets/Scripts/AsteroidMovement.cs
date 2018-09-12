using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private Transform myTransform;
    public float rotateSpeed = 50f;
    public float movementSpeed = 10f;


    // Use this for initialization
    void Start () {
        myTransform = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(movementSpeed, movementSpeed));
    }
	
	// Update is called once per frame
	void Update () {
        myTransform.Rotate(Vector3.back * Time.deltaTime * rotateSpeed);
        
    }
}
