using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007C5 RID: 1989
	public abstract class CmpBase : MonoBehaviour
	{
		// Token: 0x06003157 RID: 12631 RVA: 0x001249B9 File Offset: 0x00122DB9
		public CmpBase(bool _baseDB)
		{
			this.baseDB = _baseDB;
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x001249C8 File Offset: 0x00122DC8
		protected virtual void Reacquire()
		{
			this.rendCheckVisible = base.GetComponentsInChildren<Renderer>(true);
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x001249D7 File Offset: 0x00122DD7
		private void Reset()
		{
			this.SetReferenceObject();
			this.rendCheckVisible = base.GetComponentsInChildren<Renderer>(true);
		}

		// Token: 0x0600315A RID: 12634
		public abstract void SetReferenceObject();

		// Token: 0x0600315B RID: 12635 RVA: 0x001249EC File Offset: 0x00122DEC
		public void InitDynamicBones()
		{
			this.dynamicBones = null;
			if (this.baseDB)
			{
				this.dynamicBones = base.GetComponentsInChildren<DynamicBone>(true);
			}
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x00124A10 File Offset: 0x00122E10
		public void ResetDynamicBones(bool includeInactive = false)
		{
			if (this.baseDB && this.dynamicBones != null)
			{
				foreach (DynamicBone dynamicBone in this.dynamicBones)
				{
					if (dynamicBone.enabled || includeInactive)
					{
						dynamicBone.ResetParticlesPosition();
					}
				}
			}
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x00124A6C File Offset: 0x00122E6C
		public void EnableDynamicBones(bool enable)
		{
			if (this.baseDB && this.dynamicBones != null)
			{
				foreach (DynamicBone dynamicBone in this.dynamicBones)
				{
					if (dynamicBone.enabled != enable)
					{
						dynamicBone.enabled = enable;
						if (enable)
						{
							dynamicBone.ResetParticlesPosition();
						}
					}
				}
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x00124AD0 File Offset: 0x00122ED0
		public bool isVisible
		{
			get
			{
				if (this.rendCheckVisible == null || this.rendCheckVisible.Length == 0)
				{
					return false;
				}
				foreach (Renderer renderer in this.rendCheckVisible)
				{
					if (renderer.isVisible)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x04002F2C RID: 12076
		private bool baseDB;

		// Token: 0x04002F2D RID: 12077
		private DynamicBone[] dynamicBones;

		// Token: 0x04002F2E RID: 12078
		[Header("カメラ内判定用")]
		public Renderer[] rendCheckVisible;

		// Token: 0x04002F2F RID: 12079
		[Button("Reacquire", "再取得", new object[]
		{

		})]
		public int reacquire;
	}
}
