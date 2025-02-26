using System;
using UnityEngine;

namespace DeepSky.Haze
{
	// Token: 0x020002E9 RID: 745
	[AddComponentMenu("")]
	[Serializable]
	public class DS_HazeContextAsset : ScriptableObject
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000339D2 File Offset: 0x00031DD2
		public DS_HazeContext Context
		{
			get
			{
				return this.m_Context;
			}
		}

		// Token: 0x04000BA8 RID: 2984
		[SerializeField]
		private DS_HazeContext m_Context = new DS_HazeContext();
	}
}
