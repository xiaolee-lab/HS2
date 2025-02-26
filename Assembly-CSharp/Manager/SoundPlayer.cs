using System;
using System.Collections.Generic;
using AIProject;
using AIProject.Animal;
using AIProject.SaveData;
using Illusion.Game;
using ReMotion;
using Sound;
using uAudio.uAudio_backend;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEx;

namespace Manager
{
	// Token: 0x02000920 RID: 2336
	public class SoundPlayer : Singleton<SoundPlayer>
	{
		// Token: 0x17000C73 RID: 3187
		// (set) Token: 0x06004202 RID: 16898 RVA: 0x0019E714 File Offset: 0x0019CB14
		public bool PlayActiveAll
		{
			set
			{
				this.EnvPlayActive = value;
				this.BGMPlayActive = value;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06004203 RID: 16899 RVA: 0x0019E731 File Offset: 0x0019CB31
		// (set) Token: 0x06004204 RID: 16900 RVA: 0x0019E739 File Offset: 0x0019CB39
		public bool BGMPlayActive { get; set; }

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x0019E742 File Offset: 0x0019CB42
		// (set) Token: 0x06004206 RID: 16902 RVA: 0x0019E74A File Offset: 0x0019CB4A
		public bool EnvPlayActive { get; set; }

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06004207 RID: 16903 RVA: 0x0019E753 File Offset: 0x0019CB53
		// (set) Token: 0x06004208 RID: 16904 RVA: 0x0019E75B File Offset: 0x0019CB5B
		public AssetBundleInfo CurrentBGMAudioABInfo { get; private set; } = default(AssetBundleInfo);

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06004209 RID: 16905 RVA: 0x0019E764 File Offset: 0x0019CB64
		// (set) Token: 0x0600420A RID: 16906 RVA: 0x0019E76C File Offset: 0x0019CB6C
		public AssetBundleInfo PrevBGMAudioABInfo { get; private set; } = default(AssetBundleInfo);

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x0019E775 File Offset: 0x0019CB75
		// (set) Token: 0x0600420C RID: 16908 RVA: 0x0019E77D File Offset: 0x0019CB7D
		public AudioSource LastBGMAudio { get; private set; }

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600420D RID: 16909 RVA: 0x0019E786 File Offset: 0x0019CB86
		// (set) Token: 0x0600420E RID: 16910 RVA: 0x0019E78E File Offset: 0x0019CB8E
		private SoundPlayer.UpdateType MapBGMUpdateFlag { get; set; }

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x0019E797 File Offset: 0x0019CB97
		// (set) Token: 0x06004210 RID: 16912 RVA: 0x0019E79F File Offset: 0x0019CB9F
		private bool BGMChangePossible { get; set; } = true;

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x0019E7A8 File Offset: 0x0019CBA8
		public uAudio uAudio
		{
			get
			{
				if (this._uAudio == null)
				{
					this._uAudio = new uAudio();
					this._uAudio.Volume = 1f;
					this._uAudio.CurrentTime = TimeSpan.Zero;
				}
				return this._uAudio;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x0019E7E6 File Offset: 0x0019CBE6
		// (set) Token: 0x06004213 RID: 16915 RVA: 0x0019E7EE File Offset: 0x0019CBEE
		private SoundPlayer.UpdateType WideEnvUpdateFlag { get; set; }

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x0019E7F7 File Offset: 0x0019CBF7
		// (set) Token: 0x06004215 RID: 16917 RVA: 0x0019E7FF File Offset: 0x0019CBFF
		private SoundPlayer.UpdateType PrevWideEnvUpdateFlag { get; set; }

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004216 RID: 16918 RVA: 0x0019E808 File Offset: 0x0019CC08
		// (set) Token: 0x06004217 RID: 16919 RVA: 0x0019E810 File Offset: 0x0019CC10
		private bool WideEnvChangePossible { get; set; } = true;

		// Token: 0x06004218 RID: 16920 RVA: 0x0019E819 File Offset: 0x0019CC19
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x0019E828 File Offset: 0x0019CC28
		private void Start()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			where this.BGMPlayActive
			where this.BGMChangePossible
			where this.MapBGMUpdateFlag != (SoundPlayer.UpdateType)0
			select _).Subscribe(delegate(long _)
			{
				this.PlayMapBGM();
			});
			IObservable<long> source = from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			where this.EnvPlayActive
			select _;
			(from _ in source
			where !this.WideEnvChangePossible
			where this.WideEnvUpdateFlag.Contains(SoundPlayer.UpdateType.Area)
			where !this.PrevWideEnvUpdateFlag.Contains(SoundPlayer.UpdateType.Area)
			select _).Subscribe(delegate(long _)
			{
				this.PlayWideEnvSE(true);
				this.PrevWideEnvUpdateFlag = this.WideEnvUpdateFlag;
			});
			(from _ in source
			where this.WideEnvChangePossible
			where this.WideEnvUpdateFlag != (SoundPlayer.UpdateType)0
			select _).Subscribe(delegate(long _)
			{
				this.PlayWideEnvSE(this.WideEnvUpdateFlag.Contains(SoundPlayer.UpdateType.Area));
				this.PrevWideEnvUpdateFlag = this.WideEnvUpdateFlag;
			});
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x0019E949 File Offset: 0x0019CD49
		private void ChangedArea(PlayerActor _player)
		{
			this.MapBGMUpdateFlag |= SoundPlayer.UpdateType.Area;
			this.WideEnvUpdateFlag |= SoundPlayer.UpdateType.Area;
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x0019E967 File Offset: 0x0019CD67
		private void ChangedTime(PlayerActor _player)
		{
			this.MapBGMUpdateFlag |= SoundPlayer.UpdateType.Time;
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x0019E977 File Offset: 0x0019CD77
		private void ChangedWeather(PlayerActor _player)
		{
			this.WideEnvUpdateFlag |= SoundPlayer.UpdateType.Weather;
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x0019E987 File Offset: 0x0019CD87
		public void StartAllSubscribe()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			this.StartTimeSubscribe();
			this.StartAreaSubScribe();
			this.StartWeatherSubscribe();
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x0019E9A6 File Offset: 0x0019CDA6
		public void StopAllSubscribe()
		{
			this.DisposeDelayPlayBGM();
			this.DisposeDelayPlayWideEnv();
			this.StopTimeSubscribe();
			this.StopAreaSubscribe();
			this.StopWeatherSubscribe();
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x0019E9C8 File Offset: 0x0019CDC8
		public void StartTimeSubscribe()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			Map _map = Singleton<Map>.Instance;
			EnvironmentSimulator simulator = _map.Simulator;
			if (this.timeSubscribeDisposable != null)
			{
				this.timeSubscribeDisposable.Dispose();
			}
			this.timeSubscribeDisposable = simulator.OnMinuteAsObservable().TakeUntilDestroy(_map).Subscribe(delegate(TimeSpan _)
			{
				this.ChangedTime(_map.Player);
			});
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x0019EA44 File Offset: 0x0019CE44
		public void StopTimeSubscribe()
		{
			if (this.timeSubscribeDisposable != null)
			{
				this.timeSubscribeDisposable.Dispose();
				this.timeSubscribeDisposable = null;
			}
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x0019EA64 File Offset: 0x0019CE64
		public void StartAreaSubScribe()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			Map instance = Singleton<Map>.Instance;
			PlayerActor _player = instance.Player;
			if (this.areaSubscribeDisposable != null)
			{
				this.areaSubscribeDisposable.Dispose();
			}
			this.areaSubscribeDisposable = _player.OnMapAreaChangedAsObservable().TakeUntilDestroy(instance).Subscribe(delegate(int _)
			{
				this.ChangedArea(_player);
			});
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x0019EADB File Offset: 0x0019CEDB
		public void StopAreaSubscribe()
		{
			if (this.areaSubscribeDisposable != null)
			{
				this.areaSubscribeDisposable.Dispose();
				this.areaSubscribeDisposable = null;
			}
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x0019EAFC File Offset: 0x0019CEFC
		public void StartWeatherSubscribe()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			Map _map = Singleton<Map>.Instance;
			EnvironmentSimulator simulator = _map.Simulator;
			if (this.weatherSubscribeDisposable != null)
			{
				this.weatherSubscribeDisposable.Dispose();
			}
			this.weatherSubscribeDisposable = simulator.OnWeatherChangedAsObservable().TakeUntilDestroy(_map).Subscribe(delegate(Weather _)
			{
				this.ChangedWeather(_map.Player);
			});
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x0019EB78 File Offset: 0x0019CF78
		public void StopWeatherSubscribe()
		{
			if (this.weatherSubscribeDisposable != null)
			{
				this.weatherSubscribeDisposable.Dispose();
				this.weatherSubscribeDisposable = null;
			}
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x0019EB98 File Offset: 0x0019CF98
		public void PlayMapBGM()
		{
			this.MapBGMUpdateFlag = (SoundPlayer.UpdateType)0;
			if (!Singleton<Resources>.IsInstance() || !Singleton<Map>.IsInstance() || !Singleton<Sound>.IsInstance())
			{
				return;
			}
			int mapID = Singleton<Map>.Instance.MapID;
			PlayerActor player = Singleton<Map>.Instance.Player;
			MapArea mapArea = (!(player != null)) ? null : player.MapArea;
			if (mapArea == null)
			{
				return;
			}
			int num = mapArea.AreaID;
			if (mapID == 1)
			{
				num = 1;
			}
			string text = null;
			Dictionary<int, string> dictionary = null;
			if (Singleton<Game>.IsInstance())
			{
				if (mapID == 0)
				{
					AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
					dictionary = ((environment != null) ? environment.JukeBoxAudioNameTable : null);
				}
				else
				{
					AIProject.SaveData.Environment environment2 = Singleton<Game>.Instance.Environment;
					Dictionary<int, Dictionary<int, string>> dictionary2 = (environment2 != null) ? environment2.AnotherJukeBoxAudioNameTable : null;
					if (dictionary2 != null && (!dictionary2.TryGetValue(mapID, out dictionary) || dictionary == null))
					{
						Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
						dictionary2[mapID] = dictionary3;
						dictionary = dictionary3;
					}
				}
			}
			bool? flag = (dictionary != null) ? new bool?(dictionary.TryGetValue(num, out text)) : null;
			if (flag != null && flag.Value && !text.IsNullOrEmpty())
			{
				AudioClip audioClip = null;
				Dictionary<int, AudioClip> dictionary4 = null;
				if (!this.jukeBoxAudioClipCacheTable.TryGetValue(mapID, out dictionary4) || dictionary4 == null)
				{
					Dictionary<int, AudioClip> dictionary5 = new Dictionary<int, AudioClip>();
					this.jukeBoxAudioClipCacheTable[mapID] = dictionary5;
					dictionary4 = dictionary5;
				}
				if (dictionary4.TryGetValue(num, out audioClip) && audioClip != null)
				{
					Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>> dictionary6 = null;
					if (!this.housingAreaAudioTable.TryGetValue(mapID, out dictionary6) || dictionary6 == null)
					{
						Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>> dictionary7 = new Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>();
						this.housingAreaAudioTable[mapID] = dictionary7;
						dictionary6 = dictionary7;
					}
					UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable> value;
					if (dictionary6.TryGetValue(num, out value) && value.Item1 != null && value.Item1.isPlaying && value.Item1.clip == audioClip)
					{
						bool flag2 = false;
						if (value.Item3 != null)
						{
							value.Item3.Dispose();
							value.Item3 = null;
							flag2 = true;
						}
						if (value.Item2 == null)
						{
							float mapBGMFadeTime = Singleton<Resources>.Instance.SoundPack.BGMInfo.MapBGMFadeTime;
							AudioSource _audio = value.Item1;
							float _startVolume = _audio.volume;
							FadePlayer _fadePlayer = _audio.GetComponent<FadePlayer>();
							value.Item2 = ObservableEasing.Linear(mapBGMFadeTime, false).FrameTimeInterval(false).TakeUntilDestroy(_audio).Subscribe(delegate(TimeInterval<float> x)
							{
								float num2 = Mathf.Lerp(_startVolume, 1f, x.Value);
								if (_fadePlayer != null)
								{
									_audio.volume = (_fadePlayer.currentVolume = num2);
								}
								else
								{
									_audio.volume = num2;
								}
							});
							this.StopMapAreaBGM(mapBGMFadeTime);
							this.MuteHousingAreaBGM(mapID, num, mapBGMFadeTime, false);
							flag2 = true;
						}
						this.LastBGMAudio = value.Item1;
						if (flag2)
						{
							dictionary6[num] = value;
						}
						return;
					}
				}
				if (audioClip == null)
				{
					bool flag3 = false;
					audioClip = SoundPlayer.LoadAudioClip(SoundPlayer.Directory.AudioFile + text, ref flag3, this.uAudio);
					if (audioClip != null)
					{
						dictionary4[num] = audioClip;
					}
				}
				if (audioClip != null)
				{
					this.PlayJukeAreaBGM(audioClip, mapID, num);
					return;
				}
			}
			this.PlayDefaultBGM(mapID, num);
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x0019EF0C File Offset: 0x0019D30C
		private void PlayDefaultBGM(int _mapID, int _areaID)
		{
			Resources.SoundTable sound = Singleton<Resources>.Instance.Sound;
			Dictionary<int, SoundPlayer.MapBGMInfo> dictionary;
			SoundPlayer.MapBGMInfo bgmInfo;
			if (!sound.MapBGMInfoTable.TryGetValue(_mapID, out dictionary) || !dictionary.TryGetValue(_areaID, out bgmInfo))
			{
				return;
			}
			EnvironmentSimulator simulator = Singleton<Map>.Instance.Simulator;
			int id = bgmInfo.GetID(simulator.Now);
			AssetBundleInfo assetInfo;
			if (!sound.MapBGMABTable.TryGetValue(id, out assetInfo))
			{
				return;
			}
			Transform transform = Singleton<Sound>.Instance.FindAsset(Sound.Type.BGM, assetInfo.asset, assetInfo.assetbundle);
			if (transform != null)
			{
				float mapBGMFadeTime = Singleton<Resources>.Instance.SoundPack.BGMInfo.MapBGMFadeTime;
				AudioSource component = transform.GetComponent<AudioSource>();
				if (component != null && !component.isPlaying)
				{
					Singleton<Sound>.Instance.PlayBGM(mapBGMFadeTime);
				}
				if (component != null)
				{
					this.LastBGMAudio = component;
				}
				this.MuteHousingAreaBGM(mapBGMFadeTime, false);
				return;
			}
			this.PlayMapBGM(bgmInfo, assetInfo);
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x0019F00C File Offset: 0x0019D40C
		private void PlayMapBGM(SoundPlayer.MapBGMInfo _bgmInfo, AssetBundleInfo _assetInfo)
		{
			Illusion.Game.Utils.Sound.SettingBGM setting = _bgmInfo.Setting;
			setting.assetBundleName = _assetInfo.assetbundle;
			setting.assetName = _assetInfo.asset;
			float num = setting.fadeTime = Singleton<Resources>.Instance.SoundPack.BGMInfo.MapBGMFadeTime;
			Transform transform = Illusion.Game.Utils.Sound.Play(setting);
			AudioSource audioSource = (!(transform != null)) ? null : transform.GetComponent<AudioSource>();
			if (audioSource != null)
			{
				this.MuteHousingAreaBGM(num, false);
				this.LastBGMAudio = audioSource;
				this.PrevBGMAudioABInfo = this.CurrentBGMAudioABInfo;
				this.CurrentBGMAudioABInfo = _assetInfo;
				if (this.delayPlayBGMDisposable != null)
				{
					this.delayPlayBGMDisposable.Dispose();
				}
				this.BGMChangePossible = false;
				this.delayPlayBGMDisposable = Observable.Timer(TimeSpan.FromSeconds((double)num)).TakeUntilDestroy(base.gameObject).Subscribe(delegate(long _)
				{
					this.delayPlayBGMDisposable = null;
					this.BGMChangePossible = true;
				}, delegate(Exception ex)
				{
					this.delayPlayBGMDisposable = null;
					this.BGMChangePossible = true;
				}, delegate()
				{
					this.delayPlayBGMDisposable = null;
					this.BGMChangePossible = true;
				});
			}
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x0019F110 File Offset: 0x0019D510
		private void PlayJukeAreaBGM(AudioClip _audioClip, int _mapID, int _areaID)
		{
			if (_audioClip == null)
			{
				return;
			}
			LoadSound bgm = Illusion.Game.Utils.Sound.GetBGM();
			if (bgm != null && bgm.audioSource != null && bgm.audioSource.isPlaying && bgm.audioSource.clip == _audioClip)
			{
				return;
			}
			float mapBGMFadeTime = Singleton<Resources>.Instance.SoundPack.BGMInfo.MapBGMFadeTime;
			AudioSource _audioSource = Illusion.Game.Utils.Sound.Play(Sound.Type.BGM, _audioClip, mapBGMFadeTime);
			if (_audioSource == null)
			{
				return;
			}
			_audioSource.loop = true;
			Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>> dictionary = null;
			if (!this.housingAreaAudioTable.TryGetValue(_mapID, out dictionary) || dictionary == null)
			{
				Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>> dictionary2 = new Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>();
				this.housingAreaAudioTable[_mapID] = dictionary2;
				dictionary = dictionary2;
			}
			dictionary[_areaID] = new UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>(_audioSource, Observable.Timer(TimeSpan.FromSeconds((double)mapBGMFadeTime)).Subscribe<long>(), null);
			this.StopMapAreaBGM(mapBGMFadeTime);
			this.MuteHousingAreaBGM(_mapID, _areaID, mapBGMFadeTime, false);
			_audioSource.UpdateAsObservable().Subscribe(delegate(Unit _)
			{
				if (_audioSource.isPlaying && mp3AudioClip.flare_SongEnd)
				{
					if (_audioSource.loop)
					{
						mp3AudioClip.SongDone = (mp3AudioClip.flare_SongEnd = false);
						this.uAudio.CurrentTime = TimeSpan.Zero;
					}
					else
					{
						_audioSource.Stop();
					}
				}
			});
			this.LastBGMAudio = _audioSource;
			this.PrevBGMAudioABInfo = this.CurrentBGMAudioABInfo;
			this.CurrentBGMAudioABInfo = default(AssetBundleInfo);
			if (this.delayPlayBGMDisposable != null)
			{
				this.delayPlayBGMDisposable.Dispose();
			}
			this.BGMChangePossible = false;
			this.delayPlayBGMDisposable = Observable.Timer(TimeSpan.FromSeconds((double)mapBGMFadeTime)).TakeUntilDestroy(base.gameObject).Subscribe(delegate(long _)
			{
				this.delayPlayBGMDisposable = null;
				this.BGMChangePossible = true;
			}, delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
				this.delayPlayBGMDisposable = null;
				this.BGMChangePossible = true;
			}, delegate()
			{
				this.delayPlayBGMDisposable = null;
				this.BGMChangePossible = true;
			});
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x0019F2D4 File Offset: 0x0019D6D4
		public void StopMapAreaBGM(float _fadeTime = 0f)
		{
			if (!Singleton<Sound>.IsInstance())
			{
				return;
			}
			GameObject currentBGM = Singleton<Sound>.Instance.currentBGM;
			if (currentBGM != null)
			{
				FadePlayer component = currentBGM.GetComponent<FadePlayer>();
				if (component != null)
				{
					component.Stop(_fadeTime);
				}
				else
				{
					UnityEngine.Object.Destroy(currentBGM);
				}
				Singleton<Sound>.Instance.currentBGM = null;
			}
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x0019F334 File Offset: 0x0019D734
		private void ResizeHousingAreaAudioTable()
		{
			if (this.housingAreaAudioTable.IsNullOrEmpty<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>>())
			{
				return;
			}
			List<UnityEx.ValueTuple<int, int>> list = ListPool<UnityEx.ValueTuple<int, int>>.Get();
			foreach (KeyValuePair<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>> keyValuePair in this.housingAreaAudioTable)
			{
				Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>> value = keyValuePair.Value;
				if (!value.IsNullOrEmpty<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>())
				{
					foreach (KeyValuePair<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>> keyValuePair2 in value)
					{
						if (keyValuePair2.Value.Item1 == null)
						{
							list.Add(new UnityEx.ValueTuple<int, int>(keyValuePair.Key, keyValuePair2.Key));
						}
					}
				}
			}
			foreach (UnityEx.ValueTuple<int, int> valueTuple in list)
			{
				this.housingAreaAudioTable[valueTuple.Item1].Remove(valueTuple.Item2);
			}
			ListPool<UnityEx.ValueTuple<int, int>>.Release(list);
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x0019F494 File Offset: 0x0019D894
		public void ForcedMuteHousingAreaBGM(float _fadeTime = 0f, bool _ignoreTimeScale = false)
		{
			this.ResizeHousingAreaAudioTable();
			if (this.housingAreaAudioTable.IsNullOrEmpty<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>>())
			{
				return;
			}
			List<UnityEx.ValueTuple<int, List<int>>> list = ListPool<UnityEx.ValueTuple<int, List<int>>>.Get();
			foreach (KeyValuePair<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>> keyValuePair in this.housingAreaAudioTable)
			{
				List<int> list2 = ListPool<int>.Get();
				list2.AddRange(keyValuePair.Value.Keys);
				list.Add(new UnityEx.ValueTuple<int, List<int>>(keyValuePair.Key, list2));
			}
			foreach (UnityEx.ValueTuple<int, List<int>> valueTuple in list)
			{
				foreach (int key in valueTuple.Item2)
				{
					UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable> value = this.housingAreaAudioTable[valueTuple.Item1][key];
					if (value.Item2 != null)
					{
						value.Item2.Dispose();
						value.Item2 = null;
					}
					AudioSource _audio = value.Item1;
					float _startVolume = _audio.volume;
					FadePlayer _fadePlayer = _audio.GetComponent<FadePlayer>();
					IDisposable item = value.Item3;
					if (item != null)
					{
						item.Dispose();
					}
					value.Item3 = ObservableEasing.Linear(_fadeTime, _ignoreTimeScale).FrameTimeInterval(_ignoreTimeScale).TakeUntilDestroy(_audio).Subscribe(delegate(TimeInterval<float> x)
					{
						if (_fadePlayer != null)
						{
							_audio.volume = (_fadePlayer.currentVolume = Mathf.Lerp(_startVolume, 0f, x.Value));
						}
						else
						{
							_audio.volume = Mathf.Lerp(_startVolume, 0f, x.Value);
						}
					}, delegate(Exception ex)
					{
						if (_audio == null)
						{
							return;
						}
						if (_fadePlayer != null)
						{
							_audio.volume = (_fadePlayer.currentVolume = 0f);
						}
						else
						{
							_audio.volume = 0f;
						}
					}, delegate()
					{
						if (_audio == null)
						{
							return;
						}
						if (_fadePlayer != null)
						{
							_audio.volume = (_fadePlayer.currentVolume = 0f);
						}
						else
						{
							_audio.volume = 0f;
						}
					});
					this.housingAreaAudioTable[valueTuple.Item1][key] = value;
				}
			}
			foreach (UnityEx.ValueTuple<int, List<int>> valueTuple2 in list)
			{
				ListPool<int>.Release(valueTuple2.Item2);
			}
			ListPool<UnityEx.ValueTuple<int, List<int>>>.Release(list);
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x0019F730 File Offset: 0x0019DB30
		public void MuteHousingAreaBGM(float _fadeTime = 0f, bool _ignoreTimeScale = false)
		{
			this.ResizeHousingAreaAudioTable();
			if (this.housingAreaAudioTable.IsNullOrEmpty<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>>())
			{
				return;
			}
			List<UnityEx.ValueTuple<int, List<int>>> list = ListPool<UnityEx.ValueTuple<int, List<int>>>.Get();
			foreach (KeyValuePair<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>> keyValuePair in this.housingAreaAudioTable)
			{
				List<int> list2 = ListPool<int>.Get();
				list2.AddRange(keyValuePair.Value.Keys);
				list.Add(new UnityEx.ValueTuple<int, List<int>>(keyValuePair.Key, list2));
			}
			foreach (UnityEx.ValueTuple<int, List<int>> valueTuple in list)
			{
				foreach (int key in valueTuple.Item2)
				{
					UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable> value = this.housingAreaAudioTable[valueTuple.Item1][key];
					bool flag = false;
					if (value.Item2 != null)
					{
						value.Item2.Dispose();
						value.Item2 = null;
						flag = true;
					}
					if (value.Item3 == null)
					{
						AudioSource audio = value.Item1;
						FadePlayer fadePlayer = audio.GetComponent<FadePlayer>();
						float startVolume = audio.volume;
						value.Item3 = ObservableEasing.Linear(_fadeTime, _ignoreTimeScale).FrameTimeInterval(_ignoreTimeScale).TakeUntilDestroy(audio).Subscribe(delegate(TimeInterval<float> x)
						{
							float num = Mathf.Lerp(startVolume, 0f, x.Value);
							if (fadePlayer != null)
							{
								audio.volume = (fadePlayer.currentVolume = num);
							}
							else
							{
								audio.volume = num;
							}
						}, delegate(Exception ex)
						{
							if (audio == null)
							{
								return;
							}
							if (fadePlayer != null)
							{
								audio.volume = (fadePlayer.currentVolume = 0f);
							}
							else
							{
								audio.volume = 0f;
							}
						}, delegate()
						{
							if (audio == null)
							{
								return;
							}
							if (fadePlayer != null)
							{
								audio.volume = (fadePlayer.currentVolume = 0f);
							}
							else
							{
								audio.volume = 0f;
							}
						});
						flag = true;
					}
					if (flag)
					{
						this.housingAreaAudioTable[valueTuple.Item1][key] = value;
					}
				}
			}
			foreach (UnityEx.ValueTuple<int, List<int>> valueTuple2 in list)
			{
				ListPool<int>.Release(valueTuple2.Item2);
			}
			ListPool<UnityEx.ValueTuple<int, List<int>>>.Release(list);
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x0019F9D4 File Offset: 0x0019DDD4
		public void MuteHousingAreaBGM(int _mapID, int _areaID, float _fadeTime = 0f, bool _ignoreTimeScale = false)
		{
			this.ResizeHousingAreaAudioTable();
			if (this.housingAreaAudioTable.IsNullOrEmpty<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>>())
			{
				return;
			}
			Dictionary<int, List<int>> dictionary = DictionaryPool<int, List<int>>.Get();
			foreach (KeyValuePair<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>> keyValuePair in this.housingAreaAudioTable)
			{
				Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>> value = keyValuePair.Value;
				if (!value.IsNullOrEmpty<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>())
				{
					int key = keyValuePair.Key;
					foreach (KeyValuePair<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>> keyValuePair2 in value)
					{
						UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable> value2 = keyValuePair2.Value;
						if (!(value2.Item1 == null) && value2.Item3 == null)
						{
							int key2 = keyValuePair2.Key;
							if (key != _mapID || key2 != _areaID)
							{
								List<int> list = null;
								if (!dictionary.TryGetValue(key, out list) || list == null)
								{
									list = (dictionary[key] = ListPool<int>.Get());
								}
								if (!list.Contains(key2))
								{
									list.Add(key2);
								}
							}
						}
					}
				}
			}
			if (!dictionary.IsNullOrEmpty<int, List<int>>())
			{
				foreach (KeyValuePair<int, List<int>> keyValuePair3 in dictionary)
				{
					List<int> value3 = keyValuePair3.Value;
					if (!value3.IsNullOrEmpty<int>())
					{
						int key3 = keyValuePair3.Key;
						foreach (int key4 in value3)
						{
							UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable> value4 = this.housingAreaAudioTable[key3][key4];
							if (value4.Item2 != null)
							{
								value4.Item2.Dispose();
								value4.Item2 = null;
							}
							AudioSource audio = value4.Item1;
							FadePlayer fadePlayer = audio.GetComponent<FadePlayer>();
							float startVolume = audio.volume;
							IDisposable item = value4.Item3;
							if (item != null)
							{
								item.Dispose();
							}
							value4.Item3 = ObservableEasing.Linear(_fadeTime, _ignoreTimeScale).FrameTimeInterval(_ignoreTimeScale).TakeUntilDestroy(value4.Item1).Subscribe(delegate(TimeInterval<float> x)
							{
								float num = Mathf.Lerp(startVolume, 0f, x.Value);
								if (fadePlayer != null)
								{
									audio.volume = (fadePlayer.currentVolume = num);
								}
								else
								{
									audio.volume = num;
								}
							}, delegate(Exception ex)
							{
								if (audio == null)
								{
									return;
								}
								if (fadePlayer != null)
								{
									audio.volume = (fadePlayer.currentVolume = 0f);
								}
								else
								{
									audio.volume = 0f;
								}
							}, delegate()
							{
								if (audio == null)
								{
									return;
								}
								if (fadePlayer != null)
								{
									audio.volume = (fadePlayer.currentVolume = 0f);
								}
								else
								{
									audio.volume = 0f;
								}
							});
							this.housingAreaAudioTable[key3][key4] = value4;
						}
					}
				}
			}
			if (!dictionary.IsNullOrEmpty<int, List<int>>())
			{
				foreach (KeyValuePair<int, List<int>> keyValuePair4 in dictionary)
				{
					List<int> value5 = keyValuePair4.Value;
					if (value5 != null)
					{
						ListPool<int>.Release(value5);
					}
				}
			}
			DictionaryPool<int, List<int>>.Release(dictionary);
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x0019FD78 File Offset: 0x0019E178
		public void ActivateMapBGM()
		{
			this.BGMPlayActive = true;
			this.BGMChangePossible = true;
			this.PlayMapBGM();
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x0019FD8E File Offset: 0x0019E18E
		public void PauseMapBGM()
		{
			if (Singleton<Sound>.IsInstance())
			{
				Singleton<Sound>.Instance.PauseBGM();
			}
			this.BGMPlayActive = false;
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x0019FDAC File Offset: 0x0019E1AC
		public void StopMapBGM(float _fadeTime = 0f)
		{
			this.BGMPlayActive = false;
			this.DisposeDelayPlayBGM();
			if (!Singleton<Sound>.IsInstance())
			{
				return;
			}
			GameObject currentBGM = Singleton<Sound>.Instance.currentBGM;
			if (currentBGM == null)
			{
				return;
			}
			AudioSource _audio = currentBGM.GetComponent<AudioSource>();
			FadePlayer fadePlayer = (!(_audio != null)) ? null : _audio.GetComponent<FadePlayer>();
			if (_audio != null && _audio == this.LastBGMAudio)
			{
				this.LastBGMAudio = null;
			}
			if (fadePlayer != null)
			{
				fadePlayer.Stop(_fadeTime);
			}
			else if (_audio != null)
			{
				float _startVolume = _audio.volume;
				ObservableEasing.Linear(_fadeTime, false).FrameInterval<float>().TakeUntilDestroy(_audio).Subscribe(delegate(FrameInterval<float> x)
				{
					_audio.volume = Mathf.Lerp(_startVolume, 0f, x.Value);
				}, delegate(Exception ex)
				{
					if (_audio != null && _audio.gameObject != null)
					{
						UnityEngine.Object.Destroy(_audio.gameObject);
					}
				}, delegate()
				{
					if (_audio != null && _audio.gameObject != null)
					{
						UnityEngine.Object.Destroy(_audio.gameObject);
					}
				});
			}
			Singleton<Sound>.Instance.currentBGM = null;
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x0019FEE0 File Offset: 0x0019E2E0
		public void StopHousingAreaBGM(int _mapID, int _areaID, float _fadeTime = 0f, bool _ignoreTimeScale = false)
		{
			Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>> dictionary = null;
			if (!this.housingAreaAudioTable.TryGetValue(_mapID, out dictionary) || dictionary == null)
			{
				Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>> dictionary2 = new Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>();
				this.housingAreaAudioTable[_mapID] = dictionary2;
				dictionary = dictionary2;
			}
			UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable> valueTuple;
			if (!dictionary.TryGetValue(_areaID, out valueTuple))
			{
				return;
			}
			IDisposable item = valueTuple.Item2;
			if (item != null)
			{
				item.Dispose();
			}
			IDisposable item2 = valueTuple.Item3;
			if (item2 != null)
			{
				item2.Dispose();
			}
			AudioSource audio = valueTuple.Item1;
			FadePlayer fadePlayer = (!(audio != null)) ? null : audio.GetComponent<FadePlayer>();
			if (audio != null)
			{
				float startVolume = audio.volume;
				valueTuple.Item3 = ObservableEasing.Linear(_fadeTime, _ignoreTimeScale).FrameTimeInterval(_ignoreTimeScale).TakeUntilDestroy(audio).Subscribe(delegate(TimeInterval<float> x)
				{
					if (fadePlayer != null)
					{
						audio.volume = (fadePlayer.currentVolume = Mathf.Lerp(startVolume, 0f, x.Value));
					}
					else
					{
						audio.volume = Mathf.Lerp(startVolume, 0f, x.Value);
					}
				}, delegate(Exception ex)
				{
					if (audio != null && audio.gameObject != null)
					{
						UnityEngine.Object.Destroy(audio.gameObject);
					}
				}, delegate()
				{
					if (audio != null && audio.gameObject != null)
					{
						UnityEngine.Object.Destroy(audio.gameObject);
					}
				});
			}
			dictionary.Remove(_areaID);
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x001A0018 File Offset: 0x0019E418
		public void StopHousingAreaBGM(float _fadeTime = 0f)
		{
			if (this.housingAreaAudioTable.IsNullOrEmpty<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>>())
			{
				return;
			}
			List<UnityEx.ValueTuple<int, List<int>>> list = ListPool<UnityEx.ValueTuple<int, List<int>>>.Get();
			foreach (KeyValuePair<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>> keyValuePair in this.housingAreaAudioTable)
			{
				List<int> list2 = ListPool<int>.Get();
				list2.AddRange(keyValuePair.Value.Keys);
				list.Add(new UnityEx.ValueTuple<int, List<int>>(keyValuePair.Key, list2));
			}
			foreach (UnityEx.ValueTuple<int, List<int>> valueTuple in list)
			{
				foreach (int key in valueTuple.Item2)
				{
					UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable> valueTuple2 = this.housingAreaAudioTable[valueTuple.Item1][key];
					IDisposable item = valueTuple2.Item2;
					if (item != null)
					{
						item.Dispose();
					}
					IDisposable item2 = valueTuple2.Item3;
					if (item2 != null)
					{
						item2.Dispose();
					}
					AudioSource audio = valueTuple2.Item1;
					FadePlayer fadePlayer = (!(audio != null)) ? null : audio.GetComponent<FadePlayer>();
					if (fadePlayer != null)
					{
						fadePlayer.Stop(_fadeTime);
					}
					else
					{
						float startVolume = audio.volume;
						valueTuple2.Item3 = ObservableEasing.Linear(_fadeTime, false).FrameTimeInterval(false).TakeUntilDestroy(audio).Subscribe(delegate(TimeInterval<float> x)
						{
							audio.volume = Mathf.Lerp(startVolume, 0f, x.Value);
						}, delegate(Exception ex)
						{
							if (audio != null && audio.gameObject != null)
							{
								UnityEngine.Object.Destroy(audio.gameObject);
							}
						}, delegate()
						{
							if (audio != null && audio.gameObject != null)
							{
								UnityEngine.Object.Destroy(audio.gameObject);
							}
						});
					}
					if (audio != null && audio.gameObject != null)
					{
						UnityEngine.Object.Destroy(audio.gameObject);
					}
				}
			}
			this.housingAreaAudioTable.Clear();
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x001A02AC File Offset: 0x0019E6AC
		public void RemoveAreaAudioClip(int _mapID, int _areaID)
		{
			if (this.jukeBoxAudioClipCacheTable.ContainsKey(_mapID))
			{
				this.jukeBoxAudioClipCacheTable[_mapID].Remove(_areaID);
			}
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x001A02D2 File Offset: 0x0019E6D2
		private void DisposeDelayPlayBGM()
		{
			if (this.delayPlayBGMDisposable != null)
			{
				this.delayPlayBGMDisposable.Dispose();
				this.delayPlayBGMDisposable = null;
			}
			this.BGMChangePossible = true;
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x001A02F8 File Offset: 0x0019E6F8
		public void PlayWideEnvSE(bool _quickChange = false)
		{
			if (_quickChange)
			{
				this.DisposeDelayPlayWideEnv();
			}
			this.WideEnvUpdateFlag = (SoundPlayer.UpdateType)0;
			if (!Singleton<Resources>.IsInstance() || !Singleton<Map>.IsInstance() || !Singleton<Sound>.IsInstance())
			{
				return;
			}
			MapArea mapArea = (!(Singleton<Map>.Instance.Player != null)) ? null : Singleton<Map>.Instance.Player.MapArea;
			if (mapArea == null)
			{
				return;
			}
			float num = (!_quickChange) ? Singleton<Resources>.Instance.SoundPack.EnviroInfo.WideRangeSlowFadeTime : Singleton<Resources>.Instance.SoundPack.EnviroInfo.WideRangeQuickFadeTime;
			int mapID = Singleton<Map>.Instance.MapID;
			bool flag = mapArea != null && Singleton<Resources>.Instance.SoundPack.WideEnvMuteArea(mapID, mapArea.AreaID);
			SoundPack.PlayAreaType areaType = SoundPack.PlayAreaType.Normal;
			Weather weather = Singleton<Map>.Instance.Simulator.Weather;
			List<int> list = ListPool<int>.Get();
			if (!Singleton<Resources>.Instance.SoundPack.TryGetWideEnvIDList(areaType, weather, ref list) || flag)
			{
				if (!this.usedWideEnvSE.IsNullOrEmpty<int, UnityEx.ValueTuple<AudioSource, FadePlayer>>())
				{
					foreach (KeyValuePair<int, UnityEx.ValueTuple<AudioSource, FadePlayer>> keyValuePair in this.usedWideEnvSE)
					{
						UnityEx.ValueTuple<AudioSource, FadePlayer> value = keyValuePair.Value;
						if (value.Item2 != null)
						{
							value.Item2.Stop(num);
						}
						else if (value.Item1 != null)
						{
							Singleton<Sound>.Instance.Stop(Sound.Type.ENV, value.Item1.transform);
						}
					}
					this.usedWideEnvSE.Clear();
				}
				ListPool<int>.Release(list);
				return;
			}
			List<int> list2 = ListPool<int>.Get();
			List<int> list3 = ListPool<int>.Get();
			List<int> list4 = ListPool<int>.Get();
			foreach (KeyValuePair<int, UnityEx.ValueTuple<AudioSource, FadePlayer>> keyValuePair2 in this.usedWideEnvSE)
			{
				if (keyValuePair2.Value.Item1 == null || keyValuePair2.Value.Item1.gameObject == null)
				{
					list2.Add(keyValuePair2.Key);
				}
				else if (!keyValuePair2.Value.Item1.isPlaying)
				{
					list3.Add(keyValuePair2.Key);
				}
				else if (!list.Contains(keyValuePair2.Key))
				{
					list4.Add(keyValuePair2.Key);
				}
			}
			for (int i = 0; i < list2.Count; i++)
			{
				this.usedWideEnvSE.Remove(list2[i]);
			}
			for (int j = 0; j < list3.Count; j++)
			{
				int key = list3[j];
				UnityEngine.Object.Destroy(this.usedWideEnvSE[key].Item1.gameObject);
				this.usedWideEnvSE.Remove(key);
			}
			for (int k = 0; k < list4.Count; k++)
			{
				int key2 = list4[k];
				UnityEx.ValueTuple<AudioSource, FadePlayer> valueTuple = this.usedWideEnvSE[key2];
				if (valueTuple.Item2 != null)
				{
					valueTuple.Item2.Stop(num);
				}
				else if (valueTuple.Item1 != null)
				{
					UnityEngine.Object.Destroy(valueTuple.Item1.gameObject);
				}
				this.usedWideEnvSE.Remove(key2);
			}
			ListPool<int>.Release(list2);
			ListPool<int>.Release(list3);
			ListPool<int>.Release(list4);
			Camera cameraComponent = Map.GetCameraComponent();
			Transform _cameraT = (!(cameraComponent != null)) ? null : cameraComponent.transform;
			if (_cameraT != null)
			{
				for (int l = 0; l < list.Count; l++)
				{
					int _id = list[l];
					if (this.usedWideEnvSE.ContainsKey(_id))
					{
						list.RemoveAll((int x) => x == _id);
					}
				}
				for (int m = 0; m < list.Count; m++)
				{
					int num2 = list[m];
					AudioSource _audio = Singleton<Resources>.Instance.SoundPack.PlayEnviroSE(num2, num);
					if (!(_audio == null))
					{
						FadePlayer componentInChildren = _audio.GetComponentInChildren<FadePlayer>(true);
						_audio.loop = true;
						this.usedWideEnvSE[num2] = new UnityEx.ValueTuple<AudioSource, FadePlayer>(_audio, componentInChildren);
						_audio.LateUpdateAsObservable().TakeUntilDestroy(_cameraT).TakeUntilDestroy(_audio).Subscribe(delegate(Unit _)
						{
							_audio.transform.SetPositionAndRotation(_cameraT.position, _cameraT.rotation);
						});
					}
				}
			}
			ListPool<int>.Release(list);
			if (this.delayPlayWideEnvDisposable != null)
			{
				this.delayPlayWideEnvDisposable.Dispose();
			}
			this.WideEnvChangePossible = false;
			this.delayPlayWideEnvDisposable = Observable.Timer(TimeSpan.FromSeconds((double)num)).TakeUntilDestroy(base.gameObject).Subscribe(delegate(long _)
			{
				this.delayPlayWideEnvDisposable = null;
				this.WideEnvChangePossible = true;
			}, delegate(Exception ex)
			{
				this.delayPlayWideEnvDisposable = null;
				this.WideEnvChangePossible = true;
			}, delegate()
			{
				this.delayPlayWideEnvDisposable = null;
				this.WideEnvChangePossible = true;
			});
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x001A08D8 File Offset: 0x0019ECD8
		public void ActivateWideEnvSE(bool _quickChange = false)
		{
			this.EnvPlayActive = true;
			this.WideEnvChangePossible = true;
			SoundPlayer.UpdateType updateType = (SoundPlayer.UpdateType)0;
			this.PrevWideEnvUpdateFlag = updateType;
			this.WideEnvUpdateFlag = updateType;
			this.PlayWideEnvSE(_quickChange);
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x001A090C File Offset: 0x0019ED0C
		public void StopWideEnvSE(float _fadeTime = 0f)
		{
			this.DisposeDelayPlayWideEnv();
			if (!this.usedWideEnvSE.IsNullOrEmpty<int, UnityEx.ValueTuple<AudioSource, FadePlayer>>())
			{
				foreach (KeyValuePair<int, UnityEx.ValueTuple<AudioSource, FadePlayer>> keyValuePair in this.usedWideEnvSE)
				{
					if (keyValuePair.Value.Item2 != null)
					{
						keyValuePair.Value.Item2.Stop(_fadeTime);
					}
					else if (keyValuePair.Value.Item1 != null && keyValuePair.Value.Item1.gameObject != null)
					{
						UnityEngine.Object.Destroy(keyValuePair.Value.Item1.gameObject);
					}
				}
				this.usedWideEnvSE.Clear();
			}
			this.EnvPlayActive = false;
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x001A0A14 File Offset: 0x0019EE14
		public void DisposeDelayPlayWideEnv()
		{
			if (this.delayPlayWideEnvDisposable != null)
			{
				this.delayPlayWideEnvDisposable.Dispose();
				this.delayPlayWideEnvDisposable = null;
			}
			this.WideEnvChangePossible = true;
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x001A0A3A File Offset: 0x0019EE3A
		public void ActivateAllSound()
		{
			this.ActivateMapBGM();
			this.ActivateWideEnvSE(true);
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x001A0A49 File Offset: 0x0019EE49
		public void StopAllSound(float _fadeTime = 0f)
		{
			this.StopMapBGM(_fadeTime);
			this.MuteHousingAreaBGM(_fadeTime, false);
			this.StopWideEnvSE(_fadeTime);
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x001A0A64 File Offset: 0x0019EE64
		public void Release()
		{
			float fadeTime = (!Singleton<Resources>.IsInstance()) ? 0f : Singleton<Resources>.Instance.SoundPack.BGMInfo.MapBGMFadeTime;
			this.StopMapBGM(fadeTime);
			this.StopHousingAreaBGM(fadeTime);
			this.StopWideEnvSE(fadeTime);
			this.StopAllSubscribe();
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x001A0AB8 File Offset: 0x0019EEB8
		public static AudioClip LoadAudioClip(string fileFullPath, ref bool loadedTarget, uAudio uAudio)
		{
			ExternalAudioClip.LoadFile(fileFullPath, fileFullPath, ref loadedTarget, uAudio);
			AudioClip result = null;
			try
			{
				mp3AudioClip.SongDone = false;
				mp3AudioClip.flare_SongEnd = false;
				uAudio.targetFile = fileFullPath;
				if (uAudio.LoadMainOutputStream())
				{
					string text = null;
					result = ExternalAudioClip.Load(fileFullPath, (long)uAudio.SongLength, uAudio, ref text);
				}
			}
			catch (Exception ex)
			{
			}
			return result;
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x001A0B20 File Offset: 0x0019EF20
		public void RefreshJukeBoxTable(Map mapMana)
		{
			if (mapMana == null)
			{
				return;
			}
			int mapID = mapMana.MapID;
			Dictionary<int, string> dictionary = null;
			if (Singleton<Game>.IsInstance())
			{
				Game instance = Singleton<Game>.Instance;
				if (mapID == 0)
				{
					AIProject.SaveData.Environment environment = instance.Environment;
					dictionary = ((environment != null) ? environment.JukeBoxAudioNameTable : null);
				}
				else
				{
					AIProject.SaveData.Environment environment2 = instance.Environment;
					Dictionary<int, Dictionary<int, string>> dictionary2 = (environment2 != null) ? environment2.AnotherJukeBoxAudioNameTable : null;
					if (dictionary2 != null && (!dictionary2.TryGetValue(mapID, out dictionary) || dictionary == null))
					{
						Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
						dictionary2[mapID] = dictionary3;
						dictionary = dictionary3;
					}
				}
			}
			if (dictionary.IsNullOrEmpty<int, string>())
			{
				return;
			}
			List<int> list = ListPool<int>.Get();
			PointManager pointAgent = mapMana.PointAgent;
			if (pointAgent != null)
			{
				BasePoint[] basePoints = pointAgent.BasePoints;
				if (!basePoints.IsNullOrEmpty<BasePoint>())
				{
					foreach (BasePoint basePoint in basePoints)
					{
						if (!(basePoint == null))
						{
							if (basePoint.IsHousing)
							{
								int areaIDInHousing = basePoint.AreaIDInHousing;
								if (!list.Contains(areaIDInHousing))
								{
									list.Add(areaIDInHousing);
								}
							}
						}
					}
				}
			}
			if (list.IsNullOrEmpty<int>())
			{
				dictionary.Clear();
				ListPool<int>.Release(list);
				return;
			}
			List<int> list2 = ListPool<int>.Get();
			foreach (KeyValuePair<int, string> keyValuePair in dictionary)
			{
				if (!list.Contains(keyValuePair.Key) && !list2.Contains(keyValuePair.Key))
				{
					list2.Add(keyValuePair.Key);
				}
			}
			foreach (int key in list2)
			{
				dictionary.Remove(key);
			}
			ListPool<int>.Release(list);
			ListPool<int>.Release(list2);
		}

		// Token: 0x04003D6B RID: 15723
		private IDisposable delayPlayBGMDisposable;

		// Token: 0x04003D6C RID: 15724
		private uAudio _uAudio;

		// Token: 0x04003D6D RID: 15725
		private Dictionary<int, Dictionary<int, AudioClip>> jukeBoxAudioClipCacheTable = new Dictionary<int, Dictionary<int, AudioClip>>();

		// Token: 0x04003D6E RID: 15726
		private Dictionary<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>> housingAreaAudioTable = new Dictionary<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, IDisposable, IDisposable>>>();

		// Token: 0x04003D6F RID: 15727
		private Dictionary<int, UnityEx.ValueTuple<AudioSource, FadePlayer>> usedWideEnvSE = new Dictionary<int, UnityEx.ValueTuple<AudioSource, FadePlayer>>();

		// Token: 0x04003D73 RID: 15731
		private IDisposable delayPlayWideEnvDisposable;

		// Token: 0x04003D74 RID: 15732
		private IDisposable timeSubscribeDisposable;

		// Token: 0x04003D75 RID: 15733
		private IDisposable areaSubscribeDisposable;

		// Token: 0x04003D76 RID: 15734
		private IDisposable weatherSubscribeDisposable;

		// Token: 0x02000921 RID: 2337
		public static class Directory
		{
			// Token: 0x17000C7F RID: 3199
			// (get) Token: 0x0600424F RID: 16975 RVA: 0x001A0E33 File Offset: 0x0019F233
			public static string AudioFile { get; } = UserData.Create("audio");
		}

		// Token: 0x02000922 RID: 2338
		[Flags]
		public enum UpdateType
		{
			// Token: 0x04003D79 RID: 15737
			Area = 1,
			// Token: 0x04003D7A RID: 15738
			Weather = 2,
			// Token: 0x04003D7B RID: 15739
			Time = 4
		}

		// Token: 0x02000923 RID: 2339
		public struct MapBGMInfo
		{
			// Token: 0x06004251 RID: 16977 RVA: 0x001A0E4B File Offset: 0x0019F24B
			public MapBGMInfo(int _noonID, SoundPlayer.MapBGMInfo.Period[] _noonPeriod, int _nightID, SoundPlayer.MapBGMInfo.Period[] _nightPeriod)
			{
				this.NoonID = _noonID;
				this.NoonPeriod = _noonPeriod;
				this.NightID = _nightID;
				this.NightPeriod = _nightPeriod;
				this.Setting = new Illusion.Game.Utils.Sound.SettingBGM();
			}

			// Token: 0x17000C80 RID: 3200
			// (get) Token: 0x06004252 RID: 16978 RVA: 0x001A0E75 File Offset: 0x0019F275
			// (set) Token: 0x06004253 RID: 16979 RVA: 0x001A0E7D File Offset: 0x0019F27D
			public int NoonID { get; private set; }

			// Token: 0x17000C81 RID: 3201
			// (get) Token: 0x06004254 RID: 16980 RVA: 0x001A0E86 File Offset: 0x0019F286
			// (set) Token: 0x06004255 RID: 16981 RVA: 0x001A0E8E File Offset: 0x0019F28E
			public SoundPlayer.MapBGMInfo.Period[] NoonPeriod { get; private set; }

			// Token: 0x17000C82 RID: 3202
			// (get) Token: 0x06004256 RID: 16982 RVA: 0x001A0E97 File Offset: 0x0019F297
			// (set) Token: 0x06004257 RID: 16983 RVA: 0x001A0E9F File Offset: 0x0019F29F
			public int NightID { get; private set; }

			// Token: 0x17000C83 RID: 3203
			// (get) Token: 0x06004258 RID: 16984 RVA: 0x001A0EA8 File Offset: 0x0019F2A8
			// (set) Token: 0x06004259 RID: 16985 RVA: 0x001A0EB0 File Offset: 0x0019F2B0
			public SoundPlayer.MapBGMInfo.Period[] NightPeriod { get; private set; }

			// Token: 0x17000C84 RID: 3204
			// (get) Token: 0x0600425A RID: 16986 RVA: 0x001A0EB9 File Offset: 0x0019F2B9
			// (set) Token: 0x0600425B RID: 16987 RVA: 0x001A0EC1 File Offset: 0x0019F2C1
			public Illusion.Game.Utils.Sound.SettingBGM Setting { get; private set; }

			// Token: 0x0600425C RID: 16988 RVA: 0x001A0ECC File Offset: 0x0019F2CC
			private bool OverTime(DateTime _base, DateTime _time)
			{
				return _base.Year <= _time.Year && _base.Month <= _time.Month && _base.Day <= _time.Day && _base.Hour <= _time.Hour && _base.Minute <= _time.Minute;
			}

			// Token: 0x0600425D RID: 16989 RVA: 0x001A0F3C File Offset: 0x0019F33C
			private bool UnderTime(DateTime _base, DateTime _time)
			{
				return _time.Year <= _base.Year && _time.Month <= _base.Month && _time.Day <= _base.Day && _time.Hour <= _base.Hour && _time.Minute <= _base.Minute;
			}

			// Token: 0x0600425E RID: 16990 RVA: 0x001A0FAC File Offset: 0x0019F3AC
			public int GetID(DateTime _time)
			{
				if (!this.NoonPeriod.IsNullOrEmpty<SoundPlayer.MapBGMInfo.Period>())
				{
					foreach (SoundPlayer.MapBGMInfo.Period period in this.NoonPeriod)
					{
						if (this.OverTime(period.Start, _time) && this.UnderTime(period.End, _time))
						{
							return this.NoonID;
						}
					}
				}
				if (!this.NightPeriod.IsNullOrEmpty<SoundPlayer.MapBGMInfo.Period>())
				{
					foreach (SoundPlayer.MapBGMInfo.Period period2 in this.NightPeriod)
					{
						if (this.OverTime(period2.Start, _time) && this.UnderTime(period2.End, _time))
						{
							return this.NightID;
						}
					}
				}
				return -1;
			}

			// Token: 0x02000924 RID: 2340
			public struct Period
			{
				// Token: 0x04003D81 RID: 15745
				public DateTime Start;

				// Token: 0x04003D82 RID: 15746
				public DateTime End;
			}
		}
	}
}
