using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Player;
using Housing;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000C0E RID: 3086
	public class JukePoint : Point, ICommandable
	{
		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06005F53 RID: 24403 RVA: 0x0028422B File Offset: 0x0028262B
		// (set) Token: 0x06005F54 RID: 24404 RVA: 0x00284233 File Offset: 0x00282633
		public override int RegisterID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06005F55 RID: 24405 RVA: 0x0028423C File Offset: 0x0028263C
		public bool IsCylinderCheck
		{
			[CompilerGenerated]
			get
			{
				return this._isCylinderCheck;
			}
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06005F56 RID: 24406 RVA: 0x00284244 File Offset: 0x00282644
		public bool EnableRangeCheck
		{
			[CompilerGenerated]
			get
			{
				return this._enableRangeCheck;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06005F57 RID: 24407 RVA: 0x0028424C File Offset: 0x0028264C
		public float RangeRadius
		{
			[CompilerGenerated]
			get
			{
				return this._rangeRadius;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06005F58 RID: 24408 RVA: 0x00284254 File Offset: 0x00282654
		public Transform CommandBasePoint
		{
			get
			{
				return (!(this._commandBasePoint != null)) ? base.transform : this._commandBasePoint;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06005F59 RID: 24409 RVA: 0x00284278 File Offset: 0x00282678
		public Transform LabelPoint
		{
			get
			{
				return (!(this._labelPoint != null)) ? base.transform : this._labelPoint;
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06005F5A RID: 24410 RVA: 0x0028429C File Offset: 0x0028269C
		public Transform SoundPlayPoint
		{
			[CompilerGenerated]
			get
			{
				return (!(this._soundPlayPoint != null)) ? base.transform : this._soundPlayPoint;
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06005F5B RID: 24411 RVA: 0x002842C0 File Offset: 0x002826C0
		// (set) Token: 0x06005F5C RID: 24412 RVA: 0x002842C8 File Offset: 0x002826C8
		public int AreaID { get; private set; } = -1;

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06005F5D RID: 24413 RVA: 0x002842D4 File Offset: 0x002826D4
		public Vector3 CommandCenter
		{
			get
			{
				Vector3 position = this.CommandBasePoint.position;
				if (!this._isCylinderCheck)
				{
					return position;
				}
				CommandArea commandArea = Map.GetCommandArea();
				if (commandArea != null && commandArea.BaseTransform != null)
				{
					position.y = commandArea.BaseTransform.position.y;
				}
				return position;
			}
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06005F5E RID: 24414 RVA: 0x00284338 File Offset: 0x00282738
		public int InstanceID
		{
			get
			{
				return ((this._instanceID == null) ? (this._instanceID = new int?(base.GetInstanceID())) : this._instanceID).Value;
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06005F5F RID: 24415 RVA: 0x0028437C File Offset: 0x0028277C
		// (set) Token: 0x06005F60 RID: 24416 RVA: 0x00284384 File Offset: 0x00282784
		public bool IsImpossible { get; private set; }

		// Token: 0x06005F61 RID: 24417 RVA: 0x0028438D File Offset: 0x0028278D
		public bool SetImpossible(bool value, Actor actor)
		{
			return true;
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06005F62 RID: 24418 RVA: 0x00284390 File Offset: 0x00282790
		public bool IsNeutralCommand
		{
			[CompilerGenerated]
			get
			{
				return !this.TutorialHideMode();
			}
		}

		// Token: 0x06005F63 RID: 24419 RVA: 0x0028439B File Offset: 0x0028279B
		private bool TutorialHideMode()
		{
			return Map.TutorialMode;
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06005F64 RID: 24420 RVA: 0x002843A2 File Offset: 0x002827A2
		public CommandLabel.CommandInfo[] Labels
		{
			get
			{
				return this._labels;
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06005F65 RID: 24421 RVA: 0x002843AA File Offset: 0x002827AA
		public CommandLabel.CommandInfo[] DateLabels
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06005F66 RID: 24422 RVA: 0x002843AD File Offset: 0x002827AD
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06005F67 RID: 24423 RVA: 0x002843BA File Offset: 0x002827BA
		public ObjectLayer Layer
		{
			[CompilerGenerated]
			get
			{
				return ObjectLayer.Command;
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06005F68 RID: 24424 RVA: 0x002843BD File Offset: 0x002827BD
		public CommandType CommandType
		{
			[CompilerGenerated]
			get
			{
				return CommandType.Forward;
			}
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x002843C0 File Offset: 0x002827C0
		private void InitializeCommandLabels()
		{
			if (this._labels != null)
			{
				return;
			}
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			CommonDefine.CommonIconGroup icon = instance.CommonDefine.Icon;
			int jukeBoxIconID = instance.CommonDefine.Icon.JukeBoxIconID;
			Sprite icon2;
			instance.itemIconTables.ActionIconTable.TryGetValue(jukeBoxIconID, out icon2);
			int index = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
			Dictionary<int, List<string>> eventPointCommandLabelTextTable = instance.Map.EventPointCommandLabelTextTable;
			List<string> source;
			eventPointCommandLabelTextTable.TryGetValue(17, out source);
			this._labels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					Text = source.GetElement(index),
					Icon = icon2,
					IsHold = true,
					TargetSpriteInfo = icon.ActionSpriteInfo,
					Transform = this.LabelPoint,
					Condition = null,
					Event = delegate
					{
						PlayerActor player = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
						if (player != null)
						{
							player.CurrentjukePoint = this;
							MapUIContainer.SetVisibleHUD(false);
							player.PlayerController.ChangeState("Idle");
							MapUIContainer.JukeBoxUI.ClosedAction = delegate()
							{
								MapUIContainer.SetVisibleHUD(true);
								player.PlayerController.ChangeState("Normal");
							};
							MapUIContainer.SetActiveJukeBoxUI(true);
						}
					}
				}
			};
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x002844B4 File Offset: 0x002828B4
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (this.TutorialHideMode())
			{
				return false;
			}
			CommandType commandType = this.CommandType;
			bool flag;
			if (commandType != CommandType.Forward)
			{
				float num = ((!this._isCylinderCheck) ? 0f : this._checkRadius) + radiusB;
				if (!this._isCylinderCheck && this._enableRangeCheck)
				{
					num += this._rangeRadius;
				}
				flag = (distance <= num);
			}
			else
			{
				float num2 = ((!this._isCylinderCheck) ? 0f : this._checkRadius) + radiusA;
				if (!this._isCylinderCheck && this._enableRangeCheck)
				{
					num2 += this._rangeRadius;
				}
				if (num2 < distance)
				{
					return false;
				}
				Vector3 position = this.CommandBasePoint.position;
				position.y = 0f;
				float num3 = angle / 2f;
				float num4 = Vector3.Angle(position - basePosition, forward);
				flag = (num4 <= num3);
			}
			if (!flag)
			{
				return false;
			}
			PlayerActor player = Map.GetPlayer();
			if (player != null)
			{
				IState state = player.PlayerController.State;
				if (state is Onbu)
				{
					return false;
				}
			}
			return flag;
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x002845F4 File Offset: 0x002829F4
		public bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB)
		{
			if (this._pathForCalc == null)
			{
				this._pathForCalc = new NavMeshPath();
			}
			bool flag = true;
			if (nmAgent.isActiveAndEnabled)
			{
				if (!this._isCylinderCheck)
				{
					flag &= nmAgent.CalculatePath(this.Position, this._pathForCalc);
					if (flag)
					{
						flag = (this._pathForCalc.status == NavMeshPathStatus.PathComplete);
					}
					if (!flag)
					{
						return false;
					}
					float num = 0f;
					Vector3[] corners = this._pathForCalc.corners;
					float num2 = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
					for (int i = 0; i < corners.Length - 1; i++)
					{
						float num3 = Vector3.Distance(corners[i], corners[i + 1]);
						num += num3;
						if (num2 < num)
						{
							flag = false;
							break;
						}
					}
				}
				else
				{
					float num4 = Mathf.Abs(nmAgent.transform.position.y - this.CommandBasePoint.position.y);
					flag = (num4 <= this._checkHeight / 2f);
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x00284728 File Offset: 0x00282B28
		public void SetAreaID()
		{
			this.AreaID = -1;
			ItemComponent componentInParent = base.GetComponentInParent<ItemComponent>();
			if (componentInParent == null)
			{
				return;
			}
			Vector3 origin = componentInParent.position + Vector3.up * 5f;
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			int num = Physics.RaycastNonAlloc(origin, Vector3.down, Point._raycastHits, 1000f, areaDetectionLayer);
			num = Mathf.Min(num, Point._raycastHits.Length);
			if (0 < num)
			{
				foreach (RaycastHit raycastHit in Point._raycastHits)
				{
					Transform transform = raycastHit.transform;
					MapArea mapArea = (transform != null) ? transform.GetComponentInParent<MapArea>() : null;
					if (mapArea != null)
					{
						this.AreaID = mapArea.AreaID;
						return;
					}
				}
			}
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x00284814 File Offset: 0x00282C14
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
				GameObject gameObject2 = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.JukeBoxPointLabelTargetName);
				this._labelPoint = ((!(gameObject2 != null)) ? base.transform : gameObject2.transform);
			}
			if (this._soundPlayPoint == null)
			{
				GameObject gameObject3 = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.JukeBoxSoundRootTargetName);
				this._soundPlayPoint = ((!(gameObject3 != null)) ? base.transform : gameObject3.transform);
			}
			this.InitializeCommandLabels();
		}

		// Token: 0x040054AE RID: 21678
		[SerializeField]
		private int _id;

		// Token: 0x040054AF RID: 21679
		[SerializeField]
		private bool _isCylinderCheck;

		// Token: 0x040054B0 RID: 21680
		[SerializeField]
		private bool _enableRangeCheck;

		// Token: 0x040054B1 RID: 21681
		[SerializeField]
		private float _rangeRadius = 1f;

		// Token: 0x040054B2 RID: 21682
		[SerializeField]
		private float _checkRadius;

		// Token: 0x040054B3 RID: 21683
		[SerializeField]
		private float _checkHeight = 1f;

		// Token: 0x040054B4 RID: 21684
		[SerializeField]
		private Transform _commandBasePoint;

		// Token: 0x040054B5 RID: 21685
		[SerializeField]
		private Transform _labelPoint;

		// Token: 0x040054B6 RID: 21686
		[SerializeField]
		private Transform _soundPlayPoint;

		// Token: 0x040054B8 RID: 21688
		private int? _instanceID;

		// Token: 0x040054BA RID: 21690
		private CommandLabel.CommandInfo[] _labels;

		// Token: 0x040054BB RID: 21691
		private NavMeshPath _pathForCalc;
	}
}
