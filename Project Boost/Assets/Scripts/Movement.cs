using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Pressed SPACE - Thrusting");  // 추진 
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
