using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FireMage : MonoBehaviour {

	
	//public float range;
	public float cooldown;
	public List<GameObject> listaInimigos;
	public float bulletSpeed;
	public GameObject bala;
	public float dano;
	private GameObject target;
	
	
	// Use this for initialization
	void Start () 
	{
		listaInimigos = new List<GameObject>();
		StartCoroutine(Shot());
	}
	
	IEnumerator Shot()
	{
		while(true)
		{
			listaInimigos.RemoveAll(i => i == null);
			
			while(listaInimigos.Count<=0)
			{
				yield return null;
			}
			
			
			
			//			listaInimigos.Min(i => i.transform.position.x);
			
			if (target == null)
			{
				float menorDistancia = listaInimigos.First().transform.position.x;
				target = listaInimigos.First();
				
				foreach(GameObject g in listaInimigos)
				{
					float tempDis = g.transform.position.x;
					
					if(tempDis<menorDistancia)
					{
						target=g;
						menorDistancia = tempDis;
						
					}
					
				}
			}
			
			GameObject _bala = (GameObject) Instantiate(bala, transform.position, Quaternion.identity);
			_bala.GetComponent<NewBullet>().Target = target;
			
			if(target.GetComponent<Inimigo>().vida - bala.GetComponent<NewBullet>().bulletDamage <=0)
			{
				listaInimigos.Remove(target);
				target = null;
			}
			
			yield return new WaitForSeconds(cooldown);
			

			
		//	yield return null;
		}
		
	}
	
	
	void OnTriggerEnter2D(Collider2D col) 
	{
		if (col.gameObject.tag == "Inimigo")
			listaInimigos.Add(col.gameObject);
	}
	
	void OnTriggerExit2D(Collider2D col) 
	{
		if (col.gameObject.tag == "Inimigo")
			listaInimigos.Remove(col.gameObject);
	}
}
