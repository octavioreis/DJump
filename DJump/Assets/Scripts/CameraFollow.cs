using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform Target;
	
	void LateUpdate ()
	{
		if (Target.position.y > transform.position.y)
			transform.position = new Vector3(transform.position.x, Target.position.y, transform.position.z);
	}
}
