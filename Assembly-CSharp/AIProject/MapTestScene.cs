using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AIProject.Definitions;
using AIProject.SaveData;
using AIProject.UI;
using Manager;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;
using UnityEx.Misc;

namespace AIProject
{
	// Token: 0x020011C1 RID: 4545
	public class MapTestScene : MonoBehaviour, ISystemCommand
	{
		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x0600950B RID: 38155 RVA: 0x003D70A1 File Offset: 0x003D54A1
		// (set) Token: 0x0600950C RID: 38156 RVA: 0x003D70A9 File Offset: 0x003D54A9
		public bool EnabledInput { get; set; }

		// Token: 0x0600950D RID: 38157 RVA: 0x003D70B4 File Offset: 0x003D54B4
		public void OnUpdateInput()
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			foreach (ICommandData commandData in this._systemCommands)
			{
				commandData.Invoke(instance);
			}
		}

		// Token: 0x0600950E RID: 38158 RVA: 0x003D7118 File Offset: 0x003D5518
		private void Awake()
		{
		}

		// Token: 0x0600950F RID: 38159 RVA: 0x003D711C File Offset: 0x003D551C
		private IEnumerator Start()
		{
			this._mapRoot.SetActive(true);
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			Singleton<Manager.Input>.Instance.SystemElements.Add(this);
			Singleton<Game>.Instance.IsDebug = true;
			Manager.Resources rsrc = Singleton<Manager.Resources>.Instance;
			rsrc.SetupMap();
			GameObject uiSource = CommonLib.LoadAsset<GameObject>(rsrc.DefinePack.ABPaths.MapScenePrefab, "MapUI", false, rsrc.DefinePack.ABManifests.Default);
			GameObject uiInstance = UnityEngine.Object.Instantiate<GameObject>(uiSource);
			uiInstance.transform.SetParent(base.transform, false);
			if (!Directory.Exists(AIProject.Definitions.Path.SaveDataDirectory))
			{
				Directory.CreateDirectory(AIProject.Definitions.Path.SaveDataDirectory);
			}
			string path = AIProject.Definitions.Path.WorldSaveDataFile;
			Singleton<Game>.Instance.LoadProfile(path);
			yield return Singleton<Manager.Resources>.Instance.LoadMapResourceStream.ToYieldInstruction<Unit>();
			Manager.Map mapManager = Singleton<Manager.Map>.Instance;
			yield return mapManager.LoadMap(this._sceneAssetBundleName, this._sceneName);
			yield return mapManager.LoadEnvironment();
			AssetBundleInfo assetBundleInfo = Singleton<Manager.Resources>.Instance.Map.ChunkList[0];
			GameObject original = CommonLib.LoadAsset<GameObject>(assetBundleInfo.assetbundle, assetBundleInfo.asset, false, assetBundleInfo.manifest);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			gameObject.transform.SetParent(this._mapRoot, false);
			GameObject chara = UnityEngine.Object.Instantiate<GameObject>(this._charaPrefab, this._charaRoot);
			PlayerActor player = chara.GetComponent<PlayerActor>();
			player.Position = Singleton<Manager.Map>.Instance.PlayerStartPoint.position;
			player.Rotation = Singleton<Manager.Map>.Instance.PlayerStartPoint.rotation;
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			if (worldData == null)
			{
				Singleton<Game>.Instance.WorldData = (worldData = Game.CreateInitData(0, false));
			}
			player.PlayerData = worldData.PlayerData;
			if (player.PlayerData.CharaFileNames.IsNullOrEmpty<string>())
			{
				player.PlayerData.CharaFileNames[0] = "charaF_20170613163526688";
				player.PlayerData.CharaFileNames[1] = "charaF_20170613163526688";
			}
			player.Relocate();
			chara.SetActiveSafe(true);
			AQUAS_Reflection[] reflections = mapManager.WaterRefrections;
			if (reflections != null)
			{
				foreach (AQUAS_Reflection aquas_Reflection in reflections)
				{
					aquas_Reflection.enabled = false;
				}
			}
			if (this._isTrial)
			{
				yield return player.LoadTrialAsync();
			}
			else
			{
				yield return player.LoadAsync();
			}
			Singleton<Manager.Map>.Instance.RegisterPlayer(player);
			Singleton<Manager.Map>.Instance.RegisterActor(-99, player);
			player.ActivateNavMeshAgent();
			yield return null;
			mapManager.Simulator.EnabledSky = true;
			mapManager.Simulator.EnviroSky.AssignAndStart(player.Controller.gameObject, player.CameraControl.CameraComponent);
			mapManager.Simulator.DefaultEnviroZone.enabled = true;
			yield return null;
			Manager.Input inputManager = Singleton<Manager.Input>.Instance;
			inputManager.ReserveState(Manager.Input.ValidType.Action);
			inputManager.SetupState();
			inputManager.ActionElements.Add(player.CameraControl);
			player.CameraControl.EnabledInput = true;
			Singleton<Manager.Input>.Instance.SystemElements.Add(this);
			inputManager.ActionElements.Add(MapUIContainer.CommandLabel);
			MapUIContainer.CommandLabel.EnabledInput = true;
			MapUIContainer.SetVisibleHUD(false);
			GC.Collect();
			FadeItem panel = MapUIContainer.FadeCanvas.GetPanel(FadeCanvas.PanelType.NowLoading);
			panel.CanvasGroup.alpha = 0f;
			KeyCodeDownCommand onInputF = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.F
			};
			onInputF.TriggerEvent.AddListener(delegate()
			{
				if (this._fpsText != null)
				{
					this._fpsText.enabled = !this._fpsText.enabled;
				}
			});
			this._systemCommands.Add(onInputF);
			KeyCodeDownCommand onInputV = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.V
			};
			onInputV.TriggerEvent.AddListener(delegate()
			{
				if (this._warpPoint == null)
				{
					return;
				}
				Vector3 position = this._warpPoint.position;
				NavMeshHit navMeshHit;
				if (NavMesh.SamplePosition(position, out navMeshHit, 20f, 1))
				{
					position = navMeshHit.position;
					Singleton<Manager.Map>.Instance.Player.NavMeshAgent.Warp(position);
				}
			});
			this._systemCommands.Add(onInputV);
			this.EnabledInput = true;
			if (this._playerStartPoint != null)
			{
				player.NavMeshAgent.Warp(this._playerStartPoint.position);
			}
			(from _ in Observable.EveryUpdate()
			select MapUIContainer.CommandList.IsActiveControl || MapUIContainer.ItemBoxUI.IsActiveControl || MapUIContainer.SystemMenuUI.IsActiveControl || Singleton<ADV>.Instance.Captions.Active || Singleton<MapUIContainer>.Instance.MinimapUI.AllAreaMap.activeSelf || (!Singleton<Scene>.Instance.AddSceneName.IsNullOrEmpty() && Singleton<Scene>.Instance.AddSceneName != "MapUI")).DistinctUntilChanged<bool>().Subscribe(delegate(bool isOn)
			{
				this.IsCursorLock = !isOn;
			});
			IObservable<bool> updateCursorStream = from _ in Observable.EveryUpdate()
			select this.IsCursorLock;
			(from isOn in updateCursorStream.DistinctUntilChanged<bool>()
			where !isOn
			select isOn).Subscribe(delegate(bool IsOnOffMeshLink)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			});
			(from isOn in updateCursorStream
			where isOn
			select isOn).Subscribe(delegate(bool _)
			{
				Cursor.lockState = CursorLockMode.Locked;
			}, delegate()
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			});
			yield break;
		}

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x06009510 RID: 38160 RVA: 0x003D7137 File Offset: 0x003D5537
		// (set) Token: 0x06009511 RID: 38161 RVA: 0x003D713F File Offset: 0x003D553F
		public bool IsCursorLock { get; set; }

		// Token: 0x040077BE RID: 30654
		[SerializeField]
		private Transform _mapRoot;

		// Token: 0x040077BF RID: 30655
		[SerializeField]
		private Transform _charaRoot;

		// Token: 0x040077C0 RID: 30656
		[SerializeField]
		private GameObject _charaPrefab;

		// Token: 0x040077C1 RID: 30657
		[SerializeField]
		private Transform _playerStartPoint;

		// Token: 0x040077C2 RID: 30658
		[SerializeField]
		private Transform _warpPoint;

		// Token: 0x040077C3 RID: 30659
		[SerializeField]
		private string _sceneAssetBundleName = string.Empty;

		// Token: 0x040077C4 RID: 30660
		[SerializeField]
		private string _sceneName = string.Empty;

		// Token: 0x040077C5 RID: 30661
		[SerializeField]
		private bool _isTrial;

		// Token: 0x040077C6 RID: 30662
		private TextMeshProUGUI _fpsText;

		// Token: 0x040077C7 RID: 30663
		private List<ICommandData> _systemCommands = new List<ICommandData>();
	}
}
