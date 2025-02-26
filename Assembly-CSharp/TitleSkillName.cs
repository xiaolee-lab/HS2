using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class TitleSkillName : ScriptableObject
{
	// Token: 0x04000142 RID: 322
	public List<TitleSkillName.Param> param = new List<TitleSkillName.Param>();

	// Token: 0x02000056 RID: 86
	[Serializable]
	public class Param
	{
		// Token: 0x04000143 RID: 323
		public int id;

		// Token: 0x04000144 RID: 324
		public string name0;

		// Token: 0x04000145 RID: 325
		public string name1;

		// Token: 0x04000146 RID: 326
		public string name2;

		// Token: 0x04000147 RID: 327
		public string name3;

		// Token: 0x04000148 RID: 328
		public string name4;
	}
}
