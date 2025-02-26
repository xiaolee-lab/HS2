using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000BA5 RID: 2981
	public class LocomotionArea
	{
		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06005915 RID: 22805 RVA: 0x00265176 File Offset: 0x00263576
		public int Count
		{
			[CompilerGenerated]
			get
			{
				return this.points.Count;
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06005916 RID: 22806 RVA: 0x00265183 File Offset: 0x00263583
		public bool Empty
		{
			[CompilerGenerated]
			get
			{
				return this.points.IsNullOrEmpty<Waypoint>();
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06005917 RID: 22807 RVA: 0x00265190 File Offset: 0x00263590
		public bool ActiveEmpty
		{
			[CompilerGenerated]
			get
			{
				return this.activePoints.IsNullOrEmpty<Waypoint>();
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06005918 RID: 22808 RVA: 0x0026519D File Offset: 0x0026359D
		// (set) Token: 0x06005919 RID: 22809 RVA: 0x002651A5 File Offset: 0x002635A5
		public bool Active { get; private set; }

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x0600591A RID: 22810 RVA: 0x002651AE File Offset: 0x002635AE
		// (set) Token: 0x0600591B RID: 22811 RVA: 0x002651B6 File Offset: 0x002635B6
		public bool Complete { get; private set; }

		// Token: 0x0600591C RID: 22812 RVA: 0x002651C0 File Offset: 0x002635C0
		public void SetWaypoint(List<Waypoint> _points)
		{
			this.Clear();
			if (_points.IsNullOrEmpty<Waypoint>())
			{
				return;
			}
			foreach (Waypoint waypoint in _points)
			{
				if (!(waypoint == null))
				{
					this.points.Add(waypoint);
				}
			}
			this.Complete = true;
			this.Active = !this.Empty;
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x00265258 File Offset: 0x00263658
		public void Clear()
		{
			bool flag = false;
			this.Active = flag;
			this.Complete = flag;
			this.points.Clear();
			this.activePoints.Clear();
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x0026528C File Offset: 0x0026368C
		~LocomotionArea()
		{
			this.Clear();
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x002652BC File Offset: 0x002636BC
		public bool ActivePoint(Waypoint _point)
		{
			return _point != null && _point.gameObject.activeSelf && _point.Reserver == null && _point.OwnerArea != null;
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x002652F4 File Offset: 0x002636F4
		public bool ActivePoint(Waypoint _point, LocomotionArea.AreaType _type)
		{
			if (!this.ActivePoint(_point))
			{
				return false;
			}
			MapArea.AreaType areaType = _point.AreaType;
			bool flag = false;
			flag |= ((_type & LocomotionArea.AreaType.Normal) != (LocomotionArea.AreaType)0 && areaType == MapArea.AreaType.Normal);
			flag |= ((_type & LocomotionArea.AreaType.Indoor) != (LocomotionArea.AreaType)0 && areaType == MapArea.AreaType.Indoor);
			return flag | ((_type & LocomotionArea.AreaType.Private) != (LocomotionArea.AreaType)0 && areaType == MapArea.AreaType.Private);
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x0026534F File Offset: 0x0026374F
		public List<Waypoint> GetPointList(LocomotionArea.AreaType _areaType = LocomotionArea.AreaType.Normal | LocomotionArea.AreaType.Indoor)
		{
			this.SetActivePoints(_areaType);
			return this.activePoints;
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x0026535E File Offset: 0x0026375E
		public List<Waypoint> GetPointList(Vector3 _position, float _distance, LocomotionArea.AreaType _areaType = LocomotionArea.AreaType.Normal | LocomotionArea.AreaType.Indoor)
		{
			this.SetActivePoints(_position, _distance, _areaType);
			return this.activePoints;
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x0026536F File Offset: 0x0026376F
		public List<Waypoint> GetPointList(Vector3 _position, float _minDistance, float _maxDistance)
		{
			this.SetActivePoints(_position, _minDistance, _maxDistance);
			return this.activePoints;
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x00265380 File Offset: 0x00263780
		public List<Waypoint> GetPointList(Vector3 _position, float _minDistance, float _maxDistance, LocomotionArea.AreaType _areaType)
		{
			this.SetActivePoints(_position, _minDistance, _maxDistance, _areaType);
			return this.activePoints;
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x00265393 File Offset: 0x00263793
		public List<Waypoint> GetPointList(Vector3 _position, float _minDistance, float _maxDistance, MapArea _mapArea)
		{
			this.SetActivePoints(_position, _minDistance, _maxDistance, _mapArea);
			return this.activePoints;
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x002653A6 File Offset: 0x002637A6
		public List<Waypoint> GetPointList(Vector3 _position, float _minDistance, float _maxDistance, MapArea _mapArae, LocomotionArea.AreaType _areaType)
		{
			this.SetActivePoints(_position, _minDistance, _maxDistance, _mapArae, _areaType);
			return this.activePoints;
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x002653BC File Offset: 0x002637BC
		public List<Waypoint> GetRandomPointList(Vector3 _myPoint, Vector3 _targetPoint, float _createDistance, Vector3 _forward, float _angle, LocomotionArea.AreaType _areaType)
		{
			this.activePoints.Clear();
			if (this.Empty)
			{
				return this.activePoints;
			}
			float num = _createDistance * _createDistance;
			for (int i = 0; i < this.Count; i++)
			{
				Waypoint waypoint = this.points[i];
				if (this.ActivePoint(waypoint, _areaType))
				{
					Vector3 position = waypoint.transform.position;
					if ((_myPoint - position).sqrMagnitude > num && (_targetPoint - position).sqrMagnitude > num)
					{
						Vector2 vector = new Vector2(_forward.x, _forward.z);
						Vector2 normalized = vector.normalized;
						Vector2 vector2 = new Vector2(position.x - _targetPoint.x, position.z - _targetPoint.z);
						Vector2 normalized2 = vector2.normalized;
						float f = Mathf.Clamp(Vector2.Dot(normalized, normalized2), -1f, 1f);
						float num2 = Mathf.Acos(f) * 57.29578f;
						if (_angle >= num2 * 2f)
						{
							this.activePoints.Add(waypoint);
						}
					}
				}
			}
			if (this.ActiveEmpty)
			{
				this.SetActivePoints(_myPoint, _targetPoint, _createDistance, _areaType);
				if (this.ActiveEmpty)
				{
					this.SetActivePoints(_areaType);
				}
			}
			return this.activePoints;
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x00265522 File Offset: 0x00263922
		public bool GetRandomPoint(Vector3 _myPoint, Vector3 _targetPoint, float _createDistance, Vector3 _forward, float _angle, ref Waypoint _nextPoint, LocomotionArea.AreaType _areaType = LocomotionArea.AreaType.Normal | LocomotionArea.AreaType.Indoor)
		{
			this.GetRandomPointList(_myPoint, _targetPoint, _createDistance, _forward, _angle, _areaType);
			if (this.ActiveEmpty)
			{
				return false;
			}
			_nextPoint = this.activePoints.Rand<Waypoint>();
			return true;
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x00265550 File Offset: 0x00263950
		private void SetActivePoints(LocomotionArea.AreaType _areaType)
		{
			this.activePoints.Clear();
			foreach (Waypoint waypoint in this.points)
			{
				if (this.ActivePoint(waypoint, _areaType))
				{
					this.activePoints.Add(waypoint);
				}
			}
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x002655D0 File Offset: 0x002639D0
		private void SetActivePoints(MapArea _mapArea, LocomotionArea.AreaType _areaType)
		{
			this.activePoints.Clear();
			if (_mapArea == null)
			{
				return;
			}
			foreach (Waypoint waypoint in _mapArea.Waypoints)
			{
				if (this.ActivePoint(waypoint, _areaType))
				{
					this.activePoints.Add(waypoint);
				}
			}
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x0026565C File Offset: 0x00263A5C
		private void SetActivePoints(Vector3 _position, float _minDistance, float _maxDistance)
		{
			this.activePoints.Clear();
			_minDistance *= _minDistance;
			_maxDistance *= _maxDistance;
			foreach (Waypoint waypoint in this.points)
			{
				if (this.ActivePoint(waypoint))
				{
					float sqrMagnitude = (waypoint.transform.position - _position).sqrMagnitude;
					if (_minDistance <= sqrMagnitude && sqrMagnitude <= _maxDistance)
					{
						this.activePoints.Add(waypoint);
					}
				}
			}
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x0026570C File Offset: 0x00263B0C
		private void SetActivePoints(Vector3 _position, float _minDistance, float _maxDistance, MapArea _mapArea)
		{
			this.activePoints.Clear();
			if (_mapArea == null)
			{
				return;
			}
			_minDistance *= _minDistance;
			_maxDistance *= _maxDistance;
			foreach (Waypoint waypoint in _mapArea.Waypoints)
			{
				if (this.ActivePoint(waypoint))
				{
					float sqrMagnitude = (waypoint.transform.position - _position).sqrMagnitude;
					if (_minDistance <= sqrMagnitude && sqrMagnitude <= _maxDistance)
					{
						this.activePoints.Add(waypoint);
					}
				}
			}
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x002657CC File Offset: 0x00263BCC
		private void SetActivePoints(Vector3 _position, float _minDistance, float _maxDistance, MapArea _mapArea, LocomotionArea.AreaType _areaType)
		{
			this.activePoints.Clear();
			if (_mapArea == null)
			{
				return;
			}
			_minDistance *= _minDistance;
			_maxDistance *= _maxDistance;
			foreach (Waypoint waypoint in _mapArea.Waypoints)
			{
				if (this.ActivePoint(waypoint, _areaType))
				{
					float sqrMagnitude = (waypoint.transform.position - _position).sqrMagnitude;
					if (_minDistance <= sqrMagnitude && sqrMagnitude <= _maxDistance)
					{
						this.activePoints.Add(waypoint);
					}
				}
			}
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x0026588C File Offset: 0x00263C8C
		private void SetActivePoints(Vector3 _position, float _distance, LocomotionArea.AreaType _areaType)
		{
			this.activePoints.Clear();
			_distance *= _distance;
			foreach (Waypoint waypoint in this.points)
			{
				if (this.ActivePoint(waypoint, _areaType))
				{
					if (_distance <= (waypoint.transform.position - _position).sqrMagnitude)
					{
						this.activePoints.Add(waypoint);
					}
				}
			}
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x00265930 File Offset: 0x00263D30
		private void SetActivePoints(Vector3 _position, float _minDistance, float _maxDistance, LocomotionArea.AreaType _areaType)
		{
			this.activePoints.Clear();
			_minDistance *= _minDistance;
			_maxDistance *= _maxDistance;
			foreach (Waypoint waypoint in this.points)
			{
				if (this.ActivePoint(waypoint, _areaType))
				{
					float sqrMagnitude = (waypoint.transform.position - _position).sqrMagnitude;
					if (_minDistance <= sqrMagnitude && sqrMagnitude <= _maxDistance)
					{
						this.activePoints.Add(waypoint);
					}
				}
			}
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x002659E4 File Offset: 0x00263DE4
		private void SetActivePoints(Vector3 _myPoint, Vector3 _targetPoint, float _distance, LocomotionArea.AreaType _areaType)
		{
			this.activePoints.Clear();
			_distance *= _distance;
			foreach (Waypoint waypoint in this.points)
			{
				if (this.ActivePoint(waypoint, _areaType))
				{
					Vector3 position = waypoint.transform.position;
					float sqrMagnitude = (position - _myPoint).sqrMagnitude;
					float sqrMagnitude2 = (position - _targetPoint).sqrMagnitude;
					if (_distance <= sqrMagnitude && _distance <= sqrMagnitude2)
					{
						this.activePoints.Add(waypoint);
					}
				}
			}
		}

		// Token: 0x04005194 RID: 20884
		public List<Waypoint> points = new List<Waypoint>();

		// Token: 0x04005195 RID: 20885
		private List<Waypoint> activePoints = new List<Waypoint>();

		// Token: 0x04005198 RID: 20888
		protected static RaycastHit[] _raycastHits = new RaycastHit[3];

		// Token: 0x04005199 RID: 20889
		public const LocomotionArea.AreaType NormalAreaType = LocomotionArea.AreaType.Normal | LocomotionArea.AreaType.Indoor;

		// Token: 0x02000BA6 RID: 2982
		[Flags]
		public enum AreaType
		{
			// Token: 0x0400519B RID: 20891
			Normal = 1,
			// Token: 0x0400519C RID: 20892
			Indoor = 2,
			// Token: 0x0400519D RID: 20893
			Private = 4,
			// Token: 0x0400519E RID: 20894
			All = 7
		}
	}
}
