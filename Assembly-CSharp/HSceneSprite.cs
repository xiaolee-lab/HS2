using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using AIProject;
using AIProject.Scene;
using AIProject.UI;
using ConfigScene;
using Illusion.Component.UI.ColorPicker;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

// Token: 0x02000B06 RID: 2822
public class HSceneSprite : Singleton<HSceneSprite>
{
	// Token: 0x0600529D RID: 21149 RVA: 0x0023F3B4 File Offset: 0x0023D7B4
	public IEnumerator Init()
	{
		this.SetHelpActive(false);
		this.AtariEffect.Stop();
		this.FeelHitEffect3D.Stop();
		this.ctrlFlag = Singleton<HSceneFlagCtrl>.Instance;
		this.hScene = this.ctrlFlag.GetComponent<HScene>();
		this.hSceneManager = Singleton<HSceneManager>.Instance;
		this.MotionScroll.BlankSet(this.objMotionListInstanceButton.GetComponent<ScrollCylinderNode>(), false);
		this.hSceneMotionPool.CreatePool(this.objMotionListInstanceButton, this.objMotionList.transform, 50);
		yield return null;
		this.colorMGaugeDef = this.imageMGauge.color;
		this.colorFGaugeDef = this.imageFGauge.color;
		this.categoryFinish.Init();
		this.charaChoice.Init();
		this.objCloth.Init();
		this.objAccessory.Init();
		this.objClothCard.Init();
		this.categoryMain.Init();
		this.CategoryScroll = this.categoryMain.GetHScroll();
		yield return this.HItemCtrl.Init();
		if (this.hSceneManager.Player != null)
		{
			this.PlayerSex = (int)this.hSceneManager.Player.ChaControl.sex;
			if (this.PlayerSex == 1 && this.hSceneManager.bFutanari)
			{
				this.PlayerSex = 0;
			}
		}
		this.colorPicker.updateColorAction += delegate(Color color)
		{
			this.OnValueLightColorChanged(color);
		};
		this.endFade = -1;
		this.nowFadeTime = 0f;
		this.hScene.AtariEffect = this.AtariEffect;
		this.hScene.FeelHitEffect3D = this.FeelHitEffect3D;
		this.hScene.FeelHitEffect3DOffSet = this.FeelHitEffect3DOffSet;
		if (!this.hSceneManager.bMerchant)
		{
			if (!this.hSceneManager.IsHousingHEnter)
			{
				if (this.hSceneManager.EventKind == HSceneManager.HEvent.Normal)
				{
					yield return MapUIContainer.DrawOnceTutorialUI(5, null);
					this.PopupCommands(this.hSceneManager.isForce);
				}
				else
				{
					this.ctrlFlag.HBeforeCamera.gameObject.SetActive(false);
					if (this.PlayerSex == 0)
					{
						this.hScene.SetStartAnimationInfo(this.hSceneManager.EventKind, -1);
					}
					else if (this.PlayerSex == 1)
					{
						this.hScene.SetStartAnimationInfo(this.hSceneManager.EventKind, 4);
					}
					this.hSceneManager.Player.CameraControl.HCamera = this.ctrlFlag.cameraCtrl;
					this.CommandProc();
					this.hSceneManager.Player.CameraControl.enabled = false;
					this.hSceneManager.Player.Controller.enabled = false;
					this.hSceneManager.Player.Animation.enabled = false;
					while (this.hScene.ctrlVoice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice)
					{
						yield return null;
					}
					Singleton<HSceneFlagCtrl>.Instance.BeforeHWait = false;
					this.nowFadeTime = 0f;
					if (this.hSceneManager.Player.CameraControl.Mode != CameraMode.H)
					{
						Vector3 position = this.hSceneManager.females[0].Position;
						GlobalMethod.setCameraBase(Singleton<HSceneFlagCtrl>.Instance.cameraCtrl, position, this.hSceneManager.females[0].Rotation.eulerAngles);
						this.hSceneManager.Player.CameraControl.Mode = CameraMode.H;
						this.ctrlFlag.HBeforeCamera.gameObject.SetActive(false);
					}
				}
			}
			else
			{
				this.ctrlFlag.HBeforeCamera.gameObject.SetActive(false);
				this.hScene.SetStartAnimationInfo(this.hSceneManager.EventKind, -1);
				this.hSceneManager.Player.CameraControl.HCamera = this.ctrlFlag.cameraCtrl;
				this.CommandProc();
				this.hSceneManager.Player.CameraControl.enabled = false;
				this.hSceneManager.Player.Controller.enabled = false;
				this.hSceneManager.Player.Animation.enabled = false;
				while (this.hScene.ctrlVoice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice)
				{
					yield return null;
				}
				Singleton<HSceneFlagCtrl>.Instance.BeforeHWait = false;
				this.nowFadeTime = 0f;
				if (this.hSceneManager.Player.CameraControl.Mode != CameraMode.H)
				{
					Vector3 position2 = this.hSceneManager.females[0].Position;
					GlobalMethod.setCameraBase(Singleton<HSceneFlagCtrl>.Instance.cameraCtrl, position2, this.hSceneManager.females[0].Rotation.eulerAngles);
					this.hSceneManager.Player.CameraControl.Mode = CameraMode.H;
					this.ctrlFlag.HBeforeCamera.gameObject.SetActive(false);
				}
			}
		}
		else
		{
			this.ctrlFlag.HBeforeCamera.gameObject.SetActive(false);
			this.hScene.SetStartAnimationInfoM(this.hSceneManager.EventKind);
			this.hSceneManager.Player.CameraControl.HCamera = this.ctrlFlag.cameraCtrl;
			this.CommandProc();
			this.hSceneManager.Player.CameraControl.enabled = false;
			this.hSceneManager.Player.Controller.enabled = false;
			this.hSceneManager.Player.Animation.enabled = false;
			while (this.hScene.ctrlVoice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice)
			{
				yield return null;
			}
			Singleton<HSceneFlagCtrl>.Instance.BeforeHWait = false;
			this.nowFadeTime = 0f;
			if (this.hSceneManager.Player.CameraControl.Mode != CameraMode.H)
			{
				Vector3 position3 = this.hSceneManager.females[0].Position;
				GlobalMethod.setCameraBase(Singleton<HSceneFlagCtrl>.Instance.cameraCtrl, position3, this.hSceneManager.females[0].Rotation.eulerAngles);
				this.hSceneManager.Player.CameraControl.Mode = CameraMode.H;
				this.ctrlFlag.HBeforeCamera.gameObject.SetActive(false);
			}
		}
		this.hPointCtrl = Singleton<HPointCtrl>.Instance;
		yield break;
	}

	// Token: 0x0600529E RID: 21150 RVA: 0x0023F3D0 File Offset: 0x0023D7D0
	private void Update()
	{
		if (this.chaFemales == null)
		{
			return;
		}
		for (int i = 0; i < this.MenuCategory.Length; i++)
		{
			this.MenuCategory[i].SetActive(Config.HData.MenuIcon);
		}
		this.MenuCategorySub.SetActive(Config.HData.MenuIcon);
		this.GuageBase.SetActive(Config.HData.FeelingGauge);
		this.HelpBaseActive(Config.HData.ActionGuide);
		if (this.imageFGauge != null)
		{
			this.imageFGauge.fillAmount = this.ctrlFlag.feel_f;
			if (this.ctrlFlag.feel_f >= 0.7f)
			{
				this.imageFGauge.color = this.colorFGauge;
			}
			else
			{
				this.imageFGauge.color = this.colorFGaugeDef;
			}
		}
		if (this.imageMGauge != null)
		{
			this.imageMGauge.fillAmount = this.ctrlFlag.feel_m;
			if (this.ctrlFlag.feel_m >= 0.7f)
			{
				this.imageMGauge.color = this.colorMGauge;
			}
			else
			{
				this.imageMGauge.color = this.colorMGaugeDef;
			}
		}
		this.FadeProc();
		int item = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1;
		int item2 = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item2;
		if (this.anim_f != null && this.anim_f.isActiveAndEnabled)
		{
			AnimatorStateInfo currentAnimatorStateInfo = this.anim_f.GetCurrentAnimatorStateInfo(0);
			if (this.ctrlFlag.isGaugeHit && !this.anim_f.IsInTransition(0) && currentAnimatorStateInfo.IsName("idle") && item != 1 && (item != 5 || (item2 != 1 && item2 != 2)))
			{
				this.anim_f.CrossFade("hit", 0.3f);
			}
			if (!this.ctrlFlag.isGaugeHit && !this.anim_f.IsInTransition(0) && currentAnimatorStateInfo.IsName("hit"))
			{
				this.anim_f.CrossFade("idle", 0.3f);
			}
		}
		if (this.anim_m && this.anim_m.isActiveAndEnabled)
		{
			AnimatorStateInfo currentAnimatorStateInfo2 = this.anim_m.GetCurrentAnimatorStateInfo(0);
			if (this.ctrlFlag.isGaugeHit_M && !this.anim_m.IsInTransition(0) && currentAnimatorStateInfo2.IsName("idle") && (item == 1 || (item == 5 && (item2 == 1 || item2 == 2))))
			{
				this.anim_m.CrossFade("hit", 0.3f);
			}
			if (!this.ctrlFlag.isGaugeHit_M && !this.anim_m.IsInTransition(0) && currentAnimatorStateInfo2.IsName("hit"))
			{
				this.anim_m.CrossFade("idle", 0.3f);
			}
		}
		this.UIFade();
		if (this.objHItem.GetComponent<HSceneSpriteHitem>().Effect(6))
		{
			this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.RecoverFaintness;
		}
		if (this.SelectArea != null)
		{
			for (int j = 0; j < this.hSceneScrollNodes.Count; j++)
			{
				if (!(this.hSceneScrollNodes[j] == null))
				{
					this.motionListImages = this.hSceneScrollNodes[j].GetComponentsInChildren<Image>();
					if (j == this.MotionScroll.GetTarget().Item1)
					{
						this.motionListToggle = this.hSceneScrollNodes[j].GetComponent<Toggle>();
						if (!this.motionListToggle.isOn)
						{
							for (int k = 0; k < this.motionListImages.Length; k++)
							{
								if (this.motionListImages[k].name != "NowSelect")
								{
									this.motionListImages[k].enabled = false;
								}
								else
								{
									this.motionListImages[k].enabled = true;
								}
							}
						}
						else
						{
							for (int l = 0; l < this.motionListImages.Length; l++)
							{
								if (this.motionListImages[l].name != "Checkmark")
								{
									this.motionListImages[l].enabled = false;
								}
								else
								{
									this.motionListImages[l].enabled = true;
								}
							}
						}
					}
					else
					{
						for (int m = 0; m < this.motionListImages.Length; m++)
						{
							if (this.motionListImages[m].name == "NowSelect")
							{
								this.motionListImages[m].enabled = false;
							}
							else if (this.motionListImages[m] == this.hSceneScrollNodes[j].BG)
							{
								this.motionListImages[m].enabled = true;
							}
						}
					}
				}
			}
		}
		if (!this.ChangeStart)
		{
			foreach (ScrollCylinderNode scrollCylinderNode in this.hSceneScrollNodes)
			{
				scrollCylinderNode.text.raycastTarget = true;
			}
		}
		this.categories.Changebuttonactive(!this.ctrlFlag.nowOrgasm);
		if (this.ctrlFlag.nowOrgasm)
		{
			if (this.hPointCtrl.IsMarker)
			{
				this.MarkerObjSet();
			}
			if (this.HItemCtrl.ConfirmPanel.activeSelf)
			{
				this.HItemCtrl.ConfirmPanel.SetActive(false);
			}
			if (this.objLightCategory.activeSelf)
			{
				this.objLightCategory.SetActive(false);
			}
		}
	}

