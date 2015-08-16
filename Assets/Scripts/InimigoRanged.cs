using UnityEngine;
using System.Collections;

public class InimigoRanged : Inimigo {

//	public float vel,velAtk ;
//	public float vida;
//	public int dano;
	RaycastHit2D hit;
	public float range;
	bool isAtacking;

	public GameObject bumerangue;

	public LayerMask layerInimigos;

	// Use this for initialization
	void Start () 
	{

		GetComponent<Rigidbody2D>().velocity = new Vector2(-vel,0);
		isAtacking = false;
	
	}

	override public IEnumerator Ataque()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

		while(true)
		{

			if(estado==EnumStateInimigo.Morto)
				yield break;

			GameObject b = (GameObject) Instantiate(bumerangue,transform.position,Quaternion.identity);
			b.GetComponent<TiroBumerangue>().dano = dano;

			yield return new WaitForSeconds(velAtk);
		}
	}

	void Update()
	{
		if (!isAtacking) {
			hit = Physics2D.Raycast(transform.position, Vector3.left, range, layerInimigos );

			if (hit.transform!=null &&  hit.transform.gameObject.tag == "Torre")
			{
				isAtacking = true;
				vel = 0;
				StartCoroutine(Ataque());
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.tag=="Tiro")
		{
			Destroy(other.gameObject);	
				
			Dano(other.gameObject.GetComponent<Tiro>().dano, Enums.EnumDamageType.Physical);
		}

	}
}
