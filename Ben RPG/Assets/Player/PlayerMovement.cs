using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	

	[SerializeField] float walkMoveStopRadius = 0.2f;	//If we don't have this the play may walk around in circles when it reaches it's destination...gives us a little slop

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
	private bool isIndirectMode = false;	//TODO consider making static later
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }


	// Fixed update is called in sync with physics
    private void FixedUpdate()
    {	
    	if(Input.GetKeyDown(KeyCode.G)) 	//G for gamepad  TODO allow player to map later
    	{
    		isIndirectMode = !isIndirectMode;	//Toggle mode
    	}

    	if(isIndirectMode){
    		ProcessDirectMovement();
    	}else{
    		ProcessMouseMovement();
    	}
    }//End FixedUpdate


    private void ProcessDirectMovement(){
		// read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //calculate camera relative direction to move
		Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v*m_CamForward + h*Camera.main.transform.right;

        m_Character.Move(m_Move, false, false);
    }


	void ProcessMouseMovement ()
	{
		if (Input.GetMouseButton (0)) {
			print ("Cursor raycast hit" + cameraRaycaster.layerHit);
			switch (cameraRaycaster.layerHit) {
			case Layer.Walkable:
				currentClickTarget = cameraRaycaster.hit.point;
				break;
			case Layer.Enemy:
				Debug.Log ("Clicked enemy which is not walkable");
				break;
			default:
				Debug.LogWarning ("WARNING: using default case for switch statement");
				return;
			}
			var playerToClickPoint = currentClickTarget - transform.position;
			if (playerToClickPoint.magnitude >= walkMoveStopRadius) {
				m_Character.Move (playerToClickPoint, false, false);
			}
			else {
				m_Character.Move (Vector3.zero, false, false);
			}
		}
	}

   
}

