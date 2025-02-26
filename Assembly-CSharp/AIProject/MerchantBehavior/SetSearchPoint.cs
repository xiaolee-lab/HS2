using System;
using System.Collections;
using System.Collections.Generic;
using AIProject.Animal;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DC5 RID: 3525
	[TaskCategory("商人")]
	public class SetSearchPoint : MerchantSetPoint
	{
		// Token: 0x06006D67 RID: 28007 RVA: 0x002E90D8 File Offset: 0x002E74D8
		private bool IsMatchPoint(MerchantPoint _point, bool _isMiddlePoint)
		{
			if (_point == null)
			{
				return false;
			}
			Merchant.EventType eventType = (!_isMiddlePoint) ? AIProject.Definitions.Merchant.EventType.Search : AIProject.Definitions.Merchant.EventType.Wait;
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
			if (_point.AreaID != pointAreaID)
			{
				return false;
			}
			if (_isMiddlePoint)
			{
				return _point.GroupID == 0;
			}
			return _point.GroupID != pointGroupID || _point != prevMerchantPoint;
		}

		// Token: 0x06006D68 RID: 28008 RVA: 0x002E9183 File Offset: 0x002E7583
		private bool IsSameArea(MerchantPoint _point0, MerchantPoint _point1)
		{
			return !(_point0 == null) && !(_point1 == null) && _point0.AreaID == _point1.AreaID;
		}

		// Token: 0x06006D69 RID: 28009 RVA: 0x002E91B0 File Offset: 0x002E75B0
		protected override IEnumerator NextPointSettingCoroutine()
		{
			Merchant.EventType _eventMask = AIProject.Definitions.Merchant.EventType.Search;
			int _openAreaID = base.Merchant.OpenAreaID;
			int _currentAreaID = base.Merchant.MerchantData.PointAreaID;
			int _currentGroupID = base.Merchant.MerchantData.PointGroupID;
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
			List<MerchantPoint> _middlePoints = ListPool<MerchantPoint>.Get();
			Merchant.EventType _waitEventMask = AIProject.Definitions.Merchant.EventType.Wait;
			foreach (MerchantPoint merchantPoint in base.Merchant.MerchantPoints)
			{
				if (!(merchantPoint == null))
				{
					if (merchantPoint.AreaID == _currentAreaID)
					{
						if (_openAreaID >= merchantPoint.AreaID)
						{
							if ((merchantPoint.EventType & _waitEventMask) == _waitEventMask && merchantPoint.GroupID == 0 && !_middlePoints.Contains(merchantPoint))
							{
								_middlePoints.Add(merchantPoint);
							}
							if (!(merchantPoint == base.PrevMerchantPoint))
							{
								if ((merchantPoint.EventType & _eventMask) == _eventMask)
								{
									if (base.ChangedSchedule || merchantPoint.GroupID == _currentGroupID)
									{
										this.merchantPoints.Add(merchantPoint);
									}
								}
							}
						}
					}
				}
			}
			if (this.merchantPoints.IsNullOrEmpty<MerchantPoint>())
			{
				ListPool<MerchantPoint>.Release(_middlePoints);
				base.StopSetting();
				yield break;
			}
			List<MerchantPoint> _availablePoints = ListPool<MerchantPoint>.Get();
			List<MerchantPoint> _partialPoints = ListPool<MerchantPoint>.Get();
			foreach (MerchantPoint _point in this.merchantPoints)
			{
				if (!(_point == null))
				{
					if (!(_point == base.PrevMerchantPoint))
					{
						if (NavMesh.CalculatePath(base.Merchant.Position, _point.Destination, _areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete)
						{
							_availablePoints.Add(_point);
						}
						else if (!_partialPoints.Contains(_point))
						{
							_partialPoints.Add(_point);
						}
						yield return null;
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
			else if (!_middlePoints.IsNullOrEmpty<MerchantPoint>() && !_partialPoints.IsNullOrEmpty<MerchantPoint>())
			{
				while (!_partialPoints.IsNullOrEmpty<MerchantPoint>() && !this.Success)
				{
					_nextPoint = _partialPoints.GetRand<MerchantPoint>();
					if (!(_nextPoint == null))
					{
						foreach (MerchantPoint _middlePoint in _middlePoints)
						{
							if (!(_middlePoint == null))
							{
								if (this.IsSameArea(_nextPoint, _middlePoint))
								{
									if (NavMesh.CalculatePath(base.Merchant.Position, _middlePoint.Destination, _areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete && NavMesh.CalculatePath(_middlePoint.Destination, _nextPoint.Destination, _areaMask, this.navMeshPath) && this.navMeshPath.status == NavMeshPathStatus.PathComplete)
									{
										base.MainTargetInSightMerchantPoint = _nextPoint;
										base.TargetInSightMerchantPoint = _middlePoint;
										this.Success = true;
										break;
									}
									yield return null;
								}
							}
						}
						if (this.Success)
						{
							break;
						}
					}
				}
			}
			ListPool<MerchantPoint>.Release(_middlePoints);
			ListPool<MerchantPoint>.Release(_partialPoints);
			base.StopSetting();
			yield break;
		}
	}
}
