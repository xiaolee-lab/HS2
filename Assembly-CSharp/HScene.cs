using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ADV;
using AIChara;
using AIProject;
using AIProject.Animal;
using AIProject.Definitions;
using AIProject.SaveData;
using AIProject.Scene;
using ConfigScene;
using Correct;
using Illusion;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

// Token: 0x02000ACE RID: 2766
public class HScene : BaseLoader
{
	// Token: 0x17000EE2 RID: 3810
	// (get) Token: 0x060050F6 RID: 20726 RVA: 0x001FE38C File Offset: 0x001FC78C
	public HParticleCtrl CtrlParticle
	{
		get
		{
			if (this.ctrlParitcle == null)
			{
				this.ctrlParitcle = ((!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.HSceneTable.hParticle);
			}
			return this.ctrlParitcle;
		}
	}

	// Token: 0x17000EE3 RID: 3811
	// (get) Token: 0x060050F7 RID: 20727 RVA: 0x001FE3C4 File Offset: 0x001FC7C4
	public bool NowChangeAnim
	{
		[CompilerGenerated]
		get
		{
			return this.nowChangeAnim;
		}
	}

	// Token: 0x17000EE4 RID: 3812
	// (get) Token: 0x060050F8 RID: 20728 RVA: 0x001FE3CC File Offset: 0x001FC7CC
	private OpenData openData { get; } = new OpenData();

	// Token: 0x060050F9 RID: 20729 RVA: 0x001FE3D4 File Offset: 0x001FC7D4
	public IEnumerator InitCoroutine()
	{
		this.useLotion = false;
		this.prevCharaEntry = new bool[Config.GraphicData.CharasEntry.Length];
		for (int k = 0; k < this.prevCharaEntry.Length; k++)
		{
			this.prevCharaEntry[k] = Config.GraphicData.CharasEntry[k];
		}
		this.nowStart = true;
		this.hSceneManager = Singleton<HSceneManager>.Instance;
		this.ctrlFlag.bFutanari = this.hSceneManager.bFutanari;
		this.ctrlFlag.semenType = Config.HData.Siru;
		this.racM = new RuntimeAnimatorController[2];
		this.HoushiRacM = new RuntimeAnimatorController[2];
		this.racF = new RuntimeAnimatorController[2];
		this.HoushiRacF = new RuntimeAnimatorController[2];
		this.racEtcM.Clear();
		this.racEtcF.Clear();
		this.fade = this.hSceneManager.Player.CameraControl.CrossFade;
		this.ctrlFlag.cameraCtrl.Init();
		this.ctrlFlag.cameraCtrl.loadVanish();
		this.HousingVanishSet();
		int nipIndex = ChaFileDefine.cf_bodyshapename.Length - 1;
		this.chaFemales = new ChaControl[2];
		this.chaMales = new ChaControl[2];
		this.chaFemalesTrans = new Transform[2];
		this.chaMalesTrans = new Transform[2];
		this.initStandNip.Clear();
		GameObject objCommon = GameObject.Find("CommonSpace");
		if (this.hSceneManager.females[0] == null)
		{
			ChaFileControl chaFileControl = new ChaFileControl();
			if (!this.hSceneManager.pngFemales[0].IsNullOrEmpty())
			{
				if (!chaFileControl.LoadCharaFile(this.hSceneManager.pngFemales[0], 255, true, true))
				{
					chaFileControl = null;
				}
			}
			else
			{
				chaFileControl = null;
			}
			this.chaFemales[0] = Singleton<Character>.Instance.CreateChara(1, objCommon, 0, chaFileControl);
			this.chaFemales[0].Load(false);
			this.chaFemalesTrans[0] = this.chaFemales[0].transform;
			this.StartPos.position = this.chaFemalesTrans[0].position;
			this.StartPos.rotation = this.chaFemalesTrans[0].rotation;
		}
		else
		{
			this.chaFemales[0] = this.hSceneManager.females[0].ChaControl;
			this.chaFemales[0].visibleAll = true;
			this.chaFemalesTrans[0] = this.chaFemales[0].transform;
			Transform transform = this.hSceneManager.females[0].Controller.transform;
			this.StartPos.position = transform.position;
			this.StartPos.rotation = transform.rotation;
			this.initStandNip.Add(0, this.chaFemales[0].GetShapeBodyValue(nipIndex));
		}
		if (!(this.hSceneManager.females[1] == null))
		{
			this.chaFemales[1] = this.hSceneManager.females[1].ChaControl;
			this.chaFemalesTrans[1] = this.chaFemales[1].transform;
			this.initStandNip.Add(1, this.chaFemales[1].GetShapeBodyValue(nipIndex));
		}
		objCommon = GameObject.Find("HScene");
		if (!(this.hSceneManager.male == null))
		{
			this.chaMales[0] = this.hSceneManager.male.ChaControl;
			this.chaMalesTrans[0] = this.chaMales[0].transform;
		}
		for (int l = 0; l < this.chaFemales.Length; l++)
		{
			if (!(this.chaFemales[l] == null))
			{
				this.chaFemales[l].ChangeBustInert(true);
			}
		}
		if (this.hSceneManager.Player != null && this.hSceneManager.Player.ChaControl.sex == 1 && this.hSceneManager.bFutanari)
		{
			this.hSceneManager.Player.ChaControl.ChangeBustInert(true);
		}
		this.sprite = Singleton<HSceneSprite>.Instance;
		this.hPointCtrl.SetHSceneSprite(this.sprite, this);
		this.hPointCtrl.InitHPoint();
		this.hPointCtrl.playerSex = (int)this.hSceneManager.Player.ChaControl.sex;
		if (this.hSceneManager.Player.ChaControl.sex == 1 && this.hSceneManager.bFutanari)
		{
			this.hPointCtrl.playerSex = 0;
		}
		if ((this.hSceneManager.Player.ChaControl.sex == 0 || (this.hSceneManager.Player.ChaControl.sex == 1 && this.hSceneManager.bFutanari)) && this.chaFemales[1] != null)
		{
			this.hPointCtrl.ExistSecondfemale = true;
		}
		if (!this.hSceneManager.IsHousingHEnter)
		{
			if (this.hSceneManager.HMeshCheck(0, this.deskChairInfos))
			{
				if (this.hSceneManager.hitHmesh.Item3 < 0)
				{
					if (this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai)
					{
						this.hSceneManager.height = 0;
					}
					else
					{
						this.hSceneManager.height = 1;
					}
				}
				else
				{
					this.hSceneManager.height = this.hSceneManager.hitHmesh.Item3;
				}
			}
			else
			{
				switch (this.hSceneManager.EventKind)
				{
				case HSceneManager.HEvent.Bath:
					this.hSceneManager.height = 11;
					this.ctrlFlag.AddParam(27, 1);
					goto IL_96F;
				case HSceneManager.HEvent.Toilet1:
					this.hSceneManager.height = 13;
					goto IL_96F;
				case HSceneManager.HEvent.Toilet2:
				case HSceneManager.HEvent.ShagmiBare:
					this.hSceneManager.height = 14;
					goto IL_96F;
				case HSceneManager.HEvent.Back:
				case HSceneManager.HEvent.GyakuYobai:
				case HSceneManager.HEvent.Neonani:
					this.hSceneManager.height = 0;
					goto IL_96F;
				case HSceneManager.HEvent.Kitchen:
					this.hSceneManager.height = 9;
					goto IL_96F;
				case HSceneManager.HEvent.Tachi:
					this.hSceneManager.height = 12;
					this.ctrlFlag.AddParam(27, 1);
					goto IL_96F;
				case HSceneManager.HEvent.Stairs:
				case HSceneManager.HEvent.StairsBare:
					this.hSceneManager.height = 10;
					goto IL_96F;
				case HSceneManager.HEvent.MapBath:
					this.hSceneManager.height = 1;
					this.ctrlFlag.AddParam(27, 1);
					goto IL_96F;
				case HSceneManager.HEvent.KabeanaBack:
				case HSceneManager.HEvent.KabeanaFront:
					this.hSceneManager.height = 15;
					goto IL_96F;
				case HSceneManager.HEvent.TsukueBare:
					this.hSceneManager.height = 4;
					goto IL_96F;
				}
				yield return this.PutHmesh();
				IL_96F:
				this.hSceneManager.hitHmesh = new UnityEx.ValueTuple<RaycastHit, GameObject, int>(this.hSceneManager.hitHmesh.Item1, this.hSceneManager.hitHmesh.Item2, this.hSceneManager.height);
			}
		}
		GameObject hmesh;
		Singleton<Manager.Resources>.Instance.HSceneTable.HMeshObjDic.TryGetValue(Singleton<Manager.Map>.Instance.MapID, out hmesh);
		if (hmesh != null)
		{
			hmesh.SetActive(false);
		}
		this.hPointCtrl.Init(Singleton<Manager.Map>.Instance.MapID, this.hSceneManager.Player.AreaID);
		if (this.hSceneManager.EventKind != HSceneManager.HEvent.Yobai)
		{
			if (this.hSceneManager.height != -1 && this.hSceneManager.height != 1 && !this.hSceneManager.IsHousingHEnter)
			{
				if (this.hPointCtrl.CheckStartPoint(ref this.StartPos, this.hSceneManager.height))
				{
					this.sprite.usePoint = true;
					this.hPointCtrl.InitUsePoint = true;
				}
			}
			else if (this.hSceneManager.IsHousingHEnter)
			{
				this.hPointCtrl.HousingHStart(this.hSceneManager.enterPoint);
				this.sprite.usePoint = true;
				this.hPointCtrl.InitUsePoint = true;
				this.StartPos.position = this.hSceneManager.enterPoint.pivot.position;
				this.StartPos.rotation = this.hSceneManager.enterPoint.pivot.rotation;
			}
		}
		else if (this.hPointCtrl.CheckStartPointYobai(ref this.StartPos))
		{
			this.sprite.usePoint = true;
			this.hPointCtrl.InitUsePoint = true;
		}
		else
		{
			this.hSceneManager.height = 0;
		}
		if (this.hSceneManager.height != 11)
		{
			this.hPointCtrl.SetStartPos(this.StartPos, this.hSceneManager.height);
		}
		else if (!this.sprite.usePoint)
		{
			this.hSceneManager.height = 1;
			this.hPointCtrl.SetStartPos(this.StartPos, this.hSceneManager.height);
		}
		if (this.hSceneManager.EventKind == HSceneManager.HEvent.Normal && !this.hSceneManager.IsHousingHEnter)
		{
			this.hSceneManager.females[0].Position = this.StartPos.position;
			this.chaFemalesTrans[0].position = this.StartPos.position;
			this.chaFemalesTrans[0].rotation = this.StartPos.rotation;
			this.hSceneManager.females[0].ChaControl.animBody.transform.localPosition = Vector3.zero;
			this.hSceneManager.females[0].ChaControl.animBody.transform.localRotation = Quaternion.identity;
			if (!this.hSceneManager.bMerchant)
			{
				yield return this.BeforeWait();
			}
			this.hSceneManager.Player.SetActiveOnEquipedItem(false);
			this.hSceneManager.Player.ChaControl.setAllLayerWeight(0f);
		}
		this.ctrlFlag.nPlace = this.hSceneManager.height;
		if (this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai)
		{
			this.ctrlFlag.isFaintness = true;
		}
		this.CreateListAnimationFileName();
		if (this.chaMales[0] != null)
		{
			this.ctrlMeta = new MetaballCtrl(this.objMetaBallBase, this.chaMales[0].objBodyBone, this.chaFemales[0].objBodyBone, this.chaFemales[0]);
		}
		else
		{
			this.ctrlMeta = new MetaballCtrl(this.objMetaBallBase, null, this.chaFemales[0].objBodyBone, this.chaFemales[0]);
		}
		this.ctrlItem = new HItemCtrl();
		this.ctrlItem.HItemInit(this.hitemPlace);
		for (int i = 0; i < this.ctrlHitObjectFemales.Length; i++)
		{
			if (!(this.chaFemales[i] == null) && !(this.chaFemales[i].objBodyBone == null))
			{
				this.ctrlHitObjectFemales[i].id = i;
				yield return this.ctrlHitObjectFemales[i].HitObjInit(1, this.chaFemales[i].objBodyBone, this.chaFemales[i]);
			}
		}
		for (int j = 0; j < this.ctrlHitObjectMales.Length; j++)
		{
			if (!(this.chaMales[j] == null) && !(this.chaMales[j].objBodyBone == null))
			{
				this.ctrlHitObjectMales[j].id = j;
				yield return this.ctrlHitObjectMales[j].HitObjInit(0, this.chaMales[j].objBodyBone, this.chaMales[j]);
			}
		}
		for (int m = 0; m < this.chaMales.Length; m++)
		{
			if (!(this.chaMales[m] == null) && !(this.chaMales[m].objBodyBone == null))
			{
				this.ctrlLookAts[m].DankonInit(this.chaMales[m], this.chaFemales);
			}
		}
		this.ctrlFeelHit = new FeelHit();
		if (!this.hSceneManager.bMerchant)
		{
			this.ctrlFeelHit.FeelHitInit(this.hSceneManager.Personality[0]);
		}
		else
		{
			this.ctrlFeelHit.FeelHitInit(90);
		}
		this.ctrlYures[0].chaFemale = this.chaFemales[0];
		this.ctrlYures[0].femaleID = 0;
		if (this.chaFemales[1] != null && this.chaFemales[1].objBodyBone != null)
		{
			this.ctrlYures[1].chaFemale = this.chaFemales[1];
			this.ctrlYures[1].femaleID = 1;
		}
		if (this.chaMales[0] != null && this.chaMales[0].objBodyBone != null)
		{
			this.ctrlYureMale.chaMale = this.chaMales[0];
			this.ctrlYureMale.MaleID = 0;
		}
		this.ctrlLayer.Init(this.chaFemales, this.chaMales);
		if (!this.hSceneManager.bMerchant)
		{
			this.ctrlAuto.Load(this.hSceneManager.strAssetLeaveItToYouFolder, this.hSceneManager.Personality[0], 0);
		}
		else
		{
			this.ctrlAuto.Load(this.hSceneManager.strAssetLeaveItToYouFolder, 5, 0);
		}
		this.ctrlDynamics[0] = new DynamicBoneReferenceCtrl();
		this.ctrlDynamics[1] = new DynamicBoneReferenceCtrl();
		this.ctrlDynamics[0].Init(this.chaFemales[0]);
		this.ctrlSE = new HSeCtrl();
		if (this.hSceneManager.Player.ChaControl.sex == 0 || this.hSceneManager.Player.ChaControl.fileParam.futanari)
		{
			bool existFemale2 = this.hSceneManager.Agent[1] != null;
			if (existFemale2)
			{
				if (!this.hSceneManager.bMerchant)
				{
					yield return this.ctrlVoice.Init(this.hSceneManager.Agent[0].ChaControl.fileParam.personality, this.hSceneManager.Agent[0].ChaControl.fileParam.voicePitch, this.hSceneManager.Agent[0], this.hSceneManager.Agent[1].ChaControl.fileParam.personality, this.hSceneManager.Agent[1].ChaControl.fileParam.voicePitch, this.hSceneManager.Agent[1], -1, false, false);
				}
				else
				{
					yield return this.ctrlVoice.Init(-90, this.hSceneManager.merchantActor.ChaControl.fileParam.voicePitch, this.hSceneManager.merchantActor, this.hSceneManager.Agent[1].ChaControl.fileParam.personality, this.hSceneManager.Agent[1].ChaControl.fileParam.voicePitch, this.hSceneManager.Agent[1], 0, false, false);
				}
			}
			else if (!this.hSceneManager.bMerchant)
			{
				yield return this.ctrlVoice.Init(this.hSceneManager.Agent[0].ChaControl.fileParam.personality, this.hSceneManager.Agent[0].ChaControl.fileParam.voicePitch, this.hSceneManager.Agent[0], 0, 0f, null, -1, false, false);
			}
			else
			{
				yield return this.ctrlVoice.Init(-90, this.hSceneManager.merchantActor.ChaControl.fileParam.voicePitch, this.hSceneManager.merchantActor, 0, 0f, null, 0, false, false);
			}
		}
		else if (!this.hSceneManager.bMerchant)
		{
			yield return this.ctrlVoice.Init(this.hSceneManager.Agent[0].ChaControl.fileParam.personality, this.hSceneManager.Agent[0].ChaControl.fileParam.voicePitch, this.hSceneManager.Agent[0], 0, 0f, null, -1, false, false);
		}
		else
		{
			yield return this.ctrlVoice.Init(-90, this.hSceneManager.merchantActor.ChaControl.fileParam.voicePitch, this.hSceneManager.merchantActor, 0, 0f, null, 0, false, false);
		}
		if (this.chaFemales[0] != null && this.chaFemales[0].cmpBoneBody.targetEtc.trfHeadParent != null)
		{
			this.ctrlFlag.voice.voiceTrs[0] = this.chaFemales[0].cmpBoneBody.targetEtc.trfHeadParent;
		}
		else
		{
			this.ctrlFlag.voice.voiceTrs[0] = null;
		}
		if (this.chaFemales[1] != null && this.chaFemales[1].cmpBoneBody.targetEtc.trfHeadParent != null)
		{
			this.ctrlFlag.voice.voiceTrs[1] = this.chaFemales[1].cmpBoneBody.targetEtc.trfHeadParent;
		}
		else
		{
			this.ctrlFlag.voice.voiceTrs[1] = null;
		}
		this.ctrlEyeNeckFemale[0].Init(this.chaFemales[0], 0);
		this.ctrlEyeNeckFemale[0].SetPartner((!(this.chaMales[0] != null)) ? null : this.chaMales[0].objBodyBone, (!(this.chaMales[1] != null)) ? null : this.chaMales[1].objBodyBone, (!(this.chaFemales[1] != null)) ? null : this.chaFemales[1].objBodyBone);
		if (this.chaMales[0] != null && this.chaMales[0].objBodyBone != null)
		{
			this.ctrlEyeNeckMale[0].Init(this.chaMales[0], 0);
			this.ctrlEyeNeckMale[0].SetPartner(this.chaFemales[0].objBodyBone, (!(this.chaFemales[1] != null)) ? null : this.chaFemales[1].objBodyBone, (!(this.chaMales[1] != null)) ? null : this.chaMales[1].objBodyBone);
		}
		yield return null;
		bool tmp = false;
		if (this.chaFemales[1] != null && this.chaFemales[1].objBodyBone != null)
		{
			if (this.hSceneManager.Player.ChaControl.sex != 1 || this.ctrlFlag.bFutanari)
			{
				this.ctrlEyeNeckFemale[1].Init(this.chaFemales[1], 1);
				this.ctrlEyeNeckFemale[1].SetPartner((!(this.chaMales[0] != null)) ? null : this.chaMales[0].objBodyBone, (!(this.chaMales[1] != null)) ? null : this.chaMales[1].objBodyBone, this.chaFemales[0].objBodyBone);
			}
			else
			{
				this.hMotionEyeNeckLesP.Init(this.chaFemales[1], 1);
				this.hMotionEyeNeckLesP.SetPartner(this.chaFemales[0].objBodyBone);
			}
			tmp = true;
		}
		if (this.chaMales[1] != null && this.chaMales[1].objBodyBone != null)
		{
			this.ctrlEyeNeckMale[1].Init(this.chaMales[1], 1);
			this.ctrlEyeNeckMale[1].SetPartner(this.chaFemales[0].objBodyBone, (!(this.chaFemales[1] != null)) ? null : this.chaFemales[1].objBodyBone, (!(this.chaMales[0] != null)) ? null : this.chaMales[0].objBodyBone);
			tmp = true;
		}
		if (tmp)
		{
			yield return null;
		}
		this.ctrlSiruPastes[0].Init(this.chaFemales[0]);
		if (this.chaFemales[1] != null && this.chaFemales[1].objBodyBone != null)
		{
			this.ctrlSiruPastes[1].Init(this.chaFemales[1]);
		}
		this.ctrlMeta.SetParticle(this.CtrlParticle);
		if (this.objGrondInstantiate)
		{
			this.objGrondCollision = UnityEngine.Object.Instantiate<GameObject>(this.objGrondInstantiate, this.chaFemales[0].objTop.transform);
			this.objGrondCollision.name = this.objGrondInstantiate.name;
			this.objGrondCollision.transform.localPosition = Vector3.zero;
			this.objGrondCollision.transform.localRotation = Quaternion.identity;
		}
		this.runtimeAnimatorControllers = Singleton<Manager.Resources>.Instance.HSceneTable.HBaseRuntimeAnimatorControllers;
		if (this.hSceneManager.EventKind == HSceneManager.HEvent.FromFemale)
		{
			this.ctrlFlag.initiative = 1;
		}
		if (this.hSceneManager.EventKind == HSceneManager.HEvent.GyakuYobai)
		{
			this.ctrlFlag.initiative = 2;
		}
		if (this.ctrlFlag.nowAnimationInfo.nAnimListInfoID == 3 && this.ctrlFlag.nowAnimationInfo.id == 8)
		{
			this.chaFemales[0].SetAccessoryStateAll(false);
		}
		if (this.hSceneManager.EventKind == HSceneManager.HEvent.Bath)
		{
			this.ctrlFlag.rateTuya = 1f;
		}
		this.SetStartVoice();
		AnimatorStateInfo ai = this.chaFemales[0].getAnimatorStateInfo(0);
		this.ctrlEyeNeckFemale[0].Proc(ai, null, 0);
		this.ctrlFlag.voice.sleep = (this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai);
		this.ctrlVoice.BreathProc(ai, this.chaFemales[0], 0, this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai);
		Singleton<Sound>.Instance.Listener = this.ctrlFlag.cameraCtrl.transform;
		this.ctrlFlag.selectAnimationListInfo = null;
		yield return this.sprite.Init();
		this.sprite.StartAnimInfo = this.StartAnimInfo;
		this.sprite.setAnimationList(this.lstAnimInfo);
		this.sprite.Setting(this.chaFemales);
		bool initiative = this.ctrlFlag.initiative != 0;
		this.sprite.MainCategoryOfLeaveItToYou(initiative);
		this.sprite.SetToggleLeaveItToYou(initiative);
		this.sprite.SetFinishSelect(this.mode, this.modeCtrl, -1, -1);
		yield return null;
		DeliveryMember member = new DeliveryMember();
		member.ctrlFlag = this.ctrlFlag;
		member.chaMales = this.chaMales;
		member.chaFemales = this.chaFemales;
		member.fade = this.fade;
		member.ctrlMeta = this.ctrlMeta;
		member.sprite = this.sprite;
		member.item = this.ctrlItem;
		member.feelHit = this.ctrlFeelHit;
		member.auto = this.ctrlAuto;
		member.voice = this.ctrlVoice;
		member.particle = this.CtrlParticle;
		member.se = this.ctrlSE;
		member.lstMotionIK = this.lstMotionIK;
		member.AtariEffect = this.AtariEffect;
		member.FeelHitEffect3D = this.FeelHitEffect3D;
		this.lstProc.Add(new Aibu(member));
		this.lstProc.Add(new Houshi(member));
		(this.lstProc[1] as Houshi).SetAnimationList(this.lstAnimInfo[1]);
		this.lstProc.Add(new Sonyu(member));
		yield return null;
		this.lstProc.Add(new Spnking(member));
		this.lstProc.Add(new global::Masturbation(member));
		this.lstProc.Add(new Peeping(member));
		yield return null;
		this.lstProc.Add(new Les(member));
		this.lstProc.Add(new MultiPlay_F2M1(member));
		(this.lstProc[7] as MultiPlay_F2M1).SetAnimationList(this.lstAnimInfo[5]);
		if (this.mode != -1 && this.modeCtrl != -1 && ProcBase.endInit)
		{
			this.lstProc[this.mode].setAnimationParamater();
		}
		yield return null;
		base.enabled = true;
		this.nowStart = false;
		this.sprite.enabled = true;
		this.nullPlayer = (this.hSceneManager.Player == null);
		if (!this.nullPlayer && this.Camera == null)
		{
			this.Camera = this.hSceneManager.Player.CameraControl;
		}
		if (!this.nullPlayer && !this.hSceneManager.bMerchant)
		{
			this.Camera.HCamera = this.ctrlFlag.cameraCtrl;
		}
		List<Light> light = new List<Light>();
		if (!this.nullPlayer)
		{
			this.Camera.DisableUpdateCustomLight();
			light.Add(this.Camera.NormalKeyLight);
			light.Add(this.Camera.CustomKeyLight);
		}
		else
		{
			light.Add(this.ctrlFlag.cameraCtrl.thisCamera.GetComponentInChildren<Light>());
		}
		this.infoLight = new HScene.LightInfo[2];
		for (int n = 0; n < light.Count; n++)
		{
			int num = n;
			this.infoLight[num] = new HScene.LightInfo();
			this.infoLight[num].objCharaLight = light[num].gameObject;
			this.infoLight[num].light = light[num];
			this.infoLight[num].initRot = light[num].transform.localRotation;
			this.infoLight[num].initIntensity = Mathf.InverseLerp(this.infoLight[num].minIntensity, this.infoLight[num].maxIntensity, light[num].intensity);
			this.infoLight[num].initColor = light[num].color;
		}
		this.sprite.SetLightInfo(this.infoLight);
		this.HItemDrag = this.sprite.objHItem.GetComponent<HSceneSpriteHitem>();
		this.NowStateIsEnd = false;
		Game instance = Singleton<Game>.Instance;
		PlayerData playerData2;
		if (instance == null)
		{
			playerData2 = null;
		}
		else
		{
			WorldData worldData = instance.WorldData;
			playerData2 = ((worldData != null) ? worldData.PlayerData : null);
		}
		PlayerData playerData = playerData2;
		int charaID;
		int adv_category;
		IParams param;
		if (!this.hSceneManager.bMerchant)
		{
			AgentData agentData = this.hSceneManager.Agent[0].AgentData;
			charaID = agentData.param.charaID;
			adv_category = 10;
			param = agentData;
		}
		else
		{
			MerchantData merchantData = this.hSceneManager.merchantActor.MerchantData;
			charaID = merchantData.param.charaID;
			adv_category = 10;
			param = merchantData;
		}
		this.packData = new HScene.PackData(charaID, adv_category);
		CharaPackData charaPackData = this.packData;
		ICommandData[] array = new ICommandData[1];
		int num2 = 0;
		Game instance2 = Singleton<Game>.Instance;
		ICommandData commandData;
		if (instance2 == null)
		{
			commandData = null;
		}
		else
		{
			WorldData worldData2 = instance2.WorldData;
			commandData = ((worldData2 != null) ? worldData2.Environment : null);
		}
		array[num2] = commandData;
		charaPackData.SetCommandData(array);
		this.packData.SetParam(new IParams[]
		{
			param,
			playerData
		});
		if (this.hSceneManager.EventKind == HSceneManager.HEvent.Back)
		{
			Singleton<HSceneFlagCtrl>.Instance.AddParam(31, 1);
		}
		else if (this.hSceneManager.EventKind == HSceneManager.HEvent.Kitchen)
		{
			Singleton<HSceneFlagCtrl>.Instance.AddParam(32, 1);
		}
		else if (this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai)
		{
			Singleton<HSceneFlagCtrl>.Instance.AddParam(33, 1);
		}
		if (this.ctrlFlag.nowHPoint != null)
		{
			foreach (KeyValuePair<int, UnityEx.ValueTuple<int, int>> keyValuePair in this.ctrlFlag.nowHPoint._nPlace)
			{
				if (keyValuePair.Value.Item1 == 13 || keyValuePair.Value.Item1 == 14)
				{
					this.ctrlFlag.AddParam(29, 1);
					this.ctrlFlag.isToilet = true;
				}
			}
		}
		yield break;
	}

	// Token: 0x060050FA RID: 20730 RVA: 0x001FE3F0 File Offset: 0x001FC7F0
	private void Update()
	{
		if (this.ctrlFlag.BeforeHWait)
		{
			if (!this.hSceneManager.isForce)
			{
				this.ctrlVoice.HBeforeHouchiTime += Time.unscaledDeltaTime;
				if (this.ctrlVoice.HBeforeHouchiTime >= this.ctrlFlag.HBeforeHouchiTime)
				{
					this.ctrlVoice.HBeforeProc(this.chaFemales);
				}
			}
			this.ShortcutKey();
			return;
		}
		if (this.aInfo != this.ctrlFlag.nowAnimationInfo)
		{
			this.aInfo = this.ctrlFlag.nowAnimationInfo;
		}
		if (this.ctrlFlag.cameraCtrl.isConfigTargetTex != Config.ActData.Look)
		{
			this.ctrlFlag.cameraCtrl.isConfigTargetTex = Config.ActData.Look;
		}
		if (this.ctrlFlag.rateNip < this.ctrlFlag.feel_f)
		{
			this.ctrlFlag.rateNip = this.ctrlFlag.feel_f;
		}
		float rate = Mathf.Lerp(0f, this.ctrlFlag.rateNipMax, this.ctrlFlag.rateNip);
		bool gloss = Config.HData.Gloss;
		if (this.ctrlFlag.rateTuya < this.ctrlFlag.feel_f && gloss)
		{
			this.ctrlFlag.rateTuya = this.ctrlFlag.feel_f;
		}
		this.PlayerWet();
		if (!this.useLotion)
		{
			this.useLotion = this.HItemDrag.Effect(7);
		}
		if (this.ctrlFlag.rateTuya < 1f && this.useLotion && gloss)
		{
			this.ctrlFlag.rateTuya = 1f;
			this.HItemDrag.SetUse(7, false);
		}
		float skinGlossRate = (!this.isTuyaOn) ? this.ctrlFlag.rateTuya : 1f;
		if (!gloss)
		{
			skinGlossRate = 0f;
		}
		for (int i = 0; i < this.chaFemales.Length; i++)
		{
			if (this.chaFemales[i] == null || this.chaFemales[i].objTop == null)
			{
				break;
			}
			if (!(this.hSceneManager.Player != null) || !(this.hSceneManager.Player.ChaControl == this.chaFemales[i]))
			{
				this.chaFemales[i].ChangeNipRate(rate);
				this.chaFemales[i].skinGlossRate = skinGlossRate;
			}
		}
		if (this.HItemDrag.Effect(3))
		{
			this.hSceneManager.Toilet = 70f;
			this.HItemDrag.SetUse(3, false);
		}
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.SceneEnd)
		{
			this.EndProc();
		}
		if (this.NowStateIsEnd)
		{
			return;
		}
		AnimatorStateInfo animatorStateInfo = this.chaFemales[0].getAnimatorStateInfo(0);
		this.ctrlVoice.Proc(animatorStateInfo, this.chaFemales);
		this.ctrlSiruPastes[0].Proc(animatorStateInfo);
		if (this.aInfo.nPromiscuity >= 1)
		{
			this.ctrlSiruPastes[1].Proc(animatorStateInfo);
		}
		if (this.mode != -1 && this.modeCtrl != -1 && ProcBase.endInit)
		{
			this.lstProc[this.mode].Proc(this.modeCtrl, this.aInfo);
		}
		bool isAutoAutoLeaveItToYou = false;
		if (this.ctrlFlag.click == HSceneFlagCtrl.ClickKind.LeaveItToYou)
		{
			this.ctrlFlag.initiative = ((this.ctrlFlag.initiative != 0) ? 0 : 1);
			this.ctrlFlag.isAutoActionChange = false;
			this.sprite.MainCategoryOfLeaveItToYou(this.ctrlFlag.initiative != 0);
			this.ctrlAuto.Reset();
			this.ctrlAuto.AutoAutoLeaveItToYouInit();
			if (this.ctrlFlag.initiative != 0)
			{
				this.GetAutoAnimation(false);
				if (this.ctrlFlag.selectAnimationListInfo == null)
				{
					this.GetAutoAnimation(true);
				}
				this.ctrlFlag.numLeadFemale++;
			}
			else
			{
				this.ReturnToNormalFromTheAuto();
				this.AtariEffect.Stop();
				this.FeelHitEffect3D.Stop();
			}
		}
		if (this.ctrlFlag.isAutoActionChange && this.ctrlFlag.selectAnimationListInfo == null)
		{
			this.sprite.SetMotionListDraw(false, -1);
			this.GetAutoAnimation(false);
			if (this.ctrlFlag.selectAnimationListInfo == null)
			{
				this.GetAutoAnimation(true);
				if (this.ctrlFlag.selectAnimationListInfo == null)
				{
					this.ctrlFlag.isAutoActionChange = false;
				}
			}
			this.ctrlAuto.SetSpeed(this.ctrlFlag.speed);
		}
		if (this.ctrlFlag.selectAnimationListInfo != null && !this.nowChangeAnim)
		{
			if (this.IsIdle(this.chaFemales[0].animBody) && !this.ctrlFlag.isFaintness && !this.ctrlFlag.pointMoveAnimChange && this.ctrlFlag.nowAnimationInfo != this.ctrlFlag.selectAnimationListInfo)
			{
				this.ctrlFlag.voice.playStart = 4;
			}
			this.nowChangeAnim = true;
			Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
			{
				Observable.FromCoroutine(() => this.ChangeAnimation(this.ctrlFlag.selectAnimationListInfo, false, isAutoAutoLeaveItToYou, !this.ctrlFlag.pointMoveAnimChange), false).Finally(delegate
				{
					if (!this.nowChangeAnim)
					{
						this.ctrlFlag.selectAnimationListInfo = null;
						this.ctrlFlag.isAutoActionChange = false;
					}
					if (this.ctrlFlag.pointMoveAnimChange)
					{
						this.ctrlFlag.pointMoveAnimChange = false;
					}
					GlobalMethod.setCameraMoveFlag(this.ctrlFlag.cameraCtrl, true);
					this.sprite.ChangeStart = false;
				}).Subscribe<Unit>();
			});
			if (this.hSceneManager.Player.CameraControl.Mode != CameraMode.H)
			{
				this.hSceneManager.Player.CameraControl.Mode = CameraMode.H;
				this.ctrlFlag.HBeforeCamera.gameObject.SetActive(false);
			}
		}
		for (int j = 0; j < this.ctrlHitObjectFemales.Length; j++)
		{
			if (this.aInfo.nPromiscuity < 1 && j > 0)
			{
				break;
			}
			if (!(this.chaFemales[j] == null) && !(this.chaFemales[j].objBodyBone == null))
			{
				this.ctrlHitObjectFemales[j].Proc(this.chaFemales[j].animBody);
				bool flag = this.hSceneManager.Player.ChaControl.sex == 1 && !this.ctrlFlag.bFutanari;
				if (j == 0 || !flag)
				{
					this.ctrlEyeNeckFemale[j].Proc(animatorStateInfo, this.ctrlVoice.nowVoices[j].Face, j);
				}
				else
				{
					this.hMotionEyeNeckLesP.Proc(animatorStateInfo, j);
				}
			}
		}
		for (int k = 0; k < this.ctrlHitObjectMales.Length; k++)
		{
			if (!(this.chaMales[k] == null) && !(this.chaMales[k].objTop == null))
			{
				if (this.aInfo.nPromiscuity != 0 && k > 0)
				{
					break;
				}
				if (!(this.chaMales[k].objBodyBone == null))
				{
					this.ctrlHitObjectMales[k].Proc(this.chaMales[k].animBody);
					this.ctrlEyeNeckMale[k].Proc(animatorStateInfo);
				}
			}
		}
		this.ctrlDynamics[0].Proc();
		if (this.aInfo.nPromiscuity >= 1)
		{
			this.ctrlDynamics[1].Proc();
		}
		this.ctrlSE.Proc(animatorStateInfo, this.chaFemales);
		this.sprite.GuidProc(animatorStateInfo);
		this.ShortcutKey();
		if (!GlobalMethod.IsCameraMoveFlag(this.ctrlFlag.cameraCtrl) && !UnityEngine.Input.GetMouseButton(0) && !UnityEngine.Input.GetMouseButton(1))
		{
			GlobalMethod.setCameraMoveFlag(this.ctrlFlag.cameraCtrl, true);
		}
		this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.None;
	}

	// Token: 0x060050FB RID: 20731 RVA: 0x001FEC30 File Offset: 0x001FD030
	public void SetStartAnimationInfo(HSceneManager.HEvent hEvent, int mode = -1)
	{
		this.StartAnimInfo = null;
		List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> list = new List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>>();
		list = Singleton<Manager.Resources>.Instance.HSceneTable.lstStartAnimInfo;
		if (!this.hSceneManager.IsHousingHEnter)
		{
			switch (hEvent)
			{
			case HSceneManager.HEvent.Normal:
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Item1 == hEvent && list[i].Item2 == this.hSceneManager.height)
					{
						if (mode != -1)
						{
							if (list[i].Item3.mode != mode)
							{
								goto IL_2A1;
							}
							int id = list[i].Item3.id;
							for (int j = 0; j < this.lstAnimInfo[mode].Count; j++)
							{
								if (id == this.lstAnimInfo[mode][j].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][j].nIyaAction == 0)
										{
											goto IL_193;
										}
									}
									else if (this.lstAnimInfo[mode][j].nIyaAction == 2)
									{
										goto IL_193;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][j];
									break;
								}
								IL_193:;
							}
						}
						else
						{
							int id = list[i].Item3.id;
							int num = list[i].Item3.mode;
							for (int k = 0; k < this.lstAnimInfo[num].Count; k++)
							{
								if (id == this.lstAnimInfo[num][k].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num][k].nIyaAction == 0)
										{
											goto IL_276;
										}
									}
									else if (this.lstAnimInfo[num][k].nIyaAction == 2)
									{
										goto IL_276;
									}
									this.StartAnimInfo = this.lstAnimInfo[num][k];
									break;
								}
								IL_276:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_2A1:;
				}
				break;
			case HSceneManager.HEvent.Yobai:
				for (int l = 0; l < list.Count; l++)
				{
					if (list[l].Item1 == hEvent && list[l].Item2 == this.hSceneManager.height)
					{
						if (mode != -1)
						{
							if (list[l].Item3.mode != mode)
							{
								goto IL_474;
							}
							int id = list[l].Item3.id;
							for (int m = 0; m < this.lstAnimInfo[mode].Count; m++)
							{
								if (id == this.lstAnimInfo[mode][m].id)
								{
									if (this.lstAnimInfo[mode][m].bSleep)
									{
										this.StartAnimInfo = this.lstAnimInfo[mode][m];
										break;
									}
								}
							}
						}
						else
						{
							int id = list[l].Item3.id;
							int num2 = list[l].Item3.mode;
							for (int n = 0; n < this.lstAnimInfo[num2].Count; n++)
							{
								if (id == this.lstAnimInfo[num2][n].id)
								{
									if (this.lstAnimInfo[num2][n].bSleep)
									{
										this.StartAnimInfo = this.lstAnimInfo[num2][n];
										break;
									}
								}
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_474:;
				}
				break;
			case HSceneManager.HEvent.Bath:
				for (int num3 = 0; num3 < list.Count; num3++)
				{
					if (list[num3].Item1 == hEvent && list[num3].Item2 == 11)
					{
						if (mode != -1)
						{
							if (list[num3].Item3.mode != mode)
							{
								goto IL_6AA;
							}
							int id = list[num3].Item3.id;
							for (int num4 = 0; num4 < this.lstAnimInfo[mode].Count; num4++)
							{
								if (id == this.lstAnimInfo[mode][num4].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num4].nIyaAction == 0)
										{
											goto IL_59A;
										}
									}
									else if (this.lstAnimInfo[mode][num4].nIyaAction == 2)
									{
										goto IL_59A;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num4];
									break;
								}
								IL_59A:;
							}
						}
						else
						{
							int id = list[num3].Item3.id;
							int num5 = list[num3].Item3.mode;
							for (int num6 = 0; num6 < this.lstAnimInfo[num5].Count; num6++)
							{
								if (id == this.lstAnimInfo[num5][num6].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num5][num6].nIyaAction == 0)
										{
											goto IL_67F;
										}
									}
									else if (this.lstAnimInfo[num5][num6].nIyaAction == 2)
									{
										goto IL_67F;
									}
									this.StartAnimInfo = this.lstAnimInfo[num5][num6];
									break;
								}
								IL_67F:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_6AA:;
				}
				break;
			case HSceneManager.HEvent.Toilet1:
				for (int num7 = 0; num7 < list.Count; num7++)
				{
					if (list[num7].Item1 == hEvent && list[num7].Item2 == 13)
					{
						if (mode != -1)
						{
							if (list[num7].Item3.mode != mode)
							{
								goto IL_8E0;
							}
							int id = list[num7].Item3.id;
							for (int num8 = 0; num8 < this.lstAnimInfo[mode].Count; num8++)
							{
								if (id == this.lstAnimInfo[mode][num8].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num8].nIyaAction == 0)
										{
											goto IL_7D0;
										}
									}
									else if (this.lstAnimInfo[mode][num8].nIyaAction == 2)
									{
										goto IL_7D0;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num8];
									break;
								}
								IL_7D0:;
							}
						}
						else
						{
							int id = list[num7].Item3.id;
							int num9 = list[num7].Item3.mode;
							for (int num10 = 0; num10 < this.lstAnimInfo[num9].Count; num10++)
							{
								if (id == this.lstAnimInfo[num9][num10].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num9][num10].nIyaAction == 0)
										{
											goto IL_8B5;
										}
									}
									else if (this.lstAnimInfo[num9][num10].nIyaAction == 2)
									{
										goto IL_8B5;
									}
									this.StartAnimInfo = this.lstAnimInfo[num9][num10];
									break;
								}
								IL_8B5:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_8E0:;
				}
				break;
			case HSceneManager.HEvent.Toilet2:
				for (int num11 = 0; num11 < list.Count; num11++)
				{
					if (list[num11].Item1 == hEvent && list[num11].Item2 == 14)
					{
						if (mode != -1)
						{
							if (list[num11].Item3.mode != mode)
							{
								goto IL_B16;
							}
							int id = list[num11].Item3.id;
							for (int num12 = 0; num12 < this.lstAnimInfo[mode].Count; num12++)
							{
								if (id == this.lstAnimInfo[mode][num12].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num12].nIyaAction == 0)
										{
											goto IL_A06;
										}
									}
									else if (this.lstAnimInfo[mode][num12].nIyaAction == 2)
									{
										goto IL_A06;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num12];
									break;
								}
								IL_A06:;
							}
						}
						else
						{
							int id = list[num11].Item3.id;
							int num13 = list[num11].Item3.mode;
							for (int num14 = 0; num14 < this.lstAnimInfo[num13].Count; num14++)
							{
								if (id == this.lstAnimInfo[num13][num14].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num13][num14].nIyaAction == 0)
										{
											goto IL_AEB;
										}
									}
									else if (this.lstAnimInfo[num13][num14].nIyaAction == 2)
									{
										goto IL_AEB;
									}
									this.StartAnimInfo = this.lstAnimInfo[num13][num14];
									break;
								}
								IL_AEB:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_B16:;
				}
				break;
			case HSceneManager.HEvent.ShagmiBare:
				for (int num15 = 0; num15 < list.Count; num15++)
				{
					if (list[num15].Item1 == hEvent && list[num15].Item2 == this.hSceneManager.height)
					{
						if (mode != -1)
						{
							if (list[num15].Item3.mode != mode)
							{
								goto IL_D55;
							}
							int id = list[num15].Item3.id;
							for (int num16 = 0; num16 < this.lstAnimInfo[mode].Count; num16++)
							{
								if (id == this.lstAnimInfo[mode][num16].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num16].nIyaAction == 0)
										{
											goto IL_C45;
										}
									}
									else if (this.lstAnimInfo[mode][num16].nIyaAction == 2)
									{
										goto IL_C45;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num16];
									break;
								}
								IL_C45:;
							}
						}
						else
						{
							int id = list[num15].Item3.id;
							int num17 = list[num15].Item3.mode;
							for (int num18 = 0; num18 < this.lstAnimInfo[num17].Count; num18++)
							{
								if (id == this.lstAnimInfo[num17][num18].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num17][num18].nIyaAction == 0)
										{
											goto IL_D2A;
										}
									}
									else if (this.lstAnimInfo[num17][num18].nIyaAction == 2)
									{
										goto IL_D2A;
									}
									this.StartAnimInfo = this.lstAnimInfo[num17][num18];
									break;
								}
								IL_D2A:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_D55:;
				}
				break;
			case HSceneManager.HEvent.Back:
				for (int num19 = 0; num19 < list.Count; num19++)
				{
					if (list[num19].Item1 == hEvent && list[num19].Item2 == 0)
					{
						if (mode != -1)
						{
							if (list[num19].Item3.mode != mode)
							{
								goto IL_F89;
							}
							int id = list[num19].Item3.id;
							for (int num20 = 0; num20 < this.lstAnimInfo[mode].Count; num20++)
							{
								if (id == this.lstAnimInfo[mode][num20].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num20].nIyaAction == 0)
										{
											goto IL_E79;
										}
									}
									else if (this.lstAnimInfo[mode][num20].nIyaAction == 2)
									{
										goto IL_E79;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num20];
									break;
								}
								IL_E79:;
							}
						}
						else
						{
							int id = list[num19].Item3.id;
							int num21 = list[num19].Item3.mode;
							for (int num22 = 0; num22 < this.lstAnimInfo[num21].Count; num22++)
							{
								if (id == this.lstAnimInfo[num21][num22].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num21][num22].nIyaAction == 0)
										{
											goto IL_F5E;
										}
									}
									else if (this.lstAnimInfo[num21][num22].nIyaAction == 2)
									{
										goto IL_F5E;
									}
									this.StartAnimInfo = this.lstAnimInfo[num21][num22];
									break;
								}
								IL_F5E:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_F89:;
				}
				break;
			case HSceneManager.HEvent.Kitchen:
				for (int num23 = 0; num23 < list.Count; num23++)
				{
					if (list[num23].Item1 == hEvent && list[num23].Item2 == 9)
					{
						if (mode != -1)
						{
							if (list[num23].Item3.mode != mode)
							{
								goto IL_11BF;
							}
							int id = list[num23].Item3.id;
							for (int num24 = 0; num24 < this.lstAnimInfo[mode].Count; num24++)
							{
								if (id == this.lstAnimInfo[mode][num24].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num24].nIyaAction == 0)
										{
											goto IL_10AF;
										}
									}
									else if (this.lstAnimInfo[mode][num24].nIyaAction == 2)
									{
										goto IL_10AF;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num24];
									break;
								}
								IL_10AF:;
							}
						}
						else
						{
							int id = list[num23].Item3.id;
							int num25 = list[num23].Item3.mode;
							for (int num26 = 0; num26 < this.lstAnimInfo[num25].Count; num26++)
							{
								if (id == this.lstAnimInfo[num25][num26].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num25][num26].nIyaAction == 0)
										{
											goto IL_1194;
										}
									}
									else if (this.lstAnimInfo[num25][num26].nIyaAction == 2)
									{
										goto IL_1194;
									}
									this.StartAnimInfo = this.lstAnimInfo[num25][num26];
									break;
								}
								IL_1194:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_11BF:;
				}
				break;
			case HSceneManager.HEvent.Tachi:
				for (int num27 = 0; num27 < list.Count; num27++)
				{
					if (list[num27].Item1 == hEvent && list[num27].Item2 == 12)
					{
						if (mode != -1)
						{
							if (list[num27].Item3.mode != mode)
							{
								goto IL_13F5;
							}
							int id = list[num27].Item3.id;
							for (int num28 = 0; num28 < this.lstAnimInfo[mode].Count; num28++)
							{
								if (id == this.lstAnimInfo[mode][num28].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num28].nIyaAction == 0)
										{
											goto IL_12E5;
										}
									}
									else if (this.lstAnimInfo[mode][num28].nIyaAction == 2)
									{
										goto IL_12E5;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num28];
									break;
								}
								IL_12E5:;
							}
						}
						else
						{
							int id = list[num27].Item3.id;
							int num29 = list[num27].Item3.mode;
							for (int num30 = 0; num30 < this.lstAnimInfo[num29].Count; num30++)
							{
								if (id == this.lstAnimInfo[num29][num30].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num29][num30].nIyaAction == 0)
										{
											goto IL_13CA;
										}
									}
									else if (this.lstAnimInfo[num29][num30].nIyaAction == 2)
									{
										goto IL_13CA;
									}
									this.StartAnimInfo = this.lstAnimInfo[num29][num30];
									break;
								}
								IL_13CA:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_13F5:;
				}
				break;
			case HSceneManager.HEvent.Stairs:
			case HSceneManager.HEvent.StairsBare:
				for (int num31 = 0; num31 < list.Count; num31++)
				{
					if (list[num31].Item1 == hEvent && list[num31].Item2 == 10)
					{
						if (mode != -1)
						{
							if (list[num31].Item3.mode != mode)
							{
								goto IL_1860;
							}
							int id = list[num31].Item3.id;
							for (int num32 = 0; num32 < this.lstAnimInfo[mode].Count; num32++)
							{
								if (id == this.lstAnimInfo[mode][num32].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num32].nIyaAction == 0)
										{
											goto IL_1750;
										}
									}
									else if (this.lstAnimInfo[mode][num32].nIyaAction == 2)
									{
										goto IL_1750;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num32];
									break;
								}
								IL_1750:;
							}
						}
						else
						{
							int id = list[num31].Item3.id;
							int num33 = list[num31].Item3.mode;
							for (int num34 = 0; num34 < this.lstAnimInfo[num33].Count; num34++)
							{
								if (id == this.lstAnimInfo[num33][num34].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num33][num34].nIyaAction == 0)
										{
											goto IL_1835;
										}
									}
									else if (this.lstAnimInfo[num33][num34].nIyaAction == 2)
									{
										goto IL_1835;
									}
									this.StartAnimInfo = this.lstAnimInfo[num33][num34];
									break;
								}
								IL_1835:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_1860:;
				}
				break;
			case HSceneManager.HEvent.GyakuYobai:
				for (int num35 = 0; num35 < list.Count; num35++)
				{
					if (list[num35].Item1 == hEvent && list[num35].Item2 == this.hSceneManager.height)
					{
						if (mode != -1)
						{
							if (list[num35].Item3.mode != mode)
							{
								goto IL_1A9F;
							}
							int id = list[num35].Item3.id;
							for (int num36 = 0; num36 < this.lstAnimInfo[mode].Count; num36++)
							{
								if (id == this.lstAnimInfo[mode][num36].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num36].nIyaAction == 0)
										{
											goto IL_198F;
										}
									}
									else if (this.lstAnimInfo[mode][num36].nIyaAction == 2)
									{
										goto IL_198F;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num36];
									break;
								}
								IL_198F:;
							}
						}
						else
						{
							int id = list[num35].Item3.id;
							int num37 = list[num35].Item3.mode;
							for (int num38 = 0; num38 < this.lstAnimInfo[num37].Count; num38++)
							{
								if (id == this.lstAnimInfo[num37][num38].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num37][num38].nIyaAction == 0)
										{
											goto IL_1A74;
										}
									}
									else if (this.lstAnimInfo[num37][num38].nIyaAction == 2)
									{
										goto IL_1A74;
									}
									this.StartAnimInfo = this.lstAnimInfo[num37][num38];
									break;
								}
								IL_1A74:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_1A9F:;
				}
				break;
			case HSceneManager.HEvent.FromFemale:
				for (int num39 = 0; num39 < list.Count; num39++)
				{
					if (list[num39].Item1 == hEvent && list[num39].Item2 == this.hSceneManager.height)
					{
						if (mode != -1)
						{
							if (list[num39].Item3.mode != mode)
							{
								goto IL_1CDE;
							}
							int id = list[num39].Item3.id;
							for (int num40 = 0; num40 < this.lstAnimInfo[mode].Count; num40++)
							{
								if (id == this.lstAnimInfo[mode][num40].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num40].nIyaAction == 0)
										{
											goto IL_1BCE;
										}
									}
									else if (this.lstAnimInfo[mode][num40].nIyaAction == 2)
									{
										goto IL_1BCE;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num40];
									break;
								}
								IL_1BCE:;
							}
						}
						else
						{
							int id = list[num39].Item3.id;
							int num41 = list[num39].Item3.mode;
							for (int num42 = 0; num42 < this.lstAnimInfo[num41].Count; num42++)
							{
								if (id == this.lstAnimInfo[num41][num42].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num41][num42].nIyaAction == 0)
										{
											goto IL_1CB3;
										}
									}
									else if (this.lstAnimInfo[num41][num42].nIyaAction == 2)
									{
										goto IL_1CB3;
									}
									this.StartAnimInfo = this.lstAnimInfo[num41][num42];
									break;
								}
								IL_1CB3:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_1CDE:;
				}
				break;
			case HSceneManager.HEvent.MapBath:
				for (int num43 = 0; num43 < list.Count; num43++)
				{
					if (list[num43].Item1 == hEvent && list[num43].Item2 == 1)
					{
						if (mode != -1)
						{
							if (list[num43].Item3.mode != mode)
							{
								goto IL_162A;
							}
							int id = list[num43].Item3.id;
							for (int num44 = 0; num44 < this.lstAnimInfo[mode].Count; num44++)
							{
								if (id == this.lstAnimInfo[mode][num44].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num44].nIyaAction == 0)
										{
											goto IL_151A;
										}
									}
									else if (this.lstAnimInfo[mode][num44].nIyaAction == 2)
									{
										goto IL_151A;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num44];
									break;
								}
								IL_151A:;
							}
						}
						else
						{
							int id = list[num43].Item3.id;
							int num45 = list[num43].Item3.mode;
							for (int num46 = 0; num46 < this.lstAnimInfo[num45].Count; num46++)
							{
								if (id == this.lstAnimInfo[num45][num46].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num45][num46].nIyaAction == 0)
										{
											goto IL_15FF;
										}
									}
									else if (this.lstAnimInfo[num45][num46].nIyaAction == 2)
									{
										goto IL_15FF;
									}
									this.StartAnimInfo = this.lstAnimInfo[num45][num46];
									break;
								}
								IL_15FF:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_162A:;
				}
				break;
			case HSceneManager.HEvent.KabeanaBack:
			case HSceneManager.HEvent.KabeanaFront:
				for (int num47 = 0; num47 < list.Count; num47++)
				{
					if (list[num47].Item1 == hEvent && list[num47].Item2 == 15)
					{
						if (mode != -1)
						{
							if (list[num47].Item3.mode != mode)
							{
								goto IL_1F14;
							}
							int id = list[num47].Item3.id;
							for (int num48 = 0; num48 < this.lstAnimInfo[mode].Count; num48++)
							{
								if (id == this.lstAnimInfo[mode][num48].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num48].nIyaAction == 0)
										{
											goto IL_1E04;
										}
									}
									else if (this.lstAnimInfo[mode][num48].nIyaAction == 2)
									{
										goto IL_1E04;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num48];
									break;
								}
								IL_1E04:;
							}
						}
						else
						{
							int id = list[num47].Item3.id;
							int num49 = list[num47].Item3.mode;
							for (int num50 = 0; num50 < this.lstAnimInfo[num49].Count; num50++)
							{
								if (id == this.lstAnimInfo[num49][num50].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num49][num50].nIyaAction == 0)
										{
											goto IL_1EE9;
										}
									}
									else if (this.lstAnimInfo[num49][num50].nIyaAction == 2)
									{
										goto IL_1EE9;
									}
									this.StartAnimInfo = this.lstAnimInfo[num49][num50];
									break;
								}
								IL_1EE9:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_1F14:;
				}
				break;
			case HSceneManager.HEvent.Neonani:
				for (int num51 = 0; num51 < list.Count; num51++)
				{
					if (list[num51].Item1 == hEvent && list[num51].Item2 == 0)
					{
						if (mode != -1)
						{
							if (list[num51].Item3.mode != mode)
							{
								goto IL_2148;
							}
							int id = list[num51].Item3.id;
							for (int num52 = 0; num52 < this.lstAnimInfo[mode].Count; num52++)
							{
								if (id == this.lstAnimInfo[mode][num52].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num52].nIyaAction == 0)
										{
											goto IL_2038;
										}
									}
									else if (this.lstAnimInfo[mode][num52].nIyaAction == 2)
									{
										goto IL_2038;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num52];
									break;
								}
								IL_2038:;
							}
						}
						else
						{
							int id = list[num51].Item3.id;
							int num53 = list[num51].Item3.mode;
							for (int num54 = 0; num54 < this.lstAnimInfo[num53].Count; num54++)
							{
								if (id == this.lstAnimInfo[num53][num54].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num53][num54].nIyaAction == 0)
										{
											goto IL_211D;
										}
									}
									else if (this.lstAnimInfo[num53][num54].nIyaAction == 2)
									{
										goto IL_211D;
									}
									this.StartAnimInfo = this.lstAnimInfo[num53][num54];
									break;
								}
								IL_211D:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_2148:;
				}
				break;
			case HSceneManager.HEvent.TsukueBare:
				for (int num55 = 0; num55 < list.Count; num55++)
				{
					if (list[num55].Item1 == hEvent && list[num55].Item2 == 4)
					{
						if (mode != -1)
						{
							if (list[num55].Item3.mode != mode)
							{
								goto IL_237D;
							}
							int id = list[num55].Item3.id;
							for (int num56 = 0; num56 < this.lstAnimInfo[mode].Count; num56++)
							{
								if (id == this.lstAnimInfo[mode][num56].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[mode][num56].nIyaAction == 0)
										{
											goto IL_226D;
										}
									}
									else if (this.lstAnimInfo[mode][num56].nIyaAction == 2)
									{
										goto IL_226D;
									}
									this.StartAnimInfo = this.lstAnimInfo[mode][num56];
									break;
								}
								IL_226D:;
							}
						}
						else
						{
							int id = list[num55].Item3.id;
							int num57 = list[num55].Item3.mode;
							for (int num58 = 0; num58 < this.lstAnimInfo[num57].Count; num58++)
							{
								if (id == this.lstAnimInfo[num57][num58].id)
								{
									if (this.hSceneManager.isForce)
									{
										if (this.lstAnimInfo[num57][num58].nIyaAction == 0)
										{
											goto IL_2352;
										}
									}
									else if (this.lstAnimInfo[num57][num58].nIyaAction == 2)
									{
										goto IL_2352;
									}
									this.StartAnimInfo = this.lstAnimInfo[num57][num58];
									break;
								}
								IL_2352:;
							}
						}
						if (this.StartAnimInfo != null)
						{
							break;
						}
					}
					IL_237D:;
				}
				break;
			}
		}
		else
		{
			for (int num59 = 0; num59 < list.Count; num59++)
			{
				if (list[num59].Item2 == this.hSceneManager.height)
				{
					int id = list[num59].Item3.id;
					int num60 = list[num59].Item3.mode;
					for (int num61 = 0; num61 < this.lstAnimInfo[num60].Count; num61++)
					{
						if (id == this.lstAnimInfo[num60][num61].id)
						{
							this.StartAnimInfo = this.lstAnimInfo[num60][num61];
							break;
						}
					}
					if (this.StartAnimInfo != null)
					{
						break;
					}
				}
			}
		}
		if (this.StartAnimInfo == null)
		{
			int num62 = (mode == -1) ? 0 : mode;
			for (int num63 = 0; num63 < this.lstAnimInfo[num62].Count; num63++)
			{
				int index = num63;
				if (this.lstAnimInfo[num62][index].nPositons.Contains(this.hSceneManager.height))
				{
					this.StartAnimInfo = this.lstAnimInfo[num62][index];
					break;
				}
			}
			if (this.StartAnimInfo == null)
			{
				this.StartAnimInfo = this.lstAnimInfo[num62][0];
			}
		}
		this.ChangeStartAnimation(hEvent);
	}

	// Token: 0x060050FC RID: 20732 RVA: 0x00201168 File Offset: 0x001FF568
	private void ChangeStartAnimation(HSceneManager.HEvent hEvent)
	{
		if (Singleton<Voice>.Instance.IsVoiceCheck(this.ctrlFlag.voice.voiceTrs[0], true))
		{
			Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[0]);
		}
		bool flag = hEvent == HSceneManager.HEvent.Normal;
		this.fade.FadeStart(1f);
		Observable.FromCoroutine(() => this.StartAnim(this.StartAnimInfo, 0, hEvent), false).Finally(delegate
		{
			this.ctrlFlag.voice.playStart = 1;
			GlobalMethod.setCameraMoveFlag(this.ctrlFlag.cameraCtrl, true);
		}).Subscribe<Unit>();
		if (this.hSceneManager.Player.CameraControl.HCamera == null)
		{
			this.hSceneManager.Player.CameraControl.HCamera = this.ctrlFlag.cameraCtrl;
		}
		if (flag)
		{
			this.hSceneManager.Player.CameraControl.Mode = CameraMode.H;
			this.ctrlFlag.HBeforeCamera.gameObject.SetActive(false);
		}
		Singleton<HPointCtrl>.Instance.HEnterCategory = this.StartAnimInfo.nAnimListInfoID;
	}

	// Token: 0x060050FD RID: 20733 RVA: 0x00201294 File Offset: 0x001FF694
	public void SetStartAnimationInfoM(HSceneManager.HEvent hEvent)
	{
		List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> lstStartAnimInfoM = Singleton<Manager.Resources>.Instance.HSceneTable.lstStartAnimInfoM;
		this.StartAnimInfo = null;
		if (this.hSceneManager.MerchantLimit == 0)
		{
			return;
		}
		List<HScene.AnimationListInfo> list = new List<HScene.AnimationListInfo>();
		for (int i = 0; i < lstStartAnimInfoM.Count; i++)
		{
			int num = lstStartAnimInfoM[i].Item3.mode;
			int id = lstStartAnimInfoM[i].Item3.id;
			if (this.hSceneManager.MerchantLimit >= 1)
			{
				if (lstStartAnimInfoM[i].Item1 == HSceneManager.HEvent.Normal && lstStartAnimInfoM[i].Item2 == this.hSceneManager.height)
				{
					if (this.hSceneManager.Player.ChaControl.sex == 1 && !this.hSceneManager.bFutanari)
					{
						if (num != 4)
						{
							goto IL_192;
						}
					}
					else
					{
						if (this.hSceneManager.MerchantLimit == 1 && num != 1)
						{
							goto IL_192;
						}
						if (this.hSceneManager.Agent[1] == null && num == 5)
						{
							goto IL_192;
						}
						if (num == 4)
						{
							goto IL_192;
						}
					}
					for (int j = 0; j < this.lstAnimInfo[num].Count; j++)
					{
						if (id == this.lstAnimInfo[num][j].id)
						{
							list.Add(this.lstAnimInfo[num][j]);
						}
					}
				}
			}
			IL_192:;
		}
		ShuffleRand shuffleRand = new ShuffleRand(-1);
		shuffleRand.Init(list.Count);
		int index = shuffleRand.Get();
		this.StartAnimInfo = list[index];
		Observable.FromCoroutine(() => this.StartAnim(this.StartAnimInfo, 1, HSceneManager.HEvent.Normal), false).Finally(delegate
		{
			GlobalMethod.setCameraMoveFlag(this.ctrlFlag.cameraCtrl, true);
		}).Subscribe<Unit>();
		Singleton<HPointCtrl>.Instance.HEnterCategory = this.StartAnimInfo.nAnimListInfoID;
	}

	// Token: 0x060050FE RID: 20734 RVA: 0x002014B0 File Offset: 0x001FF8B0
	private IEnumerator StartAnim(HScene.AnimationListInfo StartAnimInfo, int mode, HSceneManager.HEvent hEvent = HSceneManager.HEvent.Normal)
	{
		while (!this.sprite.enabled)
		{
			yield return null;
		}
		if (mode == 0)
		{
			bool camChange = hEvent == HSceneManager.HEvent.Normal;
			this.ctrlFlag.selectAnimationListInfo = StartAnimInfo;
			yield return this.ChangeAnimation(StartAnimInfo, camChange, false, false);
		}
		else if (mode == 1)
		{
			this.ctrlFlag.selectAnimationListInfo = StartAnimInfo;
			yield return this.ChangeAnimation(StartAnimInfo, false, false, true);
		}
		yield break;
	}

	// Token: 0x060050FF RID: 20735 RVA: 0x002014E0 File Offset: 0x001FF8E0
	private void EndProc()
	{
		this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.None;
		this.NowStateIsEnd = true;
		this.sprite.ReSetLight();
		this.Camera.EnableUpdateCustomLight();
		Singleton<Sound>.Instance.Listener = Singleton<Manager.Map>.Instance.Player.CameraControl.transform;
		Singleton<Manager.Resources>.Instance.HSceneTable.HMeshObjDic[Singleton<Manager.Map>.Instance.MapID].SetActive(true);
		GameObject cameraMesh = this.hSceneManager.CameraMesh;
		if (cameraMesh != null)
		{
			cameraMesh.SetActive(true);
		}
		this.EndAnimChange();
		this.hSceneManager.endStatus = ((!this.ctrlFlag.isFaintness) ? 0 : 1);
		this.chaFemales[0].SetClothesStateAll(0);
		this.chaFemales[0].SetAccessoryStateAll(true);
		for (int i = 0; i < 5; i++)
		{
			ChaFileDefine.SiruParts parts = (ChaFileDefine.SiruParts)i;
			this.chaFemales[0].SetSiruFlag(parts, 0);
		}
		if (this.ctrlEyeNeckFemale[0] != null && this.chaFemales[0] != null && this.chaFemales[0].objBodyBone != null)
		{
			this.ctrlEyeNeckFemale[0].NowEndADV = true;
		}
		if (this.ctrlEyeNeckFemale[1] != null && this.chaFemales[1] != null && this.chaFemales[1].objBodyBone != null)
		{
			if (this.hSceneManager.Player.ChaControl.sex == 0)
			{
				this.ctrlEyeNeckFemale[1].NowEndADV = true;
			}
			else if (this.hSceneManager.Player.ChaControl.sex == 1)
			{
				if (this.ctrlFlag.bFutanari)
				{
					this.ctrlEyeNeckFemale[1].NowEndADV = true;
				}
				else
				{
					this.hMotionEyeNeckLesP.NowEndADV = true;
				}
			}
		}
		if (this.ctrlEyeNeckMale[0] != null && this.chaMales[0] != null && this.chaMales[0].objBodyBone != null)
		{
			this.ctrlEyeNeckMale[0].NowEndADV = true;
		}
		if (this.ctrlEyeNeckMale[1] != null && this.chaMales[1] != null && this.chaMales[1].objBodyBone != null)
		{
			this.ctrlEyeNeckMale[1].NowEndADV = true;
		}
		foreach (AgentActor agentActor in Singleton<Manager.Map>.Instance.AgentTable.Values)
		{
			if (!(agentActor == null))
			{
				if (this.hSceneManager.ReturnActionTypes.ContainsKey(agentActor))
				{
					agentActor.EnableEntity();
				}
			}
		}
		MerchantActor merchant = Singleton<Manager.Map>.Instance.Merchant;
		if (this.hSceneManager.bMerchant)
		{
			if (!merchant.ChaControl.neckLookCtrl.enabled)
			{
				merchant.ChaControl.neckLookCtrl.enabled = true;
			}
			if (!merchant.ChaControl.eyeLookCtrl.enabled)
			{
				merchant.ChaControl.eyeLookCtrl.enabled = true;
			}
			merchant.ChaControl.ChangeLookNeckPtn(3, 1f);
			merchant.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
			merchant.ChaControl.ChangeLookEyesPtn(0);
			merchant.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
			Game.Expression expression = Singleton<Game>.Instance.GetExpression(merchant.ID, "標準");
			if (expression != null)
			{
				expression.Change(merchant.ChaControl);
			}
			merchant.ChaControl.ChangeMouthOpenMin(merchant.ChaControl.fileStatus.mouthOpenMin);
		}
		int shortNameHash = merchant.ChaControl.animBody.GetCurrentAnimatorStateInfo(0).shortNameHash;
		merchant.AnimationMerchant.SetAnimatorController(merchant.ChaControl.animBody.runtimeAnimatorController);
		merchant.AnimationMerchant.MapIK.Calc(shortNameHash);
		merchant.EnableEntity();
		if (this.hSceneManager.male != null)
		{
			if (!this.hSceneManager.male.ChaControl.neckLookCtrl.enabled)
			{
				this.hSceneManager.male.ChaControl.neckLookCtrl.enabled = true;
			}
			if (!this.hSceneManager.male.ChaControl.eyeLookCtrl.enabled)
			{
				this.hSceneManager.male.ChaControl.eyeLookCtrl.enabled = true;
			}
			this.hSceneManager.male.ChaControl.ChangeLookNeckPtn(3, 1f);
			this.hSceneManager.male.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
			this.hSceneManager.male.ChaControl.ChangeLookEyesPtn(0);
			this.hSceneManager.male.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
			Game.Expression expression2 = Singleton<Game>.Instance.GetExpression(this.hSceneManager.male.ID, "標準");
			if (expression2 != null)
			{
				expression2.Change(this.hSceneManager.male.ChaControl);
			}
			this.hSceneManager.male.ChaControl.ChangeMouthOpenMin(this.hSceneManager.male.ChaControl.fileStatus.mouthOpenMin);
		}
		if (this.hSceneManager.females != null)
		{
			for (int j = 0; j < this.hSceneManager.females.Length; j++)
			{
				if (!(this.hSceneManager.females[j] == null))
				{
					if (!this.hSceneManager.females[j].ChaControl.neckLookCtrl.enabled)
					{
						this.hSceneManager.females[j].ChaControl.neckLookCtrl.enabled = true;
					}
					if (!this.hSceneManager.females[j].ChaControl.eyeLookCtrl.enabled)
					{
						this.hSceneManager.females[j].ChaControl.eyeLookCtrl.enabled = true;
					}
					this.hSceneManager.females[j].ChaControl.ChangeLookNeckPtn(3, 1f);
					this.hSceneManager.females[j].ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					this.hSceneManager.females[j].ChaControl.ChangeLookEyesPtn(0);
					this.hSceneManager.females[j].ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					int personality = 0;
					if (this.hSceneManager.females[j].GetComponent<PlayerActor>())
					{
						personality = -100;
					}
					else if (this.hSceneManager.females[j].GetComponent<MerchantActor>() == null)
					{
						personality = this.hSceneManager.females[j].ChaControl.fileParam.personality;
					}
					Game.Expression expression3 = Singleton<Game>.Instance.GetExpression(personality, "標準");
					if (expression3 != null)
					{
						expression3.Change(this.hSceneManager.females[j].ChaControl);
					}
					this.hSceneManager.females[j].ChaControl.ChangeMouthOpenMin(this.hSceneManager.females[j].ChaControl.fileStatus.mouthOpenMin);
				}
			}
		}
		for (int k = 0; k < this.chaMales.Length; k++)
		{
			if (!(this.chaMales[k] == null) && !(this.chaMales[k].objBody == null))
			{
				this.chaMales[k].visibleSon = false;
			}
		}
		this.ctrlFlag.cameraCtrl.Reset(0);
		this.Camera.enabled = true;
		this.Camera.EnabledInput = true;
		this.Camera.Mode = CameraMode.Normal;
		AnimalBase.CreateDisplay = true;
		AnimalManager instance = Singleton<AnimalManager>.Instance;
		int index = 0;
		for (int l = 0; l < instance.Animals.Count; l++)
		{
			instance.Animals[index].BodyEnabled = true;
			instance.Animals[index++].enabled = true;
		}
		instance.SettingAnimalPointBehavior();
		this.ctrlFlag.selectAnimationListInfo = null;
		if (this.ctrlItem != null)
		{
			this.ctrlItem.ReleaseItem();
		}
		this.ctrlFlag.cameraCtrl.visibleForceVanish(true);
		this.ctrlMeta.Clear();
		this.hPointCtrl.MarkerObjDel();
		this.hPointCtrl.ExistSecondfemale = false;
		if (this.ctrlFlag.nowHPoint.endPlayerPos != null)
		{
			this.hSceneManager.Player.ActivateNavMeshAgent();
			this.hSceneManager.Player.NavMeshAgent.Warp(this.ctrlFlag.nowHPoint.endPlayerPos.position);
			this.hSceneManager.Player.ChaControl.transform.position = Vector3.zero;
			this.hSceneManager.Player.ChaControl.animBody.transform.position = this.hSceneManager.Player.NavMeshAgent.transform.position;
			this.hSceneManager.Player.Rotation = this.ctrlFlag.nowHPoint.endPlayerPos.rotation;
		}
		else
		{
			this.hSceneManager.Player.ActivateNavMeshAgent();
			this.hSceneManager.Player.NavMeshAgent.Warp(this.ctrlFlag.nowHPoint.transform.position);
			this.hSceneManager.Player.ChaControl.transform.position = Vector3.zero;
			this.hSceneManager.Player.ChaControl.animBody.transform.position = this.hSceneManager.Player.NavMeshAgent.transform.position;
			this.hSceneManager.Player.Rotation = this.ctrlFlag.nowHPoint.transform.rotation;
		}
		bool flag = this.ctrlFlag.nowHPoint.endFemalePos != null;
		if (flag)
		{
			if (!this.hSceneManager.bMerchant)
			{
				for (int m = 0; m < 2; m++)
				{
					if (!(this.hSceneManager.females[m] == null))
					{
						if (!(this.hSceneManager.females[m].GetComponent<PlayerActor>() != null))
						{
							if (m > 0 && this.ctrlFlag.nowHPoint.endFemalePos.Length <= 1)
							{
								this.hSceneManager.females[m].ActivateNavMeshAgent();
								this.hSceneManager.females[m].NavMeshAgent.Warp(this.ctrlFlag.nowHPoint.transform.position);
								this.hSceneManager.females[m].ChaControl.transform.position = Vector3.zero;
								this.hSceneManager.females[m].ChaControl.animBody.transform.position = this.hSceneManager.females[m].NavMeshAgent.transform.position;
								this.hSceneManager.females[m].Rotation = this.ctrlFlag.nowHPoint.transform.rotation;
							}
							else
							{
								if (this.ctrlFlag.nowHPoint.endFemalePos[m] != null)
								{
									this.hSceneManager.females[m].ActivateNavMeshAgent();
									this.hSceneManager.females[m].NavMeshAgent.Warp(this.ctrlFlag.nowHPoint.endFemalePos[m].position);
									this.hSceneManager.females[m].ChaControl.transform.position = Vector3.zero;
									this.hSceneManager.females[m].ChaControl.animBody.transform.position = this.hSceneManager.females[m].NavMeshAgent.transform.position;
									this.hSceneManager.females[m].Rotation = this.ctrlFlag.nowHPoint.endFemalePos[m].rotation;
								}
								else
								{
									this.hSceneManager.females[m].ActivateNavMeshAgent();
									this.hSceneManager.females[m].NavMeshAgent.Warp(this.ctrlFlag.nowHPoint.transform.position);
									this.hSceneManager.females[m].ChaControl.transform.position = Vector3.zero;
									this.hSceneManager.females[m].ChaControl.animBody.transform.position = this.hSceneManager.females[m].NavMeshAgent.transform.position;
									this.hSceneManager.females[m].Rotation = this.ctrlFlag.nowHPoint.transform.rotation;
								}
								this.hSceneManager.females[m].Locomotor.transform.LookAt(this.hSceneManager.Player.Locomotor.transform);
							}
						}
					}
				}
			}
			else
			{
				if (this.ctrlFlag.nowHPoint.endFemalePos[0] != null)
				{
					this.hSceneManager.merchantActor.ActivateNavMeshAgent();
					this.hSceneManager.merchantActor.NavMeshAgent.Warp(this.ctrlFlag.nowHPoint.endFemalePos[0].position);
					this.hSceneManager.merchantActor.ChaControl.transform.position = Vector3.zero;
					this.hSceneManager.merchantActor.ChaControl.animBody.transform.position = this.hSceneManager.merchantActor.NavMeshAgent.transform.position;
					this.hSceneManager.merchantActor.Rotation = this.ctrlFlag.nowHPoint.endFemalePos[0].rotation;
				}
				else
				{
					this.hSceneManager.merchantActor.ActivateNavMeshAgent();
					this.hSceneManager.merchantActor.NavMeshAgent.Warp(this.ctrlFlag.nowHPoint.transform.position);
					this.hSceneManager.merchantActor.ChaControl.transform.position = Vector3.zero;
					this.hSceneManager.merchantActor.ChaControl.animBody.transform.position = this.hSceneManager.merchantActor.NavMeshAgent.transform.position;
					this.hSceneManager.merchantActor.Rotation = this.ctrlFlag.nowHPoint.transform.rotation;
				}
				if (this.hSceneManager.Agent[1] != null)
				{
					if (this.ctrlFlag.nowHPoint.endFemalePos[1] != null && this.ctrlFlag.nowHPoint.endFemalePos.Length > 1)
					{
						this.hSceneManager.Agent[1].ActivateNavMeshAgent();
						this.hSceneManager.Agent[1].NavMeshAgent.Warp(this.ctrlFlag.nowHPoint.endFemalePos[1].position);
						this.hSceneManager.Agent[1].ChaControl.transform.position = Vector3.zero;
						this.hSceneManager.Agent[1].ChaControl.animBody.transform.position = this.hSceneManager.Agent[1].NavMeshAgent.transform.position;
						this.hSceneManager.Agent[1].Rotation = this.ctrlFlag.nowHPoint.endFemalePos[1].rotation;
					}
					else
					{
						this.hSceneManager.Agent[1].ActivateNavMeshAgent();
						this.hSceneManager.Agent[1].NavMeshAgent.Warp(this.ctrlFlag.nowHPoint.transform.position);
						this.hSceneManager.Agent[1].ChaControl.transform.position = Vector3.zero;
						this.hSceneManager.Agent[1].ChaControl.animBody.transform.position = this.hSceneManager.Agent[1].NavMeshAgent.transform.position;
						this.hSceneManager.Agent[1].Rotation = this.ctrlFlag.nowHPoint.transform.rotation;
					}
				}
				this.hSceneManager.merchantActor.Locomotor.transform.LookAt(this.hSceneManager.Player.Locomotor.transform);
				if (this.hSceneManager.Agent[1] != null)
				{
					this.hSceneManager.Agent[1].Locomotor.transform.LookAt(this.hSceneManager.Player.Locomotor.transform);
				}
			}
		}
		this.hSceneManager.Player.ChaControl.visibleAll = false;
		if (!this.hSceneManager.bMerchant)
		{
			if (this.ctrlFlag.numOrgasmTotal < this.ctrlFlag.gotoFaintnessCount)
			{
				if (!this.hSceneManager.isForce)
				{
					this.ctrlFlag.AddParam(3, 0);
				}
				else
				{
					this.ctrlFlag.AddParam(5, 0);
					if (this.hSceneManager.HSkil.ContainsValue(9))
					{
						this.ctrlFlag.AddSkileParam(9);
					}
				}
			}
			if (this.ctrlFlag.numOutSide > 0)
			{
				this.ctrlFlag.AddParam(11, 1);
				if (this.hSceneManager.HSkil.ContainsValue(8))
				{
					this.ctrlFlag.AddSkileParam(8);
				}
			}
			if (this.ctrlFlag.numInside > 0)
			{
				this.ctrlFlag.AddParam(12, 1);
				if (this.hSceneManager.HSkil.ContainsValue(5))
				{
					this.ctrlFlag.AddSkileParam(5);
				}
			}
			if (this.ctrlFlag.numSameOrgasm > 0)
			{
				this.ctrlFlag.AddParam(13, 1);
				if (this.hSceneManager.HSkil.ContainsValue(2))
				{
					this.ctrlFlag.AddSkileParam(2);
				}
			}
			if (this.ctrlFlag.numAibu > 0)
			{
				this.ctrlFlag.AddParam(0, 1);
			}
			if (this.ctrlFlag.numHoushi > 0)
			{
				this.ctrlFlag.AddParam(1, 1);
				if (this.hSceneManager.HSkil.ContainsValue(4))
				{
					this.ctrlFlag.AddSkileParam(4);
				}
			}
			if (this.ctrlFlag.numSonyu > 0)
			{
				this.ctrlFlag.AddParam(2, 1);
			}
			if (this.ctrlFlag.numLes > 0)
			{
				this.ctrlFlag.AddParam(7, 1);
				if (this.hSceneManager.HSkil.ContainsValue(12))
				{
					this.ctrlFlag.AddSkileParam(12);
				}
			}
			if (this.ctrlFlag.numUrine > 0 && this.hSceneManager.HSkil.ContainsValue(14))
			{
				this.ctrlFlag.AddSkileParam(14);
			}
			if (this.ctrlFlag.numLeadFemale > 0)
			{
				this.ctrlFlag.AddParam(4, 1);
				if (this.hSceneManager.HSkil.ContainsValue(10))
				{
					this.ctrlFlag.AddSkileParam(10);
				}
			}
			if (this.ctrlFlag.isPainActionParam)
			{
				this.ctrlFlag.AddParam(3, 1);
			}
			if (this.hSceneManager.isForce)
			{
				this.ctrlFlag.AddParam(5, 1);
			}
			if (this.ctrlFlag.numFaintness > 0)
			{
				this.ctrlFlag.AddParam(14, 1);
				if (this.hSceneManager.HSkil.ContainsValue(20))
				{
					this.ctrlFlag.AddSkileParam(20);
				}
			}
			if (this.ctrlFlag.numOrgasmTotal <= 0 && this.hSceneManager.HSkil.ContainsValue(21))
			{
				this.ctrlFlag.AddSkileParam(21);
			}
			if (this.ctrlFlag.isNotCtrl)
			{
				this.ctrlFlag.AddParam(16, 1);
			}
			if (this.ctrlFlag.isFemaleNaked)
			{
				this.ctrlFlag.AddParam(34, 1);
			}
			foreach (KeyValuePair<int, int> keyValuePair in this.ctrlFlag.ChangeParams)
			{
				this.hSceneManager.SetParamator(keyValuePair.Key, keyValuePair.Value);
			}
		}
		this.hSceneManager.maleFinish = this.ctrlFlag.numInside + this.ctrlFlag.numOutSide + this.ctrlFlag.numDrink + this.ctrlFlag.numVomit;
		this.hSceneManager.femalePlayerFinish = this.ctrlFlag.numOrgasmFemalePlayer;
		this.hSceneManager.femaleFinish = this.ctrlFlag.numOrgasmTotal;
		this.hSceneManager.endStatus = ((!this.ctrlFlag.isFaintness) ? 0 : 1);
		this.hSceneManager.isCtrl = !this.ctrlFlag.isNotCtrl;
		this.EndProcADV();
		this.hSceneManager.merchantActor = null;
	}

	// Token: 0x06005100 RID: 20736 RVA: 0x00202BE8 File Offset: 0x00200FE8
	private void LateUpdate()
	{
		this.LotionProc();
		if (this.ctrlFlag.BeforeHWait)
		{
			if (this.preBeforWaitState != null && this.preBeforWaitState.Count > 0)
			{
				if (this.preBeforWaitState.ContainsKey(0))
				{
					int shortNameHash = this.hSceneManager.Player.ChaControl.animBody.GetCurrentAnimatorStateInfo(0).shortNameHash;
					if (this.preBeforWaitState[0] != shortNameHash)
					{
						this.hSceneManager.Player.Animation.MapIK.Calc(shortNameHash);
						this.preBeforWaitState[0] = shortNameHash;
					}
				}
				for (int i = 0; i < this.hSceneManager.Agent.Length; i++)
				{
					if (!(this.hSceneManager.Agent[i] == null) && !(this.hSceneManager.Agent[i].ChaControl == null) && !(this.hSceneManager.Agent[i].ChaControl.animBody == null))
					{
						if (this.preBeforWaitState.ContainsKey(1 + i))
						{
							int shortNameHash = this.hSceneManager.Agent[i].ChaControl.animBody.GetCurrentAnimatorStateInfo(0).shortNameHash;
							if (this.preBeforWaitState[1 + i] != shortNameHash)
							{
								this.hSceneManager.Agent[i].Animation.MapIK.Calc(shortNameHash);
								this.preBeforWaitState[1 + i] = shortNameHash;
							}
						}
					}
				}
			}
			this.prevBeforeWait = this.ctrlFlag.BeforeHWait;
			return;
		}
		if (this.prevBeforeWait && !this.ctrlFlag.BeforeHWait)
		{
			this.sprite.endFade = 0;
			this.prevBeforeWait = this.ctrlFlag.BeforeHWait;
		}
		if (!ProcBase.endInit)
		{
			return;
		}
		bool flag = Singleton<Game>.Instance.Dialog != null;
		bool activeSelf = this.hSceneManager.HSceneUISet.activeSelf;
		if (this.NowStateIsEnd)
		{
			if (!flag && activeSelf)
			{
				this.hSceneManager.HSceneUISet.SetActive(false);
			}
			return;
		}
		if (this.chaFemales[0].GetNowClothesType() == 3)
		{
			this.ctrlFlag.isFemaleNaked = true;
		}
		HSystem hdata = Config.HData;
		foreach (int clothesKind in new List<int>
		{
			0,
			2,
			1,
			3,
			5,
			6
		})
		{
			if (this.hSceneManager.Player.ChaControl.IsClothesStateKind(clothesKind))
			{
				byte state = 0;
				if (!hdata.Cloth)
				{
					state = 2;
				}
				this.hSceneManager.Player.ChaControl.SetClothesState(clothesKind, state, true);
			}
		}
		this.hSceneManager.Player.ChaControl.SetAccessoryStateAll(hdata.Accessory);
		this.hSceneManager.Player.ChaControl.SetClothesState(7, (!hdata.Shoes) ? 2 : 0, true);
		this.ctrlFlag.semenType = Config.HData.Siru;
		if (this.Camera != null)
		{
			this.Camera.AmbientLight = Config.GraphicData.AmbientLight;
		}
		this.ctrlFlag.cameraCtrl.ConfigVanish = Config.GraphicData.Shield;
		this.SyncAnimation();
	}

	// Token: 0x06005101 RID: 20737 RVA: 0x00202FB8 File Offset: 0x002013B8
	private void LotionProc()
	{
		if (this.useLotion)
		{
			float wetness = 100f;
			for (int i = 0; i < this.hSceneManager.Agent.Length; i++)
			{
				if (!(this.hSceneManager.Agent[i] == null))
				{
					AgentData agentData = this.hSceneManager.Agent[i].AgentData;
					if (agentData != null)
					{
						agentData.Wetness = wetness;
						float wetRate = Mathf.InverseLerp(0f, 100f, agentData.Wetness);
						this.hSceneManager.Agent[i].ChaControl.wetRate = wetRate;
					}
				}
			}
			if (this.hSceneManager.bMerchant && this.hSceneManager.merchantActor != null)
			{
				MerchantData merchantData = this.hSceneManager.merchantActor.MerchantData;
				if (merchantData != null)
				{
					merchantData.Wetness = wetness;
					float wetRate2 = Mathf.InverseLerp(0f, 100f, merchantData.Wetness);
					this.hSceneManager.merchantActor.ChaControl.wetRate = wetRate2;
				}
			}
		}
	}

	// Token: 0x06005102 RID: 20738 RVA: 0x002030E0 File Offset: 0x002014E0
	public void ConfigEnd()
	{
		this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.None;
		this.NowStateIsEnd = true;
		this.sprite.ReSetLight();
		this.Camera.EnableUpdateCustomLight();
		Singleton<Sound>.Instance.Listener = Singleton<Manager.Map>.Instance.Player.CameraControl.transform;
		Singleton<Manager.Resources>.Instance.HSceneTable.HMeshObjDic[Singleton<Manager.Map>.Instance.MapID].SetActive(true);
		GameObject cameraMesh = this.hSceneManager.CameraMesh;
		if (cameraMesh != null)
		{
			cameraMesh.SetActive(true);
		}
		this.EndAnimChange();
		this.hSceneManager.endStatus = ((!this.ctrlFlag.isFaintness) ? 0 : 1);
		this.chaFemales[0].SetClothesStateAll(0);
		if (this.ctrlEyeNeckFemale[0] != null && this.chaFemales[0] != null && this.chaFemales[0].objBodyBone != null)
		{
			this.ctrlEyeNeckFemale[0].NowEndADV = true;
		}
		if (this.ctrlEyeNeckFemale[1] != null && this.chaFemales[1] != null && this.chaFemales[1].objBodyBone != null)
		{
			if (this.hSceneManager.Player.ChaControl.sex == 0)
			{
				this.ctrlEyeNeckFemale[1].NowEndADV = true;
			}
			else if (this.hSceneManager.Player.ChaControl.sex == 1)
			{
				if (this.ctrlFlag.bFutanari)
				{
					this.ctrlEyeNeckFemale[1].NowEndADV = true;
				}
				else
				{
					this.hMotionEyeNeckLesP.NowEndADV = true;
				}
			}
		}
		if (this.ctrlEyeNeckMale[0] != null && this.chaMales[0] != null && this.chaMales[0].objBodyBone != null)
		{
			this.ctrlEyeNeckMale[0].NowEndADV = true;
		}
		if (this.ctrlEyeNeckMale[1] != null && this.chaMales[1] != null && this.chaMales[1].objBodyBone != null)
		{
			this.ctrlEyeNeckMale[1].NowEndADV = true;
		}
		foreach (AgentActor agentActor in Singleton<Manager.Map>.Instance.AgentTable.Values)
		{
			if (!(agentActor == null))
			{
				if (this.hSceneManager.ReturnActionTypes.ContainsKey(agentActor))
				{
					agentActor.EnableEntity();
				}
			}
		}
		MerchantActor merchant = Singleton<Manager.Map>.Instance.Merchant;
		if (this.hSceneManager.bMerchant)
		{
			if (!merchant.ChaControl.neckLookCtrl.enabled)
			{
				merchant.ChaControl.neckLookCtrl.enabled = true;
			}
			if (!merchant.ChaControl.eyeLookCtrl.enabled)
			{
				merchant.ChaControl.eyeLookCtrl.enabled = true;
			}
			merchant.ChaControl.ChangeLookNeckPtn(3, 1f);
			merchant.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
			merchant.ChaControl.ChangeLookEyesPtn(0);
			merchant.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
			Game.Expression expression = Singleton<Game>.Instance.GetExpression(merchant.ID, "標準");
			if (expression != null)
			{
				expression.Change(merchant.ChaControl);
			}
			merchant.ChaControl.ChangeMouthOpenMin(merchant.ChaControl.fileStatus.mouthOpenMin);
		}
		int shortNameHash = merchant.ChaControl.animBody.GetCurrentAnimatorStateInfo(0).shortNameHash;
		merchant.AnimationMerchant.SetAnimatorController(merchant.ChaControl.animBody.runtimeAnimatorController);
		merchant.AnimationMerchant.MapIK.Calc(shortNameHash);
		merchant.EnableEntity();
		if (this.hSceneManager.male != null)
		{
			if (!this.hSceneManager.male.ChaControl.neckLookCtrl.enabled)
			{
				this.hSceneManager.male.ChaControl.neckLookCtrl.enabled = true;
			}
			if (!this.hSceneManager.male.ChaControl.eyeLookCtrl.enabled)
			{
				this.hSceneManager.male.ChaControl.eyeLookCtrl.enabled = true;
			}
			this.hSceneManager.male.ChaControl.ChangeLookNeckPtn(3, 1f);
			this.hSceneManager.male.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
			this.hSceneManager.male.ChaControl.ChangeLookEyesPtn(0);
			this.hSceneManager.male.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
			Game.Expression expression2 = Singleton<Game>.Instance.GetExpression(this.hSceneManager.male.ID, "標準");
			if (expression2 != null)
			{
				expression2.Change(this.hSceneManager.male.ChaControl);
			}
			this.hSceneManager.male.ChaControl.ChangeMouthOpenMin(this.hSceneManager.male.ChaControl.fileStatus.mouthOpenMin);
		}
		if (this.hSceneManager.females != null)
		{
			for (int i = 0; i < this.hSceneManager.females.Length; i++)
			{
				if (!(this.hSceneManager.females[i] == null))
				{
					if (!this.hSceneManager.females[i].ChaControl.neckLookCtrl.enabled)
					{
						this.hSceneManager.females[i].ChaControl.neckLookCtrl.enabled = true;
					}
					if (!this.hSceneManager.females[i].ChaControl.eyeLookCtrl.enabled)
					{
						this.hSceneManager.females[i].ChaControl.eyeLookCtrl.enabled = true;
					}
					this.hSceneManager.females[i].ChaControl.ChangeLookNeckPtn(3, 1f);
					this.hSceneManager.females[i].ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
					this.hSceneManager.females[i].ChaControl.ChangeLookEyesPtn(0);
					this.hSceneManager.females[i].ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
					int personality = 0;
					if (this.hSceneManager.females[i].GetComponent<PlayerActor>())
					{
						personality = -100;
					}
					else if (this.hSceneManager.females[i].GetComponent<MerchantActor>() == null)
					{
						personality = this.hSceneManager.females[i].ChaControl.fileParam.personality;
					}
					Game.Expression expression3 = Singleton<Game>.Instance.GetExpression(personality, "標準");
					if (expression3 != null)
					{
						expression3.Change(this.hSceneManager.females[i].ChaControl);
					}
					this.hSceneManager.females[i].ChaControl.ChangeMouthOpenMin(this.hSceneManager.females[i].ChaControl.fileStatus.mouthOpenMin);
				}
			}
		}
		for (int j = 0; j < this.chaMales.Length; j++)
		{
			if (!(this.chaMales[j] == null) && !(this.chaMales[j].objBody == null))
			{
				this.chaMales[j].visibleSon = false;
			}
		}
		this.ctrlFlag.cameraCtrl.Reset(0);
		this.Camera.enabled = true;
		this.Camera.EnabledInput = true;
		this.Camera.Mode = CameraMode.Normal;
		AnimalBase.CreateDisplay = true;
		AnimalManager instance = Singleton<AnimalManager>.Instance;
		int index = 0;
		for (int k = 0; k < instance.Animals.Count; k++)
		{
			instance.Animals[index].BodyEnabled = true;
			instance.Animals[index++].enabled = true;
		}
		instance.SettingAnimalPointBehavior();
		this.ctrlFlag.selectAnimationListInfo = null;
		if (this.ctrlItem != null)
		{
			this.ctrlItem.ReleaseItem();
		}
		this.ctrlFlag.cameraCtrl.visibleForceVanish(true);
		this.ctrlMeta.Clear();
		this.hPointCtrl.MarkerObjDel();
		this.hPointCtrl.ExistSecondfemale = false;
		this.CloseADV();
		this.hSceneManager.HSceneUISet.SetActive(false);
		this.hSceneManager.merchantActor = null;
	}

	// Token: 0x06005103 RID: 20739 RVA: 0x00203A50 File Offset: 0x00201E50
	private void OnDisable()
	{
		if (this.nowStart)
		{
			return;
		}
		if (this.ctrlFlag && this.ctrlFlag.cameraCtrl)
		{
			this.ctrlFlag.cameraCtrl.visibleForceVanish(true);
			this.ctrlFlag.cameraCtrl.ResetVanish();
		}
		if (Singleton<Housing>.IsInstance())
		{
			Singleton<Housing>.Instance.EndShield();
		}
		this.hSceneManager.isForce = false;
		for (int i = 0; i < 2; i++)
		{
			if (this.ctrlDynamics[i] != null)
			{
				this.ctrlDynamics[i].InitDynamicBoneReferenceBone();
			}
		}
		if (this.ctrlVoice != null)
		{
			this.ctrlVoice.FaceReset(this.chaFemales[0]);
			if (this.chaFemales[1] != null)
			{
				this.ctrlVoice.FaceReset(this.chaFemales[1]);
			}
		}
		for (int j = 0; j < this.ctrlYures.Length; j++)
		{
			if (this.ctrlYures[j] != null)
			{
				this.ctrlYures[j].ResetShape();
			}
		}
		if (this.ctrlYureMale != null)
		{
			this.ctrlYureMale.ResetShape();
		}
		if (this.ctrlSiruPastes != null)
		{
			foreach (SiruPasteCtrl siruPasteCtrl in this.ctrlSiruPastes)
			{
				siruPasteCtrl.Release();
			}
		}
		foreach (ChaControl chaControl in this.chaFemales)
		{
			if (!(chaControl == null))
			{
				if (!(chaControl.objBody == null))
				{
					chaControl.ChangeBustInert(false);
					chaControl.playDynamicBoneBust(0, true);
					chaControl.playDynamicBoneBust(1, true);
					chaControl.fileStatus.skinTuyaRate = 0f;
					chaControl.ChangeEyesOpenMax(1f);
					FBSCtrlMouth mouthCtrl = chaControl.mouthCtrl;
					if (mouthCtrl != null)
					{
						mouthCtrl.OpenMin = 0f;
					}
					chaControl.SetAccessoryStateAll(true);
					chaControl.SetClothesStateAll(0);
					for (int m = 0; m < 5; m++)
					{
						ChaFileDefine.SiruParts parts = (ChaFileDefine.SiruParts)m;
						chaControl.SetSiruFlag(parts, 0);
					}
					chaControl.DisableShapeMouth(false);
					for (int n = 0; n < 7; n++)
					{
						int id = n;
						chaControl.DisableShapeBodyID(2, id, false);
					}
					chaControl.DisableShapeBodyID(2, 7, false);
					chaControl.ReleaseHitObject();
				}
			}
		}
		for (int num = 0; num < this.chaFemales.Length; num++)
		{
			if (!(this.chaFemales[num] == null))
			{
				if (!(this.chaFemales[num].objBody == null))
				{
					float rate = 0f;
					if (this.initStandNip.TryGetValue(num, out rate))
					{
						this.chaFemales[num].ChangeNipRate(rate);
					}
				}
			}
		}
		for (int num2 = 0; num2 < this.chaMales.Length; num2++)
		{
			if (!(this.chaMales[num2] == null))
			{
				if (!(this.chaMales[num2].objBody == null))
				{
					this.chaMales[num2].ReleaseHitObject();
				}
			}
		}
		this.initStandNip.Clear();
		if (this.CtrlParticle != null)
		{
			this.CtrlParticle.RePlaceObject();
		}
		for (int num3 = 0; num3 < this.ctrlHitObjectFemales.Length; num3++)
		{
			if (this.ctrlHitObjectFemales[num3] != null)
			{
				this.ctrlHitObjectFemales[num3].ReleaseObject();
			}
		}
		for (int num4 = 0; num4 < this.ctrlHitObjectMales.Length; num4++)
		{
			if (this.ctrlHitObjectMales[num4] != null)
			{
				this.ctrlHitObjectMales[num4].ReleaseObject();
			}
		}
		foreach (H_Lookat_dan h_Lookat_dan in this.ctrlLookAts)
		{
			h_Lookat_dan.Release();
		}
		if (this.objGrondCollision)
		{
			UnityEngine.Object.Destroy(this.objGrondCollision);
			this.objGrondCollision = null;
		}
		if (Singleton<Sound>.IsInstance())
		{
			Singleton<Sound>.Instance.Stop(Sound.Type.GameSE2D);
			Singleton<Sound>.Instance.Stop(Sound.Type.GameSE3D);
		}
		if (Singleton<Voice>.IsInstance())
		{
			Singleton<Voice>.Instance.StopAll(true);
		}
		this.ctrlVoice.HBeforeHouchiTime = 0f;
		this.hSceneManager.bMerchant = false;
		if (Singleton<GameCursor>.IsInstance())
		{
			Singleton<GameCursor>.Instance.SetCursorLock(false);
		}
		AssetBundleManager.UnloadAssetBundle(this.hSceneManager.strAssetSE, true, null, false);
		for (int num6 = 0; num6 < this.ctrlFlag.voice.lstUseAsset.Count; num6++)
		{
			AssetBundleManager.UnloadAssetBundle(this.ctrlFlag.voice.lstUseAsset[num6], true, null, false);
		}
		if (Singleton<HSceneManager>.IsInstance())
		{
			foreach (string assetBundleName in this.hSceneManager.hashUseAssetBundle)
			{
				AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
			}
			this.hSceneManager.hashUseAssetBundle.Clear();
		}
		this.ctrlLayer.Release();
		this.isSetStartPos = false;
		this.mode = -1;
		this.modeCtrl = -1;
		this.nowStart = false;
		this.nullPlayer = true;
		this.NowStateIsEnd = false;
		this.nowChangeAnim = false;
		this.lstMotionIK.Clear();
		Singleton<HPointCtrl>.Instance.endHScene();
		this.lstProc.Clear();
		this.packData = null;
		IDisposable choiceDisposable = this.hSceneManager.choiceDisposable;
		if (choiceDisposable != null)
		{
			choiceDisposable.Dispose();
		}
		this.hSceneManager.choiceDisposable = null;
		this.hSceneManager.nInvitePtn = -1;
		HSceneManager.SleepStart = false;
		this.hSceneManager.Agent[0] = null;
		this.hSceneManager.Agent[1] = null;
		this.useLotion = false;
		this.StartAnimInfo = null;
		if (this.hSceneManager.Player != null && this.hSceneManager.Player.ChaControl.sex == 1 && this.hSceneManager.bFutanari)
		{
			this.hSceneManager.Player.ChaControl.ChangeBustInert(false);
		}
		this.hSceneManager.numFemaleClothCustom = 0;
		this.hSceneManager.MerchantLimit = -1;
		this.hSceneManager.EndHScene();
		this.ctrlFlag.isJudgeSelect.Clear();
	}

	// Token: 0x06005104 RID: 20740 RVA: 0x0020413C File Offset: 0x0020253C
	public void HParticleSetNull()
	{
		this.ctrlParitcle = null;
	}

	// Token: 0x06005105 RID: 20741 RVA: 0x00204145 File Offset: 0x00202545
	private void CreateListAnimationFileName()
	{
		this.lstAnimInfo = Singleton<Manager.Resources>.Instance.HSceneTable.lstAnimInfo;
	}

	// Token: 0x06005106 RID: 20742 RVA: 0x0020415C File Offset: 0x0020255C
	private void SyncAnimation()
	{
		if (this.chaFemales[0].animBody == null)
		{
			return;
		}
		AnimatorStateInfo animatorStateInfo = this.chaFemales[0].getAnimatorStateInfo(0);
		List<int> list = this.ctrlFlag.lstSyncAnimLayers[0, 0];
		for (int i = 1; i < this.ctrlFlag.lstSyncAnimLayers.GetLength(0); i++)
		{
			for (int j = 1; j < this.ctrlFlag.lstSyncAnimLayers.GetLength(1); j++)
			{
				for (int k = 0; k < this.ctrlFlag.lstSyncAnimLayers[i, j].Count; k++)
				{
					if (!list.Contains(this.ctrlFlag.lstSyncAnimLayers[i, j][k]))
					{
						list.Add(this.ctrlFlag.lstSyncAnimLayers[i, j][k]);
					}
				}
			}
		}
		this.ctrlItem.ParentScaleReject();
		if (this.chaMales[0] == null || this.chaMales[0].animBody == null || this.chaMales[0].objBodyBone == null)
		{
			if (!this.chaFemales[0].isBlend(0) && this.ctrlItem.GetItem() != null)
			{
				this.ctrlItem.syncPlay(animatorStateInfo);
				this.ctrlItem.Update();
			}
			if (this.chaFemales[1])
			{
				this.chaFemales[1].syncPlay(animatorStateInfo, 0);
			}
			if (list.Count == 0)
			{
				this.isSyncFirstStep = false;
				return;
			}
			if (!this.isSyncFirstStep)
			{
				this.isSyncFirstStep = true;
				return;
			}
			for (int l = 0; l < this.chaFemales.Length; l++)
			{
				if (!(this.chaFemales[l] == null) && (!this.chaFemales[l] || !(this.chaFemales[l].animBody == null)))
				{
					for (int m = 0; m < this.ctrlFlag.lstSyncAnimLayers[1, l].Count; m++)
					{
						int num = this.ctrlFlag.lstSyncAnimLayers[1, l][m];
						if (this.chaFemales[l].animBody.layerCount > num)
						{
							this.chaFemales[l].syncPlay(animatorStateInfo, num);
						}
					}
				}
			}
			return;
		}
		else
		{
			if (this.chaFemales[1] != null && this.chaFemales[1].objTop != null)
			{
				this.chaFemales[1].syncPlay(animatorStateInfo, 0);
			}
			for (int n = 0; n < this.chaMales.Length; n++)
			{
				if (!(this.chaMales[n] == null) && !(this.chaMales[n].objTop == null))
				{
					for (int num2 = 0; num2 < this.ctrlFlag.lstSyncAnimLayers[0, n].Count; num2++)
					{
						int num3 = this.ctrlFlag.lstSyncAnimLayers[0, n][num2];
						if (this.chaMales[n].animBody.layerCount > num3)
						{
							this.chaMales[num2].syncPlay(animatorStateInfo, 0);
						}
					}
				}
			}
			if (this.ctrlItem.GetItem() != null)
			{
				this.ctrlItem.syncPlay(animatorStateInfo);
				this.ctrlItem.Update();
			}
			if (this.ctrlFlag.lstSyncAnimLayers[1, 0].Count == 0)
			{
				this.isSyncFirstStep = false;
				return;
			}
			if (!this.isSyncFirstStep)
			{
				this.isSyncFirstStep = true;
				return;
			}
			bool flag = false;
			bool flag2 = false;
			for (int num4 = 0; num4 < this.chaFemales.Length; num4++)
			{
				if (!(this.chaFemales[num4] == null) && (!this.chaFemales[num4] || !(this.chaFemales[num4].animBody == null)))
				{
					for (int num5 = 0; num5 < this.ctrlFlag.lstSyncAnimLayers[1, num4].Count; num5++)
					{
						int num6 = this.ctrlFlag.lstSyncAnimLayers[1, num4][num5];
						if (this.chaFemales[num4].animBody.layerCount > num6)
						{
							this.chaFemales[num4].syncPlay(animatorStateInfo, num6);
							flag = true;
						}
					}
				}
			}
			for (int num7 = 0; num7 < this.chaMales.Length; num7++)
			{
				if (!(this.chaMales[num7] == null) && (!this.chaMales[num7] || !(this.chaMales[num7].animBody == null)))
				{
					for (int num8 = 0; num8 < this.ctrlFlag.lstSyncAnimLayers[0, num7].Count; num8++)
					{
						int num9 = this.ctrlFlag.lstSyncAnimLayers[0, num7][num8];
						if (this.chaMales[num7].animBody.layerCount > num9)
						{
							this.chaMales[num7].syncPlay(animatorStateInfo, num9);
							flag2 = true;
						}
					}
				}
			}
			if (flag)
			{
				for (int num10 = 0; num10 < this.chaFemales.Length; num10++)
				{
					if (!(this.chaFemales[num10] == null) && (!this.chaFemales[num10] || !(this.chaFemales[num10].animBody == null)))
					{
						this.chaFemales[num10].animBody.Update(0f);
					}
				}
			}
			if (flag2)
			{
				for (int num11 = 0; num11 < this.chaMales.Length; num11++)
				{
					if (!(this.chaMales[num11] == null) && (!this.chaMales[num11] || !(this.chaMales[num11].animBody == null)))
					{
						this.chaMales[num11].animBody.Update(0f);
					}
				}
			}
			for (int num12 = 0; num12 < this.ctrlEyeNeckFemale.Length; num12++)
			{
				if (!(this.ctrlEyeNeckFemale[num12] == null))
				{
					bool flag3 = this.hSceneManager.Player.ChaControl.sex == 1 && !this.ctrlFlag.bFutanari;
					if (num12 == 0 || !flag3)
					{
						this.ctrlEyeNeckFemale[num12].EyeNeckCalc();
					}
					else
					{
						this.hMotionEyeNeckLesP.EyeNeckCalc();
					}
				}
			}
			for (int num13 = 0; num13 < this.ctrlEyeNeckMale.Length; num13++)
			{
				if (!(this.ctrlEyeNeckMale[num13] == null))
				{
					this.ctrlEyeNeckMale[num13].EyeNeckCalc();
				}
			}
			return;
		}
	}

	// Token: 0x06005107 RID: 20743 RVA: 0x00204900 File Offset: 0x00202D00
	public IEnumerator ChangeAnimation(HScene.AnimationListInfo _info, bool _isForceResetCamera, bool _isForceLoopAction = false, bool _UseFade = true)
	{
		GlobalMethod.setCameraMoveFlag(this.ctrlFlag.cameraCtrl, false);
		this.ctrlFlag.isInsert = false;
		this.ctrlFlag.isGaugeHit = false;
		this.ctrlFlag.isGaugeHit_M = false;
		this.AtariEffect.Stop();
		this.FeelHitEffect3D.Stop();
		this.nowChangeAnim = true;
		ProcBase.endInit = false;
		if (_info == null)
		{
			this.nowChangeAnim = false;
			ProcBase.endInit = true;
			yield break;
		}
		if (this.ctrlFlag.nowAnimationInfo == _info)
		{
			this.nowChangeAnim = false;
			ProcBase.endInit = true;
			yield break;
		}
		if (this.ctrlFlag.isFaintness && _info.nDownPtn == 0)
		{
			this.nowChangeAnim = false;
			ProcBase.endInit = true;
			yield break;
		}
		bool[] isSpeek = new bool[2];
		isSpeek[0] = ((this.ctrlVoice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.ctrlVoice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice) && Singleton<Voice>.Instance.IsVoiceCheck(this.ctrlFlag.voice.voiceTrs[0], true));
		if (this.chaFemales[1] != null && this.chaFemales[1].visibleAll && this.chaFemales[1].objBody != null)
		{
			isSpeek[1] = ((this.ctrlVoice.nowVoices[1].state == HVoiceCtrl.VoiceKind.voice || this.ctrlVoice.nowVoices[1].state == HVoiceCtrl.VoiceKind.startVoice) && Singleton<Voice>.Instance.IsVoiceCheck(this.ctrlFlag.voice.voiceTrs[1], true));
		}
		while (isSpeek[0] | isSpeek[1])
		{
			yield return null;
			isSpeek[0] = ((this.ctrlVoice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.ctrlVoice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice) && Singleton<Voice>.Instance.IsVoiceCheck(this.ctrlFlag.voice.voiceTrs[0], true));
			if (this.chaFemales[1] != null && this.chaFemales[1].visibleAll && this.chaFemales[1].objBody != null)
			{
				isSpeek[1] = ((this.ctrlVoice.nowVoices[1].state == HVoiceCtrl.VoiceKind.voice || this.ctrlVoice.nowVoices[1].state == HVoiceCtrl.VoiceKind.startVoice) && Singleton<Voice>.Instance.IsVoiceCheck(this.ctrlFlag.voice.voiceTrs[1], true));
			}
		}
		this.sbLoadFileName.Clear();
		bool isIdle = this.IsIdle(this.chaFemales[0].animBody) || _info.nAnimListInfoID != this.ctrlFlag.nowAnimationInfo.nAnimListInfoID || _info.nBreathID != this.ctrlFlag.nowAnimationInfo.nBreathID;
		if (isIdle && _isForceLoopAction)
		{
			isIdle = false;
		}
		bool afterIdle = this.IsAfterIdle(this.chaFemales[0].animBody, 0);
		int oldMode = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
		if (_info.ActionCtrl.Item1 < 3)
		{
			this.mode = _info.ActionCtrl.Item1;
		}
		else if (_info.ActionCtrl.Item1 == 3)
		{
			switch (_info.ActionCtrl.Item2)
			{
			case 0:
				this.mode = 2;
				break;
			case 1:
			case 7:
				this.mode = 2;
				break;
			case 2:
				this.mode = 3;
				break;
			case 3:
				this.mode = 0;
				break;
			case 4:
				this.mode = 0;
				break;
			case 5:
				this.mode = 4;
				break;
			case 6:
				this.mode = 5;
				break;
			}
		}
		else
		{
			this.mode = _info.ActionCtrl.Item1 + 2;
		}
		this.modeCtrl = _info.ActionCtrl.Item2;
		bool NumCharaChange = false;
		if (this.CtrlParticle != null)
		{
			this.CtrlParticle.RePlaceObject();
		}
		for (int s = 0; s < this.chaMales.Length; s++)
		{
			if (_info.fileMale != string.Empty && (s != 1 || _info.nPromiscuity == 0))
			{
				if (!(this.chaMales[s] == null))
				{
					if (this.chaMales[s].objTop == null)
					{
						this.chaMales[s].Load(false);
						this.chaMales[s].SetShapeBodyValue(0, 0.75f);
						NumCharaChange = true;
					}
					else
					{
						this.chaMales[s].visibleAll = true;
						NumCharaChange = true;
					}
					FrameCorrect.AddFrameCorrect(this.chaMales[s].animBody.gameObject);
					IKCorrect.AddIKCorrect(this.chaMales[s].animBody.gameObject);
					if (!this.ctrlHitObjectMales[s].isInit)
					{
						this.ctrlHitObjectMales[s].id = s;
						yield return this.ctrlHitObjectMales[s].HitObjInit(0, this.chaMales[s].objBodyBone, this.chaMales[s]);
					}
					this.ctrlHitObjectMales[s].setActiveObject(true);
					bool existChar = false;
					foreach (System.Tuple<int, int, MotionIK> tuple in this.lstMotionIK)
					{
						if (tuple.Item1 == 0 && tuple.Item2 == s)
						{
							existChar = true;
							break;
						}
					}
					if (!existChar)
					{
						this.lstMotionIK.Add(new System.Tuple<int, int, MotionIK>(0, s, new MotionIK(this.chaMales[s], true, null)));
					}
				}
			}
			else if (!(this.chaMales[s] == null) && this.chaMales[s].visibleAll)
			{
				if (this.ctrlHitObjectMales[s] != null)
				{
					if (this.ctrlHitObjectMales[s].isInit)
					{
						this.ctrlHitObjectMales[s].setActiveObject(false);
					}
					this.chaMales[s].visibleAll = false;
					NumCharaChange = true;
					this.chaMales[s].ReleaseHitObject();
					this.ctrlMaleCollisionCtrls[s].Release();
					this.ctrlFlag.lstSyncAnimLayers[0, s].Clear();
					this.ctrlFlag.lstSyncAnimLayers[0, s].Clear();
				}
			}
		}
		for (int i = 0; i < this.chaFemales.Length; i++)
		{
			if (!(this.chaFemales[i] == null))
			{
				if (i != 1 || _info.nPromiscuity >= 1)
				{
					if (this.chaFemales[i].objTop == null)
					{
						this.chaFemales[i].Load(false);
						NumCharaChange = true;
					}
					else
					{
						this.chaFemales[i].visibleAll = true;
						NumCharaChange = true;
					}
					FrameCorrect.AddFrameCorrect(this.chaFemales[i].animBody.gameObject);
					IKCorrect.AddIKCorrect(this.chaFemales[i].animBody.gameObject);
					bool flag = false;
					foreach (System.Tuple<int, int, MotionIK> tuple2 in this.lstMotionIK)
					{
						if (tuple2.Item1 == 1 && tuple2.Item2 == i)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.lstMotionIK.Add(new System.Tuple<int, int, MotionIK>(1, i, new MotionIK(this.chaFemales[i], true, null)));
					}
				}
				else if (!(this.chaFemales[i] == null) && this.chaFemales[i].visibleAll)
				{
					this.chaFemales[i].visibleAll = false;
					NumCharaChange = true;
					this.chaFemales[i].ReleaseHitObject();
					this.ctrlFemaleCollisionCtrls[i].Release();
					this.ctrlVoice.nowVoices[i].state = HVoiceCtrl.VoiceKind.breath;
					this.ctrlFlag.voice.playShorts[i] = -1;
					this.ctrlFlag.voice.playVoices[i] = false;
					this.ctrlFlag.lstSyncAnimLayers[1, i].Clear();
					this.ctrlFlag.lstSyncAnimLayers[1, i].Clear();
				}
			}
		}
		for (int j = 0; j < this.lstMotionIK.Count; j++)
		{
			this.lstMotionIK[j].Item3.MapIK = false;
			this.lstMotionIK[j].Item3.SetPartners(this.lstMotionIK);
			this.lstMotionIK[j].Item3.Reset();
		}
		this.abName = GlobalMethod.GetAssetBundleNameListFromPath(this.hSceneManager.strAssetIKListFolder, false);
		yield return null;
		HScene.AnimationListInfo tmpAddInfo = null;
		this.racM[0] = null;
		this.racM[1] = null;
		this.HoushiRacM[0] = null;
		this.HoushiRacM[1] = null;
		this.hashUseAssetBundleAnimator.Clear();
		for (int s2 = 0; s2 < this.chaMales.Length; s2++)
		{
			if (!(this.chaMales[s2] == null) && !(this.chaMales[s2].animBody == null) && !(_info.fileMale == string.Empty))
			{
				if (_info.id == -1)
				{
					this.chaMales[s2].LoadAnimation(_info.assetpathMale, _info.fileMale, string.Empty);
				}
				else
				{
					if (_info.ActionCtrl.Item1 < 3)
					{
						if (this.mode == 1 && _info.nDownPtn == 1)
						{
							foreach (HScene.AnimationListInfo tmpInfo in this.lstAnimInfo[1])
							{
								bool placeCheck = false;
								foreach (int item in tmpInfo.nPositons)
								{
									if (_info.nPositons.Contains(item))
									{
										placeCheck = true;
										break;
									}
								}
								if (_info.ActionCtrl.Item2 == tmpInfo.ActionCtrl.Item2 && placeCheck && tmpInfo.nFaintnessLimit == 1)
								{
									this.sbLoadFileName.Clear();
									this.sbLoadFileName.Append(tmpInfo.fileMale);
									this.HoushiRacM[s2] = CommonLib.LoadAsset<RuntimeAnimatorController>(tmpInfo.assetpathMale, this.sbLoadFileName.ToString(), false, string.Empty);
									this.hashUseAssetBundleAnimator.Add(tmpInfo.assetpathMale);
									yield return null;
									tmpAddInfo = tmpInfo;
									break;
								}
							}
						}
						this.sbLoadFileName.Clear();
						this.sbLoadFileName.Append(_info.fileMale);
						this.racM[s2] = CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathMale, this.sbLoadFileName.ToString(), false, string.Empty);
						yield return null;
					}
					else
					{
						if (!this.racEtcM.ContainsKey(_info.assetBaseM))
						{
							this.racEtcM.Add(_info.assetBaseM, CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathBaseM, _info.assetBaseM, false, string.Empty));
							this.hashUseAssetBundleAnimator.Add(_info.assetpathBaseM);
						}
						this.sbLoadFileName.Clear();
						this.sbLoadFileName.AppendFormat("{0}{1}", _info.fileMale, string.Empty);
						this.racM[s2] = CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathMale, this.sbLoadFileName.ToString(), false, string.Empty);
					}
					this.ctrlLayer.LoadExcel(this.sbLoadFileName.ToString(), 0, s2, false, 0);
				}
				this.hashUseAssetBundleAnimator.Add(_info.assetpathMale);
				yield return null;
			}
		}
		this.racF[0] = null;
		this.racF[1] = null;
		this.HoushiRacF[0] = null;
		this.HoushiRacF[1] = null;
		for (int s3 = 0; s3 < this.chaFemales.Length; s3++)
		{
			if (!(this.chaFemales[s3] == null) && !(this.chaFemales[s3].animBody == null))
			{
				if (_info.id == -1)
				{
					this.chaFemales[s3].LoadAnimation(_info.assetpathFemale, _info.fileFemale, string.Empty);
				}
				else
				{
					if (_info.ActionCtrl.Item1 < 3)
					{
						if (this.mode == 1 && _info.nDownPtn == 1)
						{
							if (tmpAddInfo == null)
							{
								foreach (HScene.AnimationListInfo tmpInfo2 in this.lstAnimInfo[1])
								{
									bool placeCheck2 = false;
									foreach (int item2 in tmpInfo2.nPositons)
									{
										if (_info.nPositons.Contains(item2))
										{
											placeCheck2 = true;
											break;
										}
									}
									if (_info.ActionCtrl.Item2 == tmpInfo2.ActionCtrl.Item2 && !placeCheck2 && tmpInfo2.nFaintnessLimit == 1)
									{
										this.sbLoadFileName.Clear();
										this.sbLoadFileName.Append(tmpInfo2.fileFemale);
										this.HoushiRacF[s3] = CommonLib.LoadAsset<RuntimeAnimatorController>(tmpInfo2.assetpathFemale, this.sbLoadFileName.ToString(), false, string.Empty);
										this.hashUseAssetBundleAnimator.Add(tmpInfo2.assetpathFemale);
										yield return null;
										tmpAddInfo = tmpInfo2;
										break;
									}
								}
							}
							else
							{
								this.sbLoadFileName.Clear();
								this.sbLoadFileName.Append(tmpAddInfo.fileFemale);
								this.HoushiRacF[s3] = CommonLib.LoadAsset<RuntimeAnimatorController>(tmpAddInfo.assetpathFemale, this.sbLoadFileName.ToString(), false, string.Empty);
								this.hashUseAssetBundleAnimator.Add(tmpAddInfo.assetpathFemale);
								yield return null;
							}
						}
						this.sbLoadFileName.Clear();
						this.sbLoadFileName.Append(_info.fileFemale);
						this.racF[s3] = CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathFemale, this.sbLoadFileName.ToString(), false, string.Empty);
						yield return null;
					}
					else if (_info.nPromiscuity < 1)
					{
						if (!this.racEtcF.ContainsKey(_info.assetBaseF))
						{
							this.racEtcF.Add(_info.assetBaseF, CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathBaseF, _info.assetBaseF, false, string.Empty));
							this.hashUseAssetBundleAnimator.Add(_info.assetpathBaseF);
						}
						this.sbLoadFileName.Clear();
						this.sbLoadFileName.Append(_info.fileFemale);
						this.racF[s3] = CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathFemale, this.sbLoadFileName.ToString(), false, string.Empty);
					}
					else if (_info.nPromiscuity == 1)
					{
						bool flag2 = s3 == 0;
						if (flag2)
						{
							if (!this.racEtcF.ContainsKey(_info.assetBaseF))
							{
								this.racEtcF.Add(_info.assetBaseF, CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathBaseF, _info.assetBaseF, false, string.Empty));
								this.hashUseAssetBundleAnimator.Add(_info.assetpathBaseF);
							}
							this.sbLoadFileName.Clear();
							this.sbLoadFileName.Append(_info.fileFemale);
							this.racF[s3] = CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathFemale, this.sbLoadFileName.ToString(), false, string.Empty);
						}
						else
						{
							if (!this.racEtcF.ContainsKey(_info.assetBaseF2))
							{
								this.racEtcF.Add(_info.assetBaseF2, CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathBaseF2, _info.assetBaseF2, false, string.Empty));
								this.hashUseAssetBundleAnimator.Add(_info.assetpathBaseF2);
							}
							this.sbLoadFileName.Clear();
							this.sbLoadFileName.Append(_info.fileFemale2);
							this.racF[s3] = CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathFemale2, this.sbLoadFileName.ToString(), false, string.Empty);
						}
					}
					else if (_info.nPromiscuity == 2)
					{
						if (s3 != 0)
						{
							if (!this.racEtcF.ContainsKey(_info.assetBaseF))
							{
								this.racEtcF.Add(_info.assetBaseF, CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathBaseF, _info.assetBaseF, false, string.Empty));
								this.hashUseAssetBundleAnimator.Add(_info.assetpathBaseF);
							}
							this.sbLoadFileName.Clear();
							this.sbLoadFileName.Append(_info.fileFemale);
							this.racF[s3] = CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathFemale, this.sbLoadFileName.ToString(), false, string.Empty);
						}
						else
						{
							if (!this.racEtcF.ContainsKey(_info.assetBaseF2))
							{
								this.racEtcF.Add(_info.assetBaseF2, CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathBaseF2, _info.assetBaseF2, false, string.Empty));
								this.hashUseAssetBundleAnimator.Add(_info.assetpathBaseF2);
							}
							this.sbLoadFileName.Clear();
							this.sbLoadFileName.Append(_info.fileFemale2);
							this.racF[s3] = CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathFemale2, this.sbLoadFileName.ToString(), false, string.Empty);
						}
					}
					this.ctrlLayer.LoadExcel(this.sbLoadFileName.ToString(), 1, s3, false, 0);
				}
				this.hashUseAssetBundleAnimator.Add(_info.assetpathFemale);
				if (_info.nPromiscuity > 0)
				{
					this.hashUseAssetBundleAnimator.Add(_info.assetpathFemale2);
				}
				yield return null;
			}
		}
		bool racMChange = false;
		for (int k = 0; k < this.chaMales.Length; k++)
		{
			if (!(this.chaMales[k] == null) && !(this.chaMales[k].animBody == null))
			{
				if (!(this.racM[k] == null))
				{
					racMChange = true;
					if (_info.ActionCtrl.Item1 < 3)
					{
						RuntimeAnimatorController runtimeAnimatorController = null;
						if (!_info.assetBaseM.IsNullOrEmpty())
						{
							runtimeAnimatorController = CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathBaseM, _info.assetBaseM, false, string.Empty);
						}
						if (this.mode == 0 && (oldMode != _info.ActionCtrl.Item1 || runtimeAnimatorController == null))
						{
							this.chaMales[k].animBody.runtimeAnimatorController = this.runtimeAnimatorControllers[0, 0];
						}
						else if (this.mode == 1 && (oldMode != _info.ActionCtrl.Item1 || runtimeAnimatorController == null))
						{
							this.chaMales[k].animBody.runtimeAnimatorController = this.runtimeAnimatorControllers[0, 1];
						}
						else if (this.mode == 2 && (oldMode != _info.ActionCtrl.Item1 || runtimeAnimatorController == null))
						{
							this.chaMales[k].animBody.runtimeAnimatorController = this.runtimeAnimatorControllers[0, 2];
						}
						if (runtimeAnimatorController != null)
						{
							this.chaMales[k].animBody.runtimeAnimatorController = runtimeAnimatorController;
						}
						if (this.HoushiRacM[k] == null)
						{
							this.chaMales[k].animBody.runtimeAnimatorController = Illusion.Utils.Animator.SetupAnimatorOverrideController(this.chaMales[k].animBody.runtimeAnimatorController, this.racM[k]);
						}
						else
						{
							this.chaMales[k].animBody.runtimeAnimatorController = this.MixRuntimeControler(this.chaMales[k].animBody.runtimeAnimatorController, this.racM[k], this.HoushiRacM[k]);
						}
					}
					else
					{
						this.chaMales[k].animBody.runtimeAnimatorController = Illusion.Utils.Animator.SetupAnimatorOverrideController(this.racEtcM[_info.assetBaseM], this.racM[k]);
					}
				}
			}
		}
		bool racFChange = false;
		for (int l = 0; l < this.chaFemales.Length; l++)
		{
			if (!(this.racF[l] == null))
			{
				racFChange = true;
				if (_info.ActionCtrl.Item1 < 3)
				{
					RuntimeAnimatorController runtimeAnimatorController2 = null;
					if (!_info.assetBaseF.IsNullOrEmpty())
					{
						runtimeAnimatorController2 = CommonLib.LoadAsset<RuntimeAnimatorController>(_info.assetpathBaseF, _info.assetBaseF, false, string.Empty);
					}
					if (this.mode == 0 && (oldMode != _info.ActionCtrl.Item1 || runtimeAnimatorController2 == null))
					{
						this.chaFemales[l].animBody.runtimeAnimatorController = this.runtimeAnimatorControllers[1, 0];
					}
					else if (this.mode == 1 && (oldMode != _info.ActionCtrl.Item1 || runtimeAnimatorController2 == null))
					{
						this.chaFemales[l].animBody.runtimeAnimatorController = this.runtimeAnimatorControllers[1, 1];
					}
					else if (this.mode == 2 && (oldMode != _info.ActionCtrl.Item1 || runtimeAnimatorController2 == null))
					{
						this.chaFemales[l].animBody.runtimeAnimatorController = this.runtimeAnimatorControllers[1, 2];
					}
					if (runtimeAnimatorController2 != null)
					{
						this.chaFemales[l].animBody.runtimeAnimatorController = runtimeAnimatorController2;
					}
					if (this.HoushiRacF[l] == null)
					{
						this.chaFemales[l].animBody.runtimeAnimatorController = Illusion.Utils.Animator.SetupAnimatorOverrideController(this.chaFemales[l].animBody.runtimeAnimatorController, this.racF[l]);
					}
					else
					{
						this.chaFemales[l].animBody.runtimeAnimatorController = this.MixRuntimeControler(this.chaFemales[l].animBody.runtimeAnimatorController, this.racF[l], this.HoushiRacF[l]);
					}
				}
				else if (_info.nPromiscuity < 1)
				{
					this.chaFemales[l].animBody.runtimeAnimatorController = Illusion.Utils.Animator.SetupAnimatorOverrideController(this.racEtcF[_info.assetBaseF], this.racF[l]);
				}
				else if (_info.nPromiscuity == 1)
				{
					if (l == 0)
					{
						this.chaFemales[l].animBody.runtimeAnimatorController = Illusion.Utils.Animator.SetupAnimatorOverrideController(this.racEtcF[_info.assetBaseF], this.racF[l]);
					}
					else if (l == 1)
					{
						this.chaFemales[l].animBody.runtimeAnimatorController = Illusion.Utils.Animator.SetupAnimatorOverrideController(this.racEtcF[_info.assetBaseF2], this.racF[l]);
					}
				}
				else if (_info.nPromiscuity == 2)
				{
					if (l == 0)
					{
						this.chaFemales[l].animBody.runtimeAnimatorController = Illusion.Utils.Animator.SetupAnimatorOverrideController(this.racEtcF[_info.assetBaseF2], this.racF[l]);
					}
					else if (l == 1)
					{
						this.chaFemales[l].animBody.runtimeAnimatorController = Illusion.Utils.Animator.SetupAnimatorOverrideController(this.racEtcF[_info.assetBaseF], this.racF[l]);
					}
				}
			}
		}
		if (!racMChange && !racFChange && _info.id != -1)
		{
			ProcBase.endInit = true;
			this.nowChangeAnim = false;
			yield break;
		}
		if (_UseFade)
		{
			this.fade.FadeStart(1.5f);
		}
		for (int m = 0; m < this.chaMales.Length; m++)
		{
			if (this.chaMales[m] == null || this.chaMales[m].objTop == null)
			{
				break;
			}
			this.chaMales[m].visibleSon = (_info.nMaleSon == 1);
		}
		if (!this.isSetStartPos)
		{
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			int num = -1;
			for (int n = 0; n < _info.lstOffset.Count; n++)
			{
				if (this.ctrlFlag.nPlace == _info.nPositons[n])
				{
					num = n;
					break;
				}
			}
			if (num >= 0 && _info.lstOffset[num] != null && _info.lstOffset[num] != string.Empty)
			{
				this.LoadMoveOffset(_info.lstOffset[num], out zero, out zero2);
			}
			this.SetPosition(this.StartPos.position, this.StartPos.rotation, zero, zero2, false);
			GlobalMethod.setCameraBase(this.ctrlFlag.cameraCtrl, this.chaFemalesTrans[0]);
			this.isSetStartPos = true;
		}
		else
		{
			Vector3 zero3 = Vector3.zero;
			Vector3 zero4 = Vector3.zero;
			int num2 = -1;
			for (int num3 = 0; num3 < _info.lstOffset.Count; num3++)
			{
				if (this.ctrlFlag.nPlace == _info.nPositons[num3])
				{
					num2 = num3;
					break;
				}
			}
			if (num2 >= 0 && _info.lstOffset[num2] != null && _info.lstOffset[num2] != string.Empty)
			{
				this.LoadMoveOffset(_info.lstOffset[num2], out zero3, out zero4);
			}
			this.SetPosition(this.hPointCtrl.lstMarker[this.ctrlFlag.HPointID].Item2.pivot.position, this.hPointCtrl.lstMarker[this.ctrlFlag.HPointID].Item2.pivot.rotation, zero3, zero4, false);
			GlobalMethod.setCameraBase(this.ctrlFlag.cameraCtrl, this.chaFemalesTrans[0]);
			this.setCameraLoad(_info, _isForceResetCamera);
		}
		if (afterIdle)
		{
			this.ctrlVoice.AfterFinish();
		}
		for (int num4 = 0; num4 < this.ctrlYures.Length; num4++)
		{
			if (!(this.ctrlYures[num4] == null))
			{
				if (_info.nPromiscuity < 1 && num4 == 1)
				{
					this.ctrlYures[num4].Release();
				}
				else
				{
					this.ctrlYures[num4].Load(_info.id, _info.ActionCtrl.Item1);
				}
			}
		}
		if (this.chaMales[0] == null || !this.chaMales[0].visibleAll || this.chaMales[0].objBody == null)
		{
			this.ctrlYureMale.Release();
		}
		else
		{
			this.ctrlYureMale.Load(_info.id, _info.ActionCtrl.Item1);
		}
		for (int num5 = 0; num5 < this.ctrlLookAts.Length; num5++)
		{
			if (this.chaMales[num5] == null || this.chaMales[num5].objBodyBone == null)
			{
				this.ctrlLookAts[num5].Release();
			}
			else
			{
				this.sbLoadFileName.Clear();
				this.sbLoadFileName.Append(_info.fileMale);
				this.ctrlLookAts[num5].LoadList(this.sbLoadFileName.ToString(), _info.id);
			}
		}
		for (int num6 = 0; num6 < this.chaMales.Length; num6++)
		{
			if (!(this.chaMales[num6] == null) && !(this.chaMales[num6].animBody == null) && this.chaMales[num6].visibleAll)
			{
				if (_info.isMaleHitObject)
				{
					if (!this.ctrlFlag.nowAnimationInfo.isMaleHitObject)
					{
						this.chaMales[num6].LoadHitObject();
					}
				}
				else
				{
					this.chaMales[num6].ReleaseHitObject();
				}
				if (this.chaMales[num6].objHitBody)
				{
					this.sbLoadFileName.Clear();
					this.sbLoadFileName.Append(_info.fileMale);
					this.ctrlMaleCollisionCtrls[num6].Init(this.chaFemales[0], this.chaMales[num6].objHitHead, this.chaMales[num6].objHitBody);
					this.ctrlMaleCollisionCtrls[num6].LoadExcel(this.sbLoadFileName.ToString());
				}
				else
				{
					this.ctrlMaleCollisionCtrls[num6].Release();
				}
				if (this.mode != -1)
				{
					for (int num7 = 0; num7 < this.lstMotionIK.Count; num7++)
					{
						if (this.lstMotionIK[num7].Item1 == 0 && this.lstMotionIK[num7].Item2 == num6)
						{
							this.sbLoadFileName.Clear();
							this.sbLoadFileName.Append(_info.fileMale);
							for (int num8 = 0; num8 < this.abName.Count; num8++)
							{
								if (GlobalMethod.AssetFileExist(this.abName[num8], this.sbLoadFileName.ToString(), string.Empty))
								{
									this.lstMotionIK[num7].Item3.LoadData(this.abName[num8], this.sbLoadFileName.ToString(), false);
									break;
								}
								this.lstMotionIK[num7].Item3.data.states = null;
							}
							if (this.mode == 1 && _info.nDownPtn == 1 && tmpAddInfo != null)
							{
								this.sbLoadFileName.Clear();
								this.sbLoadFileName.Append(tmpAddInfo.fileMale);
								for (int num9 = 0; num9 < this.abName.Count; num9++)
								{
									if (GlobalMethod.AssetFileExist(this.abName[num9], this.sbLoadFileName.ToString(), string.Empty))
									{
										this.lstMotionIK[num7].Item3.LoadData(this.abName[num9], this.sbLoadFileName.ToString(), true);
										break;
									}
								}
							}
						}
					}
				}
				if (this.chaMales[num6] != null && this.mode >= 0)
				{
					this.CtrlParticle.ParticleLoad(this.chaMales[num6].objBodyBone, num6 * 2);
				}
			}
		}
		for (int num10 = 0; num10 < this.chaFemales.Length; num10++)
		{
			if (!(this.chaFemales[num10] == null) && !(this.chaFemales[num10].animBody == null) && this.chaFemales[num10].visibleAll)
			{
				if (_info.id != -1 && this.mode != -1)
				{
					for (int num11 = 0; num11 < this.lstMotionIK.Count; num11++)
					{
						if (this.lstMotionIK[num11].Item1 == 1 && this.lstMotionIK[num11].Item2 == num10)
						{
							this.sbLoadFileName.Clear();
							if (_info.nPromiscuity < 1)
							{
								this.sbLoadFileName.Append(_info.fileFemale);
							}
							else if (_info.nPromiscuity == 1)
							{
								if (num10 == 0)
								{
									this.sbLoadFileName.Append(_info.fileFemale);
								}
								else if (num10 == 1)
								{
									this.sbLoadFileName.Append(_info.fileFemale2);
								}
							}
							else if (_info.nPromiscuity == 2)
							{
								if (num10 == 0)
								{
									this.sbLoadFileName.Append(_info.fileFemale2);
								}
								else if (num10 == 1)
								{
									this.sbLoadFileName.Append(_info.fileFemale);
								}
							}
							for (int num12 = 0; num12 < this.abName.Count; num12++)
							{
								if (GlobalMethod.AssetFileExist(this.abName[num12], this.sbLoadFileName.ToString(), string.Empty))
								{
									this.lstMotionIK[num11].Item3.LoadData(this.abName[num12], this.sbLoadFileName.ToString(), false);
									break;
								}
								this.lstMotionIK[num11].Item3.data.states = null;
							}
							if (this.mode == 1 && _info.nDownPtn == 1 && tmpAddInfo != null)
							{
								this.sbLoadFileName.Clear();
								this.sbLoadFileName.Append(tmpAddInfo.fileFemale);
								for (int num13 = 0; num13 < this.abName.Count; num13++)
								{
									if (GlobalMethod.AssetFileExist(this.abName[num13], this.sbLoadFileName.ToString(), string.Empty))
									{
										this.lstMotionIK[num11].Item3.LoadData(this.abName[num13], this.sbLoadFileName.ToString(), true);
										break;
									}
								}
							}
						}
					}
				}
				bool isFemaleHitObject = _info.isFemaleHitObject;
				if (isFemaleHitObject)
				{
					if (!this.ctrlFlag.nowAnimationInfo.isFemaleHitObject)
					{
						this.chaFemales[num10].LoadHitObject();
					}
				}
				else
				{
					this.chaFemales[num10].ReleaseHitObject();
				}
				if (this.chaFemales[num10].objHitBody)
				{
					this.ctrlFemaleCollisionCtrls[num10].Init(this.chaFemales[num10], this.chaFemales[num10].objHitHead, this.chaFemales[num10].objHitBody);
					this.sbLoadFileName.Clear();
					if (_info.nPromiscuity < 1)
					{
						this.sbLoadFileName.Append(_info.fileFemale);
					}
					else if (_info.nPromiscuity == 1)
					{
						if (num10 == 0)
						{
							this.sbLoadFileName.Append(_info.fileFemale);
						}
						else if (num10 == 1)
						{
							this.sbLoadFileName.Append(_info.fileFemale2);
						}
					}
					else if (_info.nPromiscuity == 2)
					{
						if (num10 == 0)
						{
							this.sbLoadFileName.Append(_info.fileFemale2);
						}
						else if (num10 == 1)
						{
							this.sbLoadFileName.Append(_info.fileFemale);
						}
					}
					this.ctrlFemaleCollisionCtrls[num10].LoadExcel(this.sbLoadFileName.ToString());
				}
				else
				{
					this.ctrlFemaleCollisionCtrls[num10].Release();
				}
				if (this.chaFemales[num10] != null && this.mode >= 0)
				{
					if (num10 == 1)
					{
						this.CtrlParticle.ParticleLoad(this.chaFemales[num10].objBodyBone, 3);
						this.ctrlDynamics[num10].Init(this.chaFemales[num10]);
					}
					else
					{
						this.CtrlParticle.ParticleLoad(this.chaFemales[num10].objBodyBone, 1);
					}
				}
			}
		}
		this.SetClothStateStartMotion(0, _info);
		if (_info.nPromiscuity >= 1)
		{
			this.SetClothStateStartMotion(1, _info);
		}
		if (_info.isNeedItem)
		{
			this.ctrlItem.ReleaseItem();
			yield return null;
			this.ctrlFlag.AddParam(9, 1);
			this.ctrlItem.LoadItem(_info.ActionCtrl.Item1, _info.id, (!(this.chaMales[0] != null)) ? null : this.chaMales[0].objBodyBone, this.chaFemales[0].objBodyBone, (!(this.chaMales[1] != null)) ? null : this.chaMales[1].objBodyBone, (!(this.chaFemales[1] != null)) ? null : this.chaFemales[1].objBodyBone);
		}
		else
		{
			this.ctrlItem.ReleaseItem();
			if (this.ctrlFlag.nowHPoint != null)
			{
				ChangeHItem componentInChildren = this.ctrlFlag.nowHPoint.GetComponentInChildren<ChangeHItem>();
				if (componentInChildren != null)
				{
					componentInChildren.ChangeActive(true);
				}
				this.ctrlFlag.nowHPoint.SetEffectActive(false);
			}
		}
		List<HItemCtrl.Item> itemList = this.ctrlItem.GetItems();
		int itemListCount = itemList.Count;
		GameObject[] items = new GameObject[itemListCount];
		for (int num14 = 0; num14 < itemListCount; num14++)
		{
			items[num14] = itemList[num14].objItem;
		}
		for (int num15 = 0; num15 < this.lstMotionIK.Count; num15++)
		{
			if (this.lstMotionIK[num15].Item3.items != null)
			{
				this.lstMotionIK[num15].Item3.items.Clear();
			}
			this.lstMotionIK[num15].Item3.SetItems(this.lstMotionIK[num15].Item3, items);
			this.lstMotionIK[num15].Item3.Reset();
		}
		if (this.mode != -1)
		{
			this.lstProc[this.mode].Init(this.modeCtrl);
			this.lstProc[this.mode].SetStartMotion(isIdle, this.modeCtrl, _info);
			this.ctrlVoice.HouchiTime = 0f;
		}
		yield return MapUIContainer.DrawOnceTutorialUI(5, this.Camera);
		this.ctrlMeta.Clear();
		if (this.mode != 0 && _info.nPromiscuity != 2)
		{
			this.ctrlMeta.Load(this.hSceneManager.strAssetMetaBallListFolder, _info.fileFemale, (!(this.chaMales[0] != null)) ? this.ctrlItem.GetItem() : ((!this.chaMales[0].objBodyBone) ? this.ctrlItem.GetItem() : this.chaMales[0].objBodyBone), (!(this.chaMales[1] != null)) ? this.ctrlItem.GetItem() : ((!this.chaMales[1].objBodyBone) ? this.ctrlItem.GetItem() : this.chaMales[1].objBodyBone), (!(this.chaFemales[1] != null)) ? null : this.chaFemales[1].objBodyBone, this.chaFemales[1]);
		}
		yield return null;
		if (this.sprite != null)
		{
			if (this.mode == 2)
			{
				this.sprite.SetFinishSelect(this.mode, this.modeCtrl, _info.ActionCtrl.Item1, _info.ActionCtrl.Item2);
			}
			else
			{
				this.sprite.SetFinishSelect(this.mode, this.modeCtrl, -1, -1);
			}
		}
		if (_info.id == -1)
		{
			this.ctrlFlag.nowAnimationInfo = _info;
			ProcBase.endInit = true;
			this.nowChangeAnim = false;
			yield break;
		}
		this.CtrlParticle.ParticleLoad(this.ctrlItem.GetItem(), 4);
		for (int num16 = 0; num16 < this.ctrlHitObjectFemales.Length; num16++)
		{
			if (this.ctrlHitObjectFemales[num16] != null)
			{
				if (_info.nPromiscuity < 1 && num16 == 1)
				{
					this.ctrlHitObjectFemales[num16].HitObjLoadExcel(string.Empty);
				}
				else
				{
					this.sbLoadFileName.Clear();
					if (_info.nPromiscuity < 1)
					{
						this.sbLoadFileName.Append(_info.fileFemale);
					}
					else if (_info.nPromiscuity == 1)
					{
						if (num16 == 0)
						{
							this.sbLoadFileName.Append(_info.fileFemale);
						}
						else if (num16 == 1)
						{
							this.sbLoadFileName.Append(_info.fileFemale2);
						}
					}
					else if (_info.nPromiscuity == 2)
					{
						if (num16 == 0)
						{
							this.sbLoadFileName.Append(_info.fileFemale2);
						}
						else if (num16 == 1)
						{
							this.sbLoadFileName.Append(_info.fileFemale);
						}
					}
					this.ctrlHitObjectFemales[num16].HitObjLoadExcel(this.sbLoadFileName.ToString());
				}
			}
		}
		for (int num17 = 0; num17 < this.ctrlHitObjectMales.Length; num17++)
		{
			if (this.ctrlHitObjectMales[num17] != null)
			{
				if (_info.nPromiscuity != 0 && num17 == 1)
				{
					this.ctrlHitObjectMales[num17].HitObjLoadExcel(string.Empty);
				}
				else
				{
					this.sbLoadFileName.Clear();
					this.sbLoadFileName.Append(_info.fileMale);
					this.ctrlHitObjectMales[num17].HitObjLoadExcel(this.sbLoadFileName.ToString());
				}
			}
		}
		for (int num18 = 0; num18 < this.ctrlDynamics.Length; num18++)
		{
			if (this.ctrlDynamics[num18] != null)
			{
				if (_info.nPromiscuity < 1 && num18 == 1)
				{
					this.ctrlDynamics[num18].Load(this.hSceneManager.strAssetDynamicBoneListFolder, string.Empty);
				}
				else
				{
					this.sbLoadFileName.Clear();
					if (_info.nPromiscuity < 1)
					{
						this.sbLoadFileName.Append(_info.fileFemale);
					}
					else if (_info.nPromiscuity == 1)
					{
						if (num18 == 0)
						{
							this.sbLoadFileName.Append(_info.fileFemale);
						}
						else if (num18 == 1)
						{
							this.sbLoadFileName.Append(_info.fileFemale2);
						}
					}
					else if (_info.nPromiscuity == 2)
					{
						if (num18 == 0)
						{
							this.sbLoadFileName.Append(_info.fileFemale2);
						}
						else if (num18 == 1)
						{
							this.sbLoadFileName.Append(_info.fileFemale);
						}
					}
					this.ctrlDynamics[num18].Load(this.hSceneManager.strAssetDynamicBoneListFolder, this.sbLoadFileName.ToString());
				}
			}
		}
		GameObject[] objBoneFemale = new GameObject[]
		{
			this.chaFemales[0].objBodyBone,
			(!(this.chaFemales[1] != null)) ? null : this.chaFemales[1].objBodyBone
		};
		this.ctrlSE.InitOldMember(-1);
		this.ctrlSE.Load(this.hSceneManager.strAssetSEListFolder, _info.fileSe, (!(this.chaMales[0] != null)) ? null : this.chaMales[0].objBodyBone, objBoneFemale);
		this.ctrlVoice.SetVoiceList(_info.nAnimListInfoID, _info.nBreathID, _info.id, _info.lstSystem);
		bool _isMainFirstPerson = true;
		if (_info.ActionCtrl.Item1 == 4)
		{
			_isMainFirstPerson = (_info.nInitiativeFemale == 0);
		}
		this.ctrlVoice.SetBreathVoiceList(this.chaFemales, _info.ActionCtrl.Item1, _info.ActionCtrl.Item2, _info.nInitiativeFemale != 0, this.hSceneManager.isForce, _isMainFirstPerson);
		bool femalePlayer = false;
		for (int num19 = 0; num19 < this.ctrlEyeNeckFemale.Length; num19++)
		{
			if (!(this.ctrlEyeNeckFemale[num19] == null) || !(this.hMotionEyeNeckLesP == null))
			{
				if (_info.nPromiscuity < 1 && num19 == 1)
				{
					if (NumCharaChange)
					{
						femalePlayer = (this.hSceneManager.Player.ChaControl.sex == 1 && !this.ctrlFlag.bFutanari);
						if (num19 == 0 || !femalePlayer)
						{
							this.ctrlEyeNeckFemale[num19].SetPartner(null, null, null);
							this.ctrlEyeNeckFemale[num19].Release();
						}
						else
						{
							this.hMotionEyeNeckLesP.SetPartner(null);
							this.hMotionEyeNeckLesP.Release();
						}
					}
				}
				else
				{
					if (NumCharaChange)
					{
						femalePlayer = false;
						femalePlayer = (this.hSceneManager.Player.ChaControl.sex == 1 && !this.ctrlFlag.bFutanari);
						if (num19 == 0 || !femalePlayer)
						{
							this.ctrlEyeNeckFemale[num19].SetPartner((!(this.chaMales[0] != null)) ? null : this.chaMales[0].objBodyBone, (!this.chaMales[1]) ? null : this.chaMales[1].objBodyBone, (!this.chaFemales[num19 ^ 1]) ? null : this.chaFemales[num19 ^ 1].objBodyBone);
						}
						else
						{
							this.hMotionEyeNeckLesP.SetPartner((!this.chaFemales[0]) ? null : this.chaFemales[0].objBodyBone);
						}
					}
					if (!(this.chaFemales[num19] == null) && !(this.chaFemales[num19].objBody == null) && this.chaFemales[num19].visibleAll)
					{
						femalePlayer = false;
						this.sbLoadFileName.Clear();
						if (_info.nPromiscuity < 1)
						{
							this.sbLoadFileName.Append(_info.fileMotionNeckFemale);
						}
						else if (_info.nPromiscuity == 1)
						{
							if (num19 == 0)
							{
								this.sbLoadFileName.Append(_info.fileMotionNeckFemale);
							}
							else if (num19 == 1)
							{
								this.sbLoadFileName.Append(_info.fileMotionNeckFemale2);
							}
						}
						else if (_info.nPromiscuity == 2)
						{
							if (num19 == 0)
							{
								this.sbLoadFileName.Append(_info.fileMotionNeckFemale2);
							}
							else if (num19 == 1)
							{
								femalePlayer = (this.hSceneManager.Player.ChaControl.sex == 1 && !this.ctrlFlag.bFutanari);
								if (!femalePlayer)
								{
									this.sbLoadFileName.Append(_info.fileMotionNeckFemale);
								}
								else
								{
									this.sbLoadFileName.Append(_info.fileMotionNeckFemalePlayer);
								}
							}
						}
						if (num19 == 0 || !femalePlayer)
						{
							this.ctrlEyeNeckFemale[num19].Load(this.hSceneManager.strAssetNeckCtrlListFolder, this.sbLoadFileName.ToString());
						}
						else
						{
							this.hMotionEyeNeckLesP.Load(this.hSceneManager.strAssetNeckCtrlListFolder, this.sbLoadFileName.ToString());
						}
					}
				}
			}
		}
		for (int num20 = 0; num20 < this.ctrlEyeNeckMale.Length; num20++)
		{
			if (!(this.ctrlEyeNeckMale[num20] == null))
			{
				if (_info.nPromiscuity != 0 && num20 == 1)
				{
					if (NumCharaChange)
					{
						this.ctrlEyeNeckMale[num20].SetPartner(null, null, null);
						this.ctrlEyeNeckMale[num20].Release();
					}
				}
				else
				{
					if (NumCharaChange)
					{
						this.ctrlEyeNeckMale[num20].SetPartner(this.chaFemales[0].objBodyBone, (!this.chaFemales[1]) ? null : this.chaFemales[1].objBodyBone, (!this.chaMales[num20 ^ 1]) ? null : this.chaMales[num20 ^ 1].objBodyBone);
					}
					this.sbLoadFileName.Clear();
					this.sbLoadFileName.Append(_info.fileMotionNeckMale);
					this.ctrlEyeNeckMale[num20].Load(this.hSceneManager.strAssetNeckCtrlListFolder, this.sbLoadFileName.ToString());
				}
			}
		}
		yield return null;
		for (int num21 = 0; num21 < this.ctrlSiruPastes.Length; num21++)
		{
			if (!(this.ctrlSiruPastes[num21] == null))
			{
				if (_info.nPromiscuity < 1 && num21 == 1)
				{
					this.ctrlSiruPastes[num21].Release();
				}
				else
				{
					this.sbLoadFileName.Clear();
					if (_info.ActionCtrl.Item1 != 5)
					{
						this.sbLoadFileName.Append(_info.fileSiruPaste);
					}
					else if (num21 == 0)
					{
						this.sbLoadFileName.Append(_info.fileSiruPaste);
					}
					else if (num21 == 1)
					{
						this.sbLoadFileName.Append(_info.fileSiruPasteSecond);
					}
					this.ctrlSiruPastes[num21].Load(this.hSceneManager.strAssetSiruPasteListFolder, this.sbLoadFileName.ToString(), _info.id);
				}
			}
		}
		this.ctrlAuto.AutoAutoLeaveItToYouInit();
		this.ctrlFlag.nowAnimationInfo = _info;
		for (int num22 = 0; num22 < this.chaFemales.Length; num22++)
		{
			if (this.chaFemales[num22] == null || !this.chaFemales[num22].visibleAll || this.chaFemales[num22].objTop == null)
			{
				break;
			}
			this.chaFemales[num22].reSetupDynamicBoneBust = true;
			this.chaFemales[num22].resetDynamicBoneAll = true;
		}
		for (int num23 = 0; num23 < this.chaMales.Length; num23++)
		{
			if (this.chaMales[num23] == null || !this.chaMales[num23].visibleAll || this.chaMales[num23].objTop == null)
			{
				break;
			}
			this.chaMales[num23].reSetupDynamicBoneBust = true;
			this.chaMales[num23].resetDynamicBoneAll = true;
		}
		for (int num24 = 0; num24 < this.chaFemales.Length; num24++)
		{
			if (_info.nPromiscuity < 1 && num24 == 1)
			{
				break;
			}
			FBSCtrlEyes eyeCtrl = this.chaFemales[num24].eyesCtrl;
			if (eyeCtrl != null)
			{
				eyeCtrl.SetOpenRateForce(1f);
			}
		}
		this.ctrlFeelHit.InitTime();
		if (this.ctrlVoice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice || this.ctrlVoice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice)
		{
			for (int num25 = 0; num25 < this.chaFemales.Length; num25++)
			{
				if (_info.nPromiscuity < 1 && num25 == 1)
				{
					break;
				}
				Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[num25]);
			}
		}
		if (this.chaFemales[1] != null && (!this.chaFemales[1].visibleAll || this.chaFemales[1].objBody == null))
		{
			Singleton<Voice>.Instance.Stop(this.ctrlFlag.voice.voiceTrs[1]);
		}
		for (int num26 = 0; num26 < _info.lstSystem.Count; num26++)
		{
			this.ctrlFlag.isJudgeSelect.Add((HSceneFlagCtrl.JudgeSelect)(1 << _info.lstSystem[num26]));
		}
		this.ctrlFlag.voice.dialog = false;
		if (this.mode == 0)
		{
			this.ctrlFlag.numAibu++;
			if (this.ctrlFlag.isJudgeSelect.Contains(HSceneFlagCtrl.JudgeSelect.Anal))
			{
				this.ctrlFlag.numAnal++;
			}
		}
		else if (this.mode == 1)
		{
			this.ctrlFlag.numHoushi++;
		}
		else if (this.mode == 2)
		{
			this.ctrlFlag.numSonyu++;
			if (this.ctrlFlag.isJudgeSelect.Contains(HSceneFlagCtrl.JudgeSelect.Kokan))
			{
				this.ctrlFlag.numKokan++;
			}
			if (this.ctrlFlag.isJudgeSelect.Contains(HSceneFlagCtrl.JudgeSelect.Anal))
			{
				this.ctrlFlag.numAnal++;
			}
		}
		else if (this.mode == 6)
		{
			this.ctrlFlag.numLes++;
		}
		if (this.ctrlFlag.isJudgeSelect.Contains(HSceneFlagCtrl.JudgeSelect.Kiss))
		{
			this.ctrlFlag.AddParam(6, 1);
		}
		if (_info.ActionCtrl.Item1 == 3 && _info.id == 13)
		{
			this.ctrlFlag.AddParam(17, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 38)
		{
			this.ctrlFlag.AddParam(19, 1);
		}
		else if (_info.ActionCtrl.Item1 == 0 && _info.id == 16)
		{
			this.ctrlFlag.AddParam(23, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 44)
		{
			this.ctrlFlag.AddParam(25, 1);
		}
		else if (_info.ActionCtrl.Item1 == 3 && _info.id == 14)
		{
			this.ctrlFlag.AddParam(35, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 45)
		{
			this.ctrlFlag.AddParam(37, 1);
		}
		else if (_info.ActionCtrl.Item1 == 1 && _info.id == 30)
		{
			this.ctrlFlag.AddParam(39, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 46)
		{
			this.ctrlFlag.AddParam(41, 1);
		}
		else if (_info.ActionCtrl.Item1 == 0 && _info.id == 100)
		{
			this.ctrlFlag.AddParam(43, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 113)
		{
			this.ctrlFlag.AddParam(45, 1);
		}
		else if (_info.ActionCtrl.Item1 == 1 && _info.id == 104)
		{
			this.ctrlFlag.AddParam(47, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 110)
		{
			this.ctrlFlag.AddParam(49, 1);
		}
		else if (_info.ActionCtrl.Item1 == 0 && _info.id == 101)
		{
			this.ctrlFlag.AddParam(51, 1);
		}
		else if (_info.ActionCtrl.Item1 == 1 && _info.id == 105)
		{
			this.ctrlFlag.AddParam(53, 1);
		}
		else if (_info.ActionCtrl.Item1 == 1 && _info.id == 106)
		{
			this.ctrlFlag.AddParam(55, 1);
		}
		else if (_info.ActionCtrl.Item1 == 1 && _info.id == 107)
		{
			this.ctrlFlag.AddParam(57, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 114)
		{
			this.ctrlFlag.AddParam(59, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 115)
		{
			this.ctrlFlag.AddParam(61, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 116)
		{
			this.ctrlFlag.AddParam(63, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 117)
		{
			this.ctrlFlag.AddParam(65, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 118)
		{
			this.ctrlFlag.AddParam(67, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 119)
		{
			this.ctrlFlag.AddParam(69, 1);
		}
		else if (_info.ActionCtrl.Item1 == 2 && _info.id == 120)
		{
			this.ctrlFlag.AddParam(71, 1);
		}
		else if (_info.ActionCtrl.Item1 == 3 && _info.id == 109)
		{
			this.ctrlFlag.AddParam(73, 1);
		}
		else if (_info.ActionCtrl.Item1 == 3 && _info.id == 110)
		{
			this.ctrlFlag.AddParam(75, 1);
		}
		else if (_info.ActionCtrl.Item1 == 3 && _info.id == 111)
		{
			this.ctrlFlag.AddParam(77, 1);
		}
		else if (_info.ActionCtrl.Item1 == 5 && _info.id == 113)
		{
			this.ctrlFlag.AddParam(79, 1);
		}
		else if (_info.ActionCtrl.Item1 == 5 && _info.id == 115)
		{
			this.ctrlFlag.AddParam(81, 1);
		}
		if (_info.ActionCtrl.Item1 == 3 && _info.ActionCtrl.Item2 == 3)
		{
			this.ctrlFlag.AddParam(21, 1);
		}
		foreach (string assetBundleName in this.hashUseAssetBundleAnimator)
		{
			AssetBundleManager.UnloadAssetBundle(assetBundleName, false, null, false);
		}
		GC.Collect();
		UnityEngine.Resources.UnloadUnusedAssets();
		yield return null;
		this.ctrlAuto.AutoAutoLeaveItToYouInit();
		this.nowChangeAnim = false;
		yield break;
	}

	// Token: 0x06005108 RID: 20744 RVA: 0x00204938 File Offset: 0x00202D38
	private bool IsIdle(Animator _anim)
	{
		if (_anim.runtimeAnimatorController == null)
		{
			return true;
		}
		AnimatorStateInfo currentAnimatorStateInfo = _anim.GetCurrentAnimatorStateInfo(0);
		return currentAnimatorStateInfo.IsName("Idle") || currentAnimatorStateInfo.IsName("WIdle") || currentAnimatorStateInfo.IsName("SIdle") || currentAnimatorStateInfo.IsName("Insert") || currentAnimatorStateInfo.IsName("D_Idle") || currentAnimatorStateInfo.IsName("D_Insert");
	}

	// Token: 0x06005109 RID: 20745 RVA: 0x002049CC File Offset: 0x00202DCC
	private bool IsAfterIdle(Animator _anim, int _mode)
	{
		if (_anim.runtimeAnimatorController == null)
		{
			return true;
		}
		AnimatorStateInfo currentAnimatorStateInfo = _anim.GetCurrentAnimatorStateInfo(0);
		if (_mode != 0)
		{
			if (_mode != 1)
			{
				if (_mode == 2)
				{
					if (currentAnimatorStateInfo.IsName("D_Orgasm_A") || currentAnimatorStateInfo.IsName("D_Orgasm_OUT_A") || currentAnimatorStateInfo.IsName("D_Orgasm_IN_A") || currentAnimatorStateInfo.IsName("D_OrgasmM_OUT_A"))
					{
						return true;
					}
				}
			}
			else if (currentAnimatorStateInfo.IsName("Orgasm_A") || currentAnimatorStateInfo.IsName("Orgasm_OUT_A") || currentAnimatorStateInfo.IsName("Drink_A") || currentAnimatorStateInfo.IsName("Vomit_A") || currentAnimatorStateInfo.IsName("Orgasm_IN_A") || currentAnimatorStateInfo.IsName("OrgasmM_OUT_A"))
			{
				return true;
			}
		}
		else if (currentAnimatorStateInfo.IsName("Orgasm_A") || currentAnimatorStateInfo.IsName("Orgasm_OUT_A") || currentAnimatorStateInfo.IsName("Drink_A") || currentAnimatorStateInfo.IsName("Vomit_A") || currentAnimatorStateInfo.IsName("Orgasm_IN_A") || currentAnimatorStateInfo.IsName("OrgasmM_OUT_A") || currentAnimatorStateInfo.IsName("D_Orgasm_A") || currentAnimatorStateInfo.IsName("D_Orgasm_OUT_A") || currentAnimatorStateInfo.IsName("D_Orgasm_IN_A") || currentAnimatorStateInfo.IsName("D_OrgasmM_OUT_A"))
		{
			return true;
		}
		return false;
	}

	// Token: 0x0600510A RID: 20746 RVA: 0x00204B78 File Offset: 0x00202F78
	public bool setCameraLoad(HScene.AnimationListInfo _info, bool _isForceResetCamera)
	{
		if (_isForceResetCamera || (this.isSetStartPos && Config.HData.InitCamera))
		{
			GlobalMethod.loadCamera(this.ctrlFlag.cameraCtrl, this.hSceneManager.strAssetCameraList, _info.nameCamera, false);
		}
		else
		{
			GlobalMethod.loadResetCamera(this.ctrlFlag.cameraCtrl, this.hSceneManager.strAssetCameraList, _info.nameCamera, false);
		}
		return true;
	}

	// Token: 0x0600510B RID: 20747 RVA: 0x00204BF0 File Offset: 0x00202FF0
	private bool SetPosition(Transform _trans, Vector3 offsetpos, Vector3 offsetrot, bool _FadeStart = true)
	{
		if (_trans == null)
		{
			return false;
		}
		for (int i = 0; i < this.chaMales.Length; i++)
		{
			if (!(this.chaMales[i] == null) && !(this.chaMales[i].objBody == null))
			{
				this.chaMalesTrans[i].position = _trans.position;
				this.chaMalesTrans[i].rotation = _trans.rotation;
				this.chaMalesTrans[i].localPosition += _trans.rotation * offsetpos;
				this.chaMalesTrans[i].localRotation = Quaternion.Euler(offsetrot) * this.chaMalesTrans[i].localRotation;
				this.chaMales[i].animBody.transform.localPosition = Vector3.zero;
				this.chaMales[i].animBody.transform.localRotation = Quaternion.identity;
				this.chaMales[i].SetPosition(Vector3.zero);
				this.chaMales[i].SetRotation(Vector3.zero);
				this.chaMales[i].resetDynamicBoneAll = true;
			}
		}
		if (this.hSceneManager.male != null && this.chaMales[0] != null && this.chaMales[0].objBody != null)
		{
			this.hSceneManager.male.Position = this.chaMalesTrans[0].position;
		}
		for (int j = 0; j < this.chaFemales.Length; j++)
		{
			if (!(this.chaFemales[j] == null) && !(this.chaFemales[j].objBody == null))
			{
				this.chaFemalesTrans[j].position = _trans.position;
				this.chaFemalesTrans[j].rotation = _trans.rotation;
				this.chaFemalesTrans[j].localPosition += _trans.rotation * offsetpos;
				this.chaFemalesTrans[j].localRotation = Quaternion.Euler(offsetrot) * this.chaFemalesTrans[j].localRotation;
				this.chaFemales[j].animBody.transform.localPosition = Vector3.zero;
				this.chaFemales[j].animBody.transform.localRotation = Quaternion.identity;
				this.chaFemales[j].SetPosition(Vector3.zero);
				this.chaFemales[j].SetRotation(Vector3.zero);
				this.chaFemales[j].ResetDynamicBoneAll(false);
				this.chaFemales[j].resetDynamicBoneAll = true;
				if (this.hSceneManager.females != null && this.hSceneManager.females[j] != null)
				{
					this.hSceneManager.females[j].Position = this.chaFemalesTrans[j].position;
				}
			}
		}
		Vector3 vector = this.chaFemales[0].objHeadBone.transform.position;
		vector += this.FeelHitEffect3DOffSet;
		this.FeelHitEffect3D.transform.position = vector;
		if (_FadeStart)
		{
			this.fade.FadeStart(2f);
		}
		this.ctrlItem.setTransform(_trans);
		return true;
	}

	// Token: 0x0600510C RID: 20748 RVA: 0x00204F60 File Offset: 0x00203360
	private bool SetPosition(Vector3 pos, Quaternion rot, Vector3 offsetpos, Vector3 offsetrot, bool _FadeStart = true)
	{
		for (int i = 0; i < this.chaMales.Length; i++)
		{
			if (!(this.chaMales[i] == null) && !(this.chaMales[i].objBody == null))
			{
				this.chaMalesTrans[i].position = pos;
				this.chaMalesTrans[i].rotation = rot;
				this.chaMalesTrans[i].localPosition += rot * offsetpos;
				this.chaMalesTrans[i].localRotation = Quaternion.Euler(offsetrot) * this.chaMalesTrans[i].localRotation;
				this.chaMales[i].animBody.transform.localPosition = Vector3.zero;
				this.chaMales[i].animBody.transform.localRotation = Quaternion.identity;
				this.chaMales[i].SetPosition(Vector3.zero);
				this.chaMales[i].SetRotation(Vector3.zero);
				this.chaMales[i].resetDynamicBoneAll = true;
			}
		}
		for (int j = 0; j < this.chaFemales.Length; j++)
		{
			if (!(this.chaFemales[j] == null) && !(this.chaFemales[j].objBody == null))
			{
				this.chaFemalesTrans[j].position = pos;
				this.chaFemalesTrans[j].rotation = rot;
				this.chaFemalesTrans[j].localPosition += rot * offsetpos;
				this.chaFemalesTrans[j].localRotation = Quaternion.Euler(offsetrot) * this.chaFemalesTrans[j].localRotation;
				this.chaFemales[j].animBody.transform.localPosition = Vector3.zero;
				this.chaFemales[j].animBody.transform.localRotation = Quaternion.identity;
				this.chaFemales[j].SetPosition(Vector3.zero);
				this.chaFemales[j].SetRotation(Vector3.zero);
				this.chaFemales[j].ResetDynamicBoneAll(false);
				this.chaFemales[j].resetDynamicBoneAll = true;
			}
		}
		Vector3 vector = this.chaFemales[0].objHeadBone.transform.position;
		vector += this.FeelHitEffect3DOffSet;
		this.FeelHitEffect3D.transform.position = vector;
		if (_FadeStart)
		{
			this.fade.FadeStart(-1f);
		}
		this.ctrlItem.setTransform(this.chaFemalesTrans[0].position, this.chaFemalesTrans[0].rotation.eulerAngles);
		this.objMetaBallBase.transform.rotation = this.chaFemalesTrans[0].rotation;
		return true;
	}

	// Token: 0x0600510D RID: 20749 RVA: 0x0020523C File Offset: 0x0020363C
	public bool SetMovePositionPoint(Transform trans, Vector3 offsetpos, Vector3 offsetrot)
	{
		this.SetPosition(trans, offsetpos, offsetrot, false);
		GlobalMethod.setCameraBase(this.ctrlFlag.cameraCtrl, this.chaFemalesTrans[0]);
		GlobalMethod.loadCamera(this.ctrlFlag.cameraCtrl, this.hSceneManager.strAssetCameraList, this.ctrlFlag.nowAnimationInfo.nameCamera, false);
		return true;
	}

	// Token: 0x0600510E RID: 20750 RVA: 0x0020529C File Offset: 0x0020369C
	private bool ShortcutKey()
	{
		bool flag = this.sprite.isFade;
		flag |= (this.sprite.GetFadeKindProc() == HSceneSprite.FadeKindProc.OutEnd);
		flag |= (Singleton<Game>.Instance.Config != null);
		flag |= (Singleton<Game>.Instance.Dialog != null);
		flag |= (Singleton<Game>.Instance.ExitScene != null);
		flag |= (Singleton<Game>.Instance.MapShortcutUI != null);
		if (flag)
		{
			return true;
		}
		GlobalMethod.CameraKeyCtrl(this.ctrlFlag.cameraCtrl, this.chaFemales);
		if (UnityEngine.Input.GetKeyDown(KeyCode.F3))
		{
			ConfigWindow.UnLoadAction = delegate()
			{
			};
			ConfigWindow.TitleChangeAction = delegate()
			{
				this.ConfigEnd();
				ConfigWindow.UnLoadAction = null;
				Singleton<Game>.Instance.Dialog.TimeScale = 1f;
			};
			Singleton<Game>.Instance.LoadConfig();
			return true;
		}
		if (!UnityEngine.Input.GetKey(KeyCode.LeftShift) && !UnityEngine.Input.GetKey(KeyCode.RightShift))
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
			{
				Config.HData.EyeDir0 = !Config.HData.EyeDir0;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
			{
				Config.HData.NeckDir0 = !Config.HData.NeckDir0;
			}
		}
		else
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
			{
				Config.HData.EyeDir1 = !Config.HData.EyeDir1;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
			{
				Config.HData.NeckDir1 = !Config.HData.NeckDir1;
			}
		}
		if (!UnityEngine.Input.GetKey(KeyCode.LeftShift) && !UnityEngine.Input.GetKey(KeyCode.RightShift))
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
			{
				Config.HData.Visible = !Config.HData.Visible;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
			{
				Config.HData.Son = !Config.HData.Son;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha5))
			{
				Config.HData.Cloth = !Config.HData.Cloth;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha6))
			{
				Config.HData.Accessory = !Config.HData.Accessory;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha7))
			{
				Config.HData.Shoes = !Config.HData.Shoes;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha8))
			{
				Config.GraphicData.SimpleBody = !Config.GraphicData.SimpleBody;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.T))
			{
				this.sprite.ChangeStateAllEquip();
			}
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha9))
		{
			Config.HData.Siru = (Config.HData.Siru + 1) % 3;
		}
		if (!UnityEngine.Input.GetKey(KeyCode.LeftShift) && !UnityEngine.Input.GetKey(KeyCode.RightShift))
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha0))
			{
				bool[] flags = new bool[]
				{
					this.chaFemales[0].GetSiruFlag(ChaFileDefine.SiruParts.SiruKao) != 0,
					this.chaFemales[0].GetSiruFlag(ChaFileDefine.SiruParts.SiruFrontTop) != 0,
					this.chaFemales[0].GetSiruFlag(ChaFileDefine.SiruParts.SiruFrontBot) != 0,
					this.chaFemales[0].GetSiruFlag(ChaFileDefine.SiruParts.SiruBackTop) != 0,
					this.chaFemales[0].GetSiruFlag(ChaFileDefine.SiruParts.SiruBackBot) != 0
				};
				byte lv = (!GlobalMethod.CheckFlagsArray(flags, 1)) ? 2 : 0;
				if (this.chaFemales[0] && this.chaFemales[0].visibleAll && this.chaFemales[0].objBody != null)
				{
					for (ChaFileDefine.SiruParts siruParts = ChaFileDefine.SiruParts.SiruKao; siruParts < (ChaFileDefine.SiruParts)5; siruParts++)
					{
						this.chaFemales[0].SetSiruFlag(siruParts, lv);
					}
				}
			}
		}
		else if (this.chaFemales[1] && this.chaFemales[1].visibleAll && this.chaFemales[1].objBody != null && UnityEngine.Input.GetKeyDown(KeyCode.Alpha0))
		{
			bool[] flags2 = new bool[]
			{
				this.chaFemales[1].GetSiruFlag(ChaFileDefine.SiruParts.SiruKao) != 0,
				this.chaFemales[1].GetSiruFlag(ChaFileDefine.SiruParts.SiruFrontTop) != 0,
				this.chaFemales[1].GetSiruFlag(ChaFileDefine.SiruParts.SiruFrontBot) != 0,
				this.chaFemales[1].GetSiruFlag(ChaFileDefine.SiruParts.SiruBackTop) != 0,
				this.chaFemales[1].GetSiruFlag(ChaFileDefine.SiruParts.SiruBackBot) != 0
			};
			byte lv2 = (!GlobalMethod.CheckFlagsArray(flags2, 1)) ? 2 : 0;
			for (ChaFileDefine.SiruParts siruParts2 = ChaFileDefine.SiruParts.SiruKao; siruParts2 < (ChaFileDefine.SiruParts)5; siruParts2++)
			{
				this.chaFemales[1].SetSiruFlag(siruParts2, lv2);
			}
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.A))
		{
			Config.HData.FeelingGauge = !Config.HData.FeelingGauge;
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.S))
		{
			Config.HData.MenuIcon = !Config.HData.MenuIcon;
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.D))
		{
			Config.HData.FinishButton = !Config.HData.FinishButton;
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.F))
		{
			Config.HData.InitCamera = !Config.HData.InitCamera;
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
		{
			Config.ActData.Look = !Config.ActData.Look;
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.X))
		{
			this.isTuyaOn = !this.isTuyaOn;
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.C))
		{
			Config.HData.ActionGuide = !Config.HData.ActionGuide;
		}
		return true;
	}

	// Token: 0x0600510F RID: 20751 RVA: 0x00205864 File Offset: 0x00203C64
	private bool GetAutoAnimation(bool _isFirst)
	{
		this.autoMotion = this.ctrlAuto.GetAnimation(this.lstAnimInfo, this.ctrlFlag.initiative, _isFirst);
		if (this.autoMotion == null || this.lstAnimInfo.Length <= this.autoMotion.mode || this.autoMotion.mode == -1)
		{
			return false;
		}
		foreach (HScene.AnimationListInfo animationListInfo in this.lstAnimInfo[this.autoMotion.mode])
		{
			if (animationListInfo.id == this.autoMotion.id)
			{
				this.ctrlFlag.selectAnimationListInfo = animationListInfo;
				break;
			}
		}
		return true;
	}

	// Token: 0x06005110 RID: 20752 RVA: 0x0020594C File Offset: 0x00203D4C
	private int GetAnimationListModeFromSelectInfo(HScene.AnimationListInfo _info)
	{
		if (_info == null)
		{
			return -1;
		}
		int result = -1;
		for (int i = 0; i < this.lstAnimInfo.Length; i++)
		{
			for (int j = 0; j < this.lstAnimInfo[i].Count; j++)
			{
				if (this.lstAnimInfo[i][j] == _info)
				{
					result = i;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06005111 RID: 20753 RVA: 0x002059BB File Offset: 0x00203DBB
	private bool SetStartVoice()
	{
		if (this.hSceneManager.EventKind != HSceneManager.HEvent.Normal)
		{
			return false;
		}
		this.ctrlFlag.voice.playStart = 0;
		return true;
	}

	// Token: 0x06005112 RID: 20754 RVA: 0x002059E4 File Offset: 0x00203DE4
	private void SetClothStateStartMotion(int _cha, HScene.AnimationListInfo info)
	{
		if (this.chaFemales.Length <= _cha)
		{
			return;
		}
		byte state = (byte)info.nFemaleUpperCloths[_cha];
		byte state2 = (byte)info.nFemaleLowerCloths[_cha];
		GlobalMethod.SetAllClothState(this.chaFemales[_cha], true, (int)state, false);
		GlobalMethod.SetAllClothState(this.chaFemales[_cha], false, (int)state2, false);
	}

	// Token: 0x06005113 RID: 20755 RVA: 0x00205A34 File Offset: 0x00203E34
	private bool ReturnToNormalFromTheAuto()
	{
		int animationListModeFromSelectInfo = this.GetAnimationListModeFromSelectInfo(this.ctrlFlag.nowAnimationInfo);
		if (animationListModeFromSelectInfo != -1)
		{
			foreach (HScene.AnimationListInfo animationListInfo in this.lstAnimInfo[animationListModeFromSelectInfo])
			{
				if (animationListInfo.id == this.ctrlFlag.nowAnimationInfo.nBackInitiativeID)
				{
					this.ctrlFlag.selectAnimationListInfo = animationListInfo;
					break;
				}
			}
		}
		return true;
	}

	// Token: 0x06005114 RID: 20756 RVA: 0x00205AD8 File Offset: 0x00203ED8
	private void EndProcADV()
	{
		if (!this.hSceneManager.bMerchant)
		{
			switch (this.hSceneManager.EventKind)
			{
			case HSceneManager.HEvent.Yobai:
			{
				ADV.ChangeADVCamera(this.hSceneManager.Agent[0].GetComponent<AgentActor>());
				this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
				this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
				this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
				this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
				this.hSceneManager.HSceneUISet.SetActive(false);
				ShuffleRand shuffleRand = new ShuffleRand(100);
				if (shuffleRand.Get() < this.ctrlFlag.YobaiBareRate)
				{
					this._bareYobai = true;
					this.openData.FindLoad("11", this.packData.charaID, this.packData.adv_category);
					this.packData.onComplete = delegate()
					{
						this.CloseADV();
					};
					Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
				}
				else
				{
					this._bareYobai = false;
					int personality = 0;
					if (this.hSceneManager.females[0].GetComponent<PlayerActor>())
					{
						personality = -100;
					}
					else if (this.hSceneManager.females[0].GetComponent<MerchantActor>() == null)
					{
						personality = this.hSceneManager.females[0].ChaControl.fileParam.personality;
					}
					Game.Expression expression = Singleton<Game>.Instance.GetExpression(personality, "標準（目閉じ）");
					if (expression != null)
					{
						expression.Change(this.hSceneManager.females[0].ChaControl);
					}
					Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
					{
						this.CloseADV();
					});
				}
				break;
			}
			case HSceneManager.HEvent.Bath:
			case HSceneManager.HEvent.Toilet1:
			case HSceneManager.HEvent.Toilet2:
			case HSceneManager.HEvent.ShagmiBare:
			case HSceneManager.HEvent.Back:
			case HSceneManager.HEvent.Kitchen:
			case HSceneManager.HEvent.Tachi:
			case HSceneManager.HEvent.Stairs:
			case HSceneManager.HEvent.StairsBare:
			case HSceneManager.HEvent.MapBath:
			case HSceneManager.HEvent.KabeanaBack:
			case HSceneManager.HEvent.KabeanaFront:
			case HSceneManager.HEvent.Neonani:
			case HSceneManager.HEvent.TsukueBare:
				if (!this.ctrlFlag.isFaintness)
				{
					this.openData.FindLoad("9", this.packData.charaID, this.packData.adv_category);
				}
				else
				{
					this.openData.FindLoad("1", this.packData.charaID, this.packData.adv_category);
				}
				this.packData.onComplete = delegate()
				{
					this.CloseADV();
				};
				ADV.ChangeADVCamera(this.hSceneManager.Agent[0].GetComponent<AgentActor>());
				this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
				this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
				this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
				this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
				Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
				break;
			case HSceneManager.HEvent.GyakuYobai:
				if (!HSceneManager.SleepStart)
				{
					if (!this.ctrlFlag.isFaintness)
					{
						this.openData.FindLoad("7", this.packData.charaID, this.packData.adv_category);
					}
					else
					{
						this.openData.FindLoad("1", this.packData.charaID, this.packData.adv_category);
					}
				}
				else if (!this.ctrlFlag.isFaintness)
				{
					this.openData.FindLoad("13", this.packData.charaID, this.packData.adv_category);
				}
				else
				{
					this.openData.FindLoad("14", this.packData.charaID, this.packData.adv_category);
				}
				this.packData.onComplete = delegate()
				{
					this.CloseADV();
				};
				ADV.ChangeADVCamera(this.hSceneManager.Agent[0].GetComponent<AgentActor>());
				this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
				this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
				this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
				this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
				Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
				break;
			default:
				if (this.hSceneManager.EventKind != HSceneManager.HEvent.FromFemale || this.hSceneManager.nInvitePtn != -1)
				{
					if (this.hSceneManager.females[1] == null || this.hSceneManager.females[1].GetComponent<PlayerActor>() != null)
					{
						bool flag = this.hSceneManager.femalePlayerFinish == 0 && this.hSceneManager.maleFinish == 0 && this.ctrlFlag.numOrgasmTotal == 0;
						bool flag2 = this.ctrlFlag.isJudgeSelect.Contains(HSceneFlagCtrl.JudgeSelect.Constraint);
						bool flag3 = this.ctrlFlag.isJudgeSelect.Contains(HSceneFlagCtrl.JudgeSelect.Pain);
						if ((flag2 || flag3) && (this.hSceneManager.isForce || this.hSceneManager.GetFlaverSkillLevel(2) < 170))
						{
							if (flag3)
							{
								if (!this.ctrlFlag.isFaintness)
								{
									this.openData.FindLoad("5", this.packData.charaID, this.packData.adv_category);
									this.packData.isPainAction = this.ctrlFlag.isPainAction;
								}
								else
								{
									this.openData.FindLoad("1", this.packData.charaID, this.packData.adv_category);
								}
								this.packData.ConditionMode = 2;
								this.packData.onComplete = delegate()
								{
									this.CloseADV();
								};
								ADV.ChangeADVCamera(this.hSceneManager.Agent[0]);
								this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
								this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
								this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
								this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
								Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
							}
							else if (flag2)
							{
								if (!this.ctrlFlag.isFaintness)
								{
									this.openData.FindLoad("12", this.packData.charaID, this.packData.adv_category);
									this.packData.isConstraintAction = this.ctrlFlag.isConstraintAction;
								}
								else
								{
									this.openData.FindLoad("1", this.packData.charaID, this.packData.adv_category);
								}
								this.packData.ConditionMode = 2;
								this.packData.onComplete = delegate()
								{
									this.CloseADV();
								};
								ADV.ChangeADVCamera(this.hSceneManager.Agent[0]);
								this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
								this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
								this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
								this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
								Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
							}
						}
						else if (!this.ctrlFlag.isNotCtrl && !flag)
						{
							if (this.hSceneManager.femalePlayerFinish > 0 || this.hSceneManager.maleFinish > 0)
							{
								if (this.ctrlFlag.numOrgasmTotal == 0)
								{
									if (!this.ctrlFlag.isFaintness)
									{
										this.openData.FindLoad("3", this.packData.charaID, this.packData.adv_category);
									}
									else
									{
										this.openData.FindLoad("1", this.packData.charaID, this.packData.adv_category);
									}
									if (this.hSceneManager.isForce)
									{
										this.packData.ConditionMode = 2;
									}
									else
									{
										this.packData.ConditionMode = ((this.hSceneManager.Agent[0].ChaControl.fileGameInfo.phase >= 2) ? 1 : 0);
									}
									this.packData.onComplete = delegate()
									{
										this.CloseADV();
									};
									ADV.ChangeADVCamera(this.hSceneManager.Agent[0]);
									this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
									this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
									this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
									this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
									Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
								}
								else if (!this.ctrlFlag.isFaintness)
								{
									this.openData.FindLoad("0", this.packData.charaID, this.packData.adv_category);
									this.packData.numOrgasmFemale = this.ctrlFlag.numOrgasmTotal;
									if (this.hSceneManager.isForce)
									{
										this.packData.ConditionMode = 2;
									}
									else
									{
										this.packData.ConditionMode = ((this.hSceneManager.Agent[0].ChaControl.fileGameInfo.phase >= 2) ? 1 : 0);
									}
									this.packData.onComplete = delegate()
									{
										this.CloseADV();
									};
									ADV.ChangeADVCamera(this.hSceneManager.Agent[0]);
									this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
									this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
									this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
									this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
									Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
								}
								else
								{
									this.openData.FindLoad("1", this.packData.charaID, this.packData.adv_category);
									if (this.hSceneManager.isForce)
									{
										this.packData.ConditionMode = 2;
									}
									else
									{
										this.packData.ConditionMode = ((this.hSceneManager.Agent[0].ChaControl.fileGameInfo.phase >= 2) ? 1 : 0);
									}
									this.packData.onComplete = delegate()
									{
										this.CloseADV();
									};
									ADV.ChangeADVCamera(this.hSceneManager.Agent[0]);
									this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
									this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
									this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
									this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
									Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
								}
							}
							else
							{
								if (!this.ctrlFlag.isFaintness)
								{
									this.openData.FindLoad("2", this.packData.charaID, this.packData.adv_category);
								}
								else
								{
									this.openData.FindLoad("1", this.packData.charaID, this.packData.adv_category);
								}
								if (this.hSceneManager.isForce)
								{
									this.packData.ConditionMode = 2;
								}
								else
								{
									this.packData.ConditionMode = ((this.hSceneManager.Agent[0].ChaControl.fileGameInfo.phase >= 2) ? 1 : 0);
								}
								this.packData.onComplete = delegate()
								{
									this.CloseADV();
								};
								ADV.ChangeADVCamera(this.hSceneManager.Agent[0]);
								this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
								this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
								this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
								this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
								Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
							}
						}
						else
						{
							this.openData.FindLoad("4", this.packData.charaID, this.packData.adv_category);
							if (this.hSceneManager.isForce)
							{
								this.packData.ConditionMode = 2;
							}
							else
							{
								this.packData.ConditionMode = ((this.hSceneManager.Agent[0].ChaControl.fileGameInfo.phase >= 2) ? 1 : 0);
							}
							this.packData.onComplete = delegate()
							{
								this.CloseADV();
							};
							ADV.ChangeADVCamera(this.hSceneManager.Agent[0].GetComponent<AgentActor>());
							this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
							this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
							this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
							this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
							Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
						}
					}
					else if (this.hSceneManager.females[1] != null && this.hSceneManager.females[1].GetComponent<PlayerActor>() == null)
					{
						if (!this.ctrlFlag.isFaintness)
						{
							this.openData.FindLoad("10", this.packData.charaID, this.packData.adv_category);
						}
						else
						{
							this.openData.FindLoad("1", this.packData.charaID, this.packData.adv_category);
						}
						this.packData.onComplete = delegate()
						{
							this.CloseADV();
						};
						ADV.ChangeADVCamera(this.hSceneManager.Agent[0].GetComponent<AgentActor>());
						this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
						this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
						this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
						this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
				else if (this.hSceneManager.EventKind == HSceneManager.HEvent.FromFemale || this.hSceneManager.nInvitePtn == 0)
				{
					if (!this.ctrlFlag.isFaintness)
					{
						this.openData.FindLoad("6", this.packData.charaID, this.packData.adv_category);
					}
					else
					{
						this.openData.FindLoad("1", this.packData.charaID, this.packData.adv_category);
					}
					if (this.hSceneManager.isForce)
					{
						this.packData.ConditionMode = 2;
					}
					else
					{
						this.packData.ConditionMode = ((this.hSceneManager.Agent[0].ChaControl.fileGameInfo.phase >= 2) ? 1 : 0);
					}
					this.packData.onComplete = delegate()
					{
						this.CloseADV();
					};
					ADV.ChangeADVCamera(this.hSceneManager.Agent[0].GetComponent<AgentActor>());
					this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
					this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
					this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
					this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
					Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
				}
				else if (this.hSceneManager.nInvitePtn == 1)
				{
					if (!this.ctrlFlag.isFaintness)
					{
						this.openData.FindLoad("8", this.packData.charaID, this.packData.adv_category);
					}
					else
					{
						this.openData.FindLoad("1", this.packData.charaID, this.packData.adv_category);
					}
					if (this.hSceneManager.isForce)
					{
						this.packData.ConditionMode = 2;
					}
					else
					{
						this.packData.ConditionMode = ((this.hSceneManager.Agent[0].ChaControl.fileGameInfo.phase >= 2) ? 1 : 0);
					}
					this.packData.onComplete = delegate()
					{
						this.CloseADV();
					};
					ADV.ChangeADVCamera(this.hSceneManager.Agent[0].GetComponent<AgentActor>());
					this.hSceneManager.females[0].ChaControl.transform.localPosition = Vector3.zero;
					this.hSceneManager.females[0].ChaControl.transform.localRotation = Quaternion.identity;
					this.hSceneManager.females[0].ChaControl.animBody.transform.position = this.hSceneManager.females[0].Position;
					this.hSceneManager.females[0].ChaControl.animBody.transform.rotation = this.hSceneManager.females[0].Rotation;
					Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
				}
				break;
			}
		}
		else
		{
			this.openData.FindLoad("0", this.packData.charaID, this.packData.adv_category);
			this.packData.onComplete = delegate()
			{
				this.CloseADV();
			};
			ADV.ChangeADVCamera(this.hSceneManager.merchantActor);
			this.packData.JumpTag = "OUT";
			this.packData.isWeaknessH = this.ctrlFlag.isFaintness;
			Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
		}
	}

	// Token: 0x06005115 RID: 20757 RVA: 0x002071C0 File Offset: 0x002055C0
	private void CloseADV()
	{
		if (this.packData != null)
		{
			this.packData.Release();
		}
		int num = Config.GraphicData.MaxCharaNum - 1;
		if (!MapScene.EqualsSequence(this.prevCharaEntry, Config.GraphicData.CharasEntry))
		{
			Singleton<Manager.Map>.Instance.ApplyConfig(delegate
			{
				this.CloseADVProc();
			}, null);
		}
		else
		{
			this.CloseADVProc();
		}
	}

	// Token: 0x06005116 RID: 20758 RVA: 0x0020722C File Offset: 0x0020562C
	private void CloseADVProc()
	{
		for (int i = 0; i < this.hSceneManager.females.Length; i++)
		{
			this.hSceneManager.females[i] = null;
		}
		this.hSceneManager.male = null;
		MerchantActor merchant = Singleton<Manager.Map>.Instance.Merchant;
		if (this.hSceneManager.Player.CameraControl.Mode != CameraMode.Normal)
		{
			this.hSceneManager.Player.CameraControl.Mode = CameraMode.Normal;
		}
		int shortNameHash = this.hSceneManager.Player.ChaControl.animBody.GetCurrentAnimatorStateInfo(0).shortNameHash;
		this.hSceneManager.Player.ChaControl.SetAccessoryStateAll(true);
		this.hSceneManager.Player.ChaControl.SetClothesStateAll(0);
		this.hSceneManager.Player.Animation.enabled = true;
		this.hSceneManager.Player.Animation.SetAnimatorController(this.hSceneManager.Player.ChaControl.animBody.runtimeAnimatorController);
		this.hSceneManager.Player.Animation.MapIK.Calc(shortNameHash);
		this.hSceneManager.Player.Controller.enabled = true;
		this.hSceneManager.Player.CameraControl.enabled = true;
		if (!this.hSceneManager.Player.NavMeshAgent.isOnNavMesh)
		{
			Vector3 position = this.hSceneManager.Player.Position;
			this.hSceneManager.Player.NavMeshWarp(position, this.hSceneManager.Player.Rotation, 3, 200f);
		}
		this.hSceneManager.Player.ChaControl.transform.position = Vector3.zero;
		this.hSceneManager.Player.ChaControl.transform.rotation = Quaternion.identity;
		if (this.hSceneManager.Player.CurrentPoint != null)
		{
			this.hSceneManager.Player.CurrentPoint.RemoveBookingUser(this.hSceneManager.Player);
		}
		this.hSceneManager.Player.ReleaseCurrentPoint();
		if (!this.hSceneManager.bMerchant)
		{
			for (int j = 0; j < this.hSceneManager.Agent.Length; j++)
			{
				if (!(this.hSceneManager.Agent[j] == null))
				{
					this.hSceneManager.Agent[j].ChaControl.transform.position = Vector3.zero;
					this.hSceneManager.Agent[j].ChaControl.transform.rotation = Quaternion.identity;
					this.hSceneManager.Agent[j].ChaControl.ChangeNowCoordinate(true, true);
				}
			}
		}
		else
		{
			merchant.ChaControl.transform.position = Vector3.zero;
			merchant.ChaControl.transform.rotation = Quaternion.identity;
		}
		if (!this.hSceneManager.Player.ChaControl.visibleAll)
		{
			this.hSceneManager.Player.ChaControl.visibleAll = true;
		}
		this.hSceneManager.Player.Controller.ChangeState("Normal");
		if (this.hSceneManager.Player.ChaControl.sex == 1 && !this.hSceneManager.bFutanari)
		{
			this.hSceneManager.Player.ChaControl.ChangeNowCoordinate(true, true);
		}
		if (this.hSceneManager.Player.Partner != null)
		{
			AgentActor component = this.hSceneManager.Player.Partner.GetComponent<AgentActor>();
			component.DeactivatePairing(0);
			component.ActivateHoldingHands(0, false);
		}
		int num = 0;
		foreach (AgentActor agentActor in Singleton<Manager.Map>.Instance.AgentTable.Values)
		{
			if (!(agentActor == null))
			{
				if (!this.hSceneManager.ReturnActionTypes.ContainsKey(agentActor))
				{
					shortNameHash = agentActor.ChaControl.animBody.GetCurrentAnimatorStateInfo(0).shortNameHash;
					agentActor.AnimationAgent.enabled = true;
					agentActor.AnimationAgent.StopAllAnimCoroutine();
					agentActor.AnimationAgent.EndIgnoreExpression();
					agentActor.AnimationAgent.ResetDefaultAnimatorController();
					agentActor.AnimationAgent.SetAnimatorController(agentActor.ChaControl.animBody.runtimeAnimatorController);
					agentActor.AnimationAgent.MapIK.Calc(shortNameHash);
					int desireKey = Desire.GetDesireKey(Desire.Type.H);
					agentActor.SetDesire(desireKey, 0f);
					this.EmptyImmoral(agentActor);
					agentActor.Controller.enabled = true;
					agentActor.AnimationAgent.EnableItems();
					agentActor.EnableBehavior();
					agentActor.LocomotorAgent.enabled = true;
					agentActor.ActivateNavMeshAgent();
					if (!agentActor.NavMeshAgent.isOnNavMesh)
					{
						Vector3 position2 = agentActor.Position;
						agentActor.NavMeshWarp(position2, agentActor.Rotation, num++, 200f);
					}
					agentActor.ChaControl.visibleAll = true;
					agentActor.ResetActionFlag();
					if (this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai)
					{
						if (this._bareYobai)
						{
							agentActor.ChangeBehavior(Desire.ActionType.Normal);
						}
						else
						{
							agentActor.RecoverAction();
						}
					}
					else
					{
						if (this.hSceneManager.endStatus == 1)
						{
							if (!this.ctrlFlag.nowHPoint._nPlace.Exists((KeyValuePair<int, UnityEx.ValueTuple<int, int>> x) => x.Value.Item1 == 11 || x.Value.Item1 == 13 || x.Value.Item1 == 14))
							{
								agentActor.StartWeakness();
								continue;
							}
						}
						agentActor.ChangeBehavior(Desire.ActionType.Normal);
					}
				}
			}
		}
		if (this.hSceneManager.bMerchant)
		{
			shortNameHash = merchant.ChaControl.animBody.GetCurrentAnimatorStateInfo(0).shortNameHash;
			merchant.AnimationMerchant.enabled = true;
			merchant.AnimationMerchant.EndIgnoreExpression();
			merchant.AnimationMerchant.SetAnimatorController(merchant.ChaControl.animBody.runtimeAnimatorController);
			merchant.AnimationMerchant.MapIK.Calc(shortNameHash);
			merchant.Controller.enabled = true;
			merchant.LocomotorMerchant.enabled = true;
			merchant.EnableBehavior();
			merchant.ChangeBehavior(merchant.LastNormalMode);
			merchant.ChaControl.ChangeNowCoordinate(true, true);
		}
		this.hSceneManager.Player.enabled = true;
		this.hSceneManager.Player.HandsHolder.enabled = this.hSceneManager.handsIK;
		this.hSceneManager.Player.SetActiveOnEquipedItem(true);
		foreach (AgentActor agentActor2 in this.hSceneManager.Agent)
		{
			if (!(agentActor2 == null))
			{
				agentActor2.SetActiveOnEquipedItem(true);
			}
		}
		if (this.hSceneManager.HSceneSet.activeSelf)
		{
			this.hSceneManager.HSceneSet.SetActive(false);
		}
		Singleton<ADV>.Instance.Captions.EndADV(null);
		Singleton<MapUIContainer>.Instance.MinimapUI.MiniMap.SetActive(true);
		Singleton<MapUIContainer>.Instance.MinimapUI.MiniMapIcon.SetActive(true);
		Singleton<Manager.Map>.Instance.SetActiveMapEffect(true);
		MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
		Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
		{
			MapUIContainer.SetVisibleHUD(true);
		});
	}

	// Token: 0x06005117 RID: 20759 RVA: 0x00207A1C File Offset: 0x00205E1C
	private RuntimeAnimatorController MixRuntimeControler(RuntimeAnimatorController src, RuntimeAnimatorController over1, RuntimeAnimatorController over2)
	{
		if (src == null || over1 == null || over2 == null)
		{
			return null;
		}
		AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(src);
		List<AnimationClip> list = new List<AnimationClip>();
		list.AddRange(new AnimatorOverrideController(over1).animationClips);
		list.AddRange(new AnimatorOverrideController(over2).animationClips);
		foreach (AnimationClip animationClip in list)
		{
			animatorOverrideController[animationClip.name] = animationClip;
		}
		animatorOverrideController.name = over1.name;
		return animatorOverrideController;
	}

	// Token: 0x06005118 RID: 20760 RVA: 0x00207ADC File Offset: 0x00205EDC
	private void EndAnimChange()
	{
		List<HScene.EndMotion> lstEndAnimInfo = Singleton<Manager.Resources>.Instance.HSceneTable.lstEndAnimInfo;
		HScene.AnimationListInfo nowAnimationInfo = this.ctrlFlag.nowAnimationInfo;
		int index;
		if (this.hSceneManager.females[0].GetComponent<MerchantActor>() == null)
		{
			index = this.chaFemales[0].fileParam.personality;
		}
		else
		{
			index = 5;
		}
		int shortNameHash;
		if (!this.hSceneManager.bMerchant)
		{
			this.chaFemales[0].LoadAnimation(lstEndAnimInfo[index].abNameH[0], lstEndAnimInfo[index].assetNameH[0], string.Empty);
			this.chaFemales[0].setPlay(lstEndAnimInfo[index].stateNameH[0], 0);
			shortNameHash = this.chaFemales[0].animBody.GetCurrentAnimatorStateInfo(0).shortNameHash;
			this.chaFemales[0].animBody.GetComponent<ActorAnimationAgent>().MapIK.Calc(shortNameHash);
		}
		else
		{
			this.chaFemales[0].LoadAnimation(lstEndAnimInfo[index].abNameM, lstEndAnimInfo[index].assetNameM, string.Empty);
			this.chaFemales[0].setPlay(lstEndAnimInfo[index].stateNameM, 0);
			shortNameHash = this.chaFemales[0].animBody.GetCurrentAnimatorStateInfo(0).shortNameHash;
			this.chaFemales[0].animBody.GetComponent<ActorAnimationMerchant>().MapIK.Calc(shortNameHash);
		}
		if (this.chaFemales[1] != null && this.chaFemales[1].objTop != null && this.chaFemales[1] != this.hSceneManager.Player.ChaControl)
		{
			this.chaFemales[1].LoadAnimation(lstEndAnimInfo[index].abNameH[1], lstEndAnimInfo[index].assetNameH[1], string.Empty);
			this.chaFemales[1].setPlay(lstEndAnimInfo[index].stateNameH[1], 0);
			shortNameHash = this.chaFemales[1].animBody.GetCurrentAnimatorStateInfo(0).shortNameHash;
			this.chaFemales[1].animBody.GetComponent<ActorAnimationAgent>().MapIK.Calc(shortNameHash);
		}
		this.hSceneManager.Player.ChaControl.LoadAnimation(lstEndAnimInfo[index].abNameP[(int)this.hSceneManager.Player.ChaControl.sex], lstEndAnimInfo[index].assetNameP[(int)this.hSceneManager.Player.ChaControl.sex], string.Empty);
		this.hSceneManager.Player.ChaControl.setPlay(lstEndAnimInfo[index].stateNameP[(int)this.hSceneManager.Player.ChaControl.sex], 0);
		shortNameHash = this.hSceneManager.Player.ChaControl.animBody.GetCurrentAnimatorStateInfo(0).shortNameHash;
		this.hSceneManager.Player.ChaControl.animBody.GetComponent<ActorAnimationPlayer>().MapIK.Calc(shortNameHash);
		this.fade.FadeStart(1f);
	}

	// Token: 0x06005119 RID: 20761 RVA: 0x00207E2C File Offset: 0x0020622C
	private IEnumerator BeforeWait()
	{
		if (this.preBeforWaitState == null)
		{
			this.preBeforWaitState = new Dictionary<int, int>();
		}
		else
		{
			this.preBeforWaitState.Clear();
		}
		int ID = -1;
		if (this.hSceneManager.EventKind == HSceneManager.HEvent.Normal)
		{
			switch (this.hSceneManager.height)
			{
			case 0:
				ID = 2;
				goto IL_10F;
			case 2:
				ID = 3;
				goto IL_10F;
			case 3:
			case 6:
			case 7:
				ID = 1;
				goto IL_10F;
			case 4:
				if (this.hSceneManager.onDeskChair)
				{
					ID = 5;
				}
				else
				{
					ID = 4;
				}
				goto IL_10F;
			}
			ID = 0;
			IL_10F:
			if (!this.hSceneManager.bMerchant && this.hSceneManager.Agent[0].ActionID == 0 && this.hSceneManager.Agent[0].PoseID == 3)
			{
				ID = 2;
			}
		}
		if (this.hSceneManager.females[1] != null && this.hSceneManager.females[1].GetComponent<PlayerActor>() == null)
		{
			ID = 0;
		}
		foreach (StartWaitAnim startWaitAnim in Singleton<Manager.Resources>.Instance.HSceneTable.startWaitAnims)
		{
			if (startWaitAnim.ID == ID)
			{
				this.startWait = startWaitAnim;
			}
		}
		this.Camera = this.hSceneManager.Player.CameraControl;
		Vector3 pos = this.chaFemalesTrans[0].position;
		GlobalMethod.setCameraBase(Singleton<HSceneFlagCtrl>.Instance.cameraCtrl, pos, this.chaFemalesTrans[0].rotation.eulerAngles);
		this.Camera.HCamera = this.ctrlFlag.cameraCtrl;
		Transform parent = this.ctrlFlag.HBeforeCamera.transform.parent;
		parent.position = pos;
		parent.rotation = this.chaFemalesTrans[0].rotation;
		this.ctrlFlag.HBeforeCamera.gameObject.SetActive(true);
		if (this.startWait == null)
		{
			GlobalMethod.loadCamera(this.ctrlFlag.HBeforeCamera, this.hSceneManager.strAssetCameraList, "DefaultCameraData", false);
		}
		else
		{
			GlobalMethod.loadCamera(this.ctrlFlag.HBeforeCamera, this.hSceneManager.strAssetCameraList, this.startWait.CameraFile, false);
		}
		this.ctrlFlag.cameraCtrl.transform.position = this.ctrlFlag.HBeforeCamera.transform.position;
		this.ctrlFlag.cameraCtrl.transform.rotation = this.ctrlFlag.HBeforeCamera.transform.rotation;
		yield return null;
		if (this.startWait == null)
		{
			yield break;
		}
		this.hSceneManager.Player.ChaControl.transform.position = this.chaFemalesTrans[0].position;
		this.hSceneManager.Player.ChaControl.transform.rotation = this.chaFemalesTrans[0].rotation;
		this.hSceneManager.Player.ChaControl.animBody.transform.localPosition = Vector3.zero;
		this.hSceneManager.Player.ChaControl.animBody.transform.localRotation = Quaternion.identity;
		if (this.startWait.VisibleMode != 1)
		{
			this.hSceneManager.Player.ChaControl.visibleAll = true;
			this.hSceneManager.Player.SetActiveOnEquipedItem(false);
			this.hSceneManager.Player.ChaControl.setAllLayerWeight(0f);
			int num;
			if (Singleton<Manager.Map>.Instance.Player.ChaControl.sex == 0 || this.hSceneManager.bFutanari)
			{
				num = 0;
			}
			else
			{
				num = 1;
			}
			this.racBeforeWait = CommonLib.LoadAsset<RuntimeAnimatorController>(this.startWait.Player[num].abName, this.startWait.Player[num].assetName, false, string.Empty);
			if (this.racBeforeWait)
			{
				this.hSceneManager.Player.Animation.SetAnimatorController(this.racBeforeWait);
				this.hSceneManager.Player.ChaControl.setPlay(this.startWait.Player[num].State, 0);
				AssetBundleManager.UnloadAssetBundle(this.startWait.Player[num].abName, true, null, false);
				AnimatorStateInfo animState = this.hSceneManager.Player.ChaControl.animBody.GetCurrentAnimatorStateInfo(0);
				int animStateHash = animState.shortNameHash;
				this.preBeforWaitState.Add(0, animStateHash);
				this.hSceneManager.Player.Animation.MapIK.Calc(animStateHash);
			}
		}
		for (int i = 0; i < this.hSceneManager.Agent.Length; i++)
		{
			if (!(this.hSceneManager.Agent[i] == null) && !(this.hSceneManager.Agent[i].ChaControl == null) && !(this.hSceneManager.Agent[i].ChaControl.animBody == null))
			{
				this.racBeforeWait = CommonLib.LoadAsset<RuntimeAnimatorController>(this.startWait.Agent[i].abName, this.startWait.Agent[i].assetName, false, string.Empty);
				if (!(this.racBeforeWait == null))
				{
					this.hSceneManager.Agent[i].AnimationAgent.SetAnimatorController(this.racBeforeWait);
					this.hSceneManager.Agent[i].ChaControl.setPlay(this.startWait.Agent[i].State, 0);
					float shapeBodyValue = this.hSceneManager.Agent[i].ChaControl.GetShapeBodyValue(0);
					this.hSceneManager.Agent[i].AnimationAgent.SetFloat("height", shapeBodyValue);
					AssetBundleManager.UnloadAssetBundle(this.startWait.Agent[i].abName, true, null, false);
					AnimatorStateInfo animState = this.hSceneManager.Agent[i].ChaControl.animBody.GetCurrentAnimatorStateInfo(0);
					int animStateHash = animState.shortNameHash;
					this.preBeforWaitState.Add(1 + i, animStateHash);
					this.hSceneManager.Agent[i].AnimationAgent.MapIK.Calc(animStateHash);
				}
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x0600511A RID: 20762 RVA: 0x00207E48 File Offset: 0x00206248
	public void LoadMoveOffset(string file, out Vector3 pos, out Vector3 rot)
	{
		string text = GlobalMethod.LoadAllListText(this.hSceneManager.strAssetMoveOffsetListFolder, file, null);
		string[][] array;
		GlobalMethod.GetListString(text, out array);
		pos = Vector3.zero;
		rot = Vector3.zero;
		int length = array.GetLength(0);
		for (int i = 0; i < length; i++)
		{
			int num = 0;
			pos.x = float.Parse(array[i][num++]);
			pos.y = float.Parse(array[i][num++]);
			pos.z = float.Parse(array[i][num++]);
			rot.x = float.Parse(array[i][num++]);
			rot.y = float.Parse(array[i][num++]);
			rot.z = float.Parse(array[i][num++]);
		}
	}

	// Token: 0x0600511B RID: 20763 RVA: 0x00207F27 File Offset: 0x00206327
	private void EmptyImmoral(AgentActor agent)
	{
		agent.SetDefaultImmoral();
	}

	// Token: 0x0600511C RID: 20764 RVA: 0x00207F2F File Offset: 0x0020632F
	public ChaControl[] GetFemales()
	{
		return this.chaFemales;
	}

	// Token: 0x0600511D RID: 20765 RVA: 0x00207F38 File Offset: 0x00206338
	private void HousingVanishSet()
	{
		if (Singleton<Manager.Map>.Instance.PointAgent != null)
		{
			BasePoint[] basePoints = Singleton<Manager.Map>.Instance.PointAgent.BasePoints;
			if (basePoints != null)
			{
				int mapID = Singleton<Manager.Map>.Instance.MapID;
				int num = -1;
				Dictionary<int, Dictionary<int, List<int>>> vanishHousingAreaGroup = Singleton<Manager.Resources>.Instance.Map.VanishHousingAreaGroup;
				Dictionary<int, List<int>> dictionary;
				if (vanishHousingAreaGroup != null && vanishHousingAreaGroup.TryGetValue(mapID, out dictionary))
				{
					foreach (KeyValuePair<int, List<int>> keyValuePair in dictionary)
					{
						if (keyValuePair.Value.Contains(this.hSceneManager.Player.AreaID))
						{
							num = keyValuePair.Key;
							break;
						}
					}
					if (num < 0)
					{
						return;
					}
					for (int i = 0; i < basePoints.Length; i++)
					{
						if (!(basePoints[i].OwnerArea == null))
						{
							if (dictionary[num].Contains(basePoints[i].OwnerArea.AreaID))
							{
								if (basePoints[i].ID >= 0)
								{
									Singleton<Housing>.Instance.StartShield(basePoints[i].ID);
									break;
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600511E RID: 20766 RVA: 0x002080A0 File Offset: 0x002064A0
	private IEnumerator PutHmesh()
	{
		if (this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai)
		{
			this.StartPos.position = this.hSceneManager.Agent[0].Position;
			yield break;
		}
		GameObject hMeshDataObj = null;
		Singleton<Manager.Resources>.Instance.HSceneTable.HMeshObjDic.TryGetValue(Singleton<Manager.Map>.Instance.MapID, out hMeshDataObj);
		HMeshData hMeshData = (!(hMeshDataObj != null)) ? null : hMeshDataObj.GetComponent<HMeshData>();
		int areaid = this.hSceneManager.Player.AreaID;
		Vector3 allNearestVertex = Vector3.zero;
		float allMiniDistanceSqr = float.PositiveInfinity;
		int GpContain = -1;
		Dictionary<int, List<int>> areagpIds = null;
		int mapID = Singleton<Manager.Map>.Instance.MapID;
		Dictionary<int, Dictionary<int, List<int>>> vanishAreaGp = Singleton<Manager.Resources>.Instance.Map.VanishHousingAreaGroup;
		if (vanishAreaGp != null && vanishAreaGp.TryGetValue(mapID, out areagpIds))
		{
			foreach (KeyValuePair<int, List<int>> keyValuePair in areagpIds)
			{
				if (keyValuePair.Value.Contains(areaid))
				{
					GpContain = keyValuePair.Key;
					break;
				}
			}
		}
		string tag = string.Empty;
		if (GpContain < 0)
		{
			this.PutHmesh(ref allNearestVertex, ref allMiniDistanceSqr, hMeshData, areaid, ref tag);
		}
		else
		{
			for (int i = 0; i < areagpIds[GpContain].Count; i++)
			{
				this.PutHmesh(ref allNearestVertex, ref allMiniDistanceSqr, hMeshData, areagpIds[GpContain][i], ref tag);
			}
		}
		Vector3 pos = Vector3.zero;
		if (this.hSceneManager.bMerchant)
		{
			this.hSceneManager.merchantActor.ActivateNavMeshAgent();
			this.hSceneManager.merchantActor.NavMeshAgent.Warp(allNearestVertex);
			yield return null;
			pos = this.hSceneManager.merchantActor.Position;
			this.hSceneManager.merchantActor.DeactivateNavMeshAgent();
		}
		else
		{
			this.hSceneManager.Agent[0].ActivateNavMeshAgent();
			this.hSceneManager.Agent[0].NavMeshAgent.Warp(allNearestVertex);
			yield return null;
			pos = this.hSceneManager.Agent[0].Position;
			this.hSceneManager.Agent[0].DeactivateNavMeshAgent();
		}
		this.StartPos.position = pos;
		bool forceFloor = this.hSceneManager.EventKind == HSceneManager.HEvent.FromFemale;
		forceFloor |= (this.hSceneManager.EventKind == HSceneManager.HEvent.GyakuYobai);
		forceFloor |= (this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai);
		if ((this.hSceneManager.females[1] == null || this.hSceneManager.females[1].GetComponent<PlayerActor>() != null) && !forceFloor)
		{
			if (HSceneManager.HmeshTag.ContainsKey(tag))
			{
				this.hSceneManager.height = ((HSceneManager.HmeshTag[tag] < 0) ? 1 : HSceneManager.HmeshTag[tag]);
			}
			else
			{
				this.hSceneManager.height = 1;
			}
		}
		else if (HSceneManager.HmeshTag.ContainsKey(tag))
		{
			this.hSceneManager.height = ((HSceneManager.HmeshTag[tag] < 0) ? 0 : HSceneManager.HmeshTag[tag]);
		}
		else
		{
			this.hSceneManager.height = 0;
		}
		if (HSceneManager.SleepStart)
		{
			this.hSceneManager.height = 0;
		}
		yield break;
	}

	// Token: 0x0600511F RID: 20767 RVA: 0x002080BC File Offset: 0x002064BC
	private void PutHmesh(ref Vector3 allNearestVertex, ref float allMiniDistanceSqr, HMeshData hMeshData, int areaid, ref string tag)
	{
		Collider[] array;
		if (hMeshData == null || !hMeshData.dicColliders.TryGetValue(areaid, out array))
		{
			array = null;
		}
		Vector3 position = this.hSceneManager.females[0].Position;
		StringBuilder stringBuilder = StringBuilderPool.Get();
		if (array != null)
		{
			foreach (Collider collider in array)
			{
				stringBuilder.Clear();
				stringBuilder.Append(collider.gameObject.tag);
				if (!(stringBuilder.ToString() == "Untagged"))
				{
					if (!this.hSceneManager.bMerchant)
					{
						if (this.hSceneManager.EventKind == HSceneManager.HEvent.FromFemale)
						{
							if ((Singleton<Manager.Map>.Instance.Player.ChaControl.sex == 0 || (Singleton<Manager.Map>.Instance.Player.ChaControl.sex == 1 && Singleton<HSceneManager>.Instance.bFutanari)) && stringBuilder.ToString() != "standfloor" && stringBuilder.ToString() != "bed")
							{
								goto IL_42E;
							}
						}
						else if (this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai && stringBuilder.ToString() != "standfloor" && stringBuilder.ToString() != "bed" && stringBuilder.ToString() != "sofabed")
						{
							goto IL_42E;
						}
						if (Singleton<Manager.Map>.Instance.Player.ChaControl.sex == 1 && !Singleton<HSceneManager>.Instance.bFutanari && !HSceneManager.HmeshLesTag.ContainsKey(stringBuilder.ToString()))
						{
							goto IL_42E;
						}
						if (this.hSceneManager.EventKind != HSceneManager.HEvent.GyakuYobai && HSceneManager.SleepStart && stringBuilder.ToString() != "standfloor" && stringBuilder.ToString() != "bed")
						{
							goto IL_42E;
						}
					}
					else
					{
						if ("sofa" == stringBuilder.ToString())
						{
							goto IL_42E;
						}
						if (!HSceneManager.HmeshTag.ContainsKey(stringBuilder.ToString()))
						{
							goto IL_42E;
						}
						if (HSceneManager.HmeshTag[stringBuilder.ToString()] > 4)
						{
							goto IL_42E;
						}
					}
					Mesh sharedMesh = collider.GetComponent<MeshCollider>().sharedMesh;
					float num = float.PositiveInfinity;
					Vector3 vector = Vector3.zero;
					foreach (Vector3 vector2 in sharedMesh.vertices)
					{
						if (vector2.y <= ((!this.hSceneManager.bMerchant) ? this.hSceneManager.Agent[0].ChaControl.objHeadBone.transform.position.y : this.hSceneManager.merchantActor.ChaControl.objHeadBone.transform.position.y))
						{
							Vector3 vector3 = new Vector3(vector2.x, vector2.y, vector2.z);
							if (Singleton<Manager.Map>.Instance.MapID == 0 && (areaid == 11 || areaid == 12 || areaid == 13))
							{
								Transform transform = collider.transform;
								vector3.x *= transform.localScale.x;
								vector3.y *= transform.localScale.y;
								vector3.z *= transform.localScale.z;
								vector3 = transform.rotation * vector3;
								vector3 += transform.position;
							}
							float sqrMagnitude = (position - vector3).sqrMagnitude;
							if (sqrMagnitude < num)
							{
								num = sqrMagnitude;
								vector = vector3;
							}
						}
					}
					if (num < allMiniDistanceSqr)
					{
						allNearestVertex = vector;
						tag = collider.gameObject.tag;
						allMiniDistanceSqr = num;
					}
				}
				IL_42E:;
			}
		}
		StringBuilderPool.Release(stringBuilder);
	}

	// Token: 0x06005120 RID: 20768 RVA: 0x0020850E File Offset: 0x0020690E
	public ProcBase GetProcBase()
	{
		if (this.mode == -1 || this.lstProc.Count < this.mode)
		{
			return null;
		}
		return this.lstProc[this.mode];
	}

	// Token: 0x06005121 RID: 20769 RVA: 0x00208548 File Offset: 0x00206948
	private void PlayerWet()
	{
		if (this.hSceneManager.Player == null)
		{
			return;
		}
		PlayerData playerData = this.hSceneManager.Player.PlayerData;
		StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
		EnvironmentSimulator simulator = Singleton<Manager.Map>.Instance.Simulator;
		if (simulator.EnabledTimeProgression)
		{
			Weather weather = simulator.Weather;
			if (this.hSceneManager.Player.AreaType == MapArea.AreaType.Indoor)
			{
				playerData.Wetness += statusProfile.DrySpeed * Time.deltaTime;
			}
			else
			{
				if (weather != Weather.Rain)
				{
					if (weather != Weather.Storm)
					{
						playerData.Wetness += statusProfile.DrySpeed * Time.deltaTime;
					}
					else
					{
						playerData.Wetness += statusProfile.WetRateInStorm * Time.deltaTime;
					}
				}
				else
				{
					playerData.Wetness += statusProfile.WetRateInRain * Time.deltaTime;
				}
				playerData.Wetness = Mathf.Clamp(playerData.Wetness, 0f, 100f);
			}
		}
		if (this.hSceneManager.Player.ChaControl != null)
		{
			float wetRate = Mathf.InverseLerp(0f, 100f, playerData.Wetness);
			this.hSceneManager.Player.ChaControl.wetRate = wetRate;
		}
	}

	// Token: 0x06005122 RID: 20770 RVA: 0x002086AA File Offset: 0x00206AAA
	public ChaControl[] GetMales()
	{
		return this.chaMales;
	}

	// Token: 0x04004A8F RID: 19087
	public HSceneFlagCtrl ctrlFlag;

	// Token: 0x04004A90 RID: 19088
	public GameObject objMetaBallBase;

	// Token: 0x04004A91 RID: 19089
	public GameObject objGrondInstantiate;

	// Token: 0x04004A92 RID: 19090
	public H_Lookat_dan[] ctrlLookAts;

	// Token: 0x04004A93 RID: 19091
	public YureCtrl[] ctrlYures;

	// Token: 0x04004A94 RID: 19092
	public YureCtrlMale ctrlYureMale;

	// Token: 0x04004A95 RID: 19093
	public HLayerCtrl ctrlLayer;

	// Token: 0x04004A96 RID: 19094
	public HAutoCtrl ctrlAuto;

	// Token: 0x04004A97 RID: 19095
	public CollisionCtrl[] ctrlMaleCollisionCtrls;

	// Token: 0x04004A98 RID: 19096
	public CollisionCtrl[] ctrlFemaleCollisionCtrls;

	// Token: 0x04004A99 RID: 19097
	public HVoiceCtrl ctrlVoice;

	// Token: 0x04004A9A RID: 19098
	public HMotionEyeNeckFemale[] ctrlEyeNeckFemale;

	// Token: 0x04004A9B RID: 19099
	public HMotionEyeNeckLesPlayer hMotionEyeNeckLesP;

	// Token: 0x04004A9C RID: 19100
	public HMotionEyeNeckMale[] ctrlEyeNeckMale;

	// Token: 0x04004A9D RID: 19101
	public SiruPasteCtrl[] ctrlSiruPastes;

	// Token: 0x04004A9E RID: 19102
	public ParticleSystem AtariEffect;

	// Token: 0x04004A9F RID: 19103
	public ParticleSystem FeelHitEffect3D;

	// Token: 0x04004AA0 RID: 19104
	public Vector3 FeelHitEffect3DOffSet;

	// Token: 0x04004AA1 RID: 19105
	public HPointCtrl hPointCtrl;

	// Token: 0x04004AA2 RID: 19106
	private HParticleCtrl ctrlParitcle;

	// Token: 0x04004AA3 RID: 19107
	public HitObjectCtrl[] ctrlHitObjectFemales = new HitObjectCtrl[2];

	// Token: 0x04004AA4 RID: 19108
	public HitObjectCtrl[] ctrlHitObjectMales = new HitObjectCtrl[2];

	// Token: 0x04004AA5 RID: 19109
	public HScene.LightInfo[] infoLight = new HScene.LightInfo[]
	{
		new HScene.LightInfo(),
		new HScene.LightInfo()
	};

	// Token: 0x04004AA6 RID: 19110
	private HSceneSprite sprite;

	// Token: 0x04004AA7 RID: 19111
	[SerializeField]
	private CrossFade fade;

	// Token: 0x04004AA8 RID: 19112
	private ChaControl[] chaFemales = new ChaControl[2];

	// Token: 0x04004AA9 RID: 19113
	private ChaControl[] chaMales = new ChaControl[2];

	// Token: 0x04004AAA RID: 19114
	private Transform[] chaFemalesTrans = new Transform[2];

	// Token: 0x04004AAB RID: 19115
	private Transform[] chaMalesTrans = new Transform[2];

	// Token: 0x04004AAC RID: 19116
	private List<System.Tuple<int, int, MotionIK>> lstMotionIK = new List<System.Tuple<int, int, MotionIK>>();

	// Token: 0x04004AAD RID: 19117
	private int mode = -1;

	// Token: 0x04004AAE RID: 19118
	private int modeCtrl = -1;

	// Token: 0x04004AAF RID: 19119
	private List<ProcBase> lstProc = new List<ProcBase>();

	// Token: 0x04004AB0 RID: 19120
	private MetaballCtrl ctrlMeta;

	// Token: 0x04004AB1 RID: 19121
	private HItemCtrl ctrlItem;

	// Token: 0x04004AB2 RID: 19122
	private FeelHit ctrlFeelHit;

	// Token: 0x04004AB3 RID: 19123
	private bool isSyncFirstStep;

	// Token: 0x04004AB4 RID: 19124
	private DynamicBoneReferenceCtrl[] ctrlDynamics = new DynamicBoneReferenceCtrl[2];

	// Token: 0x04004AB5 RID: 19125
	private HSeCtrl ctrlSE;

	// Token: 0x04004AB6 RID: 19126
	private GameObject objGrondCollision;

	// Token: 0x04004AB7 RID: 19127
	private bool isTuyaOn;

	// Token: 0x04004AB8 RID: 19128
	private List<string> abName = new List<string>();

	// Token: 0x04004AB9 RID: 19129
	public bool chaFemaleLoaded;

	// Token: 0x04004ABA RID: 19130
	private List<HScene.AnimationListInfo>[] lstAnimInfo = new List<HScene.AnimationListInfo>[]
	{
		new List<HScene.AnimationListInfo>(),
		new List<HScene.AnimationListInfo>(),
		new List<HScene.AnimationListInfo>(),
		new List<HScene.AnimationListInfo>(),
		new List<HScene.AnimationListInfo>(),
		new List<HScene.AnimationListInfo>()
	};

	// Token: 0x04004ABB RID: 19131
	private HScene.AnimationListInfo aInfo = new HScene.AnimationListInfo();

	// Token: 0x04004ABC RID: 19132
	public HScene.AnimationListInfo StartAnimInfo = new HScene.AnimationListInfo();

	// Token: 0x04004ABD RID: 19133
	private RuntimeAnimatorController[,] runtimeAnimatorControllers = new RuntimeAnimatorController[2, 3];

	// Token: 0x04004ABE RID: 19134
	public Transform StartPos;

	// Token: 0x04004ABF RID: 19135
	private bool isSetStartPos;

	// Token: 0x04004AC0 RID: 19136
	private HScene.StartMotion autoMotion;

	// Token: 0x04004AC1 RID: 19137
	private HSceneManager hSceneManager;

	// Token: 0x04004AC2 RID: 19138
	private ActorCameraControl Camera;

	// Token: 0x04004AC3 RID: 19139
	private bool nowStart;

	// Token: 0x04004AC4 RID: 19140
	private bool nullPlayer = true;

	// Token: 0x04004AC5 RID: 19141
	private StringBuilder sbLoadFileName = new StringBuilder();

	// Token: 0x04004AC6 RID: 19142
	public bool NowStateIsEnd;

	// Token: 0x04004AC7 RID: 19143
	private bool nowChangeAnim;

	// Token: 0x04004AC8 RID: 19144
	private Vector3 distanceToContoroler;

	// Token: 0x04004AC9 RID: 19145
	private HSceneSpriteHitem HItemDrag;

	// Token: 0x04004ACA RID: 19146
	public Transform hitemPlace;

	// Token: 0x04004ACB RID: 19147
	public Transform hParticlePlace;

	// Token: 0x04004ACC RID: 19148
	public Transform hitobjPlace;

	// Token: 0x04004ACD RID: 19149
	private Dictionary<int, float> initStandNip = new Dictionary<int, float>();

	// Token: 0x04004ACE RID: 19150
	[Tooltip("Hメッシュの判定を机にするかの基準")]
	public List<HScene.DeskChairInfo> deskChairInfos = new List<HScene.DeskChairInfo>();

	// Token: 0x04004ACF RID: 19151
	private RuntimeAnimatorController[] racM;

	// Token: 0x04004AD0 RID: 19152
	private RuntimeAnimatorController[] racF;

	// Token: 0x04004AD1 RID: 19153
	private RuntimeAnimatorController[] HoushiRacM;

	// Token: 0x04004AD2 RID: 19154
	private RuntimeAnimatorController[] HoushiRacF;

	// Token: 0x04004AD3 RID: 19155
	private Dictionary<string, RuntimeAnimatorController> racEtcM = new Dictionary<string, RuntimeAnimatorController>();

	// Token: 0x04004AD4 RID: 19156
	private Dictionary<string, RuntimeAnimatorController> racEtcF = new Dictionary<string, RuntimeAnimatorController>();

	// Token: 0x04004AD5 RID: 19157
	private HashSet<string> hashUseAssetBundleAnimator = new HashSet<string>();

	// Token: 0x04004AD6 RID: 19158
	private bool prevBeforeWait = true;

	// Token: 0x04004AD7 RID: 19159
	private bool _bareYobai;

	// Token: 0x04004AD8 RID: 19160
	private bool[] prevCharaEntry;

	// Token: 0x04004AD9 RID: 19161
	private bool useLotion;

	// Token: 0x04004ADB RID: 19163
	private HScene.PackData packData;

	// Token: 0x04004ADC RID: 19164
	private RuntimeAnimatorController racBeforeWait;

	// Token: 0x04004ADD RID: 19165
	private StartWaitAnim startWait;

	// Token: 0x04004ADE RID: 19166
	private Dictionary<int, int> preBeforWaitState;

	// Token: 0x02000ACF RID: 2767
	[Serializable]
	public class LightInfo
	{
		// Token: 0x04004AE2 RID: 19170
		[Tooltip("回すキャラライトオブジェクト")]
		public GameObject objCharaLight;

		// Token: 0x04004AE3 RID: 19171
		[Tooltip("キャラライト")]
		public Light light;

		// Token: 0x04004AE4 RID: 19172
		[Tooltip("初期のライトの回転")]
		public Quaternion initRot = Quaternion.identity;

		// Token: 0x04004AE5 RID: 19173
		[Tooltip("初期の強さ")]
		[Space]
		public float initIntensity = 1f;

		// Token: 0x04004AE6 RID: 19174
		[Tooltip("強さの最小値")]
		[Range(0f, 2f)]
		public float minIntensity;

		// Token: 0x04004AE7 RID: 19175
		[Tooltip("強さの最大値")]
		[Range(0f, 2f)]
		public float maxIntensity = 2f;

		// Token: 0x04004AE8 RID: 19176
		[Tooltip("初期の色")]
		public Color initColor = Color.white;
	}

	// Token: 0x02000AD0 RID: 2768
	[Serializable]
	public class AnimationListInfo
	{
		// Token: 0x04004AE9 RID: 19177
		public int id = -1;

		// Token: 0x04004AEA RID: 19178
		public string nameAnimation = string.Empty;

		// Token: 0x04004AEB RID: 19179
		public string assetpathBaseM = string.Empty;

		// Token: 0x04004AEC RID: 19180
		public string assetBaseM = string.Empty;

		// Token: 0x04004AED RID: 19181
		public string assetpathMale = string.Empty;

		// Token: 0x04004AEE RID: 19182
		public string fileMale = string.Empty;

		// Token: 0x04004AEF RID: 19183
		public bool isMaleHitObject;

		// Token: 0x04004AF0 RID: 19184
		public string fileMotionNeckMale;

		// Token: 0x04004AF1 RID: 19185
		public string assetpathBaseF = string.Empty;

		// Token: 0x04004AF2 RID: 19186
		public string assetBaseF = string.Empty;

		// Token: 0x04004AF3 RID: 19187
		public string assetpathFemale;

		// Token: 0x04004AF4 RID: 19188
		public string fileFemale;

		// Token: 0x04004AF5 RID: 19189
		public bool isFemaleHitObject;

		// Token: 0x04004AF6 RID: 19190
		public string fileMotionNeckFemale;

		// Token: 0x04004AF7 RID: 19191
		public string fileMotionNeckFemalePlayer;

		// Token: 0x04004AF8 RID: 19192
		public string assetpathBaseF2 = string.Empty;

		// Token: 0x04004AF9 RID: 19193
		public string assetBaseF2 = string.Empty;

		// Token: 0x04004AFA RID: 19194
		public string assetpathFemale2 = string.Empty;

		// Token: 0x04004AFB RID: 19195
		public string fileFemale2 = string.Empty;

		// Token: 0x04004AFC RID: 19196
		public bool isFemaleHitObject2;

		// Token: 0x04004AFD RID: 19197
		public string fileMotionNeckFemale2;

		// Token: 0x04004AFE RID: 19198
		public UnityEx.ValueTuple<int, int> ActionCtrl = new UnityEx.ValueTuple<int, int>(-1, -1);

		// Token: 0x04004AFF RID: 19199
		public List<int> nPositons = new List<int>();

		// Token: 0x04004B00 RID: 19200
		public List<string> lstOffset = new List<string>();

		// Token: 0x04004B01 RID: 19201
		public bool isNeedItem;

		// Token: 0x04004B02 RID: 19202
		public int nDownPtn;

		// Token: 0x04004B03 RID: 19203
		public int nFaintnessLimit;

		// Token: 0x04004B04 RID: 19204
		public int nIyaAction = 1;

		// Token: 0x04004B05 RID: 19205
		public int nPhase;

		// Token: 0x04004B06 RID: 19206
		public int nHentai;

		// Token: 0x04004B07 RID: 19207
		public bool bSleep;

		// Token: 0x04004B08 RID: 19208
		public int nInitiativeFemale;

		// Token: 0x04004B09 RID: 19209
		public int nFemaleProclivity = -1;

		// Token: 0x04004B0A RID: 19210
		public int nBackInitiativeID = -1;

		// Token: 0x04004B0B RID: 19211
		public List<int> lstSystem = new List<int>();

		// Token: 0x04004B0C RID: 19212
		public int nMaleSon = -1;

		// Token: 0x04004B0D RID: 19213
		public int[] nFemaleUpperCloths = new int[]
		{
			-1,
			-1
		};

		// Token: 0x04004B0E RID: 19214
		public int[] nFemaleLowerCloths = new int[]
		{
			-1,
			-1
		};

		// Token: 0x04004B0F RID: 19215
		public int nFeelHit;

		// Token: 0x04004B10 RID: 19216
		public string nameCamera;

		// Token: 0x04004B11 RID: 19217
		public string fileSiruPaste;

		// Token: 0x04004B12 RID: 19218
		public string fileSiruPasteSecond = string.Empty;

		// Token: 0x04004B13 RID: 19219
		public string fileSe;

		// Token: 0x04004B14 RID: 19220
		public int nShortBreahtPlay = 1;

		// Token: 0x04004B15 RID: 19221
		public HashSet<int> hasVoiceCategory = new HashSet<int>();

		// Token: 0x04004B16 RID: 19222
		public int nPromiscuity = -1;

		// Token: 0x04004B17 RID: 19223
		public int nAnimListInfoID = -1;

		// Token: 0x04004B18 RID: 19224
		public int nBreathID = -1;

		// Token: 0x04004B19 RID: 19225
		public bool bMerchantMotion;
	}

	// Token: 0x02000AD1 RID: 2769
	public class StartMotion
	{
		// Token: 0x0600513B RID: 20795 RVA: 0x0020893F File Offset: 0x00206D3F
		public StartMotion(int _mode, int _id)
		{
			this.mode = _mode;
			this.id = _id;
		}

		// Token: 0x04004B1A RID: 19226
		public int mode;

		// Token: 0x04004B1B RID: 19227
		public int id;
	}

	// Token: 0x02000AD2 RID: 2770
	public class EndMotion
	{
		// Token: 0x0600513C RID: 20796 RVA: 0x00208958 File Offset: 0x00206D58
		public EndMotion(int _height, string[] _abName, string[] _assetName, string[] _stateName)
		{
			this.height = _height;
			for (int i = 0; i < _abName.Length; i++)
			{
				int num = i;
				if (num < 2)
				{
					this.abNameP[num] = _abName[num];
					this.assetNameP[num] = _assetName[num];
					this.stateNameP[num] = _stateName[num];
				}
				else if (num < 4)
				{
					this.abNameH[num - 2] = _abName[num];
					this.assetNameH[num - 2] = _assetName[num];
					this.stateNameH[num - 2] = _stateName[num];
				}
				else
				{
					this.abNameM = _abName[num];
					this.assetNameM = _assetName[num];
					this.stateNameM = _stateName[num];
				}
			}
		}

		// Token: 0x04004B1C RID: 19228
		public int height;

		// Token: 0x04004B1D RID: 19229
		public string[] abNameP = new string[2];

		// Token: 0x04004B1E RID: 19230
		public string[] abNameH = new string[2];

		// Token: 0x04004B1F RID: 19231
		public string abNameM = string.Empty;

		// Token: 0x04004B20 RID: 19232
		public string[] assetNameP = new string[2];

		// Token: 0x04004B21 RID: 19233
		public string[] assetNameH = new string[2];

		// Token: 0x04004B22 RID: 19234
		public string assetNameM = string.Empty;

		// Token: 0x04004B23 RID: 19235
		public string[] stateNameP = new string[2];

		// Token: 0x04004B24 RID: 19236
		public string[] stateNameH = new string[2];

		// Token: 0x04004B25 RID: 19237
		public string stateNameM = string.Empty;
	}

	// Token: 0x02000AD3 RID: 2771
	[Serializable]
	public struct DeskChairInfo
	{
		// Token: 0x04004B26 RID: 19238
		public int eventID;

		// Token: 0x04004B27 RID: 19239
		public int poseID;
	}

	// Token: 0x02000AD4 RID: 2772
	private class PackData : CharaPackData
	{
		// Token: 0x0600513D RID: 20797 RVA: 0x00208A6F File Offset: 0x00206E6F
		public PackData(int charaID, int adv_category)
		{
			this.charaID = charaID;
			this.adv_category = adv_category;
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x0600513E RID: 20798 RVA: 0x00208A90 File Offset: 0x00206E90
		public int charaID { get; }

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x0600513F RID: 20799 RVA: 0x00208A98 File Offset: 0x00206E98
		public int adv_category { get; }

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06005140 RID: 20800 RVA: 0x00208AA0 File Offset: 0x00206EA0
		// (set) Token: 0x06005141 RID: 20801 RVA: 0x00208AA8 File Offset: 0x00206EA8
		public string JumpTag { get; set; } = string.Empty;

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06005142 RID: 20802 RVA: 0x00208AB1 File Offset: 0x00206EB1
		// (set) Token: 0x06005143 RID: 20803 RVA: 0x00208AB9 File Offset: 0x00206EB9
		public bool isWeaknessH { get; set; }

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06005144 RID: 20804 RVA: 0x00208AC2 File Offset: 0x00206EC2
		// (set) Token: 0x06005145 RID: 20805 RVA: 0x00208ACA File Offset: 0x00206ECA
		public int numOrgasmFemale { get; set; }

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x00208AD3 File Offset: 0x00206ED3
		// (set) Token: 0x06005147 RID: 20807 RVA: 0x00208ADB File Offset: 0x00206EDB
		public int ConditionMode { get; set; }

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x00208AE4 File Offset: 0x00206EE4
		// (set) Token: 0x06005149 RID: 20809 RVA: 0x00208AEC File Offset: 0x00206EEC
		public bool isPainAction { get; set; }

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x0600514A RID: 20810 RVA: 0x00208AF5 File Offset: 0x00206EF5
		// (set) Token: 0x0600514B RID: 20811 RVA: 0x00208AFD File Offset: 0x00206EFD
		public bool isConstraintAction { get; set; }

		// Token: 0x0600514C RID: 20812 RVA: 0x00208B08 File Offset: 0x00206F08
		public override List<Program.Transfer> Create()
		{
			List<Program.Transfer> list = base.Create();
			list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
			{
				"bool",
				"isWeaknessH",
				string.Format("{0}", this.isWeaknessH)
			}));
			list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
			{
				"string",
				"JumpTag",
				this.JumpTag
			}));
			list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
			{
				"int",
				"numOrgasmFemale",
				string.Format("{0}", this.numOrgasmFemale)
			}));
			list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
			{
				"int",
				"ConditionMode",
				string.Format("{0}", this.ConditionMode)
			}));
			list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
			{
				"bool",
				"isPainAction",
				string.Format("{0}", this.isPainAction)
			}));
			list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
			{
				"bool",
				"isConstraintAction",
				string.Format("{0}", this.isConstraintAction)
			}));
			return list;
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x00208C70 File Offset: 0x00207070
		public override void Receive(TextScenario scenario)
		{
			base.Receive(scenario);
		}
	}
}
