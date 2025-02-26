using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007C9 RID: 1993
	[DisallowMultipleComponent]
	public class CmpBoneBody : CmpBase
	{
		// Token: 0x06003163 RID: 12643 RVA: 0x00124FF1 File Offset: 0x001233F1
		public CmpBoneBody() : base(false)
		{
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x00125010 File Offset: 0x00123410
		public override void SetReferenceObject()
		{
			FindAssist findAssist = new FindAssist();
			findAssist.Initialize(base.transform);
			this.targetAccessory.acs_Neck = findAssist.GetTransformFromName("N_Neck");
			this.targetAccessory.acs_Chest_f = findAssist.GetTransformFromName("N_Chest_f");
			this.targetAccessory.acs_Chest = findAssist.GetTransformFromName("N_Chest");
			this.targetAccessory.acs_Tikubi_L = findAssist.GetTransformFromName("N_Tikubi_L");
			this.targetAccessory.acs_Tikubi_R = findAssist.GetTransformFromName("N_Tikubi_R");
			this.targetAccessory.acs_Back = findAssist.GetTransformFromName("N_Back");
			this.targetAccessory.acs_Back_L = findAssist.GetTransformFromName("N_Back_L");
			this.targetAccessory.acs_Back_R = findAssist.GetTransformFromName("N_Back_R");
			this.targetAccessory.acs_Waist = findAssist.GetTransformFromName("N_Waist");
			this.targetAccessory.acs_Waist_f = findAssist.GetTransformFromName("N_Waist_f");
			this.targetAccessory.acs_Waist_b = findAssist.GetTransformFromName("N_Waist_b");
			this.targetAccessory.acs_Waist_L = findAssist.GetTransformFromName("N_Waist_L");
			this.targetAccessory.acs_Waist_R = findAssist.GetTransformFromName("N_Waist_R");
			this.targetAccessory.acs_Leg_L = findAssist.GetTransformFromName("N_Leg_L");
			this.targetAccessory.acs_Leg_R = findAssist.GetTransformFromName("N_Leg_R");
			this.targetAccessory.acs_Knee_L = findAssist.GetTransformFromName("N_Knee_L");
			this.targetAccessory.acs_Knee_R = findAssist.GetTransformFromName("N_Knee_R");
			this.targetAccessory.acs_Ankle_L = findAssist.GetTransformFromName("N_Ankle_L");
			this.targetAccessory.acs_Ankle_R = findAssist.GetTransformFromName("N_Ankle_R");
			this.targetAccessory.acs_Foot_L = findAssist.GetTransformFromName("N_Foot_L");
			this.targetAccessory.acs_Foot_R = findAssist.GetTransformFromName("N_Foot_R");
			this.targetAccessory.acs_Shoulder_L = findAssist.GetTransformFromName("N_Shoulder_L");
			this.targetAccessory.acs_Shoulder_R = findAssist.GetTransformFromName("N_Shoulder_R");
			this.targetAccessory.acs_Elbo_L = findAssist.GetTransformFromName("N_Elbo_L");
			this.targetAccessory.acs_Elbo_R = findAssist.GetTransformFromName("N_Elbo_R");
			this.targetAccessory.acs_Arm_L = findAssist.GetTransformFromName("N_Arm_L");
			this.targetAccessory.acs_Arm_R = findAssist.GetTransformFromName("N_Arm_R");
			this.targetAccessory.acs_Wrist_L = findAssist.GetTransformFromName("N_Wrist_L");
			this.targetAccessory.acs_Wrist_R = findAssist.GetTransformFromName("N_Wrist_R");
			this.targetAccessory.acs_Hand_L = findAssist.GetTransformFromName("N_Hand_L");
			this.targetAccessory.acs_Hand_R = findAssist.GetTransformFromName("N_Hand_R");
			this.targetAccessory.acs_Index_L = findAssist.GetTransformFromName("N_Index_L");
			this.targetAccessory.acs_Index_R = findAssist.GetTransformFromName("N_Index_R");
			this.targetAccessory.acs_Middle_L = findAssist.GetTransformFromName("N_Middle_L");
			this.targetAccessory.acs_Middle_R = findAssist.GetTransformFromName("N_Middle_R");
			this.targetAccessory.acs_Ring_L = findAssist.GetTransformFromName("N_Ring_L");
			this.targetAccessory.acs_Ring_R = findAssist.GetTransformFromName("N_Ring_R");
			this.targetAccessory.acs_Dan = findAssist.GetTransformFromName("N_Dan");
			this.targetAccessory.acs_Kokan = findAssist.GetTransformFromName("N_Kokan");
			this.targetAccessory.acs_Ana = findAssist.GetTransformFromName("N_Ana");
			this.targetEtc.trfRoot = findAssist.GetTransformFromName("cf_J_Hips");
			this.targetEtc.trfHeadParent = findAssist.GetTransformFromName("cf_J_Head_s");
			this.targetEtc.trfNeckLookTarget = findAssist.GetTransformFromName("cf_J_Spine03");
			this.targetEtc.trfAnaCorrect = findAssist.GetTransformFromName("cf_J_Ana");
			this.targetEtc.trf_k_shoulderL_00 = findAssist.GetTransformFromName("k_f_shoulderL_00");
			this.targetEtc.trf_k_shoulderR_00 = findAssist.GetTransformFromName("k_f_shoulderR_00");
			this.targetEtc.trf_k_handL_00 = findAssist.GetTransformFromName("k_f_handL_00");
			this.targetEtc.trf_k_handR_00 = findAssist.GetTransformFromName("k_f_handR_00");
			this.dbcBust = new DynamicBoneCollider[4];
			GameObject objectFromName = findAssist.GetObjectFromName("cf_hit_Mune02_s_L");
			this.dbcBust[0] = objectFromName.GetComponent<DynamicBoneCollider>();
			objectFromName = findAssist.GetObjectFromName("cf_hit_Mune021_s_L");
			this.dbcBust[1] = objectFromName.GetComponent<DynamicBoneCollider>();
			objectFromName = findAssist.GetObjectFromName("cf_hit_Mune02_s_R");
			this.dbcBust[2] = objectFromName.GetComponent<DynamicBoneCollider>();
			objectFromName = findAssist.GetObjectFromName("cf_hit_Mune021_s_R");
			this.dbcBust[3] = objectFromName.GetComponent<DynamicBoneCollider>();
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x001254C4 File Offset: 0x001238C4
		public void InactiveBustDynamicBoneCollider()
		{
			foreach (DynamicBoneCollider dynamicBoneCollider in this.dbcBust)
			{
				if (null != dynamicBoneCollider)
				{
					dynamicBoneCollider.enabled = false;
				}
			}
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x00125504 File Offset: 0x00123904
		public void InitDynamicBonesBustAndHip()
		{
			DynamicBone_Ver02[] componentsInChildren = base.GetComponentsInChildren<DynamicBone_Ver02>(true);
			this.dynamicBonesBustAndHip = new DynamicBone_Ver02[Enum.GetNames(typeof(ChaControlDefine.DynamicBoneKind)).Length];
			foreach (DynamicBone_Ver02 dynamicBone_Ver in componentsInChildren)
			{
				if (dynamicBone_Ver.Comment == "Mune_L")
				{
					this.dynamicBonesBustAndHip[0] = dynamicBone_Ver;
				}
				else if (dynamicBone_Ver.Comment == "Mune_R")
				{
					this.dynamicBonesBustAndHip[1] = dynamicBone_Ver;
				}
				else if (dynamicBone_Ver.Comment == "Siri_L")
				{
					this.dynamicBonesBustAndHip[2] = dynamicBone_Ver;
				}
				else if (dynamicBone_Ver.Comment == "Siri_R")
				{
					this.dynamicBonesBustAndHip[3] = dynamicBone_Ver;
				}
			}
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x001255D6 File Offset: 0x001239D6
		public DynamicBone_Ver02 GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind area)
		{
			return this.GetDynamicBoneBustAndHip((int)area);
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x001255DF File Offset: 0x001239DF
		public DynamicBone_Ver02 GetDynamicBoneBustAndHip(int area)
		{
			if (area >= this.dynamicBonesBustAndHip.Length)
			{
				return null;
			}
			return this.dynamicBonesBustAndHip[area];
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x001255FC File Offset: 0x001239FC
		public void ResetDynamicBonesBustAndHip(bool includeInactive = false)
		{
			if (this.dynamicBonesBustAndHip != null)
			{
				foreach (DynamicBone_Ver02 dynamicBone_Ver in this.dynamicBonesBustAndHip)
				{
					if (dynamicBone_Ver.enabled || includeInactive)
					{
						dynamicBone_Ver.ResetParticlesPosition();
					}
				}
			}
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x0012564C File Offset: 0x00123A4C
		public void EnableDynamicBonesBustAndHip(bool enable, int area)
		{
			if (this.dynamicBonesBustAndHip == null || area >= this.dynamicBonesBustAndHip.Length)
			{
				return;
			}
			if (null == this.dynamicBonesBustAndHip[area])
			{
				return;
			}
			if (this.dynamicBonesBustAndHip[area].enabled != enable)
			{
				this.dynamicBonesBustAndHip[area].enabled = enable;
				if (enable)
				{
					this.dynamicBonesBustAndHip[area].ResetParticlesPosition();
				}
			}
		}

		// Token: 0x04002F3A RID: 12090
		[Header("アクセサリの親")]
		public CmpBoneBody.TargetAccessory targetAccessory = new CmpBoneBody.TargetAccessory();

		// Token: 0x04002F3B RID: 12091
		[Header("その他ターゲット")]
		public CmpBoneBody.TargetEtc targetEtc = new CmpBoneBody.TargetEtc();

		// Token: 0x04002F3C RID: 12092
		private DynamicBone_Ver02[] dynamicBonesBustAndHip;

		// Token: 0x04002F3D RID: 12093
		[Header("男無効用胸の判定")]
		public DynamicBoneCollider[] dbcBust;

		// Token: 0x020007CA RID: 1994
		[Serializable]
		public class TargetAccessory
		{
			// Token: 0x04002F3E RID: 12094
			public Transform acs_Neck;

			// Token: 0x04002F3F RID: 12095
			public Transform acs_Chest_f;

			// Token: 0x04002F40 RID: 12096
			public Transform acs_Chest;

			// Token: 0x04002F41 RID: 12097
			public Transform acs_Tikubi_L;

			// Token: 0x04002F42 RID: 12098
			public Transform acs_Tikubi_R;

			// Token: 0x04002F43 RID: 12099
			public Transform acs_Back;

			// Token: 0x04002F44 RID: 12100
			public Transform acs_Back_L;

			// Token: 0x04002F45 RID: 12101
			public Transform acs_Back_R;

			// Token: 0x04002F46 RID: 12102
			public Transform acs_Waist;

			// Token: 0x04002F47 RID: 12103
			public Transform acs_Waist_f;

			// Token: 0x04002F48 RID: 12104
			public Transform acs_Waist_b;

			// Token: 0x04002F49 RID: 12105
			public Transform acs_Waist_L;

			// Token: 0x04002F4A RID: 12106
			public Transform acs_Waist_R;

			// Token: 0x04002F4B RID: 12107
			public Transform acs_Leg_L;

			// Token: 0x04002F4C RID: 12108
			public Transform acs_Leg_R;

			// Token: 0x04002F4D RID: 12109
			public Transform acs_Knee_L;

			// Token: 0x04002F4E RID: 12110
			public Transform acs_Knee_R;

			// Token: 0x04002F4F RID: 12111
			public Transform acs_Ankle_L;

			// Token: 0x04002F50 RID: 12112
			public Transform acs_Ankle_R;

			// Token: 0x04002F51 RID: 12113
			public Transform acs_Foot_L;

			// Token: 0x04002F52 RID: 12114
			public Transform acs_Foot_R;

			// Token: 0x04002F53 RID: 12115
			public Transform acs_Shoulder_L;

			// Token: 0x04002F54 RID: 12116
			public Transform acs_Shoulder_R;

			// Token: 0x04002F55 RID: 12117
			public Transform acs_Elbo_L;

			// Token: 0x04002F56 RID: 12118
			public Transform acs_Elbo_R;

			// Token: 0x04002F57 RID: 12119
			public Transform acs_Arm_L;

			// Token: 0x04002F58 RID: 12120
			public Transform acs_Arm_R;

			// Token: 0x04002F59 RID: 12121
			public Transform acs_Wrist_L;

			// Token: 0x04002F5A RID: 12122
			public Transform acs_Wrist_R;

			// Token: 0x04002F5B RID: 12123
			public Transform acs_Hand_L;

			// Token: 0x04002F5C RID: 12124
			public Transform acs_Hand_R;

			// Token: 0x04002F5D RID: 12125
			public Transform acs_Index_L;

			// Token: 0x04002F5E RID: 12126
			public Transform acs_Index_R;

			// Token: 0x04002F5F RID: 12127
			public Transform acs_Middle_L;

			// Token: 0x04002F60 RID: 12128
			public Transform acs_Middle_R;

			// Token: 0x04002F61 RID: 12129
			public Transform acs_Ring_L;

			// Token: 0x04002F62 RID: 12130
			public Transform acs_Ring_R;

			// Token: 0x04002F63 RID: 12131
			public Transform acs_Dan;

			// Token: 0x04002F64 RID: 12132
			public Transform acs_Kokan;

			// Token: 0x04002F65 RID: 12133
			public Transform acs_Ana;
		}

		// Token: 0x020007CB RID: 1995
		[Serializable]
		public class TargetEtc
		{
			// Token: 0x04002F66 RID: 12134
			public Transform trfRoot;

			// Token: 0x04002F67 RID: 12135
			public Transform trfHeadParent;

			// Token: 0x04002F68 RID: 12136
			public Transform trfNeckLookTarget;

			// Token: 0x04002F69 RID: 12137
			public Transform trfAnaCorrect;

			// Token: 0x04002F6A RID: 12138
			public Transform trf_k_shoulderL_00;

			// Token: 0x04002F6B RID: 12139
			public Transform trf_k_shoulderR_00;

			// Token: 0x04002F6C RID: 12140
			public Transform trf_k_handL_00;

			// Token: 0x04002F6D RID: 12141
			public Transform trf_k_handR_00;
		}
	}
}
