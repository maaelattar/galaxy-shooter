using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField]
	private float _speed = 10.0f;
	// Start is called before the first frame update
	void Start ()
	{
        
	}

	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.up * _speed * Time.deltaTime);
		/*
		if(tag == "Player_Laser"){
			transform.Translate (Vector3.up * _speed * Time.deltaTime);
		}
		else if(tag == "Enemy_Laser"){
			transform.Translate (Vector3.down * _speed * Time.deltaTime);
		}*/

		if (transform.position.y >= 6.0f) {
			if (transform.parent != null) {
				Destroy (transform.parent.gameObject);
			}
			Destroy (gameObject);
		}
		else if (transform.position.y <= -6.0f) {
			if (transform.parent != null) {
				Destroy (transform.parent.gameObject);
			}
			Destroy (gameObject);
		}
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		/*
		if (other.tag == "Player" && tag == "Enemy_Laser") {
			Player player = other.GetComponent<Player> ();		
			if (player != null) {
				player.Damage ();
				Destroy (gameObject);
			}
		}*/
		/*
		else if (other.tag == "Enemy" && tag == "Player_Laser") {
			Enemy enemy = other.GetComponent<Enemy> ();		
			if (enemy != null) {
				enemy.Damage ();	
				Destroy (gameObject);
			}
		}*/


	}

}
