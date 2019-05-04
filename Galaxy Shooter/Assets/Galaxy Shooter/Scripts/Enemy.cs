using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	[SerializeField]
	private GameObject _enemyExplosionPrefab;

	[SerializeField]
	private float _speed = 2.0f;
	[SerializeField]
	private AudioClip _clip;


	private UIManager _uiManager;

	// Start is called before the first frame update
	void Start ()
	{

		_uiManager = GameObject.Find ("Canvas").GetComponent<UIManager>();
	}

	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y < -7.0f) {
			float randomX = Random.Range (-7.0f, 7.0f);
			transform.position = new Vector3 (randomX, 7.0f, 0);
		}

	}


	private void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Laser") {
			if (other.transform.parent != null) {
				Destroy (other.transform.parent.gameObject);
			}
			Destroy (other.gameObject);
			Destroy (gameObject);
			Instantiate (_enemyExplosionPrefab, transform.position, Quaternion.identity);
			_uiManager.UpdateScore ();
			AudioSource.PlayClipAtPoint (_clip, Camera.main.transform.position);
		}
		else if (other.tag == "Player") {
			Player player = other.GetComponent<Player> ();
			if (player != null){
				player.Damage ();
			}
			Destroy (gameObject);
			Instantiate (_enemyExplosionPrefab, transform.position, Quaternion.identity);
			AudioSource.PlayClipAtPoint (_clip, Camera.main.transform.position);

		}
	
	}
}
