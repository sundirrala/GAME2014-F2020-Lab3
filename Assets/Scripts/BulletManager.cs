using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable] // expose bullet manager to the entire project. NOT instantiating.
public class BulletManager : MonoBehaviour
{
    public GameObject bullet;
    public int maxBullets;

    private Queue<GameObject> m_bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        _BuildBulletPool();
    }

    private void _BuildBulletPool()
    {
        // create empty Queue structure
        m_bulletPool = new Queue<GameObject>();

        for (int count = 0; count < maxBullets; count++)
        {
            var tempBullet = Instantiate(bullet);
            tempBullet.SetActive(false);
            tempBullet.transform.parent = transform; // child of my object
            m_bulletPool.Enqueue(tempBullet);
        }
    }

    public GameObject GetBullet(Vector3 position)
    {
        var newBullet = m_bulletPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;
        return newBullet;
    }

    public void ReturnBullet(GameObject returnedBullet)
    {
        returnedBullet.SetActive(false);
        m_bulletPool.Enqueue(returnedBullet);
    }
}
