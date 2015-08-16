using UnityEngine;
using System.Collections;

public class LaserBullet : Bullet
{
		private TrailRenderer trailRenderer;

		private void Awake ()
		{
				trailRenderer = this.gameObject.AddComponent<TrailRenderer> ();
				trailRenderer.material = new Material (Shader.Find ("Particles/Additive"));
				trailRenderer.startWidth = 0.6f;
				trailRenderer.endWidth = 0.1f;
				trailRenderer.time = 0.1f;
				
				// Generate a random color.
				Color nColor = new Color (0.6f, 0.2f, 0.2f, 0.1f);
		
				// Get the material list of the trail as per the scripting API.
				Material trail = gameObject.GetComponent<TrailRenderer> ().material;
		
				// Set the color of the material to tint the trail.
				trail.SetColor ("_TintColor", nColor);
		}

		protected override void Update ()
		{
				base.Update ();
				
				Vector3 bulletDirection = (Target.transform.position - this.transform.position);
				bulletDirection.Normalize ();
				this.gameObject.transform.position += bulletDirection * bulletTravelSpeed * Time.deltaTime;
		}
}
