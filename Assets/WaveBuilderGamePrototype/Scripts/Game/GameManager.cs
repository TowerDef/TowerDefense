using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
		private CreepSpawner[] creepSpawners;
	
		private int currentWave = 1;
		
		private List<GameObject> unitList = new List<GameObject> ();
		
		private int playerHealth = 5000;
		private int playerGold = 150;
		
		private int goldGainOnNextWave = 60;
		
		private float baseWaveSpawnCounter = 25;
		private float nextWaveSpawnCounter;
	
		private bool gameStarted = false;
		private bool gameOver = false;
		private bool noNextWaveAvailable = false;
		
		private void Awake ()
		{
				creepSpawners = FindObjectsOfType (typeof(CreepSpawner)) as CreepSpawner[];
		}
		
		private void Start ()
		{
				nextWaveSpawnCounter = baseWaveSpawnCounter;
		
				for (int i = 0; i < creepSpawners.Length; i++) {
						creepSpawners [i].CreepSpawnerIndex = i;
				}
		}
	
		private void OnGUI ()
		{
				GUIStyle guiStyle = new GUIStyle ();
				guiStyle.fontSize = 25;
				guiStyle.normal.textColor = Color.white;
		
				if (GUI.Button (new Rect (20, Screen.height * 0.40f, Screen.width * 0.15f, Screen.height * 0.07f), gameStarted ? "NEXT WAVE" : "START WAVE")) {
						if (noNextWaveAvailable) {
								return;
						}
						
						if (!gameStarted) {
								gameStarted = true;
								SpawnWave ();
						} else {
								currentWave++;
								SpawnWave ();
						}
						
						nextWaveSpawnCounter = baseWaveSpawnCounter;
				}
				
				GUI.Label (new Rect (20, Screen.height * 0.50f, 500, 100), "Wave: " + currentWave + " / " + creepSpawners [0].creepWaveList.Length, guiStyle);

				GUI.Label (new Rect (20, Screen.height * 0.54f, 500, 100), "Monsters Alive: " + unitList.Count, guiStyle);
				
				GUI.Label (new Rect (20, Screen.height * 0.64f, 500, 100), "Base health: " + playerHealth, guiStyle);
				GUI.Label (new Rect (Screen.width * 0.865f, Screen.height * 0.30f, 500, 100), "Gold: " + playerGold, guiStyle);
				
				GUI.Label (new Rect (20, Screen.height * 0.35f, 500, 100), noNextWaveAvailable ? "NO NEXT WAVE AVAILABLE" : "NEXT WAVE IN: " + nextWaveSpawnCounter.ToString ("F0") + " SECONDS", guiStyle);
				
				//TODO: Control Scheme. (Mouse and W/^)
				
				if (gameOver) {
						if (GUI.Button (new Rect (Screen.width * 0.37f, Screen.height * 0.45f, Screen.width * 0.25f, Screen.height * 0.10f), "GAME OVER - CLICK TO PLAY AGAIN")) {
								gameOver = false;
								Application.LoadLevel (Application.loadedLevel);
						}
				}
		}
		
		private void Update ()
		{
				if (!gameStarted) {
						return;
				}
				
				if (noNextWaveAvailable) {
						return;
				}
				
				nextWaveSpawnCounter -= 1 * Time.deltaTime;
				if (nextWaveSpawnCounter <= 0) {
						nextWaveSpawnCounter = baseWaveSpawnCounter;
						
						currentWave++;
						SpawnWave ();
				}
		}
		
		public void DecreasePlayerHealth (int damagePoints)
		{
				if (gameOver) {
						return;
				}
				
				playerHealth -= damagePoints;
				
				if (playerHealth <= 0) {
						playerHealth = 0;
						gameOver = true;
				}
		}
		
		public void IncreasePlayerGold (int goldGained)
		{
				playerGold += goldGained;
		}
		
		private void SpawnWave ()
		{
				if (noNextWaveAvailable) {
						return;
				}
		
				if (currentWave >= creepSpawners [0].creepWaveList.Length) {
						noNextWaveAvailable = true;
				}
		
				foreach (CreepSpawner cs in creepSpawners) {
						cs.PlayWave (currentWave);
				}
				
				playerGold += goldGainOnNextWave;
		}
		
		public List<GameObject> UnitList { get { return unitList; } set { unitList = value; } }
		public int PlayerHealth { get { return playerHealth; } set { playerHealth = value; } }
		public int PlayerGold { get { return playerGold; } set { playerGold = value; } }
}
