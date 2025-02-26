using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Scene;
using AIProject.UI;
using Cinemachine;
using Cinemachine.Utility;
using ConfigScene;
using Illusion.Extensions;
using LuxWater;
using Manager;
using PlaceholderSoftware.WetStuff;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C42 RID: 3138
	[RequireComponent(typeof(CinemachineBrain))]
	public class ActorCameraControl : SerializedMonoBehaviour, IActionCommand
	{
		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06006191 RID: 24977 RVA: 0x0028DE5C File Offset: 0x0028C25C
		public CinemachineBrain CinemachineBrain
		{
			[CompilerGenerated]
			get
			{
				return (!(this._brain == null)) ? this._brain : (this._brain = base.GetComponent<CinemachineBrain>());
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06006192 RID: 24978 RVA: 0x0028DE94 File Offset: 0x0028C294
		public CameraConfig CameraConfig
		{
			[CompilerGenerated]
			get
			{
				return this._cameraConfig;
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06006193 RID: 24979 RVA: 0x0028DE9C File Offset: 0x0028C29C
		public Camera CameraComponent
		{
			[CompilerGenerated]
			get
			{
				return (!(this._cameraComponent == null)) ? this._cameraComponent : (this._cameraComponent = base.GetComponent<Camera>());
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06006194 RID: 24980 RVA: 0x0028DED4 File Offset: 0x0028C2D4
		public GameScreenShot ScreenShot
		{
			[CompilerGenerated]
			get
			{
				return this._screenShot;
			}
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06006195 RID: 24981 RVA: 0x0028DEDC File Offset: 0x0028C2DC
		// (set) Token: 0x06006196 RID: 24982 RVA: 0x0028DEE4 File Offset: 0x0028C2E4
		public CinemachineVirtualCameraBase ActionCameraNotMove
		{
			get
			{
				return this._actionCameraNotMove;
			}
			set
			{
				this._actionCameraNotMove = value;
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06006197 RID: 24983 RVA: 0x0028DEED File Offset: 0x0028C2ED
		// (set) Token: 0x06006198 RID: 24984 RVA: 0x0028DEF5 File Offset: 0x0028C2F5
		public CinemachineVirtualCameraBase ADVCamera
		{
			get
			{
				return this._advCamera;
			}
			set
			{
				this._advCamera = value;
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06006199 RID: 24985 RVA: 0x0028DEFE File Offset: 0x0028C2FE
		// (set) Token: 0x0600619A RID: 24986 RVA: 0x0028DF06 File Offset: 0x0028C306
		public CinemachineVirtualCameraBase ADVNotStandCamera
		{
			get
			{
				return this._advNotStandCamera;
			}
			set
			{
				this._advNotStandCamera = value;
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x0600619B RID: 24987 RVA: 0x0028DF0F File Offset: 0x0028C30F
		// (set) Token: 0x0600619C RID: 24988 RVA: 0x0028DF17 File Offset: 0x0028C317
		public CinemachineVirtualCameraBase EventCamera
		{
			get
			{
				return this._eventCamera;
			}
			set
			{
				this._eventCamera = value;
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x0600619D RID: 24989 RVA: 0x0028DF20 File Offset: 0x0028C320
		// (set) Token: 0x0600619E RID: 24990 RVA: 0x0028DF28 File Offset: 0x0028C328
		public CinemachineVirtualCameraBase HCamera
		{
			get
			{
				return this._hCamera;
			}
			set
			{
				this._hCamera = value;
			}
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x0600619F RID: 24991 RVA: 0x0028DF31 File Offset: 0x0028C331
		// (set) Token: 0x060061A0 RID: 24992 RVA: 0x0028DF39 File Offset: 0x0028C339
		public CinemachineVirtualCameraBase FishingCamera { get; set; }

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x060061A1 RID: 24993 RVA: 0x0028DF42 File Offset: 0x0028C342
		// (set) Token: 0x060061A2 RID: 24994 RVA: 0x0028DF4A File Offset: 0x0028C34A
		public Vector3 FishingLocalPosition { get; set; }

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x060061A3 RID: 24995 RVA: 0x0028DF53 File Offset: 0x0028C353
		// (set) Token: 0x060061A4 RID: 24996 RVA: 0x0028DF5B File Offset: 0x0028C35B
		public Quaternion FishingLocalRotation { get; set; }

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x060061A5 RID: 24997 RVA: 0x0028DF64 File Offset: 0x0028C364
		public Transform ADVNotStandRoot
		{
			[CompilerGenerated]
			get
			{
				return this._advNotStandRoot;
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x060061A6 RID: 24998 RVA: 0x0028DF6C File Offset: 0x0028C36C
		// (set) Token: 0x060061A7 RID: 24999 RVA: 0x0028DF74 File Offset: 0x0028C374
		public Animator EventCameraLocator
		{
			get
			{
				return this._eventCameraLocator;
			}
			set
			{
				this._eventCameraLocator = value;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x060061A8 RID: 25000 RVA: 0x0028DF7D File Offset: 0x0028C37D
		public Transform EventCameraParent
		{
			[CompilerGenerated]
			get
			{
				return this._eventCameraParent;
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x060061A9 RID: 25001 RVA: 0x0028DF85 File Offset: 0x0028C385
		public Transform VirtualCameraRoot
		{
			[CompilerGenerated]
			get
			{
				return this._virtualCameraRoot;
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x060061AA RID: 25002 RVA: 0x0028DF8D File Offset: 0x0028C38D
		public CrossFade CrossFade
		{
			[CompilerGenerated]
			get
			{
				return this._crossFade;
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x060061AB RID: 25003 RVA: 0x0028DF95 File Offset: 0x0028C395
		public VanishControl VanishControl
		{
			[CompilerGenerated]
			get
			{
				return this._vanishControl;
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x060061AC RID: 25004 RVA: 0x0028DF9D File Offset: 0x0028C39D
		// (set) Token: 0x060061AD RID: 25005 RVA: 0x0028DFA5 File Offset: 0x0028C3A5
		public ActorCameraControl.LocomotionSettingData LocomotionSetting
		{
			get
			{
				return this._locomotionSetting;
			}
			set
			{
				this._locomotionSetting = value;
			}
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x060061AE RID: 25006 RVA: 0x0028DFAE File Offset: 0x0028C3AE
		public CinemachineFreeLook ActiveFreeLookCamera
		{
			[CompilerGenerated]
			get
			{
				return this._activeVirtualCamera as CinemachineFreeLook;
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x060061AF RID: 25007 RVA: 0x0028DFBB File Offset: 0x0028C3BB
		public CinemachineVirtualCamera ActiveVirtualCamera
		{
			[CompilerGenerated]
			get
			{
				return this._activeVirtualCamera as CinemachineVirtualCamera;
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x060061B0 RID: 25008 RVA: 0x0028DFC8 File Offset: 0x0028C3C8
		// (set) Token: 0x060061B1 RID: 25009 RVA: 0x0028DFD0 File Offset: 0x0028C3D0
		public bool IsChangeable { get; set; } = true;

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x060061B2 RID: 25010 RVA: 0x0028DFD9 File Offset: 0x0028C3D9
		// (set) Token: 0x060061B3 RID: 25011 RVA: 0x0028DFE8 File Offset: 0x0028C3E8
		public float XAxisValue
		{
			get
			{
				return this._xAxis.value;
			}
			set
			{
				if (value > this._xAxis.maxSpeed || value < this._xAxis.minValue)
				{
					if (this._xAxis.wrap)
					{
						if (value > this._xAxis.maxValue)
						{
							this._xAxis.value = this._xAxis.minValue + (value - this._xAxis.maxValue);
						}
						else
						{
							this._xAxis.value = this._xAxis.maxValue + (value - this._xAxis.minValue);
						}
					}
					else
					{
						this._xAxis.value = Mathf.Clamp(value, this._xAxis.minValue, this._xAxis.maxValue);
					}
				}
				else
				{
					this._xAxis.value = value;
				}
			}
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x060061B4 RID: 25012 RVA: 0x0028E0C2 File Offset: 0x0028C4C2
		// (set) Token: 0x060061B5 RID: 25013 RVA: 0x0028E0CF File Offset: 0x0028C4CF
		public float YAxisValue
		{
			get
			{
				return this._yAxis.value;
			}
			set
			{
				this._yAxis.value = Mathf.Clamp(value, this._yAxis.minValue, this._yAxis.maxValue);
			}
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x060061B6 RID: 25014 RVA: 0x0028E0F8 File Offset: 0x0028C4F8
		// (set) Token: 0x060061B7 RID: 25015 RVA: 0x0028E100 File Offset: 0x0028C500
		public LensSettings LensSetting
		{
			get
			{
				return this._lensSetting;
			}
			set
			{
				this._lensSetting = value;
			}
		}

		// Token: 0x17001375 RID: 4981
		// (set) Token: 0x060061B8 RID: 25016 RVA: 0x0028E10C File Offset: 0x0028C50C
		public bool AmbientLight
		{
			set
			{
				if (this._charaLightNormal != null)
				{
					this._charaLightNormal.SetActiveIfDifferent(value);
				}
				if (this._charaLightCustom != null)
				{
					this._charaLightCustom.SetActiveIfDifferent(!value);
				}
				if (this._prevAmb != value && Singleton<Map>.IsInstance() && Singleton<Map>.Instance.Simulator != null && Singleton<Map>.Instance.Simulator.EnviroSky != null)
				{
					Light component = Singleton<Map>.Instance.Simulator.EnviroSky.Components.DirectLight.GetComponent<Light>();
					if (value)
					{
						component.cullingMask = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.EnvLightCulMask;
					}
					else
					{
						component.cullingMask = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.EnvLightCulMaskCustom;
					}
				}
				this._prevAmb = value;
			}
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x060061B9 RID: 25017 RVA: 0x0028E20C File Offset: 0x0028C60C
		private Light EnviroLight
		{
			get
			{
				if (!Singleton<Map>.IsInstance())
				{
					return null;
				}
				if (this._enviroLight == null)
				{
					EnviroSky enviroSky = Singleton<Map>.Instance.Simulator.EnviroSky;
					if (enviroSky != null)
					{
						this._enviroLight = enviroSky.Components.DirectLight.GetComponent<Light>();
					}
				}
				return this._enviroLight;
			}
		}

		// Token: 0x060061BA RID: 25018 RVA: 0x0028E26E File Offset: 0x0028C66E
		public void EnableUpdateCustomLight()
		{
			this._updateCustomLight = true;
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x0028E277 File Offset: 0x0028C677
		public void DisableUpdateCustomLight()
		{
			this._updateCustomLight = false;
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x060061BC RID: 25020 RVA: 0x0028E280 File Offset: 0x0028C680
		public Light NormalKeyLight
		{
			[CompilerGenerated]
			get
			{
				return this._normalKeyLight;
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x060061BD RID: 25021 RVA: 0x0028E288 File Offset: 0x0028C688
		public Light CustomKeyLight
		{
			[CompilerGenerated]
			get
			{
				return this._customKeyLight;
			}
		}

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x060061BE RID: 25022 RVA: 0x0028E290 File Offset: 0x0028C690
		// (set) Token: 0x060061BF RID: 25023 RVA: 0x0028E298 File Offset: 0x0028C698
		public Action OnCameraBlended { get; set; }

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x060061C0 RID: 25024 RVA: 0x0028E2A1 File Offset: 0x0028C6A1
		public LuxWater_UnderWaterRendering UnderWaterFX
		{
			get
			{
				if (this._underWaterFX == null)
				{
					this._underWaterFX = base.GetComponent<LuxWater_UnderWaterRendering>();
				}
				return this._underWaterFX;
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x060061C1 RID: 25025 RVA: 0x0028E2C6 File Offset: 0x0028C6C6
		public LuxWater_UnderWaterBlur UnderWaterBlurFX
		{
			get
			{
				if (this._underWaterBlurFX == null)
				{
					this._underWaterBlurFX = base.GetComponent<LuxWater_UnderWaterBlur>();
				}
				return this._underWaterBlurFX;
			}
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x060061C2 RID: 25026 RVA: 0x0028E2EB File Offset: 0x0028C6EB
		public WetStuff WetStuff
		{
			get
			{
				if (this._wetStuff == null)
				{
					this._wetStuff = base.GetComponent<WetStuff>();
				}
				return this._wetStuff;
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x060061C3 RID: 25027 RVA: 0x0028E310 File Offset: 0x0028C710
		public WetDecal WetDecal
		{
			[CompilerGenerated]
			get
			{
				return this._wetDecal;
			}
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x060061C4 RID: 25028 RVA: 0x0028E318 File Offset: 0x0028C718
		// (set) Token: 0x060061C5 RID: 25029 RVA: 0x0028E320 File Offset: 0x0028C720
		public Vector3 WetDecalOffset
		{
			get
			{
				return this._wetDecalOffset;
			}
			set
			{
				this._wetDecalOffset = value;
			}
		}

		// Token: 0x060061C6 RID: 25030 RVA: 0x0028E32C File Offset: 0x0028C72C
		private void Start()
		{
			this._wetStuff = base.GetComponent<WetStuff>();
			(from isOn in this._isBlendingChange.TakeUntilDestroy(base.gameObject).DistinctUntilChanged<bool>()
			where !isOn
			select isOn).Subscribe(delegate(bool _)
			{
				if (this.OnCameraBlended == null)
				{
					return;
				}
				this.OnCameraBlended();
				this.OnCameraBlended = null;
			});
			foreach (KeyValuePair<ShotType, CinemachineVirtualCameraBase> keyValuePair in this._virtualCameraNormalTable)
			{
				keyValuePair.Value.enabled = false;
			}
			foreach (KeyValuePair<ShotType, CinemachineVirtualCameraBase> keyValuePair2 in this._virtualCameraActionTable)
			{
				keyValuePair2.Value.enabled = false;
			}
			(this._activeVirtualCamera = (this._activeCameraTable = this._virtualCameraNormalTable)[ShotType.Near]).enabled = true;
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand();
			keyCodeDownCommand.KeyCode = KeyCode.Alpha1;
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				if (!this.CinemachineBrain.enabled)
				{
					return;
				}
				if (this._mode != CameraMode.Normal && this._mode != CameraMode.ActionFreeLook)
				{
					return;
				}
				if (!this.IsChangeable)
				{
					return;
				}
				this.ShotType = ShotType.Near;
			});
			KeyCodeDownCommand keyCodeDownCommand2 = new KeyCodeDownCommand();
			keyCodeDownCommand2.KeyCode = KeyCode.Alpha2;
			keyCodeDownCommand2.TriggerEvent.AddListener(delegate()
			{
				if (!this.CinemachineBrain.enabled)
				{
					return;
				}
				if (this._mode != CameraMode.Normal && this._mode != CameraMode.ActionFreeLook)
				{
					return;
				}
				if (!this.IsChangeable)
				{
					return;
				}
				this.ShotType = ShotType.PointOfView;
			});
			KeyCodeDownCommand keyCodeDownCommand3 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Semicolon
			};
			keyCodeDownCommand3.TriggerEvent.AddListener(delegate()
			{
				this._lensSetting.FieldOfView = this._defaultLensSetting.FieldOfView;
			});
			KeyCodeDownCommand keyCodeDownCommand4 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Slash
			};
			keyCodeDownCommand4.TriggerEvent.AddListener(delegate()
			{
				this._lensSetting.Dutch = this._defaultLensSetting.Dutch;
			});
			KeyCodeCommand keyCodeCommand = new KeyCodeCommand
			{
				KeyCode = KeyCode.Equals
			};
			keyCodeCommand.TriggerEvent.AddListener(delegate(bool isDown)
			{
				if (!isDown)
				{
					return;
				}
				float deltaTime = Time.deltaTime;
				this._lensSetting.FieldOfView = Mathf.Max(this._lensSetting.FieldOfView - this._defaultLensSetting.KeyZoomScale * deltaTime, this._defaultLensSetting.MinFOV);
			});
			KeyCodeCommand keyCodeCommand2 = new KeyCodeCommand
			{
				KeyCode = KeyCode.RightBracket
			};
			keyCodeCommand2.TriggerEvent.AddListener(delegate(bool isDown)
			{
				if (!isDown)
				{
					return;
				}
				float deltaTime = Time.deltaTime;
				this._lensSetting.FieldOfView = Mathf.Min(this._lensSetting.FieldOfView + this._defaultLensSetting.KeyZoomScale * deltaTime, this._defaultLensSetting.MaxFOV);
			});
			KeyCodeCommand keyCodeCommand3 = new KeyCodeCommand
			{
				KeyCode = KeyCode.Period
			};
			keyCodeCommand3.TriggerEvent.AddListener(delegate(bool isDown)
			{
				if (!isDown)
				{
					return;
				}
				float deltaTime = Time.deltaTime;
				float num = this._lensSetting.Dutch + this._defaultLensSetting.KeyRotateScale * deltaTime;
				if (num <= -180f)
				{
					num %= 360f;
					if (num <= -180f)
					{
						num += 360f;
					}
				}
				else if (180f < num)
				{
					num %= 360f;
					if (180f < num)
					{
						num -= 360f;
					}
				}
				this._lensSetting.Dutch = num;
			});
			KeyCodeCommand keyCodeCommand4 = new KeyCodeCommand
			{
				KeyCode = KeyCode.Backslash
			};
			keyCodeCommand4.TriggerEvent.AddListener(delegate(bool isDown)
			{
				if (!isDown)
				{
					return;
				}
				float deltaTime = Time.deltaTime;
				float num = this._lensSetting.Dutch - this._defaultLensSetting.KeyRotateScale * deltaTime;
				if (num <= -180f)
				{
					num %= 360f;
					if (num <= -180f)
					{
						num += 360f;
					}
				}
				else if (180f < num)
				{
					num %= 360f;
					if (180f < num)
					{
						num -= 360f;
					}
				}
				this._lensSetting.Dutch = num;
			});
			this._keyCommands.Add(keyCodeDownCommand);
			this._keyCommands.Add(keyCodeDownCommand2);
			this._keyCommands.Add(keyCodeDownCommand3);
			this._keyCommands.Add(keyCodeDownCommand4);
			this._keyDownCommands.Add(keyCodeCommand);
			this._keyDownCommands.Add(keyCodeCommand2);
			this._keyDownCommands.Add(keyCodeCommand3);
			this._keyDownCommands.Add(keyCodeCommand4);
			if (Singleton<Map>.IsInstance() && Singleton<Map>.Instance.Simulator != null && Singleton<Map>.Instance.Simulator.EnviroSky != null)
			{
				Light component = Singleton<Map>.Instance.Simulator.EnviroSky.Components.DirectLight.GetComponent<Light>();
				if (Config.GraphicData.AmbientLight)
				{
					component.cullingMask = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.EnvLightCulMask;
				}
				else
				{
					component.cullingMask = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.EnvLightCulMaskCustom;
				}
			}
			this._prevAmb = Config.GraphicData.AmbientLight;
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x060061C7 RID: 25031 RVA: 0x0028E6DC File Offset: 0x0028CADC
		// (set) Token: 0x060061C8 RID: 25032 RVA: 0x0028E6E4 File Offset: 0x0028CAE4
		public bool EnabledInput { get; set; }

		// Token: 0x060061C9 RID: 25033 RVA: 0x0028E6F0 File Offset: 0x0028CAF0
		public void OnUpdateInput()
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			foreach (KeyCodeDownCommand keyCodeDownCommand in this._keyCommands)
			{
				keyCodeDownCommand.Invoke(instance);
			}
			foreach (KeyCodeCommand keyCodeCommand in this._keyDownCommands)
			{
				keyCodeCommand.Invoke(instance);
			}
		}

		// Token: 0x060061CA RID: 25034 RVA: 0x0028E7A0 File Offset: 0x0028CBA0
		private void Update()
		{
			if (Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				this.CinemachineBrain.enabled = false;
			}
			else if (!this.CinemachineBrain.enabled && Singleton<MapScene>.IsInstance())
			{
				this.CinemachineBrain.enabled = !Singleton<MapScene>.Instance.IsLoading;
			}
			this.AmbientLight = Config.GraphicData.AmbientLight;
			if (this._updateCustomLight)
			{
				Light enviroLight = this.EnviroLight;
				if (enviroLight != null)
				{
					this._customKeyLight.color = enviroLight.color;
					this._customKeyLight.intensity = enviroLight.intensity;
				}
			}
			if (this.Mode != CameraMode.Normal && this.Mode != CameraMode.ActionFreeLook)
			{
				return;
			}
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			Vector2 defaultCameraAxisPow = locomotionProfile.DefaultCameraAxisPow;
			ActionSystem actData = Config.ActData;
			if (this.ShotType == ShotType.PointOfView)
			{
				float t = Mathf.InverseLerp(0f, 100f, (float)actData.FPSSensitivityX);
				float t2 = Mathf.InverseLerp(0f, 100f, (float)actData.FPSSensitivityY);
				this._xAxis.maxSpeed = locomotionProfile.CameraPowX.Lerp(t);
				this._yAxis.maxSpeed = locomotionProfile.CameraPowY.Lerp(t2);
			}
			else
			{
				float t3 = Mathf.InverseLerp(0f, 100f, (float)actData.TPSSensitivityX);
				float t4 = Mathf.InverseLerp(0f, 100f, (float)actData.TPSSensitivityY);
				this._xAxis.maxSpeed = locomotionProfile.CameraPowX.Lerp(t3);
				this._yAxis.maxSpeed = locomotionProfile.CameraPowY.Lerp(t4);
			}
			this._xAxis.invertInput = actData.InvertMoveX;
			this._yAxis.invertInput = !actData.InvertMoveY;
			Vector3 cameraAccelRate = locomotionProfile.CameraAccelRate;
			this._xAxis.accelTime = cameraAccelRate.x;
			this._yAxis.accelTime = cameraAccelRate.y;
		}

		// Token: 0x060061CB RID: 25035 RVA: 0x0028E9B4 File Offset: 0x0028CDB4
		private void LateUpdate()
		{
			this.UpdateLensSetting();
			if (Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				return;
			}
			if (this._isBlendingChange != null)
			{
				this._isBlendingChange.OnNext(this._brain.IsBlending);
			}
			if (this._vanishControl != null)
			{
				this._vanishControl.transform.position = this._activeVirtualCamera.transform.position;
				this._vanishControl.transform.rotation = this._activeVirtualCamera.transform.rotation;
			}
			if (this._brain.IsBlending)
			{
				return;
			}
			if (this._activeVirtualCamera is CinemachineFreeLook)
			{
				bool flag = false;
				float smoothDeltaTime = Time.smoothDeltaTime;
				if (this.EnabledInput)
				{
					bool flag2 = smoothDeltaTime >= 0f && CinemachineCore.Instance.IsLive(this._activeVirtualCamera);
					if (flag2)
					{
						if (this._yAxis.Update(smoothDeltaTime))
						{
							this._yAxisRecentering.CancelRecentering();
						}
						flag = this._xAxis.Update(smoothDeltaTime);
					}
				}
				CinemachineFreeLook cinemachineFreeLook = this._activeVirtualCamera as CinemachineFreeLook;
				cinemachineFreeLook.m_YAxis.m_MaxValue = this._yAxis.maxValue;
				cinemachineFreeLook.m_YAxis.m_MinValue = this._yAxis.minValue;
				cinemachineFreeLook.m_YAxis.Value = this._yAxis.value;
				cinemachineFreeLook.m_XAxis.Value = this._xAxis.value;
				cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = this._recenterToTargetHeading.enabled;
				cinemachineFreeLook.m_RecenterToTargetHeading.m_RecenteringTime = this._recenterToTargetHeading.recenteringTime;
				cinemachineFreeLook.m_RecenterToTargetHeading.m_WaitTime = this._recenterToTargetHeading.waitTime;
				CinemachineOrbitalTransposer cinemachineOrbitalTransposer = null;
				if (flag)
				{
					CinemachineVirtualCamera liveChildOrSelf = this.GetLiveChildOrSelf(cinemachineFreeLook);
					cinemachineOrbitalTransposer = liveChildOrSelf.GetCinemachineComponent<CinemachineOrbitalTransposer>();
					if (cinemachineOrbitalTransposer != null)
					{
						cinemachineOrbitalTransposer.m_RecenterToTargetHeading.CancelRecentering();
					}
				}
				if (cinemachineOrbitalTransposer != null)
				{
					if (cinemachineOrbitalTransposer.m_BindingMode != CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
					{
						float targetHeading = this.GetTargetHeading(this._xAxis.value, cinemachineOrbitalTransposer.GetReferenceOrientation(this._activeVirtualCamera.State.ReferenceUp), smoothDeltaTime, cinemachineOrbitalTransposer);
						AxisState xaxis = cinemachineFreeLook.m_XAxis;
						cinemachineOrbitalTransposer.m_RecenterToTargetHeading.DoRecentering(ref xaxis, smoothDeltaTime, targetHeading);
					}
					if (cinemachineOrbitalTransposer.m_BindingMode == CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
					{
						this._xAxis.value = 0f;
					}
				}
			}
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x0028EC28 File Offset: 0x0028D028
		private void UpdateLensSetting()
		{
			if (this.Mode == CameraMode.ADV || this.Mode == CameraMode.ADVExceptStand || this.Mode == CameraMode.Event)
			{
				return;
			}
			if (this._activeVirtualCamera is CinemachineVirtualCamera)
			{
				CinemachineVirtualCamera cinemachineVirtualCamera = this._activeVirtualCamera as CinemachineVirtualCamera;
				LensSettings lens = cinemachineVirtualCamera.m_Lens;
				if (this._lensSetting.FieldOfView != lens.FieldOfView)
				{
					cinemachineVirtualCamera.m_Lens.FieldOfView = this._lensSetting.FieldOfView;
				}
				if (this._lensSetting.NearClipPlane != lens.NearClipPlane)
				{
					cinemachineVirtualCamera.m_Lens.NearClipPlane = this._lensSetting.NearClipPlane;
				}
				if (this._lensSetting.FarClipPlane != lens.FarClipPlane)
				{
					cinemachineVirtualCamera.m_Lens.FarClipPlane = this._lensSetting.FarClipPlane;
				}
				if (this._lensSetting.Dutch != lens.Dutch)
				{
					cinemachineVirtualCamera.m_Lens.Dutch = this._lensSetting.Dutch;
				}
			}
			else if (this._activeVirtualCamera is CinemachineFreeLook)
			{
				CinemachineFreeLook cinemachineFreeLook = this._activeVirtualCamera as CinemachineFreeLook;
				LensSettings lens2 = cinemachineFreeLook.m_Lens;
				if (this._lensSetting.FieldOfView != lens2.FieldOfView)
				{
					cinemachineFreeLook.m_Lens.FieldOfView = this._lensSetting.FieldOfView;
				}
				if (this._lensSetting.NearClipPlane != lens2.NearClipPlane)
				{
					cinemachineFreeLook.m_Lens.NearClipPlane = this._lensSetting.NearClipPlane;
				}
				if (this._lensSetting.FarClipPlane != lens2.FarClipPlane)
				{
					cinemachineFreeLook.m_Lens.FarClipPlane = this._lensSetting.FarClipPlane;
				}
				if (this._lensSetting.Dutch != lens2.Dutch)
				{
					cinemachineFreeLook.m_Lens.Dutch = this._lensSetting.Dutch;
				}
			}
		}

		// Token: 0x060061CD RID: 25037 RVA: 0x0028EE10 File Offset: 0x0028D210
		private CinemachineVirtualCamera GetLiveChildOrSelf(CinemachineFreeLook freeLook)
		{
			float yaxisValue = this.GetYAxisValue(freeLook);
			if (yaxisValue < 0.33f)
			{
				return freeLook.GetRig(2);
			}
			if (yaxisValue > 0.66f)
			{
				return freeLook.GetRig(0);
			}
			return freeLook.GetRig(1);
		}

		// Token: 0x060061CE RID: 25038 RVA: 0x0028EE54 File Offset: 0x0028D254
		private float GetYAxisValue(CinemachineFreeLook freeLook)
		{
			float num = freeLook.m_YAxis.m_MaxValue - freeLook.m_YAxis.m_MinValue;
			return (num <= 0.0001f) ? 0.5f : (freeLook.m_YAxis.Value / num);
		}

		// Token: 0x060061CF RID: 25039 RVA: 0x0028EE9C File Offset: 0x0028D29C
		private float GetTargetHeading(float currentHeading, Quaternion targetOrientation, float deltaTime, CinemachineOrbitalTransposer transposer)
		{
			float result;
			if (transposer.m_BindingMode == CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
			{
				result = 0f;
			}
			else if (transposer.FollowTarget == null)
			{
				result = currentHeading;
			}
			else
			{
				CinemachineOrbitalTransposer.Heading.HeadingDefinition headingDefinition = transposer.m_Heading.m_Definition;
				if (this._prevTarget != transposer.FollowTarget)
				{
					this._prevTarget = transposer.FollowTarget;
					this._targetRigidbody = ((this._prevTarget != null) ? this._prevTarget.GetComponent<Rigidbody>() : null);
					Vector3? vector = (this._prevTarget != null) ? new Vector3?(this._prevTarget.position) : null;
					this._lastTargetPosition = ((vector == null) ? Vector3.zero : vector.Value);
				}
				if (headingDefinition == CinemachineOrbitalTransposer.Heading.HeadingDefinition.Velocity && this._targetRigidbody == null)
				{
					headingDefinition = CinemachineOrbitalTransposer.Heading.HeadingDefinition.PositionDelta;
				}
				Vector3 vector2 = Vector3.zero;
				switch (headingDefinition)
				{
				case CinemachineOrbitalTransposer.Heading.HeadingDefinition.PositionDelta:
					vector2 = transposer.FollowTargetPosition - this._lastTargetPosition;
					break;
				case CinemachineOrbitalTransposer.Heading.HeadingDefinition.Velocity:
					vector2 = this._targetRigidbody.velocity;
					break;
				case CinemachineOrbitalTransposer.Heading.HeadingDefinition.TargetForward:
					vector2 = transposer.FollowTargetRotation * Vector3.forward;
					break;
				case CinemachineOrbitalTransposer.Heading.HeadingDefinition.WorldForward:
					return 0f;
				}
				int num = transposer.m_Heading.m_VelocityFilterStrength * 5;
				if (this._headingTracker == null || this._headingTracker.FilterSize != num)
				{
					this._headingTracker = new ActorCameraControl.HeadingTracker(num);
				}
				this._headingTracker.DecayHistory();
				Vector3 vector3 = targetOrientation * Vector3.up;
				vector2 = vector2.ProjectOntoPlane(vector3);
				if (!vector2.AlmostZero())
				{
					result = UnityVectorExtensions.SignedAngle(targetOrientation * Vector3.forward, vector2, vector3);
				}
				else
				{
					result = currentHeading;
				}
			}
			return result;
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x060061D0 RID: 25040 RVA: 0x0028F077 File Offset: 0x0028D477
		// (set) Token: 0x060061D1 RID: 25041 RVA: 0x0028F080 File Offset: 0x0028D480
		public CameraMode Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				if (this._mode == value)
				{
					return;
				}
				this._mode = value;
				CinemachineVirtualCameraBase cinemachineVirtualCameraBase = null;
				if (value == CameraMode.Normal)
				{
					this._activeCameraTable = this._virtualCameraNormalTable;
				}
				else if (value == CameraMode.ActionFreeLook)
				{
					this._activeCameraTable = this._virtualCameraActionTable;
				}
				else
				{
					this._activeCameraTable = null;
					switch (value)
					{
					case CameraMode.ActionNotMove:
						cinemachineVirtualCameraBase = this._actionCameraNotMove;
						this.prevCamPos = this._brain.transform.position;
						this.prevCamrot = this._brain.transform.rotation;
						break;
					case CameraMode.ADV:
						cinemachineVirtualCameraBase = this._advCamera;
						break;
					case CameraMode.ADVExceptStand:
						cinemachineVirtualCameraBase = this._advNotStandCamera;
						break;
					case CameraMode.Event:
						cinemachineVirtualCameraBase = this._eventCamera;
						break;
					case CameraMode.H:
						cinemachineVirtualCameraBase = this._hCamera;
						break;
					case CameraMode.Fishing:
						cinemachineVirtualCameraBase = this.FishingCamera;
						break;
					case CameraMode.Crafting:
						cinemachineVirtualCameraBase = this._craftingCamera;
						break;
					}
				}
				bool active = value == CameraMode.ADV || value == CameraMode.ADVExceptStand;
				this._vanishControl.gameObject.SetActiveIfDifferent(active);
				if (this._activeCameraTable != null)
				{
					CinemachineVirtualCameraBase cinemachineVirtualCameraBase2;
					this._activeCameraTable.TryGetValue(this._shotType, out cinemachineVirtualCameraBase2);
					cinemachineVirtualCameraBase = cinemachineVirtualCameraBase2;
				}
				if (this._activeVirtualCamera is CinemachineFreeLook && cinemachineVirtualCameraBase is CinemachineFreeLook)
				{
					CameraState state = this.GetLiveChildOrSelf(this._activeVirtualCamera as CinemachineFreeLook).State;
					this._activeVirtualCamera.enabled = false;
					this._activeVirtualCamera = cinemachineVirtualCameraBase;
					this.GetLiveChildOrSelf(this._activeVirtualCamera as CinemachineFreeLook).GetComponentOwner().localRotation = state.RawOrientation;
					this._activeVirtualCamera.UpdateCameraState(state.ReferenceUp, Time.unscaledDeltaTime);
					this._activeVirtualCamera.enabled = true;
				}
				else
				{
					this._activeVirtualCamera.enabled = false;
					(this._activeVirtualCamera = cinemachineVirtualCameraBase).enabled = true;
				}
			}
		}

		// Token: 0x060061D2 RID: 25042 RVA: 0x0028F274 File Offset: 0x0028D674
		public void LoadInitialState()
		{
			foreach (KeyValuePair<CameraMode, Action<ActorCameraControl, CameraMode, ShotType>> keyValuePair in ActorCameraControl._activationEventTable)
			{
				Action<ActorCameraControl, CameraMode, ShotType> value = keyValuePair.Value;
				if (value != null)
				{
					value(this, this._mode, this._shotType);
				}
			}
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x060061D3 RID: 25043 RVA: 0x0028F2EC File Offset: 0x0028D6EC
		// (set) Token: 0x060061D4 RID: 25044 RVA: 0x0028F2F4 File Offset: 0x0028D6F4
		public ShotType ShotType
		{
			get
			{
				return this._shotType;
			}
			set
			{
				if (this._mode == CameraMode.Event || this._mode == CameraMode.H || this._mode == CameraMode.ActionNotMove)
				{
					return;
				}
				if (this._shotType == value)
				{
					return;
				}
				this._shotType = value;
				this.ChangeActiveCameraByShotType(value);
			}
		}

		// Token: 0x060061D5 RID: 25045 RVA: 0x0028F341 File Offset: 0x0028D741
		public void SetShotTypeForce(ShotType type)
		{
			this._recovShotType = this._shotType;
			this._shotType = type;
			if (this._activeCameraTable == null)
			{
				return;
			}
			this.ChangeActiveCameraByShotType(type);
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x0028F369 File Offset: 0x0028D769
		public void RecoverShotType()
		{
			this.SetShotTypeForce(this._recovShotType);
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x0028F378 File Offset: 0x0028D778
		private void ChangeActiveCameraByShotType(ShotType type)
		{
			CameraState state = this.GetLiveChildOrSelf(this._activeVirtualCamera as CinemachineFreeLook).State;
			this._activeVirtualCamera.enabled = false;
			this._activeVirtualCamera = this._activeCameraTable[type];
			this.GetLiveChildOrSelf(this._activeVirtualCamera as CinemachineFreeLook).GetComponentOwner().localRotation = state.RawOrientation;
			this._activeVirtualCamera.UpdateCameraState(state.ReferenceUp, Time.unscaledDeltaTime);
			this._activeVirtualCamera.enabled = true;
		}

		// Token: 0x060061D8 RID: 25048 RVA: 0x0028F400 File Offset: 0x0028D800
		public void AssignCameraTable(CameraTable table, CameraTable actionTable)
		{
			if (table != null)
			{
				ShotType[] keys = table.Keys;
				foreach (ShotType shotType in keys)
				{
					CinemachineVirtualCameraBase cinemachineVirtualCameraBase = table[shotType];
					this._virtualCameraNormalTable[shotType] = cinemachineVirtualCameraBase;
					if (shotType != ShotType.PointOfView)
					{
						cinemachineVirtualCameraBase.Follow = this._locomotionSetting.Follow;
						cinemachineVirtualCameraBase.LookAt = this._locomotionSetting.LookAt;
					}
					else
					{
						cinemachineVirtualCameraBase.Follow = this._locomotionSetting.LookAtPOV;
						cinemachineVirtualCameraBase.LookAt = this._locomotionSetting.LookAtPOV;
					}
				}
			}
			if (actionTable != null)
			{
				ShotType[] keys2 = actionTable.Keys;
				foreach (ShotType shotType2 in keys2)
				{
					CinemachineVirtualCameraBase cinemachineVirtualCameraBase2 = actionTable[shotType2];
					this._virtualCameraActionTable[shotType2] = cinemachineVirtualCameraBase2;
					if (shotType2 != ShotType.PointOfView)
					{
						cinemachineVirtualCameraBase2.Follow = this._locomotionSetting.ActionLookAt;
						cinemachineVirtualCameraBase2.LookAt = this._locomotionSetting.ActionLookAt;
					}
					else
					{
						cinemachineVirtualCameraBase2.Follow = this._locomotionSetting.ActionLookAtPOV;
						cinemachineVirtualCameraBase2.LookAt = this._locomotionSetting.ActionLookAtPOV;
					}
				}
			}
		}

		// Token: 0x060061D9 RID: 25049 RVA: 0x0028F54C File Offset: 0x0028D94C
		public void SetNormalLookAt(Transform lookAt)
		{
			if (lookAt == null)
			{
				return;
			}
			ShotType[] array = this._virtualCameraNormalTable.Keys.ToArray<ShotType>();
			foreach (ShotType key in array)
			{
				CinemachineVirtualCameraBase cinemachineVirtualCameraBase = this._virtualCameraNormalTable[key];
				if (cinemachineVirtualCameraBase != null)
				{
					cinemachineVirtualCameraBase.LookAt = lookAt;
				}
			}
		}

		// Token: 0x060061DA RID: 25050 RVA: 0x0028F5B4 File Offset: 0x0028D9B4
		public void SetNormalPrevLookAt()
		{
			ShotType[] array = this._virtualCameraNormalTable.Keys.ToArray<ShotType>();
			foreach (ShotType key in array)
			{
				CinemachineVirtualCameraBase cinemachineVirtualCameraBase = this._virtualCameraNormalTable[key];
				Transform lookAt = this._oldNormalLookAtTransform[key];
				if (cinemachineVirtualCameraBase != null)
				{
					cinemachineVirtualCameraBase.LookAt = lookAt;
				}
			}
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x0028F620 File Offset: 0x0028DA20
		public void SetLensSetting(LocomotionProfile.LensSettings lensSetting)
		{
			this._defaultLensSetting = lensSetting;
			this._lensSetting.FieldOfView = lensSetting.FieldOfView;
			this._lensSetting.NearClipPlane = lensSetting.NearClipPlane;
			this._lensSetting.FarClipPlane = lensSetting.FarClipPlane;
			this._lensSetting.Dutch = lensSetting.Dutch;
		}

		// Token: 0x060061DC RID: 25052 RVA: 0x0028F67C File Offset: 0x0028DA7C
		private void Reset()
		{
			this._brain = base.GetComponent<CinemachineBrain>();
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x0028F68A File Offset: 0x0028DA8A
		public bool LoadActionCameraFile(int _eventTypeID, int _poseID, Transform point = null)
		{
			if (this._mode != CameraMode.ActionFreeLook && this._mode != CameraMode.ActionNotMove)
			{
				return false;
			}
			if (this._mode == CameraMode.ActionFreeLook)
			{
				return this.LoadActionFreeCameraFile(_eventTypeID, _poseID);
			}
			return this.LoadActionNonMoveCameraFile(_eventTypeID, _poseID, point);
		}

		// Token: 0x060061DE RID: 25054 RVA: 0x0028F6C4 File Offset: 0x0028DAC4
		private bool LoadActionFreeCameraFile(int _eventTypeID, int _poseID)
		{
			Dictionary<int, ActionCameraData> dictionary;
			if (!Singleton<Manager.Resources>.Instance.Map.ActionCameraDataTable.TryGetValue(_eventTypeID, out dictionary))
			{
				this._activeVirtualCamera.LookAt.position = this.prevCamPos;
				return false;
			}
			if (!dictionary.TryGetValue(_poseID, out this._actionCameraData))
			{
				this._activeVirtualCamera.LookAt.position = this.prevCamPos;
				return false;
			}
			this._activeVirtualCamera.LookAt.position = this._actionCameraData.freePos + this.LocomotionSetting.Follow.position;
			return true;
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x0028F760 File Offset: 0x0028DB60
		private bool LoadActionNonMoveCameraFile(int _eventTypeID, int _poseID, Transform point)
		{
			CinemachineVirtualCamera component = this._activeVirtualCamera.GetComponent<CinemachineVirtualCamera>();
			CinemachineTransposer cinemachineComponent = component.GetCinemachineComponent<CinemachineTransposer>();
			Dictionary<int, ActionCameraData> dictionary;
			if (!Singleton<Manager.Resources>.Instance.Map.ActionCameraDataTable.TryGetValue(_eventTypeID, out dictionary))
			{
				this._activeVirtualCamera.transform.position = this.prevCamPos;
				this._activeVirtualCamera.transform.rotation = this.prevCamrot;
				cinemachineComponent.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
				cinemachineComponent.m_FollowOffset = this.prevCamPos - this._activeVirtualCamera.Follow.position;
				return false;
			}
			if (!dictionary.TryGetValue(_poseID, out this._actionCameraData))
			{
				this._activeVirtualCamera.transform.position = this.prevCamPos;
				this._activeVirtualCamera.transform.rotation = this.prevCamrot;
				cinemachineComponent.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
				cinemachineComponent.m_FollowOffset = this.prevCamPos - this._activeVirtualCamera.Follow.position;
				return false;
			}
			this._activeVirtualCamera.transform.position = point.rotation * this._actionCameraData.nonMovePos + this._activeVirtualCamera.Follow.position;
			this._activeVirtualCamera.transform.localRotation = Quaternion.Euler(this._actionCameraData.nonMoveRot);
			this._activeVirtualCamera.transform.rotation = point.rotation * this._activeVirtualCamera.transform.rotation;
			cinemachineComponent.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetOnAssign;
			cinemachineComponent.m_FollowOffset = this._actionCameraData.nonMovePos;
			return true;
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x0028F8FC File Offset: 0x0028DCFC
		// Note: this type is marked as 'beforefieldinit'.
		static ActorCameraControl()
		{
			Dictionary<CameraMode, Action<ActorCameraControl, CameraMode, ShotType>> dictionary = new Dictionary<CameraMode, Action<ActorCameraControl, CameraMode, ShotType>>();
			dictionary[CameraMode.Normal] = delegate(ActorCameraControl x, CameraMode y, ShotType z)
			{
				foreach (KeyValuePair<ShotType, CinemachineVirtualCameraBase> keyValuePair in x._virtualCameraNormalTable)
				{
					keyValuePair.Value.enabled = (y == CameraMode.Normal && keyValuePair.Key == z);
				}
			};
			dictionary[CameraMode.ActionFreeLook] = delegate(ActorCameraControl x, CameraMode y, ShotType z)
			{
				if (x._virtualCameraActionTable == null)
				{
					return;
				}
				foreach (KeyValuePair<ShotType, CinemachineVirtualCameraBase> keyValuePair in x._virtualCameraActionTable)
				{
					keyValuePair.Value.enabled = (y == CameraMode.ActionFreeLook && keyValuePair.Key == z);
				}
			};
			dictionary[CameraMode.Event] = delegate(ActorCameraControl x, CameraMode y, ShotType z)
			{
				if (x._eventCamera != null)
				{
					x._eventCamera.enabled = (y == CameraMode.Event);
				}
			};
			dictionary[CameraMode.H] = delegate(ActorCameraControl x, CameraMode y, ShotType z)
			{
				if (x._hCamera != null)
				{
					x._eventCamera.enabled = (y == CameraMode.H);
				}
			};
			ActorCameraControl._activationEventTable = dictionary;
		}

		// Token: 0x04005636 RID: 22070
		[SerializeField]
		private CinemachineBrain _brain;

		// Token: 0x04005637 RID: 22071
		[SerializeField]
		private CameraConfig _cameraConfig;

		// Token: 0x04005638 RID: 22072
		private Camera _cameraComponent;

		// Token: 0x04005639 RID: 22073
		[SerializeField]
		private GameScreenShot _screenShot;

		// Token: 0x0400563A RID: 22074
		[SerializeField]
		private Dictionary<ShotType, CinemachineVirtualCameraBase> _virtualCameraNormalTable = new Dictionary<ShotType, CinemachineVirtualCameraBase>();

		// Token: 0x0400563B RID: 22075
		[SerializeField]
		private Dictionary<ShotType, CinemachineVirtualCameraBase> _virtualCameraActionTable = new Dictionary<ShotType, CinemachineVirtualCameraBase>();

		// Token: 0x0400563C RID: 22076
		[SerializeField]
		private CinemachineVirtualCameraBase _actionCameraNotMove;

		// Token: 0x0400563D RID: 22077
		[SerializeField]
		private CinemachineVirtualCameraBase _advCamera;

		// Token: 0x0400563E RID: 22078
		[SerializeField]
		private CinemachineVirtualCameraBase _advNotStandCamera;

		// Token: 0x0400563F RID: 22079
		[SerializeField]
		private CinemachineVirtualCameraBase _eventCamera;

		// Token: 0x04005640 RID: 22080
		[SerializeField]
		private CinemachineVirtualCameraBase _hCamera;

		// Token: 0x04005641 RID: 22081
		[SerializeField]
		private CinemachineVirtualCameraBase _craftingCamera;

		// Token: 0x04005645 RID: 22085
		[SerializeField]
		private Transform _advNotStandRoot;

		// Token: 0x04005646 RID: 22086
		[SerializeField]
		private Animator _eventCameraLocator;

		// Token: 0x04005647 RID: 22087
		[SerializeField]
		private Transform _eventCameraParent;

		// Token: 0x04005648 RID: 22088
		[SerializeField]
		private Transform _virtualCameraRoot;

		// Token: 0x04005649 RID: 22089
		[SerializeField]
		private CrossFade _crossFade;

		// Token: 0x0400564A RID: 22090
		[SerializeField]
		private VanishControl _vanishControl;

		// Token: 0x0400564B RID: 22091
		[SerializeField]
		private ActorCameraControl.LocomotionSettingData _locomotionSetting;

		// Token: 0x0400564C RID: 22092
		[SerializeField]
		private CinemachineVirtualCameraBase _activeVirtualCamera;

		// Token: 0x0400564E RID: 22094
		private Dictionary<ShotType, CinemachineVirtualCameraBase> _activeCameraTable;

		// Token: 0x0400564F RID: 22095
		[SerializeField]
		private CustomAxisState _yAxis = new CustomAxisState(0f, 1f, false, true, 2f, 0.2f, 0.1f, ActionID.CameraVertical, "Mouse Y", false);

		// Token: 0x04005650 RID: 22096
		[SerializeField]
		private CustomAxisState.Recentering _yAxisRecentering = new CustomAxisState.Recentering(false, 1f, 2f);

		// Token: 0x04005651 RID: 22097
		[SerializeField]
		private CustomAxisState _xAxis = new CustomAxisState(-180f, 180f, true, false, 300f, 0.1f, 0.1f, ActionID.CameraHorizontal, "Mouse X", true);

		// Token: 0x04005652 RID: 22098
		[SerializeField]
		private CustomAxisState.Recentering _recenterToTargetHeading = new CustomAxisState.Recentering(true, 1f, 2f);

		// Token: 0x04005653 RID: 22099
		[SerializeField]
		private LensSettings _lensSetting = default(LensSettings);

		// Token: 0x04005654 RID: 22100
		private LocomotionProfile.LensSettings _defaultLensSetting = default(LocomotionProfile.LensSettings);

		// Token: 0x04005655 RID: 22101
		[SerializeField]
		private GameObject _charaLightNormal;

		// Token: 0x04005656 RID: 22102
		[SerializeField]
		private GameObject _charaLightCustom;

		// Token: 0x04005657 RID: 22103
		private Light _enviroLight;

		// Token: 0x04005658 RID: 22104
		public bool _updateCustomLight = true;

		// Token: 0x04005659 RID: 22105
		[SerializeField]
		private Light _normalKeyLight;

		// Token: 0x0400565A RID: 22106
		[SerializeField]
		private Light _customKeyLight;

		// Token: 0x0400565B RID: 22107
		private List<KeyCodeDownCommand> _keyCommands = new List<KeyCodeDownCommand>();

		// Token: 0x0400565C RID: 22108
		private List<KeyCodeCommand> _keyDownCommands = new List<KeyCodeCommand>();

		// Token: 0x0400565E RID: 22110
		private Subject<bool> _isBlendingChange = new Subject<bool>();

		// Token: 0x0400565F RID: 22111
		private LuxWater_UnderWaterRendering _underWaterFX;

		// Token: 0x04005660 RID: 22112
		private LuxWater_UnderWaterBlur _underWaterBlurFX;

		// Token: 0x04005661 RID: 22113
		private WetStuff _wetStuff;

		// Token: 0x04005662 RID: 22114
		[SerializeField]
		private WetDecal _wetDecal;

		// Token: 0x04005663 RID: 22115
		[SerializeField]
		private Vector3 _wetDecalOffset = Vector3.zero;

		// Token: 0x04005665 RID: 22117
		private bool _prevAmb;

		// Token: 0x04005666 RID: 22118
		private Transform _prevTarget;

		// Token: 0x04005667 RID: 22119
		private Vector3 _lastTargetPosition = Vector3.zero;

		// Token: 0x04005668 RID: 22120
		private Rigidbody _targetRigidbody;

		// Token: 0x04005669 RID: 22121
		private ActorCameraControl.HeadingTracker _headingTracker;

		// Token: 0x0400566A RID: 22122
		private CameraMode _mode;

		// Token: 0x0400566B RID: 22123
		private static Dictionary<CameraMode, Action<ActorCameraControl, CameraMode, ShotType>> _activationEventTable;

		// Token: 0x0400566C RID: 22124
		private ShotType _shotType;

		// Token: 0x0400566D RID: 22125
		private ShotType _recovShotType;

		// Token: 0x0400566E RID: 22126
		private Dictionary<ShotType, Transform> _oldNormalLookAtTransform;

		// Token: 0x0400566F RID: 22127
		public ActionCameraData _actionCameraData = default(ActionCameraData);

		// Token: 0x04005670 RID: 22128
		private Vector3 prevCamPos;

		// Token: 0x04005671 RID: 22129
		private Quaternion prevCamrot;

		// Token: 0x02000C43 RID: 3139
		[Serializable]
		public class LocomotionSettingData
		{
			// Token: 0x17001382 RID: 4994
			// (get) Token: 0x060061F0 RID: 25072 RVA: 0x0028FD40 File Offset: 0x0028E140
			public Transform Follow
			{
				[CompilerGenerated]
				get
				{
					return this._follow;
				}
			}

			// Token: 0x17001383 RID: 4995
			// (get) Token: 0x060061F1 RID: 25073 RVA: 0x0028FD48 File Offset: 0x0028E148
			public Transform LookAt
			{
				[CompilerGenerated]
				get
				{
					return this._lookAt;
				}
			}

			// Token: 0x17001384 RID: 4996
			// (get) Token: 0x060061F2 RID: 25074 RVA: 0x0028FD50 File Offset: 0x0028E150
			public Transform LookAtPOV
			{
				[CompilerGenerated]
				get
				{
					return this._lookAtPOV;
				}
			}

			// Token: 0x17001385 RID: 4997
			// (get) Token: 0x060061F3 RID: 25075 RVA: 0x0028FD58 File Offset: 0x0028E158
			public Transform ActionFollow
			{
				[CompilerGenerated]
				get
				{
					return this._actionFollow;
				}
			}

			// Token: 0x17001386 RID: 4998
			// (get) Token: 0x060061F4 RID: 25076 RVA: 0x0028FD60 File Offset: 0x0028E160
			public Transform ActionLookAt
			{
				[CompilerGenerated]
				get
				{
					return this._actionLookAt;
				}
			}

			// Token: 0x17001387 RID: 4999
			// (get) Token: 0x060061F5 RID: 25077 RVA: 0x0028FD68 File Offset: 0x0028E168
			public Transform ActionLookAtPOV
			{
				[CompilerGenerated]
				get
				{
					return this._actionLookAtPOV;
				}
			}

			// Token: 0x04005673 RID: 22131
			[Header("Normal")]
			[SerializeField]
			private Transform _follow;

			// Token: 0x04005674 RID: 22132
			[SerializeField]
			private Transform _lookAt;

			// Token: 0x04005675 RID: 22133
			[SerializeField]
			private Transform _lookAtPOV;

			// Token: 0x04005676 RID: 22134
			[Header("Action")]
			[SerializeField]
			private Transform _actionFollow;

			// Token: 0x04005677 RID: 22135
			[SerializeField]
			private Transform _actionLookAt;

			// Token: 0x04005678 RID: 22136
			[SerializeField]
			private Transform _actionLookAtPOV;
		}

		// Token: 0x02000C44 RID: 3140
		private class HeadingTracker
		{
			// Token: 0x060061F6 RID: 25078 RVA: 0x0028FD70 File Offset: 0x0028E170
			public HeadingTracker(int filterSize)
			{
				this.mHistory = new ActorCameraControl.HeadingTracker.Item[filterSize];
				float num = (float)filterSize / 5f;
				ActorCameraControl.HeadingTracker.mDecayExponent = -Mathf.Log(2f) / num;
				this.ClearHistory();
			}

			// Token: 0x17001388 RID: 5000
			// (get) Token: 0x060061F7 RID: 25079 RVA: 0x0028FDBB File Offset: 0x0028E1BB
			public int FilterSize
			{
				[CompilerGenerated]
				get
				{
					return this.mHistory.Length;
				}
			}

			// Token: 0x060061F8 RID: 25080 RVA: 0x0028FDC8 File Offset: 0x0028E1C8
			private void ClearHistory()
			{
				this.mTop = (this.mBottom = (this.mCount = 0));
				this.mWeightSum = 0f;
				this.mHeadingSum = Vector3.zero;
			}

			// Token: 0x060061F9 RID: 25081 RVA: 0x0028FE04 File Offset: 0x0028E204
			private static float Decay(float time)
			{
				return Mathf.Exp(time * ActorCameraControl.HeadingTracker.mDecayExponent);
			}

			// Token: 0x060061FA RID: 25082 RVA: 0x0028FE14 File Offset: 0x0028E214
			public void Add(Vector3 velocity)
			{
				if (this.FilterSize == 0)
				{
					this.mLastGoodHeading = velocity;
				}
				else
				{
					float magnitude = velocity.magnitude;
					if (magnitude > 0.0001f)
					{
						ActorCameraControl.HeadingTracker.Item item = default(ActorCameraControl.HeadingTracker.Item);
						item.velocity = velocity;
						item.weight = magnitude;
						item.time = Time.time;
						if (this.mCount == this.FilterSize)
						{
							this.PopBottom();
						}
						this.mCount++;
						this.mHistory[this.mTop] = item;
						if (++this.mTop == this.FilterSize)
						{
							this.mTop = 0;
						}
						this.mWeightSum *= ActorCameraControl.HeadingTracker.Decay(item.time - this.mWeightTime);
						this.mWeightTime = item.time;
						this.mWeightSum += magnitude;
						this.mHeadingSum += item.velocity;
					}
				}
			}

			// Token: 0x060061FB RID: 25083 RVA: 0x0028FF24 File Offset: 0x0028E324
			private void PopBottom()
			{
				if (this.mCount > 0)
				{
					float time = Time.time;
					ActorCameraControl.HeadingTracker.Item item = this.mHistory[this.mBottom];
					if (++this.mBottom == this.FilterSize)
					{
						this.mBottom = 0;
					}
					this.mCount--;
					float num = ActorCameraControl.HeadingTracker.Decay(time - item.time);
					this.mWeightSum -= item.weight * num;
					this.mHeadingSum -= item.velocity * num;
					if (this.mWeightSum <= 0.0001f || this.mCount == 0)
					{
						this.ClearHistory();
					}
				}
			}

			// Token: 0x060061FC RID: 25084 RVA: 0x0028FFF0 File Offset: 0x0028E3F0
			public void DecayHistory()
			{
				float time = Time.time;
				float num = ActorCameraControl.HeadingTracker.Decay(time - this.mWeightTime);
				this.mWeightSum *= num;
				this.mWeightTime = time;
				if (this.mWeightSum < 0.0001f)
				{
					this.ClearHistory();
				}
				else
				{
					this.mHeadingSum *= num;
				}
			}

			// Token: 0x060061FD RID: 25085 RVA: 0x00290054 File Offset: 0x0028E454
			public Vector3 GetReliableHeading()
			{
				if (this.mWeightSum > 0.0001f && (this.mCount == this.mHistory.Length || this.mLastGoodHeading.AlmostZero()))
				{
					Vector3 v = this.mHeadingSum / this.mWeightSum;
					if (!v.AlmostZero())
					{
						this.mLastGoodHeading = v.normalized;
					}
				}
				return this.mLastGoodHeading;
			}

			// Token: 0x04005679 RID: 22137
			private ActorCameraControl.HeadingTracker.Item[] mHistory;

			// Token: 0x0400567A RID: 22138
			private int mTop;

			// Token: 0x0400567B RID: 22139
			private int mBottom;

			// Token: 0x0400567C RID: 22140
			private int mCount;

			// Token: 0x0400567D RID: 22141
			private Vector3 mHeadingSum;

			// Token: 0x0400567E RID: 22142
			private float mWeightSum;

			// Token: 0x0400567F RID: 22143
			private float mWeightTime;

			// Token: 0x04005680 RID: 22144
			private Vector3 mLastGoodHeading = Vector3.zero;

			// Token: 0x04005681 RID: 22145
			private static float mDecayExponent;

			// Token: 0x02000C45 RID: 3141
			private struct Item
			{
				// Token: 0x04005682 RID: 22146
				public Vector3 velocity;

				// Token: 0x04005683 RID: 22147
				public float weight;

				// Token: 0x04005684 RID: 22148
				public float time;
			}
		}
	}
}
