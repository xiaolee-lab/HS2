using System;

namespace Rewired
{
	// Token: 0x02000580 RID: 1408
	public sealed class FlightYokeTemplate : ControllerTemplate, IFlightYokeTemplate, IControllerTemplate
	{
		// Token: 0x06001FAD RID: 8109 RVA: 0x000AF6BE File Offset: 0x000ADABE
		public FlightYokeTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x000AF6C7 File Offset: 0x000ADAC7
		IControllerTemplateButton IFlightYokeTemplate.leftPaddle
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x000AF6D1 File Offset: 0x000ADAD1
		IControllerTemplateButton IFlightYokeTemplate.rightPaddle
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x000AF6DB File Offset: 0x000ADADB
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x000AF6E4 File Offset: 0x000ADAE4
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x000AF6ED File Offset: 0x000ADAED
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x000AF6F7 File Offset: 0x000ADAF7
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x000AF701 File Offset: 0x000ADB01
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x000AF70B File Offset: 0x000ADB0B
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x000AF715 File Offset: 0x000ADB15
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x000AF71F File Offset: 0x000ADB1F
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x000AF729 File Offset: 0x000ADB29
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x000AF733 File Offset: 0x000ADB33
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x000AF73D File Offset: 0x000ADB3D
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x000AF747 File Offset: 0x000ADB47
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x000AF751 File Offset: 0x000ADB51
		IControllerTemplateButton IFlightYokeTemplate.centerButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x000AF75B File Offset: 0x000ADB5B
		IControllerTemplateButton IFlightYokeTemplate.centerButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x000AF765 File Offset: 0x000ADB65
		IControllerTemplateButton IFlightYokeTemplate.centerButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x000AF76F File Offset: 0x000ADB6F
		IControllerTemplateButton IFlightYokeTemplate.centerButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x000AF779 File Offset: 0x000ADB79
		IControllerTemplateButton IFlightYokeTemplate.centerButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x000AF783 File Offset: 0x000ADB83
		IControllerTemplateButton IFlightYokeTemplate.centerButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x000AF78D File Offset: 0x000ADB8D
		IControllerTemplateButton IFlightYokeTemplate.centerButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x000AF797 File Offset: 0x000ADB97
		IControllerTemplateButton IFlightYokeTemplate.centerButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x000AF7A1 File Offset: 0x000ADBA1
		IControllerTemplateButton IFlightYokeTemplate.wheel1Up
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(53);
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x000AF7AB File Offset: 0x000ADBAB
		IControllerTemplateButton IFlightYokeTemplate.wheel1Down
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(54);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x000AF7B5 File Offset: 0x000ADBB5
		IControllerTemplateButton IFlightYokeTemplate.wheel1Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x000AF7BF File Offset: 0x000ADBBF
		IControllerTemplateButton IFlightYokeTemplate.wheel2Up
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x000AF7C9 File Offset: 0x000ADBC9
		IControllerTemplateButton IFlightYokeTemplate.wheel2Down
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x000AF7D3 File Offset: 0x000ADBD3
		IControllerTemplateButton IFlightYokeTemplate.wheel2Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x000AF7DD File Offset: 0x000ADBDD
		IControllerTemplateButton IFlightYokeTemplate.consoleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(43);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x000AF7E7 File Offset: 0x000ADBE7
		IControllerTemplateButton IFlightYokeTemplate.consoleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x000AF7F1 File Offset: 0x000ADBF1
		IControllerTemplateButton IFlightYokeTemplate.consoleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(45);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x000AF7FB File Offset: 0x000ADBFB
		IControllerTemplateButton IFlightYokeTemplate.consoleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(46);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x000AF805 File Offset: 0x000ADC05
		IControllerTemplateButton IFlightYokeTemplate.consoleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(47);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x000AF80F File Offset: 0x000ADC0F
		IControllerTemplateButton IFlightYokeTemplate.consoleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(48);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x000AF819 File Offset: 0x000ADC19
		IControllerTemplateButton IFlightYokeTemplate.consoleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(49);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x000AF823 File Offset: 0x000ADC23
		IControllerTemplateButton IFlightYokeTemplate.consoleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(50);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x000AF82D File Offset: 0x000ADC2D
		IControllerTemplateButton IFlightYokeTemplate.consoleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(51);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x000AF837 File Offset: 0x000ADC37
		IControllerTemplateButton IFlightYokeTemplate.consoleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(52);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x000AF841 File Offset: 0x000ADC41
		IControllerTemplateButton IFlightYokeTemplate.mode1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x000AF84B File Offset: 0x000ADC4B
		IControllerTemplateButton IFlightYokeTemplate.mode2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x000AF855 File Offset: 0x000ADC55
		IControllerTemplateButton IFlightYokeTemplate.mode3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x000AF85F File Offset: 0x000ADC5F
		IControllerTemplateYoke IFlightYokeTemplate.yoke
		{
			get
			{
				return base.GetElement<IControllerTemplateYoke>(69);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x000AF869 File Offset: 0x000ADC69
		IControllerTemplateThrottle IFlightYokeTemplate.lever1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(70);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x000AF873 File Offset: 0x000ADC73
		IControllerTemplateThrottle IFlightYokeTemplate.lever2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(71);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x000AF87D File Offset: 0x000ADC7D
		IControllerTemplateThrottle IFlightYokeTemplate.lever3
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(72);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x000AF887 File Offset: 0x000ADC87
		IControllerTemplateThrottle IFlightYokeTemplate.lever4
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(73);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x000AF891 File Offset: 0x000ADC91
		IControllerTemplateThrottle IFlightYokeTemplate.lever5
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(74);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x000AF89B File Offset: 0x000ADC9B
		IControllerTemplateHat IFlightYokeTemplate.leftGripHat
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(75);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x000AF8A5 File Offset: 0x000ADCA5
		IControllerTemplateHat IFlightYokeTemplate.rightGripHat
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(76);
			}
		}

