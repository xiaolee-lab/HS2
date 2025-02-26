using System;
using Exploder.Utils;
using UnityEngine;

namespace Exploder.Examples
{
	// Token: 0x02000392 RID: 914
	public class ExplodeAllObjects : MonoBehaviour
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x0005A9F4 File Offset: 0x00058DF4
		private void Start()
		{
			this.DestroyableObjects = GameObject.FindGameObjectsWithTag("Exploder");
			this.Exploder = ExploderSingleton.Instance;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0005AA11 File Offset: 0x00058E11
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				this.Exploder.ExplodeObjects(this.DestroyableObjects);
			}
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0005AA30 File Offset: 0x00058E30
		private void ExplodeObject(GameObject gameObject)
		{
			ExploderUtils.SetActive(this.Exploder.gameObject, true);
			this.Exploder.transform.position = ExploderUtils.GetCentroid(gameObject);
			this.Exploder.Radius = 1f;
			this.Exploder.ExplodeRadius();
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0005AA7F File Offset: 0x00058E7F
		private void OnGUI()
		{
			GUI.Label(new Rect(200f, 10f, 300f, 30f), "Hit enter to explode everything!");
		}

		// Token: 0x040011EC RID: 4588
		private ExploderObject Exploder;

		// Token: 0x040011ED RID: 4589
		private GameObject[] DestroyableObjects;
	}
}
