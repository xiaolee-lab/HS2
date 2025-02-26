using System;

namespace Rewired
{
	// Token: 0x0200057F RID: 1407
	public sealed class HOTASTemplate : ControllerTemplate, IHOTASTemplate, IControllerTemplate
	{
		// Token: 0x06001F53 RID: 8019 RVA: 0x000AF2CB File Offset: 0x000AD6CB
		public HOTASTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x000AF2D4 File Offset: 0x000AD6D4
		IControllerTemplateButton IHOTASTemplate.stickTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(3);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x000AF2DD File Offset: 0x000AD6DD
		IControllerTemplateButton IHOTASTemplate.stickTriggerStage2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x000AF2E6 File Offset: 0x000AD6E6
		IControllerTemplateButton IHOTASTemplate.stickPinkyButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x000AF2EF File Offset: 0x000AD6EF
		IControllerTemplateButton IHOTASTemplate.stickPinkyTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(154);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x000AF2FC File Offset: 0x000AD6FC
		IControllerTemplateButton IHOTASTemplate.stickButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x000AF305 File Offset: 0x000AD705
		IControllerTemplateButton IHOTASTemplate.stickButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x000AF30E File Offset: 0x000AD70E
		IControllerTemplateButton IHOTASTemplate.stickButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x000AF317 File Offset: 0x000AD717
		IControllerTemplateButton IHOTASTemplate.stickButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x000AF321 File Offset: 0x000AD721
		IControllerTemplateButton IHOTASTemplate.stickButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x000AF32B File Offset: 0x000AD72B
		IControllerTemplateButton IHOTASTemplate.stickButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001F5E RID: 8030 RVA: 0x000AF335 File Offset: 0x000AD735
		IControllerTemplateButton IHOTASTemplate.stickButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x000AF33F File Offset: 0x000AD73F
		IControllerTemplateButton IHOTASTemplate.stickButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001F60 RID: 8032 RVA: 0x000AF349 File Offset: 0x000AD749
		IControllerTemplateButton IHOTASTemplate.stickButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x000AF353 File Offset: 0x000AD753
		IControllerTemplateButton IHOTASTemplate.stickButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x000AF35D File Offset: 0x000AD75D
		IControllerTemplateButton IHOTASTemplate.stickBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x000AF367 File Offset: 0x000AD767
		IControllerTemplateButton IHOTASTemplate.stickBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x000AF371 File Offset: 0x000AD771
		IControllerTemplateButton IHOTASTemplate.stickBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x000AF37B File Offset: 0x000AD77B
		IControllerTemplateButton IHOTASTemplate.stickBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x000AF385 File Offset: 0x000AD785
		IControllerTemplateButton IHOTASTemplate.stickBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x000AF38F File Offset: 0x000AD78F
		IControllerTemplateButton IHOTASTemplate.stickBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x000AF399 File Offset: 0x000AD799
		IControllerTemplateButton IHOTASTemplate.stickBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x000AF3A3 File Offset: 0x000AD7A3
		IControllerTemplateButton IHOTASTemplate.stickBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x000AF3AD File Offset: 0x000AD7AD
		IControllerTemplateButton IHOTASTemplate.stickBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x000AF3B7 File Offset: 0x000AD7B7
		IControllerTemplateButton IHOTASTemplate.stickBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x000AF3C1 File Offset: 0x000AD7C1
		IControllerTemplateButton IHOTASTemplate.stickBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(161);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x000AF3CE File Offset: 0x000AD7CE
		IControllerTemplateButton IHOTASTemplate.stickBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(162);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x000AF3DB File Offset: 0x000AD7DB
		IControllerTemplateButton IHOTASTemplate.mode1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x000AF3E5 File Offset: 0x000AD7E5
		IControllerTemplateButton IHOTASTemplate.mode2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(45);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x000AF3EF File Offset: 0x000AD7EF
		IControllerTemplateButton IHOTASTemplate.mode3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(46);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x000AF3F9 File Offset: 0x000AD7F9
		IControllerTemplateButton IHOTASTemplate.throttleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(50);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x000AF403 File Offset: 0x000AD803
		IControllerTemplateButton IHOTASTemplate.throttleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(51);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x000AF40D File Offset: 0x000AD80D
		IControllerTemplateButton IHOTASTemplate.throttleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(52);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x000AF417 File Offset: 0x000AD817
		IControllerTemplateButton IHOTASTemplate.throttleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(53);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x000AF421 File Offset: 0x000AD821
		IControllerTemplateButton IHOTASTemplate.throttleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(54);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x000AF42B File Offset: 0x000AD82B
		IControllerTemplateButton IHOTASTemplate.throttleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x000AF435 File Offset: 0x000AD835
		IControllerTemplateButton IHOTASTemplate.throttleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x000AF43F File Offset: 0x000AD83F
		IControllerTemplateButton IHOTASTemplate.throttleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x000AF449 File Offset: 0x000AD849
		IControllerTemplateButton IHOTASTemplate.throttleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x000AF453 File Offset: 0x000AD853
		IControllerTemplateButton IHOTASTemplate.throttleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x000AF45D File Offset: 0x000AD85D
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x000AF467 File Offset: 0x000AD867
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x000AF471 File Offset: 0x000AD871
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x000AF47B File Offset: 0x000AD87B
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x000AF485 File Offset: 0x000AD885
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x000AF48F File Offset: 0x000AD88F
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x000AF499 File Offset: 0x000AD899
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x000AF4A3 File Offset: 0x000AD8A3
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(67);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x000AF4AD File Offset: 0x000AD8AD
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(68);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x000AF4B7 File Offset: 0x000AD8B7
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(69);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x000AF4C1 File Offset: 0x000AD8C1
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(132);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x000AF4CE File Offset: 0x000AD8CE
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(133);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x000AF4DB File Offset: 0x000AD8DB
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(134);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x000AF4E8 File Offset: 0x000AD8E8
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(135);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x000AF4F5 File Offset: 0x000AD8F5
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(136);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x000AF502 File Offset: 0x000AD902
		IControllerTemplateAxis IHOTASTemplate.throttleSlider1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(70);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x000AF50C File Offset: 0x000AD90C
		IControllerTemplateAxis IHOTASTemplate.throttleSlider2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(71);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x000AF516 File Offset: 0x000AD916
		IControllerTemplateAxis IHOTASTemplate.throttleSlider3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(72);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x000AF520 File Offset: 0x000AD920
		IControllerTemplateAxis IHOTASTemplate.throttleSlider4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(73);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x000AF52A File Offset: 0x000AD92A
		IControllerTemplateAxis IHOTASTemplate.throttleDial1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(74);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x000AF534 File Offset: 0x000AD934
		IControllerTemplateAxis IHOTASTemplate.throttleDial2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(142);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x000AF541 File Offset: 0x000AD941
		IControllerTemplateAxis IHOTASTemplate.throttleDial3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(143);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x000AF54E File Offset: 0x000AD94E
		IControllerTemplateAxis IHOTASTemplate.throttleDial4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(144);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x000AF55B File Offset: 0x000AD95B
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(145);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x000AF568 File Offset: 0x000AD968
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(146);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x000AF575 File Offset: 0x000AD975
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(147);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x000AF582 File Offset: 0x000AD982
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(148);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x000AF58F File Offset: 0x000AD98F
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(149);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x000AF59C File Offset: 0x000AD99C
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(150);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x000AF5A9 File Offset: 0x000AD9A9
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(151);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x000AF5B6 File Offset: 0x000AD9B6
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(152);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x000AF5C3 File Offset: 0x000AD9C3
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(153);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x000AF5D0 File Offset: 0x000AD9D0
		IControllerTemplateAxis IHOTASTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(168);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x000AF5DD File Offset: 0x000AD9DD
		IControllerTemplateAxis IHOTASTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(169);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x000AF5EA File Offset: 0x000AD9EA
		IControllerTemplateAxis IHOTASTemplate.slidePedals
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(170);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x000AF5F7 File Offset: 0x000AD9F7
		IControllerTemplateStick IHOTASTemplate.stick
		{
			get
			{
				return base.GetElement<IControllerTemplateStick>(171);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x000AF604 File Offset: 0x000ADA04
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick1
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(172);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x000AF611 File Offset: 0x000ADA11
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick2
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(173);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x000AF61E File Offset: 0x000ADA1E
		IControllerTemplateHat IHOTASTemplate.stickHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(174);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x000AF62B File Offset: 0x000ADA2B
		IControllerTemplateHat IHOTASTemplate.stickHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(175);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x000AF638 File Offset: 0x000ADA38
		IControllerTemplateHat IHOTASTemplate.stickHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(176);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x000AF645 File Offset: 0x000ADA45
		IControllerTemplateHat IHOTASTemplate.stickHat4
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(177);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x000AF652 File Offset: 0x000ADA52
		IControllerTemplateThrottle IHOTASTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(178);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x000AF65F File Offset: 0x000ADA5F
		IControllerTemplateThrottle IHOTASTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(179);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x000AF66C File Offset: 0x000ADA6C
		IControllerTemplateThumbStick IHOTASTemplate.throttleMiniStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(180);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x000AF679 File Offset: 0x000ADA79
		IControllerTemplateHat IHOTASTemplate.throttleHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(181);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x000AF686 File Offset: 0x000ADA86
		IControllerTemplateHat IHOTASTemplate.throttleHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(182);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x000AF693 File Offset: 0x000ADA93
		IControllerTemplateHat IHOTASTemplate.throttleHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(183);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x000AF6A0 File Offset: 0x000ADAA0
		IControllerTemplateHat IHOTASTemplate.throttleHat4
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(184);
			}
		}

