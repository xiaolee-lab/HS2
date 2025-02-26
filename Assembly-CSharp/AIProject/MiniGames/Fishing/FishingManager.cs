using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.Player;
using AIProject.Scene;
using AIProject.UI;
using LuxWater;
using Manager;
using Rewired;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.MiniGames.Fishing
{
	// Token: 0x02000F2B RID: 3883
	public class FishingManager : MonoBehaviour
	{
		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x06007FF1 RID: 32753 RVA: 0x00364022 File Offset: 0x00362422
		public GameObject RootObject
		{
			[CompilerGenerated]
			get
			{
				return this.rootObject;
			}
		}

		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x06007FF2 RID: 32754 RVA: 0x0036402A File Offset: 0x0036242A
		public Transform SoundRoot
		{
			[CompilerGenerated]
			get
			{
				return this.soundRoot;
			}
		}

		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x06007FF3 RID: 32755 RVA: 0x00364032 File Offset: 0x00362432
		public Transform HitMoveArea
		{
			[CompilerGenerated]
			get
			{
				return this.hitMoveArea;
			}
		}

		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x06007FF4 RID: 32756 RVA: 0x0036403A File Offset: 0x0036243A
		public BoxCollider WaterBox
		{
			[CompilerGenerated]
			get
			{
				return this.waterBox;
			}
		}

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x06007FF5 RID: 32757 RVA: 0x00364042 File Offset: 0x00362442
		// (set) Token: 0x06007FF6 RID: 32758 RVA: 0x0036404C File Offset: 0x0036244C
		public FishingManager.FishingScene scene
		{
			get
			{
				return this.scene_;
			}
			set
			{
				FishingManager.FishingScene prev = this.scene_;
				this.scene_ = value;
				this.ChangeScene(this.scene_, prev);
				MapUIContainer.FishingUI.ChangeFishScene(this.scene_);
			}
		}

		// Token: 0x06007FF7 RID: 32759 RVA: 0x00364084 File Offset: 0x00362484
		private void ChangeScene(FishingManager.FishingScene _scene, FishingManager.FishingScene _prev)
		{
			if (_prev == FishingManager.FishingScene.WaitHit)
			{
				if (this.createFishDisposable != null)
				{
					this.createFishDisposable.Dispose();
				}
				this.createFishDisposable = null;
			}
			if (_scene != FishingManager.FishingScene.WaitHit)
			{
				if (_scene == FishingManager.FishingScene.Fishing)
				{
					this.prevInputAxis = (this.prevMouseAxis = Vector2.zero);
				}
			}
			else
			{
				Vector3 eulerAngles = this.playerFishing.player.CameraControl.CameraComponent.transform.eulerAngles;
				eulerAngles.x = (eulerAngles.z = 0f);
				this.moveArea.transform.eulerAngles = eulerAngles;
				this.hitMoveArea.localPosition = Singleton<Manager.Resources>.Instance.FishingDefinePack.FishParam.HitParam.MoveAreaOffsetPosition;
			}
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x06007FF8 RID: 32760 RVA: 0x0036415D File Offset: 0x0036255D
		public GameObject MoveArea
		{
			[CompilerGenerated]
			get
			{
				return this.moveArea;
			}
		}

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x06007FF9 RID: 32761 RVA: 0x00364168 File Offset: 0x00362568
		private GameObject FishPrefab
		{
			get
			{
				if (this._fishPrefab != null)
				{
					return this._fishPrefab;
				}
				string prefabsBundleDirectory = Singleton<Manager.Resources>.Instance.AnimalDefinePack.AssetBundleNames.PrefabsBundleDirectory;
				this._fishPrefab = AssetUtility.LoadAsset<GameObject>(prefabsBundleDirectory, "fishing_fish", "abdata");
				return this._fishPrefab;
			}
		}

		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x06007FFA RID: 32762 RVA: 0x003641BE File Offset: 0x003625BE
		// (set) Token: 0x06007FFB RID: 32763 RVA: 0x003641C6 File Offset: 0x003625C6
		public Vector2 ArrowPowerVector { get; private set; } = Vector2.up;

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x06007FFC RID: 32764 RVA: 0x003641CF File Offset: 0x003625CF
		public Vector3 AxisArrowOriginScale
		{
			[CompilerGenerated]
			get
			{
				return this.axisArrowOriginScale_;
			}
		}

		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x06007FFD RID: 32765 RVA: 0x003641D7 File Offset: 0x003625D7
		// (set) Token: 0x06007FFE RID: 32766 RVA: 0x003641DF File Offset: 0x003625DF
		public float MaxDamage { get; private set; }

		// Token: 0x06007FFF RID: 32767 RVA: 0x003641E8 File Offset: 0x003625E8
		public Color RBG(float r, float g, float b)
		{
			return new Color(r / 255f, g / 255f, b / 255f);
		}

		// Token: 0x06008000 RID: 32768 RVA: 0x00364204 File Offset: 0x00362604
		public Color RBGA(float r, float g, float b, float a)
		{
			return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x06008001 RID: 32769 RVA: 0x00364228 File Offset: 0x00362628
		public bool InputCancel
		{
			get
			{
				if (!Singleton<Manager.Input>.IsInstance())
				{
					return true;
				}
				if (!MapUIContainer.FishingUI.FocusInOn)
				{
					return true;
				}
				bool? flag = (this.playerFishing != null) ? new bool?(this.playerFishing.player.CameraControl.CinemachineBrain.IsBlending) : null;
				return flag != null && flag.Value;
			}
		}

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x06008002 RID: 32770 RVA: 0x003642AC File Offset: 0x003626AC
		public bool NextButtonDown
		{
			get
			{
				if (this.InputCancel)
				{
					return false;
				}
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				return instance.IsPressedSubmit() || instance.IsPressedKey(ActionID.MouseLeft);
			}
		}

		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x06008003 RID: 32771 RVA: 0x003642E4 File Offset: 0x003626E4
		public bool BackButtonDown
		{
			get
			{
				if (this.InputCancel)
				{
					return false;
				}
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				return instance.IsPressedCancel() || instance.IsPressedKey(ActionID.MouseRight);
			}
		}

		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x06008004 RID: 32772 RVA: 0x0036431C File Offset: 0x0036271C
		public bool IsPressedVertical
		{
			get
			{
				if (this.InputCancel)
				{
					return false;
				}
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				return instance.IsPressedVertical();
			}
		}

		// Token: 0x06008005 RID: 32773 RVA: 0x00364344 File Offset: 0x00362744
		public bool IsPressedAxis(ActionID _actionID)
		{
			if (this.InputCancel)
			{
				return false;
			}
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			return instance.IsPressedAxis(_actionID);
		}

		// Token: 0x06008006 RID: 32774 RVA: 0x0036436C File Offset: 0x0036276C
		public float GetAxis(ActionID _actionID)
		{
			if (this.InputCancel)
			{
				return 0f;
			}
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			return instance.GetAxis(_actionID);
		}

		// Token: 0x06008007 RID: 32775 RVA: 0x00364398 File Offset: 0x00362798
		public Vector2 GetLeftStickAxis()
		{
			if (this.InputCancel)
			{
				return Vector2.zero;
			}
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			return instance.LeftStickAxis;
		}

		// Token: 0x06008008 RID: 32776 RVA: 0x003643C4 File Offset: 0x003627C4
		public Vector2 GetMouseAxis()
		{
			if (this.InputCancel)
			{
				return Vector2.zero;
			}
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			return instance.MouseAxis;
		}

		// Token: 0x06008009 RID: 32777 RVA: 0x003643F0 File Offset: 0x003627F0
		public static bool GetWaterPosition(Vector3 _checkPos, out Vector3 _hitPosition)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				_hitPosition = _checkPos;
				return false;
			}
			_hitPosition = _checkPos;
			_checkPos += Vector3.up * 10f;
			Ray ray = new Ray(_checkPos, Vector3.down);
			LayerMask fishingLayerMask = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.FishingLayerMask;
			int num = Physics.RaycastNonAlloc(ray, FishingManager.raycastHits, 50f, fishingLayerMask);
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				RaycastHit raycastHit = FishingManager.raycastHits[i];
				GameObject gameObject;
				if (raycastHit.collider != null)
				{
					gameObject = raycastHit.collider.gameObject;
				}
				else
				{
					Transform transform = raycastHit.transform;
					gameObject = ((transform != null) ? transform.gameObject : null);
				}
				GameObject gameObject2 = gameObject;
				if (gameObject2 != null)
				{
					int num2 = 1 << gameObject2.layer;
					string tag = gameObject2.tag;
					if (num2 == fishingLayerMask && tag == Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.FishingMeshTagName)
					{
						if (!flag)
						{
							_hitPosition = raycastHit.point;
							flag = true;
						}
						else if (_hitPosition.y < raycastHit.point.y)
						{
							_hitPosition = raycastHit.point;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600800A RID: 32778 RVA: 0x00364564 File Offset: 0x00362964
		public static bool CheckOnWater(Vector3 _checkPos, out Vector3 _hitPoint)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				_hitPoint = _checkPos;
				return false;
			}
			_hitPoint = _checkPos;
			_checkPos += Vector3.up * 10f;
			Ray ray = new Ray(_checkPos, Vector3.down);
			LayerMask fishingLayerMask = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.FishingLayerMask;
			int num = Physics.RaycastNonAlloc(ray, FishingManager.raycastHits, 50f, fishingLayerMask);
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				RaycastHit raycastHit = FishingManager.raycastHits[i];
				GameObject gameObject;
				if (raycastHit.collider != null)
				{
					gameObject = raycastHit.collider.gameObject;
				}
				else
				{
					Transform transform = raycastHit.transform;
					gameObject = ((transform != null) ? transform.gameObject : null);
				}
				GameObject gameObject2 = gameObject;
				if (gameObject2 != null)
				{
					int num2 = 1 << gameObject2.layer;
					string tag = gameObject2.tag;
					if (num2 == fishingLayerMask && tag == Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.FishingMeshTagName)
					{
						if (!flag)
						{
							_hitPoint = raycastHit.point;
							flag = true;
						}
						else if (_hitPoint.y < raycastHit.point.y)
						{
							_hitPoint = raycastHit.point;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600800B RID: 32779 RVA: 0x003646D8 File Offset: 0x00362AD8
		public static bool CheckOnWater(Vector3 _checkPos, ref Collider _collider)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return false;
			}
			_checkPos += Vector3.up * 10f;
			Ray ray = new Ray(_checkPos, Vector3.down);
			LayerMask fishingLayerMask = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.FishingLayerMask;
			int num = Physics.RaycastNonAlloc(ray, FishingManager.raycastHits, 50f, fishingLayerMask);
			bool flag = false;
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < num; i++)
			{
				RaycastHit raycastHit = FishingManager.raycastHits[i];
				GameObject gameObject;
				if (raycastHit.collider != null)
				{
					gameObject = raycastHit.collider.gameObject;
				}
				else
				{
					Transform transform = raycastHit.transform;
					gameObject = ((transform != null) ? transform.gameObject : null);
				}
				GameObject gameObject2 = gameObject;
				if (gameObject2 != null)
				{
					int num2 = 1 << gameObject2.layer;
					string tag = gameObject2.tag;
					if (num2 == fishingLayerMask && tag == Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.FishingMeshTagName)
					{
						if (!flag)
						{
							vector = raycastHit.point;
							_collider = raycastHit.collider;
							flag = true;
						}
						else if (vector.y < raycastHit.point.y)
						{
							vector = raycastHit.point;
							_collider = raycastHit.collider;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600800C RID: 32780 RVA: 0x00364850 File Offset: 0x00362C50
		public static bool CheckOnWater(Vector3 _checkPos)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return false;
			}
			_checkPos += Vector3.up * 10f;
			Ray ray = new Ray(_checkPos, Vector3.down);
			LayerMask fishingLayerMask = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.FishingLayerMask;
			int num = Physics.RaycastNonAlloc(ray, FishingManager.raycastHits, 50f, fishingLayerMask);
			bool flag = false;
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < num; i++)
			{
				RaycastHit raycastHit = FishingManager.raycastHits[i];
				GameObject gameObject;
				if (raycastHit.collider != null)
				{
					gameObject = raycastHit.collider.gameObject;
				}
				else
				{
					Transform transform = raycastHit.transform;
					gameObject = ((transform != null) ? transform.gameObject : null);
				}
				GameObject gameObject2 = gameObject;
				if (gameObject2 != null)
				{
					int num2 = 1 << gameObject2.layer;
					string tag = gameObject2.tag;
					if (num2 == fishingLayerMask && tag == Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.FishingMeshTagName)
					{
						if (!flag)
						{
							vector = raycastHit.point;
							flag = true;
						}
						else if (vector.y < raycastHit.point.y)
						{
							vector = raycastHit.point;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600800D RID: 32781 RVA: 0x003649B4 File Offset: 0x00362DB4
		public static Material GetLuxWaterMaterial(Vector3 _checkPos)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return null;
			}
			_checkPos += Vector3.up * 10f;
			Ray ray = new Ray(_checkPos, Vector3.down);
			LayerMask luxWaterLayerMask = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.LuxWaterLayerMask;
			int num = Physics.RaycastNonAlloc(ray, FishingManager.raycastHits, 50f, luxWaterLayerMask);
			for (int i = 0; i < num; i++)
			{
				RaycastHit raycastHit = FishingManager.raycastHits[i];
				GameObject gameObject;
				if (raycastHit.collider != null)
				{
					gameObject = raycastHit.collider.gameObject;
				}
				else
				{
					Transform transform = raycastHit.transform;
					gameObject = ((transform != null) ? transform.gameObject : null);
				}
				GameObject gameObject2 = gameObject;
				if (gameObject2 != null)
				{
					LuxWater_WaterVolume component = gameObject2.GetComponent<LuxWater_WaterVolume>();
					Renderer renderer = (component != null) ? component.GetComponent<Renderer>() : null;
					if (renderer != null)
					{
						return renderer.material;
					}
				}
			}
			return null;
		}

		// Token: 0x0600800E RID: 32782 RVA: 0x00364ABC File Offset: 0x00362EBC
		public bool CheckOnMoveAreaInWorld(Vector3 _checkPos)
		{
			float moveAreaRadius = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.MoveAreaRadius;
			return (_checkPos - this.moveArea.transform.position).sqrMagnitude <= moveAreaRadius * moveAreaRadius;
		}

		// Token: 0x0600800F RID: 32783 RVA: 0x00364B04 File Offset: 0x00362F04
		public bool CheckOnMoveAreaInLocal(Vector3 _checkPos)
		{
			float moveAreaRadius = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.MoveAreaRadius;
			return _checkPos.sqrMagnitude <= moveAreaRadius * moveAreaRadius;
		}

		// Token: 0x06008010 RID: 32784 RVA: 0x00364B38 File Offset: 0x00362F38
		public Vector3 ClampMoveAreaInWorld(Vector3 _position)
		{
			if (!this.CheckOnMoveAreaInWorld(_position))
			{
				_position = Vector3.ClampMagnitude(_position - this.moveArea.transform.position, Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.MoveAreaRadius) + this.moveArea.transform.position;
			}
			return _position;
		}

		// Token: 0x06008011 RID: 32785 RVA: 0x00364B98 File Offset: 0x00362F98
		public Vector3 ClampMoveAreaInLocal(Vector3 _position)
		{
			if (!this.CheckOnMoveAreaInLocal(_position))
			{
				_position = Vector3.ClampMagnitude(_position, Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.MoveAreaRadius);
			}
			return _position;
		}

		// Token: 0x06008012 RID: 32786 RVA: 0x00364BC3 File Offset: 0x00362FC3
		private static void DebugLog(Vector3 _originPos, bool _hitWater, bool _hitGround, Vector3 _waterHitPoint, Vector3 _groundHitPoint)
		{
		}

		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x06008013 RID: 32787 RVA: 0x00364BC5 File Offset: 0x00362FC5
		// (set) Token: 0x06008014 RID: 32788 RVA: 0x00364BCD File Offset: 0x00362FCD
		public Vector3 moveAreaOriginLocalPosition { get; set; } = Vector3.zero;

		// Token: 0x06008015 RID: 32789 RVA: 0x00364BD8 File Offset: 0x00362FD8
		private void Awake()
		{
			Vector3? vector = (this.axisArrowObject != null) ? new Vector3?(this.axisArrowObject.localScale) : null;
			this.axisArrowOriginScale_ = ((vector == null) ? Vector3.one : vector.Value);
			Transform transform = this.hitMoveArea;
			Vector3 zero = Vector3.zero;
			this.hitMoveArea.localEulerAngles = zero;
			transform.localPosition = zero;
			this.moveAreaOriginLocalPosition = this.moveArea.transform.localPosition;
			Vector3 size = this.waterBox.size;
			Vector3 center = this.waterBox.center;
			size.x = (size.z = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.MoveAreaRadius * 4f);
			center.x = (center.z = 0f);
			center.y = -(size.y / 2f);
			this.waterBox.center = center;
			this.waterBox.size = size;
		}

		// Token: 0x06008016 RID: 32790 RVA: 0x00364CF0 File Offset: 0x003630F0
		private void OnEnable()
		{
			this.uiCamera.gameObject.SetActive(true);
			(from _ in Observable.EveryUpdate().TakeUntilDisable(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
			(from _ in Observable.EveryLateUpdate().TakeUntilDisable(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnLateUpdate();
			});
		}

		// Token: 0x06008017 RID: 32791 RVA: 0x00364D74 File Offset: 0x00363174
		private void OnDisable()
		{
			this.uiCamera.gameObject.SetActive(false);
			this.RemoveAllSE();
			this.RemoveAllParticle();
		}

		// Token: 0x06008018 RID: 32792 RVA: 0x00364D94 File Offset: 0x00363194
		public void Initialize(Fishing _playerFishing)
		{
			this.Release();
			if (this.playerFishing != _playerFishing)
			{
				this.playerFishing = _playerFishing;
			}
			this.scene = FishingManager.FishingScene.SelectFood;
			this.playerInfo.Set(_playerFishing);
			this.InitializeLineRenderer();
			this.lure.Initialize(this, this.playerInfo.lurePos.transform, this.fishingLine);
			Fish.FishHitEvent = (Action<Fish>)Delegate.Combine(Fish.FishHitEvent, new Action<Fish>(this.SceneToFishing));
			this.hitFish = null;
			this.InitializeCamera();
			this.InitializeSprite();
		}

		// Token: 0x06008019 RID: 32793 RVA: 0x00364E28 File Offset: 0x00363228
		private void SetEnable(Behaviour _be, bool _enable)
		{
			if (_be.enabled != _enable)
			{
				_be.enabled = _enable;
			}
		}

		// Token: 0x0600801A RID: 32794 RVA: 0x00364E3D File Offset: 0x0036323D
		private void SetActive(Component _c, bool _a)
		{
			if (_c.gameObject.activeSelf != _a)
			{
				_c.gameObject.SetActive(_a);
			}
		}

		// Token: 0x0600801B RID: 32795 RVA: 0x00364E5C File Offset: 0x0036325C
		private void SetActive(GameObject _g, bool _a)
		{
			if (_g.activeSelf != _a)
			{
				_g.SetActive(_a);
			}
		}

		// Token: 0x0600801C RID: 32796 RVA: 0x00364E74 File Offset: 0x00363274
		private void InitializeCamera()
		{
			Camera cameraComponent = this.playerFishing.player.CameraControl.CameraComponent;
			this.uiCamera.transform.position = cameraComponent.transform.position;
			this.uiCamera.transform.eulerAngles = cameraComponent.transform.eulerAngles;
			this.uiCamera.gameObject.SetActive(true);
			Camera camera = this.uiCamera;
			float fieldOfView = cameraComponent.fieldOfView;
			this.fishModelCamera.fieldOfView = fieldOfView;
			camera.fieldOfView = fieldOfView;
			this.uiCamera.depth = cameraComponent.depth + 1f;
			this.fishModelCamera.depth = this.uiCamera.depth + 1f;
		}

		// Token: 0x0600801D RID: 32797 RVA: 0x00364F30 File Offset: 0x00363330
		private void ChangeSpriteColor(int _mode)
		{
			if (this.outCircleSprite != null)
			{
				this.outCircleSprite.ChangeColor(_mode);
			}
			if (this.normalCircleSprite != null)
			{
				this.normalCircleSprite.ChangeColor(_mode);
			}
			if (this.criticalCircleSprite != null)
			{
				this.criticalCircleSprite.ChangeColor(_mode);
			}
		}

		// Token: 0x0600801E RID: 32798 RVA: 0x00364F88 File Offset: 0x00363388
		private void SetImageFillAmount(Image _image, float _angle)
		{
			if (_image == null)
			{
				return;
			}
			_image.fillAmount = _angle / 360f;
			Vector3 localEulerAngles = _image.transform.localEulerAngles;
			localEulerAngles.y = _angle / 2f;
			_image.transform.localEulerAngles = localEulerAngles;
		}

		// Token: 0x0600801F RID: 32799 RVA: 0x00364FD5 File Offset: 0x003633D5
		private void InitializeSprite()
		{
			this.spriteRootObject.gameObject.SetActive(false);
			this.spriteRootObject.localEulerAngles = Vector3.zero;
			this.ChangeSpriteColor(0);
		}

		// Token: 0x06008020 RID: 32800 RVA: 0x00365000 File Offset: 0x00363400
		private void InitializeLineRenderer()
		{
			this.fishingLine.startWidth = 0.02f;
			this.fishingLine.endWidth = 0.02f;
			LineRenderer lineRenderer = this.fishingLine;
			Color white = Color.white;
			this.fishingLine.endColor = white;
			lineRenderer.startColor = white;
			this.fishingLine.useWorldSpace = true;
			this.fishingLine.enabled = false;
		}

		// Token: 0x06008021 RID: 32801 RVA: 0x00365063 File Offset: 0x00363463
		public void Release()
		{
			this.FishAllDelete();
			this.RemoveAllSE();
			this.RemoveAllParticle();
			Lure.ThrowEndEvent = null;
			Fish.FishHitEvent = null;
		}

		// Token: 0x06008022 RID: 32802 RVA: 0x00365083 File Offset: 0x00363483
		private void SetPlayerAnimation(Fishing.PoseID _poseID)
		{
			if (this.playerFishing != null)
			{
				this.playerFishing.PlayAnimation(_poseID);
			}
		}

		// Token: 0x06008023 RID: 32803 RVA: 0x003650A0 File Offset: 0x003634A0
		private void OnUpdate()
		{
			this.NullCheckFishList();
			this.moveArea.transform.localPosition = this.moveAreaOriginLocalPosition + Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.MoveAreaOffsetPosition;
			Vector3 position = this.moveArea.transform.position;
			position.y = this.playerFishing.MoveAreaPosY;
			this.moveArea.transform.position = position;
			FishingManager.FishingScene scene = this.scene;
			if (scene != FishingManager.FishingScene.WaitHit)
			{
				if (scene == FishingManager.FishingScene.Fishing)
				{
					this.UpdateSprite();
					this.CheckArrowInCircle();
				}
			}
			else if (this.BackButtonDown)
			{
				this.SelectFishFoodScene();
			}
		}

		// Token: 0x06008024 RID: 32804 RVA: 0x00365157 File Offset: 0x00363557
		private void SetPositionAndAngle(Transform _t1, Transform _t2)
		{
			_t1.SetPositionAndRotation(_t2.position, _t2.rotation);
		}

		// Token: 0x06008025 RID: 32805 RVA: 0x0036516C File Offset: 0x0036356C
		private void SetCameraInfo(Camera _copy, Camera _source)
		{
			if (_copy == null || _source == null)
			{
				return;
			}
			Transform transform = _source.transform;
			_copy.transform.SetPositionAndRotation(transform.position, transform.rotation);
			_copy.fieldOfView = _source.fieldOfView;
		}

		// Token: 0x06008026 RID: 32806 RVA: 0x003651BC File Offset: 0x003635BC
		private void SetPositionAndAngleLocal(Transform _t1, Vector3 _localPosition, Quaternion _localRotation)
		{
			_t1.localPosition = _localPosition;
			_t1.localRotation = _localRotation;
		}

		// Token: 0x06008027 RID: 32807 RVA: 0x003651CC File Offset: 0x003635CC
		private void SetLocalAngleY(Transform _t, float _angleY)
		{
			Vector3 localEulerAngles = _t.localEulerAngles;
			localEulerAngles.y = _angleY;
			_t.localEulerAngles = localEulerAngles;
		}

		// Token: 0x06008028 RID: 32808 RVA: 0x003651F0 File Offset: 0x003635F0
		private void SetLocalAngleZ(Transform _t, float _angleZ)
		{
			Vector3 localEulerAngles = _t.localEulerAngles;
			localEulerAngles.z = _angleZ;
			_t.localEulerAngles = localEulerAngles;
		}

		// Token: 0x06008029 RID: 32809 RVA: 0x00365214 File Offset: 0x00363614
		private void OnLateUpdate()
		{
			Camera cameraComponent = Manager.Map.GetCameraComponent();
			Transform transform = (!(cameraComponent != null)) ? null : cameraComponent.transform;
			FishingManager.FishingScene scene = this.scene;
			if (scene == FishingManager.FishingScene.Fishing)
			{
				this.SetCameraInfo(this.uiCamera, cameraComponent);
				this.SetPositionAndAngleLocal(this.spriteRootObject, this.hitFish.transform.localPosition, Quaternion.identity);
				this.circleRootObject.localEulerAngles = this.hitFish.transform.localEulerAngles;
				this.SetArrowAngle(this.ArrowPowerVector);
				Vector3 vector = this.spriteRootObject.position - transform.position;
				Vector3 position = transform.position + vector.normalized * Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.DistanceToCircle;
				this.spriteRootObject.position = position;
			}
			this.soundRoot.position = transform.forward * Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.SoundRoodDistance + transform.position;
			foreach (KeyValuePair<SEType, Dictionary<int, AudioSource>> keyValuePair in this.SETable)
			{
				foreach (KeyValuePair<int, AudioSource> keyValuePair2 in keyValuePair.Value)
				{
					AudioSource value = keyValuePair2.Value;
					if (value)
					{
						value.transform.SetPositionAndRotation(this.soundRoot.position, this.soundRoot.rotation);
					}
				}
			}
		}

		// Token: 0x0600802A RID: 32810 RVA: 0x003653FC File Offset: 0x003637FC
		public void ChangeFishScene(FishingManager.FishingScene _scene)
		{
			switch (this.scene)
			{
			case FishingManager.FishingScene.StartMotion:
			case FishingManager.FishingScene.WaitHit:
			case FishingManager.FishingScene.Fishing:
			case FishingManager.FishingScene.Failure:
				return;
			}
			if (_scene != FishingManager.FishingScene.StartMotion)
			{
				if (_scene == FishingManager.FishingScene.SelectFood)
				{
					this.scene = _scene;
				}
			}
			else
			{
				this.scene = _scene;
				base.StartCoroutine(this.StartThrow());
			}
		}

		// Token: 0x0600802B RID: 32811 RVA: 0x0036546C File Offset: 0x0036386C
		private IEnumerator StartThrow()
		{
			yield return null;
			this.SetPlayerAnimation(Fishing.PoseID.Idle);
			while (!this.playerFishing.LastAnimation)
			{
				yield return null;
			}
			this.InitializeCamera();
			Vector3 _dropPosition = this.lure.transform.position;
			this.SceneToWaitHit();
			this.lure.transform.position = _dropPosition;
			yield break;
		}

		// Token: 0x0600802C RID: 32812 RVA: 0x00365488 File Offset: 0x00363888
		private void NullCheckFishList()
		{
			for (int i = 0; i < this.fishList.Count; i++)
			{
				if (this.fishList[i] == null)
				{
					this.fishList.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x0600802D RID: 32813 RVA: 0x003654D8 File Offset: 0x003638D8
		private void CreateFish()
		{
			if (this.createFishDisposable == null)
			{
				IEnumerator _coroutine = this.CreateFishCoroutine();
				this.createFishDisposable = Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
			}
		}

		// Token: 0x0600802E RID: 32814 RVA: 0x00365520 File Offset: 0x00363920
		public Vector3 GetRandomPosOnMoveArea()
		{
			float f = UnityEngine.Random.value * 3.1415927f * 2f;
			float num = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.MoveAreaRadius * Mathf.Sqrt(UnityEngine.Random.value);
			return new Vector3(num * Mathf.Cos(f), 0f, num * Mathf.Sin(f));
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x0600802F RID: 32815 RVA: 0x00365579 File Offset: 0x00363979
		public bool CreateFishEnable
		{
			[CompilerGenerated]
			get
			{
				return this.fishList.Count < Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.FishMaxNum && this.scene == FishingManager.FishingScene.WaitHit;
			}
		}

		// Token: 0x06008030 RID: 32816 RVA: 0x003655AC File Offset: 0x003639AC
		private IEnumerator CreateFishCoroutine()
		{
			yield return null;
			Vector3 _createPos = Vector3.zero;
			int i = 1;
			while (this.CreateFishEnable)
			{
				_createPos = this.GetRandomPosOnMoveArea();
				_createPos = this.moveArea.transform.TransformPoint(_createPos);
				if (FishingManager.CheckOnWater(_createPos))
				{
					_createPos = this.moveArea.transform.InverseTransformPoint(_createPos);
					break;
				}
				if (5 <= i)
				{
					i = 0;
					yield return null;
				}
				i++;
			}
			if (!this.CreateFishEnable)
			{
				this.createFishDisposable = null;
				yield break;
			}
			Manager.Resources.FishingTable FishingInfo = Singleton<Manager.Resources>.Instance.Fishing;
			FishingUI FishingUI = MapUIContainer.FishingUI;
			FishFoodInfo selectFood = FishingUI.SelectedFishFood;
			if (selectFood == null || selectFood.ItemID < 0)
			{
				this.createFishDisposable = null;
				yield break;
			}
			int _foodID = selectFood.ItemID;
			UnityEx.ValueTuple<int, int, int> _fishInfo;
			if (FishingInfo.TryGetBaitHitInfo(_foodID, out _fishInfo))
			{
				this.CreateFishPattern(_fishInfo, _createPos);
			}
			this.createFishDisposable = null;
			yield break;
		}

		// Token: 0x06008031 RID: 32817 RVA: 0x003655C8 File Offset: 0x003639C8
		private void CreateFishPattern(UnityEx.ValueTuple<int, int, int> _fishInfoValue, Vector3 _createPos)
		{
			Manager.Resources.FishingTable fishing = Singleton<Manager.Resources>.Instance.Fishing;
			Dictionary<int, Dictionary<int, FishInfo>> fishInfoTable = fishing.FishInfoTable;
			if (fishInfoTable.IsNullOrEmpty<int, Dictionary<int, FishInfo>>())
			{
				return;
			}
			Dictionary<int, FishInfo> dictionary;
			if (!fishInfoTable.TryGetValue(_fishInfoValue.Item2, out dictionary) || fishInfoTable.IsNullOrEmpty<int, Dictionary<int, FishInfo>>())
			{
				return;
			}
			FishInfo fishInfo;
			if (dictionary.TryGetValue(_fishInfoValue.Item3, out fishInfo))
			{
				this.CreateFish(fishInfo, _createPos);
			}
		}

		// Token: 0x06008032 RID: 32818 RVA: 0x00365630 File Offset: 0x00363A30
		private void CreateFish(FishInfo _fishInfo, Vector3 _createPos)
		{
			if (!_fishInfo.IsActive)
			{
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.FishPrefab, Vector3.zero, Quaternion.identity);
			gameObject.name = string.Format("Fish_{0:000}", this.fishCreateNumber++);
			Fish component = gameObject.GetComponent<Fish>();
			component.lure = this.lure;
			component.Initialize(this, _fishInfo);
			component.transform.parent = this.moveArea.transform;
			_createPos.y = 0f;
			component.transform.localPosition = _createPos;
			component.transform.localEulerAngles = new Vector3(0f, 360f * UnityEngine.Random.value, 0f);
			component.transform.localScale = Vector3.zero;
			component.startFadePosition = (component.endFadePosition = component.transform.localPosition);
			Fish fish = component;
			fish.startFadePosition.y = fish.startFadePosition.y - 5f;
			component.transform.localPosition = component.startFadePosition;
			this.fishList.Add(component);
		}

		// Token: 0x06008033 RID: 32819 RVA: 0x00365754 File Offset: 0x00363B54
		private IEnumerator AddFish()
		{
			for (;;)
			{
				FishingManager.FishingScene scene = this.scene;
				if (scene != FishingManager.FishingScene.StartMotion)
				{
					if (scene != FishingManager.FishingScene.WaitHit)
					{
						break;
					}
					if (this.CreateFishEnable)
					{
						yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 10f));
						if (this.scene != FishingManager.FishingScene.WaitHit)
						{
							goto Block_3;
						}
						this.CreateFish();
					}
					yield return null;
				}
				else
				{
					yield return null;
				}
			}
			yield break;
			Block_3:
			yield break;
			yield break;
		}

		// Token: 0x06008034 RID: 32820 RVA: 0x00365770 File Offset: 0x00363B70
		private void FishAllDelete()
		{
			foreach (Fish fish in this.fishList)
			{
				if (fish != null && fish.gameObject != null)
				{
					UnityEngine.Object.Destroy(fish.gameObject);
				}
			}
			this.fishList.Clear();
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x003657F8 File Offset: 0x00363BF8
		private void SceneToWaitHit()
		{
			this.scene = FishingManager.FishingScene.WaitHit;
			this.lure.state = Lure.State.Float;
			this.CreateFish();
			base.StartCoroutine(this.AddFish());
		}

		// Token: 0x06008036 RID: 32822 RVA: 0x00365820 File Offset: 0x00363C20
		public Vector2 SetArrowAngle(Vector2 _powerVector)
		{
			float arrowMaxPower = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.ArrowMaxPower;
			_powerVector = Vector2.ClampMagnitude(_powerVector, arrowMaxPower);
			this.SetArrowLocalAngleY(this.axisArrowObject, _powerVector);
			return _powerVector;
		}

		// Token: 0x06008037 RID: 32823 RVA: 0x0036585C File Offset: 0x00363C5C
		private void SetArrowLocalAngleY(Transform _arrow, Vector2 _powerVector)
		{
			if (_powerVector.magnitude == 0f)
			{
				return;
			}
			Vector3 localEulerAngles = _arrow.localEulerAngles;
			Vector2 to = Quaternion.Euler(0f, 0f, -localEulerAngles.y) * (Vector2.up * -1f);
			float f = Vector2.SignedAngle(_powerVector.normalized, to);
			float num = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.ArrowAddAngle * Time.deltaTime;
			if (Mathf.Abs(f) < num)
			{
				num = Mathf.Abs(f);
			}
			num *= Mathf.Sign(f);
			localEulerAngles.y += num;
			_arrow.localRotation = Quaternion.Euler(localEulerAngles);
		}

		// Token: 0x06008038 RID: 32824 RVA: 0x00365918 File Offset: 0x00363D18
		private Vector2 SetArrowAxis(Vector2 _axis)
		{
			if (this.playerFishing == null)
			{
				return Vector2.zero;
			}
			Transform transform = this.playerFishing.player.CameraControl.CameraComponent.transform;
			Transform transform2 = this.axisArrowObject;
			Vector3 position = transform.position;
			Vector3 position2 = transform2.position;
			float z = Vector3.SignedAngle(transform.forward, position2 - position, Vector3.up) * -0.3f;
			return Quaternion.Euler(0f, 0f, z) * _axis;
		}

		// Token: 0x06008039 RID: 32825 RVA: 0x003659A8 File Offset: 0x00363DA8
		private void SceneToFishing(Fish _hitFish)
		{
			this.scene = FishingManager.FishingScene.Fishing;
			this.hitFish = _hitFish;
			int level = Singleton<Manager.Map>.Instance.Player.PlayerData.FishingSkill.Level;
			FishingDefinePack.SystemParamGroup systemParam = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam;
			float t = Mathf.InverseLerp(1f, (float)systemParam.MaxLevel, (float)level);
			this.MaxDamage = Mathf.Lerp(systemParam.DefaultDamage, systemParam.MaxDamage, t);
			this.PlaySE(SEType.FishEatFood, base.transform, false, 0f);
			this.ArrowPowerVector = this.SetArrowAxis(Vector2.up * Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.ArrowMaxPower);
			MapUIContainer.FishingUI.UseFishFood();
			this.SetPlayerAnimation(Fishing.PoseID.Hit);
			this.fishHeartPoint = this.hitFish.HeartPoint;
			this.lure.state = Lure.State.Hit;
			this.lure.HitFish = _hitFish;
			this.spriteRootObject.localPosition = this.hitFish.transform.localPosition;
			this.spriteRootObject.localEulerAngles = Vector3.zero;
			this.spriteRootObject.gameObject.SetActive(true);
			this.fishingTimeCounter = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.FishingTimeLimit;
			this.prevWarningMode = false;
			this.ChangeSpriteColor(0);
			foreach (Fish fish in this.fishList)
			{
				if (fish != null && _hitFish != fish)
				{
					fish.ChangeState(Fish.State.Escape);
				}
			}
		}

		// Token: 0x0600803A RID: 32826 RVA: 0x00365B5C File Offset: 0x00363F5C
		private void SceneToSuccess()
		{
			if (this.FishGetEvent != null)
			{
				this.FishGetEvent();
			}
			this.SetPlayerAnimation(Fishing.PoseID.Success);
			FishInfo info = this.hitFish.Get();
			this.SceneToResult();
			MapUIContainer.FishingUI.StartDrawResult(info);
		}

		// Token: 0x0600803B RID: 32827 RVA: 0x00365BA8 File Offset: 0x00363FA8
		private void SceneToFailure()
		{
			this.hitFish.ChangeState(Fish.State.HitToEscape);
			this.fishList.Remove(this.hitFish);
			this.SceneToResult();
			FishingUI fishingUI = MapUIContainer.FishingUI;
			if (!fishingUI.HaveSomeFishFood())
			{
				this.playerFishing.MissAnimationEndEvent = delegate()
				{
					MapUIContainer.PushWarningMessage(Popup.Warning.Type.NonFishFood);
					this.playerFishing.MissAnimationEndEvent = null;
				};
				this.playerFishing.PlayMissEndAnimation();
				this.scene = FishingManager.FishingScene.Finish;
			}
			else if (fishingUI.FishFoodNum <= 0)
			{
				this.SelectFishFoodScene();
			}
			else
			{
				base.StartCoroutine(this.ReplayFishing());
			}
		}

		// Token: 0x0600803C RID: 32828 RVA: 0x00365C40 File Offset: 0x00364040
		private IEnumerator ReplayFishing()
		{
			this.SetPlayerAnimation(Fishing.PoseID.MissToIdle);
			while (!this.playerFishing.LastAnimation)
			{
				yield return null;
			}
			this.InitializeSprite();
			this.SceneToWaitHit();
			yield break;
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x00365C5B File Offset: 0x0036405B
		private void SceneToResult()
		{
			this.spriteRootObject.gameObject.SetActive(false);
			this.lure.state = Lure.State.None;
			this.playerInfo.fishingRod.transform.localEulerAngles = Vector3.zero;
			this.hitFish = null;
		}

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x0600803E RID: 32830 RVA: 0x00365C9B File Offset: 0x0036409B
		private Player Player0
		{
			get
			{
				return ReInput.players.GetPlayer(0);
			}
		}

		// Token: 0x0600803F RID: 32831 RVA: 0x00365CA8 File Offset: 0x003640A8
		private void UpdateSprite()
		{
			FishingDefinePack.SystemParamGroup systemParam = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam;
			Vector2 a = this.GetLeftStickAxis() * systemParam.DeviceArrowPowerScale * Time.deltaTime;
			Vector2 a2 = this.GetMouseAxis() * systemParam.MouseArrowPowerScale * Time.deltaTime;
			Vector2 arrowAxis = (a + this.prevInputAxis) / 2f + (a2 + this.prevMouseAxis) / 2f;
			this.prevInputAxis = a;
			this.prevMouseAxis = a2;
			if (0f < arrowAxis.sqrMagnitude)
			{
				arrowAxis = this.SetArrowAxis(arrowAxis);
				this.ArrowPowerVector = Vector2.ClampMagnitude(this.ArrowPowerVector + this.SetArrowAxis(arrowAxis), systemParam.ArrowMaxPower);
			}
			if (this.prevWarningMode != this.hitFish.WarningMode)
			{
				int mode = (!this.hitFish.WarningMode) ? 0 : 1;
				this.ChangeSpriteColor(mode);
			}
			this.prevWarningMode = this.hitFish.WarningMode;
		}

		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x06008040 RID: 32832 RVA: 0x00365DC1 File Offset: 0x003641C1
		public float HeartPointScale
		{
			get
			{
				return (!(this.hitFish == null)) ? (this.fishHeartPoint / this.hitFish.HeartPoint) : 1f;
			}
		}

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x06008041 RID: 32833 RVA: 0x00365DF0 File Offset: 0x003641F0
		public float TimeScale
		{
			get
			{
				return this.fishingTimeCounter / Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam.FishingTimeLimit;
			}
		}

		// Token: 0x06008042 RID: 32834 RVA: 0x00365E10 File Offset: 0x00364210
		private void CheckArrowInCircle()
		{
			FishingDefinePack.SystemParamGroup systemParam = Singleton<Manager.Resources>.Instance.FishingDefinePack.SystemParam;
			float num = systemParam.NormalDamageAngle / 2f;
			bool flag = false;
			float num2 = Vector3.SignedAngle(this.hitFish.Forward, this.axisArrowObject.forward, Vector3.up);
			num2 = Mathf.Abs(num2);
			if (num2 <= num)
			{
				if (!this.hitFish.WarningMode)
				{
					float num3 = this.MaxDamage * 0.5f * Time.deltaTime;
					float num4 = Mathf.InverseLerp(num, 0f, num2) * num3;
					num4 += num3;
					this.fishHeartPoint -= num4;
				}
				else
				{
					flag = true;
				}
			}
			else if (this.hitFish.WarningMode)
			{
				float num5 = this.MaxDamage * systemParam.AngryDamageScale * Time.deltaTime;
				this.fishHeartPoint -= num5;
			}
			if (this.fishHeartPoint <= 0f)
			{
				this.fishHeartPoint = 0f;
				this.scene = FishingManager.FishingScene.Success;
				this.SceneToSuccess();
				return;
			}
			this.fishingTimeCounter -= (flag ? systemParam.AngryCountDownScale : 1f) * Time.deltaTime;
			if (this.fishingTimeCounter <= 0f)
			{
				this.fishingTimeCounter = 0f;
				this.scene = FishingManager.FishingScene.Failure;
				this.SceneToFailure();
				return;
			}
		}

		// Token: 0x06008043 RID: 32835 RVA: 0x00365F74 File Offset: 0x00364374
		public void SelectFishFoodScene()
		{
			switch (this.scene)
			{
			case FishingManager.FishingScene.WaitHit:
				this.scene = FishingManager.FishingScene.None;
				foreach (Fish fish in this.fishList)
				{
					if (fish != null)
					{
						fish.ChangeState(Fish.State.Escape);
					}
				}
				this.fishList.Clear();
				this.playerFishing.StopAnimationEndEvent = delegate()
				{
					this.scene = FishingManager.FishingScene.SelectFood;
					this.playerFishing.StopAnimationEndEvent = null;
				};
				this.playerFishing.PlayStopAnimation();
				break;
			case FishingManager.FishingScene.Success:
				this.scene = FishingManager.FishingScene.None;
				this.playerFishing.PlayStandbyMotion(true);
				this.scene = FishingManager.FishingScene.SelectFood;
				break;
			case FishingManager.FishingScene.Failure:
				this.scene = FishingManager.FishingScene.None;
				this.playerFishing.MissAnimationEndEvent = delegate()
				{
					this.scene = FishingManager.FishingScene.SelectFood;
					this.playerFishing.MissAnimationEndEvent = null;
				};
				this.playerFishing.PlayMissAnimation();
				break;
			}
		}

		// Token: 0x06008044 RID: 32836 RVA: 0x00366088 File Offset: 0x00364488
		public void EndFishing()
		{
			this.scene = FishingManager.FishingScene.Finish;
			this.Release();
			this.playerFishing.EndFishing();
		}

		// Token: 0x06008045 RID: 32837 RVA: 0x003660A2 File Offset: 0x003644A2
		private void OnDestroy()
		{
			this.Release();
		}

		// Token: 0x06008046 RID: 32838 RVA: 0x003660AA File Offset: 0x003644AA
		private float AngleAbs(float _angle)
		{
			if (_angle < 0f)
			{
				_angle = _angle % 360f + 360f;
			}
			else if (360f <= _angle)
			{
				_angle %= 360f;
			}
			return _angle;
		}

		// Token: 0x06008047 RID: 32839 RVA: 0x003660E0 File Offset: 0x003644E0
		private Vector3 AngleAbs(Vector3 _angle)
		{
			_angle.x = this.AngleAbs(_angle.x);
			_angle.y = this.AngleAbs(_angle.y);
			_angle.z = this.AngleAbs(_angle.z);
			return _angle;
		}

		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x06008048 RID: 32840 RVA: 0x00366120 File Offset: 0x00364520
		public Vector2 MouseAxis
		{
			get
			{
				if (!this.Player0.controllers.hasMouse)
				{
					return Vector2.zero;
				}
				Mouse mouse = this.Player0.controllers.Mouse;
				return new Vector2(mouse.GetAxis(0), mouse.GetAxis(1));
			}
		}

		// Token: 0x06008049 RID: 32841 RVA: 0x0036616C File Offset: 0x0036456C
		public void PlaySE(SEType _type, Transform _root, bool _loop, float _fadeTime = 0f)
		{
			int instanceID = _root.GetInstanceID();
			int key = 0;
			AudioSource audioSource = null;
			if (this.SETable.ContainsKey(_type) && this.SETable[_type].TryGetValue(instanceID, out audioSource) && audioSource != null)
			{
				audioSource.transform.SetPositionAndRotation(this.soundRoot.position, this.soundRoot.rotation);
				audioSource.Play();
			}
			else if (Singleton<Manager.Resources>.Instance.FishingDefinePack.SETable.TryGetValue(_type, out key) && (audioSource = Singleton<Manager.Resources>.Instance.SoundPack.Play(key, Sound.Type.GameSE3D, _fadeTime)) != null)
			{
				audioSource.transform.SetPositionAndRotation(this.soundRoot.position, this.soundRoot.rotation);
				audioSource.loop = _loop;
				if (!this.SETable.ContainsKey(_type))
				{
					this.SETable[_type] = new Dictionary<int, AudioSource>();
				}
				this.SETable[_type][instanceID] = audioSource;
			}
		}

		// Token: 0x0600804A RID: 32842 RVA: 0x00366280 File Offset: 0x00364680
		public void StopSE(SEType _type, Transform _root)
		{
			AudioSource audioSource = null;
			int instanceID = _root.GetInstanceID();
			if (this.SETable.ContainsKey(_type) && this.SETable[_type].TryGetValue(instanceID, out audioSource))
			{
				if (!audioSource)
				{
					this.SETable[_type].Remove(instanceID);
					if (this.SETable[_type].Count <= 0)
					{
						this.SETable.Remove(_type);
					}
				}
				else
				{
					audioSource.Stop();
				}
			}
		}

		// Token: 0x0600804B RID: 32843 RVA: 0x00366310 File Offset: 0x00364710
		public void StopAllSE()
		{
			List<Tuple<SEType, int>> list = ListPool<Tuple<SEType, int>>.Get();
			List<SEType> list2 = ListPool<SEType>.Get();
			foreach (KeyValuePair<SEType, Dictionary<int, AudioSource>> keyValuePair in this.SETable)
			{
				foreach (KeyValuePair<int, AudioSource> keyValuePair2 in keyValuePair.Value)
				{
					AudioSource value = keyValuePair2.Value;
					if (value)
					{
						value.Stop();
					}
					else
					{
						list.Add(new Tuple<SEType, int>(keyValuePair.Key, keyValuePair2.Key));
					}
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				Tuple<SEType, int> tuple = list[i];
				this.SETable[tuple.Item1].Remove(tuple.Item2);
				if (this.SETable[tuple.Item1].Count == 0)
				{
					list2.Add(tuple.Item1);
				}
			}
			for (int j = 0; j < list2.Count; j++)
			{
				this.SETable.Remove(list2[j]);
			}
			ListPool<Tuple<SEType, int>>.Release(list);
			ListPool<SEType>.Release(list2);
		}

		// Token: 0x0600804C RID: 32844 RVA: 0x0036649C File Offset: 0x0036489C
		public void RemoveSE(SEType _type, Transform _root)
		{
			AudioSource audioSource = null;
			int instanceID = _root.GetInstanceID();
			if (this.SETable.ContainsKey(_type) && this.SETable[_type].TryGetValue(instanceID, out audioSource))
			{
				if (audioSource)
				{
					UnityEngine.Object.Destroy(audioSource.gameObject);
				}
				this.SETable[_type].Remove(instanceID);
			}
		}

		// Token: 0x0600804D RID: 32845 RVA: 0x00366508 File Offset: 0x00364908
		public void RemoveAllSE()
		{
			foreach (KeyValuePair<SEType, Dictionary<int, AudioSource>> keyValuePair in this.SETable)
			{
				foreach (KeyValuePair<int, AudioSource> keyValuePair2 in keyValuePair.Value)
				{
					AudioSource value = keyValuePair2.Value;
					if (value)
					{
						UnityEngine.Object.Destroy(value.gameObject);
					}
				}
			}
			this.SETable.Clear();
		}

		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x0600804E RID: 32846 RVA: 0x003665D0 File Offset: 0x003649D0
		// (set) Token: 0x0600804F RID: 32847 RVA: 0x003665D8 File Offset: 0x003649D8
		private Dictionary<int, Dictionary<int, Tuple<ParticleSystem, Transform>>> PlayEffectTable { get; set; } = new Dictionary<int, Dictionary<int, Tuple<ParticleSystem, Transform>>>();

		// Token: 0x06008050 RID: 32848 RVA: 0x003665E4 File Offset: 0x003649E4
		public ParticleSystem PlayParticle(int _id, Transform _t)
		{
			if (_t == null)
			{
				return null;
			}
			int instanceID = _t.GetInstanceID();
			Dictionary<int, Tuple<ParticleSystem, Transform>> dictionary;
			Tuple<ParticleSystem, Transform> tuple;
			if (this.PlayEffectTable.TryGetValue(_id, out dictionary) && dictionary.TryGetValue(instanceID, out tuple) && ((tuple != null) ? tuple.Item1 : null) != null)
			{
				this.SetActive(tuple.Item1, true);
				tuple.Item1.Play(true);
				return tuple.Item1;
			}
			AssetBundleInfo assetBundleInfo;
			if (!Singleton<Manager.Resources>.Instance.Fishing.EffectTable.TryGetValue(_id, out assetBundleInfo))
			{
				return null;
			}
			GameObject gameObject = CommonLib.LoadAsset<GameObject>(assetBundleInfo.assetbundle, assetBundleInfo.asset, false, assetBundleInfo.manifest);
			if (gameObject == null)
			{
				return null;
			}
			MapScene.AddAssetBundlePath(assetBundleInfo.assetbundle, assetBundleInfo.manifest);
			ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
			if (component == null)
			{
				return null;
			}
			ParticleSystem particleSystem = UnityEngine.Object.Instantiate<ParticleSystem>(component, _t);
			particleSystem.transform.localPosition = Vector3.zero;
			this.SetActive(particleSystem, true);
			particleSystem.Play(true);
			if (!this.PlayEffectTable.ContainsKey(_id))
			{
				this.PlayEffectTable[_id] = new Dictionary<int, Tuple<ParticleSystem, Transform>>();
			}
			this.PlayEffectTable[_id][instanceID] = new Tuple<ParticleSystem, Transform>(particleSystem, _t);
			return particleSystem;
		}

		// Token: 0x06008051 RID: 32849 RVA: 0x00366740 File Offset: 0x00364B40
		public ParticleSystem PlayParticle(ParticleType _type, Transform _t)
		{
			return this.PlayParticle((int)_type, _t);
		}

		// Token: 0x06008052 RID: 32850 RVA: 0x0036674C File Offset: 0x00364B4C
		public void PlayDelayParticle(int _id, Transform _t, Func<bool> _enableFunc, float _delayTime)
		{
			IEnumerator _coroutine = this.PlayDelayParticleCoroutine(_id, _t, _enableFunc, _delayTime);
			Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x06008053 RID: 32851 RVA: 0x00366788 File Offset: 0x00364B88
		public void PlayDelayParticle(ParticleType _type, Transform _t, Func<bool> _enableFunc, float _delayTime)
		{
			IEnumerator _coroutine = this.PlayDelayParticleCoroutine((int)_type, _t, _enableFunc, _delayTime);
			Observable.FromCoroutine(() => _coroutine, false).Subscribe<Unit>();
		}

		// Token: 0x06008054 RID: 32852 RVA: 0x003667C4 File Offset: 0x00364BC4
		private IEnumerator PlayDelayParticleCoroutine(int _id, Transform _t, Func<bool> _enableFunc, float _delayTime)
		{
			for (float _time = 0f; _time < _delayTime; _time += Time.deltaTime)
			{
				yield return null;
			}
			bool? flag = (_enableFunc != null) ? new bool?(_enableFunc()) : null;
			if (flag != null && flag.Value)
			{
				this.PlayParticle(_id, _t);
			}
			yield break;
		}

		// Token: 0x06008055 RID: 32853 RVA: 0x003667FC File Offset: 0x00364BFC
		public void StopParticle(int _id, Transform _t, ParticleSystemStopBehavior _stopBehavior = ParticleSystemStopBehavior.StopEmittingAndClear)
		{
			if (_t == null)
			{
				return;
			}
			Dictionary<int, Tuple<ParticleSystem, Transform>> dictionary;
			if (!this.PlayEffectTable.TryGetValue(_id, out dictionary))
			{
				return;
			}
			int instanceID = _t.GetInstanceID();
			Tuple<ParticleSystem, Transform> tuple;
			if (!dictionary.TryGetValue(instanceID, out tuple))
			{
				return;
			}
			if (tuple.Item1 != null)
			{
				tuple.Item1.Stop(true, _stopBehavior);
				this.SetActive(tuple.Item1, false);
			}
			else
			{
				this.PlayEffectTable[_id].Remove(instanceID);
			}
		}

		// Token: 0x06008056 RID: 32854 RVA: 0x00366883 File Offset: 0x00364C83
		public void StopParticle(ParticleType _type, Transform _t, ParticleSystemStopBehavior _stopBehavior = ParticleSystemStopBehavior.StopEmittingAndClear)
		{
			this.StopParticle((int)_type, _t, _stopBehavior);
		}

		// Token: 0x06008057 RID: 32855 RVA: 0x00366890 File Offset: 0x00364C90
		public void StopAllParticle()
		{
			List<Tuple<int, int>> list = ListPool<Tuple<int, int>>.Get();
			foreach (KeyValuePair<int, Dictionary<int, Tuple<ParticleSystem, Transform>>> keyValuePair in this.PlayEffectTable)
			{
				foreach (KeyValuePair<int, Tuple<ParticleSystem, Transform>> keyValuePair2 in keyValuePair.Value)
				{
					if (keyValuePair2.Value.Item1 != null)
					{
						keyValuePair2.Value.Item1.Stop(true);
						this.SetActive(keyValuePair2.Value.Item1, false);
					}
					else
					{
						list.Add(new Tuple<int, int>(keyValuePair.Key, keyValuePair2.Key));
					}
				}
			}
			List<int> list2 = ListPool<int>.Get();
			for (int i = 0; i < list.Count; i++)
			{
				Tuple<int, int> tuple = list[i];
				this.PlayEffectTable[tuple.Item1].Remove(tuple.Item2);
				if (this.PlayEffectTable[tuple.Item1].Count == 0)
				{
					list2.Add(tuple.Item1);
				}
			}
			for (int j = 0; j < list2.Count; j++)
			{
				this.PlayEffectTable.Remove(list2[j]);
			}
			ListPool<Tuple<int, int>>.Release(list);
			ListPool<int>.Release(list2);
		}

		// Token: 0x06008058 RID: 32856 RVA: 0x00366A40 File Offset: 0x00364E40
		public void RemoveParticle(int _id, Transform _t)
		{
			if (_t == null)
			{
				return;
			}
			Dictionary<int, Tuple<ParticleSystem, Transform>> dictionary;
			if (!this.PlayEffectTable.TryGetValue(_id, out dictionary))
			{
				return;
			}
			int instanceID = _t.GetInstanceID();
			Tuple<ParticleSystem, Transform> tuple;
			if (!dictionary.TryGetValue(instanceID, out tuple))
			{
				return;
			}
			if (((tuple != null) ? tuple.Item1 : null) != null)
			{
				UnityEngine.Object.Destroy(tuple.Item1.gameObject);
			}
			this.PlayEffectTable.Remove(instanceID);
			if (this.PlayEffectTable[_id].Count == 0)
			{
				this.PlayEffectTable.Remove(_id);
			}
		}

		// Token: 0x06008059 RID: 32857 RVA: 0x00366ADE File Offset: 0x00364EDE
		public void RemoveParticle(ParticleType _type, Transform _t)
		{
			this.RemoveParticle((int)_type, _t);
		}

		// Token: 0x0600805A RID: 32858 RVA: 0x00366AE8 File Offset: 0x00364EE8
		public void RemoveAllParticle()
		{
			foreach (KeyValuePair<int, Dictionary<int, Tuple<ParticleSystem, Transform>>> keyValuePair in this.PlayEffectTable)
			{
				foreach (KeyValuePair<int, Tuple<ParticleSystem, Transform>> keyValuePair2 in keyValuePair.Value)
				{
					if (keyValuePair2.Value != null && keyValuePair2.Value.Item1 != null)
					{
						UnityEngine.Object.Destroy(keyValuePair2.Value.Item1.gameObject);
					}
				}
			}
			this.PlayEffectTable.Clear();
		}

		// Token: 0x0400670D RID: 26381
		[NonSerialized]
		public System.Action FishGetEvent;

		// Token: 0x0400670E RID: 26382
		[SerializeField]
		private GameObject rootObject;

		// Token: 0x0400670F RID: 26383
		[SerializeField]
		private Transform soundRoot;

		// Token: 0x04006710 RID: 26384
		[SerializeField]
		private Transform hitMoveArea;

		// Token: 0x04006711 RID: 26385
		[SerializeField]
		private BoxCollider waterBox;

		// Token: 0x04006712 RID: 26386
		[SerializeField]
		private Camera uiCamera;

		// Token: 0x04006713 RID: 26387
		public Camera fishModelCamera;

		// Token: 0x04006714 RID: 26388
		private FishingManager.FishingScene scene_;

		// Token: 0x04006715 RID: 26389
		[NonSerialized]
		public PlayerInfo playerInfo = new PlayerInfo();

		// Token: 0x04006716 RID: 26390
		public Fishing playerFishing;

		// Token: 0x04006717 RID: 26391
		[SerializeField]
		private GameObject moveArea;

		// Token: 0x04006718 RID: 26392
		[SerializeField]
		private LineRenderer fishingLine;

		// Token: 0x04006719 RID: 26393
		private GameObject _fishPrefab;

		// Token: 0x0400671A RID: 26394
		private Fish hitFish;

		// Token: 0x0400671B RID: 26395
		private float fishHeartPoint;

		// Token: 0x0400671C RID: 26396
		private List<Fish> fishList = new List<Fish>();

		// Token: 0x0400671D RID: 26397
		public Lure lure;

		// Token: 0x0400671F RID: 26399
		private Vector3 axisArrowOriginScale_ = Vector3.one;

		// Token: 0x04006720 RID: 26400
		[SerializeField]
		private Transform spriteRootObject;

		// Token: 0x04006721 RID: 26401
		[SerializeField]
		private Transform axisArrowObject;

		// Token: 0x04006722 RID: 26402
		[SerializeField]
		private Transform circleRootObject;

		// Token: 0x04006723 RID: 26403
		[SerializeField]
		private FishingManager.SpriteColorPair outCircleSprite;

		// Token: 0x04006724 RID: 26404
		[SerializeField]
		private FishingManager.SpriteColorPair normalCircleSprite;

		// Token: 0x04006725 RID: 26405
		[SerializeField]
		private FishingManager.SpriteColorPair criticalCircleSprite;

		// Token: 0x04006727 RID: 26407
		public static RaycastHit[] raycastHits = new RaycastHit[3];

		// Token: 0x04006729 RID: 26409
		private IDisposable createFishDisposable;

		// Token: 0x0400672A RID: 26410
		private int fishCreateNumber;

		// Token: 0x0400672B RID: 26411
		private bool prevWarningMode;

		// Token: 0x0400672C RID: 26412
		private Vector2 prevInputAxis = Vector2.zero;

		// Token: 0x0400672D RID: 26413
		private Vector2 prevMouseAxis = Vector2.zero;

		// Token: 0x0400672E RID: 26414
		private float fishingTimeCounter;

		// Token: 0x0400672F RID: 26415
		private Dictionary<SEType, Dictionary<int, AudioSource>> SETable = new Dictionary<SEType, Dictionary<int, AudioSource>>();

		// Token: 0x02000F2C RID: 3884
		public enum FishingScene
		{
			// Token: 0x04006732 RID: 26418
			SelectFood,
			// Token: 0x04006733 RID: 26419
			StartMotion,
			// Token: 0x04006734 RID: 26420
			WaitHit,
			// Token: 0x04006735 RID: 26421
			Fishing,
			// Token: 0x04006736 RID: 26422
			Success,
			// Token: 0x04006737 RID: 26423
			Failure,
			// Token: 0x04006738 RID: 26424
			Finish,
			// Token: 0x04006739 RID: 26425
			None
		}

		// Token: 0x02000F2D RID: 3885
		[Serializable]
		public class SpriteColorPair
		{
			// Token: 0x170019C0 RID: 6592
			// (get) Token: 0x06008064 RID: 32868 RVA: 0x00366C51 File Offset: 0x00365051
			public SpriteRenderer Renderer
			{
				[CompilerGenerated]
				get
				{
					return this._renderer;
				}
			}

			// Token: 0x170019C1 RID: 6593
			// (get) Token: 0x06008065 RID: 32869 RVA: 0x00366C59 File Offset: 0x00365059
			public Color NormalColor
			{
				[CompilerGenerated]
				get
				{
					return this._normalColor;
				}
			}

			// Token: 0x170019C2 RID: 6594
			// (get) Token: 0x06008066 RID: 32870 RVA: 0x00366C61 File Offset: 0x00365061
			public Color WarningColor
			{
				[CompilerGenerated]
				get
				{
					return this._warningColor;
				}
			}

			// Token: 0x06008067 RID: 32871 RVA: 0x00366C69 File Offset: 0x00365069
			public void ChangeColor(int _mode)
			{
				if (this._renderer == null)
				{
					return;
				}
				this._renderer.color = ((_mode != 0) ? this._warningColor : this._normalColor);
			}

			// Token: 0x0400673A RID: 26426
			[SerializeField]
			private SpriteRenderer _renderer;

			// Token: 0x0400673B RID: 26427
			[SerializeField]
			private Color _normalColor = Color.white;

			// Token: 0x0400673C RID: 26428
			[SerializeField]
			private Color _warningColor = Color.white;
		}

		// Token: 0x02000F2E RID: 3886
		[Serializable]
		public class ImageColorPair
		{
			// Token: 0x170019C3 RID: 6595
			// (get) Token: 0x06008069 RID: 32873 RVA: 0x00366CBD File Offset: 0x003650BD
			public Image Image
			{
				[CompilerGenerated]
				get
				{
					return this._image;
				}
			}

			// Token: 0x170019C4 RID: 6596
			// (get) Token: 0x0600806A RID: 32874 RVA: 0x00366CC5 File Offset: 0x003650C5
			public Color NormalColor
			{
				[CompilerGenerated]
				get
				{
					return this._normalColor;
				}
			}

			// Token: 0x170019C5 RID: 6597
			// (get) Token: 0x0600806B RID: 32875 RVA: 0x00366CCD File Offset: 0x003650CD
			public Color WarningColor
			{
				[CompilerGenerated]
				get
				{
					return this._warningColor;
				}
			}

			// Token: 0x0600806C RID: 32876 RVA: 0x00366CD5 File Offset: 0x003650D5
			public void ChangeColor(int _mode)
			{
				if (this._image == null)
				{
					return;
				}
				this._image.color = ((_mode != 0) ? this._warningColor : this._normalColor);
			}

			// Token: 0x0400673D RID: 26429
			[SerializeField]
			private Image _image;

			// Token: 0x0400673E RID: 26430
			[SerializeField]
			private Color _normalColor = Color.white;

			// Token: 0x0400673F RID: 26431
			[SerializeField]
			private Color _warningColor = Color.white;
		}
	}
}
