using UnityEngine;
using System.Collections;

public class MachineBullet : Bullet
{
		protected override void Update ()
		{
				base.Update ();
		
				Vector3 bulletDirection = (Target.transform.position - this.transform.position);
				bulletDirection.Normalize ();
				this.gameObject.transform.position += bulletDirection * bulletTravelSpeed * Time.deltaTime;
		}
}
