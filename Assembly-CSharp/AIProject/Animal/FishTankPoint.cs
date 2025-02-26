using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.Animal
{
	// Token: 0x02000BA2 RID: 2978
	public class FishTankPoint : Point, ICommandable
	{
		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x060058E3 RID: 22755 RVA: 0x00262B3F File Offset: 0x00260F3F
		// (set) Token: 0x060058E4 RID: 22756 RVA: 0x00262B47 File Offset: 0x00260F47
		public PetFish Fish { get; set; }

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x060058E5 RID: 22757 RVA: 0x00262B50 File Offset: 0x00260F50
		// (set) Token: 0x060058E6 RID: 22758 RVA: 0x00262B58 File Offset: 0x00260F58
		public int TankID { get; private set; } = -1;

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x060058E7 RID: 22759 RVA: 0x00262B61 File Offset: 0x00260F61
		// (set) Token: 0x060058E8 RID: 22760 RVA: 0x00262B69 File Offset: 0x00260F69
		public int ItemID { get; private set; } = -1;

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x060058E9 RID: 22761 RVA: 0x00262B74 File Offset: 0x00260F74
		public int InstanceID
		{
			get
			{
				return ((this._instanceID == null) ? (this._instanceID = new int?(base.GetInstanceID())) : this._instanceID).Value;
			}
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x00262BB8 File Offset: 0x00260FB8
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			Vector3 commandCenter = this.CommandCenter;
			commandCenter.y = 0f;
			float num = Vector3.Distance(basePosition, commandCenter);
			CommandType commandType = this._commandType;
			if (commandType != CommandType.Forward)
			{
				float num2 = (!this._enableRangeCheck) ? radiusB : (radiusB + this._checkRadius);
				return distance <= num2;
			}
			float num3 = (!this._enableRangeCheck) ? radiusA : (radiusA + this._checkRadius);
			if (num3 < num)
			{
				return false;
			}
			Vector3 a = commandCenter;
			a.y = 0f;
			float num4 = angle / 2f;
			float num5 = Vector3.Angle(a - basePosition, forward);
			return num5 <= num4;
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x00262C70 File Offset: 0x00261070
		public bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB)
		{
			if (this._pathForCalc == null)
			{
				this._pathForCalc = new NavMeshPath();
			}
			bool flag = true;
			if (nmAgent.isActiveAndEnabled)
			{
				nmAgent.CalculatePath(this.Position, this._pathForCalc);
				flag &= (this._pathForCalc.status == NavMeshPathStatus.PathComplete);
				float num = 0f;
				Vector3[] corners = this._pathForCalc.corners;
				for (int i = 0; i < corners.Length - 1; i++)
				{
					float num2 = Vector3.Distance(corners[i], corners[i + 1]);
					num += num2;
					float num3 = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
					if (num > num3)
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x060058EC RID: 22764 RVA: 0x00262D3D File Offset: 0x0026113D
		// (set) Token: 0x060058ED RID: 22765 RVA: 0x00262D45 File Offset: 0x00261145
		public bool IsImpossible { get; protected set; }

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x060058EE RID: 22766 RVA: 0x00262D4E File Offset: 0x0026114E
		// (set) Token: 0x060058EF RID: 22767 RVA: 0x00262D56 File Offset: 0x00261156
		public Actor CommandPartner { get; set; }

		// Token: 0x060058F0 RID: 22768 RVA: 0x00262D60 File Offset: 0x00261160
		public bool SetImpossible(bool _value, Actor _actor)
		{
			if (this.IsImpossible == _value)
			{
				return false;
			}
			if (_value && this.CommandPartner != null)
			{
				return false;
			}
			this.IsImpossible = _value;
			this.CommandPartner = ((!_value) ? null : _actor);
			return true;
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x060058F1 RID: 22769 RVA: 0x00262DAF File Offset: 0x002611AF
		public bool IsNeutralCommand
		{
			[CompilerGenerated]
			get
			{
				return true;
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x060058F2 RID: 22770 RVA: 0x00262DB2 File Offset: 0x002611B2
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x060058F3 RID: 22771 RVA: 0x00262DBF File Offset: 0x002611BF
		public Vector3 CommandCenter
		{
			get
			{
				return (!(this._commandBasePoint != null)) ? base.transform.position : this._commandBasePoint.position;
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x060058F4 RID: 22772 RVA: 0x00262DED File Offset: 0x002611ED
		// (set) Token: 0x060058F5 RID: 22773 RVA: 0x00262DF5 File Offset: 0x002611F5
		public CommandLabel.CommandInfo[] Labels { get; private set; }

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x060058F6 RID: 22774 RVA: 0x00262DFE File Offset: 0x002611FE
		public CommandLabel.CommandInfo[] DateLabels
		{
			[CompilerGenerated]
			get
			{
				return this.emptyLabel;
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x060058F7 RID: 22775 RVA: 0x00262E06 File Offset: 0x00261206
		public ObjectLayer Layer
		{
			[CompilerGenerated]
			get
			{
				return ObjectLayer.Command;
			}
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x060058F8 RID: 22776 RVA: 0x00262E09 File Offset: 0x00261209
		public CommandType CommandType
		{
			[CompilerGenerated]
			get
			{
				return this._commandType;
			}
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x060058F9 RID: 22777 RVA: 0x00262E11 File Offset: 0x00261211
		public Transform CommandBasePoint
		{
			get
			{
				return this._commandBasePoint ?? base.transform;
			}
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x060058FA RID: 22778 RVA: 0x00262E26 File Offset: 0x00261226
		public Transform LabelPoint
		{
			get
			{
				return this._labelPoint ?? base.transform;
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x060058FB RID: 22779 RVA: 0x00262E3B File Offset: 0x0026123B
		public bool EnableRangeCheck
		{
			[CompilerGenerated]
			get
			{
				return this._enableRangeCheck;
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x00262E43 File Offset: 0x00261243
		public float CheckRadius
		{
			[CompilerGenerated]
			get
			{
				return this._checkRadius;
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x060058FD RID: 22781 RVA: 0x00262E4B File Offset: 0x0026124B
		public int AllowableSizeID
		{
			[CompilerGenerated]
			get
			{
				return this._allowableSizeID;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060058FE RID: 22782 RVA: 0x00262E53 File Offset: 0x00261253
		public Transform[] SizeRootPoints
		{
			[CompilerGenerated]
			get
			{
				return this._sizeRootPoints;
			}
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x00262E5C File Offset: 0x0026125C
		protected override void Start()
		{
			base.Start();
			if (this._commandBasePoint == null)
			{
				GameObject gameObject = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.CommandTargetName);
				this._commandBasePoint = ((!(gameObject != null)) ? base.transform : gameObject.transform);
			}
			if (this._labelPoint == null)
			{
			}
			if (this._sizeRootPoints.IsNullOrEmpty<Transform>())
			{
				this.SetSizeRootPoints();
			}
			this.InitializeCommandLabels();
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x00262EF0 File Offset: 0x002612F0
		protected override void OnEnable()
		{
			base.OnEnable();
			this.AddCommandableObject();
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x00262EFE File Offset: 0x002612FE
		protected override void OnDisable()
		{
			this.RemoveCommandableObject();
			base.OnDisable();
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x00262F0C File Offset: 0x0026130C
		private void InitializeCommandLabels()
		{
			if (this.Labels.IsNullOrEmpty<CommandLabel.CommandInfo>())
			{
				CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
				Sprite icon2 = null;
				Manager.Resources instance = Singleton<Manager.Resources>.Instance;
				Map instance2 = Singleton<Map>.Instance;
				instance.itemIconTables.ActionIconTable.TryGetValue(icon.FishTankIconID, out icon2);
				CommandLabel.CommandInfo[] array = new CommandLabel.CommandInfo[1];
				int num = 0;
				CommandLabel.CommandInfo commandInfo = new CommandLabel.CommandInfo();
				commandInfo.Text = "水槽";
				commandInfo.Icon = icon2;
				commandInfo.IsHold = true;
				commandInfo.TargetSpriteInfo = icon.ActionSpriteInfo;
				commandInfo.Transform = this.LabelPoint;
				commandInfo.Condition = null;
				commandInfo.Event = delegate()
				{
				};
				array[num] = commandInfo;
				this.Labels = array;
			}
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x00262FDC File Offset: 0x002613DC
		private void AddCommandableObject()
		{
			PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
			CommandArea commandArea;
			if (playerActor == null)
			{
				commandArea = null;
			}
			else
			{
				PlayerController playerController = playerActor.PlayerController;
				commandArea = ((playerController != null) ? playerController.CommandArea : null);
			}
			CommandArea commandArea2 = commandArea;
			if (commandArea2 == null)
			{
				return;
			}
			commandArea2.AddCommandableObject(this);
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x00263038 File Offset: 0x00261438
		private void RemoveCommandableObject()
		{
			PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
			CommandArea commandArea;
			if (playerActor == null)
			{
				commandArea = null;
			}
			else
			{
				PlayerController playerController = playerActor.PlayerController;
				commandArea = ((playerController != null) ? playerController.CommandArea : null);
			}
			CommandArea commandArea2 = commandArea;
			if (commandArea2 == null)
			{
				return;
			}
			bool flag = commandArea2.ContainsConsiderationObject(this);
			commandArea2.RemoveCommandableObject(this);
			if (flag)
			{
				commandArea2.RefreshCommands();
			}
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x002630A8 File Offset: 0x002614A8
		private void SetSizeRootPoints()
		{
			List<Transform> list = ListPool<Transform>.Get();
			base.transform.FindMatchLoop("^fish_point_[0-9]+$", ref list);
			if (!list.IsNullOrEmpty<Transform>())
			{
				Dictionary<int, Transform> dictionary = DictionaryPool<int, Transform>.Get();
				int num = 0;
				for (int i = 0; i < list.Count; i++)
				{
					Transform transform = list[i];
					string name = transform.name;
					Match match = Regex.Match(name, "[0-9]+");
					if (match.Success)
					{
						string value = match.Value;
						int num2;
						if (int.TryParse(value, out num2))
						{
							dictionary[num2] = transform;
							num = Mathf.Max(num, num2 + 1);
						}
					}
				}
				this._sizeRootPoints = new Transform[num];
				foreach (KeyValuePair<int, Transform> keyValuePair in dictionary)
				{
					this._sizeRootPoints[keyValuePair.Key] = keyValuePair.Value;
				}
				DictionaryPool<int, Transform>.Release(dictionary);
			}
			ListPool<Transform>.Release(list);
		}

		// Token: 0x0400517D RID: 20861
		private int? _instanceID;

		// Token: 0x0400517E RID: 20862
		private NavMeshPath _pathForCalc;

		// Token: 0x04005181 RID: 20865
		private CommandLabel.CommandInfo[] emptyLabel = new CommandLabel.CommandInfo[0];

		// Token: 0x04005183 RID: 20867
		[SerializeField]
		private CommandType _commandType;

		// Token: 0x04005184 RID: 20868
		[SerializeField]
		private Transform _commandBasePoint;

		// Token: 0x04005185 RID: 20869
		[SerializeField]
		private Transform _labelPoint;

		// Token: 0x04005186 RID: 20870
		[SerializeField]
		private bool _enableRangeCheck = true;

		// Token: 0x04005187 RID: 20871
		[SerializeField]
		private float _checkRadius = 1f;

		// Token: 0x04005188 RID: 20872
		[SerializeField]
		private int _allowableSizeID;

		// Token: 0x04005189 RID: 20873
		[SerializeField]
		private Transform[] _sizeRootPoints = new Transform[0];
	}
}
