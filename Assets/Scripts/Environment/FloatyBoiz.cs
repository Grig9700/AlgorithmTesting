using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyBoiz : MonoBehaviour
{
    [SerializeField]
    private float fastestSpeed = 20;
    [SerializeField]
    private float slowedtSpeed = 1;

    private float speed;
    private static float worldX = 10;
    private static float worldY = 5;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(2 * worldX, 2 * worldY, 0));
    }

    private void OnValidate()
    {
        if (fastestSpeed < slowedtSpeed)
        {
            slowedtSpeed = fastestSpeed - 1;
        }
        if (fastestSpeed <= 1)
        {
            fastestSpeed = 1;
        }
        if (slowedtSpeed <= 0)
        {
            slowedtSpeed = 0;
        }
    }

    private void Move()
    {
        transform.position += transform.up * (Time.deltaTime * speed);

        if (transform.position.x < -worldX)
        {
            transform.position += new Vector3(+ 2 * worldX, 0, 0);
        }
        if (transform.position.x > worldX)
        {
            transform.position += new Vector3(- 2 * worldX, 0, 0);
        }
        if (transform.position.y < -worldY)
        {
            transform.position += new Vector3(0, + 2 * worldY, 0);
        }
        if (transform.position.y > worldY)
        {
            transform.position += new Vector3(0, -2 * worldY, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(slowedtSpeed, fastestSpeed);
        transform.Rotate(Vector3.forward * Random.Range(0f, 360f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
