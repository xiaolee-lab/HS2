using System;
using UnityEngine;

namespace CTS
{
	// Token: 0x0200068D RID: 1677
	public class CTSFly : MonoBehaviour
	{
		// Token: 0x06002746 RID: 10054 RVA: 0x000E712C File Offset: 0x000E552C
		private void Start()
		{
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000E7130 File Offset: 0x000E5530
		private void Update()
		{
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				base.transform.position += base.transform.forward * (this.normalMoveSpeed * this.fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
				base.transform.position += base.transform.right * (this.normalMoveSpeed * this.fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
			}
			else
			{
				base.transform.position += base.transform.forward * this.normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
				base.transform.position += base.transform.right * this.normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.E))
			{
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					base.transform.position += base.transform.up * this.climbSpeed * this.fastMoveFactor * Time.deltaTime;
				}
				else
				{
					base.transform.position += base.transform.up * this.climbSpeed * Time.deltaTime;
				}
			}
			if (Input.GetKey(KeyCode.Q))
			{
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					base.transform.position -= base.transform.up * this.climbSpeed * this.fastMoveFactor * Time.deltaTime;
				}
				else
				{
					base.transform.position -= base.transform.up * this.climbSpeed * Time.deltaTime;
				}
			}
		}

		// Token: 0x040027AA RID: 10154
		public float cameraSensitivity = 90f;

		// Token: 0x040027AB RID: 10155
		public float climbSpeed = 4f;

		// Token: 0x040027AC RID: 10156
		public float normalMoveSpeed = 10f;

		// Token: 0x040027AD RID: 10157
		public float fastMoveFactor = 3f;
	}
}
