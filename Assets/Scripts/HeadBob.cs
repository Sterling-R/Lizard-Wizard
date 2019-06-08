using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour {

    [SerializeField] Vector3 restPosition;
    [SerializeField] float transitionSpeed = 20f; 
    [SerializeField] public float bobSpeed = 4.8f; 
    [SerializeField] float bobAmount = 0.05f;
     
	 //initialized to this value because this is where sin = 1. This will make the camera always start at the crest of the sin wave
     float timer = Mathf.PI / 2; 
     Vector3 camPos;
 
     void Start()
     {
		 restPosition = transform.localPosition;
         camPos = transform.localPosition;
     }
 
     void LateUpdate()
     {
		//moving
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
    	    timer += bobSpeed * Time.deltaTime;
 
            //use the timer value to set the position
			//abs val of y for a parabolic path
            Vector3 newPosition = new Vector3(Mathf.Cos(timer) * bobAmount, restPosition.y + Mathf.Abs((Mathf.Sin(timer) * bobAmount)), restPosition.z);
            transform.localPosition = newPosition;
        }

		//reinitialize
        else
        {
            timer = Mathf.PI / 2;

			//transition smoothly from walking to stopping.
            Vector3 newPosition = new Vector3(Mathf.Lerp(camPos.x, restPosition.x, transitionSpeed * Time.deltaTime), Mathf.Lerp(camPos.y, restPosition.y, transitionSpeed * Time.deltaTime), Mathf.Lerp(camPos.z, restPosition.z, transitionSpeed * Time.deltaTime));
            transform.localPosition = newPosition;
        }
	
		//completed a full cycle on the unit circle. Reset to 0 to avoid bloated values.
        if (timer > Mathf.PI * 2) 
        {
			 timer = 0;
		}
	 }
 }
