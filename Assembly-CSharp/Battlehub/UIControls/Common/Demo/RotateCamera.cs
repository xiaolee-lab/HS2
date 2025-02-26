using System;
using UnityEngine;

namespace Battlehub.UIControls.Common.Demo
{
	// Token: 0x02000071 RID: 113
	public class RotateCamera : MonoBehaviour
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000094D7 File Offset: 0x000078D7
		private void Start()
		{
			this.m_rand = UnityEngine.Random.onUnitSphere;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000094E4 File Offset: 0x000078E4
		private void Update()
		{
			if (Time.time - this.m_prevT > 10f)
			{
				this.m_rand = UnityEngine.Random.onUnitSphere;
				this.m_prevT = Time.time;
			}
			base.transform.rotation *= Quaternion.AngleAxis(12.566371f * Time.deltaTime, this.m_rand);
		}

		// Token: 0x040001EF RID: 495
		private Vector3 m_rand;

		// Token: 0x040001F0 RID: 496
		private float m_prevT;
	}
}
