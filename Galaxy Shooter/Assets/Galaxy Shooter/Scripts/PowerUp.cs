using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
	[SerializeField]
	private int powerupId;
	private AudioSource _audioSource;
	[SerializeField]
	private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if(transform.position.y < -7.0f){
			Destroy (gameObject);
		}
    }

    private void OnTriggerEnter2D(Collider2D other) {
       
       if (other.tag == "Player")
       {
           Player player = other.GetComponent<Player>();
			AudioSource.PlayClipAtPoint (_clip, Camera.main.transform.position);
           if(player != null){

				if (powerupId == 0) {
					player.TripleShotPowerupOn ();
				}
				else if(powerupId == 1){
					player.SpeedBoostPowerupOn ();
				}
				else if(powerupId == 2){
					player.ShieldPowerOn ();
				}

           }



			Destroy (gameObject);

           
       }

    }


}
