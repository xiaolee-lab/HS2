using System;
using CharaCustom;
using Illusion.Extensions;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02001367 RID: 4967
public class TestEntryCustom : BaseLoader
{
	// Token: 0x0600A666 RID: 42598 RVA: 0x0043A7AF File Offset: 0x00438BAF
	public void UpdateCharasList(bool modeNew, int modeSex)
	{
		this.charaLoadWin.UpdateWindow(modeNew, modeSex, false, null);
	}

	// Token: 0x0600A667 RID: 42599 RVA: 0x0043A7C0 File Offset: 0x00438BC0
	private void Start()
	{
		this.charaLoadWin.btnDisableNotSelect01 = true;
		this.charaLoadWin.btnDisableNotSelect02 = true;
		this.charaLoadWin.btnDisableNotSelect03 = true;
		this.btnNewMale.OnClickAsObservable().Subscribe(delegate(Unit _)
		{
			CharaCustom.modeNew = true;
			CharaCustom.modeSex = 0;
			Scene.Data data = new Scene.Data
			{
				levelName = "CharaCustom",
				isAdd = false,
				isFade = true,
				isAsync = true,
				isDrawProgressBar = false
			};
			Singleton<Scene>.Instance.LoadReserve(data, false);
		});
		this.btnNewFemale.OnClickAsObservable().Subscribe(delegate(Unit _)
		{
			CharaCustom.modeNew = true;
			CharaCustom.modeSex = 1;
			Scene.Data data = new Scene.Data
			{
				levelName = "CharaCustom",
				isAdd = false,
				isFade = true,
				isAsync = true,
				isDrawProgressBar = false
			};
			Singleton<Scene>.Instance.LoadReserve(data, false);
		});
		this.btnEditMale.OnClickAsObservable().Subscribe(delegate(Unit _)
		{
			CharaCustom.modeNew = false;
			CharaCustom.modeSex = 0;
			this.objCharaSelect.SetActiveIfDifferent(true);
			this.UpdateCharasList(false, 0);
		});
		this.btnEditFemale.OnClickAsObservable().Subscribe(delegate(Unit _)
		{
			CharaCustom.modeNew = false;
			CharaCustom.modeSex = 1;
			this.objCharaSelect.SetActiveIfDifferent(true);
			this.UpdateCharasList(false, 1);
		});
		this.charaLoadWin.onClick03 = delegate(CustomCharaFileInfo info, int flags)
		{
			CharaCustom.editCharaFileName = info.FileName;
			Scene.Data data = new Scene.Data
			{
				levelName = "CharaCustom",
				isAdd = false,
				isFade = true,
				isAsync = true,
				isDrawProgressBar = false
			};
			Singleton<Scene>.Instance.LoadReserve(data, false);
		};
		this.btnUploader.OnClickAsObservable().Subscribe(delegate(Unit _)
		{
			bool flag = !Singleton<GameSystem>.Instance.HandleName.IsNullOrEmpty();
			Singleton<GameSystem>.Instance.networkSceneName = "Uploader";
			if (flag)
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
			else
			{
				Singleton<Scene>.Instance.LoadReserve(new Scene.Data
				{
					levelName = "EntryHandleName",
					isFade = true
				}, false);
			}
		});
		this.btnDownloader.OnClickAsObservable().Subscribe(delegate(Unit _)
		{
			bool flag = !Singleton<GameSystem>.Instance.HandleName.IsNullOrEmpty();
			Singleton<GameSystem>.Instance.networkSceneName = "Downloader";
			if (flag)
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
			else
			{
				Singleton<Scene>.Instance.LoadReserve(new Scene.Data
				{
					levelName = "EntryHandleName",
					isFade = true
				}, false);
			}
		});
	}

	// Token: 0x040082B6 RID: 33462
	[SerializeField]
	private Button btnNewMale;

	// Token: 0x040082B7 RID: 33463
	[SerializeField]
	private Button btnNewFemale;

	// Token: 0x040082B8 RID: 33464
	[SerializeField]
	private Button btnEditMale;

	// Token: 0x040082B9 RID: 33465
	[SerializeField]
	private Button btnEditFemale;

	// Token: 0x040082BA RID: 33466
	[SerializeField]
	private GameObject objCharaSelect;

	// Token: 0x040082BB RID: 33467
	[SerializeField]
	private CustomCharaWindow charaLoadWin;

	// Token: 0x040082BC RID: 33468
	[SerializeField]
	private Button btnUploader;

	// Token: 0x040082BD RID: 33469
	[SerializeField]
	private Button btnDownloader;
}