		// Token: 0x04001EE2 RID: 7906
		public static readonly Guid typeGuid = new Guid("061a00cf-d8c2-4f8d-8cb5-a15a010bc53e");

		// Token: 0x04001EE3 RID: 7907
		public const int elementId_stickX = 0;

		// Token: 0x04001EE4 RID: 7908
		public const int elementId_stickY = 1;

		// Token: 0x04001EE5 RID: 7909
		public const int elementId_stickRotate = 2;

		// Token: 0x04001EE6 RID: 7910
		public const int elementId_stickMiniStick1X = 78;

		// Token: 0x04001EE7 RID: 7911
		public const int elementId_stickMiniStick1Y = 79;

		// Token: 0x04001EE8 RID: 7912
		public const int elementId_stickMiniStick1Press = 80;

		// Token: 0x04001EE9 RID: 7913
		public const int elementId_stickMiniStick2X = 81;

		// Token: 0x04001EEA RID: 7914
		public const int elementId_stickMiniStick2Y = 82;

		// Token: 0x04001EEB RID: 7915
		public const int elementId_stickMiniStick2Press = 83;

		// Token: 0x04001EEC RID: 7916
		public const int elementId_stickTrigger = 3;

		// Token: 0x04001EED RID: 7917
		public const int elementId_stickTriggerStage2 = 4;

