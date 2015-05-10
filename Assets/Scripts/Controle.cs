using UnityEngine;
using System.Collections;

public class Controle : MonoBehaviour {

	public GameObject ex;
	public int spawnTime;


	void Start () 
	{
		Invoke("SpawnEnemy",spawnTime);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void SelfDestroy()
	{
		Destroy(gameObject);
	}

	public void SpawnEnemy()
	{

		Instantiate(ex,new Vector2(transform.position.x,transform.position.y),Quaternion.identity);
		Invoke("SpawnEnemy",spawnTime);
	}


}
