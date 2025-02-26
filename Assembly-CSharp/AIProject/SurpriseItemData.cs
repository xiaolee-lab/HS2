using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000051 RID: 81
	public class SurpriseItemData : ScriptableObject
	{
		// Token: 0x04000138 RID: 312
		public List<SurpriseItemData.Param> param = new List<SurpriseItemData.Param>();

		// Token: 0x02000052 RID: 82
		[Serializable]
		public class Param
		{
			// Token: 0x04000139 RID: 313
			public string Summary;

			// Token: 0x0400013A RID: 314
			public string Animator;

			// Token: 0x0400013B RID: 315
			public List<string> ItemList = new List<string>();
		}
	}
}
