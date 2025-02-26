using System;
using System.Collections;
using System.Collections.Generic;
using AIProject.SaveData;
using CharaCustom;
using GameLoadCharaFileSystem;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject.UI
{
	// Token: 0x02000E40 RID: 3648
	public class CharaEntryUI : MenuUIBehaviour
	{
		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x0600725C RID: 29276 RVA: 0x00309D7C File Offset: 0x0030817C
		public MenuUIBehaviour[] MenuUIList
		{
			get
			{
				MenuUIBehaviour[] result;
				if ((result = this._menuUIList) == null)
				{
					result = (this._menuUIList = new MenuUIBehaviour[]
					{
						this
					});
				}
				return result;
			}
		}

		// Token: 0x0600725D RID: 29277 RVA: 0x00309DAC File Offset: 0x003081AC
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool active)
			{
				this.SetActiveControl(active);
			});
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.Close();
			});
			this._keyCommands.Add(keyCodeDownCommand);
			this._lcw.onLoadItemFunc = delegate(GameCharaFileInfo dat)
			{
				Singleton<Game>.Instance.WorldData.AgentTable[Singleton<Map>.Instance.AccessDeviceID].CharaFileName = dat.FileName;
				Singleton<Map>.Instance.Player.PlayerController.ChangeState("CharaEnter");
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
				this.Close();
			};
			this._lcw.onClickRightFunc = null;
			this._lcw.onCloseWindowFunc = delegate()
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				this.Close();
			};
			this._lcw.onCharaCreateClickAction = delegate(int sex)
			{
				if (Singleton<Scene>.Instance.IsNowLoadingFade)
				{
					return;
				}
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
				CharaCustom.modeNew = true;
				CharaCustom.modeSex = 1;
				CharaCustom.actEixt = null;
				CharaCustom.nextScene = Singleton<Manager.Resources>.Instance.DefinePack.SceneNames.MapScene;
				Scene.Data data = new Scene.Data
				{
					levelName = "CharaCustom",
					isAdd = false,
					isFade = true
				};
				Singleton<Scene>.Instance.LoadReserve(data, false);
			};
		}

		// Token: 0x0600725E RID: 29278 RVA: 0x00309E69 File Offset: 0x00308269
		private void Close()
		{
			this.IsActiveControl = false;
		}

		// Token: 0x0600725F RID: 29279 RVA: 0x00309E74 File Offset: 0x00308274
		private void SetActiveControl(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				Time.timeScale = 0f;
				this._lcw.useDownload = true;
				List<string> list = ListPool<string>.Get();
				WorldData autoData = Singleton<Game>.Instance.Data.AutoData;
				if (autoData != null)
				{
					list.Add(autoData.PlayerData.CharaFileName);
					foreach (KeyValuePair<int, AgentData> keyValuePair in autoData.AgentTable)
					{
						list.Add(keyValuePair.Value.CharaFileName);
					}
				}
				foreach (KeyValuePair<int, WorldData> keyValuePair2 in Singleton<Game>.Instance.Data.WorldList)
				{
					list.Add(keyValuePair2.Value.PlayerData.CharaFileName);
					foreach (KeyValuePair<int, AgentData> keyValuePair3 in keyValuePair2.Value.AgentTable)
					{
						list.Add(keyValuePair3.Value.CharaFileName);
					}
				}
				this._lcw.ReCreateList(true, false);
				instance.FocusLevel = 0;
				instance.MenuElements = this.MenuUIList;
				coroutine = this.OpenCoroutine();
			}
			else
			{
				instance.ClearMenuElements();
				instance.FocusLevel = -1;
				coroutine = this.CloseCoroutine();
			}
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x06007260 RID: 29280 RVA: 0x0030A0B0 File Offset: 0x003084B0
		private IEnumerator OpenCoroutine()
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x06007261 RID: 29281 RVA: 0x0030A0CC File Offset: 0x003084CC
		private IEnumerator CloseCoroutine()
		{
			base.EnabledInput = false;
			this._canvasGroup.blocksRaycasts = false;
			Time.timeScale = 1f;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x04005D76 RID: 23926
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005D77 RID: 23927
		[SerializeField]
		private GameLoadCharaWindow _lcw;

		// Token: 0x04005D78 RID: 23928
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04005D79 RID: 23929
		private IDisposable _fadeDisposable;
	}
}
