using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using AIChara;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UploaderSystem;

namespace ModChecker
{
	// Token: 0x02001369 RID: 4969
	public class ModChecker : BaseLoader
	{
		// Token: 0x0600A67C RID: 42620 RVA: 0x0043AF0A File Offset: 0x0043930A
		private string EncodeFromBase64(string buf)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(buf));
		}

		// Token: 0x0600A67D RID: 42621 RVA: 0x0043AF1C File Offset: 0x0043931C
		private IEnumerator Start()
		{
			this.vSyncCountBack = QualitySettings.vSyncCount;
			QualitySettings.vSyncCount = 0;
			this.log.StartLog();
			this.processing.update = false;
			if (this.btnCheck)
			{
				this.btnCheck.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					EventSystem.current.SetSelectedGameObject(null);
					this.cvsgMenu.Enable(false, false);
					this.log.AddLog("Modチェックを開始します", Array.Empty<object>());
					Observable.FromCoroutine<bool>((IObserver<bool> res) => this.CheckMod(res)).Subscribe(delegate(bool res)
					{
						this.log.AddLog("Modチェックが完了しました", Array.Empty<object>());
					}, delegate(Exception err)
					{
						this.cvsgMenu.Enable(true, false);
					}, delegate()
					{
						this.cvsgMenu.Enable(true, false);
					});
				});
			}
			if (this.btnOutput)
			{
				this.btnOutput.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					EventSystem.current.SetSelectedGameObject(null);
					this.cvsgMenu.Enable(false, false);
					this.log.AddLog("Mod情報の作成を開始します", Array.Empty<object>());
					Observable.FromCoroutine<bool>((IObserver<bool> res) => this.OutputModInfo(res)).Subscribe(delegate(bool res)
					{
						this.log.AddLog("Mod情報の作成が完了しました", Array.Empty<object>());
					}, delegate(Exception err)
					{
						this.cvsgMenu.Enable(true, false);
					}, delegate()
					{
						this.cvsgMenu.Enable(true, false);
					});
				});
			}
			if (this.btnCancel)
			{
				this.btnCancel.OnClickAsObservable().Subscribe(delegate(Unit push)
				{
					this.cancel = true;
				});
			}
			this.log.AddLog("チェックを開始出来ます。", Array.Empty<object>());
			yield break;
		}

		// Token: 0x0600A67E RID: 42622 RVA: 0x0043AF38 File Offset: 0x00439338
		private IEnumerator GetFiles(IObserver<bool> observer)
		{
			string folder = UserData.Create(this.checkDirName);
			UserData.Create(this.checkModDirName);
			this.faFile.CreateFolderInfoEx(folder, this.key, true);
			this.faFile.SortName(true);
			observer.OnNext(true);
			observer.OnCompleted();
			yield break;
		}

		// Token: 0x0600A67F RID: 42623 RVA: 0x0043AF5C File Offset: 0x0043935C
		private IEnumerator CheckMod(IObserver<bool> observer)
		{
			ObservableYieldInstruction<bool> result = null;
			this.processing.update = true;
			this.log.AddLog("キャラ情報を取得しています。", Array.Empty<object>());
			result = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.GetFiles(res)).ToYieldInstruction(false);
			yield return result;
			this.log.AddLog("キャラファイルをチェック中", Array.Empty<object>());
			ObservableYieldInstruction<Unit> ret = Observable.FromCoroutine(new Func<IEnumerator>(this.CheckChara), false).ToYieldInstruction(false);
			yield return ret;
			this.processing.update = false;
			observer.OnNext(true);
			observer.OnCompleted();
			yield break;
		}

		// Token: 0x0600A680 RID: 42624 RVA: 0x0043AF80 File Offset: 0x00439380
		private IEnumerator CheckChara()
		{
			string modDir = UserData.Create(this.checkModDirName);
			int filefig = this.faFile.GetFileCount();
			int idxTotal = this.log.AddLog("\u3000\u3000総ファイル数：{0}", new object[]
			{
				filefig
			});
			int idxFemale = this.log.AddLog("\u3000\u3000女ファイル数：0", Array.Empty<object>());
			int idxMale = this.log.AddLog("\u3000\u3000男ファイル数：0", Array.Empty<object>());
			int idxUnknown = this.log.AddLog("\u3000不明ファイル数：0", Array.Empty<object>());
			int idxMod = this.log.AddLog("ＭＯＤファイル数：0", Array.Empty<object>());
			ChaFileControl chafile = null;
			int femaleFig = 0;
			int maleFig = 0;
			int unknownFig = 0;
			int modFig = 0;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			for (int i = 0; i < filefig; i++)
			{
				if (this.cancel)
				{
					this.log.AddLog("処理を中断しました。", Array.Empty<object>());
					this.cancel = false;
					break;
				}
				chafile = new ChaFileControl();
				chafile.skipRangeCheck = true;
				bool ret = chafile.LoadCharaFile(this.faFile.lstFile[i].FullPath, byte.MaxValue, false, true);
				chafile.skipRangeCheck = false;
				if (ret)
				{
					if (chafile.parameter.sex == 0)
					{
						maleFig++;
					}
					else
					{
						femaleFig++;
					}
					List<string> list = new List<string>();
					if (ChaFileControl.CheckDataRange(chafile, true, true, true, list))
					{
						string fileName = Path.GetFileName(this.faFile.lstFile[i].FullPath);
						string text = modDir + fileName;
						if (File.Exists(text))
						{
							File.Delete(text);
						}
						File.Move(this.faFile.lstFile[i].FullPath, text);
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.faFile.lstFile[i].FileName);
						this.lstModInfo.Add(new ModChecker.CheckDataInfo
						{
							dataID = chafile.dataID,
							userID = chafile.userID,
							filename = fileNameWithoutExtension,
							lstCheck = list
						});
						modFig++;
					}
				}
				else
				{
					unknownFig++;
				}
				this.log.UpdateLog(idxFemale, "\u3000\u3000女ファイル数：{0}", new object[]
				{
					femaleFig
				});
				this.log.UpdateLog(idxMale, "\u3000\u3000男ファイル数：{0}", new object[]
				{
					maleFig
				});
				this.log.UpdateLog(idxUnknown, "\u3000不明ファイル数：{0}", new object[]
				{
					unknownFig
				});
				this.log.UpdateLog(idxMod, "ＭＯＤファイル数：{0}", new object[]
				{
					modFig
				});
				yield return null;
			}
			this.typeInfoText.textChara[0].text = femaleFig.ToString();
			this.typeInfoText.textChara[1].text = maleFig.ToString();
			this.typeInfoText.textChara[2].text = modFig.ToString();
			this.typeInfoText.textChara[3].text = unknownFig.ToString();
			sw.Stop();
			UnityEngine.Debug.Log("処理時間" + sw.Elapsed);
			yield break;
		}

		// Token: 0x0600A681 RID: 42625 RVA: 0x0043AF9C File Offset: 0x0043939C
		private IEnumerator OutputModInfo(IObserver<bool> observer)
		{
			this.log.AddLog("キャラMod情報を作成中", Array.Empty<object>());
			ObservableYieldInstruction<Unit> result = Observable.FromCoroutine((CancellationToken _) => this.CreateModInfo(), false).ToYieldInstruction(false);
			yield return result;
			observer.OnNext(true);
			observer.OnCompleted();
			yield break;
		}

		// Token: 0x0600A682 RID: 42626 RVA: 0x0043AFC0 File Offset: 0x004393C0
		private IEnumerator CreateModInfo()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int count = this.lstModInfo.Count;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ユーザーID, データID, ファイル名, Mod情報\n");
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append(this.lstModInfo[i].userID).Append("\t");
				stringBuilder.Append(this.lstModInfo[i].dataID).Append("\t");
				stringBuilder.Append(this.lstModInfo[i].filename).Append("\t");
				if (this.lstModInfo[i].lstCheck != null)
				{
					int count2 = this.lstModInfo[i].lstCheck.Count;
					for (int j = 0; j < count2; j++)
					{
						stringBuilder.Append(this.lstModInfo[i].lstCheck[j]).Append("\t");
					}
				}
				stringBuilder.Append("\n");
			}
			string path = UserData.Create(this.checkModDirName) + "modinfo.txt";
			File.WriteAllText(path, stringBuilder.ToString());
			stopwatch.Stop();
			UnityEngine.Debug.Log("処理時間" + stopwatch.Elapsed);
			yield break;
		}

		// Token: 0x0600A683 RID: 42627 RVA: 0x0043AFDB File Offset: 0x004393DB
		private void OnDestroy()
		{
			this.log.EndLog();
			QualitySettings.vSyncCount = this.vSyncCountBack;
		}

		// Token: 0x040082C9 RID: 33481
		[SerializeField]
		private CheckLog log;

		// Token: 0x040082CA RID: 33482
		[SerializeField]
		private Processing processing;

		// Token: 0x040082CB RID: 33483
		[SerializeField]
		private ModChecker.TypeInfoText typeInfoText;

		// Token: 0x040082CC RID: 33484
		[SerializeField]
		private Button btnCheck;

		// Token: 0x040082CD RID: 33485
		[SerializeField]
		private Button btnOutput;

		// Token: 0x040082CE RID: 33486
		[SerializeField]
		private Button btnCancel;

		// Token: 0x040082CF RID: 33487
		[SerializeField]
		private CanvasGroup cvsgMenu;

		// Token: 0x040082D0 RID: 33488
		private Dictionary<int, NetworkInfo.UserInfo> dictUserInfo = new Dictionary<int, NetworkInfo.UserInfo>();

		// Token: 0x040082D1 RID: 33489
		private FolderAssist faFile = new FolderAssist();

		// Token: 0x040082D2 RID: 33490
		private int vSyncCountBack;

		// Token: 0x040082D3 RID: 33491
		private bool cancel;

		// Token: 0x040082D4 RID: 33492
		private readonly string[] key = new string[]
		{
			"*.png"
		};

		// Token: 0x040082D5 RID: 33493
		private readonly string checkDirName = "check/chara";

		// Token: 0x040082D6 RID: 33494
		private readonly string checkModDirName = "check/mod";

		// Token: 0x040082D7 RID: 33495
		private List<ModChecker.CheckDataInfo> lstModInfo = new List<ModChecker.CheckDataInfo>();

		// Token: 0x0200136A RID: 4970
		[Serializable]
		public class TypeInfoText
		{
			// Token: 0x040082D8 RID: 33496
			public Text[] textChara;
		}

		// Token: 0x0200136B RID: 4971
		public class CheckDataInfo
		{
			// Token: 0x040082D9 RID: 33497
			public string dataID = string.Empty;

			// Token: 0x040082DA RID: 33498
			public string userID = string.Empty;

			// Token: 0x040082DB RID: 33499
			public string filename = string.Empty;

			// Token: 0x040082DC RID: 33500
			public List<string> lstCheck = new List<string>();
		}
	}
}
