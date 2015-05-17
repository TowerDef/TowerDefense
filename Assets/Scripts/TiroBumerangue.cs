using UnityEngine;
using System.Collections;

public class TiroBumerangue : MonoBehaviour
{
	public int dano;
	public float vel;

	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(-vel,0);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
			
		if(other.tag=="Torre")
		{
			FaseGerenciador.faseGerenciador.Dano(dano);
			Destroy(gameObject);
		}


	}
}
