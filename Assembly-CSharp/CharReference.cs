using System;

// Token: 0x020006A5 RID: 1701
public class CharReference
{
	// Token: 0x04002950 RID: 10576
	public const ulong FbxTypeBone = 1UL;

	// Token: 0x04002951 RID: 10577
	public const ulong FbxTypeBody = 2UL;

	// Token: 0x04002952 RID: 10578
	public const ulong FbxTypeHead = 3UL;

	// Token: 0x04002953 RID: 10579
	public const ulong FbxTypeBase = 8UL;

	// Token: 0x020006A6 RID: 1702
	public enum RefObjKey_New
	{
		// Token: 0x04002955 RID: 10581
		HeadParent,
		// Token: 0x04002956 RID: 10582
		HairParent,
		// Token: 0x04002957 RID: 10583
		AP_Hair_Twin_L,
		// Token: 0x04002958 RID: 10584
		AP_Hair_Twin_R,
		// Token: 0x04002959 RID: 10585
		AP_Hair_pin_L,
		// Token: 0x0400295A RID: 10586
		AP_Hair_pin_R,
		// Token: 0x0400295B RID: 10587
		AP_Head_Top,
		// Token: 0x0400295C RID: 10588
		AP_Head,
		// Token: 0x0400295D RID: 10589
		AP_Hitai,
		// Token: 0x0400295E RID: 10590
		AP_Face,
		// Token: 0x0400295F RID: 10591
		AP_Megane,
		// Token: 0x04002960 RID: 10592
		AP_Earring_L,
		// Token: 0x04002961 RID: 10593
		AP_Earring_R,
		// Token: 0x04002962 RID: 10594
		AP_Nose,
		// Token: 0x04002963 RID: 10595
		AP_Mouth,
		// Token: 0x04002964 RID: 10596
		AP_Neck,
		// Token: 0x04002965 RID: 10597
		AP_Chest_F,
		// Token: 0x04002966 RID: 10598
		AP_Chest,
		// Token: 0x04002967 RID: 10599
		AP_Tikubi_L,
		// Token: 0x04002968 RID: 10600
		AP_Tikubi_R,
		// Token: 0x04002969 RID: 10601
		AP_Back,
		// Token: 0x0400296A RID: 10602
		AP_Back_L,
		// Token: 0x0400296B RID: 10603
		AP_Back_R,
		// Token: 0x0400296C RID: 10604
		AP_Waist,
		// Token: 0x0400296D RID: 10605
		AP_Waist_F,
		// Token: 0x0400296E RID: 10606
		AP_Waist_B,
		// Token: 0x0400296F RID: 10607
		AP_Waist_L,
		// Token: 0x04002970 RID: 10608
		AP_Waist_R,
		// Token: 0x04002971 RID: 10609
		AP_Leg_L,
		// Token: 0x04002972 RID: 10610
		AP_Leg_R,
		// Token: 0x04002973 RID: 10611
		AP_Knee_L,
		// Token: 0x04002974 RID: 10612
		AP_Knee_R,
		// Token: 0x04002975 RID: 10613
		AP_Ankle_L,
		// Token: 0x04002976 RID: 10614
		AP_Ankle_R,
		// Token: 0x04002977 RID: 10615
		AP_Foot_L,
		// Token: 0x04002978 RID: 10616
		AP_Foot_R,
		// Token: 0x04002979 RID: 10617
		AP_Shoulder_L,
		// Token: 0x0400297A RID: 10618
		AP_Shoulder_R,
		// Token: 0x0400297B RID: 10619
		AP_Elbo_L,
		// Token: 0x0400297C RID: 10620
		AP_Elbo_R,
		// Token: 0x0400297D RID: 10621
		AP_Arm_L,
		// Token: 0x0400297E RID: 10622
		AP_Arm_R,
		// Token: 0x0400297F RID: 10623
		AP_Wrist_L,
		// Token: 0x04002980 RID: 10624
		AP_Wrist_R,
		// Token: 0x04002981 RID: 10625
		AP_Hand_L,
		// Token: 0x04002982 RID: 10626
		AP_Hand_R,
		// Token: 0x04002983 RID: 10627
		AP_Index_L,
		// Token: 0x04002984 RID: 10628
		AP_Index_R,
		// Token: 0x04002985 RID: 10629
		AP_Middle_L,
		// Token: 0x04002986 RID: 10630
		AP_Middle_R,
		// Token: 0x04002987 RID: 10631
		AP_Ring_L,
		// Token: 0x04002988 RID: 10632
		AP_Ring_R,
		// Token: 0x04002989 RID: 10633
		AP_Dan,
		// Token: 0x0400298A RID: 10634
		AP_Kokan,
		// Token: 0x0400298B RID: 10635
		AP_Ana
	}

