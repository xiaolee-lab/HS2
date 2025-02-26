using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F71 RID: 3953
	public class LocomotionProfile : ScriptableObject
	{
		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x0600839B RID: 33691 RVA: 0x00371572 File Offset: 0x0036F972
		public LocomotionProfile.PlayerSpeedSetting PlayerSpeed
		{
			[CompilerGenerated]
			get
			{
				return this._playerSpeed;
			}
		}

		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x0600839C RID: 33692 RVA: 0x0037157A File Offset: 0x0036F97A
		public LocomotionProfile.AgentSpeedSetting AgentSpeed
		{
			[CompilerGenerated]
			get
			{
				return this._agentSpeed;
			}
		}

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x0600839D RID: 33693 RVA: 0x00371582 File Offset: 0x0036F982
		public LocomotionProfile.MerchantSpeedSetting MerchantSpeed
		{
			[CompilerGenerated]
			get
			{
				return this._merchantSpeed;
			}
		}

		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x0600839E RID: 33694 RVA: 0x0037158A File Offset: 0x0036F98A
		public float LerpSpeed
		{
			[CompilerGenerated]
			get
			{
				return this._lerpSpeed;
			}
		}

		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x0600839F RID: 33695 RVA: 0x00371592 File Offset: 0x0036F992
		public float TutorialLerpSpeed
		{
			[CompilerGenerated]
			get
			{
				return this._tutorialLerpSpeed;
			}
		}

		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x060083A0 RID: 33696 RVA: 0x0037159A File Offset: 0x0036F99A
		public float WalkableDistance
		{
			[CompilerGenerated]
			get
			{
				return this._walkableDistance;
			}
		}

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x060083A1 RID: 33697 RVA: 0x003715A2 File Offset: 0x0036F9A2
		public float MustRunDistance
		{
			[CompilerGenerated]
			get
			{
				return this._mustRunDistance;
			}
		}

		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x060083A2 RID: 33698 RVA: 0x003715AA File Offset: 0x0036F9AA
		public float ActionPointNavMeshSampleDistance
		{
			[CompilerGenerated]
			get
			{
				return this._actionPointNavMeshSampleDistance;
			}
		}

		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x060083A3 RID: 33699 RVA: 0x003715B2 File Offset: 0x0036F9B2
		public float ApproachDistanceActionPoint
		{
			[CompilerGenerated]
			get
			{
				return this._approachDistanceActionPoint;
			}
		}

		// Token: 0x17001BFA RID: 7162
		// (get) Token: 0x060083A4 RID: 33700 RVA: 0x003715BA File Offset: 0x0036F9BA
		public float ApproachDistanceActionPointCloser
		{
			[CompilerGenerated]
			get
			{
				return this._approachDistanceActionPointCloser;
			}
		}

		// Token: 0x17001BFB RID: 7163
		// (get) Token: 0x060083A5 RID: 33701 RVA: 0x003715C2 File Offset: 0x0036F9C2
		public float PointDistanceMargin
		{
			[CompilerGenerated]
			get
			{
				return this._pointDistanceMargin;
			}
		}

		// Token: 0x17001BFC RID: 7164
		// (get) Token: 0x060083A6 RID: 33702 RVA: 0x003715CA File Offset: 0x0036F9CA
		public float WaypointTweenMinDistance
		{
			[CompilerGenerated]
			get
			{
				return this._waypointTweenMinDistance;
			}
		}

		// Token: 0x17001BFD RID: 7165
		// (get) Token: 0x060083A7 RID: 33703 RVA: 0x003715D2 File Offset: 0x0036F9D2
		public float SearchActorRadius
		{
			[CompilerGenerated]
			get
			{
				return this._searchActorRadius;
			}
		}

		// Token: 0x17001BFE RID: 7166
		// (get) Token: 0x060083A8 RID: 33704 RVA: 0x003715DA File Offset: 0x0036F9DA
		public float AccessInvasionRange
		{
			[CompilerGenerated]
			get
			{
				return this._accessInvasionRange;
			}
		}

		// Token: 0x17001BFF RID: 7167
		// (get) Token: 0x060083A9 RID: 33705 RVA: 0x003715E2 File Offset: 0x0036F9E2
		public float CharaVisibleDistance
		{
			[CompilerGenerated]
			get
			{
				return this._charaVisibleDistance;
			}
		}

		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x060083AA RID: 33706 RVA: 0x003715EA File Offset: 0x0036F9EA
		public float EffectiveDynamicBoneDistance
		{
			[CompilerGenerated]
			get
			{
				return this._effectiveDynamicBoneDistance;
			}
		}

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x060083AB RID: 33707 RVA: 0x003715F2 File Offset: 0x0036F9F2
		public float CrossFadeEnableDistance
		{
			[CompilerGenerated]
			get
			{
				return this._crossFadeEnableDistance;
			}
		}

		// Token: 0x17001C02 RID: 7170
		// (get) Token: 0x060083AC RID: 33708 RVA: 0x003715FA File Offset: 0x0036F9FA
		public float TimeToLeftState
		{
			[CompilerGenerated]
			get
			{
				return this._timeToLeftState;
			}
		}

		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x060083AD RID: 33709 RVA: 0x00371602 File Offset: 0x0036FA02
		public int ObonEventItemID
		{
			[CompilerGenerated]
			get
			{
				return this._obonEventItemID;
			}
		}

		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x060083AE RID: 33710 RVA: 0x0037160A File Offset: 0x0036FA0A
		public float TimeToBeware
		{
			[CompilerGenerated]
			get
			{
				return this._timeToBeware;
			}
		}

		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x060083AF RID: 33711 RVA: 0x00371612 File Offset: 0x0036FA12
		public float ApproachDistanceStoryPoint
		{
			[CompilerGenerated]
			get
			{
				return this._approachDistanceStoryPoint;
			}
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x060083B0 RID: 33712 RVA: 0x0037161A File Offset: 0x0036FA1A
		public float MinDistanceDoor
		{
			[CompilerGenerated]
			get
			{
				return this._minDistanceDoor;
			}
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x060083B1 RID: 33713 RVA: 0x00371622 File Offset: 0x0036FA22
		public float MaxDistanceDoor
		{
			[CompilerGenerated]
			get
			{
				return this._maxDistanceDoor;
			}
		}

		// Token: 0x17001C08 RID: 7176
		// (get) Token: 0x060083B2 RID: 33714 RVA: 0x0037162A File Offset: 0x0036FA2A
		public string PlayerLocoItemParentName
		{
			[CompilerGenerated]
			get
			{
				return this._playerLocoItemParentName;
			}
		}

		// Token: 0x17001C09 RID: 7177
		// (get) Token: 0x060083B3 RID: 33715 RVA: 0x00371632 File Offset: 0x0036FA32
		public string AgentLocoItemParentName
		{
			[CompilerGenerated]
			get
			{
				return this._agentLocoItemParentName;
			}
		}

		// Token: 0x17001C0A RID: 7178
		// (get) Token: 0x060083B4 RID: 33716 RVA: 0x0037163A File Offset: 0x0036FA3A
		public string AgentOtherParentName
		{
			[CompilerGenerated]
			get
			{
				return this._agentOtherParentName;
			}
		}

		// Token: 0x17001C0B RID: 7179
		// (get) Token: 0x060083B5 RID: 33717 RVA: 0x00371642 File Offset: 0x0036FA42
		public string RootParentName
		{
			[CompilerGenerated]
			get
			{
				return this._rootParentName;
			}
		}

		// Token: 0x17001C0C RID: 7180
		// (get) Token: 0x060083B6 RID: 33718 RVA: 0x0037164A File Offset: 0x0036FA4A
		public string LeftHandParentName
		{
			[CompilerGenerated]
			get
			{
				return this._leftHandParentName;
			}
		}

		// Token: 0x17001C0D RID: 7181
		// (get) Token: 0x060083B7 RID: 33719 RVA: 0x00371652 File Offset: 0x0036FA52
		public string HoldingHandTarget
		{
			[CompilerGenerated]
			get
			{
				return this._holdingHandTarget;
			}
		}

		// Token: 0x17001C0E RID: 7182
		// (get) Token: 0x060083B8 RID: 33720 RVA: 0x0037165A File Offset: 0x0036FA5A
		public string HoldingElboTarget
		{
			[CompilerGenerated]
			get
			{
				return this._holdingElboTarget;
			}
		}

		// Token: 0x17001C0F RID: 7183
		// (get) Token: 0x060083B9 RID: 33721 RVA: 0x00371662 File Offset: 0x0036FA62
		public string RightHandParentName
		{
			[CompilerGenerated]
			get
			{
				return this._rightHandParentName;
			}
		}

		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x060083BA RID: 33722 RVA: 0x0037166A File Offset: 0x0036FA6A
		public string FadeLightParentName
		{
			[CompilerGenerated]
			get
			{
				return this._faceLightParentName;
			}
		}

		// Token: 0x17001C11 RID: 7185
		// (get) Token: 0x060083BB RID: 33723 RVA: 0x00371672 File Offset: 0x0036FA72
		public Vector3 FaceLightOffset
		{
			[CompilerGenerated]
			get
			{
				return this._faceLightOffset;
			}
		}

		// Token: 0x17001C12 RID: 7186
		// (get) Token: 0x060083BC RID: 33724 RVA: 0x0037167A File Offset: 0x0036FA7A
		public Vector3 EnviroEffectOffset
		{
			[CompilerGenerated]
			get
			{
				return this._enviroEffectOffset;
			}
		}

		// Token: 0x17001C13 RID: 7187
		// (get) Token: 0x060083BD RID: 33725 RVA: 0x00371682 File Offset: 0x0036FA82
		public LocomotionProfile.PhotoShotSetting PhotoShot
		{
			[CompilerGenerated]
			get
			{
				return this._photoShot;
			}
		}

		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x060083BE RID: 33726 RVA: 0x0037168A File Offset: 0x0036FA8A
		public Vector3 CommunicationOffset
		{
			[CompilerGenerated]
			get
			{
				return this._communicationOffset;
			}
		}

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x060083BF RID: 33727 RVA: 0x00371692 File Offset: 0x0036FA92
		public string CommunicationDiagonalTargetName
		{
			[CompilerGenerated]
			get
			{
				return this._communicationDiagonalTargetName;
			}
		}

		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x060083C0 RID: 33728 RVA: 0x0037169A File Offset: 0x0036FA9A
		public LocomotionProfile.LensSettings DefaultLensSetting
		{
			[CompilerGenerated]
			get
			{
				return this._defaultLensSetting;
			}
		}

		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x060083C1 RID: 33729 RVA: 0x003716A2 File Offset: 0x0036FAA2
		public Threshold CameraPowX
		{
			[CompilerGenerated]
			get
			{
				return this._cameraPowX;
			}
		}

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x060083C2 RID: 33730 RVA: 0x003716AA File Offset: 0x0036FAAA
		public Threshold CameraPowY
		{
			[CompilerGenerated]
			get
			{
				return this._cameraPowY;
			}
		}

		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x060083C3 RID: 33731 RVA: 0x003716B2 File Offset: 0x0036FAB2
		public Vector2 DefaultCameraAxisPow
		{
			[CompilerGenerated]
			get
			{
				return this._defaultCameraAxisPow;
			}
		}

		// Token: 0x17001C1A RID: 7194
		// (get) Token: 0x060083C4 RID: 33732 RVA: 0x003716BA File Offset: 0x0036FABA
		public Vector3 CameraAccelRate
		{
			[CompilerGenerated]
			get
			{
				return this._cameraAccelRate;
			}
		}

		// Token: 0x17001C1B RID: 7195
		// (get) Token: 0x060083C5 RID: 33733 RVA: 0x003716C7 File Offset: 0x0036FAC7
		public float TurnEnableAngle
		{
			[CompilerGenerated]
			get
			{
				return this._turnEnableAngle;
			}
		}

		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x060083C6 RID: 33734 RVA: 0x003716CF File Offset: 0x0036FACF
		public LocomotionProfile.HousingWaypointSettings HousingWaypointSetting
		{
			[CompilerGenerated]
			get
			{
				return this._housingWaypointSetting;
			}
		}

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x060083C7 RID: 33735 RVA: 0x003716D7 File Offset: 0x0036FAD7
		public LocomotionProfile.DropSearchActionPointSettings DropSearchActionPointSetting
		{
			[CompilerGenerated]
			get
			{
				return this._dropSearchActionPointSetting;
			}
		}

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x060083C8 RID: 33736 RVA: 0x003716DF File Offset: 0x0036FADF
		public float FishingWaterCheckDistance
		{
			[CompilerGenerated]
			get
			{
				return this._fishingWaterCheckDistance;
			}
		}

		// Token: 0x04006A05 RID: 27141
		[SerializeField]
		private LocomotionProfile.PlayerSpeedSetting _playerSpeed = default(LocomotionProfile.PlayerSpeedSetting);

		// Token: 0x04006A06 RID: 27142
		[SerializeField]
		private LocomotionProfile.AgentSpeedSetting _agentSpeed = default(LocomotionProfile.AgentSpeedSetting);

		// Token: 0x04006A07 RID: 27143
		[SerializeField]
		private LocomotionProfile.MerchantSpeedSetting _merchantSpeed = default(LocomotionProfile.MerchantSpeedSetting);

		// Token: 0x04006A08 RID: 27144
		[SerializeField]
		private float _lerpSpeed = 5f;

		// Token: 0x04006A09 RID: 27145
		[SerializeField]
		private float _tutorialLerpSpeed = 10f;

		// Token: 0x04006A0A RID: 27146
		[SerializeField]
		[MinValue(1.0)]
		private float _walkableDistance = 1f;

		// Token: 0x04006A0B RID: 27147
		[SerializeField]
		[MinValue(1.0)]
		private float _mustRunDistance = 1f;

		// Token: 0x04006A0C RID: 27148
		[SerializeField]
		private float _actionPointNavMeshSampleDistance = 1f;

		// Token: 0x04006A0D RID: 27149
		[SerializeField]
		private float _approachDistanceActionPoint = 1f;

		// Token: 0x04006A0E RID: 27150
		[SerializeField]
		private float _approachDistanceActionPointCloser = 1f;

		// Token: 0x04006A0F RID: 27151
		[SerializeField]
		[MinValue(0.0)]
		private float _pointDistanceMargin = 1f;

		// Token: 0x04006A10 RID: 27152
		[SerializeField]
		[MinValue(0.0)]
		private float _waypointTweenMinDistance = 1f;

		// Token: 0x04006A11 RID: 27153
		[SerializeField]
		private float _searchActorRadius = 1f;

		// Token: 0x04006A12 RID: 27154
		[SerializeField]
		private float _accessInvasionRange = 1f;

		// Token: 0x04006A13 RID: 27155
		[SerializeField]
		private float _charaVisibleDistance = 1f;

		// Token: 0x04006A14 RID: 27156
		[SerializeField]
		[MinValue(1.0)]
		private float _effectiveDynamicBoneDistance = 20f;

		// Token: 0x04006A15 RID: 27157
		[SerializeField]
		[MinValue(1.0)]
		private float _crossFadeEnableDistance = 300f;

		// Token: 0x04006A16 RID: 27158
		[SerializeField]
		private float _timeToLeftState = 30f;

		// Token: 0x04006A17 RID: 27159
		[SerializeField]
		private int _obonEventItemID;

		// Token: 0x04006A18 RID: 27160
		[SerializeField]
		private float _timeToBeware = 1f;

		// Token: 0x04006A19 RID: 27161
		[SerializeField]
		private float _approachDistanceStoryPoint = 1f;

		// Token: 0x04006A1A RID: 27162
		[SerializeField]
		private float _minDistanceDoor = 5f;

		// Token: 0x04006A1B RID: 27163
		[SerializeField]
		private float _maxDistanceDoor = 10f;

		// Token: 0x04006A1C RID: 27164
		[SerializeField]
		private string _playerLocoItemParentName = string.Empty;

		// Token: 0x04006A1D RID: 27165
		[SerializeField]
		private string _agentLocoItemParentName = string.Empty;

		// Token: 0x04006A1E RID: 27166
		[SerializeField]
		private string _agentOtherParentName = string.Empty;

		// Token: 0x04006A1F RID: 27167
		[SerializeField]
		private string _rootParentName = string.Empty;

		// Token: 0x04006A20 RID: 27168
		[SerializeField]
		private string _leftHandParentName = string.Empty;

		// Token: 0x04006A21 RID: 27169
		[SerializeField]
		private string _holdingHandTarget = string.Empty;

		// Token: 0x04006A22 RID: 27170
		[SerializeField]
		private string _holdingElboTarget = string.Empty;

		// Token: 0x04006A23 RID: 27171
		[SerializeField]
		private string _rightHandParentName = string.Empty;

		// Token: 0x04006A24 RID: 27172
		[SerializeField]
		private string _faceLightParentName = string.Empty;

		// Token: 0x04006A25 RID: 27173
		[SerializeField]
		private Vector3 _faceLightOffset = Vector3.zero;

		// Token: 0x04006A26 RID: 27174
		[SerializeField]
		private Vector3 _enviroEffectOffset = Vector3.zero;

		// Token: 0x04006A27 RID: 27175
		[SerializeField]
		private LocomotionProfile.PhotoShotSetting _photoShot = default(LocomotionProfile.PhotoShotSetting);

		// Token: 0x04006A28 RID: 27176
		[SerializeField]
		private Vector3 _communicationOffset = Vector3.back;

		// Token: 0x04006A29 RID: 27177
		[SerializeField]
		private string _communicationDiagonalTargetName = string.Empty;

		// Token: 0x04006A2A RID: 27178
		[SerializeField]
		private LocomotionProfile.LensSettings _defaultLensSetting = default(LocomotionProfile.LensSettings);

		// Token: 0x04006A2B RID: 27179
		[SerializeField]
		private Threshold _cameraPowX = new Threshold(0f, 1f);

		// Token: 0x04006A2C RID: 27180
		[SerializeField]
		private Threshold _cameraPowY = new Threshold(0f, 1f);

		// Token: 0x04006A2D RID: 27181
		[SerializeField]
		[Space]
		private Vector2 _defaultCameraAxisPow = new Vector2(180f, 1f);

		// Token: 0x04006A2E RID: 27182
		[SerializeField]
		private Vector2 _cameraAccelRate = new Vector2(0f, 0f);

		// Token: 0x04006A2F RID: 27183
		[SerializeField]
		private float _turnEnableAngle = 30f;

		// Token: 0x04006A30 RID: 27184
		[SerializeField]
		private LocomotionProfile.HousingWaypointSettings _housingWaypointSetting = default(LocomotionProfile.HousingWaypointSettings);

		// Token: 0x04006A31 RID: 27185
		[SerializeField]
		private LocomotionProfile.DropSearchActionPointSettings _dropSearchActionPointSetting = default(LocomotionProfile.DropSearchActionPointSettings);

		// Token: 0x04006A32 RID: 27186
		[SerializeField]
		private float _fishingWaterCheckDistance;

		// Token: 0x02000F72 RID: 3954
		[Serializable]
		public struct PlayerSpeedSetting
		{
			// Token: 0x04006A33 RID: 27187
			public float normalSpeed;

			// Token: 0x04006A34 RID: 27188
			public float walkSpeed;
		}

		// Token: 0x02000F73 RID: 3955
		[Serializable]
		public struct AgentSpeedSetting
		{
			// Token: 0x04006A35 RID: 27189
			public float walkSpeed;

			// Token: 0x04006A36 RID: 27190
			public float runSpeed;

			// Token: 0x04006A37 RID: 27191
			public float hurtSpeed;

			// Token: 0x04006A38 RID: 27192
			public float escapeSpeed;

			// Token: 0x04006A39 RID: 27193
			public float followRunSpeed;

			// Token: 0x04006A3A RID: 27194
			public float tutorialWalkSpeed;

			// Token: 0x04006A3B RID: 27195
			public float tutorialRunSpeed;
		}

		// Token: 0x02000F74 RID: 3956
		[Serializable]
		public struct MerchantSpeedSetting
		{
			// Token: 0x04006A3C RID: 27196
			public float walkSpeed;

			// Token: 0x04006A3D RID: 27197
			public float runSpeed;
		}

		// Token: 0x02000F75 RID: 3957
		[Serializable]
		public struct PhotoShotSetting
		{
			// Token: 0x04006A3E RID: 27198
			public float mouseZoomScale;

			// Token: 0x04006A3F RID: 27199
			public Vector3 maxOffset;

			// Token: 0x04006A40 RID: 27200
			public Vector3 minOffset;

			// Token: 0x04006A41 RID: 27201
			public Vector3 offsetMoveValue;
		}

		// Token: 0x02000F76 RID: 3958
		[Serializable]
		public struct LensSettings
		{
			// Token: 0x04006A42 RID: 27202
			public float FieldOfView;

			// Token: 0x04006A43 RID: 27203
			public float MinFOV;

			// Token: 0x04006A44 RID: 27204
			public float MaxFOV;

			// Token: 0x04006A45 RID: 27205
			public float NearClipPlane;

			// Token: 0x04006A46 RID: 27206
			public float FarClipPlane;

			// Token: 0x04006A47 RID: 27207
			public float Dutch;

			// Token: 0x04006A48 RID: 27208
			public float KeyZoomScale;

			// Token: 0x04006A49 RID: 27209
			public float KeyRotateScale;
		}

		// Token: 0x02000F77 RID: 3959
		[Serializable]
		public struct HousingWaypointSettings
		{
			// Token: 0x04006A4A RID: 27210
			public float InstallationDistance;

			// Token: 0x04006A4B RID: 27211
			public float InstallationHeight;

			// Token: 0x04006A4C RID: 27212
			public float ClosestEdgeDistance;

			// Token: 0x04006A4D RID: 27213
			public float SampleDistance;
		}

		// Token: 0x02000F78 RID: 3960
		[Serializable]
		public struct DropSearchActionPointSettings
		{
			// Token: 0x04006A4E RID: 27214
			public float AvailableAngle;

			// Token: 0x04006A4F RID: 27215
			public float AvailableDistance;
		}
	}
}
