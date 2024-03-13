using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // float Xvalue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
       // float Yvalue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

       // transform.Translate(Xvalue,0,Yvalue);
        Vector3 movement = Vector3.zero; 

        if (Input.GetKey(KeyCode.A))
            movement += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            movement += Vector3.right;
        if (Input.GetKey(KeyCode.W))
            movement += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            movement += Vector3.back;

        transform.position += movement.normalized * moveSpeed * Time.deltaTime;
    }
}
