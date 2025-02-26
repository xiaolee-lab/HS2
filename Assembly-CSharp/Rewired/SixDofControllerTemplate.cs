using System;

namespace Rewired
{
	// Token: 0x02000582 RID: 1410
	public sealed class SixDofControllerTemplate : ControllerTemplate, ISixDofControllerTemplate, IControllerTemplate
	{
		// Token: 0x06001FE5 RID: 8165 RVA: 0x000AF8F5 File Offset: 0x000ADCF5
		public SixDofControllerTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x000AF8FE File Offset: 0x000ADCFE
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(8);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x000AF907 File Offset: 0x000ADD07
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(9);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x000AF911 File Offset: 0x000ADD11
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(10);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x000AF91B File Offset: 0x000ADD1B
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x000AF925 File Offset: 0x000ADD25
		IControllerTemplateButton ISixDofControllerTemplate.button1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x000AF92F File Offset: 0x000ADD2F
		IControllerTemplateButton ISixDofControllerTemplate.button2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x000AF939 File Offset: 0x000ADD39
		IControllerTemplateButton ISixDofControllerTemplate.button3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x000AF943 File Offset: 0x000ADD43
		IControllerTemplateButton ISixDofControllerTemplate.button4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x000AF94D File Offset: 0x000ADD4D
		IControllerTemplateButton ISixDofControllerTemplate.button5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x000AF957 File Offset: 0x000ADD57
		IControllerTemplateButton ISixDofControllerTemplate.button6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x000AF961 File Offset: 0x000ADD61
		IControllerTemplateButton ISixDofControllerTemplate.button7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x000AF96B File Offset: 0x000ADD6B
		IControllerTemplateButton ISixDofControllerTemplate.button8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x000AF975 File Offset: 0x000ADD75
		IControllerTemplateButton ISixDofControllerTemplate.button9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x000AF97F File Offset: 0x000ADD7F
		IControllerTemplateButton ISixDofControllerTemplate.button10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x000AF989 File Offset: 0x000ADD89
		IControllerTemplateButton ISixDofControllerTemplate.button11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x000AF993 File Offset: 0x000ADD93
		IControllerTemplateButton ISixDofControllerTemplate.button12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x000AF99D File Offset: 0x000ADD9D
		IControllerTemplateButton ISixDofControllerTemplate.button13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x000AF9A7 File Offset: 0x000ADDA7
		IControllerTemplateButton ISixDofControllerTemplate.button14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x000AF9B1 File Offset: 0x000ADDB1
		IControllerTemplateButton ISixDofControllerTemplate.button15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x000AF9BB File Offset: 0x000ADDBB
		IControllerTemplateButton ISixDofControllerTemplate.button16
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x000AF9C5 File Offset: 0x000ADDC5
		IControllerTemplateButton ISixDofControllerTemplate.button17
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x000AF9CF File Offset: 0x000ADDCF
		IControllerTemplateButton ISixDofControllerTemplate.button18
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x000AF9D9 File Offset: 0x000ADDD9
		IControllerTemplateButton ISixDofControllerTemplate.button19
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x000AF9E3 File Offset: 0x000ADDE3
		IControllerTemplateButton ISixDofControllerTemplate.button20
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x000AF9ED File Offset: 0x000ADDED
		IControllerTemplateButton ISixDofControllerTemplate.button21
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x000AF9F7 File Offset: 0x000ADDF7
		IControllerTemplateButton ISixDofControllerTemplate.button22
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x000AFA01 File Offset: 0x000ADE01
		IControllerTemplateButton ISixDofControllerTemplate.button23
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x000AFA0B File Offset: 0x000ADE0B
		IControllerTemplateButton ISixDofControllerTemplate.button24
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x000AFA15 File Offset: 0x000ADE15
		IControllerTemplateButton ISixDofControllerTemplate.button25
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x000AFA1F File Offset: 0x000ADE1F
		IControllerTemplateButton ISixDofControllerTemplate.button26
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x000AFA29 File Offset: 0x000ADE29
		IControllerTemplateButton ISixDofControllerTemplate.button27
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x000AFA33 File Offset: 0x000ADE33
		IControllerTemplateButton ISixDofControllerTemplate.button28
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06002006 RID: 8198 RVA: 0x000AFA3D File Offset: 0x000ADE3D
		IControllerTemplateButton ISixDofControllerTemplate.button29
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x000AFA47 File Offset: 0x000ADE47
		IControllerTemplateButton ISixDofControllerTemplate.button30
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x000AFA51 File Offset: 0x000ADE51
		IControllerTemplateButton ISixDofControllerTemplate.button31
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x000AFA5B File Offset: 0x000ADE5B
		IControllerTemplateButton ISixDofControllerTemplate.button32
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x000AFA65 File Offset: 0x000ADE65
		IControllerTemplateHat ISixDofControllerTemplate.hat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(48);
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x000AFA6F File Offset: 0x000ADE6F
		IControllerTemplateHat ISixDofControllerTemplate.hat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(49);
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x000AFA79 File Offset: 0x000ADE79
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(52);
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x000AFA83 File Offset: 0x000ADE83
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(53);
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x000AFA8D File Offset: 0x000ADE8D
		IControllerTemplateStick6D ISixDofControllerTemplate.stick
		{
			get
			{
				return base.GetElement<IControllerTemplateStick6D>(54);
			}
		}

