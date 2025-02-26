using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AIChara;
using AIProject.SaveData;
using CharaCustom;
using GameLoadCharaFileSystem;
using Illusion.Extensions;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E45 RID: 3653
	public class PlayerLookEditUI : MenuUIBehaviour
	{
		// Token: 0x060072AF RID: 29359 RVA: 0x0030E22B File Offset: 0x0030C62B
		public IObservable<int> OnSelectIDChangedAsObservable()
		{
			if (this._selectIDChange == null)
			{
				this._selectIDChange = this._selectedID.TakeUntilDestroy(base.gameObject).Publish<int>();
				this._selectIDChange.Connect();
			}
			return this._selectIDChange;
		}

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x060072B0 RID: 29360 RVA: 0x0030E268 File Offset: 0x0030C668
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

		// Token: 0x060072B1 RID: 29361 RVA: 0x0030E298 File Offset: 0x0030C698
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool active)
			{
				this.SetActiveControl(active);
			});
			this._charaButton.onClick.AddListener(delegate()
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				this._selectedID.Value = 0;
			});
			this.OnSelectIDChangedAsObservable().Subscribe(delegate(int x)
			{
				this._selectedImageTransform.gameObject.SetActiveIfDifferent(x != 1);
				if (this._selectedImageTransform.gameObject.activeSelf)
				{
					this._selectedImageTransform.localPosition = this._element.localPosition;
				}
				this._objPlayerParameterWindow.SetActiveIfDifferent(true);
				if (this._info == null)
				{
					return;
				}
				this._txtPlayerCharaName.text = this._info.name;
				this._riPlayerCard.texture = PngAssist.ChangeTextureFromByte(PngFile.LoadPngBytes(this._info.FullPath), 0, 0, TextureFormat.ARGB32, false);
				int languageInt = Singleton<GameSystem>.Instance.languageInt;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append((this._info.sex != 0) ? this._localizeFemale[languageInt] : this._localizeMale[languageInt]);
				if (this._info.sex == 1 && this._info.futanari)
				{
					stringBuilder.Append(this._localizeFutanari[languageInt]);
				}
				this._txtPlayerSex.text = stringBuilder.ToString();
			});
			this._objPlayerParameterWindow.SetActiveIfDifferent(false);
			this._txtPlayerCharaName.text = "NoName";
			this._riPlayerCard.texture = null;
			this._txtPlayerSex.text = string.Empty;
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.Close();
			});
			this._keyCommands.Add(keyCodeDownCommand);
			this._closeButton.onClick.AddListener(delegate()
			{
				this.Close();
			});
			this._charaCreateButton.onClick.AddListener(delegate()
			{
				if (Singleton<Scene>.Instance.IsNowLoadingFade)
				{
					return;
				}
				CharaCustom.modeNew = false;
				CharaCustom.modeSex = Singleton<Map>.Instance.Player.PlayerData.Sex;
				CharaCustom.editCharaFileName = Singleton<Map>.Instance.Player.PlayerData.CharaFileName;
				CharaCustom.actEixt = null;
				CharaCustom.nextScene = Singleton<Manager.Resources>.Instance.DefinePack.SceneNames.MapScene;
				Scene.Data data = new Scene.Data
				{
					levelName = "CharaCustom",
					isAdd = false,
					isFade = true
				};
				Singleton<Scene>.Instance.LoadReserve(data, false);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
			});
			this._selectedImageTransform.gameObject.SetActive(false);
		}

		// Token: 0x060072B2 RID: 29362 RVA: 0x0030E3BA File Offset: 0x0030C7BA
		private void Close()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			this.IsActiveControl = false;
		}

		// Token: 0x060072B3 RID: 29363 RVA: 0x0030E3D4 File Offset: 0x0030C7D4
		private void SetActiveControl(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				this._selectedID.Value = -1;
				Time.timeScale = 0f;
				ChaFileControl chaFileControl = new ChaFileControl();
				PlayerData playerData = Singleton<Game>.Instance.WorldData.PlayerData;
				if (!playerData.CharaFileName.IsNullOrEmpty() && chaFileControl.LoadCharaFile(playerData.CharaFileName, playerData.Sex, false, true))
				{
					string personality = string.Empty;
					VoiceInfo.Param param;
					if (!Singleton<Voice>.Instance.voiceInfoDic.TryGetValue(chaFileControl.parameter.personality, out param))
					{
						personality = "不明";
					}
					else
					{
						personality = param.Personality;
					}
					string fullPath = (playerData.Sex != 0) ? string.Format("{0}chara/female/{1}.png", UserData.Path, playerData.CharaFileName) : string.Format("{0}chara/male/{1}.png", UserData.Path, playerData.CharaFileName);
					this._info = new GameCharaFileInfo
					{
						name = chaFileControl.parameter.fullname,
						personality = personality,
						voice = chaFileControl.parameter.personality,
						hair = chaFileControl.custom.hair.kind,
						birthMonth = (int)chaFileControl.parameter.birthMonth,
						birthDay = (int)chaFileControl.parameter.birthDay,
						strBirthDay = chaFileControl.parameter.strBirthDay,
						sex = (int)chaFileControl.parameter.sex,
						FullPath = fullPath,
						FileName = playerData.CharaFileName,
						gameRegistration = chaFileControl.gameinfo.gameRegistration,
						flavorState = new Dictionary<int, int>(chaFileControl.gameinfo.flavorState),
						phase = chaFileControl.gameinfo.phase,
						normalSkill = new Dictionary<int, int>(chaFileControl.gameinfo.normalSkill),
						hSkill = new Dictionary<int, int>(chaFileControl.gameinfo.hSkill),
						favoritePlace = chaFileControl.gameinfo.favoritePlace,
						futanari = chaFileControl.parameter.futanari,
						data_uuid = chaFileControl.dataID
					};
				}
				else
				{
					this._info = null;
				}
				if (this._info != null)
				{
					this._charaText.text = this._info.name;
				}
				else
				{
					this._charaText.text = "-----";
				}
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

		// Token: 0x060072B4 RID: 29364 RVA: 0x0030E6FC File Offset: 0x0030CAFC
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

		// Token: 0x060072B5 RID: 29365 RVA: 0x0030E718 File Offset: 0x0030CB18
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

		// Token: 0x04005DE7 RID: 24039
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005DE8 RID: 24040
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04005DE9 RID: 24041
		[SerializeField]
		private Button _charaButton;

		// Token: 0x04005DEA RID: 24042
		[SerializeField]
		private RectTransform _element;

		// Token: 0x04005DEB RID: 24043
		[SerializeField]
		private Text _charaText;

		// Token: 0x04005DEC RID: 24044
		[SerializeField]
		private RectTransform _selectedImageTransform;

		// Token: 0x04005DED RID: 24045
		[SerializeField]
		private Button _charaCreateButton;

		// Token: 0x04005DEE RID: 24046
		[SerializeField]
		private Texture2D _texEmpty;

		// Token: 0x04005DEF RID: 24047
		[SerializeField]
		private GameObject _objPlayerParameterWindow;

		// Token: 0x04005DF0 RID: 24048
		[SerializeField]
		private Text _txtPlayerCharaName;

		// Token: 0x04005DF1 RID: 24049
		[SerializeField]
		private RawImage _riPlayerCard;

		// Token: 0x04005DF2 RID: 24050
		[SerializeField]
		private Text _txtPlayerSex;

		// Token: 0x04005DF3 RID: 24051
		private readonly string[] _localizeMale = new string[]
		{
			"男",
			"Male",
			"Male",
			"Male",
			string.Empty
		};

		// Token: 0x04005DF4 RID: 24052
		private readonly string[] _localizeFemale = new string[]
		{
			"女",
			"Female",
			"Female",
			"Female",
			string.Empty
		};

		// Token: 0x04005DF5 RID: 24053
		private readonly string[] _localizeFutanari = new string[]
		{
			"（フタナリ）",
			"(Futanari)",
			"(Futanari)",
			"(Futanari)",
			string.Empty
		};

		// Token: 0x04005DF6 RID: 24054
		private IntReactiveProperty _selectedID = new IntReactiveProperty(-1);

		// Token: 0x04005DF7 RID: 24055
		private IConnectableObservable<int> _selectIDChange;

		// Token: 0x04005DF8 RID: 24056
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04005DF9 RID: 24057
		private GameCharaFileInfo _info;

		// Token: 0x04005DFA RID: 24058
		private IDisposable _fadeDisposable;
	}
}
