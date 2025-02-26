using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005C9 RID: 1481
	public class RotateScript : MonoBehaviour
	{
		// Token: 0x0600221A RID: 8730 RVA: 0x000BB938 File Offset: 0x000B9D38
		private void Update()
		{
			Vector3 localEulerAngles = base.gameObject.transform.localEulerAngles;
			localEulerAngles.z += this.speed * Time.deltaTime;
			base.gameObject.transform.localEulerAngles = localEulerAngles;
		}

		// Token: 0x040021C7 RID: 8647
		public float speed = 1f;
	}
}
