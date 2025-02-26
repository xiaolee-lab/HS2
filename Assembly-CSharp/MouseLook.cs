using System;
using UnityEngine;

// Token: 0x020004D9 RID: 1241
public class MouseLook : MonoBehaviour
{
	// Token: 0x060016ED RID: 5869 RVA: 0x0008EA80 File Offset: 0x0008CE80
	private void Update()
	{
		if (GUIUtility.hotControl != 0)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			this.look = true;
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.look = false;
		}
		if (this.look)
		{
			if (this.axes == MouseLook.RotationAxes.MouseXAndY)
			{
				this.rotationX += Input.GetAxis("Mouse X") * this.sensitivityX;
				this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
				this.rotationX = MouseLook.ClampAngle(this.rotationX, this.minimumX, this.maximumX);
				this.rotationY = MouseLook.ClampAngle(this.rotationY, this.minimumY, this.maximumY);
				Quaternion rhs = Quaternion.AngleAxis(this.rotationX, Vector3.up);
				Quaternion rhs2 = Quaternion.AngleAxis(this.rotationY, Vector3.left);
				base.transform.localRotation = this.originalRotation * rhs * rhs2;
			}
			else if (this.axes == MouseLook.RotationAxes.MouseX)
			{
				this.rotationX += Input.GetAxis("Mouse X") * this.sensitivityX;
				this.rotationX = MouseLook.ClampAngle(this.rotationX, this.minimumX, this.maximumX);
				Quaternion rhs3 = Quaternion.AngleAxis(this.rotationX, Vector3.up);
				base.transform.localRotation = this.originalRotation * rhs3;
			}
			else
			{
				this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
				this.rotationY = MouseLook.ClampAngle(this.rotationY, this.minimumY, this.maximumY);
				Quaternion rhs4 = Quaternion.AngleAxis(this.rotationY, Vector3.left);
				base.transform.localRotation = this.originalRotation * rhs4;
			}
		}
		Vector3 vector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		vector = base.transform.TransformDirection(vector);
		vector *= 10f;
		float num = (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) ? 50f : 150f;
		float num2 = Input.GetAxis("Vertical") * this.forwardSpeedScale * num;
		float num3 = Input.GetAxis("Horizontal") * this.strafeSpeedScale * num;
		if (num2 != 0f)
		{
			base.transform.position += base.transform.forward * num2;
		}
		if (num3 != 0f)
		{
			base.transform.position += base.transform.right * num3;
		}
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x0008ED63 File Offset: 0x0008D163
	private void Start()
	{
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
		this.originalRotation = base.transform.localRotation;
		this.look = false;
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x0008ED99 File Offset: 0x0008D199
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x04001A31 RID: 6705
	public MouseLook.RotationAxes axes;

	// Token: 0x04001A32 RID: 6706
	public float sensitivityX = 3f;

	// Token: 0x04001A33 RID: 6707
	public float sensitivityY = 3f;

	// Token: 0x04001A34 RID: 6708
	public float minimumX = -360f;

	// Token: 0x04001A35 RID: 6709
	public float maximumX = 360f;

	// Token: 0x04001A36 RID: 6710
	public float minimumY = -80f;

	// Token: 0x04001A37 RID: 6711
	public float maximumY = 80f;

	// Token: 0x04001A38 RID: 6712
	public float forwardSpeedScale = 0.03f;

	// Token: 0x04001A39 RID: 6713
	public float strafeSpeedScale = 0.03f;

	// Token: 0x04001A3A RID: 6714
	private float rotationX;

	// Token: 0x04001A3B RID: 6715
	private float rotationY;

	// Token: 0x04001A3C RID: 6716
	private bool look;

	// Token: 0x04001A3D RID: 6717
	private Quaternion originalRotation;

	// Token: 0x020004DA RID: 1242
	public enum RotationAxes
	{
		// Token: 0x04001A3F RID: 6719
		MouseXAndY,
		// Token: 0x04001A40 RID: 6720
		MouseX,
		// Token: 0x04001A41 RID: 6721
		MouseY
	}
}
