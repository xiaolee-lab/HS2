using System;
using System.Runtime.CompilerServices;
using IllusionUtility.GetUtility;
using UnityEngine;

namespace AIProject.Animal.Resources
{
	// Token: 0x02000B92 RID: 2962
	public struct AnimalShapeInfo
	{
		// Token: 0x0600583D RID: 22589 RVA: 0x0025F2C0 File Offset: 0x0025D6C0
		public AnimalShapeInfo(string _name, int _index)
		{
			this.Name = _name;
			this.Index = _index;
			this.Enabled = (!this.Name.IsNullOrEmpty() && 0 <= this.Index);
			this.Root = null;
			this.Renderer = null;
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x0025F30E File Offset: 0x0025D70E
		public AnimalShapeInfo(bool _enabled, string _name, int _index)
		{
			this.Enabled = _enabled;
			this.Name = _name;
			this.Index = _index;
			this.Root = null;
			this.Renderer = null;
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x0600583F RID: 22591 RVA: 0x0025F333 File Offset: 0x0025D733
		public bool Active
		{
			[CompilerGenerated]
			get
			{
				return this.Enabled && !this.Name.IsNullOrEmpty() && 0 <= this.Index && this.Renderer != null;
			}
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x0025F36B File Offset: 0x0025D76B
		public void ClearState()
		{
			this.Root = null;
			this.Renderer = null;
		}

		// Token: 0x06005841 RID: 22593 RVA: 0x0025F37C File Offset: 0x0025D77C
		public bool SetRenderer(Transform _transform, string _findName = null)
		{
			if (_transform == null)
			{
				return false;
			}
			GameObject gameObject = _transform.FindLoop(_findName ?? this.Name);
			if (gameObject == null)
			{
				return false;
			}
			SkinnedMeshRenderer skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
			if (skinnedMeshRenderer == null)
			{
				skinnedMeshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>(true);
			}
			bool flag = skinnedMeshRenderer != null;
			if (flag)
			{
				this.Root = _transform;
				this.Renderer = skinnedMeshRenderer;
			}
			return flag;
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x0025F3F2 File Offset: 0x0025D7F2
		public float GetBlendShape()
		{
			if (!this.Active)
			{
				return 0f;
			}
			return this.Renderer.GetBlendShapeWeight(this.Index);
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x0025F416 File Offset: 0x0025D816
		public void SetBlendShape(float _value)
		{
			if (!this.Active)
			{
				return;
			}
			this.Renderer.SetBlendShapeWeight(this.Index, Mathf.Clamp(_value, 0f, 100f));
		}

		// Token: 0x040050FB RID: 20731
		public bool Enabled;

		// Token: 0x040050FC RID: 20732
		public string Name;

		// Token: 0x040050FD RID: 20733
		public int Index;

		// Token: 0x040050FE RID: 20734
		public Transform Root;

		// Token: 0x040050FF RID: 20735
		public SkinnedMeshRenderer Renderer;
	}
}
