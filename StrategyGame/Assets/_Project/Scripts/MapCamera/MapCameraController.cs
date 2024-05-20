using StrategyGame.AStar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraController : MonoBehaviour {
	[SerializeField] private Camera mapCamera;
	[SerializeField] private AStarGrid aStarGrid;
	[SerializeField] private float zoomLimitMin = 2;
	[SerializeField] private float zoomLimitMax = 15;
	[SerializeField] private float zoomSpeed = 10f;
	[SerializeField] private int mapMargin = 3;

	private Vector3 mouseScrollStartPos;
	private float mapZoom = 0;

	private void Start() {
		mapZoom = mapCamera.orthographicSize;
	}

	private void Update() {
		HandleMiddleMouse();
		CheckMapLimits();

		ControlMapZoom();
	}

	private void ControlMapZoom() {
		Vector2 mouseScrollValue = GameInput.Instance.GetMouseScrollValue();
		if (mouseScrollValue.y != 0) {
			mapZoom -= mouseScrollValue.y * Time.deltaTime * zoomSpeed;
			mapZoom = Mathf.Clamp(mapZoom, zoomLimitMin, zoomLimitMax);
			mapCamera.orthographicSize = mapZoom;
		}
	}

	private void HandleMiddleMouse() {
		if (GameInput.Instance.MiddleMouseDown) {
			mouseScrollStartPos = mapCamera.ScreenToWorldPoint(GameInput.Instance.GetMousePosition());
		}
		Vector3 movement = Vector3.zero;
		if (GameInput.Instance.MiddleMouseHold) {
			movement = mapCamera.ScreenToWorldPoint(GameInput.Instance.GetMousePosition()) - mouseScrollStartPos;
		}

		mapCamera.transform.position -= movement;
	}

	private void CheckMapLimits() {
		Vector2 margins = - mapCamera.ScreenToWorldPoint(Vector2.zero) + mapCamera.ScreenToWorldPoint(new Vector2(mapCamera.pixelWidth, mapCamera.pixelHeight));

		Rect boundaries = aStarGrid.MapLimits();
		boundaries.width -= margins.x - (mapMargin * 2);
		boundaries.x += (margins.x / 2) - mapMargin;
		boundaries.height -= margins.y - (mapMargin * 2);
		boundaries.y += (margins.y / 2) - mapMargin;

		Vector3 cameraPosition = mapCamera.transform.position;
		if (boundaries.xMin > cameraPosition.x) {
			mapCamera.transform.position = new Vector3(boundaries.xMin, mapCamera.transform.position.y, mapCamera.transform.position.z);
		}
		if (boundaries.xMax < cameraPosition.x) {
			mapCamera.transform.position = new Vector3(boundaries.xMax, mapCamera.transform.position.y, mapCamera.transform.position.z);
		}
		if (boundaries.yMin > cameraPosition.y) {
			mapCamera.transform.position = new Vector3(mapCamera.transform.position.x, boundaries.yMin, mapCamera.transform.position.z);
		}
		if (boundaries.yMax < cameraPosition.y) {
			mapCamera.transform.position = new Vector3(mapCamera.transform.position.x, boundaries.yMax, mapCamera.transform.position.z);
		}
	}
}


