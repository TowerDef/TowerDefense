using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
		private float cameraTranslationSpeed = 40.0f;
		private float minCameraRange = -40.0f;
		private float maxCameraRange = 20.0f;
	
		private void Update ()
		{
				if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W) || Input.mousePosition.y >= Screen.height * 0.95f) {
						MoveCamera ("Forward");
				} else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S) || Input.mousePosition.y <= Screen.height * 0.05f) {
						MoveCamera ("Backward");
				}
		}
		
		private void MoveCamera (string direction)
		{
				switch (direction) {
				case "Forward":
						if (this.transform.position.z > maxCameraRange) {
								return;
						}
						
						transform.Translate (new Vector3 (0, 0, cameraTranslationSpeed) * Time.deltaTime, Space.World);
						break;
				case "Backward":
						if (this.transform.position.z < minCameraRange) {
								return;
						}
				
						transform.Translate (new Vector3 (0, 0, -cameraTranslationSpeed) * Time.deltaTime, Space.World);
						break;
				}
		}
}
