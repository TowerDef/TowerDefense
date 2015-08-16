using UnityEngine;
using System.Collections;

public class ScorePopup : MonoBehaviour
{
		private float fadeTimer = 3.0f;
		private float decreaseFactor = 3.0f;
    
		void Update ()
		{
				//Used for Score that Follows The Player. (So People Can Read Better).
				transform.Translate (new Vector3 (0, 0.1f, 0) * Time.deltaTime);   
        
				fadeTimer -= Time.deltaTime * decreaseFactor;	//Decrease The Timer.
				guiText.material.color = new Color (1, 1, 1, fadeTimer);	//Set alpha to the Timer Variable. (Fade Out)
				Destroy (gameObject, fadeTimer);	//When The Timer Reaches 0, (3 to 0) -> Destroy This GameObject.
		}
}
