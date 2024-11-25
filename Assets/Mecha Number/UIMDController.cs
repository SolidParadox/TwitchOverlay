  using UnityEngine;

public class UIMDController : MonoBehaviour {
  private UIMechaDisplayController [ ] digits;
  protected int pastTarget = -1;

  // WARNING : NO PROTECTION AGAINST NEGATIVE NUMBERS, WILL NOT ENFORCE SO IT'S FASTER

  private void Start () {
    digits = new UIMechaDisplayController [ transform.childCount ];
    for ( int i = 0; i < transform.childCount; i++ ) {
      digits [ transform.childCount - i - 1] = transform.GetChild ( i ).GetComponent<UIMechaDisplayController> ();
    }
    SetDisplay ( 0 );
  }

  public virtual void SetDisplay ( int target, bool force = false ) {
    if ( target != pastTarget || force ) {
      pastTarget = target;
      for ( int i = 0; i < digits.Length; i++ ) {
        digits [ i ].SetNumber ( target % 10, force );
        target /= 10;
      }
    }
  }

  public void SetActiveColor ( Color c ) {
    for ( int i = 0; i < digits.Length; i++ ) {
      digits[i].colorOn = c;
    }
  }

  public void SetInactiveColor ( Color c ) {
    for ( int i = 0; i < digits.Length; i++ ) {
      digits[i].colorOff = c;
    }
  }
}
