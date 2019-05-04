using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
	
	public bool canTripleShot = false;
	public bool shieldActive = false;

	[SerializeField]
	private GameObject _laserPrefab;
	[SerializeField]
	private GameObject _tripleShotPrefab;
	[SerializeField]
	private GameObject _explosionPrefab;
	[SerializeField]
	private GameObject _shieldGameObject;
	[SerializeField]
	private GameObject[] _engines;
	[SerializeField]
	private float _fireRate = 0.25f;
	private float _canFire = 0.0f;

	[SerializeField]
	private float _speed = 5.0f;

	[SerializeField]
	private int _lives = 3;

	private int hitCount;

	private GameManager _gameManager;
	private UIManager _uiManager;
	private Spawn_Manager _spawnManager;
	private AudioSource _audioSource;


	void Start ()
	{
		hitCount = 0;
		transform.position = new Vector3 (0, 0, 0);
		_uiManager = GameObject.Find ("Canvas").GetComponent<UIManager> ();
		_gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		_spawnManager = GameObject.Find ("Spawn_Manager").GetComponent<Spawn_Manager> ();

		if (_uiManager != null) {
			_uiManager.UpdateLives (_lives);
		}

		if (_spawnManager != null) {
			_spawnManager.StartSpawnRoutine();
		}

		_audioSource = GetComponent<AudioSource> ();
	}


	void Update ()
	{
		Movement ();

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButton (0)) {
			Shoot ();
		}


	}

	private void Shoot ()
	{

		if (Time.time > _canFire) {
            
			_audioSource.Play ();
			if (canTripleShot) {
				Instantiate (_tripleShotPrefab, transform.position, Quaternion.identity);
			} else {
				Instantiate (_laserPrefab, transform.position + new Vector3 (0, 0.88f, 0), Quaternion.identity);

			}
			_canFire = Time.time + _fireRate;
		}
	}

	private void Movement ()
	{

		float horizontalInput = Input.GetAxis ("Horizontal");
		float verticallInput = Input.GetAxis ("Vertical");

		transform.Translate (Vector3.right * _speed * horizontalInput * Time.deltaTime);
		transform.Translate (Vector3.up * _speed * verticallInput * Time.deltaTime);

		if (transform.position.y > 0) {
			transform.position = new Vector3 (transform.position.x, 0, 0);
		} else if (transform.position.y < -4.2f) {
			transform.position = new Vector3 (transform.position.x, -4.2f, 0);
		}

		if (transform.position.x > 9.5f) {
			transform.position = new Vector3 (-9.5f, transform.position.y, 0);
		} else if (transform.position.x < -9.5f) {
			transform.position = new Vector3 (9.5f, transform.position.y, 0);
		}

	}



	public IEnumerator TripleShotPowerDownRoutine ()
	{
		yield return new WaitForSeconds (5.0f);
		canTripleShot = false;
	}

	public void TripleShotPowerupOn ()
	{
		canTripleShot = true;
		StartCoroutine (TripleShotPowerDownRoutine ());
	}

	public IEnumerator SpeedBoostPowerDownRoutine ()
	{
		yield return new WaitForSeconds (5.0f);
		_speed = 5.0f;
	}

	public void SpeedBoostPowerupOn ()
	{
		_speed *= 1.5f;
		StartCoroutine (SpeedBoostPowerDownRoutine ());
	}

	public void ShieldPowerOn ()
	{
		shieldActive = true;
		_shieldGameObject.SetActive (true);
	}

	public void Damage ()
	{
		
		if (shieldActive) {
			shieldActive = false;
			_shieldGameObject.SetActive (false);
			return;
		}
		_lives--;
		_uiManager.UpdateLives (_lives);
		hitCount++;
		if(hitCount == 1){
			_engines [0].SetActive (true);
		}
		else if(hitCount == 2){
			_engines [1].SetActive (true);
		}

		if (_lives < 1) {
			Destroy (gameObject);
			Instantiate (_explosionPrefab, transform.position + new Vector3 (0, 0.88f, 0), Quaternion.identity);
			_gameManager.gameOver = true;
			_uiManager.ShowTitleScreen ();
		}
	}


}