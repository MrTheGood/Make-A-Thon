using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ButtonClass {
	public RectTransform trans;
	public Vector2 offset;
}

public class uGUIMinimap : MonoBehaviour {

	[System.Serializable]
	public struct OffsetSize {
		public float x;
		public float y;
		public float sizeX;
		public float sizeY;
	}

	private Camera minimapCamera;
	public Image frame;
	private CanvasScaler canvasScaler;
	
	private Transform minimap;
	private Transform depthPlane;
	private Transform playerCircle;
	private Transform target;
	public RectTransform header;
	public Text headerAreaText;

	public OffsetSize minimapOffsetSize;
	
	public float zoomAmount = 2f;
	public int minSize;
	public int maxSize;

	public float buttonSize;

	public Vector3 headerOffset;

	public List<ButtonClass> buttons;

	// Use this for initialization
	void Start () {
		//Get the canvasScaler reference
		canvasScaler = GetComponent<CanvasScaler>();

		minimapCamera = GameObject.FindGameObjectWithTag("MinimapCamera").GetComponent<Camera>();
		minimap = minimapCamera.transform;
		depthPlane = minimapCamera.transform.Find("DepthPlane");
		target = GameObject.FindGameObjectWithTag("Player").transform;
		playerCircle = target.Find("PlayerCircle").transform;
		playerCircle.localPosition = new Vector3(0,50,0);

		//Recalculate
		Recalculate();
	}

	void Update() {
		//Follow the target
		minimap.position = new Vector3(target.position.x , 700, target.position.z);
	}
	
	void Recalculate() {

		//Position the header
		header.anchoredPosition = headerOffset;

		//Calculate the size and position of the minimap
		minimapCamera.pixelRect = new Rect(minimapOffsetSize.x * Screen.width, minimapOffsetSize.y * Screen.height,minimapOffsetSize.sizeX * Screen.width,minimapOffsetSize.sizeY * Screen.width);
		
		//Scale the depth plane so that the minimap is always as a perfect circle
		depthPlane.localScale = new Vector3(minimapCamera.orthographicSize, minimapCamera.orthographicSize, minimapCamera.orthographicSize);
		
		//Scale the circle in the middle of the minimap according to the size of the minimap
		playerCircle.localScale = new Vector3(minimapCamera.orthographicSize / 1.5f, minimapCamera.orthographicSize / 1.5f, minimapCamera.orthographicSize / 1.5f);
		
		//Calculate the size of the minimap frame
		frame.rectTransform.sizeDelta = new Vector3(canvasScaler.GetComponent<RectTransform>().sizeDelta.x * minimapCamera.rect.width, canvasScaler.GetComponent<RectTransform>().sizeDelta.y * minimapCamera.rect.height);

		//Calculate the position of the minimap frame
		frame.rectTransform.position = new Vector3(minimapCamera.rect.x * canvasScaler.GetComponent<RectTransform>().sizeDelta.x, minimapCamera.rect.y * canvasScaler.GetComponent<RectTransform>().sizeDelta.y);

		for(int i = 0; i < buttons.Count; i++) {
			buttons[i].trans.anchoredPosition = new Vector2(frame.rectTransform.rect.width * buttons[i].offset.x, frame.rectTransform.rect.height * buttons[i].offset.y);
			buttons[i].trans.sizeDelta = new Vector2(minimapOffsetSize.sizeX * buttonSize, minimapOffsetSize.sizeY * buttonSize);
		}

		//canvasScaler.referenceResolution = new Vector2(500 * ((float)Screen.width/Screen.height), 1500);
		canvasScaler.referenceResolution = new Vector2(Screen.width * 2, Screen.height * 2);
	}

	public void ZoomIn() {
		if(minimapCamera.orthographicSize > minSize) {
			minimapCamera.orthographicSize -= zoomAmount;
			Recalculate();
		}
	}

	public void ZoomOut() {
		if(minimapCamera.orthographicSize < maxSize) {
			minimapCamera.orthographicSize += zoomAmount;
			Recalculate();
		}
	}
}