	// Token: 0x0600529F RID: 21151 RVA: 0x0023F9F8 File Offset: 0x0023DDF8
	private void LateUpdate()
	{
	}

	// Token: 0x060052A0 RID: 21152 RVA: 0x0023F9FA File Offset: 0x0023DDFA
	private void OnValidate()
	{
	}

	// Token: 0x060052A1 RID: 21153 RVA: 0x0023F9FC File Offset: 0x0023DDFC
	public void SetLightInfo(HScene.LightInfo[] _info)
	{
		for (int i = 0; i < 2; i++)
		{
			int num = i;
			if (_info[num] == null)
			{
				this.infoMapLight[num] = null;
			}
			else
			{
				this.infoMapLight[num].objCharaLight = _info[num].objCharaLight;
				this.infoMapLight[num].light = _info[num].light;
				this.infoMapLight[num].initRot = _info[num].initRot;
				this.infoMapLight[num].initIntensity = _info[num].initIntensity;
				this.infoMapLight[num].initColor = _info[num].initColor;
			}
		}
		for (int j = 0; j < 2; j++)
		{
			int num2 = j;
			if (_info[num2] != null && _info[num2].light.isActiveAndEnabled)
			{
				this.categoryLightDir.SetValue(this.infoMapLight[num2].initRot.eulerAngles.x / 360f, 0);
				this.categoryLightDir.SetValue(this.infoMapLight[num2].initRot.eulerAngles.y / 360f, 1);
				this.categoryLightDir.SetValue(this.infoMapLight[num2].initIntensity, 2);
			}
		}
	}

	// Token: 0x060052A2 RID: 21154 RVA: 0x0023FB44 File Offset: 0x0023DF44
	public bool IsSpriteOver()
	{
		EventSystem current = EventSystem.current;
		return !(current == null) && current.IsPointerOverGameObject();
	}

	// Token: 0x060052A3 RID: 21155 RVA: 0x0023FB6B File Offset: 0x0023DF6B
	public void setAnimationList(List<HScene.AnimationListInfo>[] _lstAnimInfo)
	{
		this.lstAnimInfo = _lstAnimInfo;
	}

	// Token: 0x060052A4 RID: 21156 RVA: 0x0023FB74 File Offset: 0x0023DF74
	public void Setting(ChaControl[] _females)
	{
		this.chaFemales = _females;
		if (Singleton<HSceneManager>.Instance.EventKind == HSceneManager.HEvent.GyakuYobai)
		{
			this.categoryMainButton.interactable = false;
			this.hPointButton.interactable = false;
		}
		this.RefleshAutoButtom();
		this.categoryFinish.SetActive(false, -1);
		this.SetAnimationMenu();
	}

	// Token: 0x060052A5 RID: 21157 RVA: 0x0023FBCC File Offset: 0x0023DFCC
	public void RefleshAutoButtom()
	{
		int num = 0;
		for (int i = 0; i < this.lstAnimInfo.Length; i++)
		{
			for (int j = 0; j < this.lstAnimInfo[i].Count; j++)
			{
				if (this.CheckAutoMotionLimit(this.lstAnimInfo[i][j]))
				{
					num++;
				}
			}
		}
		if (this.PlayerSex == -1)
		{
			this.categoryMain.SetActive(true, -1);
		}
		this.isLeaveItToYou = (num != 0);
		this.isLeaveItToYou &= (this.hSceneManager.EventKind == HSceneManager.HEvent.FromFemale || this.hSceneManager.EventKind == HSceneManager.HEvent.GyakuYobai);
		this.SetVisibleLeaveItToYou(this.isLeaveItToYou, false);
	}

	// Token: 0x060052A6 RID: 21158 RVA: 0x0023FC98 File Offset: 0x0023E098
	public void SetFinishSelect(int _mode, int _ctrl, int infomode = -1, int infoctrl = -1)
	{
		switch (_mode)
		{
		case 0:
			this.categoryFinish.SetActive(false, -1);
			this.lstFinishVisible.Clear();
			break;
		case 1:
			this.categoryFinish.SetActive(false, -1);
			this.lstFinishVisible.Clear();
			if (this.ctrlFlag.initiative != 2)
			{
				this.categoryFinish.lstButton[0].GetComponentInChildren<Text>().text = "体にかける";
				this.lstFinishVisible.Add(0);
				if (_ctrl == 1 && !this.ctrlFlag.isFaintness)
				{
					this.lstFinishVisible.Add(4);
					this.lstFinishVisible.Add(5);
				}
				else if (_ctrl == 2)
				{
					this.lstFinishVisible.Add(4);
					this.lstFinishVisible.Add(5);
				}
			}
			break;
		case 2:
			this.categoryFinish.SetActive(false, -1);
			this.lstFinishVisible.Clear();
			if (this.ctrlFlag.initiative != 2)
			{
				this.categoryFinish.lstButton[0].GetComponentInChildren<Text>().text = "外に出す";
				this.lstFinishVisible.Add(0);
				this.lstFinishVisible.Add(1);
				if ((infomode == 2 && infoctrl == 0) || (infomode == 3 && (infoctrl == 1 || infoctrl == 7)))
				{
					this.lstFinishVisible.Add(2);
				}
				else if (infomode == 3 && (_ctrl == 0 || _ctrl == 8) && this.ctrlFlag.isFaintness)
				{
					this.categoryFinish.SetActive(false, -1);
					this.lstFinishVisible.Clear();
				}
			}
			break;
		case 3:
			this.categoryFinish.SetActive(false, -1);
			this.lstFinishVisible.Clear();
			break;
		case 4:
			this.categoryFinish.SetActive(false, -1);
			this.lstFinishVisible.Clear();
			break;
		case 5:
			this.categoryFinish.SetActive(false, -1);
			this.lstFinishVisible.Clear();
			break;
		case 6:
			this.categoryFinish.SetActive(false, -1);
			this.lstFinishVisible.Clear();
			break;
		case 7:
			this.categoryFinish.SetActive(false, -1);
			this.lstFinishVisible.Clear();
			if (_ctrl == 1 || _ctrl == 2)
			{
				this.categoryFinish.lstButton[0].GetComponentInChildren<Text>().text = "体にかける";
				this.lstFinishVisible.Add(0);
				if (_ctrl == 2 && !this.ctrlFlag.isFaintness)
				{
					this.lstFinishVisible.Add(4);
					this.lstFinishVisible.Add(5);
				}
			}
			else if (_ctrl == 3 || _ctrl == 4)
			{
				this.categoryFinish.lstButton[0].GetComponentInChildren<Text>().text = "外に出す";
				this.lstFinishVisible.Add(0);
				this.lstFinishVisible.Add(1);
				if (_ctrl == 3)
				{
					this.lstFinishVisible.Add(2);
				}
			}
			break;
		}
	}

	// Token: 0x060052A7 RID: 21159 RVA: 0x0023FFCF File Offset: 0x0023E3CF
	public bool IsFinishVisible(int _num)
	{
		return this.lstFinishVisible.Contains(_num);
	}

	// Token: 0x060052A8 RID: 21160 RVA: 0x0023FFDD File Offset: 0x0023E3DD
	public void SetVisibleLeaveItToYou(bool _visible, bool _judgeLeaveItToYou = false)
	{
		if (_judgeLeaveItToYou)
		{
			_visible = this.isLeaveItToYou;
		}
		this.tglLeaveItToYou.gameObject.SetActive(_visible);
	}

