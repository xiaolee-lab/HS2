using System;
using UnityEngine;

namespace EnviroSamples
{
	// Token: 0x02000321 RID: 801
	public class FPSController : MonoBehaviour
	{
		// Token: 0x06000E1B RID: 3611 RVA: 0x00044533 File Offset: 0x00042933
		private void Start()
		{
			this.player = base.GetComponent<CharacterController>();
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00044544 File Offset: 0x00042944
		private void Update()
		{
			this.moveFB = Input.GetAxis("Vertical") * this.speed;
			this.moveLR = Input.GetAxis("Horizontal") * this.speed;
			this.rotX = Input.GetAxis("Mouse X") * this.sensitivity;
			this.rotY -= Input.GetAxis("Mouse Y") * this.sensitivity;
			this.rotY = Mathf.Clamp(this.rotY, -60f, 60f);
			Vector3 vector = new Vector3(this.moveLR, 0f, this.moveFB);
			base.transform.Rotate(0f, this.rotX, 0f);
			this.eyes.transform.localRotation = Quaternion.Euler(this.rotY, 0f, 0f);
			vector = base.transform.rotation * vector;
			vector.y -= 4000f * Time.deltaTime;
			this.player.Move(vector * Time.deltaTime);
		}

		// Token: 0x04000E0D RID: 3597
		public float speed = 2f;

		// Token: 0x04000E0E RID: 3598
		public float sensitivity = 2f;

		// Token: 0x04000E0F RID: 3599
		private CharacterController player;

		// Token: 0x04000E10 RID: 3600
		public GameObject eyes;

		// Token: 0x04000E11 RID: 3601
		private float moveFB;

		// Token: 0x04000E12 RID: 3602
		private float moveLR;

		// Token: 0x04000E13 RID: 3603
		private float rotX;

		// Token: 0x04000E14 RID: 3604
		private float rotY;
	}
}
