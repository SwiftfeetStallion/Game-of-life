using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour {
  [SerializeField] private Player _player_1;
  [SerializeField] private Player _player_2;
  [SerializeField] private Game_manage _game;
  [SerializeField] private Canvas _start_canvas;
  [SerializeField] private Canvas _end_canvas;
  [SerializeField] private Canvas _play_canvas;
  [SerializeField] private Toggle _toggle_1;
  [SerializeField] private Toggle _toggle_2;
  [SerializeField] private Ender _ender;


  private void OnEnable() {
    Debug.Log(Screen.currentResolution);
    _toggle_1.isOn = false;
    _toggle_2.isOn = false;
    _ender.GameObject().SetActive(false);
    _play_canvas.GameObject().SetActive(false);
    _end_canvas.GameObject().SetActive(false);
    _game.GameObject().SetActive(false);
    _start_canvas.GameObject().SetActive(true);
  }

  public void OnStart() {
    _player_1.SetRand(_toggle_1.isOn);
    _player_2.SetRand(_toggle_2.isOn);
    _player_1.ClearScore();
    _player_2.ClearScore();
    _start_canvas.GameObject().SetActive(false);
    _play_canvas.GameObject().SetActive(true);
    _game.GameObject().SetActive(true);
  }
  
}
