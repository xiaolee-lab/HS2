using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B45 RID: 2885
	public class AloneButterflyHabitatPoint : SerializedMonoBehaviour
	{
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06005467 RID: 21607 RVA: 0x00253840 File Offset: 0x00251C40
		public Transform Center
		{
			[CompilerGenerated]
			get
			{
				return this._center;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06005468 RID: 21608 RVA: 0x00253848 File Offset: 0x00251C48
		public float MoveRadius
		{
			[CompilerGenerated]
			get
			{
				return this._moveRadius;
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06005469 RID: 21609 RVA: 0x00253850 File Offset: 0x00251C50
		public float MoveHeight
		{
			[CompilerGenerated]
			get
			{
				return this._moveHeight;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x0600546A RID: 21610 RVA: 0x00253858 File Offset: 0x00251C58
		public float MaxDelayTime
		{
			[CompilerGenerated]
			get
			{
				return this._maxDelayTime;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x0600546B RID: 21611 RVA: 0x00253860 File Offset: 0x00251C60
		public float MoveSpeed
		{
			[CompilerGenerated]
			get
			{
				return this._moveSpeed;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x0600546C RID: 21612 RVA: 0x00253868 File Offset: 0x00251C68
		public float AddAngle
		{
			[CompilerGenerated]
			get
			{
				return this._addAngle;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x0600546D RID: 21613 RVA: 0x00253870 File Offset: 0x00251C70
		public float TurnAngle
		{
			[CompilerGenerated]
			get
			{
				return this._turnAngle;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x0600546E RID: 21614 RVA: 0x00253878 File Offset: 0x00251C78
		public float ChangeTargetDistance
		{
			[CompilerGenerated]
			get
			{
				return this._changeTargetDistance;
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x0600546F RID: 21615 RVA: 0x00253880 File Offset: 0x00251C80
		public float NextPointMaxDistance
		{
			[CompilerGenerated]
			get
			{
				return this._nextPointMaxDistance;
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06005470 RID: 21616 RVA: 0x00253888 File Offset: 0x00251C88
		public float SpeedDownDistance
		{
			[CompilerGenerated]
			get
			{
				return this._speedDownDistance;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06005471 RID: 21617 RVA: 0x00253890 File Offset: 0x00251C90
		public bool Available
		{
			[CompilerGenerated]
			get
			{
				return this._center != null;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06005472 RID: 21618 RVA: 0x0025389E File Offset: 0x00251C9E
		public Vector2Int CreateNumRange
		{
			[CompilerGenerated]
			get
			{
				return this._createNumRange;
			}
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x002538A6 File Offset: 0x00251CA6
		private void Start()
		{
			this.Initialize();
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x002538B0 File Offset: 0x00251CB0
		private void Initialize()
		{
			if (this._prefab == null)
			{
				return;
			}
			if (this._center == null)
			{
				this._center = base.transform;
			}
			this._userMaxCount = this._createNumRange.RandomRange();
			for (int i = 0; i < this._userMaxCount; i++)
			{
				GameObject gameObject = new GameObject(string.Format("alone_butterfly_{0}", i.ToString("00")));
				gameObject.transform.SetParent(base.transform, false);
				AloneButterfly butterfly = gameObject.GetOrAddComponent<AloneButterfly>();
				butterfly.Initialize(this, this._prefab);
				butterfly.OnDestroyAsObservable().TakeUntilDestroy(this).Subscribe(delegate(Unit _)
				{
					this._use.Remove(butterfly);
				});
				this._use.Add(butterfly);
			}
		}

		// Token: 0x04004F34 RID: 20276
		private List<AloneButterfly> _use = new List<AloneButterfly>();

		// Token: 0x04004F35 RID: 20277
		[SerializeField]
		private GameObject _prefab;

		// Token: 0x04004F36 RID: 20278
		[SerializeField]
		private Transform _center;

		// Token: 0x04004F37 RID: 20279
		[SerializeField]
		private float _moveRadius = 10f;

		// Token: 0x04004F38 RID: 20280
		[SerializeField]
		private float _moveHeight = 5f;

		// Token: 0x04004F39 RID: 20281
		[SerializeField]
		private float _maxDelayTime = 1f;

		// Token: 0x04004F3A RID: 20282
		[SerializeField]
		private float _moveSpeed = 1f;

		// Token: 0x04004F3B RID: 20283
		[SerializeField]
		private float _addAngle = 90f;

		// Token: 0x04004F3C RID: 20284
		[SerializeField]
		private float _turnAngle = 170f;

		// Token: 0x04004F3D RID: 20285
		[SerializeField]
		private float _changeTargetDistance = 1f;

		// Token: 0x04004F3E RID: 20286
		[SerializeField]
		private float _nextPointMaxDistance = 5f;

		// Token: 0x04004F3F RID: 20287
		[SerializeField]
		private float _speedDownDistance = 2f;

		// Token: 0x04004F40 RID: 20288
		[SerializeField]
		private Vector2Int _createNumRange = Vector2Int.one;

		// Token: 0x04004F41 RID: 20289
		private int _userMaxCount;
	}
}
