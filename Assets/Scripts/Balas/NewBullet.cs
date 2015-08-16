using UnityEngine;
using System.Collections;

public class NewBullet : MonoBehaviour
{
	[System.Serializable]
	public class Debuff {
		public Enums.EnumDebuffType Type;
		public float Power;
		public float Time;
	}

	public Debuff debuff;
//	public Enums.EnumDebuffType debuffType;
//	public float debuffPower;
//	public float debuffTime;

	public int bulletDamage;
	public float bulletTravelSpeed;
		
	public float minDistanceToUnit = 0.5f;
	
	private GameObject target;
	public GameObject Target { get { return target; } set { target = value; } }
	protected Vector3 targetLastPosition;

	public Enums.EnumDamageType damageType;

	protected void Awake() 
	{
		//debuff = new Debuff();
	}
		
	protected virtual void Update ()
	{

		if (Target != null) 
			targetLastPosition = Target.transform.position;

				
		//Movement
		if (Vector3.Distance (targetLastPosition, this.transform.position) < minDistanceToUnit)
		{

			if (Target != null) {			
				Target.GetComponent<Inimigo>().Dano(bulletDamage, damageType);
				if (debuff.Type != Enums.EnumDebuffType.None) Target.GetComponent<Inimigo>().AplicarDebuff(debuff);
			}
						
			Destroy (this.gameObject);
		}
	}


}
