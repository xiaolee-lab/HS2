using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class MoveCamera : MonoBehaviour
{
	// Token: 0x06000070 RID: 112 RVA: 0x00006058 File Offset: 0x00004458
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.mouseOrigin = Input.mousePosition;
			this.isRotating = true;
		}
		if (Input.GetMouseButtonDown(1))
		{
			this.mouseOrigin = Input.mousePosition;
			this.isPanning = true;
		}
		if (Input.GetMouseButtonDown(2))
		{
			this.mouseOrigin = Input.mousePosition;
			this.isZooming = true;
		}
		if (!Input.GetMouseButton(0))
		{
			this.isRotating = false;
		}
		if (!Input.GetMouseButton(1))
		{
			this.isPanning = false;
		}
		if (!Input.GetMouseButton(2))
		{
			this.isZooming = false;
		}
		if (this.isRotating)
		{
			Vector3 vector = Camera.main.ScreenToViewportPoint(Input.mousePosition - this.mouseOrigin);
			base.transform.RotateAround(base.transform.position, base.transform.right, -vector.y * this.turnSpeed);
			base.transform.RotateAround(base.transform.position, Vector3.up, vector.x * this.turnSpeed);
		}
		if (this.isPanning)
		{
			Vector3 vector2 = Camera.main.ScreenToViewportPoint(Input.mousePosition - this.mouseOrigin);
			Vector3 translation = new Vector3(vector2.x * this.panSpeed, vector2.y * this.panSpeed, 0f);
			base.transform.Translate(translation, Space.Self);
		}
		if (this.isZooming)
		{
			Vector3 translation2 = Camera.main.ScreenToViewportPoint(Input.mousePosition - this.mouseOrigin).y * this.zoomSpeed * base.transform.forward;
			base.transform.Translate(translation2, Space.World);
		}
	}

	// Token: 0x04000158 RID: 344
	public float turnSpeed = 4f;

	// Token: 0x04000159 RID: 345
	public float panSpeed = 4f;

	// Token: 0x0400015A RID: 346
	public float zoomSpeed = 4f;

	// Token: 0x0400015B RID: 347
	private Vector3 mouseOrigin;

	// Token: 0x0400015C RID: 348
	private bool isPanning;

	// Token: 0x0400015D RID: 349
	private bool isRotating;

	// Token: 0x0400015E RID: 350
	private bool isZooming;
}
