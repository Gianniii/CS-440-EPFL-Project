using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public Transform topOfJoystick;

    [SerializeField] //make private variables visible in the inspector
    private float forwardBackwardTilt = 0;
    [SerializeField]
    private float sideToSideTilt = 0;

    public ButtonLamp buttonLamp;
    // Update is called once per frame
    void Update()
    {
        forwardBackwardTilt = topOfJoystick.rotation.eulerAngles.x;
        if(forwardBackwardTilt < 355 && forwardBackwardTilt > 290){
            forwardBackwardTilt = Mathf.Abs(forwardBackwardTilt - 360);
            buttonLamp.on = true;
            if(forwardBackwardTilt > 45) {
                buttonLamp.on = true;
            }
            Debug.Log("Backward" + forwardBackwardTilt);
        } else if (forwardBackwardTilt > 5 && forwardBackwardTilt < 74) {
            Debug.Log("Forward" + forwardBackwardTilt);
        }

        sideToSideTilt = topOfJoystick.rotation.eulerAngles.z;
        if(sideToSideTilt < 355 && sideToSideTilt > 290){
            sideToSideTilt = Mathf.Abs(sideToSideTilt - 360);
            Debug.Log("Right"  + sideToSideTilt);
        }
        else if(sideToSideTilt > 5 && sideToSideTilt < 74) {
            Debug.Log("Left" + sideToSideTilt);
        }

    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("PlayerHand")) {
            Vector3 targetPostition = new Vector3( this.transform.position.x, 
                                        other.transform.position.y, 
                                        other.transform.position.z ) ;
            this.transform.LookAt( targetPostition ) ;
            
            //if use this istead the joystick rotates in all directions
            //transform.LookAt(other.transform.position, transform.up);
            
        }
    }
}