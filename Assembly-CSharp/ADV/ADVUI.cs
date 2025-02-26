using System;
using System.Runtime.CompilerServices;
using AIProject;
using AIProject.UI.Viewer;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ADV
{
	// Token: 0x02000799 RID: 1945
	public class ADVUI : MonoBehaviour
	{
		// Token: 0x170007E5 RID: 2021
		// (set) Token: 0x06002E1B RID: 11803 RVA: 0x00104D3F File Offset: 0x0010313F
		public bool useSE
		{
			set
			{
				this.playSE.use = value;
			}
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x00104D4D File Offset: 0x0010314D
		public void PlaySE(SoundPack.SystemSE se)
		{
			this.playSE.Play(se);
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x00104D5B File Offset: 0x0010315B
		public void Visible(bool visible)
		{
			this.isVisible = visible;
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x00104D64 File Offset: 0x00103164
		public Toggle skip
		{
			[CompilerGenerated]
			get
			{
				return this._skip;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x00104D6C File Offset: 0x0010316C
		public Toggle auto
		{
			[CompilerGenerated]
			get
			{
				return this._auto;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x00104D74 File Offset: 0x00103174
		public Button log
		{
			[CompilerGenerated]
			get
			{
				return this._log;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x00104D7C File Offset: 0x0010317C
		public Button close
		{
			[CompilerGenerated]
			get
			{
				return this._close;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x00104D84 File Offset: 0x00103184
		private PlaySE playSE { get; } = new PlaySE();

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002E23 RID: 11811 RVA: 0x00104D8C File Offset: 0x0010318C
		// (set) Token: 0x06002E24 RID: 11812 RVA: 0x00104D94 File Offset: 0x00103194
		private bool isVisible { get; set; } = true;

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x00104D9D File Offset: 0x0010319D
		private BoolReactiveProperty visible { get; } = new BoolReactiveProperty(true);

		// Token: 0x06002E26 RID: 11814 RVA: 0x00104DA5 File Offset: 0x001031A5
		private void Start()
		{
			if (this._cg != null)
			{
				this.visible.TakeUntilDestroy(this._cg).Subscribe(delegate(bool isOn)
				{
					this._cg.alpha = (float)((!isOn) ? 0 : 1);
					this._cg.blocksRaycasts = isOn;
				}).AddTo(this);
			}
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x00104DE1 File Offset: 0x001031E1
		private void Update()
		{
			this.visible.Value = (this.isVisible && Config.GameData.TextWindowOption);
		}

		// Token: 0x04002D0B RID: 11531
		[SerializeField]
		private CanvasGroup _cg;

		// Token: 0x04002D0C RID: 11532
		[SerializeField]
		private Toggle _skip;

		// Token: 0x04002D0D RID: 11533
		[SerializeField]
		private Toggle _auto;

		// Token: 0x04002D0E RID: 11534
		[SerializeField]
		private Button _log;

		// Token: 0x04002D0F RID: 11535
		[SerializeField]
		private Button _close;
	}
}
