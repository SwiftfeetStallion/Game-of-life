using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Game_manage : MonoBehaviour {
  [SerializeField] private Player[] _players;
  [SerializeField] private Text[] _texts;
  [SerializeField] private GameField _field;
  [SerializeField] private int _player = 0;
  [SerializeField] private bool _is_paused = false;
  [SerializeField] private ScaleControl _scale_control;
  [SerializeField] private Dropdown _dropdown;
  [SerializeField] private Starter _starter;
  [SerializeField] private Ender _ender;
  private bool _pattern_chosen = false;


  private void SetPlayer(int num) {
    _player = num;
    _field.SetPlayer(_players[num]);
  }

  public (int, int) GetScore() {
    return _field.ScoreCount();
  }

  private void Pause() {
    _is_paused = true;
    Time.timeScale = 0;
  }

  private void OnPause() {
    if (Input.GetKeyDown(KeyCode.Space)) {
      _is_paused = !_is_paused;
      Time.timeScale = 1 - Time.timeScale;
    }
    if (_is_paused && Input.GetMouseButtonDown(0)) {
      if (!_pattern_chosen) {
        _field.AddByClick();
      } else {
        int index = _dropdown.value;
        _field.SetPattern(index);
      }
    }
  }

  private void DropdownOpen() {
    if (Input.GetKeyDown(KeyCode.M) && _is_paused && !_pattern_chosen) {
       _pattern_chosen = true;
       _dropdown.GameObject().SetActive(true);
    } else if (Input.GetKeyDown(KeyCode.M) && _is_paused && _pattern_chosen) {
      _pattern_chosen = false;
      _dropdown.GameObject().SetActive(false);
    }
  }

  private IEnumerator ShowText(int num) {
    _texts[1 - num].text = "";
    _texts[num].text = "Switched on player " + (num + 1).ToString();
    yield return new WaitForSeconds(0.5f);
    _texts[num].text = "";
  }

  private void SpeedChange() {
    if (Input.GetKeyDown(KeyCode.I)) {
      _field.SpeedUp();
    }
    if (Input.GetKeyDown(KeyCode.S)) {
      _field.SlowDown();
    }
  }

  public void Exit() {
    _field.GameObject().SetActive(false);
    _ender.GameObject().SetActive(true);
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.P)) {
      SetPlayer(1 - _player);
      StartCoroutine(ShowText(_player));
    }

    _scale_control.Move();
    _scale_control.Scale();
    DropdownOpen();
    SpeedChange();
    OnPause();
  }

  private void OnEnable() {
    _field.GameObject().SetActive(true);
    _field.Clear();
    _field.SetField();
    _dropdown.GameObject().SetActive(false);
    _starter.GameObject().SetActive(false);
    Pause();
    if (_players[0].IsRand()) {
      _field.SetPlayer(_players[0]);
      SetPlayer(0);
      _field.RandomGenerate();
    }
    if (_players[1].IsRand()) {
      _field.SetPlayer(_players[1]);
      SetPlayer(1);
      _field.RandomGenerate();
    }
    StartCoroutine(_field.Simulation());
  }

  
}
