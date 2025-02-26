using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011DD RID: 4573
	public class ParticleAssist : MonoBehaviour
	{
		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x06009656 RID: 38486 RVA: 0x003E0B78 File Offset: 0x003DEF78
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

		// Token: 0x17001FDD RID: 8157
		// (set) Token: 0x06009657 RID: 38487 RVA: 0x003E0BA1 File Offset: 0x003DEFA1
		private ParticleAssist.TransformC Local
		{
			set
			{
				this.Transform.localPosition = value.pos;
				this.Transform.localEulerAngles = value.rot;
				this.Transform.localScale = value.scale;
			}
		}

		// Token: 0x06009658 RID: 38488 RVA: 0x003E0BD8 File Offset: 0x003DEFD8
		private void Start()
		{
			Observable.FromCoroutine(new Func<IEnumerator>(this.YieldNull), false).Subscribe<Unit>().AddTo(this);
			(from _ in this.LateUpdateAsObservable()
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.Local = this.original;
			}).AddTo(this);
			Observable.FromCoroutine(new Func<IEnumerator>(this.EndOfFrame), false).Subscribe<Unit>().AddTo(this);
		}

		// Token: 0x06009659 RID: 38489 RVA: 0x003E0C50 File Offset: 0x003DF050
		private IEnumerator YieldNull()
		{
			for (;;)
			{
				yield return null;
				this.original.Local = this.Transform;
				this.Local = this.particle.WorldToLocal(this.Transform.parent);
			}
			yield break;
		}

		// Token: 0x0600965A RID: 38490 RVA: 0x003E0C6C File Offset: 0x003DF06C
		private IEnumerator EndOfFrame()
		{
			WaitForEndOfFrame end = new WaitForEndOfFrame();
			for (;;)
			{
				yield return end;
				this.particle.Transform = this.Transform;
			}
			yield break;
		}

		// Token: 0x040078DC RID: 30940
		private Transform _transform;

		// Token: 0x040078DD RID: 30941
		private ParticleAssist.TransformC original = new ParticleAssist.TransformC();

		// Token: 0x040078DE RID: 30942
		private ParticleAssist.TransformC particle = new ParticleAssist.TransformC();

		// Token: 0x020011DE RID: 4574
		[Serializable]
		public class TransformC
		{
			// Token: 0x17001FDE RID: 8158
			// (get) Token: 0x0600965E RID: 38494 RVA: 0x003E0CC6 File Offset: 0x003DF0C6
			// (set) Token: 0x0600965F RID: 38495 RVA: 0x003E0CD3 File Offset: 0x003DF0D3
			public Quaternion Rottation
			{
				get
				{
					return Quaternion.Euler(this.rot);
				}
				set
				{
					this.rot = value.eulerAngles;
				}
			}

			// Token: 0x17001FDF RID: 8159
			// (set) Token: 0x06009660 RID: 38496 RVA: 0x003E0CE2 File Offset: 0x003DF0E2
			public Transform Transform
			{
				set
				{
					this.pos = value.position;
					this.rot = value.eulerAngles;
					this.scale = value.lossyScale;
				}
			}

			// Token: 0x17001FE0 RID: 8160
			// (set) Token: 0x06009661 RID: 38497 RVA: 0x003E0D08 File Offset: 0x003DF108
			public Transform Local
			{
				set
				{
					this.pos = value.localPosition;
					this.rot = value.localEulerAngles;
					this.scale = value.localScale;
				}
			}

			// Token: 0x06009662 RID: 38498 RVA: 0x003E0D30 File Offset: 0x003DF130
			public ParticleAssist.TransformC WorldToLocal(Transform _parent)
			{
				Matrix4x4 rhs = Matrix4x4.TRS(this.pos, this.Rottation, this.scale);
				rhs = _parent.worldToLocalMatrix * rhs;
				this.pos = new Vector3(rhs.m03, rhs.m13, rhs.m23);
				this.Rottation = rhs.rotation;
				this.scale = rhs.lossyScale;
				return this;
			}

			// Token: 0x040078DF RID: 30943
			public Vector3 pos = Vector3.zero;

			// Token: 0x040078E0 RID: 30944
			public Vector3 rot = Vector3.zero;

			// Token: 0x040078E1 RID: 30945
			public Vector3 scale = Vector3.one;
		}
	}
}
