using UnityEngine;
using System.Collections;

public class ArchTower : Tower
{
		protected override void Awake ()
		{
				base.Awake ();
				BulletType = Resources.Load ("Prefabs/Bullets/ArchBullet") as GameObject;
		}
		
		protected override void Update ()
		{
				base.Update ();
		}
}
