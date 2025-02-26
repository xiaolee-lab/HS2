using System;
using UnityEngine;

namespace CTS
{
	// Token: 0x0200069D RID: 1693
	public class CTSWalk : MonoBehaviour
	{
		// Token: 0x0600281B RID: 10267 RVA: 0x000EE476 File Offset: 0x000EC876
		private void Start()
		{
			if (this.m_controller == null)
			{
				this.m_controller = base.GetComponent<CharacterController>();
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000EE498 File Offset: 0x000EC898
		private void Update()
		{
			if (this.m_controller != null && this.m_controller.enabled)
			{
				Vector3 a = Input.GetAxis("Vertical") * base.transform.TransformDirection(Vector3.forward) * this.m_moveSpeed;
				this.m_controller.Move(a * Time.deltaTime);
				Vector3 a2 = Input.GetAxis("Horizontal") * base.transform.TransformDirection(Vector3.right) * this.m_moveSpeed;
				this.m_controller.Move(a2 * Time.deltaTime);
				this.m_controller.SimpleMove(Physics.gravity);
			}
		}

		// Token: 0x04002929 RID: 10537
		public float m_moveSpeed = 10f;

		// Token: 0x0400292A RID: 10538
		public CharacterController m_controller;
	}
}
