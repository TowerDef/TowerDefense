using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Inimigo : MonoBehaviour {
	public float vel, velAtk;
	public float vida;
	public int dano;
	public float defesa;
	public float defesaMagica;
	public Animator anim;
	protected EnumStateInimigo estado;

	private List<Enums.EnumDebuffType> debuffList;
	
	public void Awake() 
	{
		estado = EnumStateInimigo.Andando;
		anim = GetComponent<Animator>();
		debuffList = new List<Enums.EnumDebuffType>();
	}

	public abstract IEnumerator Ataque();
	public void Dano(float d, Enums.EnumDamageType damageType) {
		if (damageType == Enums.EnumDamageType.Physical)
			d = d - (d * defesa / 100);
		else if (damageType == Enums.EnumDamageType.Magical)
			d = d - (d * defesaMagica / 100);

		vida-=d;

		if(vida<=0 && estado!=EnumStateInimigo.Morto)
		{
			anim.Play("Dano",1,0);
			GetComponent<Collider2D>().enabled =false;
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			anim.Play("Morte",0,0);
			estado=EnumStateInimigo.Morto;
		}
		else if (vida > 0) 
		{
			anim.Play("Dano",1,0);
			UpdateHealthBar();
		}
	}

	private void UpdateHealthBar() {
		//TODO: barra de vida
	}
	
	public void Morte ()
	{
		Destroy(gameObject);
	}

	public void AplicarDebuff(NewBullet.Debuff debuff) {
		switch (debuff.Type) {
		case Enums.EnumDebuffType.Weakness:
			if (!debuffList.Contains(Enums.EnumDebuffType.Weakness)) {
				debuffList.Add(Enums.EnumDebuffType.Weakness);
			}
			break;
		case Enums.EnumDebuffType.Slow:
			if (!debuffList.Contains(Enums.EnumDebuffType.Slow) && estado!=EnumStateInimigo.Morto) 
			{
				debuffList.Add(Enums.EnumDebuffType.Slow);
				StartCoroutine(Slow(debuff.Power, debuff.Time));
			}
			break;
		}
	}

	private IEnumerator Slow(float debuffPower, float debuffTime)
	{
		float newVel = vel - (vel * debuffPower / 100);
		GetComponent<Rigidbody2D>().velocity = new Vector2(-newVel,0);
	
		yield return new WaitForSeconds(debuffTime);

		if (estado!=EnumStateInimigo.Morto)
			GetComponent<Rigidbody2D>().velocity = new Vector2(-vel,0);

		debuffList.Remove(Enums.EnumDebuffType.Slow);
	} 

	public enum EnumStateInimigo
	{
		Andando,
		Atacando,
		Parado,
		Morto
	};
}
