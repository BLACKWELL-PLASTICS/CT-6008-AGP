//////////////////////////////////////////////////
/// Created:                                   ///
/// Author: Oliver Blackwell                   ///
/// Edited By:			                       ///
/// Last Edited:		                       ///
//////////////////////////////////////////////////
using UnityEngine;

public class SplineDecorator : MonoBehaviour {

	public BezierSpline spline;

	public int frequency;

	public bool lookForward;

	public Transform[] items;

	private void Awake() {
		if (frequency <= 0 || items == null || items.Length == 0) {
			return;
		}

		float stepSize = 1f / (frequency * items.Length);

		// p = point, f = frequency, i = index
		for (int p = 0, f = 0; f < frequency; f++) {
			for (int i = 0; i < items.Length; i++, p++) {
				// instantiate gameobjects around  the spline
				Transform item = Instantiate(items[i]) as Transform;
				Vector3 position = spline.GetPoint(p * stepSize);
				item.transform.localPosition = position;
				if (lookForward) { // if the objects are set to look forward
					// Set the transform look at as the position + direction of the spline
					item.transform.LookAt(position + spline.GetDirection(p * stepSize));
				}
				item.transform.parent = transform;
			}
		}
	}
}