using UnityEngine;

public class UIMechaDisplayController : MonoBehaviour {
  public Color colorOn = Color.white;
  public Color colorOff = Color.clear;
  public int number = 1;

  public float colorGravity = 0.6f;
  public float[] status = new float[20], target = new float[20];

  public void SetNumber ( int alpha, bool duplicateOverride = false ) {
    if ( alpha == number && !duplicateOverride ) return;
    if ( alpha >= 0 && alpha <= 10 ) {
      number = alpha;
    }
    for ( int i = 0; i < 20; i++ ) {
      target[i] = 0;
    }
    for ( int i = 0; i < matrix[number].Length; i++ ) {
      target[matrix[number][i]] = 1;
    }
  }

  int[][] matrix = new int[][]
{
    new int[] { 4, 5, 6, 7, 10, 11, 14, 15 },
    new int[] { 0, 1, 3, 4, 5, 6, 7, 9, 10, 11, 13, 14, 15, 17, 18 },
    new int[] { 3, 4, 5, 9, 11, 12, 15, 16 },
    new int[] { 3, 4, 9, 12, 13, 14 },
    new int[] { 1, 4, 5, 6, 7, 17, 18 },
    new int[] { 6, 7, 8, 9, 10, 12, 13, 14 },
    new int[] { 2, 6, 7, 8, 10, 12, 14 },
    new int[] { 3, 4, 5, 6, 7, 9, 10, 11, 13, 14, 15, 17, 18 },
    new int[] { 9, 12 },
    new int[] { 6, 7, 9, 10, 13, 14, 17 },
    new int[] { 9, 1, 12, 18 }
};
  private void Start () {
    SetNumber ( number, true );
  }

  private void Update () {
    for ( int i = 0; i < 20; i++ ) {
      status[i] = Mathf.Lerp ( status[i], target[i], colorGravity * Time.deltaTime );
      transform.GetChild ( i ).GetComponent<SpriteRenderer> ().color = Color.Lerp ( colorOn, colorOff, status[i] );
    }
  }
}
