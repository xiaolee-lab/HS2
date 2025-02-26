using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x0200041C RID: 1052
	public class ETFXMouseOrbit : MonoBehaviour
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x00075F04 File Offset: 0x00074304
		private void Start()
		{
			Vector3 eulerAngles = base.transform.eulerAngles;
			this.rotationYAxis = eulerAngles.y;
			this.rotationXAxis = eulerAngles.x;
			if (base.GetComponent<Rigidbody>())
			{
				base.GetComponent<Rigidbody>().freezeRotation = true;
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00075F54 File Offset: 0x00074354
		private void LateUpdate()
		{
			if (this.target)
			{
				if (Input.GetMouseButton(1))
				{
					this.velocityX += this.xSpeed * Input.GetAxis("Mouse X") * this.distance * 0.02f;
					this.velocityY += this.ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
				}
				this.rotationYAxis += this.velocityX;
				this.rotationXAxis -= this.velocityY;
				this.rotationXAxis = ETFXMouseOrbit.ClampAngle(this.rotationXAxis, this.yMinLimit, this.yMaxLimit);
				Quaternion quaternion = Quaternion.Euler(this.rotationXAxis, this.rotationYAxis, 0f);
				Quaternion rotation = quaternion;
				this.distance = Mathf.Clamp(this.distance - Input.GetAxis("Mouse ScrollWheel") * 5f, this.distanceMin, this.distanceMax);
				RaycastHit raycastHit;
				if (Physics.Linecast(this.target.position, base.transform.position, out raycastHit))
				{
					this.distance -= raycastHit.distance;
				}
				Vector3 point = new Vector3(0f, 0f, -this.distance);
				Vector3 position = rotation * point + this.target.position;
				base.transform.rotation = rotation;
				base.transform.position = position;
				this.velocityX = Mathf.Lerp(this.velocityX, 0f, Time.deltaTime * this.smoothTime);
				this.velocityY = Mathf.Lerp(this.velocityY, 0f, Time.deltaTime * this.smoothTime);
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00076119 File Offset: 0x00074519
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

		// Token: 0x0400155F RID: 5471
		public Transform target;

		// Token: 0x04001560 RID: 5472
		public float distance = 5f;

		// Token: 0x04001561 RID: 5473
		public float xSpeed = 120f;

		// Token: 0x04001562 RID: 5474
		public float ySpeed = 120f;

		// Token: 0x04001563 RID: 5475
		public float yMinLimit = -20f;

		// Token: 0x04001564 RID: 5476
		public float yMaxLimit = 80f;

		// Token: 0x04001565 RID: 5477
		public float distanceMin = 0.5f;

		// Token: 0x04001566 RID: 5478
		public float distanceMax = 15f;

		// Token: 0x04001567 RID: 5479
		public float smoothTime = 2f;

		// Token: 0x04001568 RID: 5480
		private float rotationYAxis;

		// Token: 0x04001569 RID: 5481
		private float rotationXAxis;

		// Token: 0x0400156A RID: 5482
		private float velocityX;

		// Token: 0x0400156B RID: 5483
		private float velocityY;
	}
}
