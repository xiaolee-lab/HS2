using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000040 RID: 64
	public class MotionExpressionData : ScriptableObject
	{
		// Token: 0x04000107 RID: 263
		public List<MotionExpressionData.Param> param = new List<MotionExpressionData.Param>();

		// Token: 0x02000041 RID: 65
		[Serializable]
		public class Param
		{
			// Token: 0x04000108 RID: 264
			public string Summary;

			// Token: 0x04000109 RID: 265
			public string State;

			// Token: 0x0400010A RID: 266
			public string ExpressionName;
		}
	}
}
