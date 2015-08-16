using UnityEngine;
using System.Collections;

public class MachineTower : Tower
{
		protected override void Awake ()
		{
				base.Awake ();
				BulletType = Resources.Load ("Prefabs/Bullets/MachineBullet") as GameObject;
		}
		
		protected override void Update ()
		{
				base.Update ();
		}
}
