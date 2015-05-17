using UnityEngine;
using System.Collections;

public class FaseGerenciador : MonoBehaviour {

	static public FaseGerenciador faseGerenciador;

	public int vidaTotal, vidaAtual;

	public float cooldown;
	float balaTimer;

	public GameObject bala;
	public GameObject torre;
	public float bulletSpeed;

	public GameObject[] inimigo = new GameObject[4];
	public int spawnTime;

	public UILabel vidaLabel;
	public UI2DSprite vidaBarra;

	void Awake()
	{
		faseGerenciador = this;
		Application.targetFrameRate = 60;
	}
	// Use this for initialization
	void Start () 
	{
		vidaAtual = vidaTotal;
		vidaLabel.text = vidaAtual + "/" + vidaTotal;
		Invoke("SpawnEnemy",spawnTime);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CriarBala()
	{
		if(balaTimer<Time.time)
		{ 
			balaTimer = Time.time + cooldown;

			Vector3 initialPosition = torre.transform.position;
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
			
			Vector2 direction = mousePosition - initialPosition;
			direction.Normalize();
			GameObject _bala = (GameObject) Instantiate(bala, initialPosition, Quaternion.identity);
			
			_bala.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
		
		}
	}

	public void SpawnEnemy()
	{
		float posY = -2 + Random.Range(0f,6f);
		Instantiate(inimigo[Random.Range(0,inimigo.Length)],new Vector3(7,posY,posY),Quaternion.identity);
		Invoke("SpawnEnemy",spawnTime);
	}

	public void Dano(int _dano) 
	{
		vidaAtual -= _dano;
		AtualizarHP();

		if(vidaAtual<=0)
			Application.LoadLevel(0);
	}

	public void AtualizarHP()
	{
		vidaLabel.text = vidaAtual + "/" + vidaTotal;
		vidaBarra.fillAmount =  (float) vidaAtual/vidaTotal;
	}
}
