using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using ADV;
using AIProject.Definitions;
using AIProject.Player;
using AIProject.SaveData;
using AIProject.UI;
using ConfigScene;
using Housing;
using Manager;
using ReMotion;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEx;

namespace AIProject.Scene
{
	// Token: 0x02000F19 RID: 3865
	public class MapScene : Singleton<MapScene>, ISystemCommand
	{
		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x06007EAD RID: 32429 RVA: 0x0035E321 File Offset: 0x0035C721
		// (set) Token: 0x06007EAE RID: 32430 RVA: 0x0035E328 File Offset: 0x0035C728
		public static List<UnityEx.ValueTuple<string, string>> AssetBundlePaths { get; private set; } = new List<UnityEx.ValueTuple<string, string>>();

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x06007EAF RID: 32431 RVA: 0x0035E330 File Offset: 0x0035C730
		public NavMeshSurface NavMeshSurface
		{
			[CompilerGenerated]
			get
			{
				return this._navMeshSurface;
			}
		}

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x06007EB0 RID: 32432 RVA: 0x0035E338 File Offset: 0x0035C738
		public Transform WarpPoint
		{
			[CompilerGenerated]
			get
			{
				return this._warpPoint;
			}
		}

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x06007EB1 RID: 32433 RVA: 0x0035E340 File Offset: 0x0035C740
		// (set) Token: 0x06007EB2 RID: 32434 RVA: 0x0035E348 File Offset: 0x0035C748
		public bool EnabledInput { get; set; }

		// Token: 0x06007EB3 RID: 32435 RVA: 0x0035E354 File Offset: 0x0035C754
		public void OnUpdateInput()
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			foreach (AIProject.UI.ICommandData commandData in this._systemCommands)
			{
				commandData.Invoke(instance);
			}
		}