	// Token: 0x020006A7 RID: 1703
	public enum RefObjKey
	{
		// Token: 0x0400298D RID: 10637
		HeadParent,
		// Token: 0x0400298E RID: 10638
		HairParent,
		// Token: 0x0400298F RID: 10639
		AP_Head,
		// Token: 0x04002990 RID: 10640
		AP_Megane,
		// Token: 0x04002991 RID: 10641
		AP_Earring_L,
		// Token: 0x04002992 RID: 10642
		AP_Earring_R,
		// Token: 0x04002993 RID: 10643
		AP_Mouth,
		// Token: 0x04002994 RID: 10644
		AP_Nose,
		// Token: 0x04002995 RID: 10645
		AP_Neck,
		// Token: 0x04002996 RID: 10646
		AP_Chest,
		// Token: 0x04002997 RID: 10647
		AP_Wrist_L,
		// Token: 0x04002998 RID: 10648
		AP_Wrist_R,
		// Token: 0x04002999 RID: 10649
		AP_Arm_L,
		// Token: 0x0400299A RID: 10650
		AP_Arm_R,
		// Token: 0x0400299B RID: 10651
		AP_Index_L,
		// Token: 0x0400299C RID: 10652
		AP_Index_R,
		// Token: 0x0400299D RID: 10653
		AP_Middle_L,
		// Token: 0x0400299E RID: 10654
		AP_Middle_R,
		// Token: 0x0400299F RID: 10655
		AP_Ring_L,
		// Token: 0x040029A0 RID: 10656
		AP_Ring_R,
		// Token: 0x040029A1 RID: 10657
		AP_Leg_L,
		// Token: 0x040029A2 RID: 10658
		AP_Leg_R,
		// Token: 0x040029A3 RID: 10659
		AP_Ankle_L,
		// Token: 0x040029A4 RID: 10660
		AP_Ankle_R,
		// Token: 0x040029A5 RID: 10661
		AP_Tikubi_L,
		// Token: 0x040029A6 RID: 10662
		AP_Tikubi_R,
		// Token: 0x040029A7 RID: 10663
		AP_Waist,
		// Token: 0x040029A8 RID: 10664
		AP_Shoulder_L,
		// Token: 0x040029A9 RID: 10665
		AP_Shoulder_R,
		// Token: 0x040029AA RID: 10666
		AP_Hand_L,
		// Token: 0x040029AB RID: 10667
		AP_Hand_R,
		// Token: 0x040029AC RID: 10668
		AP_Hand_L_NEO,
		// Token: 0x040029AD RID: 10669
		H_Kokan,
		// Token: 0x040029AE RID: 10670
		H_DanDir,
		// Token: 0x040029AF RID: 10671
		S_TongueF,
		// Token: 0x040029B0 RID: 10672
		S_TongueB,
		// Token: 0x040029B1 RID: 10673
		S_MNPB,
		// Token: 0x040029B2 RID: 10674
		S_NIP,
		// Token: 0x040029B3 RID: 10675
		S_TEARS_01,
		// Token: 0x040029B4 RID: 10676
		S_TEARS_02,
		// Token: 0x040029B5 RID: 10677
		S_TEARS_03,
		// Token: 0x040029B6 RID: 10678
		F_ADJUSTWIDTHSCALE,
		// Token: 0x040029B7 RID: 10679
		A_ROOTBONE
	}

	// Token: 0x020006A8 RID: 1704
	public enum TagObjKey
	{
		// Token: 0x040029B9 RID: 10681
		ObjHairB,
		// Token: 0x040029BA RID: 10682
		ObjHairF,
		// Token: 0x040029BB RID: 10683
		ObjHairS,
		// Token: 0x040029BC RID: 10684
		ObjHairO,
		// Token: 0x040029BD RID: 10685
		ObjHairAcsB,
		// Token: 0x040029BE RID: 10686
		ObjHairAcsF,
		// Token: 0x040029BF RID: 10687
		ObjHairAcsS,
		// Token: 0x040029C0 RID: 10688
		ObjHairAcsO,
		// Token: 0x040029C1 RID: 10689
		ObjSkinFace,
		// Token: 0x040029C2 RID: 10690
		ObjEyebrow,
		// Token: 0x040029C3 RID: 10691
		ObjEyeL,
		// Token: 0x040029C4 RID: 10692
		ObjEyeR,
		// Token: 0x040029C5 RID: 10693
		ObjEyeHi,
		// Token: 0x040029C6 RID: 10694
		ObjEyelashes,
		// Token: 0x040029C7 RID: 10695
		ObjBeard,
		// Token: 0x040029C8 RID: 10696
		ObjEyeW,
		// Token: 0x040029C9 RID: 10697
		ObjNail,
		// Token: 0x040029CA RID: 10698
		ObjNip,
		// Token: 0x040029CB RID: 10699
		ObjUnderHair,
		// Token: 0x040029CC RID: 10700
		ObjSkinBody,
		// Token: 0x040029CD RID: 10701
		ObjCTop,
		// Token: 0x040029CE RID: 10702
		ObjCBot,
		// Token: 0x040029CF RID: 10703
		ObjBra,
		// Token: 0x040029D0 RID: 10704
		ObjShorts,
		// Token: 0x040029D1 RID: 10705
		ObjGloves,
		// Token: 0x040029D2 RID: 10706
		ObjPanst,
		// Token: 0x040029D3 RID: 10707
		ObjSocks,
		// Token: 0x040029D4 RID: 10708
		ObjShoes,
		// Token: 0x040029D5 RID: 10709
		ObjSwim,
		// Token: 0x040029D6 RID: 10710
		ObjSTop,
		// Token: 0x040029D7 RID: 10711
		ObjSBot,
		// Token: 0x040029D8 RID: 10712
		ObjASlot01,
		// Token: 0x040029D9 RID: 10713
		ObjASlot02,
		// Token: 0x040029DA RID: 10714
		ObjASlot03,
		// Token: 0x040029DB RID: 10715
		ObjASlot04,
		// Token: 0x040029DC RID: 10716
		ObjASlot05,
		// Token: 0x040029DD RID: 10717
		ObjASlot06,
		// Token: 0x040029DE RID: 10718
		ObjASlot07,
		// Token: 0x040029DF RID: 10719
		ObjASlot08,
		// Token: 0x040029E0 RID: 10720
		ObjASlot09,
		// Token: 0x040029E1 RID: 10721
		ObjASlot10
	}
}
