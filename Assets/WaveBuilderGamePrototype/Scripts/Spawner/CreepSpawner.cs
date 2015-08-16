using UnityEngine;
using System.Collections;

public class CreepSpawner : MonoBehaviour
{
		private GameManager gameManager;

		public CreepWaveList[] creepWaveList;
		
		private int creepSpawnerIndex;
		private int secondsBetweenSpawn = 2;
		
		private void Awake ()
		{
				gameManager = FindObjectOfType (typeof(GameManager)) as GameManager;
		}
		
		public void PlayWave (int wave)
		{
				int waveIndex = wave - 1;
				
				StartCoroutine (SpawnExecutor (waveIndex));
		}
		
		private IEnumerator SpawnExecutor (int waveIndex)
		{
				for (int i = 0; i < creepWaveList[waveIndex].creeps.Length; i++) {
		
						GameObject creep = Instantiate (creepWaveList [waveIndex].creeps [i], this.transform.position, new Quaternion (0, 180, 0, 0)) as GameObject;
						creep.GetComponent<UnitAI> ().CreepSpawnerIndex = creepSpawnerIndex;
						creep.GetComponent<UnitProperties> ().onWantsToDestroyUnit += UnitDestroyer;
						gameManager.UnitList.Add (creep);
		
						yield return new WaitForSeconds (secondsBetweenSpawn);
				}
				
				yield break;
		}
		
		private void UnitDestroyer (GameObject unit)
		{
				gameManager.UnitList.Remove (unit);
				StartCoroutine (RemoveUnitFromField (unit));
		}
		
		private IEnumerator RemoveUnitFromField (GameObject unit)
		{
				yield return new WaitForSeconds (2);
				GameObject.Destroy (unit);
		}
		
		public int CreepSpawnerIndex { get { return creepSpawnerIndex; } set { creepSpawnerIndex = value; } }
}
