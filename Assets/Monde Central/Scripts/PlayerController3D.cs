using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour {

    public float speed = 1;


    private CharacterController m_cc;
    private GameObject orientationX;
    private GameObject orientationY;

    void Start()
    {
        orientationX = GameObject.Find("OrientationX");
        orientationY = GameObject.Find("OrientationY");
        m_cc = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
    }

    void Update()
    {
        float x = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Horizontal");
        Vector3 motionX = (transform.position - orientationX.transform.position);
        motionX.y = 0;

        Vector3 motionY = (transform.position - orientationY.transform.position);
        motionY.y = 0;

        Vector3 motion = (motionX*x + motionY * y) * Time.deltaTime * speed;

        m_cc.Move(motion);
        


    }

}