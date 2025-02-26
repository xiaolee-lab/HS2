using System;
using UnityEngine;

// Token: 0x02000424 RID: 1060
public class ME_MouseOrbit : MonoBehaviour
{
	// Token: 0x06001355 RID: 4949 RVA: 0x00076E78 File Offset: 0x00075278
	private void Start()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		this.x = eulerAngles.y;
		this.y = eulerAngles.x;
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x00076EAC File Offset: 0x000752AC
	private void LateUpdate()
	{
		if (this.distance < 2f)
		{
			this.distance = 2f;
		}
		this.distance -= Input.GetAxis("Mouse ScrollWheel") * 2f;
		if (this.target && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
		{
			Vector3 mousePosition = Input.mousePosition;
			if (Screen.dpi < 1f)
			{
			}
			float num;
			if (Screen.dpi < 200f)
			{
				num = 1f;
			}
			else
			{
				num = Screen.dpi / 200f;
			}
			if (mousePosition.x < 380f * num && (float)Screen.height - mousePosition.y < 250f * num)
			{
				return;
			}
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			this.x += Input.GetAxis("Mouse X") * this.xSpeed * 0.02f;
			this.y -= Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f;
			this.y = ME_MouseOrbit.ClampAngle(this.y, this.yMinLimit, this.yMaxLimit);
			Quaternion rotation = Quaternion.Euler(this.y, this.x, 0f);
			Vector3 position = rotation * new Vector3(0f, 0f, -this.distance) + this.target.transform.position;
			base.transform.rotation = rotation;
			base.transform.position = position;
		}
		else
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		if (Math.Abs(this.prevDistance - this.distance) > 0.001f)
		{
			this.prevDistance = this.distance;
			Quaternion rotation2 = Quaternion.Euler(this.y, this.x, 0f);
			Vector3 position2 = rotation2 * new Vector3(0f, 0f, -this.distance) + this.target.transform.position;
			base.transform.rotation = rotation2;
			base.transform.position = position2;
		}
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x000770FD File Offset: 0x000754FD
	private static float ClampAngle(float angle, float min, float max)
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

	// Token: 0x0400159B RID: 5531
	public GameObject target;

	// Token: 0x0400159C RID: 5532
	public float distance = 10f;

	// Token: 0x0400159D RID: 5533
	public float xSpeed = 250f;

	// Token: 0x0400159E RID: 5534
	public float ySpeed = 120f;

	// Token: 0x0400159F RID: 5535
	public float yMinLimit = -20f;

	// Token: 0x040015A0 RID: 5536
	public float yMaxLimit = 80f;

	// Token: 0x040015A1 RID: 5537
	private float x;

	// Token: 0x040015A2 RID: 5538
	private float y;

	// Token: 0x040015A3 RID: 5539
	private float prevDistance;
}
