﻿using UnityEngine;
using System.Collections;

public class InimigoMelee : Inimigo {

//	public float vel,velAtk ;
//	public float vida;
//	public int dano;

	// Use this for initialization
	void Start () 
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(-vel,0);
	}

	override public IEnumerator Ataque()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

		while(true)
		{
			FaseGerenciador.faseGerenciador.Dano(dano);
			yield return new WaitForSeconds(velAtk);
		}
	}
	

	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.tag=="Tiro")
		{
			Destroy(other.gameObject);	
				
			Dano(other.gameObject.GetComponent<Tiro>().dano);
		}
		else if (other.tag== "Torre") 
		{

			vel = 0;
			StartCoroutine(Ataque());
		}
	}

	override public void Dano(float d)
	{
		vida-=d;

		if(vida<=0)
			Destroy(gameObject);
	}

	override public void AplicarDebuff(EnumDebuffInimigo debuff, float porcDebuff)
	{
		//TODO: aplicar Debuff nos inimigos (lentidao, fraqueza...)
		Debug.Log("Aplicou Debuff Melee!!!!");
	}
}
