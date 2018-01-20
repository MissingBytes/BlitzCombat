using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayloadMovement : MonoBehaviour {
    public Rigidbody2D rb;
    int i;
    Vector3[] TravelPoints;
	// Use this for initialization
	void Start () {
        i = 0;
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(-6.47f, 13.59f);

        TravelPoints =new []{new Vector3 (-13f, 13.6f ), 
                             new Vector3(-13f,-3.2f),
                             new Vector3(6.3f,-3.2f),
                             new Vector3(6.3f,25.1f),
                             new Vector3(32.7f,25.1f),
                             new Vector3(32.7f,-3.2f),
                             new Vector3(52f,-3.2f),
                             new Vector3(52f,13.6f),
                             new Vector3(45.5f,13.6f) };
    }
	
	// Update is called once per frame
	void Update () {
        float step = 10 * Time.deltaTime;
        if (transform.position == TravelPoints[i] && TravelPoints.Length-1>i)
            i++;            
        transform.position = Vector2.MoveTowards(transform.position,TravelPoints[i],step);
        //rb.velocity = new Vector2(0, 1);

    }
}
