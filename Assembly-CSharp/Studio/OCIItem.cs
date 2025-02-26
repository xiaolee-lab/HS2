using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Manager;
using Studio.Sound;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001218 RID: 4632
	public class OCIItem : ObjectCtrlInfo
	{
		// Token: 0x1700203D RID: 8253
		// (get) Token: 0x06009816 RID: 38934 RVA: 0x003EC0B2 File Offset: 0x003EA4B2
		public OIItemInfo itemInfo
		{
			get
			{
				return this.objectInfo as OIItemInfo;
			}
		}

		// Token: 0x1700203E RID: 8254
		// (get) Token: 0x06009817 RID: 38935 RVA: 0x003EC0BF File Offset: 0x003EA4BF
		public bool isAnime
		{
			[CompilerGenerated]
			get
			{
				return this.animator != null && this.animator.enabled;
			}
		}

		// Token: 0x1700203F RID: 8255
		// (get) Token: 0x06009818 RID: 38936 RVA: 0x003EC0E4 File Offset: 0x003EA4E4
		public bool isChangeColor
		{
			get
			{
				bool flag = false;
				if (this.itemComponent != null)
				{
					flag |= (this.itemComponent.check | this.itemComponent.checkGlass);
				}
				if (this.particleComponent != null)
				{
					flag |= this.particleComponent.check;
				}
				return flag;
			}
		}

		// Token: 0x17002040 RID: 8256
		// (get) Token: 0x06009819 RID: 38937 RVA: 0x003EC140 File Offset: 0x003EA540
		public bool[] useColor
		{
			get
			{
				bool[] result = Enumerable.Repeat<bool>(false, 3).ToArray<bool>();
				if (this.itemComponent != null)
				{
					bool[] useColor = this.itemComponent.useColor;
					int i;
					for (i = 0; i < 3; i++)
					{
						useColor.SafeProc(i, delegate(bool _b)
						{
							result[i] = _b;
						});
					}
				}
				if (this.particleComponent != null)
				{
					result[0] |= this.particleComponent.UseColor1;
				}
				return result;
			}
		}

		// Token: 0x17002041 RID: 8257
		// (get) Token: 0x0600981A RID: 38938 RVA: 0x003EC200 File Offset: 0x003EA600
		public bool useColor4
		{
			[CompilerGenerated]
			get
			{
				return this.itemComponent != null && this.itemComponent.checkGlass;
			}
		}

		// Token: 0x17002042 RID: 8258
		// (get) Token: 0x0600981B RID: 38939 RVA: 0x003EC224 File Offset: 0x003EA624
		public Color[] defColor
		{
			get
			{
				Color[] result = Enumerable.Repeat<Color>(Color.white, 3).ToArray<Color>();
				if (this.itemComponent != null && !this.itemComponent.info.IsNullOrEmpty<ItemComponent.Info>())
				{
					int i;
					for (i = 0; i < 3; i++)
					{
						this.itemComponent.info.SafeProc(i, delegate(ItemComponent.Info _i)
						{
							result[i] = _i.defColor;
						});
					}
				}
				if (this.particleComponent != null && this.particleComponent.UseColor1)
				{
					result[0] = this.particleComponent.defColor01;
				}
				return result;
			}
		}

		// Token: 0x17002043 RID: 8259
		// (get) Token: 0x0600981C RID: 38940 RVA: 0x003EC30C File Offset: 0x003EA70C
		public bool[] useMetallic
		{
			[CompilerGenerated]
			get
			{
				return (!(this.itemComponent == null)) ? this.itemComponent.useMetallic : new bool[3];
			}
		}

		// Token: 0x17002044 RID: 8260
		// (get) Token: 0x0600981D RID: 38941 RVA: 0x003EC335 File Offset: 0x003EA735
		public bool[] usePattern
		{
			[CompilerGenerated]
			get
			{
				return (!(this.itemComponent == null)) ? this.itemComponent.usePattern : new bool[3];
			}
		}

		// Token: 0x17002045 RID: 8261
		// (get) Token: 0x0600981E RID: 38942 RVA: 0x003EC35E File Offset: 0x003EA75E
		public bool CheckAlpha
		{
			[CompilerGenerated]
			get
			{
				return this.itemComponent != null && this.itemComponent.checkAlpha;
			}
		}

		// Token: 0x17002046 RID: 8262
		// (get) Token: 0x0600981F RID: 38943 RVA: 0x003EC382 File Offset: 0x003EA782
		public bool CheckEmission
		{
			[CompilerGenerated]
			get
			{
				return this.itemComponent != null && this.itemComponent.CheckEmission;
			}
		}

		// Token: 0x17002047 RID: 8263
		// (get) Token: 0x06009820 RID: 38944 RVA: 0x003EC3A6 File Offset: 0x003EA7A6
		public bool CheckEmissionColor
		{
			[CompilerGenerated]
			get
			{
				return this.itemComponent != null && this.itemComponent.checkEmissionColor;
			}
		}

		// Token: 0x17002048 RID: 8264
		// (get) Token: 0x06009821 RID: 38945 RVA: 0x003EC3CA File Offset: 0x003EA7CA
		public bool CheckEmissionPower
		{
			[CompilerGenerated]
			get
			{
				return this.itemComponent != null && this.itemComponent.checkEmissionStrength;
			}
		}

		// Token: 0x17002049 RID: 8265
		// (get) Token: 0x06009822 RID: 38946 RVA: 0x003EC3EE File Offset: 0x003EA7EE
		public bool CheckLightCancel
		{
			[CompilerGenerated]
			get
			{
				return this.itemComponent != null && this.itemComponent.checkLightCancel;
			}
		}

		// Token: 0x1700204A RID: 8266
		// (get) Token: 0x06009823 RID: 38947 RVA: 0x003EC412 File Offset: 0x003EA812
		public bool IsParticle
		{
			[CompilerGenerated]
			get
			{
				return this.particleComponent != null;
			}
		}

		// Token: 0x1700204B RID: 8267
		// (set) Token: 0x06009824 RID: 38948 RVA: 0x003EC420 File Offset: 0x003EA820
		public bool VisibleIcon
		{
			set
			{
				this.iconComponent.SafeProc(delegate(IconComponent _ic)
				{
					_ic.Active = value;
				});
			}
		}

		// Token: 0x1700204C RID: 8268
		// (get) Token: 0x06009825 RID: 38949 RVA: 0x003EC452 File Offset: 0x003EA852
		public bool checkPanel
		{
			[CompilerGenerated]
			get
			{
				return this.panelComponent != null;
			}
		}

		// Token: 0x1700204D RID: 8269
		// (get) Token: 0x06009826 RID: 38950 RVA: 0x003EC460 File Offset: 0x003EA860
		public bool isFK
		{
			[CompilerGenerated]
			get
			{
				return !this.listBones.IsNullOrEmpty<OCIChar.BoneInfo>();
			}
		}

		// Token: 0x1700204E RID: 8270
		// (get) Token: 0x06009827 RID: 38951 RVA: 0x003EC470 File Offset: 0x003EA870
		public bool isDynamicBone
		{
			[CompilerGenerated]
			get
			{
				return !(this.isFK & this.itemInfo.enableFK) && !this.dynamicBones.IsNullOrEmpty<DynamicBone>();
			}
		}

		// Token: 0x1700204F RID: 8271
		// (get) Token: 0x06009828 RID: 38952 RVA: 0x003EC49D File Offset: 0x003EA89D
		public bool CheckOption
		{
			[CompilerGenerated]
			get
			{
				return this.itemComponent != null && this.itemComponent.CheckOption;
			}
		}

		// Token: 0x17002050 RID: 8272
		// (get) Token: 0x06009829 RID: 38953 RVA: 0x003EC4C1 File Offset: 0x003EA8C1
		public bool CheckAnimePattern
		{
			[CompilerGenerated]
			get
			{
				return this.itemComponent != null && this.itemComponent.CheckAnimePattern;
			}
		}

		// Token: 0x17002051 RID: 8273
		// (get) Token: 0x0600982A RID: 38954 RVA: 0x003EC4E5 File Offset: 0x003EA8E5
		public bool CheckAnim
		{
			[CompilerGenerated]
			get
			{
				return this.isChangeColor | this.checkPanel | this.isFK | this.isDynamicBone | this.CheckOption | this.CheckAnimePattern;
			}
		}

		// Token: 0x17002052 RID: 8274
		// (get) Token: 0x0600982B RID: 38955 RVA: 0x003EC510 File Offset: 0x003EA910
		// (set) Token: 0x0600982C RID: 38956 RVA: 0x003EC518 File Offset: 0x003EA918
		public bool visible
		{
			get
			{
				return this.m_Visible;
			}
			set
			{
				this.m_Visible = value;
				for (int i = 0; i < this.arrayRender.Length; i++)
				{
					this.arrayRender[i].enabled = value;
				}
				if (!this.arrayParticle.IsNullOrEmpty<ParticleSystem>())
				{
					for (int j = 0; j < this.arrayParticle.Length; j++)
					{
						if (value)
						{
							this.arrayParticle[j].Play();
						}
						else
						{
							this.arrayParticle[j].Pause();
						}
					}
				}
				if (this.seComponent != null)
				{
					this.seComponent.enabled = value;
				}
			}
		}

		// Token: 0x17002053 RID: 8275
		// (get) Token: 0x0600982D RID: 38957 RVA: 0x003EC5BE File Offset: 0x003EA9BE
		public bool IsParticleArray
		{
			[CompilerGenerated]
			get
			{
				return !this.arrayParticle.IsNullOrEmpty<ParticleSystem>();
			}
		}

		// Token: 0x0600982E RID: 38958 RVA: 0x003EC5D0 File Offset: 0x003EA9D0
		public override void OnDelete()
		{
			if (!this.listBones.IsNullOrEmpty<OCIChar.BoneInfo>())
			{
				for (int i = 0; i < this.listBones.Count; i++)
				{
					Singleton<GuideObjectManager>.Instance.Delete(this.listBones[i].guideObject, true);
				}
				this.listBones.Clear();
			}
			Singleton<GuideObjectManager>.Instance.Delete(this.guideObject, true);
			UnityEngine.Object.Destroy(this.objectItem);
			if (this.parentInfo != null)
			{
				this.parentInfo.OnDetachChild(this);
			}
			Studio.DeleteInfo(this.objectInfo, true);
		}

		// Token: 0x0600982F RID: 38959 RVA: 0x003EC670 File Offset: 0x003EAA70
		public override void OnAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
			if (_child.parentInfo == null)
			{
				Studio.DeleteInfo(_child.objectInfo, false);
			}
			else
			{
				_child.parentInfo.OnDetachChild(_child);
			}
			if (!this.itemInfo.child.Contains(_child.objectInfo))
			{
				this.itemInfo.child.Add(_child.objectInfo);
			}
			bool flag = false;
			if (_child is OCIItem)
			{
				flag = (_child as OCIItem).IsParticleArray;
			}
			if (!flag)
			{
				_child.guideObject.transformTarget.SetParent(this.childRoot);
			}
			_child.guideObject.parent = this.childRoot;
			_child.guideObject.mode = GuideObject.Mode.World;
			_child.guideObject.moveCalc = GuideMove.MoveCalc.TYPE2;
			if (!flag)
			{
				_child.objectInfo.changeAmount.pos = _child.guideObject.transformTarget.localPosition;
				_child.objectInfo.changeAmount.rot = _child.guideObject.transformTarget.localEulerAngles;
			}
			else if (_child.guideObject.nonconnect)
			{
				_child.objectInfo.changeAmount.pos = _child.guideObject.parent.InverseTransformPoint(_child.guideObject.transformTarget.position);
				Quaternion quaternion = _child.guideObject.transformTarget.rotation * Quaternion.Inverse(_child.guideObject.parent.rotation);
				_child.objectInfo.changeAmount.rot = quaternion.eulerAngles;
			}
			else
			{
				_child.objectInfo.changeAmount.pos = _child.guideObject.parent.InverseTransformPoint(_child.objectInfo.changeAmount.pos);
				Quaternion quaternion2 = Quaternion.Euler(_child.objectInfo.changeAmount.rot) * Quaternion.Inverse(_child.guideObject.parent.rotation);
				_child.objectInfo.changeAmount.rot = quaternion2.eulerAngles;
			}
			_child.guideObject.nonconnect = flag;
			_child.guideObject.calcScale = !flag;
			_child.parentInfo = this;
		}

		// Token: 0x06009830 RID: 38960 RVA: 0x003EC8A0 File Offset: 0x003EACA0
		public override void OnLoadAttach(TreeNodeObject _parent, ObjectCtrlInfo _child)
		{
			if (_child.parentInfo == null)
			{
				Studio.DeleteInfo(_child.objectInfo, false);
			}
			else
			{
				_child.parentInfo.OnDetachChild(_child);
			}
			if (!this.itemInfo.child.Contains(_child.objectInfo))
			{
				this.itemInfo.child.Add(_child.objectInfo);
			}
			bool flag = false;
			if (_child is OCIItem)
			{
				flag = (_child as OCIItem).IsParticleArray;
			}
			if (!flag)
			{
				_child.guideObject.transformTarget.SetParent(this.childRoot, false);
			}
			_child.guideObject.parent = this.childRoot;
			_child.guideObject.nonconnect = flag;
			_child.guideObject.calcScale = !flag;
			_child.guideObject.mode = GuideObject.Mode.World;
			_child.guideObject.moveCalc = GuideMove.MoveCalc.TYPE2;
			_child.objectInfo.changeAmount.OnChange();
			_child.parentInfo = this;
		}

		// Token: 0x06009831 RID: 38961 RVA: 0x003EC998 File Offset: 0x003EAD98
		public override void OnDetach()
		{
			this.parentInfo.OnDetachChild(this);
			this.guideObject.parent = null;
			Studio.AddInfo(this.objectInfo, this);
			this.objectItem.transform.SetParent(Singleton<Scene>.Instance.commonSpace.transform);
			this.objectInfo.changeAmount.pos = this.objectItem.transform.localPosition;
			this.objectInfo.changeAmount.rot = this.objectItem.transform.localEulerAngles;
			this.guideObject.mode = GuideObject.Mode.Local;
			this.guideObject.moveCalc = GuideMove.MoveCalc.TYPE1;
			this.treeNodeObject.ResetVisible();
		}

		// Token: 0x06009832 RID: 38962 RVA: 0x003ECA4C File Offset: 0x003EAE4C
		public override void OnSelect(bool _select)
		{
			int layer = LayerMask.NameToLayer((!_select) ? "Studio/Select" : "Studio/Col");
			if (!this.listBones.IsNullOrEmpty<OCIChar.BoneInfo>())
			{
				for (int i = 0; i < this.listBones.Count; i++)
				{
					this.listBones[i].layer = layer;
				}
			}
		}

		// Token: 0x06009833 RID: 38963 RVA: 0x003ECAB2 File Offset: 0x003EAEB2
		public override void OnDetachChild(ObjectCtrlInfo _child)
		{
			if (!this.itemInfo.child.Remove(_child.objectInfo))
			{
			}
			_child.parentInfo = null;
		}

		// Token: 0x06009834 RID: 38964 RVA: 0x003ECAD8 File Offset: 0x003EAED8
		public override void OnSavePreprocessing()
		{
			base.OnSavePreprocessing();
			if (this.isAnime && this.animator.layerCount != 0)
			{
				AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
				this.itemInfo.animeNormalizedTime = currentAnimatorStateInfo.normalizedTime;
			}
		}

		// Token: 0x17002054 RID: 8276
		// (get) Token: 0x06009835 RID: 38965 RVA: 0x003ECB25 File Offset: 0x003EAF25
		// (set) Token: 0x06009836 RID: 38966 RVA: 0x003ECB32 File Offset: 0x003EAF32
		public override float animeSpeed
		{
			get
			{
				return this.itemInfo.animeSpeed;
			}
			set
			{
				if (Utility.SetStruct<float>(ref this.itemInfo.animeSpeed, value) && this.animator)
				{
					this.animator.speed = this.itemInfo.animeSpeed;
				}
			}
		}

		// Token: 0x06009837 RID: 38967 RVA: 0x003ECB70 File Offset: 0x003EAF70
		public override void OnVisible(bool _visible)
		{
			this.visible = _visible;
		}

		// Token: 0x06009838 RID: 38968 RVA: 0x003ECB79 File Offset: 0x003EAF79
		public void SetColor(Color _color, int _idx)
		{
			if (MathfEx.RangeEqualOn<int>(0, _idx, 3))
			{
				this.itemInfo.colors[_idx].mainColor = _color;
			}
			this.UpdateColor();
		}

		// Token: 0x06009839 RID: 38969 RVA: 0x003ECBA1 File Offset: 0x003EAFA1
		public void SetMetallic(int _idx, float _value)
		{
			if (MathfEx.RangeEqualOn<int>(0, _idx, 3))
			{
				this.itemInfo.colors[_idx].metallic = _value;
			}
			this.UpdateColor();
		}

		// Token: 0x0600983A RID: 38970 RVA: 0x003ECBC9 File Offset: 0x003EAFC9
		public void SetGlossiness(int _idx, float _value)
		{
			if (MathfEx.RangeEqualOn<int>(0, _idx, 3))
			{
				this.itemInfo.colors[_idx].glossiness = _value;
			}
			this.UpdateColor();
		}

		// Token: 0x0600983B RID: 38971 RVA: 0x003ECBF4 File Offset: 0x003EAFF4
		public void SetupPatternTex()
		{
			for (int i = 0; i < 3; i++)
			{
				PatternInfo pattern = this.itemInfo.colors[i].pattern;
				if (!pattern.filePath.IsNullOrEmpty())
				{
					string fileName = Path.GetFileName(pattern.filePath);
					this.SetPatternTex(i, UserData.Path + "pattern/" + fileName);
				}
				else
				{
					this.SetPatternTex(i, pattern.key);
				}
			}
		}

		// Token: 0x0600983C RID: 38972 RVA: 0x003ECC6C File Offset: 0x003EB06C
		public string SetPatternTex(int _idx, int _key)
		{
			if (_key <= 0)
			{
				this.itemInfo.colors[_idx].pattern.key = _key;
				this.itemInfo.colors[_idx].pattern.filePath = string.Empty;
				if (this.itemComponent)
				{
					this.itemComponent.SetPatternTex(_idx, null);
				}
				this.ReleasePatternTex(_idx);
				return "なし";
			}
			List<PatternSelectInfo> lstSelectInfo = Singleton<Studio>.Instance.patternSelectListCtrl.lstSelectInfo;
			PatternSelectInfo patternSelectInfo = lstSelectInfo.Find((PatternSelectInfo p) => p.index == _key);
			string result = "なし";
			if (patternSelectInfo != null)
			{
				if (patternSelectInfo.assetBundle.IsNullOrEmpty())
				{
					string path = UserData.Path + "pattern/" + patternSelectInfo.assetName;
					if (!File.Exists(path))
					{
						return "なし";
					}
					this.texturePattern[_idx] = PngAssist.LoadTexture(path);
					this.itemInfo.colors[_idx].pattern.key = -1;
					this.itemInfo.colors[_idx].pattern.filePath = patternSelectInfo.assetName;
					result = patternSelectInfo.assetName;
				}
				else
				{
					string assetBundleName = patternSelectInfo.assetBundle.Replace("thumb/", string.Empty);
					string assetName = patternSelectInfo.assetName.Replace("thumb_", string.Empty);
					this.texturePattern[_idx] = CommonLib.LoadAsset<Texture2D>(assetBundleName, assetName, false, string.Empty);
					this.itemInfo.colors[_idx].pattern.key = _key;
					this.itemInfo.colors[_idx].pattern.filePath = string.Empty;
					result = patternSelectInfo.name;
				}
			}
			this.itemComponent.SetPatternTex(_idx, this.texturePattern[_idx]);
			UnityEngine.Resources.UnloadUnusedAssets();
			return result;
		}

		// Token: 0x0600983D RID: 38973 RVA: 0x003ECE50 File Offset: 0x003EB250
		public void SetPatternTex(int _idx, string _path)
		{
			if (_path.IsNullOrEmpty())
			{
				this.itemInfo.colors[_idx].pattern.key = 0;
				this.itemInfo.colors[_idx].pattern.filePath = string.Empty;
				this.itemComponent.SetPatternTex(_idx, null);
				this.ReleasePatternTex(_idx);
				return;
			}
			this.itemInfo.colors[_idx].pattern.key = -1;
			this.itemInfo.colors[_idx].pattern.filePath = _path;
			if (File.Exists(_path))
			{
				this.texturePattern[_idx] = PngAssist.LoadTexture(_path);
			}
			this.itemComponent.SetPatternTex(_idx, this.texturePattern[_idx]);
			UnityEngine.Resources.UnloadUnusedAssets();
		}

		// Token: 0x0600983E RID: 38974 RVA: 0x003ECF14 File Offset: 0x003EB314
		private void ReleasePatternTex(int _idx)
		{
			this.texturePattern[_idx] = null;
		}

		// Token: 0x0600983F RID: 38975 RVA: 0x003ECF1F File Offset: 0x003EB31F
		public void SetPatternColor(int _idx, Color _color)
		{
			this.itemInfo.colors[_idx].pattern.color = _color;
			this.UpdateColor();
		}

		// Token: 0x06009840 RID: 38976 RVA: 0x003ECF3F File Offset: 0x003EB33F
		public void SetPatternClamp(int _idx, bool _flag)
		{
			if (!Utility.SetStruct<bool>(ref this.itemInfo.colors[_idx].pattern.clamp, _flag))
			{
				return;
			}
			this.UpdateColor();
		}

		// Token: 0x06009841 RID: 38977 RVA: 0x003ECF6A File Offset: 0x003EB36A
		public void SetPatternUT(int _idx, float _value)
		{
			if (!Utility.SetStruct<float>(ref this.itemInfo.colors[_idx].pattern.uv.z, _value))
			{
				return;
			}
			this.UpdateColor();
		}

		// Token: 0x06009842 RID: 38978 RVA: 0x003ECF9A File Offset: 0x003EB39A
		public void SetPatternVT(int _idx, float _value)
		{
			if (!Utility.SetStruct<float>(ref this.itemInfo.colors[_idx].pattern.uv.w, _value))
			{
				return;
			}
			this.UpdateColor();
		}

		// Token: 0x06009843 RID: 38979 RVA: 0x003ECFCA File Offset: 0x003EB3CA
		public void SetPatternUS(int _idx, float _value)
		{
			if (!Utility.SetStruct<float>(ref this.itemInfo.colors[_idx].pattern.uv.x, _value))
			{
				return;
			}
			this.UpdateColor();
		}

		// Token: 0x06009844 RID: 38980 RVA: 0x003ECFFA File Offset: 0x003EB3FA
		public void SetPatternVS(int _idx, float _value)
		{
			if (!Utility.SetStruct<float>(ref this.itemInfo.colors[_idx].pattern.uv.y, _value))
			{
				return;
			}
			this.UpdateColor();
		}

		// Token: 0x06009845 RID: 38981 RVA: 0x003ED02A File Offset: 0x003EB42A
		public void SetPatternRot(int _idx, float _value)
		{
			if (!Utility.SetStruct<float>(ref this.itemInfo.colors[_idx].pattern.rot, _value))
			{
				return;
			}
			this.UpdateColor();
		}

		// Token: 0x06009846 RID: 38982 RVA: 0x003ED055 File Offset: 0x003EB455
		public void SetAlpha(float _value)
		{
			if (!Utility.SetStruct<float>(ref this.itemInfo.alpha, _value))
			{
				return;
			}
			this.UpdateColor();
		}

		// Token: 0x06009847 RID: 38983 RVA: 0x003ED074 File Offset: 0x003EB474
		public void SetEmissionColor(Color _color)
		{
			this.itemInfo.emissionColor = _color;
			this.UpdateColor();
		}

		// Token: 0x06009848 RID: 38984 RVA: 0x003ED088 File Offset: 0x003EB488
		public void SetEmissionPower(float _value)
		{
			this.itemInfo.emissionPower = _value;
			this.UpdateColor();
		}

		// Token: 0x06009849 RID: 38985 RVA: 0x003ED09C File Offset: 0x003EB49C
		public void SetLightCancel(float _value)
		{
			this.itemInfo.lightCancel = _value;
			this.UpdateColor();
		}

		// Token: 0x0600984A RID: 38986 RVA: 0x003ED0B0 File Offset: 0x003EB4B0
		public void UpdateColor()
		{
			if (this.itemComponent != null && (this.itemComponent.check | this.itemComponent.checkGlass))
			{
				this.itemComponent.UpdateColor(this.itemInfo);
			}
			if (this.particleComponent != null && this.particleComponent.check)
			{
				this.particleComponent.UpdateColor(this.itemInfo);
			}
			if (this.panelComponent != null)
			{
				this.panelComponent.UpdateColor(this.itemInfo);
			}
		}

		// Token: 0x0600984B RID: 38987 RVA: 0x003ED14F File Offset: 0x003EB54F
		public void SetMainTex()
		{
			this.SetMainTex(this.itemInfo.panel.filePath);
		}

		// Token: 0x0600984C RID: 38988 RVA: 0x003ED168 File Offset: 0x003EB568
		public void SetMainTex(string _file)
		{
			if (this.panelComponent == null)
			{
				return;
			}
			if (_file.IsNullOrEmpty())
			{
				this.itemInfo.panel.filePath = string.Empty;
				this.panelComponent.SetMainTex(null);
				this.textureMain = null;
				return;
			}
			this.itemInfo.panel.filePath = _file;
			string path = UserData.Path + BackgroundList.dirName + "/" + _file;
			if (!File.Exists(path))
			{
				return;
			}
			this.textureMain = PngAssist.LoadTexture(path);
			this.panelComponent.SetMainTex(this.textureMain);
			UnityEngine.Resources.UnloadUnusedAssets();
		}

		// Token: 0x0600984D RID: 38989 RVA: 0x003ED214 File Offset: 0x003EB614
		public void ActiveFK(bool _active)
		{
			if (this.itemFKCtrl == null)
			{
				return;
			}
			this.itemFKCtrl.enabled = _active;
			this.itemInfo.enableFK = _active;
			bool enabled = !_active && this.itemInfo.enableDynamicBone;
			foreach (DynamicBone dynamicBone in this.dynamicBones)
			{
				dynamicBone.enabled = enabled;
			}
			foreach (OCIChar.BoneInfo boneInfo in this.listBones)
			{
				boneInfo.active = _active;
			}
		}

		// Token: 0x0600984E RID: 38990 RVA: 0x003ED2DC File Offset: 0x003EB6DC
		public void UpdateFKColor()
		{
			if (this.listBones.IsNullOrEmpty<OCIChar.BoneInfo>())
			{
				return;
			}
			foreach (OCIChar.BoneInfo boneInfo in this.listBones)
			{
				boneInfo.color = Studio.optionSystem.colorFKItem;
			}
		}

		// Token: 0x0600984F RID: 38991 RVA: 0x003ED354 File Offset: 0x003EB754
		public void ActiveDynamicBone(bool _active)
		{
			this.itemInfo.enableDynamicBone = _active;
			if (this.dynamicBones.IsNullOrEmpty<DynamicBone>())
			{
				return;
			}
			if (this.isFK & this.itemInfo.enableFK)
			{
				return;
			}
			foreach (DynamicBone dynamicBone in this.dynamicBones)
			{
				dynamicBone.enabled = _active;
			}
		}

		// Token: 0x06009850 RID: 38992 RVA: 0x003ED3BC File Offset: 0x003EB7BC
		public void SetOptionVisible(bool _visible)
		{
			int count = this.itemInfo.option.Count;
			for (int i = 0; i < count; i++)
			{
				this.itemInfo.option[i] = _visible;
			}
			if (this.itemComponent != null)
			{
				this.itemComponent.SetOptionVisible(_visible);
			}
		}

		// Token: 0x06009851 RID: 38993 RVA: 0x003ED418 File Offset: 0x003EB818
		public void SetOptionVisible(int _idx, bool _visible)
		{
			if (MathfEx.RangeEqualOn<int>(0, _idx, this.itemInfo.option.Count - 1))
			{
				this.itemInfo.option[_idx] = _visible;
			}
			if (this.itemComponent != null)
			{
				this.itemComponent.SetOptionVisible(_idx, _visible);
			}
		}

		// Token: 0x06009852 RID: 38994 RVA: 0x003ED470 File Offset: 0x003EB870
		public void UpdateOption()
		{
			int count = this.itemInfo.option.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.itemComponent != null)
				{
					this.itemComponent.SetOptionVisible(i, this.itemInfo.option[i]);
				}
			}
		}

		// Token: 0x06009853 RID: 38995 RVA: 0x003ED4CC File Offset: 0x003EB8CC
		public void SetAnimePattern(int _idx)
		{
			if (!this.isAnime)
			{
				return;
			}
			this.itemInfo.animePattern = _idx;
			ItemComponent.AnimeInfo animeInfo = (this.itemComponent != null) ? this.itemComponent.animeInfos.SafeGet(_idx) : null;
			if (animeInfo != null)
			{
				this.animator.Play(animeInfo.state);
			}
		}

		// Token: 0x06009854 RID: 38996 RVA: 0x003ED528 File Offset: 0x003EB928
		public void RestartAnime()
		{
			if (!this.isAnime)
			{
				return;
			}
			if (this.animator.layerCount == 0)
			{
				return;
			}
			AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			this.animator.Play(currentAnimatorStateInfo.shortNameHash, 0, 0f);
		}

		// Token: 0x040079A2 RID: 31138
		public GameObject objectItem;

		// Token: 0x040079A3 RID: 31139
		public Transform childRoot;

		// Token: 0x040079A4 RID: 31140
		public Animator animator;

		// Token: 0x040079A5 RID: 31141
		public ItemComponent itemComponent;

		// Token: 0x040079A6 RID: 31142
		public ParticleComponent particleComponent;

		// Token: 0x040079A7 RID: 31143
		private Texture2D[] texturePattern = new Texture2D[3];

		// Token: 0x040079A8 RID: 31144
		public IconComponent iconComponent;

		// Token: 0x040079A9 RID: 31145
		public PanelComponent panelComponent;

		// Token: 0x040079AA RID: 31146
		private Texture2D textureMain;

		// Token: 0x040079AB RID: 31147
		public SEComponent seComponent;

		// Token: 0x040079AC RID: 31148
		public ItemFKCtrl itemFKCtrl;

		// Token: 0x040079AD RID: 31149
		public List<OCIChar.BoneInfo> listBones;

		// Token: 0x040079AE RID: 31150
		public DynamicBone[] dynamicBones;

		// Token: 0x040079AF RID: 31151
		private bool m_Visible = true;

		// Token: 0x040079B0 RID: 31152
		public Renderer[] arrayRender;

		// Token: 0x040079B1 RID: 31153
		public ParticleSystem[] arrayParticle;
	}
}
