using UnityEngine;
using System.Collections;

public class MageBullet : NewBullet
{

	protected override void Update ()
	{
		base.Update ();
		
		Vector3 bulletDirection = (targetLastPosition - this.transform.position);
		bulletDirection.Normalize ();
		this.gameObject.transform.position += bulletDirection * bulletTravelSpeed * Time.deltaTime;
	}
}
