using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

// Token: 0x02000AC5 RID: 2757
public class HPointCtrl : Singleton<HPointCtrl>
{
	// Token: 0x17000EDD RID: 3805
	// (get) Token: 0x060050B6 RID: 20662 RVA: 0x001F887C File Offset: 0x001F6C7C
	public UnityEx.ValueTuple<Vector3, Quaternion, int> InitNull
	{
		[CompilerGenerated]
		get
		{
			return this._InitNull;
		}
	}

	// Token: 0x17000EDE RID: 3806
	// (get) Token: 0x060050B7 RID: 20663 RVA: 0x001F8884 File Offset: 0x001F6C84
	public Dictionary<int, HPointList> HPointLists
	{
		[CompilerGenerated]
		get
		{
			return this.hPointLists;
		}
	}

	// Token: 0x17000EDF RID: 3807
	// (get) Token: 0x060050B8 RID: 20664 RVA: 0x001F888C File Offset: 0x001F6C8C
	public HPoint[] HousingHpoints
	{
		[CompilerGenerated]
		get
		{
			return this.housingHpoints;
		}
	}

	// Token: 0x060050B9 RID: 20665 RVA: 0x001F8894 File Offset: 0x001F6C94
	public void InitHPoint()
	{
		this.hSceneManager = Singleton<HSceneManager>.Instance;
		this.startList = Singleton<Manager.Resources>.Instance.HSceneTable.lstStartAnimInfo;
		this.startListM = Singleton<Manager.Resources>.Instance.HSceneTable.lstStartAnimInfoM;
		this.lstAnimInfo = Singleton<Manager.Resources>.Instance.HSceneTable.lstAnimInfo;
		this.initNullObj = Singleton<Manager.Resources>.Instance.HSceneTable.HPointObj;
		this.AreagpInit();
		Dictionary<int, Dictionary<bool, ForcedHideObject[]>> areaOpenObjectTable = Singleton<Map>.Instance.AreaOpenObjectTable;
		this.CheckHitObjs.Clear();
		if (areaOpenObjectTable != null && areaOpenObjectTable.Count > 0)
		{
			foreach (KeyValuePair<int, Dictionary<bool, ForcedHideObject[]>> keyValuePair in areaOpenObjectTable)
			{
				if (keyValuePair.Value != null && keyValuePair.Value.Count != 0)
				{
					int key = keyValuePair.Key;
					ForcedHideObject[] collection;
					if (keyValuePair.Value.TryGetValue(false, out collection))
					{
						this.CheckHitObjs.AddRange(collection);
					}
				}
			}
		}
		if (this.fade == null)
		{
			this.fade = this.hSceneManager.Player.CameraControl.CrossFade;
		}
	}

