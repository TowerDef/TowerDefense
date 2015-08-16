using System;
using UnityEngine;
using System.Collections;

public enum NPCTeam
{
		FRIENDLY,
		ENEMY,
		NEUTRAL
}

public class UnitProperties : MonoBehaviour
{
		//---- Public fields -----//
		public float movementSpeed = 80.0f;
		public float rotateDegreesPerSecond = 180.0f;
		public float healthPoints = 100.0f;
		public float attackSpeed = 1.5f;
		public int damagePoints = 20;
		public int goldGainOnDeath = 50;
		public NPCTeam npcTeam;
    
		//---- Public Delegates -----//
		public delegate void OnWantsToDestroyUnit (GameObject go);

		public OnWantsToDestroyUnit onWantsToDestroyUnit;
    
		//---- Private fields -----//
		public void DestroyUnit ()
		{
				if (onWantsToDestroyUnit == null) {
						return;
				}
        
				onWantsToDestroyUnit (gameObject);
				
				GameObject goldPopup = GameObject.Instantiate (Resources.Load (@"Prefabs/Popup/ScorePopup"), Camera.main.WorldToViewportPoint (transform.position + new Vector3 (0, 1, 0)), Quaternion.identity) as GameObject;
				goldPopup.guiText.text = gameObject.GetComponent<UnitProperties> ().goldGainOnDeath.ToString ();
		}
}
