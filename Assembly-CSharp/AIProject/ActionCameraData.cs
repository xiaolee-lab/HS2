using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C46 RID: 3142
	[Serializable]
	public struct ActionCameraData
	{
		// Token: 0x04005685 RID: 22149
		[Header("フリールック")]
		public Vector3 freePos;

		// Token: 0x04005686 RID: 22150
		[Space]
		[Header("固定カメラ")]
		public Vector3 nonMovePos;

		// Token: 0x04005687 RID: 22151
		public Vector3 nonMoveRot;
	}
}
