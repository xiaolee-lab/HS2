using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Player;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000BEE RID: 3054
	public class BasePoint : Point, ICommandable
	{
		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06005D4B RID: 23883 RVA: 0x00276DCE File Offset: 0x002751CE
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this._id;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06005D4C RID: 23884 RVA: 0x00276DD6 File Offset: 0x002751D6
		public int AreaIDInHousing
		{
			[CompilerGenerated]
			get
			{
				return this._areaIDInHousing;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06005D4D RID: 23885 RVA: 0x00276DDE File Offset: 0x002751DE
		public float Radius
		{
			[CompilerGenerated]
			get
			{
				return this._radius;
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06005D4E RID: 23886 RVA: 0x00276DE6 File Offset: 0x002751E6
		public bool IsHousing
		{
			[CompilerGenerated]
			get
			{
				return this._isHousing;
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06005D4F RID: 23887 RVA: 0x00276DEE File Offset: 0x002751EE
		public Transform WarpPoint
		{
			[CompilerGenerated]
			get
			{
				return this._warpPoint;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06005D50 RID: 23888 RVA: 0x00276DF6 File Offset: 0x002751F6
		public List<Transform> RecoverPoints
		{
			[CompilerGenerated]
			get
			{
				return this._recoverPoints;
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06005D51 RID: 23889 RVA: 0x00276DFE File Offset: 0x002751FE
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

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06005D52 RID: 23890 RVA: 0x00276E2C File Offset: 0x0027522C
		// (set) Token: 0x06005D53 RID: 23891 RVA: 0x00276E34 File Offset: 0x00275234
		public bool IsImpossible { get; private set; }

		// Token: 0x06005D54 RID: 23892 RVA: 0x00276E3D File Offset: 0x0027523D
		public bool SetImpossible(bool value, Actor actor)
		{
			return true;
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x00276E40 File Offset: 0x00275240
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (this.TutorialHideMode())
			{
				return false;
			}
			if (distance > radiusA)
			{
				return false;
			}
			Vector3 commandCenter = this.CommandCenter;
			commandCenter.y = 0f;
			float num = angle / 2f;
			float num2 = Vector3.Angle(commandCenter - basePosition, forward);
			return num2 <= num;
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x00276E98 File Offset: 0x00275298
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

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06005D57 RID: 23895 RVA: 0x00276F65 File Offset: 0x00275365
		public bool IsNeutralCommand
		{
			get
			{
				return !this.TutorialHideMode();
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06005D58 RID: 23896 RVA: 0x00276F75 File Offset: 0x00275375
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06005D59 RID: 23897 RVA: 0x00276F82 File Offset: 0x00275382
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

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06005D5A RID: 23898 RVA: 0x00276FAC File Offset: 0x002753AC
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

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06005D5B RID: 23899 RVA: 0x00276FFD File Offset: 0x002753FD
		// (set) Token: 0x06005D5C RID: 23900 RVA: 0x00277005 File Offset: 0x00275405
		public CommandLabel.CommandInfo[] DateLabels { get; private set; }

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06005D5D RID: 23901 RVA: 0x0027700E File Offset: 0x0027540E
		public ObjectLayer Layer { get; } = 2;

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06005D5E RID: 23902 RVA: 0x00277016 File Offset: 0x00275416
		public CommandType CommandType { get; }

		// Token: 0x06005D5F RID: 23903 RVA: 0x00277020 File Offset: 0x00275420
		protected override void Start()
		{
			if (this._commandBasePoint == null)
			{
				GameObject gameObject = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.CommandTargetName);
				this._commandBasePoint = (((gameObject != null) ? gameObject.transform : null) ?? base.transform);
			}
			if (this._warpPoint == null)
			{
				GameObject gameObject2 = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.BasePointWarpTargetName);
				this._warpPoint = (((gameObject2 != null) ? gameObject2.transform : null) ?? base.transform);
			}
			if (this._housingCenter == null)
			{
				GameObject gameObject3 = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.HousingCenterTargetName);
				this._housingCenter = (((gameObject3 != null) ? gameObject3.transform : null) ?? base.transform);
			}
			if (this._recoverPoints.IsNullOrEmpty<Transform>() || this._recoverPoints.Count < 4)
			{
				this._recoverPoints.Clear();
				string[] devicePointRecoveryTargetNames = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.DevicePointRecoveryTargetNames;
				foreach (string name in devicePointRecoveryTargetNames)
				{
					GameObject gameObject4 = base.transform.FindLoop(name);
					if (gameObject4 != null)
					{
						this._recoverPoints.Add(gameObject4.transform);
					}
				}
			}
			Singleton<Map>.Instance.HousingRecoveryPointTable[this._id] = this._recoverPoints;
			if (this._isHousing)
			{
				Singleton<Map>.Instance.HousingPointTable[this._id] = this._housingCenter;
			}
			if (Singleton<Game>.Instance.WorldData.Cleared && this._id >= 3)
			{
				Singleton<Map>.Instance.SetBaseOpenState(this._id, true);
			}
			base.Start();
			if (!this._isHousing)
			{
				return;
			}
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			DefinePack.MapGroup mapDefines = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines;
			int baseIconID = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.BaseIconID;
			Sprite icon2;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(baseIconID, out icon2);
			GameObject gameObject5 = base.transform.FindLoop(mapDefines.BasePointLabelTargetName);
			Transform transform = ((gameObject5 != null) ? gameObject5.transform : null) ?? base.transform;
			this._labels = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					Text = "拠点",
					Icon = icon2,
					IsHold = true,
					TargetSpriteInfo = icon.ActionSpriteInfo,
					Transform = transform,
					Condition = null,
					Event = delegate
					{
						if (Singleton<Map>.Instance.SetBaseOpenState(this._id, true))
						{
							string arg;
							if (Singleton<Manager.Resources>.Instance.itemIconTables.BaseName.TryGetValue(this._id, out arg))
							{
								MapUIContainer.AddSystemLog(string.Format("{0} に移動できるようになりました。", arg), true);
							}
							else
							{
								MapUIContainer.AddSystemLog(string.Format("拠点{0}に移動できるようになりました。", this._id), true);
							}
						}
						if (Map.GetTutorialProgress() == 10)
						{
							Map.SetTutorialProgress(11);
						}
						MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
						Singleton<Map>.Instance.HousingID = this._id;
						Singleton<Map>.Instance.HousingAreaID = this._areaIDInHousing;
						Singleton<Map>.Instance.Player.Controller.ChangeState("BaseMenu");
					}
				}
			};
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x00277328 File Offset: 0x00275728
		public bool TutorialHideMode()
		{
			if (Map.TutorialMode)
			{
				int num = 10;
				int num2 = 12;
				int tutorialProgress = Map.GetTutorialProgress();
				bool flag = num <= tutorialProgress && tutorialProgress <= num2;
				return !flag;
			}
			return false;
		}

		// Token: 0x0400539D RID: 21405
		[SerializeField]
		private int _id;

		// Token: 0x0400539E RID: 21406
		[SerializeField]
		private int _areaIDInHousing;

		// Token: 0x0400539F RID: 21407
		[SerializeField]
		private bool _enabledRangeCheck = true;

		// Token: 0x040053A0 RID: 21408
		[SerializeField]
		private float _radius = 1f;

		// Token: 0x040053A1 RID: 21409
		[SerializeField]
		private bool _isHousing = true;

		// Token: 0x040053A2 RID: 21410
		[SerializeField]
		private Transform _commandBasePoint;

		// Token: 0x040053A3 RID: 21411
		[SerializeField]
		private Transform _housingCenter;

		// Token: 0x040053A4 RID: 21412
		[SerializeField]
		private Transform _warpPoint;

		// Token: 0x040053A5 RID: 21413
		[SerializeField]
		private List<Transform> _recoverPoints = new List<Transform>();

		// Token: 0x040053A6 RID: 21414
		private int? _hashCode;

		// Token: 0x040053A8 RID: 21416
		private NavMeshPath _pathForCalc;

		// Token: 0x040053A9 RID: 21417
		private CommandLabel.CommandInfo[] _labels;
	}
}
