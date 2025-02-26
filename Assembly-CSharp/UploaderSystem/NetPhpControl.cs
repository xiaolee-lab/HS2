using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIChara;
using Illusion.Extensions;
using Manager;
using UniRx;
using UnityEngine;

namespace UploaderSystem
{
	// Token: 0x02001017 RID: 4119
	public class NetPhpControl : MonoBehaviour
	{
		// Token: 0x17001E2D RID: 7725
		// (get) Token: 0x06008A4F RID: 35407 RVA: 0x00393BF3 File Offset: 0x00391FF3
		protected NetworkInfo netInfo
		{
			get
			{
				return Singleton<NetworkInfo>.Instance;
			}
		}

		// Token: 0x17001E2E RID: 7726
		// (get) Token: 0x06008A50 RID: 35408 RVA: 0x00393BFA File Offset: 0x00391FFA
		protected NetCacheControl cacheCtrl
		{
			get
			{
				return (!Singleton<NetworkInfo>.IsInstance()) ? null : this.netInfo.cacheCtrl;
			}
		}

		// Token: 0x17001E2F RID: 7727
		// (get) Token: 0x06008A51 RID: 35409 RVA: 0x00393C17 File Offset: 0x00392017
		protected LogView logview
		{
			get
			{
				return this.netInfo.logview;
			}
		}

		// Token: 0x17001E30 RID: 7728
		// (get) Token: 0x06008A52 RID: 35410 RVA: 0x00393C24 File Offset: 0x00392024
		protected Net_PopupMsg popupMsg
		{
			get
			{
				return this.netInfo.popupMsg;
			}
		}

		// Token: 0x17001E31 RID: 7729
		// (get) Token: 0x06008A53 RID: 35411 RVA: 0x00393C31 File Offset: 0x00392031
		protected Net_PopupCheck popupCheck
		{
			get
			{
				return this.netInfo.popupCheck;
			}
		}

		// Token: 0x17001E32 RID: 7730
		// (get) Token: 0x06008A54 RID: 35412 RVA: 0x00393C3E File Offset: 0x0039203E
		public NetworkInfo.Profile profile
		{
			get
			{
				return this.netInfo.profile;
			}
		}

		// Token: 0x17001E33 RID: 7731
		// (get) Token: 0x06008A55 RID: 35413 RVA: 0x00393C4B File Offset: 0x0039204B
		public Dictionary<int, NetworkInfo.UserInfo> dictUserInfo
		{
			get
			{
				return this.netInfo.dictUserInfo;
			}
		}

		// Token: 0x17001E34 RID: 7732
		// (get) Token: 0x06008A56 RID: 35414 RVA: 0x00393C58 File Offset: 0x00392058
		public List<NetworkInfo.CharaInfo> lstCharaInfo
		{
			get
			{
				return this.netInfo.lstCharaInfo;
			}
		}

		// Token: 0x17001E35 RID: 7733
		// (get) Token: 0x06008A57 RID: 35415 RVA: 0x00393C65 File Offset: 0x00392065
		public List<NetworkInfo.HousingInfo> lstHousingInfo
		{
			get
			{
				return this.netInfo.lstHousingInfo;
			}
		}

