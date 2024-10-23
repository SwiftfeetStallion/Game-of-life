using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {
  [SerializeField] private Tile _alive;
  private int _score = 0;
  private bool _status = false; // отвечает за то, что у игрока уже была отмеченная клетка
  [SerializeField] private bool _rand_pos = false;

  public Tile GetAlive() { return _alive; }

  public void UpScore() { ++_score; _status = true; }
  public void DownScore() { --_score; }
  public int GetScore() { return _score; }
  public void ClearScore() { _score = 0; }
  public bool IsRand() { return _rand_pos; }
  public void SetRand(bool rand) { _rand_pos = rand; }
  public bool Status() { return _status; }

}
