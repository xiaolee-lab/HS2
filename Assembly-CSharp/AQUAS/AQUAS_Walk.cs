using System;
using UnityEngine;

namespace AQUAS
{
	// Token: 0x02000070 RID: 112
	public class AQUAS_Walk : MonoBehaviour
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000093ED File Offset: 0x000077ED
		private void Start()
		{
			if (this.m_controller == null)
			{
				this.m_controller = base.GetComponent<CharacterController>();
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000940C File Offset: 0x0000780C
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

		// Token: 0x040001ED RID: 493
		public float m_moveSpeed = 10f;

		// Token: 0x040001EE RID: 494
		public CharacterController m_controller;
	}
}
