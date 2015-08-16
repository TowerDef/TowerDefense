using UnityEngine;
using System.Collections;

public class TiroArqueiro : NewBullet {
	
	private Vector3 launchStartLocation = Vector3.zero;
	private float ArchHeigth = 3.0f;
	private float bezierTravelSpeed = 0.0f;
	public float bulletVel;

	// Use this for initialization
	void Awake () 
	{
		launchStartLocation = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update();
		bezierTravelSpeed += bulletVel * Time.deltaTime;
		gameObject.transform.position = Bezier3 (launchStartLocation, 
		                                         new Vector3 ((launchStartLocation.x + targetLastPosition.x) * 0.50f, (launchStartLocation.y + targetLastPosition.y) * 0.50f + ArchHeigth, (launchStartLocation.z + targetLastPosition.z) * 0.50f), 
		                                         targetLastPosition, bezierTravelSpeed);
	}
	
	public Vector3 Bezier3 (Vector3 start, Vector3 control, Vector3 end, float t)
	{
		return (((1 - t) * (1 - t)) * start) + (2 * t * (1 - t) * control) + ((t * t) * end);
	}
}
