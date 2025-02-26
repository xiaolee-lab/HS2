using System;
using System.Runtime.CompilerServices;
using AIProject.Player;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000C2A RID: 3114
	public class ShipPoint : Point, ICommandable
	{
		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x0600605B RID: 24667 RVA: 0x00288E26 File Offset: 0x00287226
		public Transform StartPointFromMigrate
		{
			[CompilerGenerated]
			get
			{
				return this._startPointFromMigrate;
			}
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x0600605C RID: 24668 RVA: 0x00288E2E File Offset: 0x0028722E
		public int InstanceID
		{
			get
			{
				if (this._hashCode == null)
				{
					this._hashCode = new int?(base.GetInstanceID());
				}
				return this._hashCode.Value;
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x0600605D RID: 24669 RVA: 0x00288E5C File Offset: 0x0028725C
		// (set) Token: 0x0600605E RID: 24670 RVA: 0x00288E64 File Offset: 0x00287264
		public bool IsImpossible { get; private set; }

		// Token: 0x0600605F RID: 24671 RVA: 0x00288E6D File Offset: 0x0028726D
		public bool SetImpossible(bool value, Actor actor)
		{
			return true;
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x00288E70 File Offset: 0x00287270
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (this.TutorialHideMode())
			{
				return false;
			}
			if (!Singleton<Game>.Instance.WorldData.Cleared)
			{
				return false;
			}
			if (distance > radiusA)
			{
				return false;
			}
			Vector3 position = this.Position;
			position.y = 0f;
			float num = angle / 2f;
			float num2 = Vector3.Angle(position - basePosition, forward);
			return num2 <= num;
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x00288EE0 File Offset: 0x002872E0
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

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06006062 RID: 24674 RVA: 0x00288FAD File Offset: 0x002873AD
		public bool IsNeutralCommand
		{
			get
			{
				return !this.TutorialHideMode() && Singleton<Game>.Instance.WorldData.Cleared;
			}
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x00288FD3 File Offset: 0x002873D3
		public bool TutorialHideMode()
		{
			return Map.TutorialMode;
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06006064 RID: 24676 RVA: 0x00288FE2 File Offset: 0x002873E2
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06006065 RID: 24677 RVA: 0x00288FEF File Offset: 0x002873EF
		public Vector3 CommandCenter
		{
			get
			{
				if (this._commandBasePoint != null)
				{
					return this._commandBasePoint.position;
				}
				return base.transform.position;
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06006066 RID: 24678 RVA: 0x0028901C File Offset: 0x0028741C
		public CommandLabel.CommandInfo[] Labels
		{
			get
			{
				PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
				if (playerActor != null && playerActor.PlayerController.State is Onbu)
				{
					return null;
				}
				return this._labels;
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06006067 RID: 24679 RVA: 0x0028906D File Offset: 0x0028746D
		// (set) Token: 0x06006068 RID: 24680 RVA: 0x00289075 File Offset: 0x00287475
		public CommandLabel.CommandInfo[] DateLabels { get; private set; }

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06006069 RID: 24681 RVA: 0x0028907E File Offset: 0x0028747E
		public ObjectLayer Layer { get; } = 2;

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x0600606A RID: 24682 RVA: 0x00289086 File Offset: 0x00287486
		public CommandType CommandType { get; }

		// Token: 0x0600606B RID: 24683 RVA: 0x00289090 File Offset: 0x00287490
		protected override void Start()
		{
			if (this._commandBasePoint == null)
			{
				GameObject gameObject = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.CommandTargetName);
				this._commandBasePoint = (((gameObject != null) ? gameObject.transform : null) ?? base.transform);
			}
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			DefinePack.MapGroup mapDefines = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines;
			int shipIconID = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.ShipIconID;
			Sprite icon2;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(shipIconID, out icon2);
			GameObject gameObject2 = base.transform.FindLoop(mapDefines.ShipPointLabelTargetName);
			Transform transform = ((gameObject2 != null) ? gameObject2.transform : null) ?? base.transform;
			CommandLabel.CommandInfo[] array = new CommandLabel.CommandInfo[1];
			int num = 0;
			CommandLabel.CommandInfo commandInfo = new CommandLabel.CommandInfo();
			commandInfo.Text = "船";
			commandInfo.Icon = icon2;
			commandInfo.IsHold = true;
			commandInfo.TargetSpriteInfo = icon.ActionSpriteInfo;
			commandInfo.Transform = transform;
			commandInfo.Condition = null;
			commandInfo.Event = delegate()
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
				Singleton<Map>.Instance.Player.Controller.ChangeState("ShipMenu");
			};
			array[num] = commandInfo;
			this._labels = array;
		}

		// Token: 0x04005593 RID: 21907
		[SerializeField]
		private bool _enableRangeCheck = true;

		// Token: 0x04005594 RID: 21908
		[SerializeField]
		private float _radius = 1f;

		// Token: 0x04005595 RID: 21909
		[SerializeField]
		private Transform _commandBasePoint;

		// Token: 0x04005596 RID: 21910
		[SerializeField]
		private Transform _startPointFromMigrate;

		// Token: 0x04005597 RID: 21911
		private int? _hashCode;

		// Token: 0x04005599 RID: 21913
		private NavMeshPath _pathForCalc;

		// Token: 0x0400559A RID: 21914
		private CommandLabel.CommandInfo[] _labels;
	}
}
