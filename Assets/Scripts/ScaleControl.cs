using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ScaleControl : MonoBehaviour {
 [SerializeField] private GameField _field;
 private int _size = 50;
 [SerializeField] private Camera _camera;

 public void Move() {
  if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < _size / 3f) {
    transform.position += new Vector3(0, 0.1f, 0);
  }
  if (Input.GetKey(KeyCode.DownArrow) && transform.position.y >- _size / 3f) {
    transform.position += new Vector3(0, -0.1f, 0);
  }
  if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -_size / 3f) {
    transform.position += new Vector3(-0.1f, 0, 0);
  }
  if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < _size / 3f) {
    transform.position += new Vector3(0.1f, 0, 0);
  }
 }

 public void Scale() {
  if (Input.GetKeyDown(KeyCode.Equals) && _camera.orthographicSize > 2) {
   _camera.orthographicSize /= 1.5f;
  }
  if (Input.GetKeyDown(KeyCode.Minus) && _camera.orthographicSize * 1.5f < 15) {
    _camera.orthographicSize *= 1.5f;
  }

 }

 private void Start() {
  _size = _field.GetSize();
  Debug.Log(_camera.orthographicSize / 2f);
 }
}