	// Token: 0x060052A9 RID: 21161 RVA: 0x0023FFFE File Offset: 0x0023E3FE
	public void SetToggleLeaveItToYou(bool _on)
	{
		this.tglLeaveItToYou.isOn = _on;
	}

	// Token: 0x060052AA RID: 21162 RVA: 0x0024000C File Offset: 0x0023E40C
	public void SetEnableCategoryMain(bool _enable)
	{
		this.categoryMain.SetEnable(_enable, -1);
		this.categoryMain.gameObject.SetActive(_enable);
		this.tglLeaveItToYou.interactable = _enable;
		if (!_enable)
		{
			this.categories.MainCategoryActive[3] = false;
		}
	}

	// Token: 0x060052AB RID: 21163 RVA: 0x0024004C File Offset: 0x0023E44C
	public void SetEnableHItem(bool _enable)
	{
		this.objHItem.SetActive(_enable);
		if (!_enable)
		{
			this.categories.SubCategoryActive[0] = false;
			HSceneSpriteHitem component = this.objHItem.GetComponent<HSceneSpriteHitem>();
			List<ScrollCylinderNode> list = component.hSceneScroll.GetList();
			if (!component.hSceneScroll.enabled)
			{
				component.hSceneScroll.enabled = true;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (!(list[i] == null))
				{
					Toggle component2 = list[i].GetComponent<Toggle>();
					if (!(component2 == null) && component2.isOn)
					{
						component2.isOn = false;
					}
				}
			}
		}
	}