		// Token: 0x04001EEE RID: 7918
		public const int elementId_stickPinkyButton = 5;

		// Token: 0x04001EEF RID: 7919
		public const int elementId_stickPinkyTrigger = 154;

		// Token: 0x04001EF0 RID: 7920
		public const int elementId_stickButton1 = 6;

		// Token: 0x04001EF1 RID: 7921
		public const int elementId_stickButton2 = 7;

		// Token: 0x04001EF2 RID: 7922
		public const int elementId_stickButton3 = 8;

		// Token: 0x04001EF3 RID: 7923
		public const int elementId_stickButton4 = 9;

		// Token: 0x04001EF4 RID: 7924
		public const int elementId_stickButton5 = 10;

		// Token: 0x04001EF5 RID: 7925
		public const int elementId_stickButton6 = 11;

		// Token: 0x04001EF6 RID: 7926
		public const int elementId_stickButton7 = 12;

		// Token: 0x04001EF7 RID: 7927
		public const int elementId_stickButton8 = 13;

		// Token: 0x04001EF8 RID: 7928
		public const int elementId_stickButton9 = 14;

		// Token: 0x04001EF9 RID: 7929
		public const int elementId_stickButton10 = 15;

		// Token: 0x04001EFA RID: 7930
		public const int elementId_stickBaseButton1 = 18;

