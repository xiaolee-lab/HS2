using System;
using System.Runtime.CompilerServices;
using Illusion.Extensions;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011DF RID: 4575
	[DefaultExecutionOrder(30000)]
	public class ParticleParent : MonoBehaviour
	{
		// Token: 0x17001FE1 RID: 8161
		// (get) Token: 0x06009664 RID: 38500 RVA: 0x003E0F2A File Offset: 0x003DF32A
		// (set) Token: 0x06009665 RID: 38501 RVA: 0x003E0F32 File Offset: 0x003DF332
		public GameObject ObjOriginal { get; set; }

		// Token: 0x17001FE2 RID: 8162
		// (get) Token: 0x06009666 RID: 38502 RVA: 0x003E0F3C File Offset: 0x003DF33C
		private Transform Transform
		{
			[CompilerGenerated]
			get
			{
				Transform result;
				if ((result = this._transform) == null)
				{
					result = (this._transform = base.transform);
				}
				return result;
			}
		}

		// Token: 0x06009667 RID: 38503 RVA: 0x003E0F65 File Offset: 0x003DF365
		private void OnEnable()
		{
			if (this.ObjOriginal != null)
			{
				this.ObjOriginal.SetActiveIfDifferent(true);
			}
		}

		// Token: 0x06009668 RID: 38504 RVA: 0x003E0F85 File Offset: 0x003DF385
		private void OnDisable()
		{
			if (this.ObjOriginal != null)
			{
				this.ObjOriginal.SetActiveIfDifferent(false);
			}
		}

		// Token: 0x06009669 RID: 38505 RVA: 0x003E0FA5 File Offset: 0x003DF3A5
		private void OnDestroy()
		{
			if (this.ObjOriginal != null)
			{
				UnityEngine.Object.Destroy(this.ObjOriginal);
			}
			this.ObjOriginal = null;
		}

		// Token: 0x0600966A RID: 38506 RVA: 0x003E0FCC File Offset: 0x003DF3CC
		private void LateUpdate()
		{
			Transform transform = this.ObjOriginal.transform;
			transform.position = this.Transform.position;
			transform.rotation = this.Transform.rotation;
			transform.localScale = this.Transform.lossyScale;
		}

		// Token: 0x040078E3 RID: 30947
		private Transform _transform;
	}
}
