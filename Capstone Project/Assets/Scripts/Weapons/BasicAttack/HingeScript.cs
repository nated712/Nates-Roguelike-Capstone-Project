using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeScript : MonoBehaviour
{

    public Transform shoulder;
    // how far you want the sword to be from point
    public float armLength = 1f;
    Vector3 shoulderToMouseDir;
    // Start is called before the first frame update
    void Start()
    {
        shoulder = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mousePosition != null){
        // Get the direction between the shoulder and mouse (aka the target position)
        shoulderToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shoulder.position;
        // we normalize the new direction so you can make it the arm's length
        } else{
            shoulderToMouseDir = Vector3.zero;
        }
        shoulderToMouseDir.z = 0; // zero z axis since we are using 2d
        // then we add it to the shoulder's position
        transform.position = shoulder.position + (armLength * shoulderToMouseDir.normalized);


    }
}
