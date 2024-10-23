using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScore : MonoBehaviour {
  private string _init_text;
  [SerializeField] private Text _text;
  [SerializeField] private Player _player;
  [SerializeField] private int _num;

  private void Start() {
    _init_text = "Score_" + _num.ToString() + ": ";
    _text.text = _init_text + "0";
  }

  private void Update() {
    _text.text = _init_text + _player.GetScore().ToString();
  }

}
