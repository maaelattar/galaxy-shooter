using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

     public bool gameOver = true;
     private UIManager _uiManager;
     [SerializeField]
     private GameObject _player;
     // Start is called before the first frame update
     void Start()
     {
          _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

     }

     // Update is called once per frame
     void Update()
     {
          if (gameOver) {
               if (Input.GetKeyDown(KeyCode.Space)) {
                    Instantiate(_player, Vector3.zero, Quaternion.identity);
                    gameOver = false;
                    _uiManager.HidetitleScreen();
               }

          }

          if (Input.GetKeyDown(KeyCode.Escape))
          {
               Application.Quit();
          }
     }

  

}
