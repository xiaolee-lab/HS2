using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F7A RID: 3962
	public class PlayerProfile : SerializedScriptableObject
	{
		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x060083D5 RID: 33749 RVA: 0x003717E6 File Offset: 0x0036FBE6
		public int DefaultInventoryMax
		{
			[CompilerGenerated]
			get
			{
				return this._defaultInventoryMaxSlot;
			}
		}

		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x060083D6 RID: 33750 RVA: 0x003717EE File Offset: 0x0036FBEE
		public int AgentInventoryMaxSlot
		{
			[CompilerGenerated]
			get
			{
				return this._agentInventoryMaxSlot;
			}
		}

		// Token: 0x17001C2B RID: 7211
		// (get) Token: 0x060083D7 RID: 33751 RVA: 0x003717F6 File Offset: 0x0036FBF6
		public int CommonActionIconID
		{
			[CompilerGenerated]
			get
			{
				return this._commonActionIconID;
			}
		}

		// Token: 0x17001C2C RID: 7212
		// (get) Token: 0x060083D8 RID: 33752 RVA: 0x003717FE File Offset: 0x0036FBFE
		public PlayerProfile.PoseIDCollection PoseIDData
		{
			[CompilerGenerated]
			get
			{
				return this._poseIDData;
			}
		}

		// Token: 0x17001C2D RID: 7213
		// (get) Token: 0x060083D9 RID: 33753 RVA: 0x00371806 File Offset: 0x0036FC06
		public List<int> ExPantryCommandActPTIDs
		{
			[CompilerGenerated]
			get
			{
				return this._exPantryCommandActPTIDs;
			}
		}

		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x060083DA RID: 33754 RVA: 0x0037180E File Offset: 0x0036FC0E
		public Dictionary<int, int[]> DisableWaterVFXAreaList
		{
			[CompilerGenerated]
			get
			{
				return this._disableWaterVFXAreaList;
			}
		}

		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x060083DB RID: 33755 RVA: 0x00371816 File Offset: 0x0036FC16
		public EnvironmentSimulator.DateTimeThreshold[] CanSleepTime
		{
			[CompilerGenerated]
			get
			{
				return this._canSleepTime;
			}
		}

		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x060083DC RID: 33756 RVA: 0x0037181E File Offset: 0x0036FC1E
		public EnvironmentSimulator.DateTimeSerialization WakeTime
		{
			[CompilerGenerated]
			get
			{
				return this._wakeTime;
			}
		}

		// Token: 0x17001C31 RID: 7217
		// (get) Token: 0x060083DD RID: 33757 RVA: 0x00371826 File Offset: 0x0036FC26
		public int HizamakuraPTID
		{
			[CompilerGenerated]
			get
			{
				return this._hizamakuraPTID;
			}
		}

		// Token: 0x04006A5A RID: 27226
		[SerializeField]
		private int _defaultInventoryMaxSlot = 1;

		// Token: 0x04006A5B RID: 27227
		[SerializeField]
		private int _agentInventoryMaxSlot = 1;

		// Token: 0x04006A5C RID: 27228
		[SerializeField]
		private int _commonActionIconID;

		// Token: 0x04006A5D RID: 27229
		[SerializeField]
		private PlayerProfile.PoseIDCollection _poseIDData;

		// Token: 0x04006A5E RID: 27230
		[SerializeField]
		private List<int> _exPantryCommandActPTIDs = new List<int>();

		// Token: 0x04006A5F RID: 27231
		[SerializeField]
		private Dictionary<int, int[]> _disableWaterVFXAreaList = new Dictionary<int, int[]>();

		// Token: 0x04006A60 RID: 27232
		[SerializeField]
		[DisableInPlayMode]
		private EnvironmentSimulator.DateTimeThreshold[] _canSleepTime;

		// Token: 0x04006A61 RID: 27233
		[SerializeField]
		[DisableInPlayMode]
		private EnvironmentSimulator.DateTimeSerialization _wakeTime = default(EnvironmentSimulator.DateTimeSerialization);

		// Token: 0x04006A62 RID: 27234
		[SerializeField]
		[DisableInPlayMode]
		private int _hizamakuraPTID;

		// Token: 0x02000F7B RID: 3963
		[Serializable]
		public class PoseIDCollection
		{
			// Token: 0x17001C32 RID: 7218
			// (get) Token: 0x060083DF RID: 33759 RVA: 0x0037187B File Offset: 0x0036FC7B
			public PoseKeyPair LeftPoseID
			{
				[CompilerGenerated]
				get
				{
					return this._leftPoseID;
				}
			}

			// Token: 0x17001C33 RID: 7219
			// (get) Token: 0x060083E0 RID: 33760 RVA: 0x00371883 File Offset: 0x0036FC83
			public PoseKeyPair MenuPoseID
			{
				[CompilerGenerated]
				get
				{
					return this._menuPoseID;
				}
			}

			// Token: 0x17001C34 RID: 7220
			// (get) Token: 0x060083E1 RID: 33761 RVA: 0x0037188B File Offset: 0x0036FC8B
			public PoseKeyPair WakeupPoseID
			{
				[CompilerGenerated]
				get
				{
					return this._wakeupPoseID;
				}
			}

			// Token: 0x17001C35 RID: 7221
			// (get) Token: 0x060083E2 RID: 33762 RVA: 0x00371893 File Offset: 0x0036FC93
			public string NormalLocoStateName
			{
				[CompilerGenerated]
				get
				{
					return this._normalLocoStateName;
				}
			}

			// Token: 0x17001C36 RID: 7222
			// (get) Token: 0x060083E3 RID: 33763 RVA: 0x0037189B File Offset: 0x0036FC9B
			public int TorchLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._torchLocoID;
				}
			}

			// Token: 0x17001C37 RID: 7223
			// (get) Token: 0x060083E4 RID: 33764 RVA: 0x003718A3 File Offset: 0x0036FCA3
			public int LampLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._lampLocoID;
				}
			}

			// Token: 0x17001C38 RID: 7224
			// (get) Token: 0x060083E5 RID: 33765 RVA: 0x003718AB File Offset: 0x0036FCAB
			public int TorchOnbuLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._torchOnbuLocoID;
				}
			}

			// Token: 0x17001C39 RID: 7225
			// (get) Token: 0x060083E6 RID: 33766 RVA: 0x003718B3 File Offset: 0x0036FCB3
			public int LampOnbuLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._lampOnbuLocoID;
				}
			}

			// Token: 0x04006A63 RID: 27235
			[SerializeField]
			private PoseKeyPair _leftPoseID = default(PoseKeyPair);

			// Token: 0x04006A64 RID: 27236
			[SerializeField]
			private PoseKeyPair _menuPoseID = default(PoseKeyPair);

			// Token: 0x04006A65 RID: 27237
			[SerializeField]
			private PoseKeyPair _wakeupPoseID = default(PoseKeyPair);

			// Token: 0x04006A66 RID: 27238
			[SerializeField]
			private string _normalLocoStateName = string.Empty;

			// Token: 0x04006A67 RID: 27239
			[SerializeField]
			private int _torchLocoID;

			// Token: 0x04006A68 RID: 27240
			[SerializeField]
			private int _lampLocoID;

			// Token: 0x04006A69 RID: 27241
			[SerializeField]
			private int _torchOnbuLocoID;

			// Token: 0x04006A6A RID: 27242
			[SerializeField]
			private int _lampOnbuLocoID;
		}
	}
}
