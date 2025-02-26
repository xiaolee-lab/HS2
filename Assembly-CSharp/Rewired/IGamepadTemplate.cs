using System;

namespace Rewired
{
	// Token: 0x02000577 RID: 1399
	public interface IGamepadTemplate : IControllerTemplate
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001E0C RID: 7692
		IControllerTemplateButton actionBottomRow1 { get; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001E0D RID: 7693
		IControllerTemplateButton a { get; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001E0E RID: 7694
		IControllerTemplateButton actionBottomRow2 { get; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001E0F RID: 7695
		IControllerTemplateButton b { get; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001E10 RID: 7696
		IControllerTemplateButton actionBottomRow3 { get; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001E11 RID: 7697
		IControllerTemplateButton c { get; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001E12 RID: 7698
		IControllerTemplateButton actionTopRow1 { get; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001E13 RID: 7699
		IControllerTemplateButton x { get; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001E14 RID: 7700
		IControllerTemplateButton actionTopRow2 { get; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001E15 RID: 7701
		IControllerTemplateButton y { get; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001E16 RID: 7702
		IControllerTemplateButton actionTopRow3 { get; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001E17 RID: 7703
		IControllerTemplateButton z { get; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001E18 RID: 7704
		IControllerTemplateButton leftShoulder1 { get; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001E19 RID: 7705
		IControllerTemplateButton leftBumper { get; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001E1A RID: 7706
		IControllerTemplateAxis leftShoulder2 { get; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001E1B RID: 7707
		IControllerTemplateAxis leftTrigger { get; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001E1C RID: 7708
		IControllerTemplateButton rightShoulder1 { get; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001E1D RID: 7709
		IControllerTemplateButton rightBumper { get; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001E1E RID: 7710
		IControllerTemplateAxis rightShoulder2 { get; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001E1F RID: 7711
		IControllerTemplateAxis rightTrigger { get; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001E20 RID: 7712
		IControllerTemplateButton center1 { get; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001E21 RID: 7713
		IControllerTemplateButton back { get; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001E22 RID: 7714
		IControllerTemplateButton center2 { get; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001E23 RID: 7715
		IControllerTemplateButton start { get; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001E24 RID: 7716
		IControllerTemplateButton center3 { get; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001E25 RID: 7717
		IControllerTemplateButton guide { get; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001E26 RID: 7718
		IControllerTemplateThumbStick leftStick { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001E27 RID: 7719
		IControllerTemplateThumbStick rightStick { get; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001E28 RID: 7720
		IControllerTemplateDPad dPad { get; }
	}
}
