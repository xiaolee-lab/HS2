using System;
using AIProject;
using Illusion.Extensions;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000992 RID: 2450
	public class PopupCheck : MonoBehaviour
	{
		// Token: 0x06004659 RID: 18009 RVA: 0x001AE674 File Offset: 0x001ACA74
		public void SetupWindow(string msg = "", string yes = "", string yes2 = "", string no = "")
		{
			if (!msg.IsNullOrEmpty())
			{
				this.textMsg.text = msg;
			}
			if (!yes.IsNullOrEmpty())
			{
				this.textYes.text = yes;
			}
			if (!yes2.IsNullOrEmpty())
			{
				this.textYes2.text = yes2;
			}
			if (!no.IsNullOrEmpty())
			{
				this.textNo.text = no;
			}
			this.canvasGroup.Enable(true, false);
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x001AE6EC File Offset: 0x001ACAEC
		private void Start()
		{
			if (this.btnYes)
			{
				this.btnYes.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
					if (this.actYes != null)
					{
						this.actYes();
					}
					this.canvasGroup.Enable(false, false);
				});
			}
			if (this.btnYes2)
			{
				this.btnYes2.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
					if (this.actYes2 != null)
					{
						this.actYes2();
					}
					this.canvasGroup.Enable(false, false);
				});
			}
			if (this.btnNo)
			{
				this.btnNo.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					if (this.actNo != null)
					{
						this.actNo();
					}
					this.canvasGroup.Enable(false, false);
				});
			}
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x001AE780 File Offset: 0x001ACB80
		private void Update()
		{
			if (this.canvasGroup.alpha == 1f && UnityEngine.Input.GetMouseButtonDown(1))
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				if (this.actNo != null)
				{
					this.actNo();
				}
				this.canvasGroup.Enable(false, false);
			}
		}

		// Token: 0x0400416C RID: 16748
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x0400416D RID: 16749
		[SerializeField]
		private Button btnYes;

		// Token: 0x0400416E RID: 16750
		[SerializeField]
		private Text textYes;

		// Token: 0x0400416F RID: 16751
		[SerializeField]
		private Button btnYes2;

		// Token: 0x04004170 RID: 16752
		[SerializeField]
		private Text textYes2;

		// Token: 0x04004171 RID: 16753
		[SerializeField]
		private Button btnNo;

		// Token: 0x04004172 RID: 16754
		[SerializeField]
		private Text textNo;

		// Token: 0x04004173 RID: 16755
		[SerializeField]
		private Text textMsg;

		// Token: 0x04004174 RID: 16756
		public Action actYes;

		// Token: 0x04004175 RID: 16757
		public Action actYes2;

		// Token: 0x04004176 RID: 16758
		public Action actNo;
	}
}
