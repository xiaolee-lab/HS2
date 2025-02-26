using System;
using AIProject;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000A05 RID: 2565
	public class CvsO_SubmenuEx : MonoBehaviour
	{
		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06004C04 RID: 19460 RVA: 0x001D3AD5 File Offset: 0x001D1ED5
		protected CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x001D3ADC File Offset: 0x001D1EDC
		private void Start()
		{
			if (this.btnFusion)
			{
				this.btnFusion.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.coFusion.UpdateCharasList();
					this.customBase.customCtrl.showFusionCvs = true;
				});
			}
			if (this.btnConfig)
			{
				this.btnConfig.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					if (null == Singleton<Game>.Instance.Config)
					{
						Singleton<Game>.Instance.LoadConfig();
					}
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				});
			}
			if (this.btnDrawMenu)
			{
				this.btnDrawMenu.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.customBase.customCtrl.showDrawMenu = true;
				});
			}
			if (this.btnDefaultLayout)
			{
				this.btnDefaultLayout.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.customBase.customSettingSave.ResetWinLayout();
					if (this.customUIDrags != null)
					{
						for (int i = 0; i < this.customUIDrags.Length; i++)
						{
							this.customUIDrags[i].UpdatePosition();
						}
					}
				});
			}
			if (this.btnUpdatePng)
			{
				this.btnUpdatePng.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.customBase.customCtrl.updatePng = true;
				});
			}
		}

		// Token: 0x040045C5 RID: 17861
		[SerializeField]
		private CvsO_Fusion coFusion;

		// Token: 0x040045C6 RID: 17862
		[SerializeField]
		private Button btnFusion;

		// Token: 0x040045C7 RID: 17863
		[SerializeField]
		private Button btnConfig;

		// Token: 0x040045C8 RID: 17864
		[SerializeField]
		private Button btnDrawMenu;

		// Token: 0x040045C9 RID: 17865
		[SerializeField]
		private Button btnDefaultLayout;

		// Token: 0x040045CA RID: 17866
		[SerializeField]
		private Button btnUpdatePng;

		// Token: 0x040045CB RID: 17867
		[SerializeField]
		private CustomUIDrag[] customUIDrags;
	}
}
