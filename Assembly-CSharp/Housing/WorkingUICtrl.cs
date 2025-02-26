using System;
using Illusion.Extensions;
using UniRx;
using UnityEngine;

namespace Housing
{
	// Token: 0x020008BE RID: 2238
	public class WorkingUICtrl : MonoBehaviour
	{
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06003A6E RID: 14958 RVA: 0x00156440 File Offset: 0x00154840
		// (set) Token: 0x06003A6F RID: 14959 RVA: 0x0015644D File Offset: 0x0015484D
		public bool Visible
		{
			get
			{
				return this.visibleReactive.Value;
			}
			set
			{
				this.visibleReactive.Value = value;
			}
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x0015645B File Offset: 0x0015485B
		public void Play()
		{
			this.animatorProgressbar.enabled = true;
			this.animatorProgressbar.Play("progressbar", 0, 0f);
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x0015647F File Offset: 0x0015487F
		public void Stop()
		{
			this.animatorProgressbar.enabled = false;
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x0015648D File Offset: 0x0015488D
		private void Awake()
		{
			this.visibleReactive.Subscribe(delegate(bool _b)
			{
				this.canvasGroup.Enable(_b, false);
				if (_b)
				{
					this.Play();
				}
				else
				{
					this.Stop();
				}
			});
		}

		// Token: 0x040039B2 RID: 14770
		[Header("表示関係")]
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x040039B3 RID: 14771
		[Header("UIアニメ関係")]
		[SerializeField]
		private Animator animatorProgressbar;

		// Token: 0x040039B4 RID: 14772
		private BoolReactiveProperty visibleReactive = new BoolReactiveProperty(false);
	}
}
