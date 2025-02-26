using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007D3 RID: 2003
	[DisallowMultipleComponent]
	public class CmpHair : CmpBase
	{
		// Token: 0x0600317C RID: 12668 RVA: 0x00125F6C File Offset: 0x0012436C
		public CmpHair() : base(false)
		{
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x00125F84 File Offset: 0x00124384
		protected override void Reacquire()
		{
			base.Reacquire();
			if (this.boneInfo == null || this.boneInfo.Length == 0)
			{
				return;
			}
			FindAssist findAssist = new FindAssist();
			findAssist.Initialize(base.transform);
			KeyValuePair<string, GameObject> keyValuePair = findAssist.dictObjName.FirstOrDefault((KeyValuePair<string, GameObject> x) => x.Key.Contains("_top"));
			if (keyValuePair.Equals(default(KeyValuePair<string, GameObject>)))
			{
				return;
			}
			DynamicBone[] components = base.GetComponents<DynamicBone>();
			for (int i = 0; i < this.boneInfo.Length; i++)
			{
				Transform child = keyValuePair.Value.transform.GetChild(i);
				findAssist.Initialize(child);
				List<DynamicBone> list = new List<DynamicBone>();
				DynamicBone[] array = components;
				for (int j = 0; j < array.Length; j++)
				{
					DynamicBone n = array[j];
					if (!findAssist.dictObjName.FirstOrDefault((KeyValuePair<string, GameObject> x) => x.Key == n.m_Root.name).Equals(default(KeyValuePair<string, GameObject>)))
					{
						list.Add(n);
					}
				}
				this.boneInfo[i].dynamicBone = list.ToArray();
			}
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x001260E8 File Offset: 0x001244E8
		public void SetColor()
		{
			if (this.rendAccessory.Length != 0)
			{
				Material sharedMaterial = this.rendAccessory[0].sharedMaterial;
				if (null != sharedMaterial)
				{
					this.acsDefColor = new Color[3];
					if (sharedMaterial.HasProperty("_Color"))
					{
						this.acsDefColor[0] = sharedMaterial.GetColor("_Color");
					}
					if (sharedMaterial.HasProperty("_Color2"))
					{
						this.acsDefColor[1] = sharedMaterial.GetColor("_Color2");
					}
					if (sharedMaterial.HasProperty("_Color3"))
					{
						this.acsDefColor[2] = sharedMaterial.GetColor("_Color3");
					}
				}
			}
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x001261AC File Offset: 0x001245AC
		public void SetDefaultPosition()
		{
			if (this.boneInfo != null && this.boneInfo.Length != 0)
			{
				foreach (CmpHair.BoneInfo boneInfo in this.boneInfo)
				{
					if (null != boneInfo.trfCorrect)
					{
						boneInfo.trfCorrect.transform.localPosition = boneInfo.basePos;
					}
				}
			}
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x00126218 File Offset: 0x00124618
		public void SetDefaultRotation()
		{
			if (this.boneInfo != null && this.boneInfo.Length != 0)
			{
				foreach (CmpHair.BoneInfo boneInfo in this.boneInfo)
				{
					if (null != boneInfo.trfCorrect)
					{
						boneInfo.trfCorrect.transform.localEulerAngles = boneInfo.baseRot;
					}
				}
			}
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x00126284 File Offset: 0x00124684
		public override void SetReferenceObject()
		{
			FindAssist findAssist = new FindAssist();
			findAssist.Initialize(base.transform);
			this.rendHair = (from x in base.GetComponentsInChildren<Renderer>(true)
			where !x.name.Contains("_acs")
			select x).ToArray<Renderer>();
			DynamicBone[] components = base.GetComponents<DynamicBone>();
			KeyValuePair<string, GameObject> keyValuePair = findAssist.dictObjName.FirstOrDefault((KeyValuePair<string, GameObject> x) => x.Key.Contains("_top"));
			if (keyValuePair.Equals(default(KeyValuePair<string, GameObject>)))
			{
				return;
			}
			this.boneInfo = new CmpHair.BoneInfo[keyValuePair.Value.transform.childCount];
			for (int i = 0; i < this.boneInfo.Length; i++)
			{
				Transform child = keyValuePair.Value.transform.GetChild(i);
				findAssist.Initialize(child);
				CmpHair.BoneInfo boneInfo = new CmpHair.BoneInfo();
				KeyValuePair<string, GameObject> keyValuePair2 = findAssist.dictObjName.FirstOrDefault((KeyValuePair<string, GameObject> x) => x.Key.Contains("_s"));
				if (!keyValuePair2.Equals(default(KeyValuePair<string, GameObject>)))
				{
					Transform transform = keyValuePair2.Value.transform;
					boneInfo.trfCorrect = transform;
					boneInfo.basePos = boneInfo.trfCorrect.transform.localPosition;
					boneInfo.posMin.x = boneInfo.trfCorrect.transform.localPosition.x + 0.1f;
					boneInfo.posMin.y = boneInfo.trfCorrect.transform.localPosition.y;
					boneInfo.posMin.z = boneInfo.trfCorrect.transform.localPosition.z + 0.1f;
					boneInfo.posMax.x = boneInfo.trfCorrect.transform.localPosition.x - 0.1f;
					boneInfo.posMax.y = boneInfo.trfCorrect.transform.localPosition.y - 0.2f;
					boneInfo.posMax.z = boneInfo.trfCorrect.transform.localPosition.z - 0.1f;
					boneInfo.baseRot = boneInfo.trfCorrect.transform.localEulerAngles;
					boneInfo.rotMin.x = boneInfo.trfCorrect.transform.localEulerAngles.x - 15f;
					boneInfo.rotMin.y = boneInfo.trfCorrect.transform.localEulerAngles.y - 15f;
					boneInfo.rotMin.z = boneInfo.trfCorrect.transform.localEulerAngles.z - 15f;
					boneInfo.rotMax.x = boneInfo.trfCorrect.transform.localEulerAngles.x + 15f;
					boneInfo.rotMax.y = boneInfo.trfCorrect.transform.localEulerAngles.y + 15f;
					boneInfo.rotMax.z = boneInfo.trfCorrect.transform.localEulerAngles.z + 15f;
					boneInfo.moveRate.x = Mathf.InverseLerp(boneInfo.posMin.x, boneInfo.posMax.x, boneInfo.basePos.x);
					boneInfo.moveRate.y = Mathf.InverseLerp(boneInfo.posMin.y, boneInfo.posMax.y, boneInfo.basePos.y);
					boneInfo.moveRate.z = Mathf.InverseLerp(boneInfo.posMin.z, boneInfo.posMax.z, boneInfo.basePos.z);
					boneInfo.rotRate.x = Mathf.InverseLerp(boneInfo.rotMin.x, boneInfo.rotMax.x, boneInfo.baseRot.x);
					boneInfo.rotRate.y = Mathf.InverseLerp(boneInfo.rotMin.y, boneInfo.rotMax.y, boneInfo.baseRot.y);
					boneInfo.rotRate.z = Mathf.InverseLerp(boneInfo.rotMin.z, boneInfo.rotMax.z, boneInfo.baseRot.z);
				}
				List<DynamicBone> list = new List<DynamicBone>();
				DynamicBone[] array = components;
				for (int j = 0; j < array.Length; j++)
				{
					DynamicBone n = array[j];
					if (!findAssist.dictObjName.FirstOrDefault((KeyValuePair<string, GameObject> x) => x.Key == n.m_Root.name).Equals(default(KeyValuePair<string, GameObject>)))
					{
						list.Add(n);
					}
				}
				boneInfo.dynamicBone = list.ToArray();
				this.boneInfo[i] = boneInfo;
			}
			findAssist = new FindAssist();
			findAssist.Initialize(base.transform);
			this.rendAccessory = (from s in findAssist.dictObjName
			where s.Key.Contains("_acs")
			select s into x
			select x.Value.GetComponent<Renderer>() into r
			where null != r
			select r).ToArray<Renderer>();
			this.SetColor();
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x0012688C File Offset: 0x00124C8C
		public void ResetDynamicBonesHair(bool includeInactive = false)
		{
			if (this.boneInfo == null || this.boneInfo.Length == 0)
			{
				return;
			}
			foreach (CmpHair.BoneInfo boneInfo in this.boneInfo)
			{
				if (boneInfo.dynamicBone != null)
				{
					for (int j = 0; j < boneInfo.dynamicBone.Length; j++)
					{
						if (null != boneInfo.dynamicBone[j] && (boneInfo.dynamicBone[j].enabled || includeInactive))
						{
							boneInfo.dynamicBone[j].ResetParticlesPosition();
						}
					}
				}
			}
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x00126934 File Offset: 0x00124D34
		public void EnableDynamicBonesHair(bool enable, ChaFileHair.PartsInfo parts = null)
		{
			if (this.boneInfo == null || this.boneInfo.Length == 0)
			{
				return;
			}
			if (enable)
			{
				if (parts.dictBundle == null || parts.dictBundle.Count == 0)
				{
					return;
				}
				if (this.boneInfo.Length != parts.dictBundle.Count)
				{
					return;
				}
				for (int i = 0; i < this.boneInfo.Length; i++)
				{
					if (this.boneInfo[i].dynamicBone != null)
					{
						ChaFileHair.PartsInfo.BundleInfo bundleInfo;
						if (parts.dictBundle.TryGetValue(i, out bundleInfo))
						{
							for (int j = 0; j < this.boneInfo[i].dynamicBone.Length; j++)
							{
								DynamicBone dynamicBone = this.boneInfo[i].dynamicBone[j];
								if (!(null == dynamicBone))
								{
									if (dynamicBone.enabled != !bundleInfo.noShake)
									{
										dynamicBone.enabled = !bundleInfo.noShake;
										if (dynamicBone.enabled)
										{
											dynamicBone.ResetParticlesPosition();
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				foreach (CmpHair.BoneInfo boneInfo in this.boneInfo)
				{
					if (boneInfo.dynamicBone != null)
					{
						for (int l = 0; l < boneInfo.dynamicBone.Length; l++)
						{
							if (null != boneInfo.dynamicBone[l] && boneInfo.dynamicBone[l].enabled)
							{
								boneInfo.dynamicBone[l].enabled = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x00126AE0 File Offset: 0x00124EE0
		private void Update()
		{
			if (this.boneInfo != null && this.boneInfo.Length != 0)
			{
				foreach (CmpHair.BoneInfo boneInfo in this.boneInfo)
				{
					if (!(null == boneInfo.trfCorrect))
					{
						boneInfo.trfCorrect.transform.localPosition = new Vector3(Mathf.Lerp(boneInfo.posMin.x, boneInfo.posMax.x, boneInfo.moveRate.x), Mathf.Lerp(boneInfo.posMin.y, boneInfo.posMax.y, boneInfo.moveRate.y), Mathf.Lerp(boneInfo.posMin.z, boneInfo.posMax.z, boneInfo.moveRate.z));
						boneInfo.trfCorrect.transform.localEulerAngles = new Vector3(Mathf.Lerp(boneInfo.rotMin.x, boneInfo.rotMax.x, boneInfo.rotRate.x), Mathf.Lerp(boneInfo.rotMin.y, boneInfo.rotMax.y, boneInfo.rotRate.y), Mathf.Lerp(boneInfo.rotMin.z, boneInfo.rotMax.z, boneInfo.rotRate.z));
					}
				}
			}
		}

		// Token: 0x04002FB6 RID: 12214
		[Header("< 髪の毛 >-------------------")]
		public Renderer[] rendHair;

		// Token: 0x04002FB7 RID: 12215
		[Tooltip("根本の色を使用")]
		public bool useTopColor = true;

		// Token: 0x04002FB8 RID: 12216
		[Tooltip("毛先の色を使用")]
		public bool useUnderColor = true;

		// Token: 0x04002FB9 RID: 12217
		[Tooltip("毛先（肌ボタン）")]
		public bool useSameSkinColorButton;

		// Token: 0x04002FBA RID: 12218
		[Tooltip("メッシュが可能")]
		public bool useMesh;

		// Token: 0x04002FBB RID: 12219
		public CmpHair.BoneInfo[] boneInfo;

		// Token: 0x04002FBC RID: 12220
		[Space]
		[Button("SetDefaultPosition", "初期位置", new object[]
		{

		})]
		public int setdefaultposition;

		// Token: 0x04002FBD RID: 12221
		[Button("SetDefaultRotation", "初期回転", new object[]
		{

		})]
		public int setdefaultrotation;

		// Token: 0x04002FBE RID: 12222
		[Header("< 飾り >---------------------")]
		public bool useAcsColor01;

		// Token: 0x04002FBF RID: 12223
		public bool useAcsColor02;

		// Token: 0x04002FC0 RID: 12224
		public bool useAcsColor03;

		// Token: 0x04002FC1 RID: 12225
		public Renderer[] rendAccessory;

		// Token: 0x04002FC2 RID: 12226
		public Color[] acsDefColor;

		// Token: 0x04002FC3 RID: 12227
		[Button("SetColor", "アクセサリの初期色を設定", new object[]
		{

		})]
		public int setcolor;

		// Token: 0x020007D4 RID: 2004
		[Serializable]
		public class BoneInfo
		{
			// Token: 0x04002FCB RID: 12235
			public Transform trfCorrect;

			// Token: 0x04002FCC RID: 12236
			public DynamicBone[] dynamicBone;

			// Token: 0x04002FCD RID: 12237
			[HideInInspector]
			public Vector3 basePos = Vector3.zero;

			// Token: 0x04002FCE RID: 12238
			[HideInInspector]
			public Vector3 baseRot = Vector3.zero;

			// Token: 0x04002FCF RID: 12239
			[Header("[位置 制限]---------------------")]
			public Vector3 posMin = new Vector3(0f, 0f, 0f);

			// Token: 0x04002FD0 RID: 12240
			public Vector3 posMax = new Vector3(0f, 0f, 0f);

			// Token: 0x04002FD1 RID: 12241
			[Header("[回転 制限]---------------------")]
			public Vector3 rotMin = new Vector3(0f, 0f, 0f);

			// Token: 0x04002FD2 RID: 12242
			public Vector3 rotMax = new Vector3(0f, 0f, 0f);

			// Token: 0x04002FD3 RID: 12243
			[HideInInspector]
			public Vector3 moveRate = Vector3.zero;

			// Token: 0x04002FD4 RID: 12244
			[HideInInspector]
			public Vector3 rotRate = Vector3.zero;
		}
	}
}