		// Token: 0x06007EB4 RID: 32436 RVA: 0x0035E3B8 File Offset: 0x0035C7B8
		public static void AddAssetBundlePath(AssetBundleInfo info)
		{
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == info.assetbundle && x.Item2 == info.manifest))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(info.assetbundle, info.manifest));
			}
		}

		// Token: 0x06007EB5 RID: 32437 RVA: 0x0035E414 File Offset: 0x0035C814
		public static void AddAssetBundlePath(string assetBundle, string manifest)
		{
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == assetBundle && x.Item2 == manifest))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(assetBundle, manifest));
			}
		}

		// Token: 0x06007EB6 RID: 32438 RVA: 0x0035E46C File Offset: 0x0035C86C
		public static void AddAssetBundlePath(string assetBundle)
		{
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == assetBundle))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(assetBundle, string.Empty));
			}
		}

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x06007EB7 RID: 32439 RVA: 0x0035E4BB File Offset: 0x0035C8BB
		// (set) Token: 0x06007EB8 RID: 32440 RVA: 0x0035E4C3 File Offset: 0x0035C8C3
		public bool IsLoading { get; private set; }

		// Token: 0x06007EB9 RID: 32441 RVA: 0x0035E4CC File Offset: 0x0035C8CC
		public static bool EqualsSequence(bool[] a, bool[] b)
		{
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x0035E508 File Offset: 0x0035C908
		private void StartFadingStream(IConnectableObservable<TimeInterval<float>> stream)
		{
			LoadingPanel loadingPanel = Singleton<Scene>.Instance.loadingPanel;
			float startAlpha = loadingPanel.CanvasGroup.alpha;
			Manager.Map instance = Singleton<Manager.Map>.Instance;
			foreach (KeyValuePair<int, AgentActor> keyValuePair in instance.AgentTable)
			{
				if (keyValuePair.Value.AgentData.CarryingItem != null && keyValuePair.Value.Mode != Desire.ActionType.SearchEatSpot && keyValuePair.Value.Mode != Desire.ActionType.EndTaskEat && keyValuePair.Value.Mode != Desire.ActionType.EndTaskEatThere)
				{
					keyValuePair.Value.AgentData.CarryingItem = null;
				}
				keyValuePair.Value.EnableBehavior();
				keyValuePair.Value.BehaviorResources.ChangeMode(keyValuePair.Value.Mode);
			}
			PlayerActor player = instance.Player;
			string prevPlayerStateFromCharaCreate = Game.PrevPlayerStateFromCharaCreate;
			if (!prevPlayerStateFromCharaCreate.IsNullOrEmpty())
			{
				player.PlayerController.ChangeState(prevPlayerStateFromCharaCreate);
				player.CurrentDevicePoint = Singleton<Manager.Map>.Instance.PointAgent.DevicePointDic[Game.PrevAccessDeviceID];
			}
			else
			{
				ReadOnlyDictionary<int, AgentActor> agentTable = instance.AgentTable;
				int? partnerID = player.PlayerData.PartnerID;
				AgentActor agentActor;
				if (agentTable.TryGetValue((partnerID == null) ? -1 : partnerID.Value, out agentActor))
				{
					player.Partner = agentActor;
					agentActor.Partner = player;
					if (player.PlayerData.IsOnbu)
					{
						player.PlayerController.ChangeState("Onbu");
					}
					else
					{
						agentActor.BehaviorResources.ChangeMode(Desire.ActionType.Date);
						agentActor.Mode = Desire.ActionType.Date;
						player.Mode = Desire.ActionType.Date;
						player.PlayerController.ChangeState("Normal");
					}
				}
				else
				{
					player.PlayerController.ChangeState("Normal");
				}
			}
			Game.PrevPlayerStateFromCharaCreate = null;
			this._enabledCommandArea = player.PlayerController.CommandArea.enabled;
			player.PlayerController.CommandArea.enabled = false;
			player.PlayerController.CommandArea.InitCommandStates();
			player.PlayerController.CommandArea.RefreshCommands();
			if (Singleton<Game>.Instance.Environment.TutorialProgress == 0)
			{
				FadeItem blackoutPanel = MapUIContainer.FadeCanvas.GetPanel(FadeCanvas.PanelType.Blackout);
				blackoutPanel.CanvasGroup.alpha = 1f;
				stream.Subscribe(delegate(TimeInterval<float> x)
				{
					this.Fade(loadingPanel.CanvasGroup, startAlpha, 0f, x.Value);
				}, delegate()
				{
					loadingPanel.Stop();
					blackoutPanel = MapUIContainer.FadeCanvas.GetPanel(FadeCanvas.PanelType.Blackout);
					blackoutPanel.CanvasGroup.alpha = 1f;
					this.OnLoaded();
					blackoutPanel = MapUIContainer.FadeCanvas.GetPanel(FadeCanvas.PanelType.Blackout);
					blackoutPanel.CanvasGroup.alpha = 1f;
				});
				stream.Connect();
			}
			else
			{
				stream.Subscribe(delegate(TimeInterval<float> x)
				{
					this.Fade(loadingPanel.CanvasGroup, startAlpha, 0f, x.Value);
				}, delegate()
				{
					loadingPanel.Stop();
					this.OnLoaded();
				});
				stream.Connect();
			}
		}

		// Token: 0x06007EBB RID: 32443 RVA: 0x0035E810 File Offset: 0x0035CC10
		private void Fade(FadeItem panel, float start, float destination, float value)
		{
			panel.CanvasGroup.alpha = Mathf.Lerp(start, destination, value);
		}

		// Token: 0x06007EBC RID: 32444 RVA: 0x0035E826 File Offset: 0x0035CC26
		private void Fade(CanvasGroup canvasGroup, float start, float end, float value)
		{
			canvasGroup.alpha = Mathf.Lerp(start, end, value);
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x0035E838 File Offset: 0x0035CC38
		private void OnLoaded()
		{
			Singleton<Scene>.Instance.loadingPanel.gameObject.SetActive(false);
			Singleton<Manager.Map>.Instance.StartSubscribe();
			Singleton<AnimalManager>.Instance.StartSubscribe();
			int tutorialProgress = Manager.Map.GetTutorialProgress();
			if (tutorialProgress != 0)
			{
				Singleton<SoundPlayer>.Instance.StartAllSubscribe();
				Singleton<SoundPlayer>.Instance.PlayActiveAll = true;
			}
			Singleton<Manager.Map>.Instance.Player.PlayerController.CommandArea.enabled = this._enabledCommandArea;
			Manager.Map instance = Singleton<Manager.Map>.Instance;
			MapUIContainer.SetMarkerTargetCameraTransform(instance.Player.CameraControl.transform);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			Singleton<Manager.Input>.Instance.SetupState();
			bool tutorialMode = Manager.Map.TutorialMode;
			instance.InitSearchActorTargetsAll();
			if (instance.TutorialAgent != null)
			{
				instance.TutorialAgent.ChangeFirstTutorialBehavior();
			}
			if (tutorialMode)
			{
				if (tutorialProgress < 14)
				{
					instance.CreateTutorialLockArea();
					Singleton<MapUIContainer>.Instance.MinimapUI.MinimapLockAreaInit();
				}
				instance.SetActiveMapEffect(false);
				if (tutorialProgress == 3)
				{
					instance.CreateTutorialSearchPoint();
				}
			}
			if (!Singleton<Game>.Instance.WorldData.Cleared)
			{
			}
			if (Singleton<Game>.Instance.Environment.TutorialProgress == 0)
			{
				PlayerActor player = instance.Player;
				player.PlayerController.ChangeState("OpeningWakeUp");
			}
			this.IsLoading = false;
			if (!tutorialMode)
			{
				instance.Simulator.EnabledTimeProgression = true;
			}
			IObservable<long> source = from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _;
			(from _ in source
			select MapUIContainer.AnyUIActive()).DistinctUntilChanged<bool>().Subscribe(delegate(bool isOn)
			{
				this.IsCursorLock = !isOn;
			});
			IObservable<bool> source2 = from _ in source
			select this.IsCursorLock;
			(from isOn in source2.DistinctUntilChanged<bool>()
			where !isOn
			select isOn).Subscribe(delegate(bool isOn)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			});
			(from isOn in source2
			where isOn
			select isOn).Subscribe(delegate(bool _)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}, delegate()
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			});
			this.IsCursorLock = true;
			this.InitShortCutEvents();
			Manager.Map.RefreshStoryUI();
			if (Game.IsFreeMode && Game.IsFirstGame)
			{
				this.SaveProfile(false);
			}
			(from _ in Observable.Interval(TimeSpan.FromMinutes(10.0)).ObserveOnMainThread<long>().TakeUntilDestroy(this)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				if (Singleton<CraftScene>.IsInstance())
				{
					return;
				}
				this.SaveProfile(true);
			});
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x0035EB28 File Offset: 0x0035CF28
		private void InitShortCutEvents()
		{
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Escape
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				if (UnityEngine.Input.GetKey(KeyCode.F2))
				{
					return;
				}
				if (UnityEngine.Input.GetKey(KeyCode.F3))
				{
					return;
				}
				GameUtil.GameEnd(true);
			});
			this._systemCommands.Add(keyCodeDownCommand);
			KeyCodeDownCommand keyCodeDownCommand2 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse2
			};
			keyCodeDownCommand2.TriggerEvent.AddListener(delegate()
			{
				if (Singleton<Manager.Map>.Instance.IsWarpProc)
				{
					return;
				}
				if (UnityEngine.Input.GetKey(KeyCode.Mouse1))
				{
					return;
				}
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				PlayerController playerController = player.PlayerController;
				if (playerController.State is Normal || playerController.State is Houchi)
				{
					playerController.ChangeState("Menu");
				}
			});
			this._systemCommands.Add(keyCodeDownCommand2);
			KeyCodeDownCommand keyCodeDownCommand3 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.M
			};
			keyCodeDownCommand3.TriggerEvent.AddListener(delegate()
			{
				if (MapUIContainer.FadeCanvas.IsFading)
				{
					return;
				}
				if (Singleton<Manager.Map>.Instance.IsWarpProc)
				{
					return;
				}
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				IState state = player.PlayerController.State;
				if (state is Normal || state is Houchi)
				{
					if (Singleton<MapUIContainer>.Instance.MinimapUI.VisibleMode == 0)
					{
						player.Controller.ChangeState("WMap");
					}
					else if (Singleton<MapUIContainer>.Instance.MinimapUI.VisibleMode == 2)
					{
						bool miniMap = Config.GameData.MiniMap;
						if (miniMap)
						{
							Singleton<MapUIContainer>.Instance.MinimapUI.OpenMiniMap();
						}
						else
						{
							player.Controller.ChangeState("WMap");
						}
					}
				}
			});
			this._systemCommands.Add(keyCodeDownCommand3);
			KeyCodeDownCommand keyCodeDownCommand4 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Space
			};
			keyCodeDownCommand4.TriggerEvent.AddListener(delegate()
			{
				if (Manager.Map.TutorialMode)
				{
					return;
				}
				if (Singleton<Manager.Map>.Instance.IsWarpProc)
				{
					return;
				}
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				IState state = player.PlayerController.State;
				if (state is Normal || state is Houchi || state is Onbu)
				{
					player.CallProc();
				}
			});
			this._systemCommands.Add(keyCodeDownCommand4);
			KeyCodeDownCommand keyCodeDownCommand5 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.F1
			};
			keyCodeDownCommand5.TriggerEvent.AddListener(delegate()
			{
				if (Singleton<Manager.Map>.Instance.IsWarpProc)
				{
					return;
				}
				if (UnityEngine.Input.GetKey(KeyCode.Mouse1))
				{
					return;
				}
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				IState state = player.PlayerController.State;
				if (state is Normal || state is Houchi)
				{
					player.Controller.ChangeState("Menu");
				}
			});
			this._systemCommands.Add(keyCodeDownCommand5);
			KeyCodeDownCommand keyCodeDownCommand6 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.F2
			};
			keyCodeDownCommand6.TriggerEvent.AddListener(delegate()
			{
				if (UnityEngine.Input.GetKey(KeyCode.Escape))
				{
					return;
				}
				if (UnityEngine.Input.GetKey(KeyCode.F3))
				{
					return;
				}
				if (Singleton<Manager.Map>.Instance.IsWarpProc)
				{
					return;
				}
				PlayerActor player = Manager.Map.GetPlayer();
				IState state = (player != null) ? player.PlayerController.State : null;
				if (player.CameraControl.CinemachineBrain.IsBlending)
				{
					return;
				}
				int index = 0;
				System.Action closedEvent = null;
				if (state is Normal || state is Houchi || state is Onbu || state is Communication)
				{
					Singleton<Game>.Instance.LoadShortcut(index, closedEvent);
				}
				else if (state is Fishing)
				{
					index = 3;
					float timeScale = Time.timeScale;
					Time.timeScale = 0f;
					closedEvent = delegate()
					{
						Time.timeScale = timeScale;
					};
					Singleton<Game>.Instance.LoadShortcut(index, closedEvent);
				}
				else if (state is Photo)
				{
					index = 5;
					float timeScale = Time.timeScale;
					closedEvent = delegate()
					{
						Time.timeScale = timeScale;
					};
					Singleton<Game>.Instance.LoadShortcut(index, closedEvent);
				}
				else if (state is Sex)
				{
					index = 2;
					float timeScale = Time.timeScale;
					Time.timeScale = 0f;
					closedEvent = delegate()
					{
						Time.timeScale = timeScale;
					};
					Singleton<Game>.Instance.LoadShortcut(index, closedEvent);
				}
			});
			this._systemCommands.Add(keyCodeDownCommand6);
			KeyCodeDownCommand keyCodeDownCommand7 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.F3
			};
			keyCodeDownCommand7.TriggerEvent.AddListener(delegate()
			{
				if (Singleton<Manager.Map>.Instance.IsWarpProc)
				{
					return;
				}
				PlayerActor player = Singleton<Manager.Map>.Instance.Player;
				IState state = player.PlayerController.State;
				if (state is Normal || state is Houchi || state is Onbu)
				{
					if (this._charasEntry == null || this._charasEntry.Length != Config.GraphicData.CharasEntry.Length)
					{
						this._charasEntry = new bool[Config.GraphicData.CharasEntry.Length];
					}
					for (int i = 0; i < this._charasEntry.Length; i++)
					{
						this._charasEntry[i] = Config.GraphicData.CharasEntry[i];
					}
					ConfigWindow.UnLoadAction = delegate()
					{
						if (!MapScene.EqualsSequence(this._charasEntry, Config.GraphicData.CharasEntry))
						{
							player.PlayerController.ChangeState("Idle");
							Singleton<Manager.Map>.Instance.ApplyConfig(null, delegate
							{
								player.PlayerController.ChangeState("Normal");
							});
						}
					};
					ConfigWindow.TitleChangeAction = delegate()
					{
						ConfigWindow.UnLoadAction = null;
						Singleton<Game>.Instance.Dialog.TimeScale = 1f;
					};
					Singleton<Game>.Instance.LoadConfig();
				}
			});
			this._systemCommands.Add(keyCodeDownCommand7);
			KeyCodeDownCommand keyCodeDownCommand8 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.F11
			};
			keyCodeDownCommand8.TriggerEvent.AddListener(delegate()
			{
				this.CaptureSS();
			});
			this._systemCommands.Add(keyCodeDownCommand8);
			KeyCodeDownCommand keyCodeDownCommand9 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.K
			};
			keyCodeDownCommand9.TriggerEvent.AddListener(delegate()
			{
				Config.GraphicData.FaceLight = !Config.GraphicData.FaceLight;
			});
			this._systemCommands.Add(keyCodeDownCommand9);
			KeyCodeDownCommand keyCodeDownCommand10 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.L
			};
			keyCodeDownCommand10.TriggerEvent.AddListener(delegate()
			{
				Config.GraphicData.AmbientLight = !Config.GraphicData.AmbientLight;
			});
			this._systemCommands.Add(keyCodeDownCommand10);
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x06007EBF RID: 32447 RVA: 0x0035EDDF File Offset: 0x0035D1DF
		// (set) Token: 0x06007EC0 RID: 32448 RVA: 0x0035EDE7 File Offset: 0x0035D1E7
		public bool IsCursorLock { get; set; }

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x06007EC1 RID: 32449 RVA: 0x0035EDF0 File Offset: 0x0035D1F0
		// (set) Token: 0x06007EC2 RID: 32450 RVA: 0x0035EDF8 File Offset: 0x0035D1F8
		public bool isLoadEnd { get; private set; }

		// Token: 0x06007EC3 RID: 32451 RVA: 0x0035EE04 File Offset: 0x0035D204
		private IEnumerator Load()
		{
			Game gameMgr = Singleton<Game>.Instance;
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			bool isFreeMode = worldData.FreeMode;
			bool isFirstGame = Game.IsFirstGame;
			if (Singleton<MapUIContainer>.IsInstance())
			{
				StorySupportUI storySupportUI = MapUIContainer.StorySupportUI;
				if (isFreeMode)
				{
					worldData.Cleared = true;
					if (worldData.Environment.TutorialProgress <= 28)
					{
						worldData.Environment.TutorialProgress = 29;
					}
				}
				else if (worldData.Environment.TutorialProgress == 28)
				{
					worldData.Environment.TutorialProgress = 29;
				}
				if (worldData.Environment.TutorialProgress == 29 && worldData.MapID != 0)
				{
					worldData.Environment.TutorialProgress = 30;
				}
				storySupportUI.SetIndexClose(gameMgr.WorldData.Environment.TutorialProgress);
			}
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			mapManager.MapRoot = this._mapRoot;
			mapManager.NavMeshSurface = this._navMeshSurface;
			mapManager.InitializeDefaultState();
			yield return mapManager.LoadMap(worldData.MapID);
			if (isFreeMode)
			{
				for (int i = -1; i <= 2; i++)
				{
					mapManager.SetBaseOpenState(0, i, true);
				}
			}
			yield return mapManager.LoadEnvironment();
			yield return mapManager.LoadNavMeshSource();
			yield return mapManager.LoadElements();
			yield return mapManager.LoadMerchantPoint();
			yield return mapManager.LoadEventPoints();
			yield return mapManager.LoadStoryPoints();
			yield return mapManager.LoadHousingObj(worldData.MapID);
			mapManager.SetVanishList();
			mapManager.BuildNavMesh();
			yield return mapManager.LoadAnimalPoint();
			yield return mapManager.SetupPoint();
			AnimalManager animalManager = Singleton<AnimalManager>.Instance;
			yield return animalManager.SetupPointsAsync(mapManager.PointAgent);
			if (Singleton<SoundPlayer>.IsInstance())
			{
				Singleton<SoundPlayer>.Instance.RefreshJukeBoxTable(mapManager);
			}
			WorldData data = gameMgr.WorldData;
			if (data.AgentTable.IsNullOrEmpty<int, AgentData>())
			{
				int agentMax = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AgentMax;
				int agentDefaultNum = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AgentDefaultNum;
				for (int j = 0; j < agentMax; j++)
				{
					AgentData agentData = new AgentData();
					data.AgentTable[j] = agentData;
					AgentData agentData2 = agentData;
					agentData2.OpenState = (j < 1);
					agentData2.PlayEnterScene = (j < 1);
				}
			}
			yield return null;
			SaveData profile = gameMgr.Data;
			mapManager.ActorRoot = this._actorRoot;
			bool existsBackup = gameMgr.ExistsBackup(worldData.WorldID);
			yield return mapManager.ApplyProfile(gameMgr.WorldData, existsBackup);
			mapManager.InitActionPoints();
			AQUAS_Reflection[] reflections = mapManager.WaterRefrections;
			if (reflections != null)
			{
				foreach (AQUAS_Reflection aquas_Reflection in reflections)
				{
					aquas_Reflection.enabled = false;
				}
			}
			mapManager.InitCommandable();
			mapManager.AddAppendTargetPoints(Singleton<Manager.Housing>.Instance.ActionPoints);
			mapManager.AddRuntimeFarmPoints(Singleton<Manager.Housing>.Instance.FarmPoints);
			mapManager.AddPetHomePoints(Singleton<Manager.Housing>.Instance.PetHomePoints);
			mapManager.AddJukePoints(Singleton<Manager.Housing>.Instance.JukePoints);
			mapManager.AddRuntimeCraftPoints(Singleton<Manager.Housing>.Instance.CraftPoints);
			mapManager.AddRuntimeLightSwitchPoints(Singleton<Manager.Housing>.Instance.LightSwitchPoints);
			mapManager.AddAppendHPoints(Singleton<Manager.Housing>.Instance.HPoints);
			foreach (ActionPoint actionPoint in Singleton<Manager.Housing>.Instance.ActionPoints)
			{
				OffMeshLink[] componentsInChildren = actionPoint.GetComponentsInChildren<OffMeshLink>();
				foreach (OffMeshLink offMeshLink in componentsInChildren)
				{
					offMeshLink.UpdatePositions();
				}
			}
			mapManager.LoadAgentTargetActionPoint();
			mapManager.LoadAgentTargetActor();
			mapManager.PointAgent.CreateHousingWaypoint();
			yield return mapManager.LoadAnimals(gameMgr.WorldData, existsBackup);
			mapManager.RefreshAreaOpenLinkedObject();
			mapManager.RefreshTimeOpenLinkedObject();
			foreach (SoundData soundData in Config.SoundData.Sounds)
			{
				if (soundData != null)
				{
					soundData.Refresh();
				}
			}
			yield return mapManager.LoadEnviroObject();
			Manager.Map map = mapManager;
			int mapID = mapManager.MapID;
			MapArea mapArea = mapManager.Player.MapArea;
			int? num = (mapArea != null) ? new int?(mapArea.AreaID) : null;
			map.ResettingEnviroAreaElement(mapID, (num == null) ? 0 : num.Value);
			Manager.Map map2 = mapManager;
			int mapID2 = mapManager.MapID;
			MapArea mapArea2 = mapManager.Player.MapArea;
			int? num2 = (mapArea2 != null) ? new int?(mapArea2.AreaID) : null;
			map2.RefreshHousingEnv3DSEPoints(mapID2, (num2 == null) ? 0 : num2.Value);
			global::WaitForSecondsRealtime waitRealtime = new global::WaitForSecondsRealtime(1f);
			yield return waitRealtime;
			WaitForSeconds waitTime = new WaitForSeconds(0.5f);
			while (!this.WaitCompletionAgents())
			{
				yield return waitTime;
			}
			foreach (AgentActor agentActor in Singleton<Manager.Map>.Instance.AgentTable.Values)
			{
				agentActor.ActivateNavMeshAgent();
			}
			yield return null;
			foreach (AgentActor agent in Singleton<Manager.Map>.Instance.AgentTable.Values)
			{
				if (agent.AgentData.ReservedWaypointIDList.IsNullOrEmpty<int>())
				{
					yield return agent.CalculateWaypoints();
				}
				else
				{
					agent.LoadWaypoints();
					yield return null;
				}
				if (agent.AgentData.YandereWarpRetry)
				{
					agent.YandereWarpRetryReserve();
				}
			}
			if (!mapManager.IsHour7After)
			{
				foreach (AgentActor agentActor2 in mapManager.AgentTable.Values)
				{
					agentActor2.SetAdvEventLimitationResetReserve();
					agentActor2.SetGreetFlagResetReserve();
				}
			}
			mapManager.Player.ActivateNavMeshAgent();
			mapManager.Merchant.ActivateNavMeshAgent();
			mapManager.Merchant.FirstAppear();
			MapUIContainer.InitializeMinimap();
			animalManager.StartAllAnimalCreate();
			yield return Singleton<Manager.Resources>.Instance.HSceneTable.LoadHObj();
			if (Singleton<AnimalManager>.IsInstance())
			{
				Singleton<AnimalManager>.Instance.ActiveMapScene = true;
			}
			GC.Collect();
			this.isLoadEnd = true;
			yield break;
		}

		// Token: 0x06007EC4 RID: 32452 RVA: 0x0035EE20 File Offset: 0x0035D220
		private bool WaitCompletionAgents()
		{
			foreach (AgentActor agentActor in Singleton<Manager.Map>.Instance.AgentTable.Values)
			{
				if (!agentActor.IsInit)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007EC5 RID: 32453 RVA: 0x0035EE90 File Offset: 0x0035D290
		public void SaveProfile(bool isAuto = false)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return;
			}
			string worldSaveDataFile = Path.WorldSaveDataFile;
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			worldData.SaveTime = DateTime.Now;
			worldData.SaveTimeString = worldData.SaveTime.ToString("o");
			worldData.Environment.SetSimulation(Singleton<Manager.Map>.Instance.Simulator);
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			worldData.PlayerData.IsOnbu = (player.PlayerController.State != null && player.PlayerController.PrevStateName == "Onbu");
			if (player.AgentPartner != null)
			{
				worldData.PlayerData.PartnerID = new int?(player.AgentPartner.ID);
			}
			else
			{
				worldData.PlayerData.PartnerID = new int?(-1);
			}
			foreach (AgentActor agentActor in Singleton<Manager.Map>.Instance.AgentTable.Values)
			{
				agentActor.ChaControl.chaFile.SaveCharaFile(agentActor.ChaControl.chaFile.charaFileName, byte.MaxValue, false);
				if (agentActor.CurrentPoint != null)
				{
					agentActor.AgentData.CurrentActionPointID = agentActor.CurrentPoint.RegisterID;
				}
				else
				{
					agentActor.AgentData.CurrentActionPointID = -1;
				}
				if (agentActor.TargetInSightActor != null)
				{
					agentActor.AgentData.ActionTargetID = agentActor.TargetInSightActor.ID;
				}
				else
				{
					agentActor.AgentData.ActionTargetID = -1;
				}
				agentActor.AgentData.ScheduleEnabled = agentActor.Schedule.enabled;
				agentActor.AgentData.ScheduleElapsedTime = agentActor.Schedule.elapsedTime;
				agentActor.AgentData.ScheduleDuration = agentActor.Schedule.duration;
				agentActor.AgentData.ReservedWaypointIDList.Clear();
				foreach (Waypoint waypoint in agentActor.GetReservedWaypoints())
				{
					agentActor.AgentData.ReservedWaypointIDList.Add(waypoint.ID);
				}
				agentActor.AgentData.WalkRouteWaypointIDList.Clear();
				foreach (Waypoint waypoint2 in agentActor.WalkRoute)
				{
					if (!(waypoint2 == null))
					{
						agentActor.AgentData.WalkRouteWaypointIDList.Add(waypoint2.ID);
					}
				}
			}
			if (Singleton<AnimalManager>.IsInstance())
			{
				Singleton<AnimalManager>.Instance.SetupSaveDataWildAnimals();
			}
			SaveData data = Singleton<Game>.Instance.Data;
			WorldData worldData2;
			if (isAuto)
			{
				if (data.AutoData == null)
				{
					data.AutoData = new WorldData();
				}
				data.AutoData.Copy(worldData);
			}
			else if (data.WorldList.TryGetValue(worldData.WorldID, out worldData2))
			{
				worldData2.Copy(worldData);
			}
			else
			{
				WorldData worldData3 = new WorldData();
				data.WorldList[worldData.WorldID] = worldData3;
				worldData2 = worldData3;
				worldData2.Copy(worldData);
			}
			Singleton<Game>.Instance.SaveProfile(worldSaveDataFile);
			GC.Collect();
		}

		// Token: 0x06007EC6 RID: 32454 RVA: 0x0035F254 File Offset: 0x0035D654
		private void CaptureSS()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Photo);
			Singleton<Manager.Map>.Instance.Player.CameraControl.ScreenShot.Capture(string.Empty);
		}

		// Token: 0x06007EC7 RID: 32455 RVA: 0x0035F285 File Offset: 0x0035D685
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			base.Awake();
		}

		// Token: 0x06007EC8 RID: 32456 RVA: 0x0035F29C File Offset: 0x0035D69C
		private IEnumerator Start()
		{
			Manager.Resources rm = Singleton<Manager.Resources>.Instance;
			QualitySettings.shadowDistance = (float)rm.DefinePack.MapDefines.ShadowDistance;
			Singleton<Scene>.Instance.loadingPanel.gameObject.SetActive(true);
			Singleton<Scene>.Instance.loadingPanel.CanvasGroup.alpha = 1f;
			Singleton<Scene>.Instance.loadingPanel.Play();
			Singleton<Manager.Map>.Instance.PlayerStartPoint = this._startPositionPoint;
			this.IsLoading = true;
			rm.SetupMap();
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			GameObject uiSource = CommonLib.LoadAsset<GameObject>(definePack.ABPaths.MapScenePrefabAdd07, "MapUI", false, definePack.ABManifests.Add07);
			GameObject uiInstance = UnityEngine.Object.Instantiate<GameObject>(uiSource);
			uiInstance.transform.SetParent(base.transform, false);
			IConnectableObservable<TimeInterval<float>> fadeOutProgression = ObservableEasing.EaseOutQuint(1f, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			yield return rm.LoadMapResourceStream.ToYieldInstruction<Unit>();
			IConnectableObservable<Unit> stream = Observable.FromCoroutine(() => this.Load(), false).Publish<Unit>();
			stream.Connect();
			Singleton<Manager.Input>.Instance.SystemElements.RemoveAll((ISystemCommand x) => x is MapScene);
			Singleton<Manager.Input>.Instance.SystemElements.Add(this);
			Singleton<Manager.Input>.Instance.ActionElements.RemoveAll((IActionCommand x) => x is CommandLabel);
			Singleton<Manager.Input>.Instance.ActionElements.Add(MapUIContainer.CommandLabel);
			fadeOutProgression.Subscribe(delegate(TimeInterval<float> _)
			{
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				this.EnabledInput = true;
				MapUIContainer.CommandLabel.EnabledInput = true;
				Singleton<Manager.Input>.Instance.ActionElements.Add(Singleton<Manager.Map>.Instance.Player.CameraControl);
				ActorCameraControl cameraControl = Singleton<Manager.Map>.Instance.Player.CameraControl;
				cameraControl.EnabledInput = true;
				Singleton<Sound>.Instance.Listener = cameraControl.transform;
			});
			Observable.WhenAll(new IObservable<Unit>[]
			{
				stream
			}).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				if (Singleton<Game>.Instance.Environment.TutorialProgress != 0)
				{
					Singleton<SoundPlayer>.Instance.PlayMapBGM();
					Singleton<SoundPlayer>.Instance.PlayWideEnvSE(true);
					FadeItem panel = MapUIContainer.FadeCanvas.GetPanel(FadeCanvas.PanelType.Blackout);
					panel.CanvasGroup.alpha = 0f;
				}
				this.StartFadingStream(fadeOutProgression);
			});
			Singleton<MapUIContainer>.Instance.advScene.Scenario.TextLog += delegate(string name, string text, IReadOnlyCollection<TextScenario.IVoice[]> voices)
			{
				MapUIContainer.AddLog(name, text, voices);
			};
			Singleton<MapUIContainer>.Instance.advScene.Scenario.VisibleLog += delegate()
			{
				MapUIContainer.GameLogUI.IsActiveControl = true;
				MapUIContainer.GameLogUI.OnClosed = delegate()
				{
					Singleton<MapUIContainer>.Instance.advScene.Scenario.captionSystem.Visible = true;
				};
				Singleton<MapUIContainer>.Instance.advScene.Scenario.captionSystem.Visible = false;
			};
			yield break;
		}

		// Token: 0x06007EC9 RID: 32457 RVA: 0x0035F2B8 File Offset: 0x0035D6B8
		private void Update()
		{
			if (Singleton<Game>.IsInstance())
			{
				Game instance = Singleton<Game>.Instance;
				AIProject.SaveData.Environment environment = instance.Environment;
				if (environment != null)
				{
					environment.TotalPlayTime = environment.TotalPlayTime.TimeSpan + TimeSpan.FromSeconds((double)Time.unscaledDeltaTime);
				}
			}
		}

		// Token: 0x06007ECA RID: 32458 RVA: 0x0035F30B File Offset: 0x0035D70B
		private void OnDisable()
		{
			if (Singleton<Manager.Input>.IsInstance())
			{
				Singleton<Manager.Input>.Instance.SystemElements.Remove(this);
				if (Singleton<MapUIContainer>.IsInstance())
				{
					Singleton<Manager.Input>.Instance.ActionElements.Remove(MapUIContainer.CommandLabel);
				}
			}
		}

		// Token: 0x06007ECB RID: 32459 RVA: 0x0035F348 File Offset: 0x0035D748
		protected override void OnDestroy()
		{
			if (Singleton<MapScene>.Instance != this)
			{
				return;
			}
			base.OnDestroy();
			if (!Singleton<Scene>.IsInstance())
			{
				return;
			}
			if (Singleton<Manager.Map>.IsInstance())
			{
				Singleton<Manager.Map>.Instance.ReleaseComponents();
			}
			if (Singleton<Manager.Resources>.IsInstance())
			{
				Singleton<Manager.Resources>.Instance.ReleaseMapResources();
				Singleton<Manager.Resources>.Instance.EndLoadAssetBundle(true);
			}
			if (Singleton<SoundPlayer>.IsInstance())
			{
				Singleton<SoundPlayer>.Instance.Release();
			}
			if (Singleton<AnimalManager>.IsInstance())
			{
				Singleton<AnimalManager>.Instance.ActiveMapScene = false;
			}
		}

		// Token: 0x06007ECC RID: 32460 RVA: 0x0035F3D3 File Offset: 0x0035D7D3
		public void PushResult(int id, string message)
		{
		}

		// Token: 0x0400662B RID: 26155
		[Header("Root")]
		[SerializeField]
		[FormerlySerializedAs("Map")]
		[Indent(1)]
		private Transform _mapRoot;

		// Token: 0x0400662C RID: 26156
		[SerializeField]
		[FormerlySerializedAs("Entity")]
		[Indent(1)]
		private Transform _actorRoot;

		// Token: 0x0400662D RID: 26157
		[SerializeField]
		private NavMeshSurface _navMeshSurface;

		// Token: 0x0400662E RID: 26158
		[SerializeField]
		private Transform _startPositionPoint;

		// Token: 0x0400662F RID: 26159
		[SerializeField]
		private Transform _warpPoint;

		// Token: 0x04006630 RID: 26160
		private List<AIProject.UI.ICommandData> _systemCommands = new List<AIProject.UI.ICommandData>();

		// Token: 0x04006633 RID: 26163
		private bool _enabledCommandArea;

		// Token: 0x04006634 RID: 26164
		private bool[] _charasEntry;
	}
}
