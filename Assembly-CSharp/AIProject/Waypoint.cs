using System;
using System.Runtime.CompilerServices;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C36 RID: 3126
	public class Waypoint : Point, IRoute
	{
		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x060060D1 RID: 24785 RVA: 0x0028AF57 File Offset: 0x00289357
		public Waypoint.ElementType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x060060D2 RID: 24786 RVA: 0x0028AF5F File Offset: 0x0028935F
		// (set) Token: 0x060060D3 RID: 24787 RVA: 0x0028AF67 File Offset: 0x00289367
		public Waypoint.AffiliationType Affiliation
		{
			get
			{
				return this._affiliation;
			}
			set
			{
				this._affiliation = value;
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x060060D4 RID: 24788 RVA: 0x0028AF70 File Offset: 0x00289370
		// (set) Token: 0x060060D5 RID: 24789 RVA: 0x0028AF78 File Offset: 0x00289378
		public int GroupID { get; set; }

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x060060D6 RID: 24790 RVA: 0x0028AF81 File Offset: 0x00289381
		// (set) Token: 0x060060D7 RID: 24791 RVA: 0x0028AF89 File Offset: 0x00289389
		public int ID { get; set; }

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x060060D8 RID: 24792 RVA: 0x0028AF92 File Offset: 0x00289392
		// (set) Token: 0x060060D9 RID: 24793 RVA: 0x0028AF9A File Offset: 0x0028939A
		public INavMeshActor Reserver { get; set; }

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x060060DA RID: 24794 RVA: 0x0028AFA3 File Offset: 0x002893A3
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x060060DB RID: 24795 RVA: 0x0028AFB0 File Offset: 0x002893B0
		public bool Available(INavMeshActor user)
		{
			return base.isActiveAndEnabled && (this.Reserver == null || this.Reserver == user);
		}

		// Token: 0x060060DC RID: 24796 RVA: 0x0028AFD6 File Offset: 0x002893D6
		protected override void OnEnable()
		{
			base.OnEnable();
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x0028AFDE File Offset: 0x002893DE
		protected override void OnDisable()
		{
		}

		// Token: 0x060060DE RID: 24798 RVA: 0x0028AFE0 File Offset: 0x002893E0
		public override void RefreshExistence()
		{
			if (!this._isManual)
			{
				base.RefreshExistence();
			}
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x0028AFF4 File Offset: 0x002893F4
		public void RefilterToActionPoint(Point[] points)
		{
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			float pointDistanceMargin = Singleton<Manager.Resources>.Instance.LocomotionProfile.PointDistanceMargin;
			Vector3 position = base.transform.position;
			foreach (Point point in points)
			{
				float num = Vector3.Distance(position, point.transform.position);
				if (num < pointDistanceMargin)
				{
					base.gameObject.SetActive(false);
					return;
				}
			}
		}

		// Token: 0x040055D5 RID: 21973
		[SerializeField]
		[Tooltip("手動操作か？")]
		private bool _isManual;

		// Token: 0x040055D6 RID: 21974
		[SerializeField]
		private Waypoint.ElementType _type = (Waypoint.ElementType)(-1);

		// Token: 0x040055D7 RID: 21975
		[SerializeField]
		private Waypoint.AffiliationType _affiliation = Waypoint.AffiliationType.Map;

		// Token: 0x02000C37 RID: 3127
		[Flags]
		public enum ElementType
		{
			// Token: 0x040055DC RID: 21980
			HalfWay = 1,
			// Token: 0x040055DD RID: 21981
			Destination = 2
		}

		// Token: 0x02000C38 RID: 3128
		[Flags]
		public enum AffiliationType
		{
			// Token: 0x040055DF RID: 21983
			Map = 1,
			// Token: 0x040055E0 RID: 21984
			Housing = 2,
			// Token: 0x040055E1 RID: 21985
			Item = 4
		}
	}
}