		// Token: 0x04001FDD RID: 8157
		public static readonly Guid typeGuid = new Guid("2599beb3-522b-43dd-a4ef-93fd60e5eafa");

		// Token: 0x04001FDE RID: 8158
		public const int elementId_positionX = 1;

		// Token: 0x04001FDF RID: 8159
		public const int elementId_positionY = 2;

		// Token: 0x04001FE0 RID: 8160
		public const int elementId_positionZ = 0;

		// Token: 0x04001FE1 RID: 8161
		public const int elementId_rotationX = 3;

		// Token: 0x04001FE2 RID: 8162
		public const int elementId_rotationY = 5;

		// Token: 0x04001FE3 RID: 8163
		public const int elementId_rotationZ = 4;

		// Token: 0x04001FE4 RID: 8164
		public const int elementId_throttle1Axis = 6;

		// Token: 0x04001FE5 RID: 8165
		public const int elementId_throttle1MinDetent = 50;

		// Token: 0x04001FE6 RID: 8166
		public const int elementId_throttle2Axis = 7;

		// Token: 0x04001FE7 RID: 8167
		public const int elementId_throttle2MinDetent = 51;

		// Token: 0x04001FE8 RID: 8168
		public const int elementId_extraAxis1 = 8;

		// Token: 0x04001FE9 RID: 8169
		public const int elementId_extraAxis2 = 9;

		// Token: 0x04001FEA RID: 8170
		public const int elementId_extraAxis3 = 10;

		// Token: 0x04001FEB RID: 8171
		public const int elementId_extraAxis4 = 11;

		// Token: 0x04001FEC RID: 8172
		public const int elementId_button1 = 12;

		// Token: 0x04001FED RID: 8173
		public const int elementId_button2 = 13;

		// Token: 0x04001FEE RID: 8174
		public const int elementId_button3 = 14;

		// Token: 0x04001FEF RID: 8175
		public const int elementId_button4 = 15;

		// Token: 0x04001FF0 RID: 8176
		public const int elementId_button5 = 16;

		// Token: 0x04001FF1 RID: 8177
		public const int elementId_button6 = 17;

		// Token: 0x04001FF2 RID: 8178
		public const int elementId_button7 = 18;

		// Token: 0x04001FF3 RID: 8179
		public const int elementId_button8 = 19;

		// Token: 0x04001FF4 RID: 8180
		public const int elementId_button9 = 20;

		// Token: 0x04001FF5 RID: 8181
		public const int elementId_button10 = 21;

