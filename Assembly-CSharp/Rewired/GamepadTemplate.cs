using System;

namespace Rewired
{
	// Token: 0x0200057D RID: 1405
	public sealed class GamepadTemplate : ControllerTemplate, IGamepadTemplate, IControllerTemplate
	{
		// Token: 0x06001F08 RID: 7944 RVA: 0x000AEFE4 File Offset: 0x000AD3E4
		public GamepadTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x000AEFED File Offset: 0x000AD3ED
		IControllerTemplateButton IGamepadTemplate.actionBottomRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x000AEFF6 File Offset: 0x000AD3F6
		IControllerTemplateButton IGamepadTemplate.a
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x000AEFFF File Offset: 0x000AD3FF
		IControllerTemplateButton IGamepadTemplate.actionBottomRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x000AF008 File Offset: 0x000AD408
		IControllerTemplateButton IGamepadTemplate.b
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x000AF011 File Offset: 0x000AD411
		IControllerTemplateButton IGamepadTemplate.actionBottomRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x000AF01A File Offset: 0x000AD41A
		IControllerTemplateButton IGamepadTemplate.c
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x000AF023 File Offset: 0x000AD423
		IControllerTemplateButton IGamepadTemplate.actionTopRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x000AF02C File Offset: 0x000AD42C
		IControllerTemplateButton IGamepadTemplate.x
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x000AF035 File Offset: 0x000AD435
		IControllerTemplateButton IGamepadTemplate.actionTopRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x000AF03E File Offset: 0x000AD43E
		IControllerTemplateButton IGamepadTemplate.y
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x000AF047 File Offset: 0x000AD447
		IControllerTemplateButton IGamepadTemplate.actionTopRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x000AF051 File Offset: 0x000AD451
		IControllerTemplateButton IGamepadTemplate.z
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x000AF05B File Offset: 0x000AD45B
		IControllerTemplateButton IGamepadTemplate.leftShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x000AF065 File Offset: 0x000AD465
		IControllerTemplateButton IGamepadTemplate.leftBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x000AF06F File Offset: 0x000AD46F
		IControllerTemplateAxis IGamepadTemplate.leftShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x000AF079 File Offset: 0x000AD479
		IControllerTemplateAxis IGamepadTemplate.leftTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x000AF083 File Offset: 0x000AD483
		IControllerTemplateButton IGamepadTemplate.rightShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x000AF08D File Offset: 0x000AD48D
		IControllerTemplateButton IGamepadTemplate.rightBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x000AF097 File Offset: 0x000AD497
		IControllerTemplateAxis IGamepadTemplate.rightShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x000AF0A1 File Offset: 0x000AD4A1
		IControllerTemplateAxis IGamepadTemplate.rightTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x000AF0AB File Offset: 0x000AD4AB
		IControllerTemplateButton IGamepadTemplate.center1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x000AF0B5 File Offset: 0x000AD4B5
		IControllerTemplateButton IGamepadTemplate.back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x000AF0BF File Offset: 0x000AD4BF
		IControllerTemplateButton IGamepadTemplate.center2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x000AF0C9 File Offset: 0x000AD4C9
		IControllerTemplateButton IGamepadTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x000AF0D3 File Offset: 0x000AD4D3
		IControllerTemplateButton IGamepadTemplate.center3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x000AF0DD File Offset: 0x000AD4DD
		IControllerTemplateButton IGamepadTemplate.guide
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x000AF0E7 File Offset: 0x000AD4E7
		IControllerTemplateThumbStick IGamepadTemplate.leftStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(23);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x000AF0F1 File Offset: 0x000AD4F1
		IControllerTemplateThumbStick IGamepadTemplate.rightStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(24);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x000AF0FB File Offset: 0x000AD4FB
		IControllerTemplateDPad IGamepadTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(25);
			}
		}

		// Token: 0x04001E8B RID: 7819
		public static readonly Guid typeGuid = new Guid("83b427e4-086f-47f3-bb06-be266abd1ca5");

		// Token: 0x04001E8C RID: 7820
		public const int elementId_leftStickX = 0;

		// Token: 0x04001E8D RID: 7821
		public const int elementId_leftStickY = 1;

		// Token: 0x04001E8E RID: 7822
		public const int elementId_rightStickX = 2;

		// Token: 0x04001E8F RID: 7823
		public const int elementId_rightStickY = 3;

		// Token: 0x04001E90 RID: 7824
		public const int elementId_actionBottomRow1 = 4;

		// Token: 0x04001E91 RID: 7825
		public const int elementId_a = 4;

		// Token: 0x04001E92 RID: 7826
		public const int elementId_actionBottomRow2 = 5;

		// Token: 0x04001E93 RID: 7827
		public const int elementId_b = 5;

		// Token: 0x04001E94 RID: 7828
		public const int elementId_actionBottomRow3 = 6;

		// Token: 0x04001E95 RID: 7829
		public const int elementId_c = 6;

		// Token: 0x04001E96 RID: 7830
		public const int elementId_actionTopRow1 = 7;

		// Token: 0x04001E97 RID: 7831
		public const int elementId_x = 7;

		// Token: 0x04001E98 RID: 7832
		public const int elementId_actionTopRow2 = 8;

		// Token: 0x04001E99 RID: 7833
		public const int elementId_y = 8;

		// Token: 0x04001E9A RID: 7834
		public const int elementId_actionTopRow3 = 9;

		// Token: 0x04001E9B RID: 7835
		public const int elementId_z = 9;

		// Token: 0x04001E9C RID: 7836
		public const int elementId_leftShoulder1 = 10;

		// Token: 0x04001E9D RID: 7837
		public const int elementId_leftBumper = 10;

		// Token: 0x04001E9E RID: 7838
		public const int elementId_leftShoulder2 = 11;

		// Token: 0x04001E9F RID: 7839
		public const int elementId_leftTrigger = 11;

		// Token: 0x04001EA0 RID: 7840
		public const int elementId_rightShoulder1 = 12;

		// Token: 0x04001EA1 RID: 7841
		public const int elementId_rightBumper = 12;

		// Token: 0x04001EA2 RID: 7842
		public const int elementId_rightShoulder2 = 13;

		// Token: 0x04001EA3 RID: 7843
		public const int elementId_rightTrigger = 13;

		// Token: 0x04001EA4 RID: 7844
		public const int elementId_center1 = 14;

		// Token: 0x04001EA5 RID: 7845
		public const int elementId_back = 14;

		// Token: 0x04001EA6 RID: 7846
		public const int elementId_center2 = 15;

		// Token: 0x04001EA7 RID: 7847
		public const int elementId_start = 15;

		// Token: 0x04001EA8 RID: 7848
		public const int elementId_center3 = 16;

		// Token: 0x04001EA9 RID: 7849
		public const int elementId_guide = 16;

		// Token: 0x04001EAA RID: 7850
		public const int elementId_leftStickButton = 17;

		// Token: 0x04001EAB RID: 7851
		public const int elementId_rightStickButton = 18;

		// Token: 0x04001EAC RID: 7852
		public const int elementId_dPadUp = 19;

		// Token: 0x04001EAD RID: 7853
		public const int elementId_dPadRight = 20;

		// Token: 0x04001EAE RID: 7854
		public const int elementId_dPadDown = 21;

		// Token: 0x04001EAF RID: 7855
		public const int elementId_dPadLeft = 22;

		// Token: 0x04001EB0 RID: 7856
		public const int elementId_leftStick = 23;

		// Token: 0x04001EB1 RID: 7857
		public const int elementId_rightStick = 24;

		// Token: 0x04001EB2 RID: 7858
		public const int elementId_dPad = 25;
	}
}
