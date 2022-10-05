using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public float speed = 10;
    public float rotSpeed = 205;
    float mx;
    float my;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 dir = v * Vector3.forward + h * Vector3.right;
        dir.Normalize();
        transform.forward = dir;
        transform.position += dir * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q))
        {
           my += -1 * rotSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            my += rotSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            mx += 1 * rotSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            mx += -rotSpeed * Time.deltaTime;
        }
        transform.eulerAngles = new Vector3(-mx, my, 0);
    }
}
