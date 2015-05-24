using UnityEngine;
using System.Collections;

public abstract class Inimigo : MonoBehaviour {
	public float vel, velAtk;
	public float vida;
	public int dano;
	public Animator anim;

	public abstract IEnumerator Ataque();
	public abstract void Dano(float d);

	public void Morte ()
	{
		Destroy(gameObject);
	}

	public abstract void AplicarDebuff(EnumDebuffInimigo debuff, float porcDebuff);

	public enum EnumDebuffInimigo
	{
		None,
		Lentidao,
		Fraqueza
	};
}
