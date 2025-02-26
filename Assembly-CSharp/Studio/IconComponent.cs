using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001257 RID: 4695
	public class IconComponent : MonoBehaviour
	{
		// Token: 0x1700210B RID: 8459
		// (set) Token: 0x06009AD9 RID: 39641 RVA: 0x003F8684 File Offset: 0x003F6A84
		public bool Active
		{
			set
			{
				this.renderer.gameObject.SetActive(value);
			}
		}

		// Token: 0x1700210C RID: 8460
		// (set) Token: 0x06009ADA RID: 39642 RVA: 0x003F8697 File Offset: 0x003F6A97
		public bool Visible
		{
			set
			{
				this.renderer.enabled = value;
			}
		}

		// Token: 0x1700210D RID: 8461
		// (set) Token: 0x06009ADB RID: 39643 RVA: 0x003F86A5 File Offset: 0x003F6AA5
		public int Layer
		{
			set
			{
				this.renderer.gameObject.layer = value;
			}
		}

		// Token: 0x06009ADC RID: 39644 RVA: 0x003F86B8 File Offset: 0x003F6AB8
		private void Awake()
		{
			this.transRender = this.renderer.transform;
			this.transTarget = Camera.main.transform;
			(from _ in this.UpdateAsObservable()
			where this.renderer.enabled
			select _).Subscribe(delegate(Unit _)
			{
				this.transRender.LookAt(this.transTarget.position);
			});
		}

		// Token: 0x04007B7E RID: 31614
		[SerializeField]
		private Renderer renderer;

		// Token: 0x04007B7F RID: 31615
		private Transform transTarget;

		// Token: 0x04007B80 RID: 31616
		private Transform transRender;
	}
}
