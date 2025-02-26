using System;
using UnityEngine;

// Token: 0x020003FF RID: 1023
public class FreeCam : MonoBehaviour
{
	// Token: 0x06001234 RID: 4660 RVA: 0x00071FE4 File Offset: 0x000703E4
	private void Update()
	{
		if (this.axes == FreeCam.RotationAxes.MouseXAndY)
		{
			float y = base.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * this.sensitivityX;
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			base.transform.localEulerAngles = new Vector3(-this.rotationY, y, 0f);
		}
		else if (this.axes == FreeCam.RotationAxes.MouseX)
		{
			base.transform.Rotate(0f, Input.GetAxis("Mouse X") * this.sensitivityX, 0f);
		}
		else
		{
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			base.transform.localEulerAngles = new Vector3(-this.rotationY, base.transform.localEulerAngles.y, 0f);
		}
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		if (this.lockHeight)
		{
			Vector3 b = base.transform.TransformDirection(new Vector3(axis, 0f, axis2) * this.moveSpeed);
			b.y = 0f;
			base.transform.position += b;
		}
		else
		{
			base.transform.Translate(new Vector3(axis, 0f, axis2) * this.moveSpeed);
		}
	}

	// Token: 0x040014BA RID: 5306
	public FreeCam.RotationAxes axes;

	// Token: 0x040014BB RID: 5307
	public float sensitivityX = 15f;

	// Token: 0x040014BC RID: 5308
	public float sensitivityY = 15f;

	// Token: 0x040014BD RID: 5309
	public float minimumX = -360f;

	// Token: 0x040014BE RID: 5310
	public float maximumX = 360f;

	// Token: 0x040014BF RID: 5311
	public float minimumY = -60f;

	// Token: 0x040014C0 RID: 5312
	public float maximumY = 60f;

	// Token: 0x040014C1 RID: 5313
	public float moveSpeed = 1f;

	// Token: 0x040014C2 RID: 5314
	public bool lockHeight;

	// Token: 0x040014C3 RID: 5315
	private float rotationY;

	// Token: 0x02000400 RID: 1024
	public enum RotationAxes
	{
		// Token: 0x040014C5 RID: 5317
		MouseXAndY,
		// Token: 0x040014C6 RID: 5318
		MouseX,
		// Token: 0x040014C7 RID: 5319
		MouseY
	}
}
