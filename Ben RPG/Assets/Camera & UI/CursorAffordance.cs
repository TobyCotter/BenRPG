﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {
	//Variables
	[SerializeField] Texture2D walkCursor = null;
	[SerializeField] Texture2D unknownCursor = null;
	[SerializeField] Texture2D targetEnemy = null;
	[SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

	CameraRaycaster cameraRaycaster;


	void Start () {
		cameraRaycaster = GetComponent<CameraRaycaster>();
	}//End Start
	

	void LateUpdate () {
		switch(cameraRaycaster.layerHit){
			case Layer.Walkable:
				Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.RaycastEndStop:
				Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
				break;
			case Layer.Enemy: 
				Cursor.SetCursor(targetEnemy, cursorHotspot, CursorMode.Auto);
				break;
			default:
				Debug.LogError("Don't know what cursor to show");
				break;
		}
	}//End Update
}
