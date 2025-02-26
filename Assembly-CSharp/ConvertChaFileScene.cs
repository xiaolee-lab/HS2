using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AIChara;
using Manager;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A34 RID: 2612
public class ConvertChaFileScene : BaseLoader
{
	// Token: 0x06004DD0 RID: 19920 RVA: 0x001DCC30 File Offset: 0x001DB030
	private void Start()
	{
		this.vSyncCountBack = QualitySettings.vSyncCount;
		QualitySettings.vSyncCount = 0;
		if (null != this.objHide)
		{
			this.objHide.SetActive(true);
		}
		string folder = UserData.Path + "chara/female/";
		this.lstEtcCha = new List<string>();
		this.fa.CreateFolderInfo(folder, "*.png", true, true);
		this.dictConvChaTrial = new Dictionary<string, ChaFile.ProductInfo>();
		int fileCount = this.fa.GetFileCount();
		string text = string.Empty;
		ChaFile.ProductInfo productInfo = null;
		for (int i = 0; i < fileCount; i++)
		{
			text = this.fa.lstFile[i].FullPath;
			productInfo = null;
			if (ChaFile.GetProductInfo(text, out productInfo) && productInfo.productNo == 100)
			{
				if (productInfo.version < new Version(1, 0, 0))
				{
					this.dictConvChaTrial[text] = productInfo;
				}
			}
			else
			{
				this.lstEtcCha.Add(text);
			}
		}
		if (this.lstEtcCha.Count != 0)
		{
			string text2 = UserData.Path + "chara/old/etc/";
			if (!Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			string text3 = string.Empty;
			for (int j = 0; j < this.lstEtcCha.Count; j++)
			{
				int num = 0;
				for (;;)
				{
					text3 = text2 + Path.GetFileNameWithoutExtension(this.lstEtcCha[j]) + ((num != 0) ? ("(" + num.ToString() + ").png") : ".png");
					if (!File.Exists(text3))
					{
						break;
					}
					num++;
				}
				File.Move(this.lstEtcCha[j], text3);
			}
		}
		if (this.dictConvChaTrial.Count == 0)
		{
			QualitySettings.vSyncCount = this.vSyncCountBack;
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "Logo",
				isFade = true,
				isAsync = true
			}, false);
			return;
		}
		if (this.dictConvChaTrial.Count != 0)
		{
			this.dirMoveCha = UserData.Path + "chara/old/female/";
			if (!Directory.Exists(this.dirMoveCha))
			{
				Directory.CreateDirectory(this.dirMoveCha);
			}
		}
		if (null != this.objHide)
		{
			this.objHide.SetActive(false);
		}
		this.caConvert = new CoroutineAssist(this, new Func<IEnumerator>(this.Convert));
		this.caConvert.Start(false, 20f);
	}

	// Token: 0x06004DD1 RID: 19921 RVA: 0x001DCEE8 File Offset: 0x001DB2E8
	private void Update()
	{
		if (this.objClick && this.objClick.activeSelf && UnityEngine.Input.anyKeyDown)
		{
			QualitySettings.vSyncCount = this.vSyncCountBack;
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "Logo",
				isFade = true,
				isAsync = true
			}, false);
		}
	}

	// Token: 0x06004DD2 RID: 19922 RVA: 0x001DCF58 File Offset: 0x001DB358
	private IEnumerator Convert()
	{
		int total = this.dictConvChaTrial.Count;
		float rate = 0f;
		string movePath = string.Empty;
		int renameCnt = 0;
		int cnvCnt = 0;
		foreach (var i in this.dictConvChaTrial.Select((KeyValuePair<string, ChaFile.ProductInfo> val, int idx) => new
		{
			val,
			idx
		}))
		{
			renameCnt = 0;
			for (;;)
			{
				movePath = this.dirMoveCha + Path.GetFileNameWithoutExtension(i.val.Key) + ((renameCnt != 0) ? ("(" + renameCnt.ToString() + ").png") : ".png");
				if (!File.Exists(movePath))
				{
					break;
				}
				renameCnt++;
			}
			ChaFileControl trialFile = new ChaFileControl();
			trialFile.LoadCharaFile(i.val.Key, byte.MaxValue, false, true);
			File.Move(i.val.Key, movePath);
			trialFile.userID = Singleton<GameSystem>.Instance.UserUUID;
			if (!trialFile.gameinfo.gameRegistration)
			{
				trialFile.gameinfo.MemberInit();
			}
			ChaFileControl.CheckDataRange(trialFile, true, true, true, null);
			trialFile.SaveCharaFile(i.val.Key, byte.MaxValue, false);
			this.txtResult.text = (cnvCnt + 1).ToString("00000") + " / " + total.ToString("00000");
			rate = (float)(cnvCnt + 1) / (float)total;
			this.imgPBFront.fillAmount = rate;
			cnvCnt++;
			yield return null;
		}
		this.caConvert.EndStatus();
		if (this.objClick)
		{
			this.objClick.SetActive(true);
		}
		yield return null;
		yield break;
	}

	// Token: 0x06004DD3 RID: 19923 RVA: 0x001DCF73 File Offset: 0x001DB373
	private void OnDestroy()
	{
		QualitySettings.vSyncCount = this.vSyncCountBack;
	}

	// Token: 0x04004700 RID: 18176
	public GameObject objHide;

	// Token: 0x04004701 RID: 18177
	public GameObject objClick;

	// Token: 0x04004702 RID: 18178
	[Header("キャラ用 -------------------------------------------")]
	public Image imgPBFront;

	// Token: 0x04004703 RID: 18179
	public Text txtResult;

	// Token: 0x04004704 RID: 18180
	private int vSyncCountBack;

	// Token: 0x04004705 RID: 18181
	private FolderAssist fa = new FolderAssist();

	// Token: 0x04004706 RID: 18182
	private string dirMoveCha = string.Empty;

	// Token: 0x04004707 RID: 18183
	private Dictionary<string, ChaFile.ProductInfo> dictConvChaTrial = new Dictionary<string, ChaFile.ProductInfo>();

	// Token: 0x04004708 RID: 18184
	private List<string> lstEtcCha;

	// Token: 0x04004709 RID: 18185
	public CoroutineAssist caConvert;
}
