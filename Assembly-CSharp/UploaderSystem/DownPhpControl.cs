using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AIProject;
using Manager;
using UniRx;
using UnityEngine;

namespace UploaderSystem
{
	// Token: 0x02000FFD RID: 4093
	public class DownPhpControl : NetPhpControl
	{
		// Token: 0x060089A9 RID: 35241 RVA: 0x00399AE0 File Offset: 0x00397EE0
		public IEnumerator AddApplauseCount(IObserver<bool> observer, DataType type, NetworkInfo.BaseIndex info)
		{
			string errorMsg = "拍手に失敗しました。";
			int user_idx = base.profile.userIdx;
			int index = info.idx;
			if (Singleton<GameSystem>.Instance.IsApplause(type, info.data_uid))
			{
				observer.OnNext(true);
				observer.OnCompleted();
				yield break;
			}
			string[] urls = new string[]
			{
				"ais_uploadChara_url.dat",
				"ais_uploadHousing_url.dat"
			};
			string URL = CreateURL.LoadURL(urls[(int)type]);
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("mode", 6);
			wwwform.AddField("index", index);
			ObservableYieldInstruction<string> o = ObservableWWW.Post(URL, wwwform, null).ToYieldInstruction(false);
			yield return o;
			if (o.HasError)
			{
				base.netInfo.DrawMessage(Color.red, errorMsg);
				observer.OnError(o.Error);
				yield break;
			}
			if (o.HasResult)
			{
				if (o.Result.StartsWith("ERROR_"))
				{
					base.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
				if (o.Result.StartsWith("SQLSTATE"))
				{
					base.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
			}
			if (!string.IsNullOrEmpty(o.Result) && "S_OK" == o.Result)
			{
				observer.OnNext(true);
				observer.OnCompleted();
				yield break;
			}
			base.netInfo.DrawMessage(Color.red, errorMsg);
			observer.OnError(new Exception(errorMsg));
			yield break;
		}

		// Token: 0x060089AA RID: 35242 RVA: 0x00399B10 File Offset: 0x00397F10
		public IEnumerator GetThumbnail(IObserver<bool> observer, DataType type)
		{
			string msg = string.Empty;
			string errorMsg = "サムネイル画像の取得に失敗しました。";
			Exception exp = null;
			Dictionary<string, Dictionary<int, Tuple<int, byte[]>>> dictUpdateThumb = new Dictionary<string, Dictionary<int, Tuple<int, byte[]>>>();
			Dictionary<int, Tuple<int, byte[]>> dictGetThumb = new Dictionary<int, Tuple<int, byte[]>>();
			Dictionary<int, byte[]> dictThumbBytes = new Dictionary<int, byte[]>();
			Tuple<int, int>[] datas = this.uiCtrl.GetThumbnailIndex(type);
			if (datas.Length == 0)
			{
				msg = "アップロードされたデータが見つかりませんでした。";
				base.netInfo.DrawMessage(Color.red, msg);
				observer.OnError(new Exception(msg));
				yield break;
			}
			if (base.cacheCtrl.enableCache)
			{
				for (int i = 0; i < datas.Length; i++)
				{
					dictThumbBytes[datas[i].Item1] = null;
					NetCacheControl.CacheHeader cacheHeader2;
					string cacheHeader = base.cacheCtrl.GetCacheHeader(type, datas[i].Item1, out cacheHeader2);
					if (string.Empty != cacheHeader)
					{
						if (datas[i].Item2 == cacheHeader2.update_idx)
						{
							dictThumbBytes[datas[i].Item1] = base.cacheCtrl.LoadCache(cacheHeader, cacheHeader2.pos, cacheHeader2.size);
						}
						else
						{
							Dictionary<int, Tuple<int, byte[]>> dictionary;
							if (!dictUpdateThumb.TryGetValue(cacheHeader, out dictionary))
							{
								dictionary = new Dictionary<int, Tuple<int, byte[]>>();
								dictUpdateThumb[cacheHeader] = dictionary;
							}
							dictionary[cacheHeader2.idx] = new Tuple<int, byte[]>(datas[i].Item2, null);
						}
					}
					else
					{
						dictGetThumb[datas[i].Item1] = new Tuple<int, byte[]>(datas[i].Item2, null);
					}
				}
			}
			else
			{
				for (int j = 0; j < datas.Length; j++)
				{
					dictGetThumb[datas[j].Item1] = new Tuple<int, byte[]>(datas[j].Item2, null);
					dictThumbBytes[datas[j].Item1] = null;
				}
			}
			if (dictGetThumb.Count != 0 || dictUpdateThumb.Count != 0)
			{
				List<int> lstIndexs = new List<int>();
				StringBuilder sb = new StringBuilder(256);
				foreach (KeyValuePair<string, Dictionary<int, Tuple<int, byte[]>>> keyValuePair in dictUpdateThumb)
				{
					foreach (KeyValuePair<int, Tuple<int, byte[]>> keyValuePair2 in keyValuePair.Value)
					{
						sb.Append(keyValuePair2.Key.ToString()).Append("\t");
						lstIndexs.Add(keyValuePair2.Key);
					}
				}
				foreach (KeyValuePair<int, Tuple<int, byte[]>> keyValuePair3 in dictGetThumb)
				{
					sb.Append(keyValuePair3.Key.ToString()).Append("\t");
					lstIndexs.Add(keyValuePair3.Key);
				}
				string strIdx = sb.ToString().TrimEnd(new char[]
				{
					'\t'
				});
				string[] urls = new string[]
				{
					"ais_uploadChara_url.dat",
					"ais_uploadHousing_url.dat"
				};
				string URL = CreateURL.LoadURL(urls[(int)type]);
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("mode", 1);
				wwwform.AddField("indexs", strIdx);
				ObservableYieldInstruction<string> o = ObservableWWW.Post(URL, wwwform, null).ToYieldInstruction(false);
				yield return o;
				if (o.HasError)
				{
					base.netInfo.DrawMessage(Color.red, errorMsg);
					exp = o.Error;
				}
				else
				{
					if (o.HasResult)
					{
						if (o.Result.StartsWith("ERROR_"))
						{
							base.netInfo.DrawMessage(Color.red, errorMsg);
							exp = new Exception(o.Result);
							goto IL_878;
						}
						if (o.Result.StartsWith("SQLSTATE"))
						{
							base.netInfo.DrawMessage(Color.red, errorMsg);
							exp = new Exception(o.Result);
							goto IL_878;
						}
					}
					if (!string.IsNullOrEmpty(o.Result))
					{
						string[] array = o.Result.Split(new char[]
						{
							'\t'
						});
						for (int k = 0; k < lstIndexs.Count; k++)
						{
							byte[] array2 = null;
							if (!(array[k] == string.Empty))
							{
								array2 = Convert.FromBase64String(array[k]);
							}
							dictThumbBytes[lstIndexs[k]] = array2;
							if (dictGetThumb.ContainsKey(lstIndexs[k]))
							{
								dictGetThumb[lstIndexs[k]] = new Tuple<int, byte[]>(dictGetThumb[lstIndexs[k]].Item1, array2);
							}
							else
							{
								foreach (KeyValuePair<string, Dictionary<int, Tuple<int, byte[]>>> keyValuePair4 in dictUpdateThumb)
								{
									if (keyValuePair4.Value.ContainsKey(lstIndexs[k]))
									{
										keyValuePair4.Value[lstIndexs[k]] = new Tuple<int, byte[]>(keyValuePair4.Value[lstIndexs[k]].Item1, array2);
										break;
									}
								}
							}
						}
						foreach (KeyValuePair<string, Dictionary<int, Tuple<int, byte[]>>> keyValuePair5 in dictUpdateThumb)
						{
							Dictionary<int, Tuple<int, byte[]>> dictionary2 = base.cacheCtrl.LoadCacheFile(keyValuePair5.Key);
							foreach (KeyValuePair<int, Tuple<int, byte[]>> keyValuePair6 in keyValuePair5.Value)
							{
								if (keyValuePair6.Value.Item2 != null)
								{
									dictionary2[keyValuePair6.Key] = keyValuePair6.Value;
								}
							}
							base.cacheCtrl.SaveCacheFile(keyValuePair5.Key, dictionary2);
						}
						base.cacheCtrl.CreateCache(type, dictGetThumb);
						base.cacheCtrl.UpdateCacheHeaderInfo(type);
					}
					else
					{
						base.netInfo.DrawMessage(Color.red, errorMsg);
						exp = new Exception(errorMsg);
					}
				}
			}
			IL_878:
			byte[][] work = new byte[datas.Length][];
			for (int l = 0; l < datas.Length; l++)
			{
				byte[] array3;
				if (dictThumbBytes.TryGetValue(datas[l].Item1, out array3))
				{
					work[l] = array3;
				}
			}
			this.uiCtrl.ChangeThumbnail(work);
			if (exp == null)
			{
				observer.OnNext(true);
				observer.OnCompleted();
			}
			else
			{
				observer.OnError(exp);
			}
			yield break;
		}

		// Token: 0x060089AB RID: 35243 RVA: 0x00399B3C File Offset: 0x00397F3C
		public IEnumerator DownloadPNG(IObserver<byte[]> observer, DataType type)
		{
			string errorMsg = "ダウンロードに失敗しました。";
			NetworkInfo.BaseIndex info = this.uiCtrl.GetSelectServerInfo(type);
			int index = info.idx;
			int add_count = (!Singleton<GameSystem>.Instance.IsDownload(type, info.data_uid)) ? 1 : 0;
			string[] urls = new string[]
			{
				"ais_uploadChara_url.dat",
				"ais_uploadHousing_url.dat"
			};
			string URL = CreateURL.LoadURL(urls[(int)type]);
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("mode", 4);
			wwwform.AddField("index", index);
			wwwform.AddField("add_count", add_count);
			ObservableYieldInstruction<string> o = ObservableWWW.Post(URL, wwwform, null).ToYieldInstruction(false);
			yield return o;
			if (o.HasError)
			{
				base.netInfo.DrawMessage(Color.red, errorMsg);
				observer.OnError(o.Error);
				yield break;
			}
			if (o.HasResult)
			{
				if (o.Result.StartsWith("ERROR_"))
				{
					base.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
				if (o.Result.StartsWith("SQLSTATE"))
				{
					base.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
			}
			if (!string.IsNullOrEmpty(o.Result))
			{
				byte[] data = null;
				string[] infos = o.Result.Split(new char[]
				{
					'\t'
				});
				bool compression = !("0" == infos[0]);
				data = Convert.FromBase64String(infos[1]);
				if (compression)
				{
					base.netInfo.popupMsg.StartMessage(0.2f, 2f, 0.2f, "ファイルの解凍を行っています…", 2);
					FileZip fileZip = new FileZip();
					ObservableYieldInstruction<byte[]> retBytes = Observable.FromCoroutine<byte[]>((IObserver<byte[]> res) => fileZip.FileUnzipCor(res, data)).ToYieldInstruction(false);
					yield return retBytes;
					if (retBytes.HasError)
					{
						string text = "ファイルの解凍に失敗しました。";
						base.netInfo.DrawMessage(Color.red, text);
						observer.OnError(new Exception(text));
						yield break;
					}
					data = retBytes.Result;
				}
				observer.OnNext(data);
				observer.OnCompleted();
				yield break;
			}
			base.netInfo.DrawMessage(Color.red, errorMsg);
			observer.OnError(new Exception(errorMsg));
			yield break;
		}

		// Token: 0x060089AC RID: 35244 RVA: 0x00399B68 File Offset: 0x00397F68
		public IEnumerator DeleteMyData(IObserver<bool> observer, DataType type)
		{
			string msgCheck = "本当に削除しますか？";
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			ObservableYieldInstruction<bool> chk = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.popupCheck.CheckAnswerCor(res, msgCheck)).ToYieldInstruction(false);
			yield return chk;
			if (!chk.Result)
			{
				observer.OnError(new Exception("削除しない"));
				yield break;
			}
			base.netInfo.BlockUI();
			string errorMsg = "データの削除に失敗しました。";
			NetworkInfo.BaseIndex info = this.uiCtrl.GetSelectServerInfo(type);
			int index = info.idx;
			string uuid = Singleton<GameSystem>.Instance.UserUUID;
			string passwd = Singleton<GameSystem>.Instance.UserPasswd;
			string[] urls = new string[]
			{
				"ais_uploadChara_url.dat",
				"ais_uploadHousing_url.dat"
			};
			string URL = CreateURL.LoadURL(urls[(int)type]);
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("mode", 5);
			wwwform.AddField("index", index);
			wwwform.AddField("uuid", uuid);
			wwwform.AddField("passwd", passwd);
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
			if (!string.IsNullOrEmpty(o.Result) && "S_OK" == o.Result)
			{
				if (type != DataType.Chara)
				{
					if (type == DataType.Housing)
					{
						base.netInfo.changeHosingList = true;
					}
				}
				else
				{
					base.netInfo.changeCharaList = true;
				}
				ObservableYieldInstruction<bool> ubi = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.UpdateBaseInfo(res)).ToYieldInstruction(false);
				yield return ubi;
				this.uiCtrl.changeSearchSetting = true;
				observer.OnNext(true);
				observer.OnCompleted();
				base.netInfo.UnblockUI();
				yield break;
			}
			base.netInfo.DrawMessage(Color.red, errorMsg);
			observer.OnError(new Exception(errorMsg));
			base.netInfo.UnblockUI();
			yield break;
		}

		// Token: 0x060089AD RID: 35245 RVA: 0x00399B94 File Offset: 0x00397F94
		public IEnumerator DeleteCache(IObserver<bool> observer, DataType type)
		{
			string msgCheck = "本当に削除しますか？";
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			ObservableYieldInstruction<bool> chk = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.popupCheck.CheckAnswerCor(res, msgCheck)).ToYieldInstruction(false);
			yield return chk;
			if (!chk.Result)
			{
				observer.OnError(new Exception("削除しない"));
				yield break;
			}
			base.netInfo.BlockUI();
			base.cacheCtrl.DeleteCache(type);
			observer.OnNext(true);
			observer.OnCompleted();
			base.netInfo.UnblockUI();
			yield break;
		}

		// Token: 0x060089AE RID: 35246 RVA: 0x00399BBD File Offset: 0x00397FBD
		private void Start()
		{
		}

		// Token: 0x060089AF RID: 35247 RVA: 0x00399BBF File Offset: 0x00397FBF
		private void Update()
		{
		}

		// Token: 0x04006FB5 RID: 28597
		public DownloadScene downScene;

		// Token: 0x04006FB6 RID: 28598
		public DownUIControl uiCtrl;
	}
}
