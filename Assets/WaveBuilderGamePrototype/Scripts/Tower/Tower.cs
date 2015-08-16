using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
		private GameManager gameManager;

		public int goldCost;
		public string towerName;
		public float fireRate;
		public float agroRange;
			
		private float fireTimer = 0;
		private bool ableToFire = true;

		private GameObject target;
		public GameObject Target{ get { return target; } set { target = value; } }

		private GameObject bulletType;
		public GameObject BulletType { get { return bulletType; } set { bulletType = value; } }
		
		protected virtual void Awake ()
		{
				gameManager = FindObjectOfType (typeof(GameManager)) as GameManager;
		}
		
		protected virtual void Update ()
		{
				UpdateTarget ();
				UpdateAbleToFire ();
				UpdateFireAtTarget ();
		}
	
		private void UpdateTarget ()
		{
				if (Target != null) {
						if (Vector3.Distance (Target.transform.position, this.transform.position) > agroRange) {
								Target = null;
						} else {
								if (!gameManager.UnitList.Contains (Target)) {
										Target = null;
								}
						}
						return;
				}
		
				for (int i = 0; i < gameManager.UnitList.Count; i++) {
						if (Vector3.Distance (gameManager.UnitList [i].transform.position, this.transform.position) < agroRange) {
								Target = gameManager.UnitList [i];
						}
				}
		}
	
		private void UpdateAbleToFire ()
		{
				if (ableToFire) {
						return;
				}
		
				fireTimer += 1.0f * Time.deltaTime;
				if (fireTimer >= fireRate) {
						fireTimer = 0;
			
						ableToFire = true;
				}
		}
	
		private void UpdateFireAtTarget ()
		{
				if (Target == null) {
						return;
				}
		
				if (!ableToFire) {
						return;
				}
		
				GameObject bullet = Instantiate (BulletType, transform.position, Quaternion.identity) as GameObject;
				bullet.GetComponent<Bullet> ().Target = Target;
				ableToFire = false;
		}
}
