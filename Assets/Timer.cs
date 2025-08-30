using UnityEngine;

public class Timer : MonoBehaviour {
  public float time;
  public float elapsed;

  public Color colON, colOFF;

  public UIMDController cH, cM, cS;

  public SpriteRenderer sr1;

  private bool oldPV, paused, hasChanged;

  private int selPos = 0;

  public enum TimerMode { Countdown, Timer }
  public TimerMode mode = TimerMode.Countdown;

  public SpriteRenderer dot1, dot2;
  public SpriteRenderer sel1, sel2;

  public Transform selector;
  public GameObject pausedLed;
  public GameObject stopwatchLed;
  public Animator selAnim;

  private void Start () {
    oldPV = true;
    paused = false;
    pausedLed.SetActive ( false );
    stopwatchLed.SetActive ( false );
  }

  void Update () {
    selAnim.SetBool ( "Transition", false );

    if ( Input.GetKeyDown (KeyCode.Space) ) {
      paused = !paused;
      pausedLed.SetActive ( paused );
    }

    if ( Input.GetKeyDown(KeyCode.E) && paused) {
      if ( mode == TimerMode.Timer ) mode = TimerMode.Countdown; else mode = TimerMode.Timer;
      stopwatchLed.SetActive ( mode == TimerMode.Timer );
    }

    bool pulse = Mathf.Repeat (time, 1 ) > 0.5f;
    
    dot1.color = pulse ? colON : colOFF;
    dot2.color = pulse ? colON : colOFF;

    sel1.color = paused ? colON : colOFF;
    sel2.color = paused ? colON : colOFF;

    selector.position = new Vector3 ( 2.75f, 0, 0 ) * selPos; 

    if ( !paused ) {
      if ( mode == TimerMode.Countdown ) { 
        time -= Time.deltaTime; 
      } else {
        time += Time.deltaTime;
      }
    } else {
      if ( Input.GetKeyDown ( KeyCode.LeftArrow ) ) {
        selAnim.SetBool ( "Transition", true );
        selPos--; if ( selPos < -1 ) selPos = 1;
      }
      if ( Input.GetKeyDown ( KeyCode.RightArrow ) ) {
        selAnim.SetBool ( "Transition", true );
        selPos++; if ( selPos > 1 ) selPos = -1;
      }
      float modifiedInput = Mathf.RoundToInt ( Input.GetAxis ( "Vertical" ) ) * Time.deltaTime;
      switch ( selPos ) {
        case 1:
          time += modifiedInput * 15;
          break;
        case 0:
          time += modifiedInput * 60 * 3;
          break;
        case -1:
          time += modifiedInput * 3600;
          break;
      }
    }

    if ( paused != oldPV ) {
      oldPV = paused;
      hasChanged = true;
      if ( paused ) {
        sr1.color = colON;
        cH.SetActiveColor ( colOFF );
        cM.SetActiveColor ( colOFF );
        cS.SetActiveColor ( colOFF );
      } else {
        sr1.color = colOFF;
        cH.SetActiveColor ( colON );
        cM.SetActiveColor ( colON );
        cS.SetActiveColor ( colON );
      }
    }

    cH.SetDisplay ( Mathf.FloorToInt ( time / 3600 ), hasChanged );
    cM.SetDisplay ( Mathf.FloorToInt ( time / 60 ) % 60, hasChanged );
    cS.SetDisplay ( Mathf.FloorToInt ( time ) % 60, hasChanged );

    if ( time < 0 ) {
      time = 0;
    }

    if ( time < 0 ) {
      enabled = false;
    }
    hasChanged = false;
  }
}