	// Token: 0x060052AC RID: 21164 RVA: 0x0024010C File Offset: 0x0023E50C
	public void MainCategoryOfLeaveItToYou(bool _isLeaveItToYou)
	{
		if (!_isLeaveItToYou)
		{
			this.SetAnimationMenu();
		}
		else
		{
			bool flag = true;
			for (int i = 0; i < this.lstAnimInfo.Length; i++)
			{
				int num = 0;
				int j = 0;
				while (j < this.lstAnimInfo[i].Count)
				{
					if (this.chaFemales[1] != null)
					{
						if (i >= 4)
						{
							if (this.PlayerSex == 0)
							{
								if (i != 5)
								{
									goto IL_10C;
								}
							}
							else if (i != 4)
							{
								goto IL_10C;
							}
							goto IL_7C;
						}
					}
					else if (i <= 3)
					{
						goto IL_7C;
					}
					IL_10C:
					j++;
					continue;
					IL_7C:
					if (this.ctrlFlag.initiative == 1)
					{
						if (this.lstAnimInfo[i][j].nInitiativeFemale != 1 && (!flag || this.lstAnimInfo[i][j].nInitiativeFemale != 2))
						{
							goto IL_10C;
						}
					}
					else
					{
						if (this.ctrlFlag.initiative != 2)
						{
							goto IL_10C;
						}
						if (this.lstAnimInfo[i][j].nInitiativeFemale != 2)
						{
							goto IL_10C;
						}
					}
					num++;
					goto IL_10C;
				}
				if (this.PlayerSex != -1)
				{
					this.categoryMain.SetActive(num != 0, i);
				}
			}
			if (this.PlayerSex == -1)
			{
				this.categoryMain.SetActive(true, -1);
			}
			this.CategoryScroll.ListNodeSet(null, true);
			Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit x)
			{
				this.CategoryScroll.SetTarget(this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1);
			});
		}
	}

	// Token: 0x060052AD RID: 21165 RVA: 0x002402AB File Offset: 0x0023E6AB
	public bool IsEnableLeaveItToYou()
	{
		return this.tglLeaveItToYou.interactable && this.tglLeaveItToYou.gameObject.activeSelf;
	}

	// Token: 0x060052AE RID: 21166 RVA: 0x002402D8 File Offset: 0x0023E6D8
	public void OnChangePlaySelect(GameObject objClick)
	{
		Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
		if (null != objClick)
		{
			HAnimationInfoComponent component = objClick.GetComponent<HAnimationInfoComponent>();
			if (null != component)
			{
				this.ctrlFlag.selectAnimationListInfo = component.info;
			}
			objClick.GetComponent<Toggle>().isOn = true;
			this.ChangeStart = true;
		}
	}

	// Token: 0x060052AF RID: 21167 RVA: 0x00240338 File Offset: 0x0023E738
	private void MotionListView(int taii)
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		if (this.endFade == 0)
		{
			return;
		}
		this.SetMotionListDraw(true, taii);
	}

	// Token: 0x060052B0 RID: 21168 RVA: 0x00240398 File Offset: 0x0023E798
	public void OnClickMotion(int _motion)
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		HRotationScrollNode component = this.categoryMain.GetHScroll().GetTarget().Item2.GetComponent<HRotationScrollNode>();
		if (component == null || component.id != _motion)
		{
			return;
		}
		if (this.endFade == 0)
		{
			return;
		}
		this.SetMotionListDraw(true, _motion);
	}

	// Token: 0x060052B1 RID: 21169 RVA: 0x00240430 File Offset: 0x0023E830
	public void SetMotionListDraw(bool _active, int _motion = -1)
	{
		this.objMotionListPanel.SetActive(_active);
		this.ctrlFlag.categoryMotionList = _motion;
		if (_active)
		{
			this.LoadMotionList(this.ctrlFlag.categoryMotionList);
			switch (_motion)
			{
			case 0:
				this.MotionListLabel.text = "愛撫";
				break;
			case 1:
				this.MotionListLabel.text = "奉仕";
				break;
			case 2:
				this.MotionListLabel.text = "挿入";
				break;
			case 3:
				this.MotionListLabel.text = "特殊";
				break;
			case 4:
				this.MotionListLabel.text = "レズ";
				break;
			case 5:
				this.MotionListLabel.text = "複数";
				break;
			}
		}
		else if (this.categoryMain.gameObject.activeSelf)
		{
			this.categoryMain.gameObject.SetActive(false);
		}
	}

	// Token: 0x060052B2 RID: 21170 RVA: 0x0024053C File Offset: 0x0023E93C
	public void OnClickMainCategories(int _menu)
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		if (this.endFade == 0)
		{
			return;
		}
		if (this.HItemCtrl.ConfirmPanel.activeSelf)
		{
			return;
		}
		GameObject[] array = new GameObject[]
		{
			this.objCloth.gameObject,
			this.objAccessory.gameObject,
			this.objClothCard.gameObject,
			this.categoryMain.gameObject
		};
		if (array[_menu].activeSelf)
		{
			array[_menu].SetActive(false);
			if (_menu == 3)
			{
				this.SetMotionListDraw(false, -1);
			}
			this.charaChoice.CloseChoice();
			this.charaChoice.gameObject.SetActive(false);
			this.charaChoice.SetMaleSelectBtn(false);
			this.categories.MainCategoryActive[_menu] = false;
			if (_menu == 2)
			{
				this.objClothCard.CloseSort();
			}
		}
		else
		{
			if (_menu != 3)
			{
				array[_menu].SetActive(true);
				this.categories.MainCategoryActive[_menu] = true;
			}
			else
			{
				ScrollCylinderNode[] componentsInChildren = this.CategoryScroll.Contents.GetComponentsInChildren<ScrollCylinderNode>();
				if (componentsInChildren.Length > 0)
				{
					this.SetEnableCategoryMain(true);
					this.categories.MainCategoryActive[_menu] = true;
					Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
					{
						this.CategoryScroll.ListNodeSet(null, false);
						Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit x)
						{
							this.CategoryScroll.SetTarget(this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1);
							this.MotionListView(this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1);
						});
					});
				}
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (i != _menu)
				{
					array[i].SetActive(false);
					this.categories.MainCategoryActive[i] = false;
				}
			}
			bool isMarker = this.hPointCtrl.IsMarker;
			if (isMarker)
			{
				this.MarkerObjSet();
			}
			this.categories.MainCategoryActive[this.categories.MainCategoryActive.Length - 1] = false;
			if (_menu != 3)
			{
				this.SetMotionListDraw(false, -1);
			}
			else
			{
				this.SetAnimationMenu();
			}
			this.charaChoice.SetMaleSelectBtn(_menu == 1);
			if (_menu == 0)
			{
				this.objCloth.SetClothCharacter(false);
			}
			else if (_menu == 1)
			{
				this.objAccessory.SetAccessoryCharacter(false);
			}
			this.charaChoice.gameObject.SetActive(_menu < 3);
		}
		GameObject[] array2 = new GameObject[]
		{
			this.objHItem,
			this.objLightCategory
		};
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].SetActive(false);
		}
		this.HItemCtrl.ConfirmPanel.SetActive(false);
		for (int k = 0; k < this.categories.SubCategoryActive.Length; k++)
		{
			this.categories.SubCategoryActive[k] = false;
		}
	}

	// Token: 0x060052B3 RID: 21171 RVA: 0x00240818 File Offset: 0x0023EC18
	public void OnClickSubCategories(int _menu)
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		if (this.endFade == 0)
		{
			return;
		}
		if (this.HItemCtrl.ConfirmPanel.activeSelf)
		{
			return;
		}
		GameObject[] array = new GameObject[]
		{
			this.objCloth.gameObject,
			this.objAccessory.gameObject,
			this.objClothCard.gameObject,
			this.categoryMain.gameObject
		};
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		this.objClothCard.CloseSort();
		this.SetMotionListDraw(false, -1);
		for (int j = 0; j < this.categories.MainCategoryActive.Length; j++)
		{
			this.categories.MainCategoryActive[j] = false;
		}
		bool isMarker = this.hPointCtrl.IsMarker;
		if (isMarker)
		{
			this.MarkerObjSet();
		}
		GameObject[] array2 = new GameObject[]
		{
			this.objHItem,
			this.objLightCategory
		};
		if (array2[_menu].activeSelf)
		{
			array2[_menu].SetActive(false);
			this.categories.SubCategoryActive[(_menu != 0) ? 2 : _menu] = false;
		}
		else
		{
			array2[_menu].SetActive(true);
			this.categories.SubCategoryActive[(_menu != 0) ? 2 : _menu] = true;
			if (_menu == 0)
			{
				this.objHItem.GetComponent<HSceneSpriteHitem>().SetVisible(true);
			}
			else
			{
				for (int k = 0; k < this.infoMapLight.Length; k++)
				{
					if (this.infoMapLight[k] != null)
					{
						if (this.infoMapLight[k].light.isActiveAndEnabled)
						{
							this.colorPicker.SetColor(this.infoMapLight[k].light.color);
							break;
						}
					}
				}
			}
			for (int l = 0; l < array2.Length; l++)
			{
				if (l != _menu)
				{
					array2[l].SetActive(false);
				}
			}
			for (int m = 0; m < this.categories.SubCategoryActive.Length; m++)
			{
				if (m != ((_menu != 0) ? 2 : _menu))
				{
					this.categories.SubCategoryActive[m] = false;
				}
			}
		}
		this.charaChoice.CloseChoice();
		this.charaChoice.gameObject.SetActive(false);
	}

	// Token: 0x060052B4 RID: 21172 RVA: 0x00240AD4 File Offset: 0x0023EED4
	public void OnClickFinishBefore()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.FinishBefore;
	}

	// Token: 0x060052B5 RID: 21173 RVA: 0x00240B2C File Offset: 0x0023EF2C
	public void OnClickFinishInSide()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.FinishInSide;
	}

	// Token: 0x060052B6 RID: 21174 RVA: 0x00240B84 File Offset: 0x0023EF84
	public void OnClickFinishOutSide()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.FinishOutSide;
	}

	// Token: 0x060052B7 RID: 21175 RVA: 0x00240BDC File Offset: 0x0023EFDC
	public void OnClickFinishSame()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.FinishSame;
	}

	// Token: 0x060052B8 RID: 21176 RVA: 0x00240C34 File Offset: 0x0023F034
	public void OnClickFinishDrink()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.FinishDrink;
	}

	// Token: 0x060052B9 RID: 21177 RVA: 0x00240C8C File Offset: 0x0023F08C
	public void OnClickFinishVomit()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.FinishVomit;
	}

	// Token: 0x060052BA RID: 21178 RVA: 0x00240CE4 File Offset: 0x0023F0E4
	public void OnClickSpanking()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.Spnking;
	}

	// Token: 0x060052BB RID: 21179 RVA: 0x00240D3C File Offset: 0x0023F13C
	public void OnClickSceneEnd()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		if (this.endFade == 0)
		{
			return;
		}
		Singleton<GameCursor>.Instance.SetCursorLock(false);
		ConfirmScene.Sentence = "Hシーンを終了しますか";
		ConfirmScene.OnClickedYes = delegate()
		{
			this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.SceneEnd;
		};
		ConfirmScene.OnClickedNo = delegate()
		{
		};
		Singleton<Game>.Instance.LoadDialog();
	}

	// Token: 0x060052BC RID: 21180 RVA: 0x00240DE8 File Offset: 0x0023F1E8
	public void OnClickStopFeel(int _sex)
	{
		if ((Singleton<Scene>.IsInstance() && (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade)) || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		if (_sex == 0)
		{
			this.ctrlFlag.stopFeelMale = !this.ctrlFlag.stopFeelMale;
		}
		else
		{
			this.ctrlFlag.stopFeelFemal = !this.ctrlFlag.stopFeelFemal;
		}
	}

	// Token: 0x060052BD RID: 21181 RVA: 0x00240E7B File Offset: 0x0023F27B
	public void OnClickMovePoint()
	{
		if (this.endFade == 0)
		{
			return;
		}
		if (this.HItemCtrl.ConfirmPanel.activeSelf)
		{
			return;
		}
		this.MarkerObjSet();
	}

	// Token: 0x060052BE RID: 21182 RVA: 0x00240EA8 File Offset: 0x0023F2A8
	public void OnClickLeave()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		if (!this.IsEnableLeaveItToYou())
		{
			return;
		}
		if (!UnityEngine.Input.GetMouseButtonUp(0))
		{
			return;
		}
		this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.LeaveItToYou;
		this.SetMotionListDraw(false, -1);
		this.categoryMain.gameObject.SetActive(false);
		this.categories.MainCategoryActive[3] = false;
	}

	// Token: 0x060052BF RID: 21183 RVA: 0x00240F40 File Offset: 0x0023F340
	public void OnValueLightColorChanged(Color color)
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.infoMapLight[i] != null)
			{
				this.infoMapLight[i].light.color = color;
			}
		}
		GlobalMethod.setCameraMoveFlag(this.ctrlFlag.cameraCtrl, false);
	}

	// Token: 0x060052C0 RID: 21184 RVA: 0x00240FD4 File Offset: 0x0023F3D4
	public void OnValueLightDireChanged()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		float x = this.categoryLightDir.GetValue(0) * 360f;
		float y = this.categoryLightDir.GetValue(1) * 360f;
		for (int i = 0; i < 2; i++)
		{
			if (this.infoMapLight[i] != null)
			{
				this.infoMapLight[i].objCharaLight.transform.localRotation = Quaternion.Euler(x, y, 0f);
			}
		}
		GlobalMethod.setCameraMoveFlag(this.ctrlFlag.cameraCtrl, false);
	}

	// Token: 0x060052C1 RID: 21185 RVA: 0x002410A0 File Offset: 0x0023F4A0
	public void OnValuePowerChanged()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.infoMapLight[i] != null)
			{
				this.infoMapLight[i].light.intensity = Mathf.Lerp(this.infoMapLight[i].minIntensity, this.infoMapLight[i].maxIntensity, this.categoryLightDir.GetValue(2));
			}
		}
		GlobalMethod.setCameraMoveFlag(this.ctrlFlag.cameraCtrl, false);
	}

	// Token: 0x060052C2 RID: 21186 RVA: 0x0024115E File Offset: 0x0023F55E
	public void ReSetLight()
	{
		this.ReSetLightDir();
		this.ReSetLightColor();
		this.ReSetLightPower();
	}

	// Token: 0x060052C3 RID: 21187 RVA: 0x00241174 File Offset: 0x0023F574
	public void ReSetLightDir()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.infoMapLight[i] != null)
			{
				this.infoMapLight[i].objCharaLight.transform.localRotation = this.infoMapLight[i].initRot;
			}
		}
	}

	// Token: 0x060052C4 RID: 21188 RVA: 0x002411CC File Offset: 0x0023F5CC
	public void ReSetLightPower()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.infoMapLight[i] != null)
			{
				this.infoMapLight[i].light.intensity = Mathf.Lerp(this.infoMapLight[i].minIntensity, this.infoMapLight[i].maxIntensity, this.infoMapLight[i].initIntensity);
			}
		}
	}

	// Token: 0x060052C5 RID: 21189 RVA: 0x0024123C File Offset: 0x0023F63C
	public void ReSetLightColor()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.infoMapLight[i] != null)
			{
				this.infoMapLight[i].light.color = this.infoMapLight[i].initColor;
			}
		}
	}

	// Token: 0x060052C6 RID: 21190 RVA: 0x0024128C File Offset: 0x0023F68C
	public void OnClickConfig()
	{
		bool flag = Singleton<Game>.Instance.Config != null;
		flag |= (Singleton<Game>.Instance.Dialog != null);
		flag |= (Singleton<Game>.Instance.ExitScene != null);
		flag |= (Singleton<Game>.Instance.MapShortcutUI != null);
		if (flag)
		{
			return;
		}
		GameObject[] array = new GameObject[]
		{
			this.objCloth.gameObject,
			this.objAccessory.gameObject,
			this.objClothCard.gameObject,
			this.categoryMain.gameObject
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].activeSelf)
			{
				array[i].SetActive(false);
				this.categories.MainCategoryActive[i] = false;
			}
		}
		this.SetMotionListDraw(false, -1);
		this.charaChoice.CloseChoice();
		this.charaChoice.gameObject.SetActive(false);
		this.objClothCard.CloseSort();
		if (this.hPointCtrl.IsMarker)
		{
			this.hPointCtrl.MarkerObjDel();
			this.categories.MainCategoryActive[this.categories.MainCategoryActive.Length - 1] = false;
		}
		GameObject[] array2 = new GameObject[]
		{
			this.objHItem,
			this.objLightCategory
		};
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].SetActive(false);
		}
		this.categories.SubCategoryActive[0] = false;
		this.categories.SubCategoryActive[2] = false;
		this.SetEnableHItem(false);
		this.HItemCtrl.ConfirmPanel.SetActive(false);
		this.categories.AllForceClose(1);
		ConfigWindow.UnLoadAction = delegate()
		{
		};
		ConfigWindow.TitleChangeAction = delegate()
		{
			this.ctrlFlag.click = HSceneFlagCtrl.ClickKind.SceneEnd;
			this.hScene.NowStateIsEnd = true;
			this.hScene.ConfigEnd();
			ConfigWindow.UnLoadAction = null;
			Singleton<Game>.Instance.Dialog.TimeScale = 1f;
		};
		Singleton<Game>.Instance.LoadConfig();
	}

	// Token: 0x060052C7 RID: 21191 RVA: 0x00241484 File Offset: 0x0023F884
	public void FadeState(HSceneSprite.FadeKind _kind, float _timeFade = -1f)
	{
		this.isFade = true;
		this.timeFadeTime = 0f;
		if (_timeFade < 0f)
		{
			this.timeFade = ((_kind == HSceneSprite.FadeKind.OutIn) ? (this.timeFadeBase * 2f) : this.timeFadeBase);
		}
		else
		{
			this.timeFade = ((_kind == HSceneSprite.FadeKind.OutIn) ? (_timeFade * 2f) : _timeFade);
		}
		this.kindFade = _kind;
		switch (this.kindFade)
		{
		case HSceneSprite.FadeKind.Out:
			this.kindFadeProc = HSceneSprite.FadeKindProc.Out;
			break;
		case HSceneSprite.FadeKind.In:
			this.kindFadeProc = HSceneSprite.FadeKindProc.In;
			break;
		case HSceneSprite.FadeKind.OutIn:
			this.kindFadeProc = HSceneSprite.FadeKindProc.OutIn;
			break;
		}
	}

	// Token: 0x060052C8 RID: 21192 RVA: 0x0024153F File Offset: 0x0023F93F
	public HSceneSprite.FadeKindProc GetFadeKindProc()
	{
		return this.kindFadeProc;
	}

	// Token: 0x060052C9 RID: 21193 RVA: 0x00241548 File Offset: 0x0023F948
	private bool FadeProc()
	{
		if (!this.imageFade)
		{
			return false;
		}
		if (!this.isFade)
		{
			return false;
		}
		this.timeFadeTime += Time.deltaTime;
		Color color = this.imageFade.color;
		float num = Mathf.Clamp01(this.timeFadeTime / this.timeFade);
		num = this.fadeAnimation.Evaluate(num);
		switch (this.kindFade)
		{
		case HSceneSprite.FadeKind.Out:
			color.a = num;
			break;
		case HSceneSprite.FadeKind.In:
			color.a = 1f - num;
			break;
		case HSceneSprite.FadeKind.OutIn:
			color.a = Mathf.Sin(0.017453292f * Mathf.Lerp(0f, 180f, num));
			break;
		}
		this.imageFade.color = color;
		if (num >= 1f)
		{
			this.isFade = false;
			switch (this.kindFade)
			{
			case HSceneSprite.FadeKind.Out:
				this.kindFadeProc = HSceneSprite.FadeKindProc.OutEnd;
				break;
			case HSceneSprite.FadeKind.In:
				this.kindFadeProc = HSceneSprite.FadeKindProc.InEnd;
				break;
			case HSceneSprite.FadeKind.OutIn:
				this.kindFadeProc = HSceneSprite.FadeKindProc.OutInEnd;
				break;
			}
		}
		return true;
	}

	// Token: 0x060052CA RID: 21194 RVA: 0x00241684 File Offset: 0x0023FA84
	public bool LoadMotionList(int _motion)
	{
		List<GameObject> list = this.hSceneMotionPool.GetList();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.activeSelf)
			{
				list[i].gameObject.SetActive(false);
			}
		}
		this.hSceneScrollNodes.Clear();
		if (_motion < 0 || this.lstAnimInfo.Length <= _motion)
		{
			return true;
		}
		int j = 0;
		while (j < this.lstAnimInfo[_motion].Count)
		{
			if (!this.hSceneManager.bMerchant || this.hSceneManager.MerchantLimit != 1)
			{
				goto IL_DD;
			}
			if (this.PlayerSex != 0 || _motion == 1)
			{
				if (this.PlayerSex != 1 || _motion == 4)
				{
					goto IL_DD;
				}
			}
			IL_296:
			j++;
			continue;
			IL_DD:
			if (!this.CheckMotionLimit(this.lstAnimInfo[_motion][j]))
			{
				goto IL_296;
			}
			GameObject objClone = this.hSceneMotionPool.Get(this.hSceneScrollNodes.Count);
			HAnimationInfoComponent component = objClone.GetComponent<HAnimationInfoComponent>();
			component.info = this.lstAnimInfo[_motion][j];
			objClone.transform.SetParent(this.objMotionList.transform, false);
			GameObject gameObject = objClone.transform.FindLoop("Label");
			if (gameObject)
			{
				Text component2 = gameObject.GetComponent<Text>();
				component2.text = component.info.nameAnimation;
				if (component2.text != this.ctrlFlag.nowAnimationInfo.nameAnimation)
				{
					objClone.GetComponent<Toggle>().isOn = false;
				}
				else
				{
					objClone.GetComponent<Toggle>().isOn = true;
				}
			}
			UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
			int no = this.hSceneScrollNodes.Count;
			PointerClickTrigger pointerClickTrigger = objClone.GetComponent<PointerClickTrigger>();
			if (pointerClickTrigger == null)
			{
				pointerClickTrigger = objClone.AddComponent<PointerClickTrigger>();
			}
			pointerClickTrigger.Triggers.Clear();
			pointerClickTrigger.Triggers.Add(triggerEvent);
			triggerEvent.AddListener(delegate(BaseEventData _)
			{
				if (no != this.objMotionList.transform.parent.GetComponent<ScrollCylinder>().GetTarget().Item1)
				{
					this.objMotionList.transform.parent.GetComponent<ScrollCylinder>().SetTarget(objClone.GetComponent<ScrollCylinderNode>());
					return;
				}
				this.OnChangePlaySelect(objClone);
				if (this.ChangeStart)
				{
					foreach (ScrollCylinderNode scrollCylinderNode in this.hSceneScrollNodes)
					{
						scrollCylinderNode.text.raycastTarget = false;
					}
				}
			});
			if (this.lstAnimInfo[_motion][j] == this.ctrlFlag.nowAnimationInfo)
			{
				objClone.GetComponent<Toggle>().isOn = true;
			}
			this.hSceneScrollNodes.Add(objClone.GetComponent<ScrollCylinderNode>());
			goto IL_296;
		}
		Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
		{
			this.MotionScroll.ListNodeSet(this.hSceneScrollNodes);
		});
		return true;
	}

	// Token: 0x060052CB RID: 21195 RVA: 0x0024195C File Offset: 0x0023FD5C
	public bool SetAnimationMenu()
	{
		int[] array = new int[this.lstAnimInfo.Length];
		int i = 0;
		while (i < this.lstAnimInfo.Length)
		{
			if (!this.hSceneManager.bMerchant || this.hSceneManager.MerchantLimit == 2)
			{
				goto IL_B9;
			}
			if (this.hSceneManager.MerchantLimit != 1)
			{
				break;
			}
			if ((this.hSceneManager.Player.ChaControl.sex != 0 && !this.hSceneManager.bFutanari) || i == 1)
			{
				if (this.hSceneManager.Player.ChaControl.sex != 1 || this.hSceneManager.bFutanari || i == 4)
				{
					goto IL_B9;
				}
			}
			IL_101:
			i++;
			continue;
			IL_B9:
			for (int j = 0; j < this.lstAnimInfo[i].Count; j++)
			{
				if (this.CheckMotionLimit(this.lstAnimInfo[i][j]))
				{
					array[i]++;
				}
			}
			goto IL_101;
		}
		for (int k = 0; k < array.Length; k++)
		{
			this.canMainCategory[k] = (array[k] != 0);
			this.categoryMain.SetActive(this.canMainCategory[k], k);
		}
		if (this.PlayerSex == -1)
		{
			this.categoryMain.SetActive(true, -1);
		}
		return true;
	}

	// Token: 0x060052CC RID: 21196 RVA: 0x00241AD0 File Offset: 0x0023FED0
	private void UIFade()
	{
		if (this.endFade != 0)
		{
			return;
		}
		this.nowFadeTime += Time.unscaledDeltaTime;
		this.UIGroup.alpha = Mathf.Lerp(0f, 1f, this.nowFadeTime / this.fadeTime);
		if (this.UIGroup.alpha >= 1f)
		{
			this.endFade = 1;
			this.UIGroup.blocksRaycasts = true;
		}
	}

	// Token: 0x060052CD RID: 21197 RVA: 0x00241B4C File Offset: 0x0023FF4C
	public void PopupCommands(bool isForce)
	{
		this.hScene.ctrlVoice.HBeforeHouchiTime = 0f;
		Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
		Singleton<Manager.Input>.Instance.SetupState();
		this.startList = Singleton<Manager.Resources>.Instance.HSceneTable.lstStartAnimInfo;
		MapUIContainer.CommandList.CancelEvent = null;
		if (this.hSceneManager.Player.ChaControl.sex == 0 || (this.hSceneManager.Player.ChaControl.sex == 1 && this.ctrlFlag.bFutanari))
		{
			ChaControl[] females = this.hScene.GetFemales();
			if (females[1] == null)
			{
				this.CommandRefresh(isForce);
			}
			else if (this.StartListContain(this.startList, this.hSceneManager.height, HSceneManager.HEvent.Normal, 5))
			{
				CommCommandList.CommandInfo[] options = new CommCommandList.CommandInfo[]
				{
					new CommCommandList.CommandInfo("リードする", null, delegate(int x)
					{
						this.ctrlFlag.AddParam(8, 1);
						this.hScene.SetStartAnimationInfo(HSceneManager.HEvent.Normal, 5);
						this.CommandProc();
						Singleton<HSceneFlagCtrl>.Instance.BeforeHWait = false;
					})
				};
				MapUIContainer.RefreshCommands(0, options);
			}
			MapUIContainer.SetActiveCommandList(true, "どう始める？");
		}
		else if (this.StartListContain(this.startList, this.hSceneManager.height, HSceneManager.HEvent.Normal, 4))
		{
			CommCommandList.CommandInfo[] options2 = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("リードする", null, delegate(int x)
				{
					this.ctrlFlag.AddParam(7, 1);
					this.hScene.SetStartAnimationInfo(HSceneManager.HEvent.Normal, 4);
					this.CommandProc();
					Singleton<HSceneFlagCtrl>.Instance.BeforeHWait = false;
				})
			};
			MapUIContainer.RefreshCommands(0, options2);
			MapUIContainer.SetActiveCommandList(true, "どう始める？");
		}
	}

	// Token: 0x060052CE RID: 21198 RVA: 0x00241CBC File Offset: 0x002400BC
	private void CommandRefresh(bool isForce)
	{
		List<CommCommandList.CommandInfo> list = new List<CommCommandList.CommandInfo>();
		if (!isForce)
		{
			bool[] array = new bool[]
			{
				this.StartListContain(this.startList, this.hSceneManager.height, HSceneManager.HEvent.Normal, 0),
				this.StartListContain(this.startList, this.hSceneManager.height, HSceneManager.HEvent.Normal, 1),
				this.StartListContain(this.startList, this.hSceneManager.height, HSceneManager.HEvent.Normal, 2)
			};
			CommCommandList.CommandInfo[] array2 = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("リードする", null, delegate(int x)
				{
					this.ctrlFlag.AddParam(0, 0);
					this.hScene.SetStartAnimationInfo(HSceneManager.HEvent.Normal, 0);
					this.CommandProc();
					Singleton<HSceneFlagCtrl>.Instance.BeforeHWait = false;
				}),
				new CommCommandList.CommandInfo("してほしい", null, delegate(int x)
				{
					this.ctrlFlag.AddParam(1, 0);
					this.hScene.SetStartAnimationInfo(HSceneManager.HEvent.Normal, 1);
					this.CommandProc();
					Singleton<HSceneFlagCtrl>.Instance.BeforeHWait = false;
				}),
				new CommCommandList.CommandInfo("いきなり挿入する", null, delegate(int x)
				{
					this.ctrlFlag.AddParam(2, 0);
					this.hScene.SetStartAnimationInfo(HSceneManager.HEvent.Normal, 2);
					this.CommandProc();
					Singleton<HSceneFlagCtrl>.Instance.BeforeHWait = false;
				})
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					list.Add(array2[i]);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			array2 = new CommCommandList.CommandInfo[list.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = list[j];
			}
			MapUIContainer.RefreshCommands(0, array2);
		}
		else
		{
			bool[] array = new bool[]
			{
				this.StartListContain(this.startList, this.hSceneManager.height, HSceneManager.HEvent.Normal, 1),
				this.StartListContain(this.startList, this.hSceneManager.height, HSceneManager.HEvent.Normal, 2)
			};
			CommCommandList.CommandInfo[] array2 = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("奉仕させる", null, delegate(int x)
				{
					this.hScene.SetStartAnimationInfo(HSceneManager.HEvent.Normal, 1);
					this.CommandProc();
					Singleton<HSceneFlagCtrl>.Instance.BeforeHWait = false;
				}),
				new CommCommandList.CommandInfo("挿入する", null, delegate(int x)
				{
					this.hScene.SetStartAnimationInfo(HSceneManager.HEvent.Normal, 2);
					this.CommandProc();
					Singleton<HSceneFlagCtrl>.Instance.BeforeHWait = false;
				})
			};
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k])
				{
					list.Add(array2[k]);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			array2 = new CommCommandList.CommandInfo[list.Count];
			for (int l = 0; l < array2.Length; l++)
			{
				array2[l] = list[l];
			}
			MapUIContainer.RefreshCommands(0, array2);
		}
	}

	// Token: 0x060052CF RID: 21199 RVA: 0x00241EE4 File Offset: 0x002402E4
	private void CommandProc()
	{
		MapUIContainer.SetActiveCommandList(false);
		this.beforeChoice = MapUIContainer.CommandList.OnCompletedStopAsObservable().Take(1).Subscribe(delegate(Unit _)
		{
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			this.hSceneManager.Player.CameraControl.enabled = false;
			this.hSceneManager.Player.Controller.enabled = false;
			this.hSceneManager.Player.Animation.enabled = false;
			this.hSceneManager.Player.CameraControl.EnabledInput = false;
		});
		this.StartAnimInfo = this.hScene.StartAnimInfo;
		bool isForceResetCamera = true;
		this.hScene.setCameraLoad(this.StartAnimInfo, isForceResetCamera);
	}

	// Token: 0x060052D0 RID: 21200 RVA: 0x00241F44 File Offset: 0x00240344
	public void MarkerObjSet()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		if (!this.hPointCtrl.IsMarker)
		{
			Actor actor = this.hSceneManager.females[0];
			int areaID = actor.AreaID;
			this.hPointCtrl.MarkerObjSet(this.ctrlFlag.nowHPoint.transform.position, Singleton<Map>.Instance.MapID, areaID);
			this.categories.MainCategoryActive[this.categories.MainCategoryActive.Length - 1] = true;
			GameObject[] array = new GameObject[]
			{
				this.objCloth.gameObject,
				this.objAccessory.gameObject,
				this.objClothCard.gameObject,
				this.categoryMain.gameObject
			};
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
				if (i == 3)
				{
					this.SetMotionListDraw(false, -1);
				}
				this.charaChoice.CloseChoice();
				this.charaChoice.gameObject.SetActive(false);
				this.categories.MainCategoryActive[i] = false;
			}
			this.objClothCard.CloseSort();
			GameObject[] array2 = new GameObject[]
			{
				this.objHItem,
				this.objLightCategory
			};
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].SetActive(false);
			}
			for (int k = 0; k < this.categories.SubCategoryActive.Length; k++)
			{
				this.categories.SubCategoryActive[k] = false;
			}
		}
		else
		{
			this.hPointCtrl.MarkerObjDel();
			this.categories.MainCategoryActive[this.categories.MainCategoryActive.Length - 1] = false;
		}
	}

	// Token: 0x060052D1 RID: 21201 RVA: 0x00242140 File Offset: 0x00240540
	private bool CheckMotionLimit(HScene.AnimationListInfo lstAnimInfo)
	{
		if (this.PlayerSex == 0 || (this.PlayerSex == 1 && this.ctrlFlag.bFutanari))
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
			if (lstAnimInfo.ActionCtrl.Item1 == 3 && !this.NonTokushuCheckIDs.Contains(lstAnimInfo.id) && lstAnimInfo.fileMale == string.Empty)
			{
				return false;
			}
		}
		else if (this.PlayerSex == 1 && lstAnimInfo.nPromiscuity < 2)
		{
			return false;
		}
		if (!this.hSceneManager.bMerchant)
		{
			if (lstAnimInfo.nHentai == 1 && !this.hSceneManager.isHAddTaii[0])
			{
				return false;
			}
			if (lstAnimInfo.nHentai == 2 && !this.hSceneManager.isHAddTaii[1])
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
				if (lstAnimInfo.nIyaAction == 0)
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
		if (!this.usePoint)
		{
			int item = this.hSceneManager.hitHmesh.Item3;
			if (item != -1 && !lstAnimInfo.nPositons.Contains(item))
			{
				return false;
			}
			if (item == -1 && !lstAnimInfo.nPositons.Contains(0) && !lstAnimInfo.nPositons.Contains(1))
			{
				return false;
			}
		}
		else if (this.ctrlFlag.HPointID == 0)
		{
			if (!lstAnimInfo.nPositons.Contains(this.hPointCtrl.InitNull.Item3))
			{
				return false;
			}
		}
		else
		{
			foreach (HPoint.NotMotionInfo notMotionInfo in this.hPointCtrl.lstMarker[this.ctrlFlag.HPointID].Item2.notMotion)
			{
				if (notMotionInfo.motionID.Contains(lstAnimInfo.id))
				{
					return false;
				}
			}
			if (!HPointCtrl.DicTupleContainsValue(this.hPointCtrl.lstMarker[this.ctrlFlag.HPointID].Item2._nPlace, lstAnimInfo.nPositons, 0))
			{
				return false;
			}
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

	// Token: 0x060052D2 RID: 21202 RVA: 0x002424FC File Offset: 0x002408FC
	private bool CheckAutoMotionLimit(HScene.AnimationListInfo lstAnimInfo)
	{
		if (!this.hSceneManager.bMerchant)
		{
			if (this.hSceneManager.EventKind == HSceneManager.HEvent.Yobai)
			{
				return false;
			}
			if (this.hSceneManager.isForce)
			{
				return false;
			}
		}
		if (this.ctrlFlag.isFaintness)
		{
			return false;
		}
		if (!this.hSceneManager.HSkil.ContainsValue(13))
		{
			return false;
		}
		if (this.PlayerSex == 0 || (this.PlayerSex == 1 && this.ctrlFlag.bFutanari))
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
			if (lstAnimInfo.ActionCtrl.Item1 == 3 && !this.NonTokushuCheckIDs.Contains(lstAnimInfo.id) && lstAnimInfo.fileMale == string.Empty)
			{
				return false;
			}
		}
		else if (this.PlayerSex == 1 && lstAnimInfo.nPromiscuity < 2)
		{
			return false;
		}
		if (!this.hSceneManager.bMerchant)
		{
			if (lstAnimInfo.nHentai == 1 && !this.hSceneManager.isHAddTaii[0])
			{
				return false;
			}
			if (lstAnimInfo.nHentai == 2 && !this.hSceneManager.isHAddTaii[1])
			{
				return false;
			}
		}
		if (this.hSceneManager.bMerchant && !lstAnimInfo.bMerchantMotion)
		{
			return false;
		}
		if (!this.usePoint)
		{
			int item = this.hSceneManager.hitHmesh.Item3;
			if (item != -1 && !lstAnimInfo.nPositons.Contains(item))
			{
				return false;
			}
			if (item == -1 && !lstAnimInfo.nPositons.Contains(0) && !lstAnimInfo.nPositons.Contains(1))
			{
				return false;
			}
		}
		else if (this.ctrlFlag.HPointID == 0)
		{
			if (!lstAnimInfo.nPositons.Contains(this.hPointCtrl.InitNull.Item3))
			{
				return false;
			}
		}
		else
		{
			foreach (HPoint.NotMotionInfo notMotionInfo in this.hPointCtrl.lstMarker[this.ctrlFlag.HPointID].Item2.notMotion)
			{
				if (notMotionInfo.motionID.Contains(lstAnimInfo.id))
				{
					return false;
				}
			}
			if (!HPointCtrl.DicTupleContainsValue(this.hPointCtrl.lstMarker[this.ctrlFlag.HPointID].Item2._nPlace, lstAnimInfo.nPositons, 0))
			{
				return false;
			}
		}
		if (lstAnimInfo.isNeedItem && !this.hSceneManager.CheckHadItem(lstAnimInfo.ActionCtrl.Item1, lstAnimInfo.id))
		{
			return false;
		}
		if (lstAnimInfo.nFaintnessLimit == 1)
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
		else if (lstAnimInfo.nInitiativeFemale == 0)
		{
			return false;
		}
		return true;
	}

	// Token: 0x060052D3 RID: 21203 RVA: 0x00242884 File Offset: 0x00240C84
	private bool StartListContain(List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> _startList, HSceneManager.HEvent target)
	{
		foreach (UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion> valueTuple in _startList)
		{
			if (valueTuple.Item1 == target)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060052D4 RID: 21204 RVA: 0x002428EC File Offset: 0x00240CEC
	private bool StartListContain(List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> _startList, int target, HSceneManager.HEvent hEvent = HSceneManager.HEvent.Normal, int category = -1)
	{
		foreach (UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion> valueTuple in _startList)
		{
			if (valueTuple.Item1 == hEvent)
			{
				if (valueTuple.Item2 == target)
				{
					if (category < 0)
					{
						return true;
					}
					if (valueTuple.Item3.mode == category)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060052D5 RID: 21205 RVA: 0x00242988 File Offset: 0x00240D88
	private bool StartListContain(List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> _startList, HScene.StartMotion target)
	{
		foreach (UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion> valueTuple in _startList)
		{
			if (valueTuple.Item3 == target)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060052D6 RID: 21206 RVA: 0x002429F0 File Offset: 0x00240DF0
	public void ChangeStateAllEquip()
	{
		this.objAccessory.ChangeStateAllEquip(this.AllEquip);
		this.AllEquip ^= true;
	}

	// Token: 0x060052D7 RID: 21207 RVA: 0x00242A14 File Offset: 0x00240E14
	private void OnDisable()
	{
		this.UIGroup.blocksRaycasts = false;
		this.UIGroup.alpha = 0f;
		this.HItemCtrl.EndProc();
		this.endFade = -1;
		this.usePoint = false;
		this.objGaugeLockF.isOn = false;
		this.objGaugeLockM.isOn = false;
		this.categoryFinish.SetActive(true, -1);
		this.categoryFinish.gameObject.SetActive(true);
		this.objAccessory.EndProc();
		this.charaChoice.EndProc();
		this.categoryMain.EndProc();
		this.hPointCtrl.EndProc();
		this.objCloth.gameObject.SetActive(false);
		this.objAccessory.gameObject.SetActive(false);
		this.objClothCard.gameObject.SetActive(false);
		this.categoryMain.gameObject.SetActive(false);
		this.objHItem.SetActive(false);
		this.objLightCategory.SetActive(false);
		this.objMotionListPanel.SetActive(false);
		this.charaChoice.CloseChoice();
		this.charaChoice.gameObject.SetActive(false);
		this.HelpBase.SetActive(false);
		this.ReSetHelpText();
		for (int i = 0; i < this.categories.MainCategoryActive.Length; i++)
		{
			this.categories.MainCategoryActive[i] = false;
		}
		this.isLeaveItToYou = false;
		this.SetMotionListDraw(false, -1);
		this.SetEnableCategoryMain(false);
		this.SetEnableHItem(false);
		this.CategoryScroll.ListClear();
		this.MotionScroll.GetList().Clear();
		this.MotionScroll.ClearBlank();
		this.objClothCard.EndProc();
		this.categoryMainButton.interactable = true;
		this.hPointButton.interactable = true;
		List<GameObject> list = this.hSceneMotionPool.GetList();
		foreach (GameObject obj in list)
		{
			UnityEngine.Object.Destroy(obj);
		}
		list.Clear();
		this.chaFemales = null;
		if (this.beforeChoice != null)
		{
			this.beforeChoice.Dispose();
		}
		this.beforeChoice = null;
		if (base.enabled)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060052D8 RID: 21208 RVA: 0x00242C74 File Offset: 0x00241074
	private void SetHelpActive(bool _active)
	{
		if (this.HelpBase.activeSelf == _active)
		{
			return;
		}
		this.HelpBase.SetActive(_active);
	}

	// Token: 0x060052D9 RID: 21209 RVA: 0x00242C94 File Offset: 0x00241094
	private void SetHelpText(string _text)
	{
		if (this.HelpTxt == null)
		{
			return;
		}
		this.HelpTxt.text = _text;
	}

	// Token: 0x060052DA RID: 21210 RVA: 0x00242CB4 File Offset: 0x002410B4
	private void ReSetHelpText()
	{
		if (this.HelpTxt == null)
		{
			return;
		}
		this.HelpTxt.text = "エッチを開始する";
	}

	// Token: 0x060052DB RID: 21211 RVA: 0x00242CD8 File Offset: 0x002410D8
	public void GuidProc(AnimatorStateInfo ai)
	{
		int id = this.ctrlFlag.nowAnimationInfo.id;
		bool flag = this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 3 && (id == 3 || id == 13 || id == 14);
		if (this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 3 && this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item2 == 5 && !this.NonTokushuCheckIDs.Contains(this.ctrlFlag.nowAnimationInfo.id))
		{
			this.SetHelpActive(false);
			return;
		}
		int num = -1;
		if (!flag)
		{
			for (int i = 0; i < this.GuidTiming.Length; i++)
			{
				for (int j = 0; j < this.GuidTiming[i].Length; j++)
				{
					if (ai.IsName(this.GuidTiming[i][j]))
					{
						num = i;
						break;
					}
				}
				if (num >= 0)
				{
					break;
				}
			}
		}
		else
		{
			for (int k = 0; k < this.GuidTimingEtc.Length; k++)
			{
				for (int l = 0; l < this.GuidTimingEtc[k].Length; l++)
				{
					if (ai.IsName(this.GuidTimingEtc[k][l]))
					{
						num = k;
						break;
					}
				}
				if (num >= 0)
				{
					break;
				}
			}
		}
		if (num < 0)
		{
			this.SetHelpActive(false);
			return;
		}
		if ((num != 1 && this.hScene.ctrlVoice.nowVoices[0].state == HVoiceCtrl.VoiceKind.voice) || this.hScene.ctrlVoice.nowVoices[0].state == HVoiceCtrl.VoiceKind.startVoice || this.hScene.ctrlVoice.nowVoices[1].state == HVoiceCtrl.VoiceKind.voice || this.hScene.ctrlVoice.nowVoices[1].state == HVoiceCtrl.VoiceKind.startVoice || this.ctrlFlag.voice.playStart > 4)
		{
			this.SetHelpActive(false);
			return;
		}
		if (this.ctrlFlag.nowAnimationInfo.ActionCtrl.Item1 == 2)
		{
			Sonyu sonyu = this.hScene.GetProcBase() as Sonyu;
			if (sonyu != null && sonyu.nowInsert)
			{
				this.SetHelpActive(false);
				return;
			}
		}
		if (this.hScene.NowChangeAnim)
		{
			this.SetHelpActive(false);
			return;
		}
		if (num != 1 && this.ctrlFlag.initiative != 0)
		{
			this.SetHelpActive(false);
			return;
		}
		switch (num)
		{
		case 0:
			this.ReSetHelpText();
			this.SetHelpActive(true);
			return;
		case 1:
			this.SetHelpText("上で速くなる\n下で遅くなる");
			this.SetHelpActive(true);
			return;
		case 2:
			this.SetHelpText("上で続ける\n下で抜く");
			this.SetHelpActive(true);
			return;
		case 3:
			this.SetHelpText("続ける");
			this.SetHelpActive(true);
			return;
		default:
			this.SetHelpActive(false);
			return;
		}
	}

	// Token: 0x060052DC RID: 21212 RVA: 0x00242FFE File Offset: 0x002413FE
	public void HelpBaseActive(bool _active)
	{
		if (this.HelpBaseConfig == null)
		{
			return;
		}
		if (this.HelpBaseConfig.activeSelf == _active)
		{
			return;
		}
		this.HelpBaseConfig.SetActive(_active);
	}

	// Token: 0x060052DD RID: 21213 RVA: 0x00243030 File Offset: 0x00241430
	public bool GetHelpActive()
	{
		return !(this.HelpBaseConfig == null) && this.HelpBaseConfig.activeSelf;
	}

	// Token: 0x060052DE RID: 21214 RVA: 0x00243050 File Offset: 0x00241450
	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x04004D08 RID: 19720
	public Toggle objGaugeLockF;

	// Token: 0x04004D09 RID: 19721
	public Toggle objGaugeLockM;

	// Token: 0x04004D0A RID: 19722
	public Image imageMGauge;

	// Token: 0x04004D0B RID: 19723
	public Image imageFGauge;

	// Token: 0x04004D0C RID: 19724
	public Color colorMGauge = Color.white;

	// Token: 0x04004D0D RID: 19725
	public Color colorFGauge = Color.white;

	// Token: 0x04004D0E RID: 19726
	private Color colorMGaugeDef;

	// Token: 0x04004D0F RID: 19727
	private Color colorFGaugeDef;

	// Token: 0x04004D10 RID: 19728
	[SerializeField]
	private ParticleSystem AtariEffect;

	// Token: 0x04004D11 RID: 19729
	public GameObject buttonEnd;

	// Token: 0x04004D12 RID: 19730
	public HSceneSpriteFinishCategory categoryFinish;

	// Token: 0x04004D13 RID: 19731
	public HSceneSpriteClothCondition objCloth;

	// Token: 0x04004D14 RID: 19732
	public HSceneSpriteAccessoryCondition objAccessory;

	// Token: 0x04004D15 RID: 19733
	public HSceneSpriteCoordinatesCard objClothCard;

	// Token: 0x04004D16 RID: 19734
	public HSceneSpriteChaChoice charaChoice;

	// Token: 0x04004D17 RID: 19735
	public HsceneSpriteTaiiCategory categoryMain;

	// Token: 0x04004D18 RID: 19736
	private RotationScroll CategoryScroll;

	// Token: 0x04004D19 RID: 19737
	[SerializeField]
	private ScrollCylinder MotionScroll;

	// Token: 0x04004D1A RID: 19738
	public Toggle tglLeaveItToYou;

	// Token: 0x04004D1B RID: 19739
	public GameObject objMotionListPanel;

	// Token: 0x04004D1C RID: 19740
	public GameObject objMotionListInstanceButton;

	// Token: 0x04004D1D RID: 19741
	public GameObject objMotionList;

	// Token: 0x04004D1E RID: 19742
	public float HpointSearchRange;

	// Token: 0x04004D1F RID: 19743
	public GameObject objHItem;

	// Token: 0x04004D20 RID: 19744
	public HSceneSpriteHitem HItemCtrl;

	// Token: 0x04004D21 RID: 19745
	public GameObject objLightCategory;

	// Token: 0x04004D22 RID: 19746
	public HSceneSpriteLightCategory categoryLightDir;

	// Token: 0x04004D23 RID: 19747
	public PickerRect colorPicker;

	// Token: 0x04004D24 RID: 19748
	public AnimationCurve fadeAnimation;

	// Token: 0x04004D25 RID: 19749
	public HScene.AnimationListInfo StartAnimInfo;

	// Token: 0x04004D26 RID: 19750
	public RawImage imageFade;

	// Token: 0x04004D27 RID: 19751
	public float timeFadeBase;

	// Token: 0x04004D28 RID: 19752
	public bool isFade;

	// Token: 0x04004D29 RID: 19753
	[SerializeField]
	private ParticleSystem FeelHitEffect3D;

	// Token: 0x04004D2A RID: 19754
	[SerializeField]
	private Vector3 FeelHitEffect3DOffSet;

	// Token: 0x04004D2B RID: 19755
	public HSceneSpriteCategories categories;

	// Token: 0x04004D2C RID: 19756
	public float fadeTime = 1f;

	// Token: 0x04004D2D RID: 19757
	private HSceneFlagCtrl ctrlFlag;

	// Token: 0x04004D2E RID: 19758
	private HScene hScene;

	// Token: 0x04004D2F RID: 19759
	private HSceneSprite.FadeKind kindFade;

	// Token: 0x04004D30 RID: 19760
	private HSceneSprite.FadeKindProc kindFadeProc;

	// Token: 0x04004D31 RID: 19761
	private float timeFade;

	// Token: 0x04004D32 RID: 19762
	private float timeFadeTime;

	// Token: 0x04004D33 RID: 19763
	private bool isLeaveItToYou;

	// Token: 0x04004D34 RID: 19764
	private ChaControl[] chaFemales;

	// Token: 0x04004D35 RID: 19765
	public bool usePoint;

	// Token: 0x04004D36 RID: 19766
	private List<HScene.AnimationListInfo>[] lstAnimInfo;

	// Token: 0x04004D37 RID: 19767
	public Button categoryMainButton;

	// Token: 0x04004D38 RID: 19768
	public Button hPointButton;

	// Token: 0x04004D39 RID: 19769
	[SerializeField]
	private GameObject[] MenuCategory = new GameObject[2];

	// Token: 0x04004D3A RID: 19770
	[SerializeField]
	private GameObject MenuCategorySub;

	// Token: 0x04004D3B RID: 19771
	[SerializeField]
	private GameObject GuageBase;

	// Token: 0x04004D3C RID: 19772
	private List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> startList;

	// Token: 0x04004D3D RID: 19773
	private List<int> lstFinishVisible = new List<int>();

	// Token: 0x04004D3E RID: 19774
	private HScene.LightInfo[] infoMapLight = new HScene.LightInfo[]
	{
		new HScene.LightInfo(),
		new HScene.LightInfo()
	};

	// Token: 0x04004D3F RID: 19775
	public Animator anim_f;

	// Token: 0x04004D40 RID: 19776
	public Animator anim_m;

	// Token: 0x04004D41 RID: 19777
	public int endFade = -1;

	// Token: 0x04004D42 RID: 19778
	[SerializeField]
	private CanvasGroup UIGroup;

	// Token: 0x04004D43 RID: 19779
	private HSceneManager hSceneManager;

	// Token: 0x04004D44 RID: 19780
	[SerializeField]
	private int PlayerSex;

	// Token: 0x04004D45 RID: 19781
	private bool[] canMainCategory = new bool[6];

	// Token: 0x04004D46 RID: 19782
	private float nowFadeTime;

	// Token: 0x04004D47 RID: 19783
	private HSceneNodePool hSceneMotionPool = new HSceneNodePool();

	// Token: 0x04004D48 RID: 19784
	private List<ScrollCylinderNode> hSceneScrollNodes = new List<ScrollCylinderNode>();

	// Token: 0x04004D49 RID: 19785
	private HPointCtrl hPointCtrl;

	// Token: 0x04004D4A RID: 19786
	[SerializeField]
	private Text MotionListLabel;

	// Token: 0x04004D4B RID: 19787
	[SerializeField]
	private GameObject SelectArea;

	// Token: 0x04004D4C RID: 19788
	private Image[] motionListImages;

	// Token: 0x04004D4D RID: 19789
	private Toggle motionListToggle;

	// Token: 0x04004D4E RID: 19790
	private IDisposable beforeChoice;

	// Token: 0x04004D4F RID: 19791
	public bool ChangeStart;

	// Token: 0x04004D50 RID: 19792
	private bool AllEquip = true;

	// Token: 0x04004D51 RID: 19793
	[SerializeField]
	private GameObject HelpBaseConfig;

	// Token: 0x04004D52 RID: 19794
	[SerializeField]
	private GameObject HelpBase;

	// Token: 0x04004D53 RID: 19795
	[SerializeField]
	private Text HelpTxt;

	// Token: 0x04004D54 RID: 19796
	private const string HelpTextDef = "エッチを開始する";

	// Token: 0x04004D55 RID: 19797
	private string[][] GuidTiming = new string[][]
	{
		new string[]
		{
			"Idle",
			"D_Idle"
		},
		new string[]
		{
			"WLoop",
			"SLoop",
			"OLoop",
			"D_WLoop",
			"D_SLoop",
			"D_OLoop"
		},
		new string[]
		{
			"Orgasm_IN_A",
			"D_Orgasm_IN_A"
		},
		new string[]
		{
			"Orgasm_A",
			"Orgasm_OUT_A",
			"Drink_A",
			"Vomit_A",
			"OrgasmM_OUT_A",
			"D_Orgasm_A",
			"D_OrgasmM_OUT_A"
		}
	};

	// Token: 0x04004D56 RID: 19798
	private string[][] GuidTimingEtc = new string[][]
	{
		new string[]
		{
			"Idle"
		},
		new string[]
		{
			"WLoop",
			"SLoop",
			"MLoop",
			"OLoop"
		},
		new string[0],
		new string[]
		{
			"Orgasm_A"
		}
	};

	// Token: 0x04004D57 RID: 19799
	public readonly List<int> NonTokushuCheckIDs = new List<int>
	{
		3,
		13,
		14,
		109,
		110
	};

	// Token: 0x04004D58 RID: 19800
	private const string CheckmarkName = "Checkmark";

	// Token: 0x04004D59 RID: 19801
	private const string nowSelectName = "NowSelect";

	// Token: 0x02000B07 RID: 2823
	public enum FadeKind
	{
		// Token: 0x04004D5D RID: 19805
		Out,
		// Token: 0x04004D5E RID: 19806
		In,
		// Token: 0x04004D5F RID: 19807
		OutIn
	}

	// Token: 0x02000B08 RID: 2824
	public enum FadeKindProc
	{
		// Token: 0x04004D61 RID: 19809
		None,
		// Token: 0x04004D62 RID: 19810
		Out,
		// Token: 0x04004D63 RID: 19811
		OutEnd,
		// Token: 0x04004D64 RID: 19812
		In,
		// Token: 0x04004D65 RID: 19813
		InEnd,
		// Token: 0x04004D66 RID: 19814
		OutIn,
		// Token: 0x04004D67 RID: 19815
		OutInEnd
	}
}
