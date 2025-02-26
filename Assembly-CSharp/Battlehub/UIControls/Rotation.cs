using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x0200007C RID: 124
	public class Rotation : MonoBehaviour
	{
		// Token: 0x06000106 RID: 262 RVA: 0x0000A168 File Offset: 0x00008568
		private void Start()
		{
			this.m_rand = UnityEngine.Random.onUnitSphere;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000A178 File Offset: 0x00008578
		private void Update()
		{
			if (Time.time - this.m_prevT > 10f)
			{
				this.m_rand = UnityEngine.Random.onUnitSphere;
				this.m_prevT = Time.time;
			}
			base.transform.rotation *= Quaternion.AngleAxis(12.566371f * Time.deltaTime, this.m_rand);
		}

		// Token: 0x04000200 RID: 512
		private Vector3 m_rand;

		// Token: 0x04000201 RID: 513
		private float m_prevT;
	}
}
