using System;
using System.Collections;
using System.IO;
using Illusion.Extensions;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UploaderSystem
{
	// Token: 0x02000FF0 RID: 4080
	public class NetworkCheckScene : MonoBehaviour
	{
		// Token: 0x06008953 RID: 35155 RVA: 0x003928C0 File Offset: 0x00390CC0
		private void Start()
		{
			string path = UserData.Path + "maintenance.dat";
			if (File.Exists(path))
			{
				this.maintenance = true;
			}
			this.caCheck = new CoroutineAssist(this, new Func<IEnumerator>(this.CheckNetworkStatus));
			this.caCheck.Start(true, 10f);
			this.startTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06008954 RID: 35156 RVA: 0x00392924 File Offset: 0x00390D24
		public IEnumerator CheckNetworkStatus()
		{
			string url = CreateURL.LoadURL("ais_check_url.dat");
			if (url.IsNullOrEmpty())
			{
				if (this.txtInfomation)
				{
					this.txtInfomation.text = "サーバーへアクセスするための情報の読み込みに失敗しました。";
				}
				this.caCheck.EndStatus();
				if (this.objClick)
				{
					this.objClick.SetActiveIfDifferent(true);
				}
				if (this.objMsg)
				{
					this.objMsg.SetActiveIfDifferent(false);
				}
				this.nextType = 2;
				yield break;
			}
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("mode", (!(Singleton<GameSystem>.Instance.networkSceneName == "Uploader")) ? 1 : 0);
			wwwform.AddField("version", Singleton<GameSystem>.Instance.GameVersion.ToString());
			WWW www = new WWW(url, wwwform);
			yield return www;
			if (www.error != null)
			{
				if (this.txtInfomation)
				{
					this.txtInfomation.text = "サーバーへのアクセスに失敗しました。";
				}
				this.caCheck.EndStatus();
				if (this.objClick)
				{
					this.objClick.SetActiveIfDifferent(true);
				}
				if (this.objMsg)
				{
					this.objMsg.SetActiveIfDifferent(false);
				}
				this.nextType = 2;
				yield break;
			}
			string[] array = www.text.Split(new char[]
			{
				"\t"[0]
			});
			if ("0" == array[0])
			{
				this.nextType = 1;
			}
			else if ("1" == array[0])
			{
				this.nextType = 0;
				this.txtInfomation.text = array[1];
				if (this.objClick)
				{
					this.objClick.SetActiveIfDifferent(true);
				}
				if (this.objMsg)
				{
					this.objMsg.SetActiveIfDifferent(false);
				}
			}
			else if (this.maintenance && array[1].Contains("メンテナンス"))
			{
				this.nextType = 1;
			}
			else
			{
				this.nextType = 2;
				this.txtInfomation.text = array[1];
				if (this.objClick)
				{
					this.objClick.SetActiveIfDifferent(true);
				}
				if (this.objMsg)
				{
					this.objMsg.SetActiveIfDifferent(false);
				}
			}
			this.caCheck.EndStatus();
			yield return null;
			yield break;
		}

		// Token: 0x06008955 RID: 35157 RVA: 0x00392940 File Offset: 0x00390D40
		private void Update()
		{
			if (this.nextType == -1)
			{
				int num = Mathf.FloorToInt(Time.realtimeSinceStartup - this.startTime);
				string text = "サーバーをチェックしています";
				for (int i = 0; i < num; i++)
				{
					text += "．";
				}
				if (this.txtInfomation)
				{
					this.txtInfomation.text = text;
				}
			}
			if (this.caCheck != null && this.caCheck.status == CoroutineAssist.Status.Run && this.caCheck.TimeOutCheck())
			{
				this.caCheck.End();
				if (this.txtInfomation)
				{
					this.txtInfomation.text = "サーバーへのアクセスに失敗しました。";
				}
				this.caCheck.EndStatus();
				if (this.objClick)
				{
					this.objClick.SetActiveIfDifferent(true);
				}
				if (this.objMsg)
				{
					this.objMsg.SetActiveIfDifferent(false);
				}
				this.nextType = 2;
			}
			if (!this.changeScene)
			{
				if (this.nextType == 0)
				{
					if (UnityEngine.Input.anyKeyDown)
					{
						this.nextType = 1;
					}
				}
				else if (this.nextType == 1)
				{
					bool isAdd = false;
					bool isFade = true;
					Singleton<Scene>.Instance.LoadReserve(new Scene.Data
					{
						isAdd = isAdd,
						levelName = Singleton<GameSystem>.Instance.networkSceneName,
						isFade = isFade
					}, false);
					this.changeScene = true;
				}
				else if (this.nextType == 2 && UnityEngine.Input.anyKeyDown)
				{
					Singleton<Scene>.Instance.LoadReserve(new Scene.Data
					{
						levelName = "Title",
						isFade = true
					}, false);
					this.changeScene = true;
				}
			}
		}

		// Token: 0x04006F3D RID: 28477
		public Text txtInfomation;

		// Token: 0x04006F3E RID: 28478
		public GameObject objClick;

		// Token: 0x04006F3F RID: 28479
		public GameObject objMsg;

		// Token: 0x04006F40 RID: 28480
		private CoroutineAssist caCheck;

		// Token: 0x04006F41 RID: 28481
		private int nextType = -1;

		// Token: 0x04006F42 RID: 28482
		private bool changeScene;

		// Token: 0x04006F43 RID: 28483
		private float startTime;

		// Token: 0x04006F44 RID: 28484
		private bool maintenance;

		// Token: 0x04006F45 RID: 28485
		private bool isActiveUploader;
	}
}
