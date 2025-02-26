using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x02000382 RID: 898
	public class PanelResetScene : UseObject
	{
		// Token: 0x06000FE0 RID: 4064 RVA: 0x00059508 File Offset: 0x00057908
		private void Start()
		{
			this.objectList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Exploder"));
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00059520 File Offset: 0x00057920
		public override void Use()
		{
			base.Use();
			ExploderUtils.ClearLog();
			foreach (GameObject obj in this.objectList)
			{
				ExploderUtils.SetActiveRecursively(obj, true);
				ExploderUtils.SetVisible(obj, true);
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00059590 File Offset: 0x00057990
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				this.Use();
			}
		}

		// Token: 0x040011A6 RID: 4518
		private List<GameObject> objectList;
	}
}
