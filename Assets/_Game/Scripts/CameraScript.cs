using System.Collections;
using UnityEngine;

public class CameraScript:MonoBehaviour {

	public float moveSpeed;
	public float zoomSpeed;
	public float camZoom;
	public int maxFov;
	public int minFov;
	public float yMargin;
	public float xMargin;
	Rect recDown;
	Rect recUp;
	Rect recLeft;
	Rect recRight;
	public bool panning;

	Vector3 moveDistance;

	public Texture2D[] arrows;

	// Use this for initialization
	void Start() {
		yMargin = Screen.height / 100;
		xMargin = Screen.width / 100;
		recDown = new Rect(0,0,Screen.width,yMargin);
		recUp = new Rect(0,Screen.height - yMargin,Screen.width,yMargin);
		recLeft = new Rect(0,0,xMargin,Screen.height);
		recRight = new Rect(Screen.width - xMargin,0,xMargin,Screen.height);
	}

	// Update is called once per frame
	void Update() {
		EdgePan();
		ButtonPan();
		ScrollZoom();
	}

	void LateUpdate() {
		transform.Translate(moveDistance *moveSpeed * (0.1f * Camera.main.orthographicSize) * Time.unscaledDeltaTime, Space.Self);

		if(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)) {
			Cursor.SetCursor(arrows[4], new Vector2(4,0), CursorMode.Auto);
		} else if(moveDistance.magnitude==0){
			Cursor.SetCursor(null, new Vector2(4,0), CursorMode.Auto);
		}
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x,-Global.map.terrainSize * Global.map.tileScale, 0);
		pos.z = Mathf.Clamp(pos.z,-Global.map.terrainSize * Global.map.tileScale, 0);
		transform.position = pos;

		moveDistance = Vector3.zero;
	}

	//pans the camera from the edges
	void EdgePan() {
		if(panning)
			return;

		if(recDown.Contains(Input.mousePosition) || Input.GetKey(KeyCode.DownArrow)) {
			Cursor.SetCursor(arrows[0],new Vector2(11,16),CursorMode.Auto);
			moveDistance.z = -1;
		}
		if(recUp.Contains(Input.mousePosition) || Input.GetKey(KeyCode.UpArrow)) {
			Cursor.SetCursor(arrows[1],new Vector2(11,0),CursorMode.Auto);
			moveDistance.z = 1;
		}
		if(recLeft.Contains(Input.mousePosition) || Input.GetKey(KeyCode.LeftArrow)) {
			Cursor.SetCursor(arrows[2],new Vector2(0,11),CursorMode.Auto);
			moveDistance.x = -1;
		}
		if(recRight.Contains(Input.mousePosition) || Input.GetKey(KeyCode.RightArrow)) {
			Cursor.SetCursor(arrows[3],new Vector2(16,11),CursorMode.Auto);
			moveDistance.x = 1;
		}
		
		if(moveDistance.magnitude == 0) {
			Cursor.SetCursor(null,new Vector2(4,0),CursorMode.Auto);
		}
	}

	void ButtonPan() {
		if(Input.GetMouseButton(2)) {
			panning = true;
			moveDistance.x = -Input.GetAxisRaw("Mouse X");
			moveDistance.z = -Input.GetAxisRaw("Mouse Y");
		} else {
			panning = false;
		}
	}

	void ScrollZoom() {
		camZoom = Input.GetAxisRaw("Mouse ScrollWheel");
		Camera.main.orthographicSize += -camZoom*zoomSpeed * Camera.main.orthographicSize * Time.unscaledDeltaTime;
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize,minFov,maxFov);
	}

	//void OnGUI() {
	//	if(arrowIndex != null)
	//		GUI.DrawTexture(new Rect(Input.mousePosition.x,Screen.height - Input.mousePosition.y,32,32),arrows[(int)arrowIndex]);

	//}
}