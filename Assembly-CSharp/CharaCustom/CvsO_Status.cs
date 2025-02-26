using System;
using System.Linq;
using Illusion.Extensions;
using UniRx;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x02000A04 RID: 2564
	public class CvsO_Status : CvsBase
	{
		// Token: 0x06004BFB RID: 19451 RVA: 0x001D379D File Offset: 0x001D1B9D
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x001D37C7 File Offset: 0x001D1BC7
		private void CalculateUI()
		{
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x001D37CC File Offset: 0x001D1BCC
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			foreach (int num in base.parameter.hsWish)
			{
				this.tglWish[num].SetIsOnWithoutCallback(true);
				this.tglWish[num].SetTextColor(1);
			}
			for (int i = 0; i < this.tglWish.Length; i++)
			{
				bool flag = false;
				foreach (int num2 in base.parameter.hsWish)
				{
					if (i == num2)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					this.tglWish[i].SetIsOnWithoutCallback(false);
				}
			}
			this.ChangeRestrictWishSelect();
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x001D38E0 File Offset: 0x001D1CE0
		public void ChangeRestrictWishSelect()
		{
			if (base.parameter.hsWish.Count >= 3)
			{
				foreach (UI_ToggleEx ui_ToggleEx in this.tglWish)
				{
					ui_ToggleEx.interactable = ui_ToggleEx.isOn;
				}
			}
			else
			{
				foreach (UI_ToggleEx ui_ToggleEx2 in this.tglWish)
				{
					ui_ToggleEx2.interactable = true;
				}
			}
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x001D3964 File Offset: 0x001D1D64
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsStatus += this.UpdateCustomUI;
			if (this.tglWish.Any<UI_ToggleEx>())
			{
				(from tgl in this.tglWish.Select((UI_ToggleEx val, int idx) => new
				{
					val,
					idx
				})
				where tgl.val != null
				select tgl).ToList().ForEach(delegate(tgl)
				{
					tgl.val.onValueChanged.AsObservable<bool>().Subscribe(delegate(bool isOn)
					{
						if (isOn)
						{
							this.parameter.hsWish.Add(tgl.idx);
						}
						else
						{
							this.parameter.hsWish.Remove(tgl.idx);
						}
						this.ChangeRestrictWishSelect();
					});
				});
			}
		}

		// Token: 0x040045C2 RID: 17858
		[SerializeField]
		private UI_ToggleEx[] tglWish;
	}
}
