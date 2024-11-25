using UnityEngine;

public class UISpeedometer : UIMDController {
  public Rigidbody2D rgb;
  void LateUpdate () {
    SetDisplay ( Mathf.RoundToInt ( rgb.velocity.magnitude * 3.6f ) );
  }
}