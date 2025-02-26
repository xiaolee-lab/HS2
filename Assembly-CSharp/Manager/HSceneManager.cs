using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIChara;
using AIProject;
using AIProject.Animal;
using AIProject.Definitions;
using AIProject.SaveData;
using Correct;
using Housing;
using UniRx;
using UnityEngine;
using UnityEx;

namespace Manager
{
	// Token: 0x02000B26 RID: 2854
	public class HSceneManager : Singleton<HSceneManager>
	{
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x060053B2 RID: 21426 RVA: 0x0024BD7B File Offset: 0x0024A17B
		// (set) Token: 0x060053B1 RID: 21425 RVA: 0x0024BD64 File Offset: 0x0024A164
		public static bool SleepStart
		{
			get
			{
				return Singleton<HSceneManager>.IsInstance() && Singleton<HSceneManager>.Instance.bSleepStart;
			}
			set
			{
				if (Singleton<HSceneManager>.IsInstance())
				{
					Singleton<HSceneManager>.Instance.bSleepStart = value;
				}
			}
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x0024BD93 File Offset: 0x0024A193
		public void SetEventInfo(HSceneManager.HEvent _event)
		{
			this.EventKind = _event;
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x060053B4 RID: 21428 RVA: 0x0024BD9C File Offset: 0x0024A19C
		public static bool isHScene
		{
			[CompilerGenerated]
			get
			{
				return Singleton<HSceneManager>.IsInstance() && Singleton<HSceneManager>.Instance._isH;
			}
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x0024BDB8 File Offset: 0x0024A1B8
		public void HsceneEnter(Actor actor, int isMerchantLimitType = -1, AgentActor agent2 = null, HSceneManager.HEvent _numEvent = HSceneManager.HEvent.Normal)
		{
			this._isH = true;
			this.enterPoint = null;
			this.IsHousingHEnter = false;
			MapUIContainer.SetActiveCommandList(false);
			this.choiceDisposable = MapUIContainer.CommandList.OnCompletedStopAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			});
			Singleton<MapUIContainer>.Instance.MinimapUI.MiniMap.SetActive(false);
			Singleton<MapUIContainer>.Instance.MinimapUI.MiniMapIcon.SetActive(false);
			Singleton<Map>.Instance.SetActiveMapEffect(false);
			if (this.HSceneSet == null)
			{
				this.HSceneSet = Singleton<Resources>.Instance.HSceneTable.HSceneSet;
			}
			if (this.HSceneUISet == null)
			{
				this.HSceneUISet = Singleton<Resources>.Instance.HSceneTable.HSceneUISet;
			}
			this.Player = Singleton<Map>.Instance.Player;
			this.Player.enabled = false;
			this.handsIK = this.Player.HandsHolder.enabled;
			if (this.handsIK)
			{
				this.Player.HandsHolder.enabled = false;
			}
			this.Player.SetActiveOnEquipedItem(false);
			this.Player.ChaControl.setAllLayerWeight(0f);
			this.bMerchant = (actor.GetComponent<MerchantActor>() != null);
			this.MerchantLimit = isMerchantLimitType;
			this.FemaleLumpActive[0] = false;
			this.FemaleLumpActive[1] = false;
			if (!this.bMerchant)
			{
				this.Agent[0] = actor.GetComponent<AgentActor>();
				this.Agent[0].BehaviorResources.ChangeMode(Desire.ActionType.Idle);
				this.Agent[0].DisableBehavior();
				this.Agent[0].AnimationAgent.DisableItems();
				this.Agent[0].Controller.enabled = false;
				this.Agent[0].AnimationAgent.enabled = false;
				this.PersonalPhase[0] = this.Agent[0].ChaControl.fileGameInfo.phase;
				this.Personality[0] = this.Agent[0].ChaControl.chaFile.parameter.personality;
				if (this.Agent[0].ChaControl.objExtraAccessory[3] != null)
				{
					this.FemaleLumpActive[0] = this.Agent[0].ChaControl.objExtraAccessory[3].activeSelf;
					this.Agent[0].ChaControl.ShowExtraAccessory(ChaControlDefine.ExtraAccessoryParts.Waist, false);
				}
				this.Agent[0].SetActiveOnEquipedItem(false);
				this.Agent[0].ChaControl.setAllLayerWeight(0f);
				this.HSkil = this.Agent[0].ChaControl.fileGameInfo.hSkill;
			}
			else
			{
				this.merchantActor = actor.GetComponent<MerchantActor>();
				this.merchantActor.DisableBehavior();
				this.merchantActor.Controller.enabled = false;
				this.merchantActor.AnimationMerchant.enabled = false;
				this.PersonalPhase[0] = 3;
			}
			if (agent2 != null)
			{
				this.Agent[1] = agent2;
				this.Agent[1].BehaviorResources.ChangeMode(Desire.ActionType.Idle);
				this.Agent[1].DisableBehavior();
				this.Agent[1].AnimationAgent.DisableItems();
				this.Agent[1].Controller.enabled = false;
				this.Agent[1].AnimationAgent.enabled = false;
				this.PersonalPhase[1] = this.Agent[1].ChaControl.fileGameInfo.phase;
				this.Personality[1] = this.Agent[1].ChaControl.chaFile.parameter.personality;
				if (this.Agent[1].ChaControl.objExtraAccessory[3] != null)
				{
					this.FemaleLumpActive[1] = this.Agent[1].ChaControl.objExtraAccessory[3].activeSelf;
					this.Agent[1].ChaControl.ShowExtraAccessory(ChaControlDefine.ExtraAccessoryParts.Waist, false);
				}
				this.Agent[1].SetActiveOnEquipedItem(false);
				this.Agent[1].ChaControl.setAllLayerWeight(0f);
			}
			this.isCtrl = false;
			this.endStatus = 0;
			AnimalBase.CreateDisplay = false;
			AnimalManager instance = Singleton<AnimalManager>.Instance;
			for (int i = 0; i < instance.Animals.Count; i++)
			{
				int index = i;
				instance.Animals[index].BodyEnabled = false;
				instance.Animals[index].enabled = false;
			}
			instance.ClearAnimalPointBehavior();
			if (!this.bMerchant)
			{
				this.Temperature = this.Agent[0].AgentData.StatsTable[0];
				this.Mood[0] = this.Agent[0].AgentData.StatsTable[1];
				this.Mood[1] = ((!this.Agent[1]) ? 0f : this.Agent[1].AgentData.StatsTable[1]);
				if (this.ReserveToilet != null)
				{
					this.Toilet = this.ReserveToilet.Value;
					this.ReserveToilet = null;
				}
				else
				{
					int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
					this.Toilet = this.Agent[0].AgentData.DesireTable[desireKey];
				}
				int desireKey2 = Desire.GetDesireKey(Desire.Type.Bath);
				this.Bath = this.Agent[0].AgentData.DesireTable[desireKey2];
				this.Reliability = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Reliability);
				this.Instinct = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Instinct);
				this.Dirty = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Dirty);
				this.RiskManagement = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Wariness);
				this.Darkness = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Darkness);
				this.Sociability = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Sociability);
				this.Pheromone = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Pheromone);
				this.isHAddTaii = new bool[]
				{
					this.Agent[0].ChaControl.fileGameInfo.isHAddTaii0,
					this.Agent[0].ChaControl.fileGameInfo.isHAddTaii1
				};
			}
			this.Player.Controller.ChangeState("Sex");
			this.ReturnActionTypes.Clear();
			this.EventKind = _numEvent;
			Singleton<Voice>.Instance.StopAll(true);
			if (!this.bMerchant)
			{
				base.StartCoroutine(this.HsceneInit(this.Agent));
			}
			else
			{
				base.StartCoroutine(this.HsceneInit(this.merchantActor, (!(agent2 != null)) ? null : this.Agent[1]));
			}
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0024C4AC File Offset: 0x0024A8AC
		public void HousingHEnter(Actor actor, HPoint hpoint)
		{
			this._isH = true;
			this.IsHousingHEnter = true;
			MapUIContainer.SetActiveCommandList(false);
			this.choiceDisposable = MapUIContainer.CommandList.OnCompletedStopAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			});
			Singleton<MapUIContainer>.Instance.MinimapUI.MiniMap.SetActive(false);
			Singleton<MapUIContainer>.Instance.MinimapUI.MiniMapIcon.SetActive(false);
			Singleton<Map>.Instance.SetActiveMapEffect(false);
			if (this.HSceneSet == null)
			{
				this.HSceneSet = Singleton<Resources>.Instance.HSceneTable.HSceneSet;
			}
			if (this.HSceneUISet == null)
			{
				this.HSceneUISet = Singleton<Resources>.Instance.HSceneTable.HSceneUISet;
			}
			this.Player = Singleton<Map>.Instance.Player;
			this.Player.enabled = false;
			this.handsIK = this.Player.HandsHolder.enabled;
			if (this.handsIK)
			{
				this.Player.HandsHolder.enabled = false;
			}
			this.Player.SetActiveOnEquipedItem(false);
			this.Player.ChaControl.setAllLayerWeight(0f);
			this.Agent[0] = actor.GetComponent<AgentActor>();
			this.Agent[0].BehaviorResources.ChangeMode(Desire.ActionType.Idle);
			this.Agent[0].DisableBehavior();
			this.Agent[0].AnimationAgent.DisableItems();
			this.Agent[0].Controller.enabled = false;
			this.Agent[0].AnimationAgent.enabled = false;
			this.PersonalPhase[0] = this.Agent[0].ChaControl.fileGameInfo.phase;
			this.Personality[0] = this.Agent[0].ChaControl.chaFile.parameter.personality;
			this.Agent[0].SetActiveOnEquipedItem(false);
			this.Agent[0].ChaControl.setAllLayerWeight(0f);
			this.HSkil = this.Agent[0].ChaControl.fileGameInfo.hSkill;
			this.isCtrl = false;
			this.endStatus = 0;
			AnimalBase.CreateDisplay = false;
			AnimalManager instance = Singleton<AnimalManager>.Instance;
			for (int i = 0; i < instance.Animals.Count; i++)
			{
				int index = i;
				instance.Animals[index].BodyEnabled = false;
				instance.Animals[index].enabled = false;
			}
			instance.ClearAnimalPointBehavior();
			if (!this.bMerchant)
			{
				this.Temperature = this.Agent[0].AgentData.StatsTable[0];
				this.Mood[0] = this.Agent[0].AgentData.StatsTable[1];
				this.Mood[1] = ((!this.Agent[1]) ? 0f : this.Agent[1].AgentData.StatsTable[1]);
				if (this.Mood[0] < this.Agent[0].ChaControl.fileGameInfo.moodBound.lower && this.PersonalPhase[0] >= 2)
				{
					this.isForce = true;
				}
				int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
				this.Toilet = this.Agent[0].AgentData.DesireTable[desireKey];
				int desireKey2 = Desire.GetDesireKey(Desire.Type.Bath);
				this.Bath = this.Agent[0].AgentData.DesireTable[desireKey2];
				this.Reliability = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Reliability);
				this.Instinct = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Instinct);
				this.Dirty = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Dirty);
				this.RiskManagement = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Wariness);
				this.Darkness = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Darkness);
				this.Sociability = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Sociability);
				this.Pheromone = this.Agent[0].GetFlavorSkill(FlavorSkill.Type.Pheromone);
				this.isHAddTaii = new bool[]
				{
					this.Agent[0].ChaControl.fileGameInfo.isHAddTaii0,
					this.Agent[0].ChaControl.fileGameInfo.isHAddTaii1
				};
			}
			this.Player.Controller.ChangeState("Sex");
			this.ReturnActionTypes.Clear();
			this.EventKind = HSceneManager.HEvent.Normal;
			if (this.EventKind == HSceneManager.HEvent.Back)
			{
				Singleton<HSceneFlagCtrl>.Instance.AddParam(31, 1);
			}
			Singleton<Voice>.Instance.StopAll(true);
			this.height = hpoint._nPlace[0].Item1;
			this.enterPoint = hpoint;
			base.StartCoroutine(this.HsceneInit(this.Agent));
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x0024C9A0 File Offset: 0x0024ADA0
		private IEnumerator HsceneInit(AgentActor[] agent)
		{
			if (this.CameraMesh == null)
			{
				string name = Singleton<Resources>.Instance.Map.CameraColliderList[Singleton<Map>.Instance.MapID].asset + "(Clone)";
				this.CameraMesh = GameObject.Find(name);
				yield return null;
			}
			if (this.CameraMesh != null)
			{
				this.CameraMesh.SetActive(false);
			}
			this.HSceneSet.SetActive(true);
			this.HSceneUISet.SetActive(true);
			foreach (AgentActor agentActor in Singleton<Map>.Instance.AgentTable.Values)
			{
				if (!(agentActor == null))
				{
					if (!(agentActor == agent[0]) && (!(agent[1] != null) || !(agentActor == agent[1])))
					{
						agentActor.DisableEntity();
						this.ReturnActionTypes.Add(agentActor, agentActor.BehaviorResources.Mode);
					}
				}
			}
			MerchantActor merchant = Singleton<Map>.Instance.Merchant;
			if (merchant.CurrentMode != Merchant.ActionType.Absent)
			{
				merchant.ChaControl.visibleAll = false;
			}
			merchant.DisableBehavior();
			merchant.Controller.enabled = false;
			merchant.AnimationMerchant.enabled = false;
			this.females[0] = agent[0];
			this.bFutanari = Singleton<Map>.Instance.Player.ChaControl.fileParam.futanari;
			if (Singleton<Map>.Instance.Player.ChaControl.sex == 0 || (Singleton<Map>.Instance.Player.ChaControl.sex == 1 && this.bFutanari))
			{
				this.male = this.Player;
				FrameCorrect.AddFrameCorrect(this.male.ChaControl.animBody.gameObject);
				IKCorrect.AddIKCorrect(this.male.ChaControl.animBody.gameObject);
				if (agent[1] != null)
				{
					this.females[1] = agent[1];
					FrameCorrect.AddFrameCorrect(this.females[1].ChaControl.animBody.gameObject);
					IKCorrect.AddIKCorrect(this.females[1].ChaControl.animBody.gameObject);
				}
			}
			else
			{
				this.females[1] = this.Player;
				FrameCorrect.AddFrameCorrect(this.females[1].ChaControl.animBody.gameObject);
				IKCorrect.AddIKCorrect(this.females[1].ChaControl.animBody.gameObject);
			}
			yield return null;
			HScene hScene = Singleton<HSceneFlagCtrl>.Instance.GetComponent<HScene>();
			IEnumerator init = hScene.InitCoroutine();
			yield return init;
			Singleton<HPointCtrl>.Instance.CameraSet();
			yield break;
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x0024C9C4 File Offset: 0x0024ADC4
		private IEnumerator HsceneInit(MerchantActor Merchant, AgentActor agent = null)
		{
			if (this.CameraMesh == null)
			{
				string name = Singleton<Resources>.Instance.Map.CameraColliderList[Singleton<Map>.Instance.MapID].asset + "(Clone)";
				this.CameraMesh = GameObject.Find(name);
				yield return null;
			}
			if (this.CameraMesh != null)
			{
				this.CameraMesh.SetActive(false);
			}
			this.HSceneSet.SetActive(true);
			this.HSceneUISet.SetActive(true);
			foreach (AgentActor agentactor in Singleton<Map>.Instance.AgentTable.Values)
			{
				if (!(agentactor == null))
				{
					if (!(agent != null) || !(agentactor == agent))
					{
						agentactor.DisableEntity();
						this.ReturnActionTypes.Add(agentactor, agentactor.BehaviorResources.Mode);
						yield return null;
					}
				}
			}
			this.females[0] = this.merchantActor;
			this.bFutanari = Singleton<Map>.Instance.Player.ChaControl.fileParam.futanari;
			if (Singleton<Map>.Instance.Player.ChaControl.sex == 0 || (Singleton<Map>.Instance.Player.ChaControl.sex == 1 && this.bFutanari))
			{
				this.male = this.Player;
				FrameCorrect.AddFrameCorrect(this.male.ChaControl.animBody.gameObject);
				yield return null;
				IKCorrect.AddIKCorrect(this.male.ChaControl.animBody.gameObject);
				if (agent != null)
				{
					this.females[1] = agent;
					FrameCorrect.AddFrameCorrect(this.females[1].ChaControl.animBody.gameObject);
					IKCorrect.AddIKCorrect(this.females[1].ChaControl.animBody.gameObject);
				}
			}
			else
			{
				this.females[1] = this.Player;
				FrameCorrect.AddFrameCorrect(this.females[1].ChaControl.animBody.gameObject);
				IKCorrect.AddIKCorrect(this.females[1].ChaControl.animBody.gameObject);
			}
			yield return null;
			GC.Collect();
			Resources.UnloadUnusedAssets();
			yield return null;
			HScene hScene = Singleton<HSceneFlagCtrl>.Instance.GetComponent<HScene>();
			IEnumerator init = hScene.InitCoroutine();
			yield return init;
			Singleton<HPointCtrl>.Instance.CameraSet();
			yield break;
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x0024C9E8 File Offset: 0x0024ADE8
		public bool HMeshCheck(int mode, List<HScene.DeskChairInfo> deskChairInfos)
		{
			this.hits = new RaycastHit[10];
			this.start = Vector3.zero;
			if (this.bMerchant)
			{
				this.start = ((mode != 1) ? this.merchantActor.Controller.transform.position : this.merchantActor.ChaControl.transform.position);
			}
			else
			{
				this.start = ((mode != 1) ? this.Agent[0].Controller.transform.position : this.Agent[0].ChaControl.transform.position);
			}
			this.start.y = ((!this.bMerchant) ? this.Agent[0].ChaControl.objHeadBone.transform.position.y : this.merchantActor.ChaControl.objHeadBone.transform.position.y);
			int layerMask = 131072;
			int num = Physics.RaycastNonAlloc(this.start, Vector3.down, this.hits, this.SerchHmeshRange, layerMask);
			if (num == 0)
			{
				return false;
			}
			int mapID = Singleton<Map>.Instance.MapID;
			int areaID = this.Player.AreaID;
			HSceneFlagCtrl.HousingID[] housingAreaID = Singleton<HSceneFlagCtrl>.Instance.HousingAreaID;
			BasePoint[] basePoints = Singleton<Map>.Instance.PointAgent.BasePoints;
			int num2 = -1;
			int num3 = -1;
			for (int i = 0; i < housingAreaID.Length; i++)
			{
				if (mapID == housingAreaID[i].mapID)
				{
					for (int j = 0; j < housingAreaID[i].areaID.Length; j++)
					{
						if (housingAreaID[i].areaID[j] == areaID)
						{
							num2 = areaID;
							break;
						}
					}
				}
			}
			for (int k = 0; k < basePoints.Length; k++)
			{
				if (basePoints[k].AreaIDInHousing == num2)
				{
					num3 = basePoints[k].ID;
					break;
				}
			}
			HPoint[] array = null;
			if (num3 != -1)
			{
				array = Singleton<Housing>.Instance.GetHPoint(num3);
			}
			bool flag = false;
			for (int l = 0; l < num; l++)
			{
				flag = HSceneManager.HmeshNomalKindTag.ContainsKey(this.hits[l].collider.gameObject.tag);
				if (flag)
				{
					break;
				}
			}
			RaycastHit item = this.hits[0];
			if (num > 1)
			{
				float num4 = (item.point - this.start).sqrMagnitude;
				int m = 1;
				while (m < num)
				{
					if (!this.bMerchant)
					{
						if (this.EventKind == HSceneManager.HEvent.FromFemale)
						{
							if ((Singleton<Map>.Instance.Player.ChaControl.sex == 0 || (Singleton<Map>.Instance.Player.ChaControl.sex == 1 && this.bFutanari)) && this.hits[m].collider.gameObject.tag != "standfloor" && this.hits[m].collider.gameObject.tag != "bed")
							{
								goto IL_611;
							}
						}
						else if (this.EventKind == HSceneManager.HEvent.Yobai)
						{
							if (this.hits[m].collider.gameObject.tag != "bed" && this.hits[m].collider.gameObject.tag != "sofabed")
							{
								goto IL_611;
							}
						}
						else if (this.EventKind == HSceneManager.HEvent.Normal)
						{
							if (flag && !HSceneManager.HmeshNomalKindTag.ContainsKey(this.hits[m].collider.gameObject.tag))
							{
								goto IL_611;
							}
							if (this.EventKind != HSceneManager.HEvent.GyakuYobai && HSceneManager.SleepStart && this.hits[m].collider.gameObject.tag != "standfloor" && this.hits[m].collider.gameObject.tag != "bed")
							{
								goto IL_611;
							}
						}
						if (Singleton<Map>.Instance.Player.ChaControl.sex != 1 || this.bFutanari || HSceneManager.HmeshLesTag.ContainsKey(this.hits[m].collider.gameObject.tag))
						{
							goto IL_594;
						}
					}
					else if (!("sofa" == this.hits[m].collider.gameObject.tag))
					{
						if (HSceneManager.HmeshTag.ContainsKey(this.hits[m].collider.gameObject.tag))
						{
							if (HSceneManager.HmeshTag[this.hits[m].collider.gameObject.tag] <= 4)
							{
								goto IL_594;
							}
						}
					}
					IL_611:
					m++;
					continue;
					IL_594:
					float sqrMagnitude = (this.hits[m].point - this.start).sqrMagnitude;
					if (num4 < sqrMagnitude)
					{
						goto IL_611;
					}
					if (this.hits[m].collider.gameObject.tag == "Untagged")
					{
						goto IL_611;
					}
					item = this.hits[m];
					num4 = sqrMagnitude;
					goto IL_611;
				}
			}
			else if (item.collider.gameObject.tag == "Untagged")
			{
				return false;
			}
			if (array != null && array.Length > 0)
			{
				float num5 = -1f;
				for (int n = 0; n < array.Length; n++)
				{
					List<Collider> housingHcol = this.GetHousingHcol(array[n]);
					for (int num6 = 0; num6 < num; num6++)
					{
						if (housingHcol.Contains(this.hits[num6].collider))
						{
							if (!this.bMerchant)
							{
								if (this.EventKind == HSceneManager.HEvent.FromFemale)
								{
									if ((Singleton<Map>.Instance.Player.ChaControl.sex == 0 || (Singleton<Map>.Instance.Player.ChaControl.sex == 1 && this.bFutanari)) && this.hits[num6].collider.gameObject.tag != "standfloor" && this.hits[num6].collider.gameObject.tag != "bed")
									{
										goto IL_A13;
									}
								}
								else if (this.EventKind == HSceneManager.HEvent.Yobai)
								{
									if (this.hits[num6].collider.gameObject.tag != "standfloor" && this.hits[num6].collider.gameObject.tag != "bed" && this.hits[num6].collider.gameObject.tag != "sofabed")
									{
										goto IL_A13;
									}
								}
								else if (this.EventKind == HSceneManager.HEvent.Normal)
								{
									if (flag && !HSceneManager.HmeshNomalKindTag.ContainsKey(this.hits[num6].collider.gameObject.tag))
									{
										goto IL_A13;
									}
									if (this.EventKind != HSceneManager.HEvent.GyakuYobai && HSceneManager.SleepStart && this.hits[num6].collider.gameObject.tag != "standfloor" && this.hits[num6].collider.gameObject.tag != "bed")
									{
										goto IL_A13;
									}
								}
								if (Singleton<Map>.Instance.Player.ChaControl.sex == 1 && !this.bFutanari && !HSceneManager.HmeshLesTag.ContainsKey(this.hits[num6].collider.gameObject.tag))
								{
									goto IL_A13;
								}
							}
							else
							{
								if ("sofa" == this.hits[num6].collider.gameObject.tag)
								{
									goto IL_A13;
								}
								if (!HSceneManager.HmeshTag.ContainsKey(this.hits[num6].collider.gameObject.tag))
								{
									goto IL_A13;
								}
								if (HSceneManager.HmeshTag[this.hits[num6].collider.gameObject.tag] > 4)
								{
									goto IL_A13;
								}
							}
							float sqrMagnitude2 = (this.hits[num6].point - this.start).sqrMagnitude;
							if (num5 < 0f || num5 >= sqrMagnitude2)
							{
								if (!(this.hits[num6].collider.gameObject.tag == "Untagged"))
								{
									item = this.hits[num6];
									num5 = sqrMagnitude2;
								}
							}
						}
						IL_A13:;
					}
				}
			}
			if (!this.bMerchant)
			{
				if (this.EventKind == HSceneManager.HEvent.FromFemale)
				{
					if (item.collider.gameObject.tag != "standfloor" && item.collider.gameObject.tag != "bed")
					{
						return false;
					}
				}
				else if (this.EventKind == HSceneManager.HEvent.Yobai && item.collider.gameObject.tag != "standfloor" && item.collider.gameObject.tag != "bed" && item.collider.gameObject.tag != "sofabed")
				{
					return false;
				}
				if (Singleton<Map>.Instance.Player.ChaControl.sex == 1 && !this.bFutanari && !HSceneManager.HmeshLesTag.ContainsKey(item.collider.gameObject.tag))
				{
					return false;
				}
			}
			else
			{
				if ("sofa" == item.collider.gameObject.tag)
				{
					return false;
				}
				if (!HSceneManager.HmeshTag.ContainsKey(item.collider.gameObject.tag))
				{
					return false;
				}
				if (HSceneManager.HmeshTag[item.collider.gameObject.tag] > 4)
				{
					return false;
				}
			}
			if (item.collider.gameObject.tag != "table" || this.bMerchant)
			{
				this.hitHmesh.Item1 = item;
				this.hitHmesh.Item2 = item.collider.gameObject;
			}
			else
			{
				bool flag2 = false;
				RaycastHit item2 = default(RaycastHit);
				GameObject gameObject = null;
				for (int num7 = 0; num7 < num; num7++)
				{
					if (!(this.hits[num7].collider.gameObject.tag != "chair"))
					{
						flag2 = true;
						item2 = this.hits[num7];
						gameObject = this.hits[num7].collider.gameObject;
						break;
					}
				}
				if (flag2)
				{
					this.onDeskChair = false;
					for (int num8 = 0; num8 < deskChairInfos.Count; num8++)
					{
						int index = num8;
						if (deskChairInfos[index].eventID == this.Agent[0].ActionID && deskChairInfos[index].poseID == this.Agent[0].PoseID)
						{
							this.onDeskChair = true;
							break;
						}
					}
					if (!this.onDeskChair)
					{
						this.hitHmesh.Item1 = item2;
						this.hitHmesh.Item2 = gameObject;
						this.hitHmesh.Item3 = ((!HSceneManager.HmeshTag.ContainsKey(gameObject.tag)) ? -2 : HSceneManager.HmeshTag[gameObject.tag]);
						return true;
					}
					this.hitHmesh.Item1 = item;
					this.hitHmesh.Item2 = item.collider.gameObject;
				}
			}
			switch (this.EventKind)
			{
			case HSceneManager.HEvent.Yobai:
				if (HSceneManager.HmeshTag.ContainsKey(item.collider.gameObject.tag))
				{
					if (HSceneManager.HmeshTag[item.collider.gameObject.tag] == -1)
					{
						this.hitHmesh.Item3 = 0;
					}
					else
					{
						this.hitHmesh.Item3 = HSceneManager.HmeshTag[item.collider.gameObject.tag];
					}
				}
				else
				{
					this.hitHmesh.Item3 = 0;
				}
				break;
			case HSceneManager.HEvent.Bath:
				this.hitHmesh.Item3 = 11;
				break;
			case HSceneManager.HEvent.Toilet1:
				this.hitHmesh.Item3 = 13;
				break;
			case HSceneManager.HEvent.Toilet2:
			case HSceneManager.HEvent.ShagmiBare:
				this.hitHmesh.Item3 = 14;
				break;
			case HSceneManager.HEvent.Back:
			case HSceneManager.HEvent.GyakuYobai:
			case HSceneManager.HEvent.FromFemale:
				this.hitHmesh.Item3 = 0;
				break;
			case HSceneManager.HEvent.Kitchen:
				this.hitHmesh.Item3 = 9;
				break;
			case HSceneManager.HEvent.Tachi:
			case HSceneManager.HEvent.MapBath:
				this.hitHmesh.Item3 = 1;
				break;
			case HSceneManager.HEvent.Stairs:
			case HSceneManager.HEvent.StairsBare:
				this.hitHmesh.Item3 = 10;
				break;
			case HSceneManager.HEvent.KabeanaBack:
			case HSceneManager.HEvent.KabeanaFront:
				this.hitHmesh.Item3 = 15;
				break;
			case HSceneManager.HEvent.Neonani:
				this.hitHmesh.Item3 = 0;
				break;
			case HSceneManager.HEvent.TsukueBare:
				this.hitHmesh.Item3 = 4;
				break;
			default:
				if (this.females[1] == null || this.females[1].GetComponent<PlayerActor>() != null)
				{
					this.hitHmesh.Item3 = ((!HSceneManager.HmeshTag.ContainsKey(item.collider.gameObject.tag)) ? -2 : HSceneManager.HmeshTag[item.collider.gameObject.tag]);
				}
				else
				{
					this.hitHmesh.Item3 = 0;
				}
				break;
			}
			if (HSceneManager.SleepStart)
			{
				this.hitHmesh.Item3 = 0;
			}
			return true;
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x0024D9C8 File Offset: 0x0024BDC8
		public void SetFlaverSkillParamator(int kind, int val)
		{
			switch (kind)
			{
			case 0:
				this.Reliability += val;
				this.Agent[0].SetFlavorSkill(FlavorSkill.Type.Reliability, this.Reliability);
				break;
			case 1:
				this.Instinct += val;
				this.Agent[0].SetFlavorSkill(FlavorSkill.Type.Instinct, this.Instinct);
				break;
			case 2:
				this.Dirty += val;
				this.Agent[0].SetFlavorSkill(FlavorSkill.Type.Dirty, this.Dirty);
				break;
			case 3:
				this.RiskManagement += val;
				this.Agent[0].SetFlavorSkill(FlavorSkill.Type.Wariness, this.RiskManagement);
				break;
			case 4:
				this.Darkness += val;
				this.Agent[0].SetFlavorSkill(FlavorSkill.Type.Darkness, this.Darkness);
				break;
			case 5:
				this.Sociability += val;
				this.Agent[0].SetFlavorSkill(FlavorSkill.Type.Sociability, this.Sociability);
				break;
			case 6:
				this.Pheromone += val;
				this.Agent[0].SetFlavorSkill(FlavorSkill.Type.Pheromone, this.Pheromone);
				break;
			}
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x0024DB10 File Offset: 0x0024BF10
		public void SetParamator(int kind, int val)
		{
			if (kind < 10)
			{
				this.Agent[0].AddStatus(kind, (float)val);
			}
			else if (kind < 100)
			{
				this.Agent[0].AddFlavorSkill(kind - 10, val);
			}
			else
			{
				this.Agent[0].SetDesire(kind - 100, (float)val);
			}
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x0024DB70 File Offset: 0x0024BF70
		public int GetFlaverSkillLevel(int kind)
		{
			switch (kind)
			{
			case 0:
				return this.Reliability;
			case 1:
				return this.Instinct;
			case 2:
				return this.Dirty;
			case 3:
				return this.RiskManagement;
			case 4:
				return this.Darkness;
			case 5:
				return this.Sociability;
			default:
				return -1;
			}
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x0024DBCC File Offset: 0x0024BFCC
		public bool CheckHadItem(int _mode, int _id)
		{
			List<Dictionary<int, List<HItemCtrl.ListItem>>>[] lstHItemObjInfo = Singleton<Resources>.Instance.HSceneTable.lstHItemObjInfo;
			List<StuffItem> itemList = Singleton<Map>.Instance.Player.PlayerData.ItemList;
			List<StuffItem> itemListInStorage = Singleton<Game>.Instance.Environment.ItemListInStorage;
			this.tmpHadItems.Clear();
			for (int i = 0; i < lstHItemObjInfo[_mode].Count; i++)
			{
				if (lstHItemObjInfo[_mode][i].ContainsKey(_id))
				{
					for (int j = 0; j < lstHItemObjInfo[_mode][i][_id].Count; j++)
					{
						int num = j;
						if (lstHItemObjInfo[_mode][i][_id][num].itemkind != 1)
						{
							this.tmpHadItems.Add(num, true);
						}
						else
						{
							int itemID = lstHItemObjInfo[_mode][i][_id][num].itemID;
							foreach (StuffItem stuffItem in itemList)
							{
								if (stuffItem.CategoryID == 14 && stuffItem.ID == itemID)
								{
									this.tmpHadItems.Add(num, true);
									break;
								}
							}
							if (!this.tmpHadItems.ContainsKey(num) || !this.tmpHadItems[num])
							{
								foreach (StuffItem stuffItem2 in itemListInStorage)
								{
									if (stuffItem2.CategoryID == 14 && stuffItem2.ID == itemID)
									{
										if (this.tmpHadItems.ContainsKey(num))
										{
											this.tmpHadItems[num] = true;
										}
										else
										{
											this.tmpHadItems.Add(num, true);
										}
									}
								}
								if (!this.tmpHadItems.ContainsKey(num))
								{
									this.tmpHadItems.Add(num, false);
								}
							}
						}
					}
				}
			}
			using (Dictionary<int, bool>.ValueCollection.Enumerator enumerator3 = this.tmpHadItems.Values.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					if (!enumerator3.Current)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x0024DE80 File Offset: 0x0024C280
		public void EndHScene()
		{
			this._isH = false;
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x0024DE8C File Offset: 0x0024C28C
		private List<Collider> GetHousingHcol(HPoint hPoint)
		{
			ItemComponent componentInParent = hPoint.GetComponentInParent<ItemComponent>();
			List<Collider> list = new List<Collider>();
			if (componentInParent != null)
			{
				Collider[] componentsInChildren = componentInParent.GetComponentsInChildren<Collider>();
				foreach (Collider collider in componentsInChildren)
				{
					if (collider.gameObject.layer == LayerMask.NameToLayer("HArea"))
					{
						list.Add(collider);
					}
				}
			}
			return list;
		}

		// Token: 0x04004E4E RID: 20046
		public PlayerActor Player;

		// Token: 0x04004E4F RID: 20047
		public AgentActor[] Agent = new AgentActor[2];

		// Token: 0x04004E50 RID: 20048
		public MerchantActor merchantActor;

		// Token: 0x04004E51 RID: 20049
		public Actor[] females = new Actor[2];

		// Token: 0x04004E52 RID: 20050
		public Actor male;

		// Token: 0x04004E53 RID: 20051
		public string[] pngFemales = new string[2];

		// Token: 0x04004E54 RID: 20052
		public string pngMale;

		// Token: 0x04004E55 RID: 20053
		public HSceneManager.HEvent EventKind;

		// Token: 0x04004E56 RID: 20054
		public HashSet<string> hashUseAssetBundle = new HashSet<string>();

		// Token: 0x04004E57 RID: 20055
		public bool bMerchant;

		// Token: 0x04004E58 RID: 20056
		public int MerchantLimit = -1;

		// Token: 0x04004E59 RID: 20057
		public float SerchHmeshRange = 50f;

		// Token: 0x04004E5A RID: 20058
		public GameObject HSceneSet;

		// Token: 0x04004E5B RID: 20059
		public GameObject HSceneUISet;

		// Token: 0x04004E5C RID: 20060
		public int numFemaleClothCustom;

		// Token: 0x04004E5D RID: 20061
		public Dictionary<int, int> HSkil = new Dictionary<int, int>
		{
			{
				0,
				0
			},
			{
				1,
				0
			},
			{
				2,
				0
			},
			{
				3,
				0
			},
			{
				4,
				0
			}
		};

		// Token: 0x04004E5E RID: 20062
		public int[] PersonalPhase = new int[]
		{
			-1,
			-1
		};

		// Token: 0x04004E5F RID: 20063
		public int[] Personality = new int[2];

		// Token: 0x04004E60 RID: 20064
		public int hMainKind;

		// Token: 0x04004E61 RID: 20065
		public int height = -1;

		// Token: 0x04004E62 RID: 20066
		public bool isForce;

		// Token: 0x04004E63 RID: 20067
		public float Temperature;

		// Token: 0x04004E64 RID: 20068
		public float[] Mood = new float[2];

		// Token: 0x04004E65 RID: 20069
		public float Toilet;

		// Token: 0x04004E66 RID: 20070
		public float? ReserveToilet;

		// Token: 0x04004E67 RID: 20071
		public float Bath;

		// Token: 0x04004E68 RID: 20072
		private int Reliability;

		// Token: 0x04004E69 RID: 20073
		private int Instinct;

		// Token: 0x04004E6A RID: 20074
		private int Dirty;

		// Token: 0x04004E6B RID: 20075
		private int RiskManagement;

		// Token: 0x04004E6C RID: 20076
		private int Darkness;

		// Token: 0x04004E6D RID: 20077
		private int Sociability;

		// Token: 0x04004E6E RID: 20078
		private int Pheromone;

		// Token: 0x04004E6F RID: 20079
		public bool handsIK;

		// Token: 0x04004E70 RID: 20080
		[DisabledGroup("ふたなりボディか")]
		public bool bFutanari;

		// Token: 0x04004E71 RID: 20081
		[DisabledGroup("ヒロインからHに誘ってきたパターン")]
		public int nInvitePtn = -1;

		// Token: 0x04004E72 RID: 20082
		private bool bSleepStart;

		// Token: 0x04004E73 RID: 20083
		public UnityEx.ValueTuple<RaycastHit, GameObject, int> hitHmesh = default(UnityEx.ValueTuple<RaycastHit, GameObject, int>);

		// Token: 0x04004E74 RID: 20084
		public RaycastHit[] hits;

		// Token: 0x04004E75 RID: 20085
		[DisabledGroup("cameramesh")]
		public GameObject CameraMesh;

		// Token: 0x04004E76 RID: 20086
		public static readonly Dictionary<string, int> HmeshTag = new Dictionary<string, int>
		{
			{
				"bath",
				11
			},
			{
				"stand",
				1
			},
			{
				"standfloor",
				-1
			},
			{
				"wall",
				2
			},
			{
				"bed",
				0
			},
			{
				"chair",
				3
			},
			{
				"table",
				4
			},
			{
				"stairs",
				10
			},
			{
				"sofa",
				6
			},
			{
				"sofabed",
				7
			},
			{
				"wasiki",
				14
			},
			{
				"counter",
				9
			},
			{
				"shower",
				12
			},
			{
				"pool",
				26
			}
		};

		// Token: 0x04004E77 RID: 20087
		public static readonly Dictionary<string, int> HmeshNomalKindTag = new Dictionary<string, int>
		{
			{
				"bath",
				11
			},
			{
				"wall",
				2
			},
			{
				"bed",
				0
			},
			{
				"chair",
				3
			},
			{
				"table",
				4
			},
			{
				"stairs",
				10
			},
			{
				"sofa",
				6
			},
			{
				"sofabed",
				7
			},
			{
				"wasiki",
				14
			},
			{
				"counter",
				9
			},
			{
				"shower",
				12
			},
			{
				"pool",
				26
			}
		};

		// Token: 0x04004E78 RID: 20088
		public static readonly Dictionary<string, int> HmeshLesTag = new Dictionary<string, int>
		{
			{
				"standfloor",
				-1
			},
			{
				"wall",
				2
			},
			{
				"bed",
				0
			},
			{
				"chair",
				3
			},
			{
				"table",
				4
			},
			{
				"stand",
				1
			},
			{
				"sofa",
				6
			}
		};

		// Token: 0x04004E79 RID: 20089
		[Label("キャラの位置にパーティクルを出すか")]
		public bool isParticle;

		// Token: 0x04004E7A RID: 20090
		private Dictionary<int, bool> tmpHadItems = new Dictionary<int, bool>();

		// Token: 0x04004E7B RID: 20091
		public bool[] isHAddTaii = new bool[2];

		// Token: 0x04004E7C RID: 20092
		[HideInInspector]
		public readonly string strAssetCameraList = "list/h/camera/";

		// Token: 0x04004E7D RID: 20093
		[HideInInspector]
		public readonly string strAssetAnimationInfoListFolder = "list/h/animationinfo/";

		// Token: 0x04004E7E RID: 20094
		[HideInInspector]
		public readonly string strAssetStartAnimationListFolder = "list/h/startanimation/";

		// Token: 0x04004E7F RID: 20095
		[HideInInspector]
		public readonly string strAssetStartWaitAnimListFolder = "list/h/startwaitanim/";

		// Token: 0x04004E80 RID: 20096
		[HideInInspector]
		public readonly string strAssetEndAnimationInfoFolder = "list/h/endanimation/";

		// Token: 0x04004E81 RID: 20097
		[HideInInspector]
		public readonly string strAssetMoveOffsetListFolder = "list/h/move/";

		// Token: 0x04004E82 RID: 20098
		[HideInInspector]
		public readonly string strAssetNeckCtrlListFolder = "list/h/neckcontrol/";

		// Token: 0x04004E83 RID: 20099
		[HideInInspector]
		public readonly string strAssetYureListFolder = "list/h/yure/";

		// Token: 0x04004E84 RID: 20100
		[HideInInspector]
		public readonly string strAssetLayerCtrlListFolder = "list/h/layer/";

		// Token: 0x04004E85 RID: 20101
		[HideInInspector]
		public readonly string strAssetDankonListFolder = "list/h/lookatdan/";

		// Token: 0x04004E86 RID: 20102
		[HideInInspector]
		public readonly string strAssetDynamicBoneListFolder = "list/h/dynamicbone/";

		// Token: 0x04004E87 RID: 20103
		[HideInInspector]
		public readonly string strAssetHAutoListFolder = "list/h/hauto/hauto/01.unity3d";

		// Token: 0x04004E88 RID: 20104
		[HideInInspector]
		public readonly string strAssetLeaveItToYouFolder = "list/h/hauto/leaveittoyou/";

		// Token: 0x04004E89 RID: 20105
		[HideInInspector]
		public readonly string strAssetHParticleListFolder = "list/h/hparticle/";

		// Token: 0x04004E8A RID: 20106
		[HideInInspector]
		public readonly string strAssetSiruPasteListFolder = "list/h/sirupaste/";

		// Token: 0x04004E8B RID: 20107
		[HideInInspector]
		public readonly string strAssetMetaBallListFolder = "list/h/sirumetaball/";

		// Token: 0x04004E8C RID: 20108
		[HideInInspector]
		public readonly string strAssetHpointPrefabListFolder = "list/h/hpoint/prefab/";

		// Token: 0x04004E8D RID: 20109
		[HideInInspector]
		public readonly string strAssetHpointListFolder = "list/h/hpoint/pointinfo/";

		// Token: 0x04004E8E RID: 20110
		[HideInInspector]
		public readonly string strAssetFeelHitListFolder = "list/h/feelhit/";

		// Token: 0x04004E8F RID: 20111
		[HideInInspector]
		public readonly string strAssetHItemInfoListFolder = "list/h/hitem/";

		// Token: 0x04004E90 RID: 20112
		[HideInInspector]
		public readonly string strAssetHItemObjInfoListFolder = "list/h/hitemobj/";

		// Token: 0x04004E91 RID: 20113
		[HideInInspector]
		public readonly string strAssetHitObjListFolder = "list/h/hit/hitobject/";

		// Token: 0x04004E92 RID: 20114
		[HideInInspector]
		public readonly string strAssetCollisionListFolder = "list/h/hit/collision/";

		// Token: 0x04004E93 RID: 20115
		[HideInInspector]
		public readonly string strAssetSEListFolder = "list/h/sound/se/";

		// Token: 0x04004E94 RID: 20116
		[HideInInspector]
		public readonly string strAssetBGMListFolder = "list/h/sound/bgm/";

		// Token: 0x04004E95 RID: 20117
		[HideInInspector]
		public readonly string strAssetVoiceListFolder = "list/h/sound/voice/";

		// Token: 0x04004E96 RID: 20118
		[HideInInspector]
		public readonly string strAssetBreathListFolder = "list/h/sound/breath/";

		// Token: 0x04004E97 RID: 20119
		[HideInInspector]
		public readonly string strAssetSE = "sound/data/se/h";

		// Token: 0x04004E98 RID: 20120
		[HideInInspector]
		public readonly string strAssetParam = "list/h/param/";

		// Token: 0x04004E99 RID: 20121
		[HideInInspector]
		public readonly string strAssetIKListFolder = "list/h/ikinfo/";

		// Token: 0x04004E9A RID: 20122
		[HideInInspector]
		public readonly string HmeshListFolder = "list/map/chunk/";

		// Token: 0x04004E9B RID: 20123
		public int maleFinish;

		// Token: 0x04004E9C RID: 20124
		public int femalePlayerFinish;

		// Token: 0x04004E9D RID: 20125
		public int femaleFinish;

		// Token: 0x04004E9E RID: 20126
		public bool isCtrl;

		// Token: 0x04004E9F RID: 20127
		public byte endStatus;

		// Token: 0x04004EA0 RID: 20128
		public bool onDeskChair;

		// Token: 0x04004EA1 RID: 20129
		public Dictionary<AgentActor, Desire.ActionType> ReturnActionTypes = new Dictionary<AgentActor, Desire.ActionType>();

		// Token: 0x04004EA2 RID: 20130
		private Vector3 start = Vector3.zero;

		// Token: 0x04004EA3 RID: 20131
		public IDisposable choiceDisposable;

		// Token: 0x04004EA4 RID: 20132
		private bool _isH;

		// Token: 0x04004EA5 RID: 20133
		public bool IsHousingHEnter;

		// Token: 0x04004EA6 RID: 20134
		public HPoint enterPoint;

		// Token: 0x04004EA7 RID: 20135
		public bool[] FemaleLumpActive = new bool[2];

		// Token: 0x02000B27 RID: 2855
		public enum HEvent
		{
			// Token: 0x04004EAB RID: 20139
			Hpoint = -1,
			// Token: 0x04004EAC RID: 20140
			Normal,
			// Token: 0x04004EAD RID: 20141
			Yobai,
			// Token: 0x04004EAE RID: 20142
			Bath,
			// Token: 0x04004EAF RID: 20143
			Toilet1,
			// Token: 0x04004EB0 RID: 20144
			Toilet2,
			// Token: 0x04004EB1 RID: 20145
			ShagmiBare,
			// Token: 0x04004EB2 RID: 20146
			Back,
			// Token: 0x04004EB3 RID: 20147
			Kitchen,
			// Token: 0x04004EB4 RID: 20148
			Tachi,
			// Token: 0x04004EB5 RID: 20149
			Stairs,
			// Token: 0x04004EB6 RID: 20150
			StairsBare,
			// Token: 0x04004EB7 RID: 20151
			GyakuYobai,
			// Token: 0x04004EB8 RID: 20152
			FromFemale,
			// Token: 0x04004EB9 RID: 20153
			MapBath,
			// Token: 0x04004EBA RID: 20154
			KabeanaBack,
			// Token: 0x04004EBB RID: 20155
			KabeanaFront,
			// Token: 0x04004EBC RID: 20156
			Neonani,
			// Token: 0x04004EBD RID: 20157
			TsukueBare
		}
	}
}
