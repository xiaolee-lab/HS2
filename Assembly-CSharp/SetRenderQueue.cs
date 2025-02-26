using System;
using UnityEngine;

// Token: 0x02001121 RID: 4385
[AddComponentMenu("Rendering/SetRenderQueue")]
public class SetRenderQueue : MonoBehaviour
{
	// Token: 0x0600912F RID: 37167 RVA: 0x003C6FDC File Offset: 0x003C53DC
	protected void Awake()
	{
		Material[] materials = base.GetComponent<Renderer>().materials;
		int num = 0;
		while (num < materials.Length && num < this.m_queues.Length)
		{
			materials[num].renderQueue = this.m_queues[num];
			num++;
		}
	}

	// Token: 0x040075C4 RID: 30148
	[SerializeField]
	protected int[] m_queues = new int[]
	{
		3000
	};
}
