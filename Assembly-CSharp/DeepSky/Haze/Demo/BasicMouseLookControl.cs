using System;
using UnityEngine;

namespace DeepSky.Haze.Demo
{
	// Token: 0x020002E6 RID: 742
	public class BasicMouseLookControl : MonoBehaviour
	{
		// Token: 0x06000C91 RID: 3217 RVA: 0x0003340B File Offset: 0x0003180B
		private void Start()
		{
			this.m_StartRotation = base.transform.localRotation;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00033420 File Offset: 0x00031820
		private void Update()
		{
			this.m_X += Input.GetAxis("Mouse X") * this.m_XSensitivity;
			this.m_Y += Input.GetAxis("Mouse Y") * this.m_YSensitivity;
			if (this.m_X > 360f)
			{
				this.m_X = 0f;
			}
			else if (this.m_X < 0f)
			{
				this.m_X = 360f;
			}
			if (this.m_Y > 60f)
			{
				this.m_Y = 60f;
			}
			else if (this.m_Y < -60f)
			{
				this.m_Y = -60f;
			}
			Quaternion rhs = Quaternion.AngleAxis(this.m_X, Vector3.up);
			Quaternion rhs2 = Quaternion.AngleAxis(this.m_Y, Vector3.left);
			base.transform.localRotation = this.m_StartRotation * rhs * rhs2;
			if (Input.GetKeyUp(KeyCode.Escape))
			{
				Application.Quit();
			}
		}

		// Token: 0x04000B9E RID: 2974
		[SerializeField]
		private float m_XSensitivity = 2.5f;

		// Token: 0x04000B9F RID: 2975
		[SerializeField]
		private float m_YSensitivity = 2.5f;

		// Token: 0x04000BA0 RID: 2976
		private Quaternion m_StartRotation;

		// Token: 0x04000BA1 RID: 2977
		private float m_X;

		// Token: 0x04000BA2 RID: 2978
		private float m_Y;
	}
}