	// Token: 0x060050BA RID: 20666 RVA: 0x001F89F0 File Offset: 0x001F6DF0
	public void MarkerObjSet(Vector3 PlayerPos, int mapID = 0, int AreaID = 0)
	{
		if (!this.InitUsePoint)
		{
			float sqrMagnitude = (this.initNullObj.transform.position - PlayerPos).sqrMagnitude;
			if (sqrMagnitude <= this.hSceneSprite.HpointSearchRange * this.hSceneSprite.HpointSearchRange)
			{
				this.lstMarker[0].Item2.SetEffectActive(true);
				Collider collider = this.lstMarker[0].Item2.GetCollider();
				if (collider != null)
				{
					collider.enabled = true;
				}
			}
		}
		bool flag = false;
		if (this.gpID == -1)
		{
			if (this.hPointLists.ContainsKey(mapID) && this.hPointLists[mapID].lst.ContainsKey(AreaID))
			{
				for (int i = 1; i < this.lstMarker.Count; i++)
				{
					int index = i;
					HPoint item = this.lstMarker[index].Item2;
					if (this.hPointLists[mapID].lst[AreaID].Contains(item))
					{
						float sqrMagnitude = (item.transform.position - PlayerPos).sqrMagnitude;
						if (sqrMagnitude <= this.hSceneSprite.HpointSearchRange * this.hSceneSprite.HpointSearchRange)
						{
							if (this.playerSex == 1)
							{
								if (!this.CheckPlace(item._nPlace, 0))
								{
									goto IL_29C;
								}
							}
							else if (this.ExistSecondfemale && !this.CheckPlace(item._nPlace, 1))
							{
								goto IL_29C;
							}
							flag = false;
							foreach (UnityEx.ValueTuple<int, int> valueTuple in item._nPlace.Values)
							{
								if (valueTuple.Item1 == this.dildoPlaceID)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								if (this.CanInfo(item._nPlace))
								{
									if (!this.HitObstacle(PlayerPos, item))
									{
										if (this.HpointAreaOpen(item))
										{
											this.lstMarker[index].Item2.SetEffectActive(true);
											Collider collider = this.lstMarker[index].Item2.GetCollider();
											if (collider != null)
											{
												collider.enabled = true;
											}
										}
									}
								}
							}
						}
					}
					IL_29C:;
				}
			}
		}
		else if (this.hPointLists.ContainsKey(mapID))
		{
			Dictionary<int, HPointCtrl.AreaGroupDefine> dictionary;
			if (!this.Areagp.TryGetValue(mapID, out dictionary))
			{
				return;
			}
			for (int j = 0; j < dictionary[this.gpID].IDs.Count; j++)
			{
				if (this.hPointLists[mapID].lst.ContainsKey(dictionary[this.gpID].IDs[j]))
				{
					for (int k = 1; k < this.lstMarker.Count; k++)
					{
						int index = k;
						HPoint item2 = this.lstMarker[index].Item2;
						if (this.hPointLists[mapID].lst[dictionary[this.gpID].IDs[j]].Contains(item2))
						{
							float sqrMagnitude = (item2.transform.position - PlayerPos).sqrMagnitude;
							if (sqrMagnitude <= this.hSceneSprite.HpointSearchRange * this.hSceneSprite.HpointSearchRange)
							{
								if (this.playerSex == 1)
								{
									if (!this.CheckPlace(item2._nPlace, 0))
									{
										goto IL_4FD;
									}
								}
								else if (this.ExistSecondfemale && !this.CheckPlace(item2._nPlace, 1))
								{
									goto IL_4FD;
								}
								flag = false;
								foreach (UnityEx.ValueTuple<int, int> valueTuple2 in item2._nPlace.Values)
								{
									if (valueTuple2.Item1 == this.dildoPlaceID)
									{
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									if (this.CanInfo(item2._nPlace))
									{
										if (!this.HitObstacle(PlayerPos, item2))
										{
											if (this.HpointAreaOpen(item2))
											{
												this.lstMarker[index].Item2.SetEffectActive(true);
												Collider collider = this.lstMarker[index].Item2.GetCollider();
												if (collider != null)
												{
													collider.enabled = true;
												}
											}
										}
									}
								}
							}
						}
						IL_4FD:;
					}
				}
			}
		}
		if (this.housingHpoints != null && this.housingHpoints.Length != 0)
		{
			for (int l = 1; l < this.lstMarker.Count; l++)
			{
				int index = l;
				HPoint item3 = this.lstMarker[index].Item2;
				if (this.HousingListContain(item3))
				{
					float sqrMagnitude = (item3.transform.position - PlayerPos).sqrMagnitude;
					if (sqrMagnitude <= this.hSceneSprite.HpointSearchRange * this.hSceneSprite.HpointSearchRange)
					{
						if (this.playerSex == 1)
						{
							if (!this.CheckPlace(item3._nPlace, 0))
							{
								goto IL_706;
							}
						}
						else if (this.ExistSecondfemale && !this.CheckPlace(item3._nPlace, 1))
						{
							goto IL_706;
						}
						flag = false;
						foreach (UnityEx.ValueTuple<int, int> valueTuple3 in item3._nPlace.Values)
						{
							if (valueTuple3.Item1 == this.dildoPlaceID)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							if (this.CanInfo(item3._nPlace))
							{
								if (!this.HitObstacle(PlayerPos, item3))
								{
									if (this.HpointAreaOpen(item3))
									{
										this.lstMarker[index].Item2.SetEffectActive(true);
										Collider collider = this.lstMarker[index].Item2.GetCollider();
										if (collider != null)
										{
											collider.enabled = true;
										}
									}
								}
							}
						}
					}
				}
				IL_706:;
			}
		}
		this.IsMarker = true;
	}

	// Token: 0x060050BB RID: 20667 RVA: 0x001F914C File Offset: 0x001F754C
	public void MarkerObjDel()
	{
		for (int i = 0; i < this.lstMarker.Count; i++)
		{
			if (this.lstMarker[i].Item2.EffectActive())
			{
				this.lstMarker[i].Item2.SetEffectActive(false);
				Collider collider = this.lstMarker[i].Item2.GetCollider();
				if (collider != null)
				{
					collider.enabled = false;
				}
			}
		}
		this.IsMarker = false;
	}

	// Token: 0x060050BC RID: 20668 RVA: 0x001F91E8 File Offset: 0x001F75E8
	private bool HousingListContain(HPoint target)
	{
		if (this.housingHpoints == null)
		{
			return false;
		}
		for (int i = 0; i < this.housingHpoints.Length; i++)
		{
			if (this.housingHpoints[i] == target)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060050BD RID: 20669 RVA: 0x001F9234 File Offset: 0x001F7634
	public void Init(int mapID = 0, int AreaID = 0)
	{
		this.lstMarker.Clear();
		this.housingHpoints = null;
		this.lstMarker.Add(new UnityEx.ValueTuple<GameObject, HPoint>(null, this.initNullObj.GetComponent<HPoint>()));
		this.hPointLists = Singleton<Manager.Resources>.Instance.HSceneTable.hPointLists;
		this.gpID = -1;
		HPointList hpointList = this.hPointLists[mapID];
		Dictionary<int, HPointCtrl.AreaGroupDefine> dictionary;
		if (!this.Areagp.TryGetValue(mapID, out dictionary))
		{
			return;
		}
		foreach (KeyValuePair<int, HPointCtrl.AreaGroupDefine> keyValuePair in dictionary)
		{
			if (keyValuePair.Value.IDs.Contains(AreaID))
			{
				this.gpID = keyValuePair.Key;
				for (int i = 0; i < keyValuePair.Value.IDs.Count; i++)
				{
					int key = keyValuePair.Value.IDs[i];
					if (hpointList.lst.ContainsKey(key))
					{
						for (int j = 0; j < hpointList.lst[key].Count; j++)
						{
							this.lstMarker.Add(new UnityEx.ValueTuple<GameObject, HPoint>(hpointList.lst[key][j].markerPos.gameObject, hpointList.lst[key][j]));
						}
					}
				}
				break;
			}
		}
		if (this.gpID == -1 && hpointList.lst.ContainsKey(AreaID))
		{
			for (int k = 0; k < hpointList.lst[AreaID].Count; k++)
			{
				this.lstMarker.Add(new UnityEx.ValueTuple<GameObject, HPoint>(hpointList.lst[AreaID][k].markerPos.gameObject, hpointList.lst[AreaID][k]));
			}
		}
		if (Singleton<Map>.Instance.PointAgent != null && Singleton<Map>.Instance.PointAgent.BasePoints != null)
		{
			this.BasePoints = Singleton<Map>.Instance.PointAgent.BasePoints;
			for (int l = 0; l < this.BasePoints.Length; l++)
			{
				if (!(this.BasePoints[l].OwnerArea == null))
				{
					if (this.gpID != -1 && dictionary[this.gpID].IDs.Contains(this.BasePoints[l].OwnerArea.AreaID))
					{
						this.housingHpoints = Singleton<Housing>.Instance.GetHPoint(this.BasePoints[l].ID);
					}
				}
			}
			if (this.housingHpoints != null)
			{
				for (int m = 0; m < this.housingHpoints.Length; m++)
				{
					this.lstMarker.Add(new UnityEx.ValueTuple<GameObject, HPoint>(this.housingHpoints[m].markerPos.gameObject, this.housingHpoints[m]));
				}
			}
		}
	}

	// Token: 0x060050BE RID: 20670 RVA: 0x001F958C File Offset: 0x001F798C
	public void SetStartPos(Transform trans, int basho)
	{
		this._InitNull = new UnityEx.ValueTuple<Vector3, Quaternion, int>(trans.position, trans.rotation, basho);
		HPoint component = this.initNullObj.GetComponent<HPoint>();
		component.id = -1;
		component.Init();
		if (!component._nPlace.ContainsKey(0))
		{
			component._nPlace.Add(0, new UnityEx.ValueTuple<int, int>(this._InitNull.Item3, this.HEnterCategory));
		}
		else
		{
			component._nPlace[0] = new UnityEx.ValueTuple<int, int>(this._InitNull.Item3, this.HEnterCategory);
		}
		this.lstMarker[0] = new UnityEx.ValueTuple<GameObject, HPoint>(component.markerPos.gameObject, component);
		if (this.initNullObj.transform.parent != base.transform)
		{
			this.initNullObj.transform.SetParent(base.transform);
		}
		this.initNullObj.transform.position = this._InitNull.Item1;
		this.initNullObj.transform.rotation = this._InitNull.Item2;
		if (!this.InitUsePoint)
		{
			this.ctrlFlag.nowHPoint = component;
		}
	}

	// Token: 0x060050BF RID: 20671 RVA: 0x001F96C4 File Offset: 0x001F7AC4
	private void Update()
	{
		if (!this.IsMarker)
		{
			return;
		}
		int num = -1;
		if (!this.hSceneSprite.IsSpriteOver())
		{
			Ray ray = this.Cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
			int layerMask = 1;
			int num2 = Physics.RaycastNonAlloc(ray, this.HitInfo, 1000f, layerMask);
			if (num2 > 0)
			{
				Array.Sort<RaycastHit>(this.HitInfo, 0, num2, new HPointCtrl.RayDistanceCompare());
				num = this.HitObjectFind(num2);
			}
		}
		if (num == -1)
		{
			return;
		}
		if (!this.ctrlFlag.nowOrgasm && !this.hScene.NowChangeAnim && Singleton<Manager.Input>.Instance.IsPressedKey(ActionID.MouseLeft))
		{
			this.ctrlFlag.pointMoveAnimChange = true;
			HScene.AnimationListInfo nowAnimationInfo = this.ctrlFlag.nowAnimationInfo;
			this.fade.FadeStart(1.5f);
			this.OffSetMove(num, nowAnimationInfo);
			if (this.lstMarker[this.ctrlFlag.HPointID].Item2.id != this.lstMarker[num].Item2.id)
			{
				this.ChangeAnim(num);
			}
			else
			{
				this.ctrlFlag.pointMoveAnimChange = false;
			}
			this.ctrlFlag.HPointID = num;
			ChangeHItem componentInChildren = this.ctrlFlag.nowHPoint.GetComponentInChildren<ChangeHItem>();
			if (componentInChildren != null)
			{
				componentInChildren.ChangeActive(true);
			}
			this.lstMarker[this.ctrlFlag.HPointID].Item2.SetEffectActive(false);
			componentInChildren = this.lstMarker[this.ctrlFlag.HPointID].Item2.GetComponentInChildren<ChangeHItem>();
			if (componentInChildren != null)
			{
				componentInChildren.ChangeActive(false);
			}
			this.ctrlFlag.nowHPoint = this.lstMarker[this.ctrlFlag.HPointID].Item2;
			this.hSceneSprite.RefleshAutoButtom();
			this.hSceneSprite.usePoint = true;
			this.hSceneSprite.SetMotionListDraw(false, -1);
			this.hSceneSprite.LoadMotionList(this.ctrlFlag.categoryMotionList);
			this.hSceneSprite.categoryMain.gameObject.SetActive(false);
			this.hSceneSprite.MarkerObjSet();
		}
	}

	// Token: 0x060050C0 RID: 20672 RVA: 0x001F9918 File Offset: 0x001F7D18
	private void OffSetMove(int hitID, HScene.AnimationListInfo info)
	{
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		int num = -1;
		for (int i = 0; i < info.lstOffset.Count; i++)
		{
			if (this.lstMarker[hitID].Item2._nPlace[0].Item1 == info.nPositons[i])
			{
				num = i;
				break;
			}
		}
		if (num >= 0 && info.lstOffset[num] != null && info.lstOffset[num] != string.Empty)
		{
			this.hScene.LoadMoveOffset(info.lstOffset[num], out zero, out zero2);
		}
		this.hScene.SetMovePositionPoint(this.lstMarker[hitID].Item2.pivot, zero, zero2);
	}

	// Token: 0x060050C1 RID: 20673 RVA: 0x001F9A0C File Offset: 0x001F7E0C
	private void ChangeAnim(int hitID)
	{
		List<HScene.AnimationListInfo> infos = new List<HScene.AnimationListInfo>();
		int num = this.lstMarker[hitID].Item2._nPlace[0].Item1;
		if (this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai && num != 0 && num != 7)
		{
			num = this.YobaiPlaceCheck(num, this.lstMarker[hitID].Item2._nPlace);
		}
		if (this.hSceneManager.bMerchant && num >= this.NotMerchantPlaceID)
		{
			for (int i = 0; i < this.lstMarker[hitID].Item2._nPlace.Count; i++)
			{
				if (this.lstMarker[hitID].Item2._nPlace[i].Item1 < this.NotMerchantPlaceID)
				{
					num = this.lstMarker[hitID].Item2._nPlace[i].Item1;
					break;
				}
			}
		}
		if (this.ctrlFlag.nowAnimationInfo.nPositons.Contains(num))
		{
			this.ctrlFlag.pointMoveAnimChange = false;
			this.ctrlFlag.nPlace = num;
			return;
		}
		List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> list = this.startList;
		if (this.hSceneManager.bMerchant)
		{
			list = this.startListM;
		}
		if (this.playerSex == 0)
		{
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j].Item2 == num)
				{
					if (!this.ExistSecondfemale || list[j].Item3.mode == 5)
					{
						if (this.ExistSecondfemale || list[j].Item3.mode != 5)
						{
							this.ctrlFlag.nPlace = num;
							int mode = list[j].Item3.mode;
							int id = list[j].Item3.id;
							if (mode != 4)
							{
								if (!this.hSceneManager.bMerchant || this.hSceneManager.MerchantLimit != 1 || mode == 1)
								{
									if (!this.lstMarker[hitID].Item2.notMotion[mode].motionID.Contains(id))
									{
										if (this.lstAnimInfo.Length >= mode)
										{
											int num2 = -1;
											for (int k = 0; k < this.lstAnimInfo[mode].Count; k++)
											{
												if (this.lstAnimInfo[mode][k].id == id)
												{
													num2 = k;
												}
											}
											if (num2 != -1)
											{
												if (this.hSceneManager.bMerchant)
												{
													if (!this.lstAnimInfo[mode][num2].bMerchantMotion)
													{
														goto IL_4FD;
													}
													if (this.lstAnimInfo[mode][num2].nIyaAction == 2)
													{
														goto IL_4FD;
													}
												}
												else if (this.hSceneManager.EventKind != HSceneManager.HEvent.Yobai)
												{
													if (this.hSceneManager.isForce)
													{
														if (this.lstAnimInfo[mode][num2].nIyaAction == 0)
														{
															goto IL_4FD;
														}
													}
													else if (this.lstAnimInfo[mode][num2].nIyaAction == 2)
													{
														goto IL_4FD;
													}
												}
												else if (!this.lstAnimInfo[mode][num2].bSleep)
												{
													goto IL_4FD;
												}
												if (!this.lstAnimInfo[mode][num2].isNeedItem || this.hSceneManager.CheckHadItem(this.lstAnimInfo[mode][num2].ActionCtrl.Item1, this.lstAnimInfo[mode][num2].id))
												{
													if (this.ctrlFlag.isFaintness)
													{
														if (this.lstAnimInfo[mode][num2].nDownPtn == 0)
														{
															goto IL_4FD;
														}
													}
													else if (this.lstAnimInfo[mode][num2].nFaintnessLimit == 1)
													{
														goto IL_4FD;
													}
													if (this.lstAnimInfo[mode][num2].nInitiativeFemale == 0)
													{
														infos.Add(this.lstAnimInfo[mode][num2]);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				IL_4FD:;
			}
			if (infos.Count != 0)
			{
				int randID = 0;
				this.rand.Init(infos.Count);
				randID = this.rand.Get();
				this.ctrlFlag.selectAnimationListInfo = infos[randID];
				Observable.FromCoroutine(() => this.hScene.ChangeAnimation(infos[randID], false, false, false), false).Finally(delegate
				{
					this.ctrlFlag.selectAnimationListInfo = null;
					this.ctrlFlag.isAutoActionChange = false;
					if (this.ctrlFlag.pointMoveAnimChange)
					{
						this.ctrlFlag.pointMoveAnimChange = false;
					}
					GlobalMethod.setCameraMoveFlag(this.ctrlFlag.cameraCtrl, true);
					this.hSceneSprite.ChangeStart = false;
				}).Subscribe<Unit>();
			}
		}
		else if (this.playerSex == 1)
		{
			for (int l = 0; l < list.Count; l++)
			{
				if (list[l].Item2 == num)
				{
					if (list[l].Item3.mode == 4)
					{
						this.ctrlFlag.nPlace = num;
						int mode2 = list[l].Item3.mode;
						int id2 = list[l].Item3.id;
						if (!this.lstMarker[hitID].Item2.notMotion[mode2].motionID.Contains(id2))
						{
							if (this.lstAnimInfo.Length >= mode2)
							{
								int num3 = -1;
								for (int m = 0; m < this.lstAnimInfo[mode2].Count; m++)
								{
									if (this.lstAnimInfo[mode2][m].id == id2)
									{
										num3 = m;
									}
								}
								if (num3 != -1)
								{
									if (!this.hSceneManager.bMerchant || this.lstAnimInfo[mode2][num3].bMerchantMotion)
									{
										if (!this.lstAnimInfo[mode2][num3].isNeedItem || this.hSceneManager.CheckHadItem(this.lstAnimInfo[mode2][num3].ActionCtrl.Item1, this.lstAnimInfo[mode2][num3].id))
										{
											if (this.ctrlFlag.isFaintness)
											{
												if (this.lstAnimInfo[mode2][num3].nDownPtn == 0)
												{
													goto IL_7FC;
												}
											}
											else if (this.lstAnimInfo[mode2][num3].nFaintnessLimit == 1)
											{
												goto IL_7FC;
											}
											if (this.lstAnimInfo[mode2][num3].nInitiativeFemale == 0)
											{
												infos.Add(this.lstAnimInfo[mode2][num3]);
											}
										}
									}
								}
							}
						}
					}
				}
				IL_7FC:;
			}
			if (infos.Count != 0)
			{
				int randID = 0;
				this.rand.Init(infos.Count);
				randID = this.rand.Get();
				this.ctrlFlag.selectAnimationListInfo = infos[randID];
				Observable.FromCoroutine(() => this.hScene.ChangeAnimation(infos[randID], false, false, false), false).Finally(delegate
				{
					this.ctrlFlag.selectAnimationListInfo = null;
					this.ctrlFlag.isAutoActionChange = false;
					if (this.ctrlFlag.pointMoveAnimChange)
					{
						this.ctrlFlag.pointMoveAnimChange = false;
					}
					GlobalMethod.setCameraMoveFlag(this.ctrlFlag.cameraCtrl, true);
					this.hSceneSprite.ChangeStart = false;
				}).Subscribe<Unit>();
			}
		}
	}

	// Token: 0x060050C2 RID: 20674 RVA: 0x001FA2C0 File Offset: 0x001F86C0
	public static bool DicTupleContainsValue(Dictionary<int, UnityEx.ValueTuple<int, int>> dic, int target, int item)
	{
		foreach (KeyValuePair<int, UnityEx.ValueTuple<int, int>> keyValuePair in dic)
		{
			if ((item == 0 && keyValuePair.Value.Item1 == target) || (item == 1 && keyValuePair.Value.Item2 == target))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060050C3 RID: 20675 RVA: 0x001FA354 File Offset: 0x001F8754
	public static bool DicTupleContainsValue(Dictionary<int, UnityEx.ValueTuple<int, int>> dic, List<int> target, int item)
	{
		foreach (KeyValuePair<int, UnityEx.ValueTuple<int, int>> keyValuePair in dic)
		{
			if ((item == 0 && target.Contains(keyValuePair.Value.Item1)) || (item == 1 && target.Contains(keyValuePair.Value.Item2)))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060050C4 RID: 20676 RVA: 0x001FA3F4 File Offset: 0x001F87F4
	public void SetHSceneSprite(HSceneSprite sprite, HScene HScene)
	{
		this.ctrlFlag = Singleton<HSceneFlagCtrl>.Instance;
		this.hSceneSprite = sprite;
		this.hScene = HScene;
	}

	// Token: 0x060050C5 RID: 20677 RVA: 0x001FA40F File Offset: 0x001F880F
	public void CameraSet()
	{
		this.Cam = this.ctrlFlag.cameraCtrl.thisCamera;
	}

	// Token: 0x060050C6 RID: 20678 RVA: 0x001FA428 File Offset: 0x001F8828
	private int HitObjectFind(int hitNum)
	{
		int result = -1;
		for (int i = 0; i < hitNum; i++)
		{
			for (int j = 0; j < this.lstMarker.Count; j++)
			{
				Collider collider = this.lstMarker[j].Item2.GetCollider();
				if (!(collider == null) && !(collider != this.HitInfo[i].collider) && j != this.ctrlFlag.HPointID)
				{
					return j;
				}
			}
		}
		return result;
	}

	// Token: 0x060050C7 RID: 20679 RVA: 0x001FA4C4 File Offset: 0x001F88C4
	public bool CheckStartPoint(ref Transform res, int place = -2)
	{
		int mapID = Singleton<Map>.Instance.MapID;
		int areaID = this.hSceneManager.Player.AreaID;
		HPointCtrl.MinHpoint minHpoint;
		minHpoint.AreaID = -1;
		minHpoint.PointID = -1;
		minHpoint.yobaiPlace = -1;
		int num = -1;
		List<HPointCtrl.MinHpoint> list = new List<HPointCtrl.MinHpoint>();
		this.gpID = -1;
		Dictionary<int, HPointCtrl.AreaGroupDefine> dictionary;
		if (!this.Areagp.TryGetValue(mapID, out dictionary))
		{
			return false;
		}
		foreach (KeyValuePair<int, HPointCtrl.AreaGroupDefine> keyValuePair in dictionary)
		{
			if (keyValuePair.Value.IDs.Contains(areaID))
			{
				this.gpID = keyValuePair.Key;
				break;
			}
		}
		List<bool> list2 = new List<bool>();
		if (this.gpID == -1)
		{
			list2.Add(this.hPointLists.Count == 0 || !this.hPointLists.ContainsKey(mapID) || !this.hPointLists[mapID].lst.ContainsKey(areaID) || this.hPointLists[mapID].lst[areaID].Count == 0);
		}
		else
		{
			for (int i = 0; i < dictionary[this.gpID].IDs.Count; i++)
			{
				int key = dictionary[this.gpID].IDs[i];
				list2.Add(this.hPointLists.Count == 0 || !this.hPointLists.ContainsKey(mapID) || !this.hPointLists[mapID].lst.ContainsKey(key) || this.hPointLists[mapID].lst[key].Count == 0);
			}
		}
		bool flag = this.housingHpoints == null || this.housingHpoints.Length == 0;
		bool[] array = new bool[2];
		if (!list2.Contains(false) && flag)
		{
			return false;
		}
		if (list2.Contains(false))
		{
			if (this.gpID == -1)
			{
				minHpoint.AreaID = areaID;
				minHpoint.PointID = this.PlayerClosePointID(res, this.hPointLists[mapID].lst[areaID], place);
			}
			else
			{
				for (int j = 0; j < dictionary[this.gpID].IDs.Count; j++)
				{
					if (!list2[j])
					{
						list.Add(new HPointCtrl.MinHpoint
						{
							AreaID = dictionary[this.gpID].IDs[j],
							PointID = this.PlayerClosePointID(res, this.hPointLists[mapID].lst[dictionary[this.gpID].IDs[j]], place)
						});
					}
				}
				foreach (HPointCtrl.MinHpoint minHpoint2 in list)
				{
					if (minHpoint2.PointID != -1)
					{
						if (minHpoint.AreaID == -1 || minHpoint.PointID == -1)
						{
							minHpoint = minHpoint2;
						}
						else
						{
							float sqrMagnitude = (this.hPointLists[mapID].lst[minHpoint2.AreaID][minHpoint2.PointID].transform.position - res.position).sqrMagnitude;
							float sqrMagnitude2 = (this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].transform.position - res.position).sqrMagnitude;
							if (sqrMagnitude < sqrMagnitude2)
							{
								minHpoint.AreaID = minHpoint2.AreaID;
								minHpoint.PointID = minHpoint2.PointID;
							}
						}
					}
				}
			}
		}
		if (minHpoint.AreaID != -1 && minHpoint.PointID != -1)
		{
			array[0] = true;
		}
		if (!flag)
		{
			num = this.PlayerClosePointID(res, this.housingHpoints, place);
		}
		if (num != -1)
		{
			array[1] = true;
		}
		if (!array[0] && !array[1])
		{
			return false;
		}
		bool flag2 = true;
		if (array[0] && array[1])
		{
			float num2 = (this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].transform.position - res.position).sqrMagnitude - (this.HousingHpoints[num].transform.position - res.position).sqrMagnitude;
			if (num2 >= 0f)
			{
				res.position = this.HousingHpoints[num].pivot.position;
				res.rotation = this.HousingHpoints[num].pivot.rotation;
				if (this.hSceneManager.females[1] == null || this.hSceneManager.females[1].GetComponent<PlayerActor>() != null)
				{
					if (place == -2)
					{
						this.hSceneManager.height = this.housingHpoints[num]._nPlace[0].Item1;
					}
					else
					{
						this.hSceneManager.height = place;
					}
				}
				else
				{
					this.hSceneManager.height = 0;
				}
				flag2 = false;
			}
			else
			{
				res.position = this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].pivot.position;
				res.rotation = this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].pivot.rotation;
				if (this.hSceneManager.females[1] == null || this.hSceneManager.females[1].GetComponent<PlayerActor>() != null)
				{
					if (place == -2)
					{
						this.hSceneManager.height = this.housingHpoints[num]._nPlace[0].Item1;
					}
					else
					{
						this.hSceneManager.height = place;
					}
				}
				else
				{
					this.hSceneManager.height = 0;
				}
				flag2 = true;
			}
		}
		else if (array[0])
		{
			res.position = this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].pivot.position;
			res.rotation = this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].pivot.rotation;
			if (this.hSceneManager.females[1] == null || this.hSceneManager.females[1].GetComponent<PlayerActor>() != null)
			{
				if (place == -2)
				{
					this.hSceneManager.height = this.housingHpoints[num]._nPlace[0].Item1;
				}
				else
				{
					this.hSceneManager.height = place;
				}
			}
			else
			{
				this.hSceneManager.height = 0;
			}
			flag2 = true;
		}
		else if (array[1])
		{
			res.position = this.HousingHpoints[num].pivot.position;
			res.rotation = this.HousingHpoints[num].pivot.rotation;
			if (this.hSceneManager.females[1] == null || this.hSceneManager.females[1].GetComponent<PlayerActor>() != null)
			{
				if (place == -2)
				{
					this.hSceneManager.height = this.housingHpoints[num]._nPlace[0].Item1;
				}
				else
				{
					this.hSceneManager.height = place;
				}
			}
			else
			{
				this.hSceneManager.height = 0;
			}
			flag2 = false;
		}
		if (flag2)
		{
			int num3 = 0;
			if (this.gpID != -1)
			{
				foreach (KeyValuePair<int, List<HPoint>> keyValuePair2 in this.hPointLists[mapID].lst)
				{
					if (dictionary.ContainsKey(this.gpID) && dictionary[this.gpID].IDs.Contains(keyValuePair2.Key) && keyValuePair2.Key < minHpoint.AreaID)
					{
						num3 += keyValuePair2.Value.Count;
					}
				}
			}
			this.ctrlFlag.HPointID = num3 + minHpoint.PointID + 1;
			this.ctrlFlag.nowHPoint = this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID];
		}
		else
		{
			if (!list2.Contains(false))
			{
				this.ctrlFlag.HPointID = num + 1;
			}
			else if (this.gpID == -1)
			{
				this.ctrlFlag.HPointID = num + 1 + this.hPointLists[mapID].lst[areaID].Count;
			}
			else
			{
				int num4 = 0;
				for (int k = 0; k < dictionary[this.gpID].IDs.Count; k++)
				{
					if (!list2[k])
					{
						num4 += this.hPointLists[mapID].lst[dictionary[this.gpID].IDs[k]].Count;
					}
				}
				this.ctrlFlag.HPointID = num + 1 + num4;
			}
			this.ctrlFlag.nowHPoint = this.housingHpoints[num];
		}
		ChangeHItem componentInChildren = this.ctrlFlag.nowHPoint.GetComponentInChildren<ChangeHItem>();
		if (componentInChildren != null)
		{
			componentInChildren.ChangeActive(false);
		}
		return true;
	}

	// Token: 0x060050C8 RID: 20680 RVA: 0x001FB008 File Offset: 0x001F9408
	public bool CheckStartPointYobai(ref Transform res)
	{
		int mapID = Singleton<Map>.Instance.MapID;
		int areaID = this.hSceneManager.Player.AreaID;
		HPointCtrl.MinHpoint minHpoint;
		minHpoint.AreaID = -1;
		minHpoint.PointID = -1;
		minHpoint.yobaiPlace = -1;
		int[] array = new int[2];
		int num = -1;
		int num2 = 0;
		List<HPointCtrl.MinHpoint> list = new List<HPointCtrl.MinHpoint>();
		this.gpID = -1;
		Dictionary<int, HPointCtrl.AreaGroupDefine> dictionary;
		if (!this.Areagp.TryGetValue(mapID, out dictionary))
		{
			return false;
		}
		foreach (KeyValuePair<int, HPointCtrl.AreaGroupDefine> keyValuePair in dictionary)
		{
			if (keyValuePair.Value.IDs.Contains(areaID))
			{
				this.gpID = keyValuePair.Key;
				break;
			}
		}
		List<bool> list2 = new List<bool>();
		if (this.gpID == -1)
		{
			list2.Add(this.hPointLists.Count == 0 || !this.hPointLists.ContainsKey(mapID) || !this.hPointLists[mapID].lst.ContainsKey(areaID) || this.hPointLists[mapID].lst[areaID].Count == 0);
		}
		else
		{
			for (int i = 0; i < dictionary[this.gpID].IDs.Count; i++)
			{
				int key = dictionary[this.gpID].IDs[i];
				list2.Add(this.hPointLists.Count == 0 || !this.hPointLists.ContainsKey(mapID) || !this.hPointLists[mapID].lst.ContainsKey(key) || this.hPointLists[mapID].lst[key].Count == 0);
			}
		}
		bool flag = this.housingHpoints == null || this.housingHpoints.Length == 0;
		bool[] array2 = new bool[2];
		if (!list2.Contains(false) && flag)
		{
			return false;
		}
		if (list2.Contains(false))
		{
			if (this.gpID == -1)
			{
				minHpoint.AreaID = areaID;
				array[0] = this.PlayerClosePointID(res, this.hPointLists[mapID].lst[areaID], 0);
				array[1] = this.PlayerClosePointID(res, this.hPointLists[mapID].lst[areaID], 7);
				if (array[0] != array[1])
				{
					if (array[0] == -1)
					{
						minHpoint.PointID = array[1];
						minHpoint.yobaiPlace = 1;
					}
					else if (array[1] == -1)
					{
						minHpoint.PointID = array[0];
						minHpoint.yobaiPlace = 0;
					}
					else
					{
						minHpoint.yobaiPlace = this.YobaiMin(res.position, this.hPointLists[mapID].lst[areaID], array);
						minHpoint.PointID = array[minHpoint.yobaiPlace];
					}
				}
				else
				{
					minHpoint.PointID = array[0];
					minHpoint.yobaiPlace = 0;
				}
			}
			else
			{
				for (int j = 0; j < dictionary[this.gpID].IDs.Count; j++)
				{
					if (!list2[j])
					{
						array[0] = this.PlayerClosePointID(res, this.hPointLists[mapID].lst[dictionary[this.gpID].IDs[j]], 0);
						array[1] = this.PlayerClosePointID(res, this.hPointLists[mapID].lst[dictionary[this.gpID].IDs[j]], 7);
						int pointID;
						int num3;
						if (array[0] != array[1])
						{
							if (array[0] == -1)
							{
								pointID = array[1];
								num3 = 1;
							}
							else if (array[1] == -1)
							{
								pointID = array[0];
								num3 = 0;
							}
							else
							{
								num3 = this.YobaiMin(res.position, this.hPointLists[mapID].lst[areaID], array);
								pointID = array[num3];
							}
						}
						else
						{
							pointID = array[0];
							num3 = 0;
						}
						list.Add(new HPointCtrl.MinHpoint
						{
							AreaID = dictionary[this.gpID].IDs[j],
							PointID = pointID,
							yobaiPlace = num3
						});
					}
				}
				foreach (HPointCtrl.MinHpoint minHpoint2 in list)
				{
					if (minHpoint2.PointID != -1)
					{
						if (minHpoint.AreaID == -1 || minHpoint.PointID == -1)
						{
							minHpoint = minHpoint2;
						}
						else
						{
							float sqrMagnitude = (this.hPointLists[mapID].lst[minHpoint2.AreaID][minHpoint2.PointID].transform.position - res.position).sqrMagnitude;
							float sqrMagnitude2 = (this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].transform.position - res.position).sqrMagnitude;
							if (sqrMagnitude < sqrMagnitude2)
							{
								minHpoint.AreaID = minHpoint2.AreaID;
								minHpoint.PointID = minHpoint2.PointID;
								minHpoint.yobaiPlace = minHpoint2.yobaiPlace;
							}
						}
					}
				}
			}
		}
		if (minHpoint.AreaID != -1 && minHpoint.PointID != -1)
		{
			array2[0] = true;
		}
		if (!flag)
		{
			int[] array3 = new int[]
			{
				this.PlayerClosePointID(res, this.housingHpoints, 0),
				this.PlayerClosePointID(res, this.housingHpoints, 7)
			};
			if (array3[0] != array3[1])
			{
				if (array3[0] == -1)
				{
					num = array3[1];
					num2 = 1;
				}
				else if (array3[1] == -1)
				{
					num = array3[0];
					num2 = 0;
				}
				else
				{
					num2 = this.YobaiMin(res.position, this.housingHpoints, array3);
					num = array3[num2];
				}
			}
			else
			{
				num = array3[0];
				num2 = 0;
			}
		}
		if (num != -1)
		{
			array2[1] = true;
		}
		if (!array2[0] && !array2[1])
		{
			return false;
		}
		bool flag2 = true;
		if (array2[0] && array2[1])
		{
			float num4 = (this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].transform.position - res.position).sqrMagnitude - (this.HousingHpoints[num].transform.position - res.position).sqrMagnitude;
			if (num4 >= 0f)
			{
				res.position = this.HousingHpoints[num].pivot.position;
				res.rotation = this.HousingHpoints[num].pivot.rotation;
				if (this.hSceneManager.females[1] == null || this.hSceneManager.females[1].GetComponent<PlayerActor>() != null)
				{
					this.hSceneManager.height = ((num2 != 0) ? 7 : 0);
				}
				else
				{
					this.hSceneManager.height = 0;
				}
				flag2 = false;
			}
			else
			{
				res.position = this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].pivot.position;
				res.rotation = this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].pivot.rotation;
				if (this.hSceneManager.females[1] == null || this.hSceneManager.females[1].GetComponent<PlayerActor>() != null)
				{
					this.hSceneManager.height = ((minHpoint.yobaiPlace != 0) ? 7 : 0);
				}
				else
				{
					this.hSceneManager.height = 0;
				}
				flag2 = true;
			}
		}
		else if (array2[0])
		{
			res.position = this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].pivot.position;
			res.rotation = this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID].pivot.rotation;
			if (this.hSceneManager.females[1] == null || this.hSceneManager.females[1].GetComponent<PlayerActor>() != null)
			{
				this.hSceneManager.height = ((minHpoint.yobaiPlace != 0) ? 7 : 0);
			}
			else
			{
				this.hSceneManager.height = 0;
			}
			flag2 = true;
		}
		else if (array2[1])
		{
			res.position = this.HousingHpoints[num].pivot.position;
			res.rotation = this.HousingHpoints[num].pivot.rotation;
			if (this.hSceneManager.females[1] == null || this.hSceneManager.females[1].GetComponent<PlayerActor>() != null)
			{
				this.hSceneManager.height = ((num2 != 0) ? 7 : 0);
			}
			else
			{
				this.hSceneManager.height = 0;
			}
			flag2 = false;
		}
		if (flag2)
		{
			int num5 = 0;
			if (this.gpID != -1)
			{
				foreach (KeyValuePair<int, List<HPoint>> keyValuePair2 in this.hPointLists[mapID].lst)
				{
					if (dictionary.ContainsKey(this.gpID) && dictionary[this.gpID].IDs.Contains(keyValuePair2.Key) && keyValuePair2.Key < minHpoint.AreaID)
					{
						num5 += keyValuePair2.Value.Count;
					}
				}
			}
			this.ctrlFlag.HPointID = num5 + minHpoint.PointID + 1;
			this.ctrlFlag.nowHPoint = this.hPointLists[mapID].lst[minHpoint.AreaID][minHpoint.PointID];
		}
		else
		{
			if (!list2.Contains(false))
			{
				this.ctrlFlag.HPointID = num + 1;
			}
			else if (this.gpID == -1)
			{
				this.ctrlFlag.HPointID = num + 1 + this.hPointLists[mapID].lst[areaID].Count;
			}
			else
			{
				int num6 = 0;
				for (int k = 0; k < dictionary[this.gpID].IDs.Count; k++)
				{
					if (!list2[k])
					{
						num6 += this.hPointLists[mapID].lst[dictionary[this.gpID].IDs[k]].Count;
					}
				}
				this.ctrlFlag.HPointID = num + 1 + num6;
			}
			this.ctrlFlag.nowHPoint = this.housingHpoints[num];
		}
		ChangeHItem componentInChildren = this.ctrlFlag.nowHPoint.GetComponentInChildren<ChangeHItem>();
		if (componentInChildren != null)
		{
			componentInChildren.ChangeActive(false);
		}
		return true;
	}

	// Token: 0x060050C9 RID: 20681 RVA: 0x001FBCDC File Offset: 0x001FA0DC
	public void HousingHStart(HPoint hPoint)
	{
		int mapID = Singleton<Map>.Instance.MapID;
		int areaID = this.hSceneManager.Player.AreaID;
		this.gpID = -1;
		Dictionary<int, HPointCtrl.AreaGroupDefine> dictionary;
		if (!this.Areagp.TryGetValue(mapID, out dictionary))
		{
			return;
		}
		foreach (KeyValuePair<int, HPointCtrl.AreaGroupDefine> keyValuePair in dictionary)
		{
			if (keyValuePair.Value.IDs.Contains(areaID))
			{
				this.gpID = keyValuePair.Key;
				break;
			}
		}
		List<bool> list = new List<bool>();
		if (this.gpID == -1)
		{
			list.Add(this.hPointLists.Count == 0 || !this.hPointLists.ContainsKey(mapID) || !this.hPointLists[mapID].lst.ContainsKey(areaID) || this.hPointLists[mapID].lst[areaID].Count == 0);
		}
		else
		{
			for (int i = 0; i < dictionary[this.gpID].IDs.Count; i++)
			{
				int key = dictionary[this.gpID].IDs[i];
				list.Add(this.hPointLists.Count == 0 || !this.hPointLists.ContainsKey(mapID) || !this.hPointLists[mapID].lst.ContainsKey(key) || this.hPointLists[mapID].lst[key].Count == 0);
			}
		}
		int num = this.PlayerClosePointID(hPoint, this.housingHpoints, -2);
		if (this.gpID == -1)
		{
			this.ctrlFlag.HPointID = num + 1 + this.hPointLists[mapID].lst[areaID].Count;
		}
		else
		{
			int num2 = 0;
			for (int j = 0; j < dictionary[this.gpID].IDs.Count; j++)
			{
				if (!list[j])
				{
					num2 += this.hPointLists[mapID].lst[dictionary[this.gpID].IDs[j]].Count;
				}
			}
			this.ctrlFlag.HPointID = num + 1 + num2;
		}
		this.ctrlFlag.nowHPoint = this.housingHpoints[num];
		if (this.ctrlFlag.HPointID < this.lstMarker.Count)
		{
			ChangeHItem componentInChildren = this.lstMarker[this.ctrlFlag.HPointID].Item2.GetComponentInChildren<ChangeHItem>();
			if (componentInChildren != null)
			{
				componentInChildren.ChangeActive(false);
			}
		}
	}

	// Token: 0x060050CA RID: 20682 RVA: 0x001FC000 File Offset: 0x001FA400
	private int PlayerClosePointID(Transform nowPos, List<HPoint> hPoints, int nPlace = -2)
	{
		int result = -1;
		float num = this.hSceneSprite.HpointSearchRange * this.hSceneSprite.HpointSearchRange;
		float num2 = float.MaxValue;
		float y = nowPos.position.y;
		int num3 = 0;
		if (this.hSceneManager.EventKind != HSceneManager.HEvent.GyakuYobai && this.hSceneManager.EventKind != HSceneManager.HEvent.Yobai && this.hSceneManager.EventKind != HSceneManager.HEvent.FromFemale && this.hSceneManager.Agent[1] == null)
		{
			num3 = 1;
		}
		int i = 0;
		while (i < hPoints.Count)
		{
			int num4 = i;
			if (nPlace == -1)
			{
				if (HPointCtrl.DicTupleContainsValue(hPoints[num4]._nPlace, 0, 0) || HPointCtrl.DicTupleContainsValue(hPoints[num4]._nPlace, 1, 0))
				{
					goto IL_F8;
				}
			}
			else if (nPlace == -2 || HPointCtrl.DicTupleContainsValue(hPoints[num4]._nPlace, nPlace, 0))
			{
				goto IL_F8;
			}
			IL_2F2:
			i++;
			continue;
			IL_F8:
			if (!this.CheckEvent(hPoints[num4]._nPlace, this.hSceneManager.EventKind))
			{
				goto IL_2F2;
			}
			if ((this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai || this.hSceneManager.EventKind == HSceneManager.HEvent.GyakuYobai) && hPoints[num4].id != 1 && hPoints[num4].id != 17)
			{
				goto IL_2F2;
			}
			if (this.hSceneManager.EventKind != HSceneManager.HEvent.TsukueBare)
			{
				if (hPoints[num4].id == this.kadOnaID)
				{
					goto IL_2F2;
				}
			}
			else if (hPoints[num4].id != this.kadOnaID)
			{
				goto IL_2F2;
			}
			if (!this.CanInfo(hPoints[num4]._nPlace))
			{
				goto IL_2F2;
			}
			if (num3 == 0)
			{
				if (hPoints[num4].transform.position.y < y - this.OffsetDownHeight || hPoints[num4].transform.position.y > y + this.OffsetUpHeight)
				{
					goto IL_2F2;
				}
				Vector2 vector = default(Vector2);
				vector.x = hPoints[num4].transform.position.x - nowPos.position.x;
				vector.y = hPoints[num4].transform.position.z - nowPos.position.z;
				num2 = vector.sqrMagnitude;
			}
			else if (num3 == 1)
			{
				num2 = (hPoints[num4].transform.position - nowPos.position).sqrMagnitude;
			}
			if (num <= num2)
			{
				goto IL_2F2;
			}
			num = num2;
			result = num4;
			goto IL_2F2;
		}
		return result;
	}

	// Token: 0x060050CB RID: 20683 RVA: 0x001FC314 File Offset: 0x001FA714
	private int PlayerClosePointID(HPoint now, HPoint[] hPoints, int nPlace = -2)
	{
		int result = -1;
		for (int i = 0; i < hPoints.Length; i++)
		{
			if (!(now != hPoints[i]))
			{
				result = i;
				break;
			}
		}
		return result;
	}

	// Token: 0x060050CC RID: 20684 RVA: 0x001FC354 File Offset: 0x001FA754
	private int PlayerClosePointID(Transform nowPos, HPoint[] hPoints, int nPlace = -2)
	{
		int result = -1;
		float num = this.hSceneSprite.HpointSearchRange * this.hSceneSprite.HpointSearchRange;
		float num2 = float.MaxValue;
		float y = nowPos.position.y;
		int num3 = 0;
		if (this.hSceneManager.EventKind != HSceneManager.HEvent.GyakuYobai && this.hSceneManager.EventKind != HSceneManager.HEvent.Yobai && this.hSceneManager.EventKind != HSceneManager.HEvent.FromFemale && this.hSceneManager.Agent[1] == null)
		{
			num3 = 1;
		}
		int i = 0;
		while (i < hPoints.Length)
		{
			int num4 = i;
			if (nPlace == -1)
			{
				if (HPointCtrl.DicTupleContainsValue(hPoints[num4]._nPlace, 0, 0) || HPointCtrl.DicTupleContainsValue(hPoints[num4]._nPlace, 1, 0))
				{
					goto IL_EC;
				}
			}
			else if (nPlace == -2 || HPointCtrl.DicTupleContainsValue(hPoints[num4]._nPlace, nPlace, 0))
			{
				goto IL_EC;
			}
			IL_273:
			i++;
			continue;
			IL_EC:
			if (!this.CheckEvent(hPoints[num4]._nPlace, this.hSceneManager.EventKind))
			{
				goto IL_273;
			}
			if (!this.CanInfo(hPoints[num4]._nPlace))
			{
				goto IL_273;
			}
			if (this.hSceneManager.EventKind != HSceneManager.HEvent.TsukueBare)
			{
				if (hPoints[num4].id == this.kadOnaID)
				{
					goto IL_273;
				}
			}
			else if (hPoints[num4].id != this.kadOnaID)
			{
				goto IL_273;
			}
			if (num3 == 0)
			{
				if (hPoints[num4].transform.position.y < y - this.OffsetDownHeight || hPoints[num4].transform.position.y > y + this.OffsetUpHeight)
				{
					goto IL_273;
				}
				Vector2 vector = default(Vector2);
				vector.x = hPoints[num4].transform.position.x - nowPos.position.x;
				vector.y = hPoints[num4].transform.position.z - nowPos.position.z;
				num2 = vector.sqrMagnitude;
			}
			else if (num3 == 1)
			{
				num2 = (hPoints[num4].transform.position - nowPos.position).sqrMagnitude;
			}
			if (num <= num2)
			{
				goto IL_273;
			}
			num = num2;
			result = num4;
			goto IL_273;
		}
		return result;
	}

	// Token: 0x060050CD RID: 20685 RVA: 0x001FC5E8 File Offset: 0x001FA9E8
	private bool CheckEvent(Dictionary<int, UnityEx.ValueTuple<int, int>> dic, HSceneManager.HEvent Event)
	{
		switch (Event)
		{
		case HSceneManager.HEvent.Bath:
			return HPointCtrl.DicTupleContainsValue(dic, 11, 0);
		case HSceneManager.HEvent.Toilet1:
			return HPointCtrl.DicTupleContainsValue(dic, 13, 0);
		case HSceneManager.HEvent.Kitchen:
			return HPointCtrl.DicTupleContainsValue(dic, 9, 0);
		case HSceneManager.HEvent.Tachi:
		case HSceneManager.HEvent.MapBath:
			return HPointCtrl.DicTupleContainsValue(dic, 1, 0);
		case HSceneManager.HEvent.Stairs:
		case HSceneManager.HEvent.StairsBare:
			return HPointCtrl.DicTupleContainsValue(dic, 10, 0);
		case HSceneManager.HEvent.KabeanaBack:
		case HSceneManager.HEvent.KabeanaFront:
			return HPointCtrl.DicTupleContainsValue(dic, 15, 0);
		case HSceneManager.HEvent.Neonani:
			return HPointCtrl.DicTupleContainsValue(dic, 0, 0);
		case HSceneManager.HEvent.TsukueBare:
			return HPointCtrl.DicTupleContainsValue(dic, 4, 0);
		}
		return true;
	}

	// Token: 0x060050CE RID: 20686 RVA: 0x001FC690 File Offset: 0x001FAA90
	public bool CheckPlace(Dictionary<int, UnityEx.ValueTuple<int, int>> place, int mode)
	{
		int[] array = null;
		if (mode == 0)
		{
			array = this.LesPlaceID;
		}
		else if (mode == 1)
		{
			array = this.multiFemalePlaceID;
		}
		if (array == null)
		{
			return false;
		}
		for (int i = 0; i < array.Length; i++)
		{
			foreach (UnityEx.ValueTuple<int, int> valueTuple in place.Values)
			{
				if (valueTuple.Item1 == array[i])
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060050CF RID: 20687 RVA: 0x001FC740 File Offset: 0x001FAB40
	private bool CanInfo(Dictionary<int, UnityEx.ValueTuple<int, int>> place)
	{
		foreach (UnityEx.ValueTuple<int, int> valueTuple in place.Values)
		{
			if (this.CheckMotionLimit(valueTuple.Item1))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060050D0 RID: 20688 RVA: 0x001FC7B4 File Offset: 0x001FABB4
	private bool CheckMotionLimit(int place)
	{
		if (this.hSceneManager.bMerchant && this.hSceneManager.MerchantLimit == 1)
		{
			if (this.playerSex == 0)
			{
				for (int i = 0; i < this.lstAnimInfo[1].Count; i++)
				{
					if (this.CheckMotionLimit(place, this.lstAnimInfo[1][i]))
					{
						return true;
					}
				}
			}
			else if (this.playerSex == 1)
			{
				for (int j = 0; j < this.lstAnimInfo[4].Count; j++)
				{
					if (this.CheckMotionLimit(place, this.lstAnimInfo[4][j]))
					{
						return true;
					}
				}
			}
		}
		else
		{
			for (int k = 0; k < this.lstAnimInfo.Length; k++)
			{
				for (int l = 0; l < this.lstAnimInfo[k].Count; l++)
				{
					if (this.CheckMotionLimit(place, this.lstAnimInfo[k][l]))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060050D1 RID: 20689 RVA: 0x001FC8DC File Offset: 0x001FACDC
	private bool CheckMotionLimit(int place, HScene.AnimationListInfo lstAnimInfo)
	{
		if (this.playerSex == 0 || (this.playerSex == 1 && this.ctrlFlag.bFutanari))
		{
			if (this.hSceneManager.females[1] == null)
			{
				if (lstAnimInfo.nPromiscuity == 1)
				{
					return false;
				}
			}
			else if (lstAnimInfo.nPromiscuity != 1)
			{
				return false;
			}
			if (lstAnimInfo.nPromiscuity == 2)
			{
				return false;
			}
			if (lstAnimInfo.ActionCtrl.Item1 == 3 && !this.hSceneSprite.NonTokushuCheckIDs.Contains(lstAnimInfo.id) && lstAnimInfo.fileMale == string.Empty)
			{
				return false;
			}
		}
		else if (this.playerSex == 1 && lstAnimInfo.nPromiscuity < 2)
		{
			return false;
		}
		if (!this.hSceneManager.bMerchant)
		{
			if (lstAnimInfo.nHentai == 1 && this.hSceneManager.GetFlaverSkillLevel(2) < 100)
			{
				return false;
			}
			if (lstAnimInfo.nHentai == 2 && this.hSceneManager.GetFlaverSkillLevel(2) < 170)
			{
				return false;
			}
		}
		if (this.hSceneManager.bMerchant)
		{
			if (!lstAnimInfo.bMerchantMotion)
			{
				return false;
			}
			if (lstAnimInfo.nIyaAction == 2)
			{
				return false;
			}
		}
		else if (this.hSceneManager.EventKind != HSceneManager.HEvent.Yobai)
		{
			if (this.hSceneManager.isForce)
			{
				if (lstAnimInfo.nIyaAction < 1)
				{
					return false;
				}
			}
			else if (lstAnimInfo.nIyaAction == 2)
			{
				return false;
			}
		}
		else if (!lstAnimInfo.bSleep)
		{
			return false;
		}
		if (!this.hSceneManager.bMerchant)
		{
			if (!lstAnimInfo.nPositons.Contains(place))
			{
				return false;
			}
		}
		else if (place >= this.NotMerchantPlaceID)
		{
			return false;
		}
		if (lstAnimInfo.isNeedItem && !this.hSceneManager.CheckHadItem(lstAnimInfo.ActionCtrl.Item1, lstAnimInfo.id))
		{
			return false;
		}
		if (lstAnimInfo.nDownPtn == 0 && this.ctrlFlag.isFaintness)
		{
			return false;
		}
		if (lstAnimInfo.nFaintnessLimit == 1 && !this.ctrlFlag.isFaintness)
		{
			return false;
		}
		int initiative = this.ctrlFlag.initiative;
		if (initiative != 0)
		{
			if (initiative != 1)
			{
				if (initiative == 2)
				{
					if (lstAnimInfo.nInitiativeFemale != 2)
					{
						return false;
					}
				}
			}
			else if (lstAnimInfo.nInitiativeFemale == 0)
			{
				return false;
			}
		}
		else if (lstAnimInfo.nInitiativeFemale != 0)
		{
			return false;
		}
		return true;
	}

	// Token: 0x060050D2 RID: 20690 RVA: 0x001FCB90 File Offset: 0x001FAF90
	private int YobaiMin(Vector3 nowPos, List<HPoint> points, int[] check)
	{
		float sqrMagnitude = (points[check[0]].transform.position - nowPos).sqrMagnitude;
		float sqrMagnitude2 = (points[check[1]].transform.position - nowPos).sqrMagnitude;
		return (sqrMagnitude >= sqrMagnitude2) ? 1 : 0;
	}

	// Token: 0x060050D3 RID: 20691 RVA: 0x001FCBF0 File Offset: 0x001FAFF0
	private int YobaiMin(Vector3 nowPos, HPoint[] points, int[] check)
	{
		float sqrMagnitude = (points[check[0]].transform.position - nowPos).sqrMagnitude;
		float sqrMagnitude2 = (points[check[1]].transform.position - nowPos).sqrMagnitude;
		return (sqrMagnitude >= sqrMagnitude2) ? 1 : 0;
	}

	// Token: 0x060050D4 RID: 20692 RVA: 0x001FCC48 File Offset: 0x001FB048
	public void endHScene()
	{
		for (int i = 0; i < this.lstMarker.Count; i++)
		{
			this.lstMarker[i].Item2.SetEffectActive(false);
			Collider collider = this.lstMarker[i].Item2.GetCollider();
			if (collider != null)
			{
				collider.enabled = false;
			}
		}
		this.IsMarker = false;
		this.lstMarker.Clear();
		this.hPointLists = null;
		this.InitUsePoint = false;
	}

	// Token: 0x060050D5 RID: 20693 RVA: 0x001FCCD8 File Offset: 0x001FB0D8
	private bool HitObstacle(Vector3 pos, HPoint point)
	{
		pos.y += 20f;
		Vector3 position = point.transform.position;
		Ray ray = new Ray(pos, (position - pos).normalized);
		int layerMask = 2048;
		int num = Physics.RaycastNonAlloc(ray, this.HitInfo2, this.hSceneSprite.HpointSearchRange, layerMask);
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				if (this.HitInfo2[i].distance <= (position - pos).magnitude)
				{
					foreach (ForcedHideObject forcedHideObject in this.CheckHitObjs)
					{
						Collider[] componentsInChildren = forcedHideObject.GetComponentsInChildren<Collider>();
						foreach (Collider y in componentsInChildren)
						{
							if (this.HitInfo2[i].collider == y)
							{
								return true;
							}
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060050D6 RID: 20694 RVA: 0x001FCE28 File Offset: 0x001FB228
	private bool HpointAreaOpen(HPoint point)
	{
		if (point.OpenID == null || point.OpenID.Count == 0)
		{
			return true;
		}
		bool flag = true;
		for (int i = 0; i < point.OpenID.Count; i++)
		{
			int key = point.OpenID[i];
			flag &= Singleton<Game>.Instance.Environment.AreaOpenState[key];
		}
		return flag;
	}

	// Token: 0x060050D7 RID: 20695 RVA: 0x001FCE96 File Offset: 0x001FB296
	public void EndProc()
	{
		this.housingHpoints = null;
		this.ExistSecondfemale = false;
	}

	// Token: 0x060050D8 RID: 20696 RVA: 0x001FCEA8 File Offset: 0x001FB2A8
	private void AreagpInit()
	{
		Dictionary<int, Dictionary<int, List<int>>> vanishHousingAreaGroup = Singleton<Manager.Resources>.Instance.Map.VanishHousingAreaGroup;
		if (vanishHousingAreaGroup == null)
		{
			return;
		}
		foreach (KeyValuePair<int, Dictionary<int, List<int>>> keyValuePair in vanishHousingAreaGroup)
		{
			if (!this.Areagp.ContainsKey(keyValuePair.Key))
			{
				this.Areagp.Add(keyValuePair.Key, new Dictionary<int, HPointCtrl.AreaGroupDefine>());
			}
			foreach (KeyValuePair<int, List<int>> keyValuePair2 in keyValuePair.Value)
			{
				if (!this.Areagp[keyValuePair.Key].ContainsKey(keyValuePair2.Key))
				{
					this.Areagp[keyValuePair.Key].Add(keyValuePair2.Key, new HPointCtrl.AreaGroupDefine());
				}
				for (int i = 0; i < keyValuePair2.Value.Count; i++)
				{
					if (!this.Areagp[keyValuePair.Key][keyValuePair2.Key].IDs.Contains(keyValuePair2.Value[i]))
					{
						this.Areagp[keyValuePair.Key][keyValuePair2.Key].IDs.Add(keyValuePair2.Value[i]);
					}
				}
			}
		}
	}

	// Token: 0x060050D9 RID: 20697 RVA: 0x001FD074 File Offset: 0x001FB474
	private int YobaiPlaceCheck(int place, Dictionary<int, UnityEx.ValueTuple<int, int>> checkPlaces)
	{
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, UnityEx.ValueTuple<int, int>> keyValuePair in checkPlaces)
		{
			list.Add(keyValuePair.Value.Item1);
		}
		int result = place;
		if (list.Contains(7))
		{
			result = 7;
		}
		else if (list.Contains(0))
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x04004A51 RID: 19025
	[SerializeField]
	private GameObject initNullObj;

	// Token: 0x04004A52 RID: 19026
	public List<UnityEx.ValueTuple<GameObject, HPoint>> lstMarker = new List<UnityEx.ValueTuple<GameObject, HPoint>>();

	// Token: 0x04004A53 RID: 19027
	private HScene hScene;

	// Token: 0x04004A54 RID: 19028
	private HSceneManager hSceneManager;

	// Token: 0x04004A55 RID: 19029
	private HSceneSprite hSceneSprite;

	// Token: 0x04004A56 RID: 19030
	private Camera Cam;

	// Token: 0x04004A57 RID: 19031
	private UnityEx.ValueTuple<Vector3, Quaternion, int> _InitNull;

	// Token: 0x04004A58 RID: 19032
	public int HEnterCategory = -1;

	// Token: 0x04004A59 RID: 19033
	private Dictionary<int, HPointList> hPointLists = new Dictionary<int, HPointList>();

	// Token: 0x04004A5A RID: 19034
	private HPoint[] housingHpoints;

	// Token: 0x04004A5B RID: 19035
	public bool IsMarker;

	// Token: 0x04004A5C RID: 19036
	private HSceneFlagCtrl ctrlFlag;

	// Token: 0x04004A5D RID: 19037
	public int playerSex;

	// Token: 0x04004A5E RID: 19038
	public bool ExistSecondfemale;

	// Token: 0x04004A5F RID: 19039
	private RaycastHit[] HitInfo = new RaycastHit[50];

	// Token: 0x04004A60 RID: 19040
	private RaycastHit[] HitInfo2 = new RaycastHit[100];

	// Token: 0x04004A61 RID: 19041
	public List<HScene.AnimationListInfo>[] lstAnimInfo = new List<HScene.AnimationListInfo>[6];

	// Token: 0x04004A62 RID: 19042
	private List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> startList;

	// Token: 0x04004A63 RID: 19043
	private List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> startListM;

	// Token: 0x04004A64 RID: 19044
	public bool InitUsePoint;

	// Token: 0x04004A65 RID: 19045
	[SerializeField]
	private int gpID = -1;

	// Token: 0x04004A66 RID: 19046
	private BasePoint[] BasePoints;

	// Token: 0x04004A67 RID: 19047
	private ShuffleRand rand = new ShuffleRand(-1);

	// Token: 0x04004A68 RID: 19048
	private List<ForcedHideObject> CheckHitObjs = new List<ForcedHideObject>();

	// Token: 0x04004A69 RID: 19049
	private int[] LesPlaceID = new int[]
	{
		0,
		1,
		2,
		3,
		4,
		5,
		6,
		13
	};

	// Token: 0x04004A6A RID: 19050
	private int[] multiFemalePlaceID = new int[1];

	// Token: 0x04004A6B RID: 19051
	private int dildoPlaceID = 21;

	// Token: 0x04004A6C RID: 19052
	private int NotMerchantPlaceID = 5;

	// Token: 0x04004A6D RID: 19053
	private int kadOnaID = 20;

	// Token: 0x04004A6E RID: 19054
	private Dictionary<int, Dictionary<int, HPointCtrl.AreaGroupDefine>> Areagp = new Dictionary<int, Dictionary<int, HPointCtrl.AreaGroupDefine>>
	{
		{
			0,
			new Dictionary<int, HPointCtrl.AreaGroupDefine>
			{
				{
					0,
					new HPointCtrl.AreaGroupDefine
					{
						IDs = new List<int>
						{
							0,
							11,
							14
						}
					}
				},
				{
					1,
					new HPointCtrl.AreaGroupDefine
					{
						IDs = new List<int>
						{
							4,
							12,
							15,
							16
						}
					}
				},
				{
					2,
					new HPointCtrl.AreaGroupDefine
					{
						IDs = new List<int>
						{
							6,
							13,
							17
						}
					}
				}
			}
		},
		{
			1,
			new Dictionary<int, HPointCtrl.AreaGroupDefine>
			{
				{
					3,
					new HPointCtrl.AreaGroupDefine
					{
						IDs = new List<int>
						{
							0,
							1
						}
					}
				}
			}
		}
	};

	// Token: 0x04004A6F RID: 19055
	private CrossFade fade;

	// Token: 0x04004A70 RID: 19056
	private float OffsetDownHeight = 5f;

	// Token: 0x04004A71 RID: 19057
	private float OffsetUpHeight = 20f;

	// Token: 0x02000AC6 RID: 2758
	public class RayDistanceCompare : IComparer<RaycastHit>
	{
		// Token: 0x060050DB RID: 20699 RVA: 0x001FD114 File Offset: 0x001FB514
		public int Compare(RaycastHit x, RaycastHit y)
		{
			if (x.distance < y.distance)
			{
				return -1;
			}
			if (x.distance > y.distance)
			{
				return 1;
			}
			return 0;
		}
	}

	// Token: 0x02000AC7 RID: 2759
	[Serializable]
	public struct MinHpoint
	{
		// Token: 0x04004A72 RID: 19058
		public int AreaID;

		// Token: 0x04004A73 RID: 19059
		public int PointID;

		// Token: 0x04004A74 RID: 19060
		public int yobaiPlace;
	}

	// Token: 0x02000AC8 RID: 2760
	[Serializable]
	private class AreaGroupDefine
	{
		// Token: 0x04004A75 RID: 19061
		public List<int> IDs = new List<int>();
	}
}
