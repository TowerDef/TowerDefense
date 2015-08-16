using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
		public int bulletDamage;
		public float bulletTravelSpeed;
		
		private float minDistanceToUnit = 0.5f;

		private GameObject target;
		public GameObject Target { get { return target; } set { target = value; } }
		
		protected virtual void Update ()
		{
				if (Target == null) {
						Destroy (this.gameObject);
						return;
				}
				
				//Movement
				
				if (Vector3.Distance (Target.transform.position, this.transform.position) < minDistanceToUnit) {
						Target.GetComponent<UnitAI> ().DecreaseHealth (bulletDamage);
						Destroy (this.gameObject);
				}
		}
}
