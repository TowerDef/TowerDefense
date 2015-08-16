using UnityEngine;
using System.Collections;

public class ArchBullet : Bullet
{
		private float offsetX = 2;
		private float offsetY = 2;
	
		private TrailRenderer trailRenderer;
	
		private Vector3 launchStartLocation = Vector3.zero;
		private float ArchHeigth = 15.0f;
		private float bezierTravelSpeed = 0.0f;
	
		private void Awake ()
		{
				trailRenderer = this.gameObject.AddComponent<TrailRenderer> ();
				trailRenderer.material = new Material (Shader.Find ("Particles/Additive"));
				trailRenderer.startWidth = 0.6f;
				trailRenderer.endWidth = 0.1f;
				trailRenderer.time = 0.2f;
		
				launchStartLocation = gameObject.transform.position;
		
				// Generate a random color.
				Color nColor = new Color (0.2f, 1, 0.2f, 0.2f);
		
				// Get the material list of the trail as per the scripting API.
				Material trail = gameObject.GetComponent<TrailRenderer> ().material;
		
				// Set the color of the material to tint the trail.
				trail.SetColor ("_TintColor", nColor);
		}
	
		protected override void Update ()
		{
				base.Update ();
		
				bezierTravelSpeed += 2.00f * Time.deltaTime;
				gameObject.transform.position = Bezier3 (launchStartLocation, 
		                                         new Vector3 ((launchStartLocation.x + Target.transform.position.x) * 0.50f, (launchStartLocation.y + Target.transform.position.y) * 0.50f + ArchHeigth, (launchStartLocation.z + Target.transform.position.z) * 0.50f), 
		                                         Target.transform.position, bezierTravelSpeed);
		}
	
		public Vector3 Bezier3 (Vector3 start, Vector3 control, Vector3 end, float t)
		{
				return (((1 - t) * (1 - t)) * start) + (2 * t * (1 - t) * control) + ((t * t) * end);
		}
}


