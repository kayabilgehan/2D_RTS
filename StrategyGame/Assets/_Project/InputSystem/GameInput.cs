using UnityEngine;

public class GameInput : MonoBehaviour
{
	private static GameInput instance;

	private bool leftMouseDown = false;
	private bool leftMouseHold = false;
	private bool leftMouseUp = false;

	private bool rightMouseDown = false;
	private bool rightMouseHold = false;
	private bool rightMouseUp = false;

	private bool middleMouseDown = false;
	private bool middleMouseHold = false;
	private bool middleMouseUp = false;

	public static GameInput Instance => instance;

	public bool LeftMouseDown => leftMouseDown;
	public bool LeftMouseHold => leftMouseHold;
	public bool LeftMouseUp => leftMouseUp;

	public bool RightMouseDown => rightMouseDown;
	public bool RightMouseHold => rightMouseHold;
	public bool RightMouseUp => rightMouseUp;

	public bool MiddleMouseDown => middleMouseDown;
	public bool MiddleMouseHold => middleMouseHold;
	public bool MiddleMouseUp => middleMouseUp;

	public Vector3 GetMousePosition() {
		return Input.mousePosition;
	}
	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		else {
			Destroy(this.gameObject);
		}
	}

	public Vector2 GetMouseScrollValue() {
		return Input.mouseScrollDelta;
	}
	public bool GetEscButton() {
		return Input.GetKeyDown(KeyCode.Escape);
	}

	private void Update() {
		ResetValues();

		leftMouseDown = Input.GetMouseButtonDown(0);
		leftMouseHold = Input.GetMouseButton(0);
		leftMouseUp = Input.GetMouseButtonUp(0);

		rightMouseDown = Input.GetMouseButtonDown(1);
		rightMouseHold = Input.GetMouseButton(1);
		rightMouseUp = Input.GetMouseButtonUp(1);

		middleMouseDown = Input.GetMouseButtonDown(2);
		middleMouseHold = Input.GetMouseButton(2);
		middleMouseUp = Input.GetMouseButtonUp(2);
	}

	private void ResetValues() {
		leftMouseDown = false;
		leftMouseHold = false;
		leftMouseUp = false;

		rightMouseDown = false;
		rightMouseHold = false;
		rightMouseUp = false;

		middleMouseDown = false;
		middleMouseHold = false;
		middleMouseUp = false;
	}
}
