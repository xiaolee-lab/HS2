using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x0200003E RID: 62
	public class MotionBustCtrlData : ScriptableObject
	{
		// Token: 0x04000103 RID: 259
		public List<MotionBustCtrlData.Param> param = new List<MotionBustCtrlData.Param>();

		// Token: 0x0200003F RID: 63
		[Serializable]
		public class Param
		{
			// Token: 0x04000104 RID: 260
			public string Summary;

			// Token: 0x04000105 RID: 261
			public string State;

			// Token: 0x04000106 RID: 262
			public List<string> Parameters = new List<string>();
		}
	}
}