		// Token: 0x04001EFB RID: 7931
		public const int elementId_stickBaseButton2 = 19;

		// Token: 0x04001EFC RID: 7932
		public const int elementId_stickBaseButton3 = 20;

		// Token: 0x04001EFD RID: 7933
		public const int elementId_stickBaseButton4 = 21;

		// Token: 0x04001EFE RID: 7934
		public const int elementId_stickBaseButton5 = 22;

		// Token: 0x04001EFF RID: 7935
		public const int elementId_stickBaseButton6 = 23;

		// Token: 0x04001F00 RID: 7936
		public const int elementId_stickBaseButton7 = 24;

		// Token: 0x04001F01 RID: 7937
		public const int elementId_stickBaseButton8 = 25;

		// Token: 0x04001F02 RID: 7938
		public const int elementId_stickBaseButton9 = 26;

		// Token: 0x04001F03 RID: 7939
		public const int elementId_stickBaseButton10 = 27;

		// Token: 0x04001F04 RID: 7940
		public const int elementId_stickBaseButton11 = 161;

		// Token: 0x04001F05 RID: 7941
		public const int elementId_stickBaseButton12 = 162;

		// Token: 0x04001F06 RID: 7942
		public const int elementId_stickHat1Up = 28;

		// Token: 0x04001F07 RID: 7943
		public const int elementId_stickHat1UpRight = 29;

		// Token: 0x04001F08 RID: 7944
		public const int elementId_stickHat1Right = 30;

		// Token: 0x04001F09 RID: 7945
		public const int elementId_stickHat1DownRight = 31;

		// Token: 0x04001F0A RID: 7946
		public const int elementId_stickHat1Down = 32;

		// Token: 0x04001F0B RID: 7947
		public const int elementId_stickHat1DownLeft = 33;

		// Token: 0x04001F0C RID: 7948
		public const int elementId_stickHat1Left = 34;

		// Token: 0x04001F0D RID: 7949
		public const int elementId_stickHat1Up_Left = 35;

		// Token: 0x04001F0E RID: 7950
		public const int elementId_stickHat2Up = 36;

		// Token: 0x04001F0F RID: 7951
		public const int elementId_stickHat2Up_right = 37;

		// Token: 0x04001F10 RID: 7952
		public const int elementId_stickHat2Right = 38;

		// Token: 0x04001F11 RID: 7953
		public const int elementId_stickHat2Down_Right = 39;

		// Token: 0x04001F12 RID: 7954
		public const int elementId_stickHat2Down = 40;

		// Token: 0x04001F13 RID: 7955
		public const int elementId_stickHat2Down_Left = 41;

		// Token: 0x04001F14 RID: 7956
		public const int elementId_stickHat2Left = 42;

		// Token: 0x04001F15 RID: 7957
		public const int elementId_stickHat2Up_Left = 43;

		// Token: 0x04001F16 RID: 7958
		public const int elementId_stickHat3Up = 84;

		// Token: 0x04001F17 RID: 7959
		public const int elementId_stickHat3Up_Right = 85;

		// Token: 0x04001F18 RID: 7960
		public const int elementId_stickHat3Right = 86;

		// Token: 0x04001F19 RID: 7961
		public const int elementId_stickHat3Down_Right = 87;

