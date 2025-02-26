using System;
using AIChara;
using AIProject;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000A08 RID: 2568
	public class CvsExit : MonoBehaviour
	{
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06004C67 RID: 19559 RVA: 0x001D69B3 File Offset: 0x001D4DB3
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06004C68 RID: 19560 RVA: 0x001D69BA File Offset: 0x001D4DBA
		private ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x001D69C7 File Offset: 0x001D4DC7
		private void Start()
		{
			if (this.btnExit)
			{
				this.btnExit.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					if (this.customBase.modeNew)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						this.popupEndNew.actYes = delegate()
						{
							this.ExitScene(false);
						};
						this.popupEndNew.SetupWindow(string.Empty, string.Empty, string.Empty, string.Empty);
					}
					else
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						this.popupEndEdit.actYes = delegate()
						{
							this.ExitScene(true);
						};
						this.popupEndEdit.actYes2 = delegate()
						{
							this.ExitScene(false);
						};
						this.popupEndEdit.SetupWindow(string.Empty, string.Empty, string.Empty, string.Empty);
					}
				});
			}
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x001D69F8 File Offset: 0x001D4DF8
		public void ExitScene(bool saveChara)
		{
			if (saveChara)
			{
				this.chaCtrl.chaFile.SaveCharaFile(this.customBase.editSaveFileName, byte.MaxValue, false);
			}
			this.customBase.customCtrl.cvsChangeScene.gameObject.SetActive(true);
			this.customBase.customSettingSave.Save();
			if (this.customBase.nextSceneName.IsNullOrEmpty())
			{
				Singleton<Scene>.Instance.UnLoad();
			}
			else
			{
				Scene.Data data = new Scene.Data
				{
					levelName = this.customBase.nextSceneName,
					isAdd = false,
					isFade = true,
					isAsync = true,
					isDrawProgressBar = false
				};
				Singleton<Scene>.Instance.LoadReserve(data, false);
			}
		}

		// Token: 0x04004621 RID: 17953
		[SerializeField]
		private Button btnExit;

		// Token: 0x04004622 RID: 17954
		[SerializeField]
		private PopupCheck popupEndNew;

		// Token: 0x04004623 RID: 17955
		[SerializeField]
		private PopupCheck popupEndEdit;
	}
}
