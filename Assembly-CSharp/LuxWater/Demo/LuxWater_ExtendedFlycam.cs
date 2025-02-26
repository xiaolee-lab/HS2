using System;
using UnityEngine;

namespace LuxWater.Demo
{
	// Token: 0x020003D4 RID: 980
	public class LuxWater_ExtendedFlycam : MonoBehaviour
	{
		// Token: 0x06001164 RID: 4452 RVA: 0x0006636C File Offset: 0x0006476C
		private void Start()
		{
			this.rotationX = base.transform.eulerAngles.y;
			this.cam = base.GetComponent<Camera>();
			if (this.cam != null)
			{
				this.isOrtho = this.cam.orthographic;
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x000663C0 File Offset: 0x000647C0
		private void Update()
		{
			float deltaTime = Time.deltaTime;
			this.rotationX += Input.GetAxis("Mouse X") * this.cameraSensitivity * deltaTime;
			this.rotationY += Input.GetAxis("Mouse Y") * this.cameraSensitivity * deltaTime;
			this.rotationY = Mathf.Clamp(this.rotationY, -90f, 90f);
			Quaternion quaternion = Quaternion.AngleAxis(this.rotationX, Vector3.up);
			quaternion *= Quaternion.AngleAxis(this.rotationY, Vector3.left);
			base.transform.localRotation = Quaternion.Slerp(base.transform.localRotation, quaternion, deltaTime * 6f);
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				base.transform.position += base.transform.forward * (this.normalMoveSpeed * this.fastMoveFactor) * Input.GetAxis("Vertical") * deltaTime;
				base.transform.position += base.transform.right * (this.normalMoveSpeed * this.fastMoveFactor) * Input.GetAxis("Horizontal") * deltaTime;
			}
			else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			{
				base.transform.position += base.transform.forward * (this.normalMoveSpeed * this.slowMoveFactor) * Input.GetAxis("Vertical") * deltaTime;
				base.transform.position += base.transform.right * (this.normalMoveSpeed * this.slowMoveFactor) * Input.GetAxis("Horizontal") * deltaTime;
			}
			else
			{
				if (this.isOrtho)
				{
					this.cam.orthographicSize *= 1f - Input.GetAxis("Vertical") * deltaTime;
				}
				else
				{
					base.transform.position += base.transform.forward * this.normalMoveSpeed * Input.GetAxis("Vertical") * deltaTime;
				}
				base.transform.position += base.transform.right * this.normalMoveSpeed * Input.GetAxis("Horizontal") * deltaTime;
			}
			if (Input.GetKey(KeyCode.Q))
			{
				base.transform.position -= base.transform.up * this.climbSpeed * deltaTime;
			}
			if (Input.GetKey(KeyCode.E))
			{
				base.transform.position += base.transform.up * this.climbSpeed * deltaTime;
			}
		}

		// Token: 0x0400132E RID: 4910
		public float cameraSensitivity = 90f;

		// Token: 0x0400132F RID: 4911
		public float climbSpeed = 4f;

		// Token: 0x04001330 RID: 4912
		public float normalMoveSpeed = 10f;

		// Token: 0x04001331 RID: 4913
		public float slowMoveFactor = 0.25f;

		// Token: 0x04001332 RID: 4914
		public float fastMoveFactor = 3f;

		// Token: 0x04001333 RID: 4915
		private float rotationX;

		// Token: 0x04001334 RID: 4916
		private float rotationY;

		// Token: 0x04001335 RID: 4917
		private bool isOrtho;

		// Token: 0x04001336 RID: 4918
		private Camera cam;
	}
}