		// Token: 0x04001F1A RID: 7962
		public const int elementId_stickHat3Down = 88;

		// Token: 0x04001F1B RID: 7963
		public const int elementId_stickHat3Down_Left = 89;

		// Token: 0x04001F1C RID: 7964
		public const int elementId_stickHat3Left = 90;

		// Token: 0x04001F1D RID: 7965
		public const int elementId_stickHat3Up_Left = 91;

		// Token: 0x04001F1E RID: 7966
		public const int elementId_stickHat4Up = 92;

		// Token: 0x04001F1F RID: 7967
		public const int elementId_stickHat4Up_Right = 93;

		// Token: 0x04001F20 RID: 7968
		public const int elementId_stickHat4Right = 94;

		// Token: 0x04001F21 RID: 7969
		public const int elementId_stickHat4Down_Right = 95;

		// Token: 0x04001F22 RID: 7970
		public const int elementId_stickHat4Down = 96;

		// Token: 0x04001F23 RID: 7971
		public const int elementId_stickHat4Down_Left = 97;

		// Token: 0x04001F24 RID: 7972
		public const int elementId_stickHat4Left = 98;

		// Token: 0x04001F25 RID: 7973
		public const int elementId_stickHat4Up_Left = 99;

		// Token: 0x04001F26 RID: 7974
		public const int elementId_mode1 = 44;

		// Token: 0x04001F27 RID: 7975
		public const int elementId_mode2 = 45;

		// Token: 0x04001F28 RID: 7976
		public const int elementId_mode3 = 46;

		// Token: 0x04001F29 RID: 7977
		public const int elementId_throttle1Axis = 49;

		// Token: 0x04001F2A RID: 7978
		public const int elementId_throttle2Axis = 155;

		// Token: 0x04001F2B RID: 7979
		public const int elementId_throttle1MinDetent = 166;

		// Token: 0x04001F2C RID: 7980
		public const int elementId_throttle2MinDetent = 167;

		// Token: 0x04001F2D RID: 7981
		public const int elementId_throttleButton1 = 50;

		// Token: 0x04001F2E RID: 7982
		public const int elementId_throttleButton2 = 51;

		// Token: 0x04001F2F RID: 7983
		public const int elementId_throttleButton3 = 52;

		// Token: 0x04001F30 RID: 7984
		public const int elementId_throttleButton4 = 53;

		// Token: 0x04001F31 RID: 7985
		public const int elementId_throttleButton5 = 54;

		// Token: 0x04001F32 RID: 7986
		public const int elementId_throttleButton6 = 55;

		// Token: 0x04001F33 RID: 7987
		public const int elementId_throttleButton7 = 56;

		// Token: 0x04001F34 RID: 7988
		public const int elementId_throttleButton8 = 57;

		// Token: 0x04001F35 RID: 7989
		public const int elementId_throttleButton9 = 58;

		// Token: 0x04001F36 RID: 7990
		public const int elementId_throttleButton10 = 59;

		// Token: 0x04001F37 RID: 7991
		public const int elementId_throttleBaseButton1 = 60;

		// Token: 0x04001F38 RID: 7992
		public const int elementId_throttleBaseButton2 = 61;

		// Token: 0x04001F39 RID: 7993
		public const int elementId_throttleBaseButton3 = 62;

		// Token: 0x04001F3A RID: 7994
		public const int elementId_throttleBaseButton4 = 63;

		// Token: 0x04001F3B RID: 7995
		public const int elementId_throttleBaseButton5 = 64;

		// Token: 0x04001F3C RID: 7996
		public const int elementId_throttleBaseButton6 = 65;

		// Token: 0x04001F3D RID: 7997
		public const int elementId_throttleBaseButton7 = 66;

		// Token: 0x04001F3E RID: 7998
		public const int elementId_throttleBaseButton8 = 67;

		// Token: 0x04001F3F RID: 7999
		public const int elementId_throttleBaseButton9 = 68;