		// Token: 0x06008A58 RID: 35416 RVA: 0x00393C72 File Offset: 0x00392072
		protected string EncodeFromBase64(string buf)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(buf));
		}

		// Token: 0x06008A59 RID: 35417 RVA: 0x00393C84 File Offset: 0x00392084
		public IEnumerator GetBaseInfo(bool upload)
		{
			this.logview.StartLog();
			this.logview.AddLog("サーバーから情報を取得しています。", Array.Empty<object>());
			this.logview.AddLog("最新のバージョン情報の取得を開始します。", Array.Empty<object>());
			ObservableYieldInstruction<bool> newest = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.GetNewestVersion(res)).ToYieldInstruction(false);
			yield return newest;
			this.logview.AddLog("ユーザー情報の確認を開始します。", Array.Empty<object>());
			ObservableYieldInstruction<bool> userIdx = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.UpdateUserInfo(res)).ToYieldInstruction(false);
			yield return userIdx;
			if (!userIdx.HasError)
			{
				this.logview.AddLog("ハンドル名の登録を開始します。", Array.Empty<object>());
				ObservableYieldInstruction<string> hn = Observable.FromCoroutine<string>((IObserver<string> res) => this.UpdateHandleName(res)).ToYieldInstruction(false);
				yield return hn;
				if (!hn.HasError)
				{
					this.logview.AddLog("登録された全ハンドル名の取得を開始します。", Array.Empty<object>());
					ObservableYieldInstruction<bool> allusers = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.GetAllUsers(res)).ToYieldInstruction(false);
					yield return allusers;
					if (!allusers.HasError)
					{
						this.logview.AddLog("全てのキャラ情報の取得を開始します。", Array.Empty<object>());
						ObservableYieldInstruction<bool> allchara = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.GetAllCharaInfo(res)).ToYieldInstruction(false);
						yield return allchara;
						if (!allchara.HasError)
						{
							this.netInfo.dictUploaded[0].Clear();
							this.logview.AddLog("全てのハウジング情報の取得を開始します。", Array.Empty<object>());
							ObservableYieldInstruction<bool> allscene = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.GetAllSceneInfo(res)).ToYieldInstruction(false);
							yield return allscene;
							if (!allscene.HasError)
							{
								this.netInfo.dictUploaded[1].Clear();
								if (!upload)
								{
									this.netInfo.netSelectHN.Init();
								}
								this.logview.AddLog("準備が完了しました。", Array.Empty<object>());
								this.logview.EndLog();
								this.logview.SetActiveCanvas(false);
								yield break;
							}
						}
					}
				}
			}
			this.logview.AddLog("ネットワークシステムの準備が完了しませんでした。", Array.Empty<object>());
			this.logview.EndLog();
			this.logview.onClose = delegate()
			{
				Singleton<Scene>.Instance.LoadReserve(new Scene.Data
				{
					levelName = "Title",
					isFade = true
				}, false);
				if (null != this.cvsChangeScene)
				{
					this.cvsChangeScene.gameObject.SetActiveIfDifferent(true);
				}
			};
			yield break;
		}

		// Token: 0x06008A5A RID: 35418 RVA: 0x00393CA8 File Offset: 0x003920A8
		public IEnumerator UpdateBaseInfo(IObserver<bool> observer)
		{
			this.netInfo.BlockUI();
			this.netInfo.noUserControl = true;
			if (this.netInfo.changeCharaList)
			{
				this.netInfo.DrawMessage(NetworkDefine.colorWhite, "全てのキャラ情報の取得を開始します。");
				ObservableYieldInstruction<bool> allchara = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.GetAllCharaInfo(res)).ToYieldInstruction(false);
				yield return allchara;
				if (allchara.HasError)
				{
					observer.OnError(new Exception("キャラ情報の取得に失敗しました"));
				}
				this.netInfo.changeCharaList = false;
				this.netInfo.dictUploaded[0].Clear();
			}
			if (this.netInfo.changeHosingList)
			{
				this.netInfo.DrawMessage(NetworkDefine.colorWhite, "全てのハウジング情報の取得を開始します。");
				ObservableYieldInstruction<bool> allscene = Observable.FromCoroutine<bool>((IObserver<bool> res) => this.GetAllSceneInfo(res)).ToYieldInstruction(false);
				yield return allscene;
				if (allscene.HasError)
				{
					observer.OnError(new Exception("ハウジング情報の取得に失敗しました"));
				}
				this.netInfo.changeHosingList = false;
				this.netInfo.dictUploaded[1].Clear();
			}
			this.netInfo.noUserControl = false;
			this.netInfo.DrawMessage(NetworkDefine.colorWhite, "データの取得が完了しました。");
			observer.OnNext(true);
			observer.OnCompleted();
			this.netInfo.UnblockUI();
			yield break;
		}

		// Token: 0x06008A5B RID: 35419 RVA: 0x00393CCC File Offset: 0x003920CC
		public IEnumerator GetNewestVersion(IObserver<bool> observer)
		{
			string errorMsg = "バージョン情報の取得に失敗しました。";
			string URL = CreateURL.LoadURL("ais_version_url.dat");
			WWWForm wwwform = new WWWForm();
			ObservableYieldInstruction<string> o = ObservableWWW.Get(URL, null, null).ToYieldInstruction(false);
			yield return o;
			if (o.HasError)
			{
				this.netInfo.DrawMessage(Color.red, errorMsg);
				observer.OnError(o.Error);
				yield break;
			}
			if (o.HasResult)
			{
				if (o.Result.StartsWith("ERROR_"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
				if (o.Result.StartsWith("SQLSTATE"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
			}
			if (!o.Result.IsNullOrEmpty())
			{
				this.netInfo.newestVersion = new Version(o.Result);
				this.netInfo.updateVersion = true;
				observer.OnNext(true);
				observer.OnCompleted();
				yield break;
			}
			this.netInfo.DrawMessage(Color.red, errorMsg);
			observer.OnError(new Exception(errorMsg));
			yield break;
		}

		// Token: 0x06008A5C RID: 35420 RVA: 0x00393CF0 File Offset: 0x003920F0
		public IEnumerator UpdateUserInfo(IObserver<bool> observer)
		{
			string URL = CreateURL.LoadURL("ais_system_url.dat");
			string errorMsg = "ユーザー情報の確認に失敗しました。";
			string uuid = Singleton<GameSystem>.Instance.UserUUID;
			string passwd = Singleton<GameSystem>.Instance.UserPasswd;
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("mode", 0);
			wwwform.AddField("uuid", uuid);
			wwwform.AddField("passwd", passwd);
			ObservableYieldInstruction<string> o = ObservableWWW.Post(URL, wwwform, null).ToYieldInstruction(false);
			yield return o;
			if (o.HasError)
			{
				this.netInfo.DrawMessage(Color.red, errorMsg);
				observer.OnError(o.Error);
				yield break;
			}
			if (o.HasResult)
			{
				if (o.Result.StartsWith("ERROR_"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
				if (o.Result.StartsWith("SQLSTATE"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
			}
			bool result = true;
			if (!o.Result.IsNullOrEmpty())
			{
				string[] array = o.Result.Split(new char[]
				{
					'\t'
				});
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.EncodeFromBase64(array[i]);
				}
				int num = 0;
				if (array.Length >= 1)
				{
					int userIdx;
					if (!int.TryParse(array[num++], out userIdx))
					{
						result = false;
					}
					else
					{
						this.profile.userIdx = userIdx;
					}
					observer.OnNext(result);
					observer.OnCompleted();
					this.netInfo.updateProfile = true;
					yield break;
				}
			}
			this.netInfo.DrawMessage(Color.red, errorMsg);
			observer.OnError(new Exception(errorMsg));
			yield break;
		}

		// Token: 0x06008A5D RID: 35421 RVA: 0x00393D14 File Offset: 0x00392114
		public IEnumerator UpdateHandleName(IObserver<string> observer)
		{
			string URL = CreateURL.LoadURL("ais_system_url.dat");
			string errorMsg = "ハンドル名の更新に失敗しました。";
			string uuid = Singleton<GameSystem>.Instance.UserUUID;
			string passwd = Singleton<GameSystem>.Instance.UserPasswd;
			string handle_name = Singleton<GameSystem>.Instance.HandleName;
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("mode", 1);
			wwwform.AddField("uuid", uuid);
			wwwform.AddField("passwd", passwd);
			wwwform.AddField("handle_name", handle_name);
			ObservableYieldInstruction<string> o = ObservableWWW.Post(URL, wwwform, null).ToYieldInstruction(false);
			yield return o;
			if (o.HasError)
			{
				this.netInfo.DrawMessage(Color.red, errorMsg);
				observer.OnError(o.Error);
				yield break;
			}
			if (o.HasResult)
			{
				if (o.Result.StartsWith("ERROR_"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
				if (o.Result.StartsWith("SQLSTATE"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
			}
			if (!o.Result.IsNullOrEmpty())
			{
				observer.OnNext(o.Result);
				observer.OnCompleted();
				this.netInfo.DrawMessage(NetworkDefine.colorWhite, "ハンドル名を更新しました。");
			}
			else
			{
				this.netInfo.DrawMessage(Color.red, errorMsg);
				observer.OnError(new Exception(errorMsg));
			}
			yield break;
		}

		// Token: 0x06008A5E RID: 35422 RVA: 0x00393D38 File Offset: 0x00392138
		public IEnumerator GetAllUsers(IObserver<bool> observer)
		{
			string URL = CreateURL.LoadURL("ais_system_url.dat");
			string errorMsg = "登録された全ハンドル名の取得に失敗しました。";
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("mode", 2);
			ObservableYieldInstruction<string> o = ObservableWWW.Post(URL, wwwform, null).ToYieldInstruction(false);
			yield return o;
			if (o.HasError)
			{
				this.netInfo.DrawMessage(Color.red, o.Error.ToString());
				observer.OnError(o.Error);
				yield break;
			}
			if (o.HasResult)
			{
				if (o.Result.StartsWith("ERROR_"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
				if (o.Result.StartsWith("SQLSTATE"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
			}
			if (!string.IsNullOrEmpty(o.Result))
			{
				this.dictUserInfo.Clear();
				string[] array = o.Result.Split(new char[]
				{
					'\n'
				});
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[]
					{
						'\t'
					});
					for (int j = 0; j < array3.Length; j++)
					{
						array3[j] = this.EncodeFromBase64(array3[j]);
					}
					int key;
					if (array3.Length >= 1 && int.TryParse(array3[0], out key))
					{
						NetworkInfo.UserInfo userInfo = new NetworkInfo.UserInfo();
						userInfo.handleName = array3[1];
						this.dictUserInfo[key] = userInfo;
					}
				}
				observer.OnNext(true);
				observer.OnCompleted();
				yield break;
			}
			this.netInfo.DrawMessage(Color.red, errorMsg);
			observer.OnError(new Exception(errorMsg));
			yield break;
		}

		// Token: 0x06008A5F RID: 35423 RVA: 0x00393D5C File Offset: 0x0039215C
		public IEnumerator GetAllCharaInfo(IObserver<bool> observer)
		{
			string URL = CreateURL.LoadURL("ais_uploadChara_url.dat");
			string errorMsg = "キャラ情報の取得に失敗しました。";
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("mode", 0);
			ObservableYieldInstruction<string> o = ObservableWWW.Post(URL, wwwform, null).ToYieldInstruction(false);
			yield return o;
			if (o.HasError)
			{
				this.netInfo.DrawMessage(Color.red, errorMsg);
				observer.OnError(o.Error);
				yield break;
			}
			if (o.HasResult)
			{
				if (o.Result.StartsWith("ERROR_"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
				if (o.Result.StartsWith("SQLSTATE"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
			}
			if (!string.IsNullOrEmpty(o.Result))
			{
				this.lstCharaInfo.Clear();
				string[] array = o.Result.Split(new char[]
				{
					'\n'
				});
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[]
					{
						'\t'
					});
					for (int j = 0; j < array3.Length; j++)
					{
						array3[j] = this.EncodeFromBase64(array3[j]);
					}
					if (array3.Length >= 0)
					{
						NetworkInfo.CharaInfo charaInfo = new NetworkInfo.CharaInfo();
						int num = 0;
						int num2;
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.idx = num2;
						}
						charaInfo.data_uid = array3[num++];
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.user_idx = num2;
						}
						charaInfo.name = array3[num++];
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.type = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.birthmonth = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.birthday = num2;
						}
						charaInfo.strBirthDay = ChaFileDefine.GetBirthdayStr(charaInfo.birthmonth, charaInfo.birthday, "ja-JP");
						charaInfo.comment = array3[num++];
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.sex = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.height = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.bust = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.hair = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.phase = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.lifestyle = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.pheromone = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.reliability = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.reason = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.instinct = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.dirty = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.wariness = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.sociability = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.darkness = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.skill_n01 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.skill_n02 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.skill_n03 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.skill_n04 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.skill_n05 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.skill_h01 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.skill_h02 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.skill_h03 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.skill_h04 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.skill_h05 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.wish_01 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.wish_02 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.wish_03 = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.registration = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.dlCount = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.weekCount = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.applause = num2;
						}
						string s = array3[num++];
						DateTime dateTime;
						if (DateTime.TryParse(s, out dateTime))
						{
							charaInfo.updateTime = dateTime;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							charaInfo.update_idx = num2;
						}
						s = array3[num++];
						if (DateTime.TryParse(s, out dateTime))
						{
							charaInfo.createTime = dateTime;
						}
						this.lstCharaInfo.Add(charaInfo);
					}
				}
				if (this.lstCharaInfo.Count != 0)
				{
					int count = this.lstCharaInfo.Count;
					this.netInfo.lstCharaInfo = (from item in this.lstCharaInfo
					orderby item.dlCount descending
					select item).ToList<NetworkInfo.CharaInfo>();
					for (int k = 0; k < count; k++)
					{
						this.lstCharaInfo[k].rankingT = k;
					}
					this.netInfo.lstCharaInfo = (from item in this.lstCharaInfo
					orderby item.weekCount descending
					select item).ToList<NetworkInfo.CharaInfo>();
					for (int l = 0; l < count; l++)
					{
						this.lstCharaInfo[l].rankingW = l;
					}
				}
			}
			observer.OnNext(true);
			observer.OnCompleted();
			yield break;
		}

		// Token: 0x06008A60 RID: 35424 RVA: 0x00393D80 File Offset: 0x00392180
		public IEnumerator GetAllSceneInfo(IObserver<bool> observer)
		{
			string URL = CreateURL.LoadURL("ais_uploadHousing_url.dat");
			string errorMsg = "ハウジング情報の取得に失敗しました。";
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("mode", 0);
			ObservableYieldInstruction<string> o = ObservableWWW.Post(URL, wwwform, null).ToYieldInstruction(false);
			yield return o;
			if (o.HasError)
			{
				this.netInfo.DrawMessage(Color.red, errorMsg);
				observer.OnError(o.Error);
				yield break;
			}
			if (o.HasResult)
			{
				if (o.Result.StartsWith("ERROR_"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
				if (o.Result.StartsWith("SQLSTATE"))
				{
					this.netInfo.DrawMessage(Color.red, errorMsg);
					observer.OnError(new Exception(o.Result));
					yield break;
				}
			}
			if (!string.IsNullOrEmpty(o.Result))
			{
				this.lstHousingInfo.Clear();
				string[] array = o.Result.Split(new char[]
				{
					'\n'
				});
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[]
					{
						'\t'
					});
					for (int j = 0; j < array3.Length; j++)
					{
						array3[j] = this.EncodeFromBase64(array3[j]);
					}
					if (array3.Length >= 0)
					{
						NetworkInfo.HousingInfo housingInfo = new NetworkInfo.HousingInfo();
						int num = 0;
						int num2;
						if (int.TryParse(array3[num++], out num2))
						{
							housingInfo.idx = num2;
						}
						housingInfo.data_uid = array3[num++];
						if (int.TryParse(array3[num++], out num2))
						{
							housingInfo.user_idx = num2;
						}
						housingInfo.name = array3[num++];
						if (int.TryParse(array3[num++], out num2))
						{
							housingInfo.mapSize = num2;
						}
						housingInfo.comment = array3[num++];
						if (int.TryParse(array3[num++], out num2))
						{
							housingInfo.dlCount = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							housingInfo.weekCount = num2;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							housingInfo.applause = num2;
						}
						string s = array3[num++];
						DateTime dateTime;
						if (DateTime.TryParse(s, out dateTime))
						{
							housingInfo.updateTime = dateTime;
						}
						if (int.TryParse(array3[num++], out num2))
						{
							housingInfo.update_idx = num2;
						}
						s = array3[num++];
						if (DateTime.TryParse(s, out dateTime))
						{
							housingInfo.createTime = dateTime;
						}
						this.lstHousingInfo.Add(housingInfo);
					}
				}
				if (this.lstHousingInfo.Count != 0)
				{
					int count = this.lstHousingInfo.Count;
					this.netInfo.lstHousingInfo = (from item in this.lstHousingInfo
					orderby item.dlCount descending
					select item).ToList<NetworkInfo.HousingInfo>();
					for (int k = 0; k < count; k++)
					{
						this.lstHousingInfo[k].rankingT = k;
					}
					this.netInfo.lstHousingInfo = (from item in this.lstHousingInfo
					orderby item.weekCount descending
					select item).ToList<NetworkInfo.HousingInfo>();
					for (int l = 0; l < count; l++)
					{
						this.lstHousingInfo[l].rankingW = l;
					}
				}
			}
			observer.OnNext(true);
			observer.OnCompleted();
			yield break;
		}

		// Token: 0x06008A61 RID: 35425 RVA: 0x00393DA2 File Offset: 0x003921A2
		private void Start()
		{
		}

		// Token: 0x06008A62 RID: 35426 RVA: 0x00393DA4 File Offset: 0x003921A4
		private void Update()
		{
		}

		// Token: 0x040070AC RID: 28844
		public Canvas cvsChangeScene;
	}
}
