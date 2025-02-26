using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B95 RID: 2965
	public class AnimalSearchActionPoint : MonoBehaviour
	{
		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06005852 RID: 22610 RVA: 0x0025F75C File Offset: 0x0025DB5C
		public float VisibleDistance
		{
			[CompilerGenerated]
			get
			{
				return this._visibleDistance;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06005853 RID: 22611 RVA: 0x0025F764 File Offset: 0x0025DB64
		public float VisibleHeight
		{
			[CompilerGenerated]
			get
			{
				return this._visibleHeight;
			}
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06005854 RID: 22612 RVA: 0x0025F76C File Offset: 0x0025DB6C
		public float VisibleAngle
		{
			[CompilerGenerated]
			get
			{
				return this._visibleAngle;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06005855 RID: 22613 RVA: 0x0025F774 File Offset: 0x0025DB74
		// (set) Token: 0x06005856 RID: 22614 RVA: 0x0025F77C File Offset: 0x0025DB7C
		public List<AnimalActionPoint> SearchPoints { get; private set; } = new List<AnimalActionPoint>();

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06005857 RID: 22615 RVA: 0x0025F785 File Offset: 0x0025DB85
		// (set) Token: 0x06005858 RID: 22616 RVA: 0x0025F78D File Offset: 0x0025DB8D
		public List<AnimalActionPoint> VisibleList { get; private set; } = new List<AnimalActionPoint>();

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06005859 RID: 22617 RVA: 0x0025F796 File Offset: 0x0025DB96
		// (set) Token: 0x0600585A RID: 22618 RVA: 0x0025F79E File Offset: 0x0025DB9E
		public bool SearchEnabled { get; set; } = true;

		// Token: 0x0600585B RID: 22619 RVA: 0x0025F7A8 File Offset: 0x0025DBA8
		private void Start()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDisable(base.gameObject)
			where base.isActiveAndEnabled
			where this.SearchEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x0600585C RID: 22620 RVA: 0x0025F7F9 File Offset: 0x0025DBF9
		public void SetSearchEnabled(bool _enabled, bool _clearCollision = true)
		{
			if (_enabled == this.SearchEnabled)
			{
				return;
			}
			this.SearchEnabled = _enabled;
			if (!this.SearchEnabled && _clearCollision)
			{
				this.ClearCollisionState();
			}
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x0025F828 File Offset: 0x0025DC28
		private void OnUpdate()
		{
			if (this.animal == null)
			{
				return;
			}
			Transform transform = this.animal.transform;
			foreach (AnimalActionPoint animalActionPoint in this.SearchPoints)
			{
				if (!(animalActionPoint == null))
				{
					int instanceID = animalActionPoint.InstanceID;
					CollisionState collisionState;
					if (!this.collisionStateTable.TryGetValue(instanceID, out collisionState))
					{
						CollisionState collisionState2 = CollisionState.None;
						this.collisionStateTable[instanceID] = collisionState2;
						collisionState = collisionState2;
					}
					bool flag = animalActionPoint.gameObject.activeSelf && this.animal.CheckTargetOnArea(animalActionPoint.Destination, this._visibleDistance, this._visibleHeight, this._visibleAngle);
					if (flag)
					{
						switch (collisionState)
						{
						case CollisionState.None:
						case CollisionState.Exit:
							this.collisionStateTable[instanceID] = CollisionState.Enter;
							this.OnEnter(animalActionPoint);
							break;
						case CollisionState.Enter:
						case CollisionState.Stay:
							this.collisionStateTable[instanceID] = CollisionState.Stay;
							break;
						}
					}
					else
					{
						switch (collisionState)
						{
						case CollisionState.None:
						case CollisionState.Exit:
							this.collisionStateTable[instanceID] = CollisionState.None;
							break;
						case CollisionState.Enter:
						case CollisionState.Stay:
							this.collisionStateTable[instanceID] = CollisionState.Exit;
							this.OnExit(animalActionPoint);
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600585E RID: 22622 RVA: 0x0025F9BC File Offset: 0x0025DDBC
		private void OnEnter(AnimalActionPoint point)
		{
			if (point == null)
			{
				return;
			}
			if (!this.VisibleList.Contains(point))
			{
				this.VisibleList.Add(point);
			}
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x0025F9E8 File Offset: 0x0025DDE8
		private void OnExit(AnimalActionPoint point)
		{
			if (point == null)
			{
				return;
			}
			this.VisibleList.Remove(point);
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x0025FA04 File Offset: 0x0025DE04
		public void RefreshQueryPoints()
		{
			AnimalActionPoint[] animalActionPoints = Singleton<Map>.Instance.PointAgent.AnimalActionPoints;
			this.SearchPoints.Clear();
			foreach (AnimalActionPoint animalActionPoint in animalActionPoints)
			{
				if (animalActionPoint != null)
				{
					this.SearchPoints.Add(animalActionPoint);
				}
			}
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x0025FA5C File Offset: 0x0025DE5C
		public void ClearCollisionState()
		{
			this.VisibleList.Clear();
			this.collisionStateTable.Clear();
		}

		// Token: 0x0400510A RID: 20746
		[SerializeField]
		private AnimalBase animal;

		// Token: 0x0400510B RID: 20747
		[SerializeField]
		[Tooltip("視界に入る距離")]
		private float _visibleDistance = 75f;

		// Token: 0x0400510C RID: 20748
		[SerializeField]
		[Tooltip("視界に入る高さ")]
		private float _visibleHeight = 20f;

		// Token: 0x0400510D RID: 20749
		[SerializeField]
		[Tooltip("視界に入る左右の角度")]
		private float _visibleAngle = 130f;

		// Token: 0x0400510E RID: 20750
		private Dictionary<int, CollisionState> collisionStateTable = new Dictionary<int, CollisionState>();
	}
}
