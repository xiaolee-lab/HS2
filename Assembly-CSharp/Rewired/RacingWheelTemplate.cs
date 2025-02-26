using System;

namespace Rewired
{
	// Token: 0x0200057E RID: 1406
	public sealed class RacingWheelTemplate : ControllerTemplate, IRacingWheelTemplate, IControllerTemplate
	{
		// Token: 0x06001F27 RID: 7975 RVA: 0x000AF116 File Offset: 0x000AD516
		public RacingWheelTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x000AF11F File Offset: 0x000AD51F
		IControllerTemplateAxis IRacingWheelTemplate.wheel
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x000AF128 File Offset: 0x000AD528
		IControllerTemplateAxis IRacingWheelTemplate.accelerator
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x000AF131 File Offset: 0x000AD531
		IControllerTemplateAxis IRacingWheelTemplate.brake
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x000AF13A File Offset: 0x000AD53A
		IControllerTemplateAxis IRacingWheelTemplate.clutch
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(3);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x000AF143 File Offset: 0x000AD543
		IControllerTemplateButton IRacingWheelTemplate.shiftDown
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x000AF14C File Offset: 0x000AD54C
		IControllerTemplateButton IRacingWheelTemplate.shiftUp
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x000AF155 File Offset: 0x000AD555
		IControllerTemplateButton IRacingWheelTemplate.wheelButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x000AF15E File Offset: 0x000AD55E
		IControllerTemplateButton IRacingWheelTemplate.wheelButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x000AF167 File Offset: 0x000AD567
		IControllerTemplateButton IRacingWheelTemplate.wheelButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x000AF170 File Offset: 0x000AD570
		IControllerTemplateButton IRacingWheelTemplate.wheelButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x000AF17A File Offset: 0x000AD57A
		IControllerTemplateButton IRacingWheelTemplate.wheelButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x000AF184 File Offset: 0x000AD584
		IControllerTemplateButton IRacingWheelTemplate.wheelButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x000AF18E File Offset: 0x000AD58E
		IControllerTemplateButton IRacingWheelTemplate.wheelButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x000AF198 File Offset: 0x000AD598
		IControllerTemplateButton IRacingWheelTemplate.wheelButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x000AF1A2 File Offset: 0x000AD5A2
		IControllerTemplateButton IRacingWheelTemplate.wheelButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x000AF1AC File Offset: 0x000AD5AC
		IControllerTemplateButton IRacingWheelTemplate.wheelButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x000AF1B6 File Offset: 0x000AD5B6
		IControllerTemplateButton IRacingWheelTemplate.consoleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x000AF1C0 File Offset: 0x000AD5C0
		IControllerTemplateButton IRacingWheelTemplate.consoleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x000AF1CA File Offset: 0x000AD5CA
		IControllerTemplateButton IRacingWheelTemplate.consoleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x000AF1D4 File Offset: 0x000AD5D4
		IControllerTemplateButton IRacingWheelTemplate.consoleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x000AF1DE File Offset: 0x000AD5DE
		IControllerTemplateButton IRacingWheelTemplate.consoleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x000AF1E8 File Offset: 0x000AD5E8
		IControllerTemplateButton IRacingWheelTemplate.consoleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x000AF1F2 File Offset: 0x000AD5F2
		IControllerTemplateButton IRacingWheelTemplate.consoleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x000AF1FC File Offset: 0x000AD5FC
		IControllerTemplateButton IRacingWheelTemplate.consoleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x000AF206 File Offset: 0x000AD606
		IControllerTemplateButton IRacingWheelTemplate.consoleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x000AF210 File Offset: 0x000AD610
		IControllerTemplateButton IRacingWheelTemplate.consoleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x000AF21A File Offset: 0x000AD61A
		IControllerTemplateButton IRacingWheelTemplate.shifter1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x000AF224 File Offset: 0x000AD624
		IControllerTemplateButton IRacingWheelTemplate.shifter2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x000AF22E File Offset: 0x000AD62E
		IControllerTemplateButton IRacingWheelTemplate.shifter3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x000AF238 File Offset: 0x000AD638
		IControllerTemplateButton IRacingWheelTemplate.shifter4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x000AF242 File Offset: 0x000AD642
		IControllerTemplateButton IRacingWheelTemplate.shifter5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x000AF24C File Offset: 0x000AD64C
		IControllerTemplateButton IRacingWheelTemplate.shifter6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x000AF256 File Offset: 0x000AD656
		IControllerTemplateButton IRacingWheelTemplate.shifter7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(32);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x000AF260 File Offset: 0x000AD660
		IControllerTemplateButton IRacingWheelTemplate.shifter8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(33);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x000AF26A File Offset: 0x000AD66A
		IControllerTemplateButton IRacingWheelTemplate.shifter9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(34);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x000AF274 File Offset: 0x000AD674
		IControllerTemplateButton IRacingWheelTemplate.shifter10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(35);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x000AF27E File Offset: 0x000AD67E
		IControllerTemplateButton IRacingWheelTemplate.reverseGear
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x000AF288 File Offset: 0x000AD688
		IControllerTemplateButton IRacingWheelTemplate.select
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(36);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x000AF292 File Offset: 0x000AD692
		IControllerTemplateButton IRacingWheelTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(37);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x000AF29C File Offset: 0x000AD69C
		IControllerTemplateButton IRacingWheelTemplate.systemButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(38);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x000AF2A6 File Offset: 0x000AD6A6
		IControllerTemplateButton IRacingWheelTemplate.horn
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(43);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x000AF2B0 File Offset: 0x000AD6B0
		IControllerTemplateDPad IRacingWheelTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(45);
			}
		}

		// Token: 0x04001EB3 RID: 7859
		public static readonly Guid typeGuid = new Guid("104e31d8-9115-4dd5-a398-2e54d35e6c83");

		// Token: 0x04001EB4 RID: 7860
		public const int elementId_wheel = 0;

		// Token: 0x04001EB5 RID: 7861
		public const int elementId_accelerator = 1;

		// Token: 0x04001EB6 RID: 7862
		public const int elementId_brake = 2;

		// Token: 0x04001EB7 RID: 7863
		public const int elementId_clutch = 3;

		// Token: 0x04001EB8 RID: 7864
		public const int elementId_shiftDown = 4;

		// Token: 0x04001EB9 RID: 7865
		public const int elementId_shiftUp = 5;

		// Token: 0x04001EBA RID: 7866
		public const int elementId_wheelButton1 = 6;

		// Token: 0x04001EBB RID: 7867
		public const int elementId_wheelButton2 = 7;

		// Token: 0x04001EBC RID: 7868
		public const int elementId_wheelButton3 = 8;

		// Token: 0x04001EBD RID: 7869
		public const int elementId_wheelButton4 = 9;

		// Token: 0x04001EBE RID: 7870
		public const int elementId_wheelButton5 = 10;

		// Token: 0x04001EBF RID: 7871
		public const int elementId_wheelButton6 = 11;

		// Token: 0x04001EC0 RID: 7872
		public const int elementId_wheelButton7 = 12;

		// Token: 0x04001EC1 RID: 7873
		public const int elementId_wheelButton8 = 13;

		// Token: 0x04001EC2 RID: 7874
		public const int elementId_wheelButton9 = 14;

		// Token: 0x04001EC3 RID: 7875
		public const int elementId_wheelButton10 = 15;

		// Token: 0x04001EC4 RID: 7876
		public const int elementId_consoleButton1 = 16;

		// Token: 0x04001EC5 RID: 7877
		public const int elementId_consoleButton2 = 17;

		// Token: 0x04001EC6 RID: 7878
		public const int elementId_consoleButton3 = 18;

		// Token: 0x04001EC7 RID: 7879
		public const int elementId_consoleButton4 = 19;

		// Token: 0x04001EC8 RID: 7880
		public const int elementId_consoleButton5 = 20;

		// Token: 0x04001EC9 RID: 7881
		public const int elementId_consoleButton6 = 21;

		// Token: 0x04001ECA RID: 7882
		public const int elementId_consoleButton7 = 22;

		// Token: 0x04001ECB RID: 7883
		public const int elementId_consoleButton8 = 23;

		// Token: 0x04001ECC RID: 7884
		public const int elementId_consoleButton9 = 24;

		// Token: 0x04001ECD RID: 7885
		public const int elementId_consoleButton10 = 25;

		// Token: 0x04001ECE RID: 7886
		public const int elementId_shifter1 = 26;

		// Token: 0x04001ECF RID: 7887
		public const int elementId_shifter2 = 27;

		// Token: 0x04001ED0 RID: 7888
		public const int elementId_shifter3 = 28;

		// Token: 0x04001ED1 RID: 7889
		public const int elementId_shifter4 = 29;

		// Token: 0x04001ED2 RID: 7890
		public const int elementId_shifter5 = 30;

		// Token: 0x04001ED3 RID: 7891
		public const int elementId_shifter6 = 31;

		// Token: 0x04001ED4 RID: 7892
		public const int elementId_shifter7 = 32;

		// Token: 0x04001ED5 RID: 7893
		public const int elementId_shifter8 = 33;

		// Token: 0x04001ED6 RID: 7894
		public const int elementId_shifter9 = 34;

		// Token: 0x04001ED7 RID: 7895
		public const int elementId_shifter10 = 35;

		// Token: 0x04001ED8 RID: 7896
		public const int elementId_reverseGear = 44;

		// Token: 0x04001ED9 RID: 7897
		public const int elementId_select = 36;

		// Token: 0x04001EDA RID: 7898
		public const int elementId_start = 37;

		// Token: 0x04001EDB RID: 7899
		public const int elementId_systemButton = 38;

		// Token: 0x04001EDC RID: 7900
		public const int elementId_horn = 43;

		// Token: 0x04001EDD RID: 7901
		public const int elementId_dPadUp = 39;

		// Token: 0x04001EDE RID: 7902
		public const int elementId_dPadRight = 40;

		// Token: 0x04001EDF RID: 7903
		public const int elementId_dPadDown = 41;

		// Token: 0x04001EE0 RID: 7904
		public const int elementId_dPadLeft = 42;

		// Token: 0x04001EE1 RID: 7905
		public const int elementId_dPad = 45;
	}
}
