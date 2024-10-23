using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameField : MonoBehaviour {
  [SerializeField] private Tilemap _current_state;
  [SerializeField] private Tilemap _next_state;
  [SerializeField] private Player[] _players;
  [SerializeField] private Player _current_player;
  [SerializeField] private Tile _dead;
  [SerializeField] private Pattern[] _patterns;
  [SerializeField] private int _size = 100;
  [SerializeField] private float _interval = 0.1f;
  private HashSet<Vector3Int> _alive_cells;
  private HashSet<Vector3Int> _check_cells;


 public void SetPattern(int ind) {
  if (EventSystem.current.IsPointerOverGameObject()) {
    return;
  }
  var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
  var cell = _current_state.WorldToCell(point);
  for (int i = 0; i < _patterns[ind]._pattern.Length; ++i) {
   Vector3Int position = (Vector3Int)_patterns[ind]._pattern[i] + cell;
    if (!_alive_cells.Contains(position)) {
      _current_state.SetTile(position, _current_player.GetAlive());
      _alive_cells.Add(position);
      _current_player.UpScore();
    }
  } 
 }

 public void SpeedUp() { _interval /= 1.5f; }

 public void SlowDown() { _interval *= 1.5f; }

 public int GetSize() { return _size; }

 public void SetPlayer(Player player)  {
  _current_player = player; 
 }
 
 public void Clear() {
  _current_state.ClearAllTiles();
  _next_state.ClearAllTiles();
 }

 public void SetField() {  // начальное определение размеров поля
  for (int i = -_size; i < _size; ++i) {
   for (int j = -_size; j < _size; ++j) {
     _current_state.SetTile(new Vector3Int(i, j, 0), _dead);  
   }
  } 
 }

 private void UpdateState() {
  _check_cells.Clear();
  for (int i = -_size; i <= _size; ++i) {  // для отображения мёртвых клеток на поле в каждом кадре
    for (int j = -_size; j <=_size; ++j) {
      _next_state.SetTile(new Vector3Int(i, j, 0), _dead); 
    }
  }

  foreach (Vector3Int cell in _alive_cells) {
    for (int i = -1; i <= 1; ++i) {
      for (int j = -1; j <= 1; ++j) {
        _check_cells.Add(cell + new Vector3Int(i, j, 0));
      }
    }
  }
  foreach (Vector3Int cell in _check_cells) {
    var alive_cells = CountAliveNeigh(cell);
    if (IsAlive(cell) && alive_cells.Item3 > 3) {
      int ind = (_current_state.GetTile(cell) == _players[1].GetAlive()) ? 1 : 0;
      _players[ind].DownScore();
      _next_state.SetTile(cell, _dead);
      _alive_cells.Remove(cell);
    } else if (IsAlive(cell) && alive_cells.Item3 < 2) {
      int ind = (_current_state.GetTile(cell) == _players[1].GetAlive()) ? 1 : 0;
      _players[ind].DownScore();
      _next_state.SetTile(cell, _dead);
      _alive_cells.Remove(cell);
    } else if (!IsAlive(cell) && alive_cells.Item3 == 3) {
      int ind = (alive_cells.Item2 > alive_cells.Item1) ? 1 : 0;
      _players[ind].UpScore();
      _next_state.SetTile(cell, _players[ind].GetAlive());
      _alive_cells.Add(cell);
    } else {
       _next_state.SetTile(cell, _current_state.GetTile(cell));
    }
  }
  (_current_state, _next_state) = (_next_state, _current_state);
  _next_state.ClearAllTiles();
 }

 private (int, int, int) CountAliveNeigh(Vector3Int cell) {
  int count_1 = 0;
  int count_2 = 0;
  for (int i = -1; i <= 1; ++i) {
     for (int j = -1; j <= 1; ++j) {
       if (i == 0 && j == 0) {
         continue;
       }
       Vector3Int position = cell + new Vector3Int(i, j, 0);
       if (IsAlive_1(position)) {
         ++count_1;
       } else if (IsAlive_2(position)) {
         ++count_2;
       }
     }
  }
    return (count_1, count_2, count_1 + count_2);
 }

 private bool IsAlive(Vector3Int cell) {
  return _current_state.GetTile(cell) == _players[0].GetAlive() ||
         _current_state.GetTile(cell) == _players[1].GetAlive();
 }

 private bool IsAlive_1(Vector3Int cell) {
  return _current_state.GetTile(cell) == _players[0].GetAlive();
 }
 private bool IsAlive_2(Vector3Int cell) {
  return _current_state.GetTile(cell) == _players[1].GetAlive();
 }

 public (int, int) AliveCount() {
  int count_1 = 0;
  int count_2 = 0;
  foreach (var cell in _alive_cells) {
    if (_current_state.GetTile(cell) == _players[0].GetAlive()) {
      ++count_1;
    } else {
      ++count_2;
    }
  }
  return (count_1, count_2);
 }

 public (int, int) ScoreCount() {
  int count_1 = 0;
  int count_2 = 0;
  for (int i = -_size; i < _size; ++i) {
    for (int j = -_size; j < _size; ++j) {
      if (_current_state.GetTile(new Vector3Int(i, j, 0)) == _players[0].GetAlive()) {
        ++count_1;
      } else if (_current_state.GetTile(new Vector3Int(i, j, 0)) == _players[1].GetAlive()){
        ++count_2;
      }
    }
  }
  return (count_1, count_2);
 }

 public void RandomGenerate() {
  var rand = new System.Random();
  int amount = rand.Next(1, _size * _size); // количество клеток
  for (int i = 0; i < amount; ++i) {
    Vector3Int cell = new Vector3Int(rand.Next(-_size + 1, _size), rand.Next(-_size + 1, _size), 0);
    if (!_alive_cells.Contains(cell)) {
      _alive_cells.Add(cell);
      _current_state.SetTile(cell, _current_player.GetAlive());
      _current_player.UpScore();
    }
  }
 }

 public void AddByClick() {
  if (EventSystem.current.IsPointerOverGameObject()) {
    return;
  }
  var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
  var cell = _current_state.WorldToCell(point);
  if (!_alive_cells.Contains(cell)) {
    _current_state.SetTile(cell, _current_player.GetAlive());
    _current_player.UpScore();
    _alive_cells.Add(cell);
  }
 }

 public IEnumerator Simulation() {
  yield return new WaitForSeconds(_interval);
  while (enabled) {
    UpdateState();
    yield return new WaitForSeconds(_interval);
  } 
 }

 private void OnEnable() {
  _alive_cells = new HashSet<Vector3Int>();
  _check_cells = new HashSet<Vector3Int>();   
 }

 
}
