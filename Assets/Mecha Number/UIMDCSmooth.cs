using UnityEngine;

public class UIMDCSmooth : UIMDController {
  public float smoothTime;
  private float deltaTime = -1;

  private int pointA, pointB;

  public override void SetDisplay ( int target, bool force = false ) {
    if ( target != pointB ) {
      pointA = pastTarget;
      pointB = target;
      deltaTime = 0;
    }
  }

  private void LateUpdate () {
    if ( deltaTime >= 0 ) {
      base.SetDisplay ( Mathf.RoundToInt ( Mathf.Lerp ( pointA, pointB, deltaTime / smoothTime ) ) );
      deltaTime += Time.unscaledDeltaTime;
      if ( deltaTime > smoothTime ) {
        deltaTime = -1;
        base.SetDisplay ( pointB );
      }
    }
  }
}
