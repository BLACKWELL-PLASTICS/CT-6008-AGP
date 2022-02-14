﻿using UnityEngine;

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

				Transform item = Instantiate(items[i]) as Transform;
				Vector3 position = spline.GetPoint(p * stepSize);
				item.transform.localPosition = position;
				if (lookForward) {
					item.transform.LookAt(position + spline.GetDirection(p * stepSize));
				}
				item.transform.parent = transform;
			}
		}
	}
}