using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exploder
{
	// Token: 0x02000399 RID: 921
	internal class BakeSkinManager
	{
		// Token: 0x06001047 RID: 4167 RVA: 0x0005AF4C File Offset: 0x0005934C
		public BakeSkinManager(Core core)
		{
			this.parent = new GameObject("BakeSkinParent");
			this.parent.gameObject.transform.parent = core.transform;
			this.parent.transform.position = Vector3.zero;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0005AFAC File Offset: 0x000593AC
		public GameObject CreateBakeObject(string name)
		{
			GameObject gameObject = new GameObject(name);
			gameObject.transform.parent = this.parent.transform;
			this.bakedObjects.Add(gameObject);
			return gameObject;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0005AFE4 File Offset: 0x000593E4
		public void Clear()
		{
			for (int i = 0; i < this.bakedObjects.Count; i++)
			{
				if (this.bakedObjects[i])
				{
					UnityEngine.Object.Destroy(this.bakedObjects[i]);
				}
			}
			this.bakedObjects.Clear();
		}

		// Token: 0x040011FB RID: 4603
		private readonly GameObject parent;

		// Token: 0x040011FC RID: 4604
		private readonly List<GameObject> bakedObjects = new List<GameObject>();
	}
}
