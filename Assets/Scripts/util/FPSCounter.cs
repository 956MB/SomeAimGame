using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour {
	public TMP_Text fpsText;
    private float deltaTime = 0.0f;
	private float msec, fps;

	//void Update() {
 //       deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
 //   }

  //  void OnGUI() {
		//if (ToggleHandler.FPSCounterOn()) {
		//	deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		//	msec = (deltaTime * 1000.0f) / 2;
		//	fps = (1.0f / deltaTime) * 2;
		//	fpsText.SetText($"{fps:0.}fps  ({msec:0.0}ms)");
  //      }
  //  }

    /*
	public float updateInterval = 0.05F;

	private float accum = 0; // FPS accumulated over the interval
	private int frames = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval

	void Start() {
		timeleft = updateInterval;
	}

	void Update() {
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		++frames;

		// Interval ended - update GUI text and start new interval
		if (timeleft <= 0.0) {
			float fps = (accum / frames) * 2;
			string format = $"{fps:0.}fps";
			fpsText.SetText(format);

			if (fps < 30)
				fpsText.color = Color.yellow;
			else
				if (fps < 10)
				fpsText.color = Color.red;

			//	DebugConsole.Log(format,level);
			timeleft = updateInterval;
			accum = 0.0F;
			frames = 0;
		}
	}

	*/
}