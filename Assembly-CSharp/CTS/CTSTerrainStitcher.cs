using System;
using UnityEngine;

namespace CTS
{
	// Token: 0x0200069B RID: 1691
	[ExecuteInEditMode]
	public class CTSTerrainStitcher : MonoBehaviour
	{
		// Token: 0x06002807 RID: 10247 RVA: 0x000EDED8 File Offset: 0x000EC2D8
		private void Start()
		{
			CTSSingleton<CTSTerrainManager>.Instance.RemoveWorldSeams();
		}
	}
}
