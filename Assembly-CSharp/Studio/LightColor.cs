using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001266 RID: 4710
	public class LightColor : MonoBehaviour
	{
		// Token: 0x1700215C RID: 8540
		// (set) Token: 0x06009BDE RID: 39902 RVA: 0x003FBDA4 File Offset: 0x003FA1A4
		public Color color
		{
			set
			{
				if (this.material)
				{
					this.material.color = value;
				}
			}
		}

		// Token: 0x06009BDF RID: 39903 RVA: 0x003FBDC2 File Offset: 0x003FA1C2
		public virtual void Awake()
		{
			this.material = this.renderer.material;
		}

		// Token: 0x04007C33 RID: 31795
		[SerializeField]
		private Renderer renderer;

		// Token: 0x04007C34 RID: 31796
		private Material material;
	}
}
