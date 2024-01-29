using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController charController;
    private float speed = 9.0f;
    private float gravity = -9.8f;

    //float horizInput;
    //float vertInput;
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }



    // Update is called once per frame
    void Update()
    {

        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizInput, 0, vertInput);

        //Clamp magnitude to limit diagonal movement 
        movement = Vector3.ClampMagnitude(movement, 1.0f);

        //take speed into account
        movement *= speed;

        movement.y = gravity;

        //make movement processor independant 
        movement *= Time.deltaTime;

        //Convert local to global coordianates
        movement = transform.TransformDirection(movement);


        charController.Move(movement);
    }

}