		// Token: 0x04001F8B RID: 8075
		public static readonly Guid typeGuid = new Guid("f311fa16-0ccc-41c0-ac4b-50f7100bb8ff");

		// Token: 0x04001F8C RID: 8076
		public const int elementId_rotateYoke = 0;

		// Token: 0x04001F8D RID: 8077
		public const int elementId_yokeZ = 1;

		// Token: 0x04001F8E RID: 8078
		public const int elementId_leftPaddle = 59;

		// Token: 0x04001F8F RID: 8079
		public const int elementId_rightPaddle = 60;

		// Token: 0x04001F90 RID: 8080
		public const int elementId_lever1Axis = 2;

		// Token: 0x04001F91 RID: 8081
		public const int elementId_lever1MinDetent = 64;

		// Token: 0x04001F92 RID: 8082
		public const int elementId_lever2Axis = 3;

		// Token: 0x04001F93 RID: 8083
		public const int elementId_lever2MinDetent = 65;

		// Token: 0x04001F94 RID: 8084
		public const int elementId_lever3Axis = 4;

		// Token: 0x04001F95 RID: 8085
		public const int elementId_lever3MinDetent = 66;

		// Token: 0x04001F96 RID: 8086
		public const int elementId_lever4Axis = 5;

		// Token: 0x04001F97 RID: 8087
		public const int elementId_lever4MinDetent = 67;

		// Token: 0x04001F98 RID: 8088
		public const int elementId_lever5Axis = 6;

		// Token: 0x04001F99 RID: 8089
		public const int elementId_lever5MinDetent = 68;

		// Token: 0x04001F9A RID: 8090
		public const int elementId_leftGripButton1 = 7;

		// Token: 0x04001F9B RID: 8091
		public const int elementId_leftGripButton2 = 8;

		// Token: 0x04001F9C RID: 8092
		public const int elementId_leftGripButton3 = 9;

		// Token: 0x04001F9D RID: 8093
		public const int elementId_leftGripButton4 = 10;

		// Token: 0x04001F9E RID: 8094
		public const int elementId_leftGripButton5 = 11;

		// Token: 0x04001F9F RID: 8095
		public const int elementId_leftGripButton6 = 12;

		// Token: 0x04001FA0 RID: 8096
		public const int elementId_rightGripButton1 = 13;

		// Token: 0x04001FA1 RID: 8097
		public const int elementId_rightGripButton2 = 14;

		// Token: 0x04001FA2 RID: 8098
		public const int elementId_rightGripButton3 = 15;

		// Token: 0x04001FA3 RID: 8099
		public const int elementId_rightGripButton4 = 16;

		// Token: 0x04001FA4 RID: 8100
		public const int elementId_rightGripButton5 = 17;

		// Token: 0x04001FA5 RID: 8101
		public const int elementId_rightGripButton6 = 18;

		// Token: 0x04001FA6 RID: 8102
		public const int elementId_centerButton1 = 19;

		// Token: 0x04001FA7 RID: 8103
		public const int elementId_centerButton2 = 20;

		// Token: 0x04001FA8 RID: 8104
		public const int elementId_centerButton3 = 21;

		// Token: 0x04001FA9 RID: 8105
		public const int elementId_centerButton4 = 22;

		// Token: 0x04001FAA RID: 8106
		public const int elementId_centerButton5 = 23;

		// Token: 0x04001FAB RID: 8107
		public const int elementId_centerButton6 = 24;

