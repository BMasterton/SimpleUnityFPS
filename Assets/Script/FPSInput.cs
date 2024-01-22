using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 9.0f;
    float horizInput;
    float vertInput;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizInput, 0, vertInput) * Time.deltaTime * speed;

        transform.Translate(movement);
    }
}
