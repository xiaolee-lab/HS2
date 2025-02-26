using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x02000380 RID: 896
	public class MoveController : MonoBehaviour
	{
		// Token: 0x06000FD9 RID: 4057 RVA: 0x00059249 File Offset: 0x00057649
		private void Start()
		{
			this.controller = base.GetComponent<CharacterController>();
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00059258 File Offset: 0x00057658
		private void Update()
		{
			if (this.controller.isGrounded)
			{
				this.moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
				this.moveDirection = base.transform.TransformDirection(this.moveDirection);
				this.moveDirection *= this.speed;
				if (Input.GetButton("Jump"))
				{
					this.moveDirection.y = this.jumpSpeed;
				}
			}
			this.moveDirection.y = this.moveDirection.y - this.gravity * Time.deltaTime;
			this.controller.Move(this.moveDirection * Time.deltaTime);
		}

		// Token: 0x0400119B RID: 4507
		public float speed = 6f;

		// Token: 0x0400119C RID: 4508
		public float jumpSpeed = 8f;

		// Token: 0x0400119D RID: 4509
		public float gravity = 20f;

		// Token: 0x0400119E RID: 4510
		private Vector3 moveDirection = Vector3.zero;

		// Token: 0x0400119F RID: 4511
		private CharacterController controller;
	}
}
