using System;
using UnityEngine;

// Token: 0x02000675 RID: 1653
public class CameraZoom : MonoBehaviour
{
	// Token: 0x060026D7 RID: 9943 RVA: 0x000E0A00 File Offset: 0x000DEE00
	private void Update()
	{
		base.transform.Translate(Vector3.forward * this.zoomSpeed * Input.GetAxis("Mouse ScrollWheel"));
		base.transform.Translate(Vector3.forward * this.keyZoomSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
	}

	// Token: 0x04002713 RID: 10003
	public float zoomSpeed = 10f;

	// Token: 0x04002714 RID: 10004
	public float keyZoomSpeed = 20f;
}
