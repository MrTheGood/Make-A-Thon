using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {

	[System.Serializable]
	public struct OffsetSize {
		public float x;
		public float y;
		public float sizeX;
		public float sizeY;
	}
	
	Camera cam;								//The camera component of the minimap
	
	public Transform target;				//The target the minimap should follow

	public OffsetSize minimapOffsetSize;

	public Transform depthPlane;			//The plane that makes sure the minimap is draw an a circle
	public Transform playerCircle;			//The plane that holds the texture we want in the middle of the minimap
	
	public int minSize;						//How far can the player zoom in?
	public int maxSize;						//How far can the player zoom out?

	public Canvas minimapCanvas;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();	
		//Set the size of the camera between middle and max
		cam.orthographicSize = (minSize + maxSize) / 2;

		Recalculate();
	}
	
	// Recalculate the variables for the camera
	void Recalculate () {
		
		//Follow the target
		transform.position = new Vector3(target.position.x , 700, target.position.z);
		
		//Calculate the size and position of the minimap
		cam.pixelRect = new Rect(minimapOffsetSize.x * Screen.width, minimapOffsetSize.y * Screen.height,minimapOffsetSize.sizeX * Screen.width,minimapOffsetSize.sizeY * Screen.width);
		
		//Scale the depth plane so that the minimap is always as a perfect circle
		depthPlane.localScale = new Vector3(cam.orthographicSize, cam.orthographicSize, cam.orthographicSize);
		
		//Scale the circle in the middle of the minimap according to the size of the minimap
		playerCircle.localScale = new Vector3(cam.orthographicSize / 1.5f, cam.orthographicSize / 2.25f, cam.orthographicSize / 1.5f);

//		minimapCanvas.GetComponent<RectTransform>().localScale = new Vector3(cam.orthographicSize , cam.orthographicSize,1  );
	}

	public void ZoomIn() {
		if(cam.orthographicSize > minSize) {
			cam.orthographicSize -= 2;
			Recalculate();
		}
	}

	public void ZoomOut() {
		if(cam.orthographicSize < maxSize) {
			cam.orthographicSize += 2;
			Recalculate();
		}
	}
}
