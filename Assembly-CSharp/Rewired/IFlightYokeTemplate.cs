using System;

namespace Rewired
{
	// Token: 0x0200057A RID: 1402
	public interface IFlightYokeTemplate : IControllerTemplate
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001EAB RID: 7851
		IControllerTemplateButton leftPaddle { get; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001EAC RID: 7852
		IControllerTemplateButton rightPaddle { get; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001EAD RID: 7853
		IControllerTemplateButton leftGripButton1 { get; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001EAE RID: 7854
		IControllerTemplateButton leftGripButton2 { get; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001EAF RID: 7855
		IControllerTemplateButton leftGripButton3 { get; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001EB0 RID: 7856
		IControllerTemplateButton leftGripButton4 { get; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001EB1 RID: 7857
		IControllerTemplateButton leftGripButton5 { get; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001EB2 RID: 7858
		IControllerTemplateButton leftGripButton6 { get; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001EB3 RID: 7859
		IControllerTemplateButton rightGripButton1 { get; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001EB4 RID: 7860
		IControllerTemplateButton rightGripButton2 { get; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001EB5 RID: 7861
		IControllerTemplateButton rightGripButton3 { get; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001EB6 RID: 7862
		IControllerTemplateButton rightGripButton4 { get; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001EB7 RID: 7863
		IControllerTemplateButton rightGripButton5 { get; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001EB8 RID: 7864
		IControllerTemplateButton rightGripButton6 { get; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001EB9 RID: 7865
		IControllerTemplateButton centerButton1 { get; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001EBA RID: 7866
		IControllerTemplateButton centerButton2 { get; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001EBB RID: 7867
		IControllerTemplateButton centerButton3 { get; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001EBC RID: 7868
		IControllerTemplateButton centerButton4 { get; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001EBD RID: 7869
		IControllerTemplateButton centerButton5 { get; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001EBE RID: 7870
		IControllerTemplateButton centerButton6 { get; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001EBF RID: 7871
		IControllerTemplateButton centerButton7 { get; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001EC0 RID: 7872
		IControllerTemplateButton centerButton8 { get; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001EC1 RID: 7873
		IControllerTemplateButton wheel1Up { get; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001EC2 RID: 7874
		IControllerTemplateButton wheel1Down { get; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001EC3 RID: 7875
		IControllerTemplateButton wheel1Press { get; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001EC4 RID: 7876
		IControllerTemplateButton wheel2Up { get; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001EC5 RID: 7877
		IControllerTemplateButton wheel2Down { get; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001EC6 RID: 7878
		IControllerTemplateButton wheel2Press { get; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001EC7 RID: 7879
		IControllerTemplateButton consoleButton1 { get; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001EC8 RID: 7880
		IControllerTemplateButton consoleButton2 { get; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001EC9 RID: 7881
		IControllerTemplateButton consoleButton3 { get; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001ECA RID: 7882
		IControllerTemplateButton consoleButton4 { get; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001ECB RID: 7883
		IControllerTemplateButton consoleButton5 { get; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001ECC RID: 7884
		IControllerTemplateButton consoleButton6 { get; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001ECD RID: 7885
		IControllerTemplateButton consoleButton7 { get; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001ECE RID: 7886
		IControllerTemplateButton consoleButton8 { get; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001ECF RID: 7887
		IControllerTemplateButton consoleButton9 { get; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001ED0 RID: 7888
		IControllerTemplateButton consoleButton10 { get; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001ED1 RID: 7889
		IControllerTemplateButton mode1 { get; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001ED2 RID: 7890
		IControllerTemplateButton mode2 { get; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001ED3 RID: 7891
		IControllerTemplateButton mode3 { get; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001ED4 RID: 7892
		IControllerTemplateYoke yoke { get; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001ED5 RID: 7893
		IControllerTemplateThrottle lever1 { get; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001ED6 RID: 7894
		IControllerTemplateThrottle lever2 { get; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001ED7 RID: 7895
		IControllerTemplateThrottle lever3 { get; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001ED8 RID: 7896
		IControllerTemplateThrottle lever4 { get; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001ED9 RID: 7897
		IControllerTemplateThrottle lever5 { get; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001EDA RID: 7898
		IControllerTemplateHat leftGripHat { get; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001EDB RID: 7899
		IControllerTemplateHat rightGripHat { get; }
	}
}
