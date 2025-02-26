using System;
using AIProject;
using Manager;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace UploaderSystem
{
	// Token: 0x02000FF1 RID: 4081
	public class EntryHandleName : MonoBehaviour
	{
		// Token: 0x06008957 RID: 35159 RVA: 0x00392F30 File Offset: 0x00391330
		private void Start()
		{
			this.handleName = Singleton<GameSystem>.Instance.HandleName;
			this.inpHandleName.text = this.handleName;
			this.inpHandleName.ActivateInputField();
			this.inpHandleName.OnEndEditAsObservable().Subscribe(delegate(string buf)
			{
				if (buf == "イリュージョン公式")
				{
					this.notIllusion = false;
				}
				else
				{
					this.notIllusion = true;
				}
				this.handleName = buf;
			});
			if (this.btnYes)
			{
				TextMeshProUGUI text = this.btnYes.GetComponentInChildren<TextMeshProUGUI>(true);
				this.btnYes.UpdateAsObservable().Subscribe(delegate(Unit _)
				{
					bool flag = !this.handleName.IsNullOrEmpty() && this.notIllusion;
					this.btnYes.interactable = flag;
					if (text)
					{
						text.color = new Color(text.color.r, text.color.g, text.color.b, (!flag) ? 0.5f : 1f);
					}
				});
				this.btnYes.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					Singleton<GameSystem>.Instance.SaveHandleName(this.handleName);
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.cvsChangeScene.gameObject.SetActive(true);
					if ("Uploader" == this.backSceneName || "Downloader" == this.backSceneName)
					{
						Singleton<Scene>.Instance.UnLoad();
					}
					else
					{
						Scene.Data data = new Scene.Data
						{
							levelName = "NetworkCheckScene",
							isAdd = false,
							isFade = true,
							isAsync = true
						};
						Singleton<Scene>.Instance.LoadReserve(data, true);
					}
				});
			}
			if (this.btnNo)
			{
				this.btnNo.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					if ("Uploader" == this.backSceneName || "Downloader" == this.backSceneName)
					{
						Singleton<Scene>.Instance.UnLoad();
					}
					else
					{
						Singleton<Scene>.Instance.LoadReserve(new Scene.Data
						{
							levelName = "Title",
							isFade = true
						}, false);
					}
				});
			}
		}

		// Token: 0x06008958 RID: 35160 RVA: 0x0039301C File Offset: 0x0039141C
		private void OnDestroy()
		{
			if (this.onEnd != null)
			{
				this.onEnd();
			}
		}

		// Token: 0x04006F46 RID: 28486
		public string backSceneName = "Title";

		// Token: 0x04006F47 RID: 28487
		[SerializeField]
		private Canvas cvsChangeScene;

		// Token: 0x04006F48 RID: 28488
		[SerializeField]
		private InputField inpHandleName;

		// Token: 0x04006F49 RID: 28489
		[SerializeField]
		private Button btnYes;

		// Token: 0x04006F4A RID: 28490
		[SerializeField]
		private Button btnNo;

		// Token: 0x04006F4B RID: 28491
		private string handleName = string.Empty;

		// Token: 0x04006F4C RID: 28492
		private bool notIllusion = true;

		// Token: 0x04006F4D RID: 28493
		public Action onEnd;
	}
}
