using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float verticalSpeed;
    public float verticalBound;

    public BulletManager bulletManager;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        transform.position += new Vector3(0.0f, verticalSpeed, 0.0f);
    }

    private void _CheckBounds()
    {
        if(transform.position.y > verticalBound)
        {
            bulletManager.ReturnBullet(gameObject);
        }
    }

}
