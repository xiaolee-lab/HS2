using System;
using System.Collections;
using System.Collections.Generic;
using AIProject.Animal;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DC6 RID: 3526
	[TaskCategory("商人")]
	public class SetWaitPoint : MerchantSetPoint
	{
		// Token: 0x06006D6B RID: 28011 RVA: 0x002E9B70 File Offset: 0x002E7F70
		private bool IsMatchPoint(MerchantPoint _point, bool _isMiddlePoint)
		{
			if (_point == null)
			{
				return false;
			}
			Merchant.EventType eventType = AIProject.Definitions.Merchant.EventType.Wait;
			MerchantActor merchant = base.Merchant;
			MerchantData merchantData = merchant.MerchantData;
			int openAreaID = merchant.OpenAreaID;
			int pointAreaID = merchantData.PointAreaID;
			int pointGroupID = merchantData.PointGroupID;
			MerchantPoint prevMerchantPoint = base.PrevMerchantPoint;
			if (openAreaID < _point.AreaID)
			{
				return false;
			}
			if ((_point.EventType & eventType) != eventType)
			{
				return false;
			}
			if (_isMiddlePoint)
			{
				return _point.GroupID == 0;
			}
			bool flag = _point.AreaID == pointAreaID;
			return (!flag) ? (_point.GroupID == 0) : (_point.GroupID != pointGroupID || _point != prevMerchantPoint);
		}

		// Token: 0x06006D6C RID: 28012 RVA: 0x002E9C21 File Offset: 0x002E8021
		private bool IsSameArea(MerchantPoint _point0, MerchantPoint _point1)
		{
			return !(_point0 == null) && !(_point1 == null) && _point0.AreaID == _point1.AreaID;
		}

		// Token: 0x06006D6D RID: 28013 RVA: 0x002E9C4C File Offset: 0x002E804C
		protected override IEnumerator NextPointSettingCoroutine()
		{
			Merchant.EventType _eventMask = AIProject.Definitions.Merchant.EventType.Wait;
			MerchantActor _merchant = base.Merchant;
			int _openAreaID = _merchant.OpenAreaID;
			MerchantPoint _targetPoint = base.TargetInSightMerchantPoint;
			MerchantPoint _mainTargetPoint = base.MainTargetInSightMerchantPoint;
			int _areaMask = base.Merchant.NavMeshAgent.areaMask;
			if (_mainTargetPoint != null)
			{
				if (this.IsMatchPoint(_targetPoint, true) && this.IsMatchPoint(_mainTargetPoint, false) && this.IsSameArea(_targetPoint, _mainTargetPoint))
				{
					if (NavMesh.CalculatePath(base.Merchant.Position, _mainTargetPoint.Destination, _areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete)
					{
						base.TargetInSightMerchantPoint = base.MainTargetInSightMerchantPoint;
						base.MainTargetInSightMerchantPoint = null;
						this.Success = true;
						yield break;
					}
					yield return null;
					if (NavMesh.CalculatePath(base.Merchant.Position, _targetPoint.Destination, _areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete && NavMesh.CalculatePath(_targetPoint.Destination, _mainTargetPoint.Destination, _areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete)
					{
						this.Success = true;
						yield break;
					}
					base.TargetInSightMerchantPoint = null;
					base.MainTargetInSightMerchantPoint = null;
					yield return null;
				}
			}
			else if (this.IsMatchPoint(_targetPoint, false))
			{
				if (NavMesh.CalculatePath(base.Merchant.Position, _targetPoint.Destination, _areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete)
				{
					this.Success = true;
					yield break;
				}
				base.TargetInSightMerchantPoint = null;
				yield return null;
			}
			int _currentAreaID = _merchant.MerchantData.PointAreaID;
			int _currentGroupID = _merchant.MerchantData.PointGroupID;
			List<int> _differenctAreaIDList = ListPool<int>.Get();
			List<MerchantPoint> _points = base.Merchant.MerchantPoints;
			if (!_points.IsNullOrEmpty<MerchantPoint>())
			{
				foreach (MerchantPoint merchantPoint in _points)
				{
					if (!(merchantPoint == null))
					{
						if ((merchantPoint.EventType & _eventMask) == _eventMask)
						{
							if (_openAreaID >= merchantPoint.AreaID)
							{
								if (merchantPoint.AreaID != _currentAreaID && !_differenctAreaIDList.Contains(merchantPoint.AreaID))
								{
									_differenctAreaIDList.Add(merchantPoint.AreaID);
								}
								this.merchantPoints.Add(merchantPoint);
							}
						}
					}
				}
			}
			if (this.merchantPoints.IsNullOrEmpty<MerchantPoint>())
			{
				ListPool<int>.Release(_differenctAreaIDList);
				yield break;
			}
			int _nextAreaID = _currentAreaID;
			float _range = Singleton<Manager.Resources>.Instance.MerchantProfile.DifferentAreaSelectedRange;
			if (!_differenctAreaIDList.IsNullOrEmpty<int>() && !Mathf.Approximately(_range, 0f))
			{
				float num = UnityEngine.Random.Range(0f, 100f);
				if (num <= _range)
				{
					_nextAreaID = _differenctAreaIDList.Rand<int>();
				}
			}
			ListPool<int>.Release(_differenctAreaIDList);
			List<MerchantPoint> _availablePoints = ListPool<MerchantPoint>.Get();
			List<MerchantPoint> _sameAreaPoints = ListPool<MerchantPoint>.Get();
			List<MerchantPoint> _partialPoints = ListPool<MerchantPoint>.Get();
			bool _sameArea = _nextAreaID == _currentAreaID;
			foreach (MerchantPoint _point in this.merchantPoints)
			{
				if (!(_point == null))
				{
					if (_point.AreaID == _nextAreaID)
					{
						if (_sameArea && !_sameAreaPoints.Contains(_point))
						{
							_sameAreaPoints.Add(_point);
						}
						if (!(_point == base.PrevMerchantPoint))
						{
							if (_sameArea || _point.GroupID == 0)
							{
								if (NavMesh.CalculatePath(base.Merchant.Position, _point.Destination, _areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete)
								{
									if (!_availablePoints.Contains(_point))
									{
										_availablePoints.Add(_point);
									}
								}
								else if (!_partialPoints.Contains(_point))
								{
									_partialPoints.Add(_point);
								}
								yield return null;
							}
						}
					}
				}
			}
			MerchantPoint _nextPoint = _availablePoints.Rand<MerchantPoint>();
			ListPool<MerchantPoint>.Release(_availablePoints);
			this.Success = (_nextPoint != null);
			if (this.Success)
			{
				base.MainTargetInSightMerchantPoint = null;
				base.TargetInSightMerchantPoint = _nextPoint;
			}
			else if (_sameArea)
			{
				while (!_partialPoints.IsNullOrEmpty<MerchantPoint>() && !this.Success)
				{
					_nextPoint = _partialPoints.GetRand<MerchantPoint>();
					if (!(_nextPoint == null))
					{
						_sameAreaPoints.Remove(_nextPoint);
						if (_nextPoint.GroupID == 0)
						{
							if (NavMesh.CalculatePath(base.Merchant.Position, _nextPoint.Destination, _areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete)
							{
								base.MainTargetInSightMerchantPoint = null;
								base.TargetInSightMerchantPoint = _nextPoint;
								this.Success = true;
								break;
							}
							yield return null;
						}
						else if (!_sameAreaPoints.IsNullOrEmpty<MerchantPoint>())
						{
							List<MerchantPoint> _middlePoints = _sameAreaPoints.FindAll((MerchantPoint x) => x != null && x.GroupID == 0);
							if (!_middlePoints.IsNullOrEmpty<MerchantPoint>())
							{
								MerchantPoint _middlePoint = _middlePoints.GetRand<MerchantPoint>();
								if (_middlePoint != null && NavMesh.CalculatePath(base.Merchant.Position, _middlePoint.Destination, _areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete && NavMesh.CalculatePath(_middlePoint.Destination, _nextPoint.Destination, _areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete)
								{
									base.TargetInSightMerchantPoint = _middlePoint;
									base.MainTargetInSightMerchantPoint = _nextPoint;
									this.Success = true;
									break;
								}
								yield return null;
							}
						}
					}
				}
			}
			ListPool<MerchantPoint>.Release(_sameAreaPoints);
			ListPool<MerchantPoint>.Release(_partialPoints);
			yield break;
		}
	}
}
