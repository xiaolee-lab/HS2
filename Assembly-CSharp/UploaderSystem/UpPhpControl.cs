using System;
using System.Collections;
using System.IO;
using AIChara;
using AIProject;
using Housing;
using Manager;
using UniRx;
using UnityEngine;

namespace UploaderSystem
{
	// Token: 0x02000FF6 RID: 4086
	public class UpPhpControl : NetPhpControl
	{
		// Token: 0x0600897F RID: 35199 RVA: 0x00395E70 File Offset: 0x00394270
		public IEnumerator UploadChara(IObserver<bool> observer, bool update)
		{
			string msgType = (!update) ? "アップロード" : "更新";
			string msgCheck = string.Format("本当に{0}しますか？", msgType);
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			ObservableYieldInstruction<bool> chk = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.popupCheck.CheckAnswerCor(res, msgCheck)).ToYieldInstruction(false);
			yield return chk;
			if (!chk.Result)
			{
				yield break;
			}
			base.netInfo.BlockUI();
			string msg = string.Empty;
			string errorMsg = string.Format("{0}に失敗しました。", msgType);
			string path = this.uiCtrl.GetUploadFile(DataType.Chara);
			byte[] data = null;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					data = binaryReader.ReadBytes((int)fileStream.Length);
				}
			}
			if (data == null)
			{
				msg = string.Format("{0}するファイルが読み込めませんでした", msgType);
				base.netInfo.DrawMessage(Color.red, msg);
				observer.OnError(new Exception(msg));
				base.netInfo.UnblockUI();
				yield break;
			}
			string comment = this.uiCtrl.GetComment(DataType.Chara);
			Singleton<Character>.Instance.isMod = false;
			ChaFileControl cfc = new ChaFileControl();
			cfc.LoadCharaFile(path, byte.MaxValue, true, true);
			bool mod = false;
			if (Singleton<Character>.Instance.isMod)
			{
				mod = true;
			}
			NetworkInfo.CharaInfo info = base.netInfo.lstCharaInfo.Find((NetworkInfo.CharaInfo x) => x.data_uid == cfc.dataID);
			int chara_idx = 0;
			if (update)
			{
				if (info == null)
				{
					if (!base.netInfo.dictUploaded[0].TryGetValue(cfc.dataID, out chara_idx))
					{
						msg = "更新するファイルが特定出来ませんでした。";
						base.netInfo.DrawMessage(Color.red, msg);
						observer.OnError(new Exception(msg));
						base.netInfo.UnblockUI();
						yield break;
					}
				}
				else
				{
					chara_idx = info.idx;
				}
			}
			else if (info != null)
			{
				msg = string.Format("そのファイルは既にアップロードされています。", Array.Empty<object>());
				base.netInfo.DrawMessage(Color.red, msg);
				observer.OnError(new Exception(msg));
				base.netInfo.UnblockUI();
				yield break;
			}
			string URL = CreateURL.LoadURL("ais_uploadChara_url.dat");
			WWWForm wwwform = new WWWForm();
			if (update)
			{
				wwwform.AddField("mode", 3);
			}
			else
			{
				wwwform.AddField("mode", 2);
			}
			wwwform.AddBinaryData("png", data);
			if (update)
			{
				wwwform.AddField("index", chara_idx);
			}
			else
			{
				wwwform.AddField("chara_uid", cfc.dataID);
			}
			wwwform.AddField("upload_type", 0);
			wwwform.AddField("uuid", Singleton<GameSystem>.Instance.UserUUID);
			wwwform.AddField("passwd", Singleton<GameSystem>.Instance.UserPasswd);
			wwwform.AddField("mac_id", Singleton<GameSystem>.Instance.EncryptedMacAddress);
			wwwform.AddField("name", cfc.parameter.fullname);
			wwwform.AddField("voicetype", (cfc.parameter.sex != 0) ? cfc.parameter.personality : 99);
			wwwform.AddField("birthmonth", (int)cfc.parameter.birthMonth);
			wwwform.AddField("birthday", (int)cfc.parameter.birthDay);
			wwwform.AddField("comment", comment);
			wwwform.AddField("sex", (int)((!mod) ? cfc.parameter.sex : 99));
			wwwform.AddField("height", cfc.custom.GetHeightKind());
			wwwform.AddField("bust", (cfc.parameter.sex != 0) ? cfc.custom.GetBustSizeKind() : 99);
			wwwform.AddField("hair", cfc.custom.hair.kind);
			wwwform.AddField("phase", cfc.gameinfo.phase);
			wwwform.AddField("lifestyle", cfc.gameinfo.lifestyle);
			wwwform.AddField("pheromone", cfc.gameinfo.flavorState[0]);
			wwwform.AddField("reliability", cfc.gameinfo.flavorState[1]);
			wwwform.AddField("reason", cfc.gameinfo.flavorState[2]);
			wwwform.AddField("instinct", cfc.gameinfo.flavorState[3]);
			wwwform.AddField("dirty", cfc.gameinfo.flavorState[4]);
			wwwform.AddField("wariness", cfc.gameinfo.flavorState[5]);
			wwwform.AddField("sociability", cfc.gameinfo.flavorState[7]);
			wwwform.AddField("darkness", cfc.gameinfo.flavorState[6]);
			wwwform.AddField("skill_n01", cfc.gameinfo.normalSkill[0]);
			wwwform.AddField("skill_n02", cfc.gameinfo.normalSkill[1]);
			wwwform.AddField("skill_n03", cfc.gameinfo.normalSkill[2]);
			wwwform.AddField("skill_n04", cfc.gameinfo.normalSkill[3]);
			wwwform.AddField("skill_n05", cfc.gameinfo.normalSkill[4]);
			wwwform.AddField("skill_h01", cfc.gameinfo.hSkill[0]);
			wwwform.AddField("skill_h02", cfc.gameinfo.hSkill[1]);
			wwwform.AddField("skill_h03", cfc.gameinfo.hSkill[2]);
			wwwform.AddField("skill_h04", cfc.gameinfo.hSkill[3]);
			wwwform.AddField("skill_h05", cfc.gameinfo.hSkill[4]);
			wwwform.AddField("wish_01", cfc.parameter.wish01);
			wwwform.AddField("wish_02", cfc.parameter.wish02);
			wwwform.AddField("wish_03", cfc.parameter.wish03);
			wwwform.AddField("registration", (!cfc.gameinfo.gameRegistration) ? 0 : 1);
			ObservableYieldInstruction<string> o = ObservableWWW.Post(URL, wwwform, null).ToYieldInstruction(false);
			yield return o;
			if (o.HasError)
			{
				base.netInfo.DrawMessage(Color.red, errorMsg);
				observer.OnError(o.Error);
				base.netInfo.UnblockUI();
				yield break;
			}
			if (o.HasResult)
			{
				if (o.Result.StartsWith("ERROR_"))
				{
					base.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					base.netInfo.UnblockUI();
					yield break;
				}
				if (o.Result.StartsWith("SQLSTATE"))
				{
					base.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					base.netInfo.UnblockUI();
					yield break;
				}
			}
			if (!string.IsNullOrEmpty(o.Result))
			{
				bool flag = false;
				if (!update)
				{
					int value;
					if (int.TryParse(o.Result, out value))
					{
						flag = true;
						base.netInfo.dictUploaded[0][cfc.dataID] = value;
						this.uiCtrl.ChangeUploadData();
					}
				}
				else if ("S_OK" == o.Result)
				{
					flag = true;
				}
				if (flag)
				{
					observer.OnNext(true);
					observer.OnCompleted();
					base.netInfo.changeCharaList = true;
					base.netInfo.DrawMessage(NetworkDefine.colorWhite, string.Format("データを{0}しました。", msgType));
					base.netInfo.UnblockUI();
					yield break;
				}
			}
			base.netInfo.DrawMessage(Color.red, errorMsg);
			observer.OnError(new Exception(errorMsg));
			base.netInfo.UnblockUI();
			yield break;
		}

		// Token: 0x06008980 RID: 35200 RVA: 0x00395E9C File Offset: 0x0039429C
		public IEnumerator UploadHousing(IObserver<bool> observer)
		{
			string msgType = "アップロード";
			string msgCheck = string.Format("本当に{0}しますか？", msgType);
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			ObservableYieldInstruction<bool> chk = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.popupCheck.CheckAnswerCor(res, msgCheck)).ToYieldInstruction(false);
			yield return chk;
			if (!chk.Result)
			{
				yield break;
			}
			base.netInfo.BlockUI();
			string msg = string.Empty;
			string errorMsg = string.Format("{0}に失敗しました。", msgType);
			string path = this.uiCtrl.GetUploadFile(DataType.Housing);
			CraftInfo.AboutInfo hi = CraftInfo.LoadAbout(path);
			NetworkInfo.HousingInfo info = base.netInfo.lstHousingInfo.Find((NetworkInfo.HousingInfo x) => x.data_uid == hi.HUID);
			if (info != null)
			{
				msg = string.Format("そのファイルは既にアップロードされています。", Array.Empty<object>());
				base.netInfo.DrawMessage(Color.red, msg);
				observer.OnError(new Exception(msg));
				base.netInfo.UnblockUI();
				yield break;
			}
			byte[] data = null;
			bool compression = false;
			byte[] thumbnail = null;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					long length = binaryReader.BaseStream.Length;
					if (length / 1024L / 1024L >= 5L)
					{
						compression = true;
					}
					if (compression)
					{
						thumbnail = PngFile.LoadPngBytes(binaryReader);
						if (thumbnail == null)
						{
							compression = false;
						}
						binaryReader.BaseStream.Seek(0L, SeekOrigin.Begin);
					}
					data = binaryReader.ReadBytes((int)fileStream.Length);
				}
			}
			if (data == null)
			{
				msg = string.Format("{0}するファイルが読み込めませんでした", msgType);
				base.netInfo.DrawMessage(Color.red, msg);
				observer.OnError(new Exception(msg));
				base.netInfo.UnblockUI();
				yield break;
			}
			if (compression)
			{
				Texture2D tex = PngAssist.ChangeTextureFromByte(thumbnail, 0, 0, TextureFormat.ARGB32, false);
				if (null == tex)
				{
					compression = false;
				}
				else
				{
					TextureScale.Bilinear(tex, 320, 180);
					thumbnail = tex.EncodeToPNG();
					UnityEngine.Object.Destroy(tex);
					if (thumbnail == null)
					{
						compression = false;
					}
				}
				base.netInfo.popupMsg.StartMessage(0.2f, 2f, 0.2f, "ハウジングの圧縮を行っています…", 2);
				string entryName = Path.GetFileName(path);
				FileZip fileZip = new FileZip();
				ObservableYieldInstruction<byte[]> retBytes = Observable.FromCoroutine<byte[]>((IObserver<byte[]> res) => fileZip.FileZipCor(res, data, entryName)).ToYieldInstruction(false);
				yield return retBytes;
				if (!retBytes.HasError)
				{
					data = retBytes.Result;
				}
				else
				{
					compression = false;
				}
			}
			string title = this.uiCtrl.GetTitle();
			string comment = this.uiCtrl.GetComment(DataType.Housing);
			base.netInfo.popupMsg.StartMessage(0.2f, 2f, 0.2f, "ハウジングのアップロードを開始します…", 2);
			string URL = CreateURL.LoadURL("ais_uploadHousing_url.dat");
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("mode", 2);
			wwwform.AddField("housing_uid", hi.HUID);
			if (compression)
			{
				wwwform.AddField("upload_type", 1);
				wwwform.AddBinaryData("thumbnail", thumbnail);
				wwwform.AddBinaryData("zip", data);
			}
			else
			{
				wwwform.AddField("upload_type", 0);
				wwwform.AddBinaryData("png", data);
			}
			wwwform.AddField("uuid", Singleton<GameSystem>.Instance.UserUUID);
			wwwform.AddField("passwd", Singleton<GameSystem>.Instance.UserPasswd);
			wwwform.AddField("mac_id", Singleton<GameSystem>.Instance.EncryptedMacAddress);
			wwwform.AddField("name", title);
			wwwform.AddField("map_size", hi.AreaType);
			wwwform.AddField("comment", comment);
			ObservableYieldInstruction<string> o = ObservableWWW.Post(URL, wwwform, null).ToYieldInstruction(false);
			yield return o;
			if (o.HasError)
			{
				base.netInfo.DrawMessage(Color.red, errorMsg);
				observer.OnError(o.Error);
				base.netInfo.UnblockUI();
				yield break;
			}
			if (o.HasResult)
			{
				if (o.Result.StartsWith("ERROR_"))
				{
					base.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					base.netInfo.UnblockUI();
					yield break;
				}
				if (o.Result.StartsWith("SQLSTATE"))
				{
					base.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					base.netInfo.UnblockUI();
					yield break;
				}
			}
			if (!string.IsNullOrEmpty(o.Result))
			{
				bool flag = false;
				int value;
				if (int.TryParse(o.Result, out value))
				{
					flag = true;
					base.netInfo.dictUploaded[1][hi.HUID] = value;
					this.uiCtrl.ChangeUploadData();
				}
				if (flag)
				{
					observer.OnNext(true);
					observer.OnCompleted();
					base.netInfo.changeHosingList = true;
					base.netInfo.DrawMessage(NetworkDefine.colorWhite, string.Format("データを{0}しました。", msgType));
					base.netInfo.UnblockUI();
					yield break;
				}
			}
			base.netInfo.DrawMessage(Color.red, errorMsg);
			observer.OnError(new Exception(errorMsg));
			base.netInfo.UnblockUI();
			yield break;
		}

		// Token: 0x06008981 RID: 35201 RVA: 0x00395EBE File Offset: 0x003942BE
		private void Start()
		{
		}

		// Token: 0x06008982 RID: 35202 RVA: 0x00395EC0 File Offset: 0x003942C0
		private void Update()
		{
		}

		// Token: 0x04006F6E RID: 28526
		public UploadScene upScene;

		// Token: 0x04006F6F RID: 28527
		public UpUIControl uiCtrl;
	}
}
