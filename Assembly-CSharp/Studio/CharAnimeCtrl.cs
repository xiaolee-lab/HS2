using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011D4 RID: 4564
	public class CharAnimeCtrl : MonoBehaviour
	{
		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x060095E2 RID: 38370 RVA: 0x003DEED7 File Offset: 0x003DD2D7
		// (set) Token: 0x060095E3 RID: 38371 RVA: 0x003DEEF5 File Offset: 0x003DD2F5
		public bool isForceLoop
		{
			get
			{
				return this.oiCharInfo != null && this.oiCharInfo.isAnimeForceLoop;
			}
			set
			{
				this.oiCharInfo.isAnimeForceLoop = value;
			}
		}

		// Token: 0x17001FBB RID: 8123
		// (get) Token: 0x060095E4 RID: 38372 RVA: 0x003DEF03 File Offset: 0x003DD303
		// (set) Token: 0x060095E5 RID: 38373 RVA: 0x003DEF0B File Offset: 0x003DD30B
		public OICharInfo oiCharInfo { get; set; }

		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x060095E6 RID: 38374 RVA: 0x003DEF14 File Offset: 0x003DD314
		// (set) Token: 0x060095E7 RID: 38375 RVA: 0x003DEF1C File Offset: 0x003DD31C
		public int nameHadh { get; set; }

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x060095E8 RID: 38376 RVA: 0x003DEF28 File Offset: 0x003DD328
		public float normalizedTime
		{
			get
			{
				if (this.animator == null)
				{
					return 0f;
				}
				return this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
			}
		}

		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x060095E9 RID: 38377 RVA: 0x003DEF60 File Offset: 0x003DD360
		private bool isSon
		{
			get
			{
				return this.oiCharInfo != null && this.oiCharInfo.visibleSon;
			}
		}

		// Token: 0x060095EA RID: 38378 RVA: 0x003DEF7E File Offset: 0x003DD37E
		public void Play(string _name)
		{
			if (this.animator != null)
			{
				this.animator.Play(_name);
			}
		}

		// Token: 0x060095EB RID: 38379 RVA: 0x003DEF99 File Offset: 0x003DD399
		public void Play(string _name, float _normalizedTime)
		{
			if (this.animator == null)
			{
				return;
			}
			this.animator.Play(_name, 0, _normalizedTime);
		}

		// Token: 0x060095EC RID: 38380 RVA: 0x003DEFBB File Offset: 0x003DD3BB
		public void Play(string _name, float _normalizedTime, int _layer)
		{
			if (this.animator == null)
			{
				return;
			}
			if (_normalizedTime != 0f)
			{
				this.animator.Play(_name, _layer, _normalizedTime);
			}
			else
			{
				this.animator.Play(_name, _layer);
			}
		}

		// Token: 0x060095ED RID: 38381 RVA: 0x003DEFFC File Offset: 0x003DD3FC
		private void Awake()
		{
			(from _ in this.LateUpdateAsObservable()
			where this.isForceLoop
			select _).Subscribe(delegate(Unit _)
			{
				AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
				if (!currentAnimatorStateInfo.loop && currentAnimatorStateInfo.normalizedTime >= 1f)
				{
					this.animator.Play(this.nameHadh, 0, 0f);
				}
			});
			this.LateUpdateAsObservable().Subscribe(delegate(Unit _)
			{
				if (this.isSon && this.transSon)
				{
					this.transSon.localScale = new Vector3(this.oiCharInfo.sonLength, this.oiCharInfo.sonLength, this.oiCharInfo.sonLength);
				}
			});
		}

		// Token: 0x040078AA RID: 30890
		public Animator animator;

		// Token: 0x040078AD RID: 30893
		public Transform transSon;
	}
}