		// Token: 0x04001FAC RID: 8108
		public const int elementId_centerButton7 = 25;

		// Token: 0x04001FAD RID: 8109
		public const int elementId_centerButton8 = 26;

		// Token: 0x04001FAE RID: 8110
		public const int elementId_wheel1Up = 53;

		// Token: 0x04001FAF RID: 8111
		public const int elementId_wheel1Down = 54;

		// Token: 0x04001FB0 RID: 8112
		public const int elementId_wheel1Press = 55;

		// Token: 0x04001FB1 RID: 8113
		public const int elementId_wheel2Up = 56;

		// Token: 0x04001FB2 RID: 8114
		public const int elementId_wheel2Down = 57;

		// Token: 0x04001FB3 RID: 8115
		public const int elementId_wheel2Press = 58;

		// Token: 0x04001FB4 RID: 8116
		public const int elementId_leftGripHatUp = 27;

		// Token: 0x04001FB5 RID: 8117
		public const int elementId_leftGripHatUpRight = 28;

		// Token: 0x04001FB6 RID: 8118
		public const int elementId_leftGripHatRight = 29;

		// Token: 0x04001FB7 RID: 8119
		public const int elementId_leftGripHatDownRight = 30;

		// Token: 0x04001FB8 RID: 8120
		public const int elementId_leftGripHatDown = 31;

		// Token: 0x04001FB9 RID: 8121
		public const int elementId_leftGripHatDownLeft = 32;

		// Token: 0x04001FBA RID: 8122
		public const int elementId_leftGripHatLeft = 33;

		// Token: 0x04001FBB RID: 8123
		public const int elementId_leftGripHatUpLeft = 34;

		// Token: 0x04001FBC RID: 8124
		public const int elementId_rightGripHatUp = 35;

		// Token: 0x04001FBD RID: 8125
		public const int elementId_rightGripHatUpRight = 36;

		// Token: 0x04001FBE RID: 8126
		public const int elementId_rightGripHatRight = 37;

		// Token: 0x04001FBF RID: 8127
		public const int elementId_rightGripHatDownRight = 38;

		// Token: 0x04001FC0 RID: 8128
		public const int elementId_rightGripHatDown = 39;

		// Token: 0x04001FC1 RID: 8129
		public const int elementId_rightGripHatDownLeft = 40;

		// Token: 0x04001FC2 RID: 8130
		public const int elementId_rightGripHatLeft = 41;

		// Token: 0x04001FC3 RID: 8131
		public const int elementId_rightGripHatUpLeft = 42;

		// Token: 0x04001FC4 RID: 8132
		public const int elementId_consoleButton1 = 43;

		// Token: 0x04001FC5 RID: 8133
		public const int elementId_consoleButton2 = 44;

		// Token: 0x04001FC6 RID: 8134
		public const int elementId_consoleButton3 = 45;

		// Token: 0x04001FC7 RID: 8135
		public const int elementId_consoleButton4 = 46;

		// Token: 0x04001FC8 RID: 8136
		public const int elementId_consoleButton5 = 47;

		// Token: 0x04001FC9 RID: 8137
		public const int elementId_consoleButton6 = 48;

		// Token: 0x04001FCA RID: 8138
		public const int elementId_consoleButton7 = 49;

		// Token: 0x04001FCB RID: 8139
		public const int elementId_consoleButton8 = 50;

		// Token: 0x04001FCC RID: 8140
		public const int elementId_consoleButton9 = 51;

		// Token: 0x04001FCD RID: 8141
		public const int elementId_consoleButton10 = 52;

		// Token: 0x04001FCE RID: 8142
		public const int elementId_mode1 = 61;

		// Token: 0x04001FCF RID: 8143
		public const int elementId_mode2 = 62;

		// Token: 0x04001FD0 RID: 8144
		public const int elementId_mode3 = 63;

		// Token: 0x04001FD1 RID: 8145
		public const int elementId_yoke = 69;

		// Token: 0x04001FD2 RID: 8146
		public const int elementId_lever1 = 70;

		// Token: 0x04001FD3 RID: 8147
		public const int elementId_lever2 = 71;

		// Token: 0x04001FD4 RID: 8148
		public const int elementId_lever3 = 72;

		// Token: 0x04001FD5 RID: 8149
		public const int elementId_lever4 = 73;

		// Token: 0x04001FD6 RID: 8150
		public const int elementId_lever5 = 74;

		// Token: 0x04001FD7 RID: 8151
		public const int elementId_leftGripHat = 75;

		// Token: 0x04001FD8 RID: 8152
		public const int elementId_rightGripHat = 76;
	}
}