		// Token: 0x04001FF6 RID: 8182
		public const int elementId_button11 = 22;

		// Token: 0x04001FF7 RID: 8183
		public const int elementId_button12 = 23;

		// Token: 0x04001FF8 RID: 8184
		public const int elementId_button13 = 24;

		// Token: 0x04001FF9 RID: 8185
		public const int elementId_button14 = 25;

		// Token: 0x04001FFA RID: 8186
		public const int elementId_button15 = 26;

		// Token: 0x04001FFB RID: 8187
		public const int elementId_button16 = 27;

		// Token: 0x04001FFC RID: 8188
		public const int elementId_button17 = 28;

		// Token: 0x04001FFD RID: 8189
		public const int elementId_button18 = 29;

		// Token: 0x04001FFE RID: 8190
		public const int elementId_button19 = 30;

		// Token: 0x04001FFF RID: 8191
		public const int elementId_button20 = 31;

		// Token: 0x04002000 RID: 8192
		public const int elementId_button21 = 55;

		// Token: 0x04002001 RID: 8193
		public const int elementId_button22 = 56;

		// Token: 0x04002002 RID: 8194
		public const int elementId_button23 = 57;

		// Token: 0x04002003 RID: 8195
		public const int elementId_button24 = 58;

		// Token: 0x04002004 RID: 8196
		public const int elementId_button25 = 59;

		// Token: 0x04002005 RID: 8197
		public const int elementId_button26 = 60;

		// Token: 0x04002006 RID: 8198
		public const int elementId_button27 = 61;

		// Token: 0x04002007 RID: 8199
		public const int elementId_button28 = 62;

		// Token: 0x04002008 RID: 8200
		public const int elementId_button29 = 63;

		// Token: 0x04002009 RID: 8201
		public const int elementId_button30 = 64;

		// Token: 0x0400200A RID: 8202
		public const int elementId_button31 = 65;

		// Token: 0x0400200B RID: 8203
		public const int elementId_button32 = 66;

		// Token: 0x0400200C RID: 8204
		public const int elementId_hat1Up = 32;

		// Token: 0x0400200D RID: 8205
		public const int elementId_hat1UpRight = 33;

		// Token: 0x0400200E RID: 8206
		public const int elementId_hat1Right = 34;

		// Token: 0x0400200F RID: 8207
		public const int elementId_hat1DownRight = 35;

		// Token: 0x04002010 RID: 8208
		public const int elementId_hat1Down = 36;

		// Token: 0x04002011 RID: 8209
		public const int elementId_hat1DownLeft = 37;

		// Token: 0x04002012 RID: 8210
		public const int elementId_hat1Left = 38;

		// Token: 0x04002013 RID: 8211
		public const int elementId_hat1UpLeft = 39;

		// Token: 0x04002014 RID: 8212
		public const int elementId_hat2Up = 40;

		// Token: 0x04002015 RID: 8213
		public const int elementId_hat2UpRight = 41;

		// Token: 0x04002016 RID: 8214
		public const int elementId_hat2Right = 42;

		// Token: 0x04002017 RID: 8215
		public const int elementId_hat2DownRight = 43;

		// Token: 0x04002018 RID: 8216
		public const int elementId_hat2Down = 44;

		// Token: 0x04002019 RID: 8217
		public const int elementId_hat2DownLeft = 45;

		// Token: 0x0400201A RID: 8218
		public const int elementId_hat2Left = 46;

		// Token: 0x0400201B RID: 8219
		public const int elementId_hat2UpLeft = 47;

		// Token: 0x0400201C RID: 8220
		public const int elementId_hat1 = 48;

		// Token: 0x0400201D RID: 8221
		public const int elementId_hat2 = 49;

		// Token: 0x0400201E RID: 8222
		public const int elementId_throttle1 = 52;

		// Token: 0x0400201F RID: 8223
		public const int elementId_throttle2 = 53;

		// Token: 0x04002020 RID: 8224
		public const int elementId_stick = 54;
	}
}
