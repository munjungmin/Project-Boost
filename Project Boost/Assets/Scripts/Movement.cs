using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()  // 추진
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("thrust");
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);   // 0 1 0 // 월드말고 물체 좌표 기준으로 힘을 가함 
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))  // 왼쪽에 우선권 = 양쪽을 동시에 누르면 왼쪽으로 감 
        {
            Debug.Log("rotate left");
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Debug.Log("rotate right");
        }
    }
}