		// Token: 0x04001F40 RID: 8000
		public const int elementId_throttleBaseButton10 = 69;

		// Token: 0x04001F41 RID: 8001
		public const int elementId_throttleBaseButton11 = 132;

		// Token: 0x04001F42 RID: 8002
		public const int elementId_throttleBaseButton12 = 133;

		// Token: 0x04001F43 RID: 8003
		public const int elementId_throttleBaseButton13 = 134;

		// Token: 0x04001F44 RID: 8004
		public const int elementId_throttleBaseButton14 = 135;

		// Token: 0x04001F45 RID: 8005
		public const int elementId_throttleBaseButton15 = 136;

		// Token: 0x04001F46 RID: 8006
		public const int elementId_throttleSlider1 = 70;

		// Token: 0x04001F47 RID: 8007
		public const int elementId_throttleSlider2 = 71;

		// Token: 0x04001F48 RID: 8008
		public const int elementId_throttleSlider3 = 72;

		// Token: 0x04001F49 RID: 8009
		public const int elementId_throttleSlider4 = 73;

		// Token: 0x04001F4A RID: 8010
		public const int elementId_throttleDial1 = 74;

		// Token: 0x04001F4B RID: 8011
		public const int elementId_throttleDial2 = 142;

		// Token: 0x04001F4C RID: 8012
		public const int elementId_throttleDial3 = 143;

		// Token: 0x04001F4D RID: 8013
		public const int elementId_throttleDial4 = 144;

		// Token: 0x04001F4E RID: 8014
		public const int elementId_throttleMiniStickX = 75;

		// Token: 0x04001F4F RID: 8015
		public const int elementId_throttleMiniStickY = 76;

		// Token: 0x04001F50 RID: 8016
		public const int elementId_throttleMiniStickPress = 77;

		// Token: 0x04001F51 RID: 8017
		public const int elementId_throttleWheel1Forward = 145;

		// Token: 0x04001F52 RID: 8018
		public const int elementId_throttleWheel1Back = 146;

		// Token: 0x04001F53 RID: 8019
		public const int elementId_throttleWheel1Press = 147;

		// Token: 0x04001F54 RID: 8020
		public const int elementId_throttleWheel2Forward = 148;

		// Token: 0x04001F55 RID: 8021
		public const int elementId_throttleWheel2Back = 149;

		// Token: 0x04001F56 RID: 8022
		public const int elementId_throttleWheel2Press = 150;

		// Token: 0x04001F57 RID: 8023
		public const int elementId_throttleWheel3Forward = 151;

		// Token: 0x04001F58 RID: 8024
		public const int elementId_throttleWheel3Back = 152;

		// Token: 0x04001F59 RID: 8025
		public const int elementId_throttleWheel3Press = 153;

		// Token: 0x04001F5A RID: 8026
		public const int elementId_throttleHat1Up = 100;

		// Token: 0x04001F5B RID: 8027
		public const int elementId_throttleHat1Up_Right = 101;

		// Token: 0x04001F5C RID: 8028
		public const int elementId_throttleHat1Right = 102;

		// Token: 0x04001F5D RID: 8029
		public const int elementId_throttleHat1Down_Right = 103;

		// Token: 0x04001F5E RID: 8030
		public const int elementId_throttleHat1Down = 104;

		// Token: 0x04001F5F RID: 8031
		public const int elementId_throttleHat1Down_Left = 105;

		// Token: 0x04001F60 RID: 8032
		public const int elementId_throttleHat1Left = 106;

		// Token: 0x04001F61 RID: 8033
		public const int elementId_throttleHat1Up_Left = 107;

		// Token: 0x04001F62 RID: 8034
		public const int elementId_throttleHat2Up = 108;

		// Token: 0x04001F63 RID: 8035
		public const int elementId_throttleHat2Up_Right = 109;

		// Token: 0x04001F64 RID: 8036
		public const int elementId_throttleHat2Right = 110;

