using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020011B6 RID: 4534
public class UVData : ScriptableObject
{
	// Token: 0x040077A4 RID: 30628
	public List<UVData.Param> data = new List<UVData.Param>();

	// Token: 0x040077A5 RID: 30629
	public int[] rangeIndex;

	// Token: 0x020011B7 RID: 4535
	[Serializable]
	public class Param
	{
		// Token: 0x040077A6 RID: 30630
		public string ObjectName = string.Empty;

		// Token: 0x040077A7 RID: 30631
		public List<Vector2> UV = new List<Vector2>();
	}
}
