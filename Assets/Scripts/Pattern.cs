using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Pattern")]
public class Pattern : ScriptableObject {
  public Vector2Int[] _pattern;
  public string _name;

  public void Create(Vector2Int[] cells) {
    _pattern = cells;
  } 
}
