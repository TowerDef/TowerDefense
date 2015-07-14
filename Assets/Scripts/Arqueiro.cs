using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Arqueiro : MonoBehaviour {

	public float range;
	public List<GameObject> listaInimigos;
	public float bulletSpeed;
	public GameObject bala;
	public float dano;
	
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

			float menorDistancia = listaInimigos.First().transform.position.x;
			GameObject target = listaInimigos.First();

			foreach(GameObject g in listaInimigos)
			{
				float tempDis = g.transform.position.x;

				if(tempDis<menorDistancia)
				{
					target=g;
					menorDistancia = tempDis;

				}

			}


			Vector2? predictedPosition =  getPreddictedPosition(target.transform.position,target.GetComponent<Rigidbody2D>().velocity,transform.position,bulletSpeed);
			//Vector2 direction = target.transform.position - transform.position;
			if (predictedPosition != null && predictedPosition.HasValue) {
				Vector2 direction = predictedPosition.Value - (Vector2)transform.position;
				direction.Normalize();

				if(target!=null)
				{
					GameObject _bala = (GameObject) Instantiate(bala, transform.position, Quaternion.identity);
					_bala.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
					yield return new WaitForSeconds(1);
				}
				else
				{

				}
			}

			yield return null;
		}

	}

	// Update is called once per frame
	void Update () 
	{
	
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
	

	Vector2? getPreddictedPosition(Vector2 targetPosition, Vector2 targetVelocity, Vector2 sourcePosition, float bulletSpeed) {
		float a = Mathf.Sqrt(targetVelocity.x) + Mathf.Sqrt(targetVelocity.y) - Mathf.Sqrt(bulletSpeed);
		float b = 2 * (targetVelocity.x * (targetPosition.x - sourcePosition.x) + targetVelocity.y * (targetPosition.y - sourcePosition.y));
		float c = Mathf.Sqrt(targetPosition.x - sourcePosition.x) + Mathf.Sqrt(targetPosition.y - sourcePosition.y);
		
		float delta = Mathf.Sqrt(b) - 4 * a * c;
		Debug.Log(delta);
		if (delta < 0) { //Se delta for menor que 0, o tiro nunca vai chegar a tempo
			return null;

		}
		else {		
			float t1 = (-b + Mathf.Sqrt(delta)) / (2 * a);
			float t2 = (-b - Mathf.Sqrt(delta)) / (2 * a);
			
			float t = Mathf.Min(t1, t2);
			//Debug.Log(t);
			if (t < 0) t = Mathf.Max(t1, t2);
			if (t > 0) { //Se o t for positivo
				float aimX = t * targetVelocity.x + targetPosition.x;
				float aimY = t * targetVelocity.y + targetPosition.y;
				
				return new Vector2(aimX, aimY);
			}
			else //Se for negativo
				return null;
		}
	}
}
