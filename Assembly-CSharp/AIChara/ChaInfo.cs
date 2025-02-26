using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CharaUtils;
using Correct.Process;
using Illusion.Extensions;
using RootMotion.FinalIK;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007BF RID: 1983
	public class ChaInfo : ChaReference
	{
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x001095CB File Offset: 0x001079CB
		// (set) Token: 0x0600307C RID: 12412 RVA: 0x001095D3 File Offset: 0x001079D3
		public ChaFileControl chaFile { get; protected set; }

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600307D RID: 12413 RVA: 0x001095DC File Offset: 0x001079DC
		public ChaFileCustom fileCustom
		{
			[CompilerGenerated]
			get
			{
				return this.chaFile.custom;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x001095E9 File Offset: 0x001079E9
		public ChaFileBody fileBody
		{
			[CompilerGenerated]
			get
			{
				return this.chaFile.custom.body;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600307F RID: 12415 RVA: 0x001095FB File Offset: 0x001079FB
		public ChaFileFace fileFace
		{
			[CompilerGenerated]
			get
			{
				return this.chaFile.custom.face;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x0010960D File Offset: 0x00107A0D
		public ChaFileHair fileHair
		{
			[CompilerGenerated]
			get
			{
				return this.chaFile.custom.hair;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x0010961F File Offset: 0x00107A1F
		public ChaFileParameter fileParam
		{
			[CompilerGenerated]
			get
			{
				return this.chaFile.parameter;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x0010962C File Offset: 0x00107A2C
		public ChaFileGameInfo fileGameInfo
		{
			[CompilerGenerated]
			get
			{
				return this.chaFile.gameinfo;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x00109639 File Offset: 0x00107A39
		public ChaFileParameter2 fileParam2
		{
			[CompilerGenerated]
			get
			{
				return this.chaFile.parameter2;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x00109646 File Offset: 0x00107A46
		public ChaFileGameInfo2 fileGameInfo2
		{
			[CompilerGenerated]
			get
			{
				return this.chaFile.gameinfo2;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x00109653 File Offset: 0x00107A53
		public ChaFileStatus fileStatus
		{
			[CompilerGenerated]
			get
			{
				return this.chaFile.status;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x00109660 File Offset: 0x00107A60
		// (set) Token: 0x06003087 RID: 12423 RVA: 0x00109668 File Offset: 0x00107A68
		public ChaListControl lstCtrl { get; protected set; }

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x00109671 File Offset: 0x00107A71
		// (set) Token: 0x06003089 RID: 12425 RVA: 0x00109679 File Offset: 0x00107A79
		public EyeLookController eyeLookCtrl { get; protected set; }

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x00109682 File Offset: 0x00107A82
		// (set) Token: 0x0600308B RID: 12427 RVA: 0x0010968A File Offset: 0x00107A8A
		public NeckLookControllerVer2 neckLookCtrl { get; protected set; }

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x00109693 File Offset: 0x00107A93
		// (set) Token: 0x0600308D RID: 12429 RVA: 0x0010969B File Offset: 0x00107A9B
		public FaceBlendShape fbsCtrl { get; protected set; }

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x001096A4 File Offset: 0x00107AA4
		// (set) Token: 0x0600308F RID: 12431 RVA: 0x001096AC File Offset: 0x00107AAC
		public FBSCtrlEyebrow eyebrowCtrl { get; protected set; }

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x001096B5 File Offset: 0x00107AB5
		// (set) Token: 0x06003091 RID: 12433 RVA: 0x001096BD File Offset: 0x00107ABD
		public FBSCtrlEyes eyesCtrl { get; protected set; }

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x001096C6 File Offset: 0x00107AC6
		// (set) Token: 0x06003093 RID: 12435 RVA: 0x001096CE File Offset: 0x00107ACE
		public FBSCtrlMouth mouthCtrl { get; protected set; }

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x001096D7 File Offset: 0x00107AD7
		// (set) Token: 0x06003095 RID: 12437 RVA: 0x001096DF File Offset: 0x00107ADF
		public Expression expression { get; protected set; }

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x001096E8 File Offset: 0x00107AE8
		// (set) Token: 0x06003097 RID: 12439 RVA: 0x001096F0 File Offset: 0x00107AF0
		public CmpBoneHead cmpBoneHead { get; protected set; }

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x001096F9 File Offset: 0x00107AF9
		// (set) Token: 0x06003099 RID: 12441 RVA: 0x00109701 File Offset: 0x00107B01
		public CmpBoneBody cmpBoneBody { get; protected set; }

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x0010970A File Offset: 0x00107B0A
		// (set) Token: 0x0600309B RID: 12443 RVA: 0x00109712 File Offset: 0x00107B12
		public CmpFace cmpFace { get; protected set; }

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x0010971B File Offset: 0x00107B1B
		// (set) Token: 0x0600309D RID: 12445 RVA: 0x00109723 File Offset: 0x00107B23
		public CmpBody cmpBody { get; protected set; }

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x0010972C File Offset: 0x00107B2C
		// (set) Token: 0x0600309F RID: 12447 RVA: 0x00109734 File Offset: 0x00107B34
		public CmpBody cmpSimpleBody { get; protected set; }

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x0010973D File Offset: 0x00107B3D
		// (set) Token: 0x060030A1 RID: 12449 RVA: 0x00109745 File Offset: 0x00107B45
		public CmpHair[] cmpHair { get; protected set; }

		// Token: 0x060030A2 RID: 12450 RVA: 0x00109750 File Offset: 0x00107B50
		public CmpHair GetCustomHairComponent(int parts)
		{
			if (this.cmpHair == null)
			{
				return null;
			}
			if (parts >= this.cmpHair.Length)
			{
				return null;
			}
			CmpHair cmpHair = this.cmpHair[parts];
			if (null == cmpHair)
			{
				return null;
			}
			return cmpHair;
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x00109792 File Offset: 0x00107B92
		// (set) Token: 0x060030A4 RID: 12452 RVA: 0x0010979A File Offset: 0x00107B9A
		public CmpClothes[] cmpClothes { get; protected set; }

		// Token: 0x060030A5 RID: 12453 RVA: 0x001097A4 File Offset: 0x00107BA4
		public CmpClothes GetCustomClothesComponent(int parts)
		{
			if (this.cmpClothes == null)
			{
				return null;
			}
			if (parts >= this.cmpClothes.Length)
			{
				return null;
			}
			CmpClothes cmpClothes = this.cmpClothes[parts];
			if (null == cmpClothes)
			{
				return null;
			}
			return cmpClothes;
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x001097E6 File Offset: 0x00107BE6
		// (set) Token: 0x060030A7 RID: 12455 RVA: 0x001097EE File Offset: 0x00107BEE
		public CmpAccessory[] cmpAccessory { get; protected set; }

		// Token: 0x060030A8 RID: 12456 RVA: 0x001097F8 File Offset: 0x00107BF8
		public CmpAccessory GetAccessoryComponent(int parts)
		{
			if (this.cmpAccessory == null)
			{
				return null;
			}
			if (parts >= this.cmpAccessory.Length)
			{
				return null;
			}
			CmpAccessory cmpAccessory = this.cmpAccessory[parts];
			if (null == cmpAccessory)
			{
				return null;
			}
			return cmpAccessory;
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x0010983A File Offset: 0x00107C3A
		// (set) Token: 0x060030AA RID: 12458 RVA: 0x00109842 File Offset: 0x00107C42
		public CmpAccessory[] cmpExtraAccessory { get; protected set; }

		// Token: 0x060030AB RID: 12459 RVA: 0x0010984C File Offset: 0x00107C4C
		public CmpAccessory GetExtraAccessoryComponent(int parts)
		{
			if (this.cmpExtraAccessory == null)
			{
				return null;
			}
			if (parts >= this.cmpExtraAccessory.Length)
			{
				return null;
			}
			CmpAccessory cmpAccessory = this.cmpExtraAccessory[parts];
			if (null == cmpAccessory)
			{
				return null;
			}
			return cmpAccessory;
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x0010988E File Offset: 0x00107C8E
		// (set) Token: 0x060030AD RID: 12461 RVA: 0x00109896 File Offset: 0x00107C96
		public FullBodyBipedIK fullBodyIK { get; protected set; }

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x0010989F File Offset: 0x00107C9F
		// (set) Token: 0x060030AF RID: 12463 RVA: 0x001098A7 File Offset: 0x00107CA7
		public int chaID { get; protected set; }

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x001098B0 File Offset: 0x00107CB0
		// (set) Token: 0x060030B1 RID: 12465 RVA: 0x001098B8 File Offset: 0x00107CB8
		public int loadNo { get; protected set; }

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x001098C1 File Offset: 0x00107CC1
		public byte sex
		{
			[CompilerGenerated]
			get
			{
				return this.chaFile.parameter.sex;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x001098D3 File Offset: 0x00107CD3
		// (set) Token: 0x060030B4 RID: 12468 RVA: 0x001098DB File Offset: 0x00107CDB
		public bool isPlayer { get; set; }

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x001098E4 File Offset: 0x00107CE4
		// (set) Token: 0x060030B6 RID: 12470 RVA: 0x001098EC File Offset: 0x00107CEC
		public bool hideMoz { get; set; }

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x001098F5 File Offset: 0x00107CF5
		// (set) Token: 0x060030B8 RID: 12472 RVA: 0x001098FD File Offset: 0x00107CFD
		public bool loadEnd { get; protected set; }

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x00109906 File Offset: 0x00107D06
		// (set) Token: 0x060030BA RID: 12474 RVA: 0x0010990E File Offset: 0x00107D0E
		public bool visibleAll { get; set; }

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x00109917 File Offset: 0x00107D17
		// (set) Token: 0x060030BC RID: 12476 RVA: 0x00109924 File Offset: 0x00107D24
		public bool visibleBody
		{
			get
			{
				return this.fileStatus.visibleBodyAlways;
			}
			set
			{
				this.fileStatus.visibleBodyAlways = value;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x00109932 File Offset: 0x00107D32
		// (set) Token: 0x060030BE RID: 12478 RVA: 0x0010996C File Offset: 0x00107D6C
		public bool visibleSon
		{
			get
			{
				if (this.sex == 0)
				{
					return this.fileStatus.visibleSon;
				}
				return this.fileParam.futanari && this.fileStatus.visibleSonAlways;
			}
			set
			{
				if (this.sex == 0)
				{
					this.fileStatus.visibleSonAlways = true;
					this.fileStatus.visibleSon = value;
				}
				else
				{
					this.fileStatus.visibleSonAlways = (this.fileParam.futanari && value);
					this.fileStatus.visibleSon = true;
				}
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x001099CF File Offset: 0x00107DCF
		// (set) Token: 0x060030C0 RID: 12480 RVA: 0x001099D7 File Offset: 0x00107DD7
		public bool updateShapeFace { get; set; }

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x001099E0 File Offset: 0x00107DE0
		// (set) Token: 0x060030C2 RID: 12482 RVA: 0x001099E8 File Offset: 0x00107DE8
		public bool updateShapeBody { get; set; }

		// Token: 0x17000849 RID: 2121
		// (set) Token: 0x060030C3 RID: 12483 RVA: 0x001099F1 File Offset: 0x00107DF1
		public bool updateShape
		{
			set
			{
				this.updateShapeFace = value;
				this.updateShapeBody = value;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x00109A01 File Offset: 0x00107E01
		// (set) Token: 0x060030C5 RID: 12485 RVA: 0x00109A09 File Offset: 0x00107E09
		public bool updateWet { get; set; }

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x00109A12 File Offset: 0x00107E12
		// (set) Token: 0x060030C7 RID: 12487 RVA: 0x00109A1A File Offset: 0x00107E1A
		public bool resetDynamicBoneAll { get; set; }

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x00109A23 File Offset: 0x00107E23
		// (set) Token: 0x060030C9 RID: 12489 RVA: 0x00109A2B File Offset: 0x00107E2B
		public bool reSetupDynamicBoneBust { get; set; }

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x00109A34 File Offset: 0x00107E34
		// (set) Token: 0x060030CB RID: 12491 RVA: 0x00109A3C File Offset: 0x00107E3C
		protected bool[] enableDynamicBoneBustAndHip { get; set; }

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x060030CC RID: 12492 RVA: 0x00109A45 File Offset: 0x00107E45
		// (set) Token: 0x060030CD RID: 12493 RVA: 0x00109A4D File Offset: 0x00107E4D
		public bool updateBustSize { get; set; }

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x00109A56 File Offset: 0x00107E56
		// (set) Token: 0x060030CF RID: 12495 RVA: 0x00109A5E File Offset: 0x00107E5E
		public bool releaseCustomInputTexture { get; set; }

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x00109A67 File Offset: 0x00107E67
		// (set) Token: 0x060030D1 RID: 12497 RVA: 0x00109A6F File Offset: 0x00107E6F
		public bool loadWithDefaultColorAndPtn { get; set; }

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x00109A78 File Offset: 0x00107E78
		// (set) Token: 0x060030D3 RID: 12499 RVA: 0x00109A80 File Offset: 0x00107E80
		protected bool[] showExtraAccessory { get; set; }

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x00109A89 File Offset: 0x00107E89
		// (set) Token: 0x060030D5 RID: 12501 RVA: 0x00109A91 File Offset: 0x00107E91
		public bool hideHairForThumbnailCapture { get; set; }

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x00109A9A File Offset: 0x00107E9A
		// (set) Token: 0x060030D7 RID: 12503 RVA: 0x00109AA2 File Offset: 0x00107EA2
		public Renderer[] rendBra { get; protected set; }

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x00109AAB File Offset: 0x00107EAB
		// (set) Token: 0x060030D9 RID: 12505 RVA: 0x00109AB3 File Offset: 0x00107EB3
		public Renderer rendInnerTB { get; protected set; }

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x00109ABC File Offset: 0x00107EBC
		// (set) Token: 0x060030DB RID: 12507 RVA: 0x00109AC4 File Offset: 0x00107EC4
		public Renderer rendInnerB { get; protected set; }

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x00109ACD File Offset: 0x00107ECD
		// (set) Token: 0x060030DD RID: 12509 RVA: 0x00109AD5 File Offset: 0x00107ED5
		public Renderer rendPanst { get; protected set; }

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060030DE RID: 12510 RVA: 0x00109ADE File Offset: 0x00107EDE
		// (set) Token: 0x060030DF RID: 12511 RVA: 0x00109AE6 File Offset: 0x00107EE6
		public CustomTextureControl customTexCtrlFace { get; protected set; }

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060030E0 RID: 12512 RVA: 0x00109AEF File Offset: 0x00107EEF
		// (set) Token: 0x060030E1 RID: 12513 RVA: 0x00109AF7 File Offset: 0x00107EF7
		public CustomTextureControl customTexCtrlBody { get; protected set; }

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x00109B00 File Offset: 0x00107F00
		public Material customMatFace
		{
			get
			{
				return (this.customTexCtrlFace != null) ? this.customTexCtrlFace.matDraw : null;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x00109B1E File Offset: 0x00107F1E
		public Material customMatBody
		{
			get
			{
				return (this.customTexCtrlBody != null) ? this.customTexCtrlBody.matDraw : null;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x00109B3C File Offset: 0x00107F3C
		// (set) Token: 0x060030E5 RID: 12517 RVA: 0x00109B44 File Offset: 0x00107F44
		public CustomTextureCreate[,] ctCreateClothes { get; protected set; }

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x00109B4D File Offset: 0x00107F4D
		// (set) Token: 0x060030E7 RID: 12519 RVA: 0x00109B55 File Offset: 0x00107F55
		public CustomTextureCreate[,] ctCreateClothesGloss { get; protected set; }

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x00109B5E File Offset: 0x00107F5E
		// (set) Token: 0x060030E9 RID: 12521 RVA: 0x00109B66 File Offset: 0x00107F66
		public GameObject objRoot { get; protected set; }

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x00109B6F File Offset: 0x00107F6F
		// (set) Token: 0x060030EB RID: 12523 RVA: 0x00109B77 File Offset: 0x00107F77
		public GameObject objTop { get; protected set; }

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x00109B80 File Offset: 0x00107F80
		// (set) Token: 0x060030ED RID: 12525 RVA: 0x00109B88 File Offset: 0x00107F88
		public GameObject objAnim { get; protected set; }

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x00109B91 File Offset: 0x00107F91
		// (set) Token: 0x060030EF RID: 12527 RVA: 0x00109B99 File Offset: 0x00107F99
		public GameObject objBodyBone { get; protected set; }

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x00109BA2 File Offset: 0x00107FA2
		// (set) Token: 0x060030F1 RID: 12529 RVA: 0x00109BAA File Offset: 0x00107FAA
		public GameObject objBody { get; protected set; }

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x00109BB3 File Offset: 0x00107FB3
		// (set) Token: 0x060030F3 RID: 12531 RVA: 0x00109BBB File Offset: 0x00107FBB
		public GameObject objSimpleBody { get; protected set; }

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060030F4 RID: 12532 RVA: 0x00109BC4 File Offset: 0x00107FC4
		// (set) Token: 0x060030F5 RID: 12533 RVA: 0x00109BCC File Offset: 0x00107FCC
		public GameObject objHeadBone { get; protected set; }

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x00109BD5 File Offset: 0x00107FD5
		// (set) Token: 0x060030F7 RID: 12535 RVA: 0x00109BDD File Offset: 0x00107FDD
		public GameObject objHead { get; protected set; }

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x00109BE6 File Offset: 0x00107FE6
		// (set) Token: 0x060030F9 RID: 12537 RVA: 0x00109BEE File Offset: 0x00107FEE
		public GameObject[] objHair
		{
			get
			{
				return this._objHair;
			}
			protected set
			{
				this._objHair = value;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x00109BF7 File Offset: 0x00107FF7
		// (set) Token: 0x060030FB RID: 12539 RVA: 0x00109BFF File Offset: 0x00107FFF
		public GameObject[] objClothes
		{
			get
			{
				return this._objClothes;
			}
			protected set
			{
				this._objClothes = value;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x00109C08 File Offset: 0x00108008
		// (set) Token: 0x060030FD RID: 12541 RVA: 0x00109C10 File Offset: 0x00108010
		public GameObject[] objAccessory
		{
			get
			{
				return this._objAccessory;
			}
			protected set
			{
				this._objAccessory = value;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x00109C19 File Offset: 0x00108019
		// (set) Token: 0x060030FF RID: 12543 RVA: 0x00109C21 File Offset: 0x00108021
		public Transform[,] trfAcsMove
		{
			get
			{
				return this._trfAcsMove;
			}
			protected set
			{
				this._trfAcsMove = value;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x00109C2A File Offset: 0x0010802A
		// (set) Token: 0x06003101 RID: 12545 RVA: 0x00109C32 File Offset: 0x00108032
		public GameObject objHitBody { get; protected set; }

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x00109C3B File Offset: 0x0010803B
		// (set) Token: 0x06003103 RID: 12547 RVA: 0x00109C43 File Offset: 0x00108043
		public GameObject objHitHead { get; protected set; }

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x00109C4C File Offset: 0x0010804C
		// (set) Token: 0x06003105 RID: 12549 RVA: 0x00109C54 File Offset: 0x00108054
		public Animator animBody { get; protected set; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x00109C5D File Offset: 0x0010805D
		// (set) Token: 0x06003107 RID: 12551 RVA: 0x00109C65 File Offset: 0x00108065
		public GameObject objEyesLookTargetP { get; protected set; }

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x00109C6E File Offset: 0x0010806E
		// (set) Token: 0x06003109 RID: 12553 RVA: 0x00109C76 File Offset: 0x00108076
		public GameObject objEyesLookTarget { get; protected set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x00109C7F File Offset: 0x0010807F
		// (set) Token: 0x0600310B RID: 12555 RVA: 0x00109C87 File Offset: 0x00108087
		public GameObject objNeckLookTargetP { get; protected set; }

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x00109C90 File Offset: 0x00108090
		// (set) Token: 0x0600310D RID: 12557 RVA: 0x00109C98 File Offset: 0x00108098
		public GameObject objNeckLookTarget { get; protected set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x00109CA1 File Offset: 0x001080A1
		// (set) Token: 0x0600310F RID: 12559 RVA: 0x00109CA9 File Offset: 0x001080A9
		public GameObject[] objExtraAccessory
		{
			get
			{
				return this._objExtraAccessory;
			}
			protected set
			{
				this._objExtraAccessory = value;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x00109CB2 File Offset: 0x001080B2
		// (set) Token: 0x06003111 RID: 12561 RVA: 0x00109CBA File Offset: 0x001080BA
		public ListInfoBase infoHead { get; protected set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x00109CC3 File Offset: 0x001080C3
		// (set) Token: 0x06003113 RID: 12563 RVA: 0x00109CCB File Offset: 0x001080CB
		public ListInfoBase[] infoHair
		{
			get
			{
				return this._infoHair;
			}
			protected set
			{
				this._infoHair = value;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x00109CD4 File Offset: 0x001080D4
		// (set) Token: 0x06003115 RID: 12565 RVA: 0x00109CDC File Offset: 0x001080DC
		public ListInfoBase[] infoClothes
		{
			get
			{
				return this._infoClothes;
			}
			protected set
			{
				this._infoClothes = value;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x00109CE5 File Offset: 0x001080E5
		// (set) Token: 0x06003117 RID: 12567 RVA: 0x00109CED File Offset: 0x001080ED
		public ListInfoBase[] infoAccessory
		{
			get
			{
				return this._infoAccessory;
			}
			protected set
			{
				this._infoAccessory = value;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x00109CF6 File Offset: 0x001080F6
		// (set) Token: 0x06003119 RID: 12569 RVA: 0x00109D1A File Offset: 0x0010811A
		public bool enableExpression
		{
			get
			{
				return !(null == this.expression) && this.expression.enable;
			}
			set
			{
				if (null != this.expression)
				{
					this.expression.enable = value;
				}
			}
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x00109D39 File Offset: 0x00108139
		public void EnableExpressionIndex(int indexNo, bool enable)
		{
			if (null != this.expression)
			{
				this.expression.EnableIndex(indexNo, enable);
			}
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x00109D59 File Offset: 0x00108159
		public void EnableExpressionCategory(int categoryNo, bool enable)
		{
			if (null != this.expression)
			{
				this.expression.EnableCategory(categoryNo, enable);
			}
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x00109D79 File Offset: 0x00108179
		public int GetBustSizeKind()
		{
			return this.fileCustom.GetBustSizeKind();
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x00109D86 File Offset: 0x00108186
		public int GetHeightKind()
		{
			return this.fileCustom.GetHeightKind();
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x00109D93 File Offset: 0x00108193
		public int GetHairType()
		{
			return this.fileHair.kind;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x00109DA0 File Offset: 0x001081A0
		protected void MemberInitializeAll()
		{
			this.chaFile = null;
			this.lstCtrl = null;
			this.chaID = 0;
			this.loadNo = -1;
			this.hideMoz = false;
			this.releaseCustomInputTexture = true;
			this.loadWithDefaultColorAndPtn = false;
			this.hideHairForThumbnailCapture = false;
			this.objRoot = null;
			this.customTexCtrlBody = null;
			this.MemberInitializeObject();
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x00109DFC File Offset: 0x001081FC
		protected void MemberInitializeObject()
		{
			this.eyeLookCtrl = null;
			this.neckLookCtrl = null;
			this.fbsCtrl = null;
			this.eyebrowCtrl = null;
			this.eyesCtrl = null;
			this.mouthCtrl = null;
			this.expression = null;
			this.cmpFace = null;
			this.cmpBody = null;
			this.cmpHair = new CmpHair[Enum.GetNames(typeof(ChaFileDefine.HairKind)).Length];
			this.cmpClothes = new CmpClothes[Enum.GetNames(typeof(ChaFileDefine.ClothesKind)).Length];
			this.cmpAccessory = new CmpAccessory[20];
			this.cmpExtraAccessory = new CmpAccessory[Enum.GetNames(typeof(ChaControlDefine.ExtraAccessoryParts)).Length];
			this.customTexCtrlFace = null;
			this.ctCreateClothes = new CustomTextureCreate[Enum.GetNames(typeof(ChaFileDefine.ClothesKind)).Length, 3];
			this.ctCreateClothesGloss = new CustomTextureCreate[Enum.GetNames(typeof(ChaFileDefine.ClothesKind)).Length, 3];
			this.loadEnd = false;
			this.visibleAll = true;
			this.updateShapeFace = false;
			this.updateShapeBody = false;
			this.resetDynamicBoneAll = false;
			this.reSetupDynamicBoneBust = false;
			this.enableDynamicBoneBustAndHip = new bool[]
			{
				true,
				true,
				true,
				true
			};
			this.updateBustSize = false;
			this.showExtraAccessory = new bool[Enum.GetNames(typeof(ChaControlDefine.ExtraAccessoryParts)).Length];
			for (int i = 0; i < this.showExtraAccessory.Length; i++)
			{
				this.showExtraAccessory[i] = false;
			}
			this.rendBra = new Renderer[2];
			this.rendInnerTB = null;
			this.rendInnerB = null;
			this.rendPanst = null;
			this.objTop = null;
			this.objAnim = null;
			this.objBodyBone = null;
			this.objBody = null;
			this.objSimpleBody = null;
			this.objHeadBone = null;
			this.objHead = null;
			this.objHair = new GameObject[Enum.GetNames(typeof(ChaFileDefine.HairKind)).Length];
			this.objClothes = new GameObject[Enum.GetNames(typeof(ChaFileDefine.ClothesKind)).Length];
			this.objAccessory = new GameObject[20];
			this.trfAcsMove = new Transform[20, 2];
			this.objHitHead = null;
			this.objHitBody = null;
			this.animBody = null;
			this.objEyesLookTargetP = null;
			this.objEyesLookTarget = null;
			this.objNeckLookTargetP = null;
			this.objNeckLookTarget = null;
			this.dictAccessoryParent = null;
			this.objExtraAccessory = new GameObject[Enum.GetNames(typeof(ChaControlDefine.ExtraAccessoryParts)).Length];
			this.infoHead = null;
			this.infoHair = new ListInfoBase[Enum.GetNames(typeof(ChaFileDefine.HairKind)).Length];
			this.infoClothes = new ListInfoBase[Enum.GetNames(typeof(ChaFileDefine.ClothesKind)).Length];
			this.infoAccessory = new ListInfoBase[20];
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x0010A0B5 File Offset: 0x001084B5
		protected void ReleaseInfoAll()
		{
			this.ReleaseInfoObject(false);
			if (this.customTexCtrlBody != null)
			{
				this.customTexCtrlBody.Release();
			}
			Resources.UnloadUnusedAssets();
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x0010A0DC File Offset: 0x001084DC
		protected void ReleaseInfoObject(bool init = true)
		{
			if (this.customTexCtrlFace != null)
			{
				this.customTexCtrlFace.Release();
			}
			if (this.ctCreateClothes != null)
			{
				for (int i = 0; i < this.ctCreateClothes.GetLength(0); i++)
				{
					for (int j = 0; j < 3; j++)
					{
						if (this.ctCreateClothes[i, j] != null)
						{
							this.ctCreateClothes[i, j].Release();
							this.ctCreateClothes[i, j] = null;
						}
					}
				}
			}
			if (this.ctCreateClothesGloss != null)
			{
				for (int k = 0; k < this.ctCreateClothesGloss.GetLength(0); k++)
				{
					for (int l = 0; l < 3; l++)
					{
						if (this.ctCreateClothesGloss[k, l] != null)
						{
							this.ctCreateClothesGloss[k, l].Release();
							this.ctCreateClothesGloss[k, l] = null;
						}
					}
				}
			}
			bool flag = false;
			if (flag)
			{
				if (null != this.objTop)
				{
					IKAfterProcess[] componentsInChildren = this.objTop.GetComponentsInChildren<IKAfterProcess>(true);
					IKBeforeOfDankonProcess[] componentsInChildren2 = this.objTop.GetComponentsInChildren<IKBeforeOfDankonProcess>(true);
					IKBeforeProcess[] componentsInChildren3 = this.objTop.GetComponentsInChildren<IKBeforeProcess>(true);
					for (int m = 0; m < componentsInChildren.Length; m++)
					{
						UnityEngine.Object.DestroyImmediate(componentsInChildren[m]);
					}
					for (int n = 0; n < componentsInChildren2.Length; n++)
					{
						UnityEngine.Object.DestroyImmediate(componentsInChildren2[n]);
					}
					for (int num = 0; num < componentsInChildren3.Length; num++)
					{
						UnityEngine.Object.DestroyImmediate(componentsInChildren3[num]);
					}
					this.objTop.SetActiveIfDifferent(false);
					this.objTop.name = "Delete_Reserve";
					UnityEngine.Object.Destroy(this.objTop);
				}
			}
			else
			{
				this.SafeDestroy(this.objTop);
			}
			this.objTop = null;
			base.ReleaseRefAll();
			if (init)
			{
				this.MemberInitializeObject();
			}
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x0010A2D8 File Offset: 0x001086D8
		public void SafeDestroy(GameObject obj)
		{
			if (null != obj)
			{
				IKAfterProcess[] componentsInChildren = obj.GetComponentsInChildren<IKAfterProcess>(true);
				IKBeforeOfDankonProcess[] componentsInChildren2 = obj.GetComponentsInChildren<IKBeforeOfDankonProcess>(true);
				IKBeforeProcess[] componentsInChildren3 = obj.GetComponentsInChildren<IKBeforeProcess>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					UnityEngine.Object.DestroyImmediate(componentsInChildren[i]);
				}
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					UnityEngine.Object.DestroyImmediate(componentsInChildren2[j]);
				}
				for (int k = 0; k < componentsInChildren3.Length; k++)
				{
					UnityEngine.Object.DestroyImmediate(componentsInChildren3[k]);
				}
				obj.SetActiveIfDifferent(false);
				obj.transform.SetParent(null);
				obj.name = "Delete_Reserve";
				UnityEngine.Object.Destroy(obj);
			}
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x0010A38C File Offset: 0x0010878C
		public DynamicBone[] GetDynamicBoneHairAll()
		{
			if (this.cmpHair == null)
			{
				return null;
			}
			List<DynamicBone> list = new List<DynamicBone>();
			for (int i = 0; i < this.cmpHair.Length; i++)
			{
				if (!(null == this.cmpHair[i]))
				{
					if (this.cmpHair[i].boneInfo != null)
					{
						foreach (DynamicBone[] collection in from x in this.cmpHair[i].boneInfo
						where x != null && null != x.dynamicBone
						select x.dynamicBone)
						{
							list.AddRange(collection);
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x0010A494 File Offset: 0x00108894
		public DynamicBone[] GetDynamicBoneHair(int parts)
		{
			if (this.cmpHair == null)
			{
				return null;
			}
			if (parts >= this.cmpHair.Length)
			{
				return null;
			}
			if (this.cmpHair[parts].boneInfo == null)
			{
				return null;
			}
			List<DynamicBone> list = new List<DynamicBone>();
			foreach (DynamicBone[] collection in from x in this.cmpHair[parts].boneInfo
			where x != null && null != x.dynamicBone
			select x.dynamicBone)
			{
				list.AddRange(collection);
			}
			return list.ToArray();
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x0010A578 File Offset: 0x00108978
		public void InitializeAccessoryParent()
		{
			this.dictAccessoryParent = new Dictionary<int, Transform>();
			if (null != this.cmpBoneHead)
			{
				string[] array = new string[]
				{
					"N_Hair_pony",
					"N_Hair_twin_L",
					"N_Hair_twin_R",
					"N_Hair_pin_L",
					"N_Hair_pin_R",
					"N_Head_top",
					"N_Head",
					"N_Hitai",
					"N_Face",
					"N_Megane",
					"N_Earring_L",
					"N_Earring_R",
					"N_Nose",
					"N_Mouth"
				};
				Transform[] array2 = new Transform[]
				{
					this.cmpBoneHead.targetAccessory.acs_Hair_pony,
					this.cmpBoneHead.targetAccessory.acs_Hair_twin_L,
					this.cmpBoneHead.targetAccessory.acs_Hair_twin_R,
					this.cmpBoneHead.targetAccessory.acs_Hair_pin_L,
					this.cmpBoneHead.targetAccessory.acs_Hair_pin_R,
					this.cmpBoneHead.targetAccessory.acs_Head_top,
					this.cmpBoneHead.targetAccessory.acs_Head,
					this.cmpBoneHead.targetAccessory.acs_Hitai,
					this.cmpBoneHead.targetAccessory.acs_Face,
					this.cmpBoneHead.targetAccessory.acs_Megane,
					this.cmpBoneHead.targetAccessory.acs_Earring_L,
					this.cmpBoneHead.targetAccessory.acs_Earring_R,
					this.cmpBoneHead.targetAccessory.acs_Nose,
					this.cmpBoneHead.targetAccessory.acs_Mouth
				};
				for (int i = 0; i < array.Length; i++)
				{
					int accessoryParentInt = ChaAccessoryDefine.GetAccessoryParentInt(array[i]);
					this.dictAccessoryParent[accessoryParentInt] = array2[i];
				}
			}
			if (null != this.cmpBoneBody)
			{
				string[] array3 = new string[]
				{
					"N_Neck",
					"N_Chest_f",
					"N_Chest",
					"N_Tikubi_L",
					"N_Tikubi_R",
					"N_Back",
					"N_Back_L",
					"N_Back_R",
					"N_Waist",
					"N_Waist_f",
					"N_Waist_b",
					"N_Waist_L",
					"N_Waist_R",
					"N_Leg_L",
					"N_Leg_R",
					"N_Knee_L",
					"N_Knee_R",
					"N_Ankle_L",
					"N_Ankle_R",
					"N_Foot_L",
					"N_Foot_R",
					"N_Shoulder_L",
					"N_Shoulder_R",
					"N_Elbo_L",
					"N_Elbo_R",
					"N_Arm_L",
					"N_Arm_R",
					"N_Wrist_L",
					"N_Wrist_R",
					"N_Hand_L",
					"N_Hand_R",
					"N_Index_L",
					"N_Index_R",
					"N_Middle_L",
					"N_Middle_R",
					"N_Ring_L",
					"N_Ring_R",
					"N_Dan",
					"N_Kokan",
					"N_Ana"
				};
				Transform[] array4 = new Transform[]
				{
					this.cmpBoneBody.targetAccessory.acs_Neck,
					this.cmpBoneBody.targetAccessory.acs_Chest_f,
					this.cmpBoneBody.targetAccessory.acs_Chest,
					this.cmpBoneBody.targetAccessory.acs_Tikubi_L,
					this.cmpBoneBody.targetAccessory.acs_Tikubi_R,
					this.cmpBoneBody.targetAccessory.acs_Back,
					this.cmpBoneBody.targetAccessory.acs_Back_L,
					this.cmpBoneBody.targetAccessory.acs_Back_R,
					this.cmpBoneBody.targetAccessory.acs_Waist,
					this.cmpBoneBody.targetAccessory.acs_Waist_f,
					this.cmpBoneBody.targetAccessory.acs_Waist_b,
					this.cmpBoneBody.targetAccessory.acs_Waist_L,
					this.cmpBoneBody.targetAccessory.acs_Waist_R,
					this.cmpBoneBody.targetAccessory.acs_Leg_L,
					this.cmpBoneBody.targetAccessory.acs_Leg_R,
					this.cmpBoneBody.targetAccessory.acs_Knee_L,
					this.cmpBoneBody.targetAccessory.acs_Knee_R,
					this.cmpBoneBody.targetAccessory.acs_Ankle_L,
					this.cmpBoneBody.targetAccessory.acs_Ankle_R,
					this.cmpBoneBody.targetAccessory.acs_Foot_L,
					this.cmpBoneBody.targetAccessory.acs_Foot_R,
					this.cmpBoneBody.targetAccessory.acs_Shoulder_L,
					this.cmpBoneBody.targetAccessory.acs_Shoulder_R,
					this.cmpBoneBody.targetAccessory.acs_Elbo_L,
					this.cmpBoneBody.targetAccessory.acs_Elbo_R,
					this.cmpBoneBody.targetAccessory.acs_Arm_L,
					this.cmpBoneBody.targetAccessory.acs_Arm_R,
					this.cmpBoneBody.targetAccessory.acs_Wrist_L,
					this.cmpBoneBody.targetAccessory.acs_Wrist_R,
					this.cmpBoneBody.targetAccessory.acs_Hand_L,
					this.cmpBoneBody.targetAccessory.acs_Hand_R,
					this.cmpBoneBody.targetAccessory.acs_Index_L,
					this.cmpBoneBody.targetAccessory.acs_Index_R,
					this.cmpBoneBody.targetAccessory.acs_Middle_L,
					this.cmpBoneBody.targetAccessory.acs_Middle_R,
					this.cmpBoneBody.targetAccessory.acs_Ring_L,
					this.cmpBoneBody.targetAccessory.acs_Ring_R,
					this.cmpBoneBody.targetAccessory.acs_Dan,
					this.cmpBoneBody.targetAccessory.acs_Kokan,
					this.cmpBoneBody.targetAccessory.acs_Ana
				};
				for (int j = 0; j < array3.Length; j++)
				{
					int accessoryParentInt2 = ChaAccessoryDefine.GetAccessoryParentInt(array3[j]);
					this.dictAccessoryParent[accessoryParentInt2] = array4[j];
				}
			}
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x0010AC34 File Offset: 0x00109034
		public Transform GetAccessoryParentTransform(string key)
		{
			int accessoryParentInt = ChaAccessoryDefine.GetAccessoryParentInt(key);
			Transform result;
			if (this.dictAccessoryParent.TryGetValue(accessoryParentInt, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x0010AC60 File Offset: 0x00109060
		public Transform GetAccessoryParentTransform(int index)
		{
			Transform result;
			if (this.dictAccessoryParent.TryGetValue(index, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x04002E5F RID: 11871
		private GameObject[] _objHair;

		// Token: 0x04002E60 RID: 11872
		private GameObject[] _objClothes;

		// Token: 0x04002E61 RID: 11873
		private GameObject[] _objAccessory;

		// Token: 0x04002E62 RID: 11874
		private Transform[,] _trfAcsMove;

		// Token: 0x04002E6A RID: 11882
		public Dictionary<int, Transform> dictAccessoryParent;

		// Token: 0x04002E6B RID: 11883
		private GameObject[] _objExtraAccessory;

		// Token: 0x04002E6C RID: 11884
		private Transform[,] _trfExtraAcsMove;

		// Token: 0x04002E6E RID: 11886
		private ListInfoBase[] _infoHair;

		// Token: 0x04002E6F RID: 11887
		private ListInfoBase[] _infoClothes;

		// Token: 0x04002E70 RID: 11888
		private ListInfoBase[] _infoAccessory;
	}
}
