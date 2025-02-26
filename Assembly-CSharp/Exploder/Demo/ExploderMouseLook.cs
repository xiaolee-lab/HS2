using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x0200037C RID: 892
	public class ExploderMouseLook : MonoBehaviour
	{
		// Token: 0x06000FCC RID: 4044 RVA: 0x00058B94 File Offset: 0x00056F94
		private void Update()
		{
			if (!CursorLocking.IsLocked)
			{
				return;
			}
			float y;
			if (this.axes == ExploderMouseLook.RotationAxes.MouseXAndY)
			{
				float num = base.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * this.sensitivityX;
				this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
				this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
				y = num;
				this.mainCamera.transform.localEulerAngles = new Vector3(-this.rotationY, 0f, 0f);
				if (this.kickTimeout > 0f)
				{
					this.rotationTarget += Input.GetAxis("Mouse Y") * this.sensitivityY;
				}
			}
			else if (this.axes == ExploderMouseLook.RotationAxes.MouseX)
			{
				y = Input.GetAxis("Mouse X") * this.sensitivityX;
				this.mainCamera.transform.Rotate(0f, 0f, 0f);
			}
			else
			{
				this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
				this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
				if (this.kickTimeout > 0f)
				{
					this.rotationTarget += Input.GetAxis("Mouse Y") * this.sensitivityY;
				}
				y = base.transform.localEulerAngles.y;
				this.mainCamera.transform.localEulerAngles = new Vector3(-this.rotationY, 0f, 0f);
			}
			this.kickTimeout -= Time.deltaTime;
			if (this.kickTimeout > 0f)
			{
				this.rotationY = Mathf.Lerp(this.rotationY, this.rotationTarget, Time.deltaTime * 10f);
			}
			else if (this.kickBack)
			{
				this.kickBack = false;
				this.kickTimeout = 0.5f;
				this.rotationTarget = this.kickBackRot;
			}
			base.gameObject.transform.rotation = Quaternion.Euler(0f, y, 0f);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00058DF0 File Offset: 0x000571F0
		private void Start()
		{
			if (base.GetComponent<Rigidbody>())
			{
				base.GetComponent<Rigidbody>().freezeRotation = true;
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00058E0E File Offset: 0x0005720E
		public void Kick()
		{
			this.kickTimeout = 0.1f;
			this.rotationTarget = this.rotationY + (float)UnityEngine.Random.Range(15, 20);
			this.kickBackRot = this.rotationY;
			this.kickBack = true;
		}

		// Token: 0x04001179 RID: 4473
		public ExploderMouseLook.RotationAxes axes;

		// Token: 0x0400117A RID: 4474
		public float sensitivityX = 15f;

		// Token: 0x0400117B RID: 4475
		public float sensitivityY = 15f;

		// Token: 0x0400117C RID: 4476
		public float minimumX = -360f;

		// Token: 0x0400117D RID: 4477
		public float maximumX = 360f;

		// Token: 0x0400117E RID: 4478
		public float minimumY = -60f;

		// Token: 0x0400117F RID: 4479
		public float maximumY = 60f;

		// Token: 0x04001180 RID: 4480
		private float rotationY;

		// Token: 0x04001181 RID: 4481
		private float kickTimeout;

		// Token: 0x04001182 RID: 4482
		private float kickBackRot;

		// Token: 0x04001183 RID: 4483
		private bool kickBack;

		// Token: 0x04001184 RID: 4484
		private float rotationTarget;

		// Token: 0x04001185 RID: 4485
		public Camera mainCamera;

		// Token: 0x0200037D RID: 893
		public enum RotationAxes
		{
			// Token: 0x04001187 RID: 4487
			MouseXAndY,
			// Token: 0x04001188 RID: 4488
			MouseX,
			// Token: 0x04001189 RID: 4489
			MouseY
		}
	}
}
