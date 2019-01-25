using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBall : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.AddForce(Vector3.back * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * speed * Time.deltaTime);
        }*/
    }
    
    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);
    }
}