using UnityEngine;
using System.Collections;

public class LaserTower : Tower
{
		protected override void Awake ()
		{
				base.Awake ();
				BulletType = Resources.Load ("Prefabs/Bullets/LaserBullet") as GameObject;
		}
	
		protected override void Update ()
		{
				base.Update ();
		}
}
