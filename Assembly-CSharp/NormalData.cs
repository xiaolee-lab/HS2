using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020011B3 RID: 4531
public class NormalData : ScriptableObject
{
	// Token: 0x040077A0 RID: 30624
	public List<NormalData.Param> data = new List<NormalData.Param>();

	// Token: 0x020011B4 RID: 4532
	[Serializable]
	public class Param
	{
		// Token: 0x040077A1 RID: 30625
		public string ObjectName = string.Empty;

		// Token: 0x040077A2 RID: 30626
		public List<Vector3> NormalMin = new List<Vector3>();

		// Token: 0x040077A3 RID: 30627
		public List<Vector3> NormalMax = new List<Vector3>();
	}
}
