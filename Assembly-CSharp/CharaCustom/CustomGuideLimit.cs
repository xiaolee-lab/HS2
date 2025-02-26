using System;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x02000A12 RID: 2578
	public class CustomGuideLimit : MonoBehaviour
	{
		// Token: 0x04004652 RID: 18002
		public bool limited;

		// Token: 0x04004653 RID: 18003
		public Transform trfParent;

		// Token: 0x04004654 RID: 18004
		public Vector3 limitMin = Vector3.zero;

		// Token: 0x04004655 RID: 18005
		public Vector3 limitMax = Vector3.zero;
	}
}