		// Token: 0x04001F65 RID: 8037
		public const int elementId_throttleHat2Down_Right = 111;

		// Token: 0x04001F66 RID: 8038
		public const int elementId_throttleHat2Down = 112;

		// Token: 0x04001F67 RID: 8039
		public const int elementId_throttleHat2Down_Left = 113;

		// Token: 0x04001F68 RID: 8040
		public const int elementId_throttleHat2Left = 114;

		// Token: 0x04001F69 RID: 8041
		public const int elementId_throttleHat2Up_Left = 115;

		// Token: 0x04001F6A RID: 8042
		public const int elementId_throttleHat3Up = 116;

		// Token: 0x04001F6B RID: 8043
		public const int elementId_throttleHat3Up_Right = 117;

		// Token: 0x04001F6C RID: 8044
		public const int elementId_throttleHat3Right = 118;

		// Token: 0x04001F6D RID: 8045
		public const int elementId_throttleHat3Down_Right = 119;

		// Token: 0x04001F6E RID: 8046
		public const int elementId_throttleHat3Down = 120;

		// Token: 0x04001F6F RID: 8047
		public const int elementId_throttleHat3Down_Left = 121;

		// Token: 0x04001F70 RID: 8048
		public const int elementId_throttleHat3Left = 122;

		// Token: 0x04001F71 RID: 8049
		public const int elementId_throttleHat3Up_Left = 123;

		// Token: 0x04001F72 RID: 8050
		public const int elementId_throttleHat4Up = 124;

		// Token: 0x04001F73 RID: 8051
		public const int elementId_throttleHat4Up_Right = 125;

		// Token: 0x04001F74 RID: 8052
		public const int elementId_throttleHat4Right = 126;

		// Token: 0x04001F75 RID: 8053
		public const int elementId_throttleHat4Down_Right = 127;

		// Token: 0x04001F76 RID: 8054
		public const int elementId_throttleHat4Down = 128;

		// Token: 0x04001F77 RID: 8055
		public const int elementId_throttleHat4Down_Left = 129;

		// Token: 0x04001F78 RID: 8056
		public const int elementId_throttleHat4Left = 130;

		// Token: 0x04001F79 RID: 8057
		public const int elementId_throttleHat4Up_Left = 131;

		// Token: 0x04001F7A RID: 8058
		public const int elementId_leftPedal = 168;

		// Token: 0x04001F7B RID: 8059
		public const int elementId_rightPedal = 169;

		// Token: 0x04001F7C RID: 8060
		public const int elementId_slidePedals = 170;

		// Token: 0x04001F7D RID: 8061
		public const int elementId_stick = 171;

		// Token: 0x04001F7E RID: 8062
		public const int elementId_stickMiniStick1 = 172;

		// Token: 0x04001F7F RID: 8063
		public const int elementId_stickMiniStick2 = 173;

		// Token: 0x04001F80 RID: 8064
		public const int elementId_stickHat1 = 174;

		// Token: 0x04001F81 RID: 8065
		public const int elementId_stickHat2 = 175;

		// Token: 0x04001F82 RID: 8066
		public const int elementId_stickHat3 = 176;

		// Token: 0x04001F83 RID: 8067
		public const int elementId_stickHat4 = 177;

		// Token: 0x04001F84 RID: 8068
		public const int elementId_throttle1 = 178;

		// Token: 0x04001F85 RID: 8069
		public const int elementId_throttle2 = 179;

		// Token: 0x04001F86 RID: 8070
		public const int elementId_throttleMiniStick = 180;

		// Token: 0x04001F87 RID: 8071
		public const int elementId_throttleHat1 = 181;

		// Token: 0x04001F88 RID: 8072
		public const int elementId_throttleHat2 = 182;

		// Token: 0x04001F89 RID: 8073
		public const int elementId_throttleHat3 = 183;

		// Token: 0x04001F8A RID: 8074
		public const int elementId_throttleHat4 = 184;
	}
}
