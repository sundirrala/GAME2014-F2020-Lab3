using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalBound;
    public float horizontalSpeed;
    public float maxSpeed;

    private Vector3 m_touchesEnd;
    private Rigidbody2D m_rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        m_touchesEnd = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }


    private void _Move()
    {
        float direction = 0.0f;


        //var touch = Input.touches[0];

        // touch input-
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (worldTouch.x > transform.position.x)
            {
                // direction is positive!
                direction = 1.0f;
            }

            if (worldTouch.x < transform.position.x)
            {
                // direction is negative!
                direction = -1.0f;
            }

            m_touchesEnd = worldTouch;
        }

  

        // keyboard input!
        if(Input.GetAxis("Horizontal") >= 0.1f)
        {
            // direction is positive!
            direction = 1.0f;
        }

        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            // direction is negative!
            direction = -1.0f;
        }

        Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
        m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
        m_rigidBody.velocity *= 0.99f;

        if(m_touchesEnd.x != 0.0f)
        {
            Debug.Log("Lerping");
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, m_touchesEnd.x, 0.01f), transform.position.y);
        }

        // Vector2.Lerp(transform.position, Touch.position, 0.1f);
    }



    private void _CheckBounds()
    {
        // check right bounds.
        if(transform.position.x >= horizontalBound)
        {
            transform.position = new Vector3(horizontalBound, transform.position.y, 0.0f);
        }

        // check left bounds.
        if (transform.position.x <= -horizontalBound)
        {
            transform.position = new Vector3(-horizontalBound, transform.position.y, 0.0f);
        }

    }

}
