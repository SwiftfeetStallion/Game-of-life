using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ender : MonoBehaviour {
  [SerializeField] private Canvas _end_canvas;
  [SerializeField] private Canvas _play_canvas;
  [SerializeField] private Game_manage _game_manage;
  [SerializeField] private Starter _starter;
  [SerializeField] private Text _score_1;
  [SerializeField] private Text _score_2;
  [SerializeField] private Text _winner;
  private void OnEnable() {
    _play_canvas.GameObject().SetActive(false);
    _game_manage.GameObject().SetActive(false);
    (int score_1, int score_2) = _game_manage.GetScore();
    string winner = "";
    if (score_1 > score_2) {
      winner = "player 1";
    } else if (score_1 < score_2) {
      winner = "player 2";
    } else {
      winner = "undefined";
    }
    _score_1.text = "Player 1" + '\n' + '\n' + "Field score: " + score_1.ToString(); 
    _score_2.text = "Player 2" + '\n' + '\n' + "Field score: " + score_2.ToString(); 
    _winner.text = "The winner is " + winner.ToString(); 
    _end_canvas.GameObject().SetActive(true);
  }

  public void Restart() {
    _end_canvas.GameObject().SetActive(false);
    _starter.GameObject().SetActive(true);
  }

}
