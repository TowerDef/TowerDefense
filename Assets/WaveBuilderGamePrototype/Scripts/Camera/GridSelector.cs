using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TOWERSELECTED
{
		NONE,
		ARCH,
		MACHINE,
		LASER
}

public class GridSelector : MonoBehaviour
{
		private GameManager gameManager;

		private TOWERSELECTED towerSelected;
		private Dictionary<TOWERSELECTED, GameObject> towerButtons = new Dictionary<TOWERSELECTED, GameObject> () {};
	
		private GameObject gridSlotSelector;
		
		private GameObject archTower;
		private GameObject machineTower;
		private GameObject laserTower;
	
		private void Awake ()
		{
				gameManager = FindObjectOfType (typeof(GameManager)) as GameManager;
		
				gridSlotSelector = Instantiate (Resources.Load ("Prefabs/Selector/GridSelector"), Vector3.zero, Quaternion.identity) as GameObject;
				
				archTower = Resources.Load ("Prefabs/Towers/ArchTower") as GameObject;
				machineTower = Resources.Load ("Prefabs/Towers/MachineTower") as GameObject;
				laserTower = Resources.Load ("Prefabs/Towers/LaserTower") as GameObject;
		}
		
		private void Start ()
		{
				towerButtons.Add (TOWERSELECTED.ARCH, archTower);
				towerButtons.Add (TOWERSELECTED.MACHINE, machineTower);
				towerButtons.Add (TOWERSELECTED.LASER, laserTower);
		}

		private void Update ()
		{
				if (towerSelected == TOWERSELECTED.NONE) {
						return;
				}
				
				UpdateTowerPlacement ();
		}
		
		private void OnGUI ()
		{
				GUILayout.BeginArea (new Rect (Screen.width * 0.85f, Screen.height * 0.35f, 1000, 1000));
				GUILayout.BeginVertical ();
				foreach (KeyValuePair<TOWERSELECTED, GameObject> kvp in towerButtons) {
						if (GUILayout.Button (kvp.Value.GetComponent<Tower> ().towerName + " (" + kvp.Value.GetComponent<Tower> ().goldCost + " gold)", GUILayout.Height (100), GUILayout.Width (200))) {
								if (gameManager.PlayerGold >= kvp.Value.GetComponent<Tower> ().goldCost) {
										towerSelected = kvp.Key;
								} else {
										towerSelected = TOWERSELECTED.NONE;
										DeselectGrid ();
								}
						}
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
		}
		
		private void UpdateTowerPlacement ()
		{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
		
				if (Physics.Raycast (ray, out hit)) {
						if (hit.collider.GetComponent<GridSlot> () == null) {
								DeselectGrid ();
								return;
						}
			
						if (hit.collider.GetComponent<GridSlot> ().GridSlotFilled) {
								DeselectGrid ();
								return;
						}
			
						gridSlotSelector.transform.position = hit.collider.transform.position + new Vector3 (0, 0.5f, 0);
			
						if (Input.GetMouseButtonDown (0)) {
								GameObject tower = null;
								
								switch (towerSelected) {
								case TOWERSELECTED.ARCH:
										tower = Instantiate (archTower, hit.collider.transform.position, Quaternion.identity) as GameObject;
										break;
								case TOWERSELECTED.MACHINE:
										tower = Instantiate (machineTower, hit.collider.transform.position, Quaternion.identity) as GameObject;
										break;
								case TOWERSELECTED.LASER:
										tower = Instantiate (laserTower, hit.collider.transform.position, Quaternion.identity) as GameObject;
										break;
								}
								
								if (tower == null) {
										return;
								}
								
								towerSelected = TOWERSELECTED.NONE;
				
								gameManager.PlayerGold -= tower.GetComponent<Tower> ().goldCost;
								hit.collider.GetComponent<GridSlot> ().GridSlotFilled = true;
								
								DeselectGrid ();
								tower.transform.position += new Vector3 (0, 1, 0);
						}
				}
		}
		
		private void DeselectGrid ()
		{
				gridSlotSelector.transform.position = Vector3.zero;
		}
}
