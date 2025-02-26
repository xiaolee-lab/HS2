using System;
using System.Collections.Generic;
using System.Linq;
using Exploder.Utils;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x02000391 RID: 913
	public class DemoSimple : MonoBehaviour
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x0005A858 File Offset: 0x00058C58
		private void Start()
		{
			this.Exploder = ExploderSingleton.Instance;
			if (this.Exploder.DontUseTag)
			{
				UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(Explodable));
				List<GameObject> list = new List<GameObject>(array.Length);
				list.AddRange(from Explodable ex in array
				where ex
				select ex.gameObject);
				this.DestroyableObjects = list.ToArray();
			}
			else
			{
				this.DestroyableObjects = GameObject.FindGameObjectsWithTag("Exploder");
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0005A90C File Offset: 0x00058D0C
		private void OnGUI()
		{
			if (GUI.Button(new Rect(10f, 10f, 100f, 30f), "Explode!") && this.Exploder)
			{
				this.Exploder.ExplodeRadius();
			}
			if (GUI.Button(new Rect(130f, 10f, 100f, 30f), "Reset"))
			{
				ExploderUtils.SetActive(this.Exploder.gameObject, true);
				if (!this.Exploder.DestroyOriginalObject)
				{
					foreach (GameObject obj in this.DestroyableObjects)
					{
						ExploderUtils.SetActiveRecursively(obj, true);
					}
					ExploderUtils.SetActive(this.Exploder.gameObject, true);
				}
			}
		}

		// Token: 0x040011E8 RID: 4584
		private ExploderObject Exploder;

		// Token: 0x040011E9 RID: 4585
		private GameObject[] DestroyableObjects;
	}
}
