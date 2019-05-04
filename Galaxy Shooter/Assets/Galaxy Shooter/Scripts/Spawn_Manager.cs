using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
	[SerializeField]
	private GameObject _playerPrefab;
	[SerializeField]
	private GameObject _enemyShipPrefab;
	[SerializeField]
	private GameObject[] _powerups;
	private GameManager _gameManager;

	private Player _player;
	// Start is called before the first frame update
	void Start ()
	{
		_gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		StartCoroutine (EnemySpawnRoutine ());
		StartCoroutine (PowerupSpawnRoutine ());
	
	}

	public void StartSpawnRoutine(){
		StartCoroutine (EnemySpawnRoutine ());
		StartCoroutine (PowerupSpawnRoutine ());
	}

	public IEnumerator EnemySpawnRoutine ()
	{
		while (!_gameManager.gameOver) {
			Instantiate (_enemyShipPrefab, new Vector3 (Random.Range (-7.0f, 7.0f), 7.0f, 0), Quaternion.identity);
			yield return new WaitForSeconds (5.0f); 
		}
			
	}

	public IEnumerator PowerupSpawnRoutine ()
	{
		while (!_gameManager.gameOver) {
			int randomPowerup = Random.Range (0, 3);
			Instantiate (_powerups [randomPowerup], new Vector3 (Random.Range (-7.0f, 7.0f), 7.0f, 0), Quaternion.identity);
			yield return new WaitForSeconds (5.0f); 
		}
	}
		

}

