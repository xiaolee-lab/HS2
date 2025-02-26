using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AIChara;
using Illusion.Extensions;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009C0 RID: 2496
	public class CustomBase : Singleton<CustomBase>
	{
		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060047B2 RID: 18354 RVA: 0x001B9095 File Offset: 0x001B7495
		// (set) Token: 0x060047B3 RID: 18355 RVA: 0x001B90A2 File Offset: 0x001B74A2
		public bool sliderControlWheel
		{
			get
			{
				return this._sliderControlWheel.Value;
			}
			set
			{
				this._sliderControlWheel.Value = value;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060047B4 RID: 18356 RVA: 0x001B90B0 File Offset: 0x001B74B0
		// (set) Token: 0x060047B5 RID: 18357 RVA: 0x001B90B8 File Offset: 0x001B74B8
		public string nextSceneName { get; set; } = string.Empty;

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060047B6 RID: 18358 RVA: 0x001B90C1 File Offset: 0x001B74C1
		// (set) Token: 0x060047B7 RID: 18359 RVA: 0x001B90C9 File Offset: 0x001B74C9
		public string editSaveFileName { get; set; } = string.Empty;

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060047B8 RID: 18360 RVA: 0x001B90D2 File Offset: 0x001B74D2
		// (set) Token: 0x060047B9 RID: 18361 RVA: 0x001B90DA File Offset: 0x001B74DA
		public bool modeNew { get; set; } = true;

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060047BA RID: 18362 RVA: 0x001B90E3 File Offset: 0x001B74E3
		// (set) Token: 0x060047BB RID: 18363 RVA: 0x001B90EB File Offset: 0x001B74EB
		public byte modeSex { get; set; } = 1;

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x060047BC RID: 18364 RVA: 0x001B90F4 File Offset: 0x001B74F4
		// (set) Token: 0x060047BD RID: 18365 RVA: 0x001B90FC File Offset: 0x001B74FC
		public ChaControl chaCtrl { get; set; }

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x060047BE RID: 18366 RVA: 0x001B9105 File Offset: 0x001B7505
		// (set) Token: 0x060047BF RID: 18367 RVA: 0x001B910D File Offset: 0x001B750D
		public MotionIK customMotionIK { get; set; }

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x060047C0 RID: 18368 RVA: 0x001B9116 File Offset: 0x001B7516
		// (set) Token: 0x060047C1 RID: 18369 RVA: 0x001B911E File Offset: 0x001B751E
		public bool autoClothesState { get; set; } = true;

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x060047C2 RID: 18370 RVA: 0x001B9127 File Offset: 0x001B7527
		// (set) Token: 0x060047C3 RID: 18371 RVA: 0x001B912F File Offset: 0x001B752F
		public int autoClothesStateNo { get; set; }

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x060047C4 RID: 18372 RVA: 0x001B9138 File Offset: 0x001B7538
		// (set) Token: 0x060047C5 RID: 18373 RVA: 0x001B9140 File Offset: 0x001B7540
		public int clothesStateNo { get; set; }

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060047C6 RID: 18374 RVA: 0x001B9149 File Offset: 0x001B7549
		// (set) Token: 0x060047C7 RID: 18375 RVA: 0x001B9151 File Offset: 0x001B7551
		public bool showAcsControllerAll { get; set; }

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060047C8 RID: 18376 RVA: 0x001B915A File Offset: 0x001B755A
		// (set) Token: 0x060047C9 RID: 18377 RVA: 0x001B9162 File Offset: 0x001B7562
		public bool showHairController { get; set; }

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060047CA RID: 18378 RVA: 0x001B916B File Offset: 0x001B756B
		// (set) Token: 0x060047CB RID: 18379 RVA: 0x001B9173 File Offset: 0x001B7573
		public int eyebrowPtn { get; set; }

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060047CC RID: 18380 RVA: 0x001B917C File Offset: 0x001B757C
		// (set) Token: 0x060047CD RID: 18381 RVA: 0x001B9184 File Offset: 0x001B7584
		public int eyePtn { get; set; }

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060047CE RID: 18382 RVA: 0x001B918D File Offset: 0x001B758D
		// (set) Token: 0x060047CF RID: 18383 RVA: 0x001B9195 File Offset: 0x001B7595
		public int mouthPtn { get; set; }

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060047D0 RID: 18384 RVA: 0x001B919E File Offset: 0x001B759E
		// (set) Token: 0x060047D1 RID: 18385 RVA: 0x001B91AB File Offset: 0x001B75AB
		public bool centerDraw
		{
			get
			{
				return this._centerDraw.Value;
			}
			set
			{
				this._centerDraw.Value = value;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060047D2 RID: 18386 RVA: 0x001B91B9 File Offset: 0x001B75B9
		// (set) Token: 0x060047D3 RID: 18387 RVA: 0x001B91C6 File Offset: 0x001B75C6
		public float bgmVol
		{
			get
			{
				return this._bgmVol.Value;
			}
			set
			{
				this._bgmVol.Value = value;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060047D4 RID: 18388 RVA: 0x001B91D4 File Offset: 0x001B75D4
		// (set) Token: 0x060047D5 RID: 18389 RVA: 0x001B91E1 File Offset: 0x001B75E1
		public float seVol
		{
			get
			{
				return this._seVol.Value;
			}
			set
			{
				this._seVol.Value = value;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060047D6 RID: 18390 RVA: 0x001B91EF File Offset: 0x001B75EF
		// (set) Token: 0x060047D7 RID: 18391 RVA: 0x001B91FC File Offset: 0x001B75FC
		public bool drawSaveFrameTop
		{
			get
			{
				return this._drawSaveFrameTop.Value;
			}
			set
			{
				this._drawSaveFrameTop.Value = value;
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060047D8 RID: 18392 RVA: 0x001B920A File Offset: 0x001B760A
		// (set) Token: 0x060047D9 RID: 18393 RVA: 0x001B9217 File Offset: 0x001B7617
		public bool forceBackFrameHide
		{
			get
			{
				return this._forceBackFrameHide.Value;
			}
			set
			{
				this._forceBackFrameHide.Value = value;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060047DA RID: 18394 RVA: 0x001B9225 File Offset: 0x001B7625
		// (set) Token: 0x060047DB RID: 18395 RVA: 0x001B9232 File Offset: 0x001B7632
		public bool drawSaveFrameBack
		{
			get
			{
				return this._drawSaveFrameBack.Value;
			}
			set
			{
				this._drawSaveFrameBack.Value = value;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060047DC RID: 18396 RVA: 0x001B9240 File Offset: 0x001B7640
		// (set) Token: 0x060047DD RID: 18397 RVA: 0x001B924D File Offset: 0x001B764D
		public bool drawSaveFrameFront
		{
			get
			{
				return this._drawSaveFrameFront.Value;
			}
			set
			{
				this._drawSaveFrameFront.Value = value;
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x060047DE RID: 18398 RVA: 0x001B925B File Offset: 0x001B765B
		// (set) Token: 0x060047DF RID: 18399 RVA: 0x001B9268 File Offset: 0x001B7668
		public bool changeCharaName
		{
			get
			{
				return this._changeCharaName.Value;
			}
			set
			{
				this._changeCharaName.Value = value;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060047E0 RID: 18400 RVA: 0x001B9276 File Offset: 0x001B7676
		// (set) Token: 0x060047E1 RID: 18401 RVA: 0x001B9283 File Offset: 0x001B7683
		public bool drawTopHairColor
		{
			get
			{
				return this._drawTopHairColor.Value;
			}
			set
			{
				this._drawTopHairColor.Value = value;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060047E2 RID: 18402 RVA: 0x001B9291 File Offset: 0x001B7691
		// (set) Token: 0x060047E3 RID: 18403 RVA: 0x001B929E File Offset: 0x001B769E
		public bool drawUnderHairColor
		{
			get
			{
				return this._drawUnderHairColor.Value;
			}
			set
			{
				this._drawUnderHairColor.Value = value;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060047E4 RID: 18404 RVA: 0x001B92AC File Offset: 0x001B76AC
		// (set) Token: 0x060047E5 RID: 18405 RVA: 0x001B92B9 File Offset: 0x001B76B9
		public bool autoHairColor
		{
			get
			{
				return this._autoHairColor.Value;
			}
			set
			{
				this._autoHairColor.Value = value;
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x060047E6 RID: 18406 RVA: 0x001B92C7 File Offset: 0x001B76C7
		// (set) Token: 0x060047E7 RID: 18407 RVA: 0x001B92D4 File Offset: 0x001B76D4
		public bool playPoseAnime
		{
			get
			{
				return this._playPoseAnime.Value;
			}
			set
			{
				this._playPoseAnime.Value = value;
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060047E8 RID: 18408 RVA: 0x001B92E2 File Offset: 0x001B76E2
		// (set) Token: 0x060047E9 RID: 18409 RVA: 0x001B92EF File Offset: 0x001B76EF
		public bool cursorDraw
		{
			get
			{
				return this._cursorDraw.Value;
			}
			set
			{
				this._cursorDraw.Value = value;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x060047EA RID: 18410 RVA: 0x001B92FD File Offset: 0x001B76FD
		// (set) Token: 0x060047EB RID: 18411 RVA: 0x001B9305 File Offset: 0x001B7705
		public bool updateCustomUI { get; set; }

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x060047EC RID: 18412 RVA: 0x001B930E File Offset: 0x001B770E
		// (set) Token: 0x060047ED RID: 18413 RVA: 0x001B931B File Offset: 0x001B771B
		public bool accessoryDraw
		{
			get
			{
				return this._accessoryDraw.Value;
			}
			set
			{
				this._accessoryDraw.Value = value;
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x001B9329 File Offset: 0x001B7729
		// (set) Token: 0x060047EF RID: 18415 RVA: 0x001B9336 File Offset: 0x001B7736
		public int poseNo
		{
			get
			{
				return this._poseNo.Value;
			}
			set
			{
				this._poseNo.Value = value;
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x060047F0 RID: 18416 RVA: 0x001B9344 File Offset: 0x001B7744
		// (set) Token: 0x060047F1 RID: 18417 RVA: 0x001B9351 File Offset: 0x001B7751
		public int eyelook
		{
			get
			{
				return this._eyelook.Value;
			}
			set
			{
				this._eyelook.Value = value;
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x060047F2 RID: 18418 RVA: 0x001B935F File Offset: 0x001B775F
		// (set) Token: 0x060047F3 RID: 18419 RVA: 0x001B936C File Offset: 0x001B776C
		public int necklook
		{
			get
			{
				return this._necklook.Value;
			}
			set
			{
				this._necklook.Value = value;
			}
		}

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x060047F4 RID: 18420 RVA: 0x001B937C File Offset: 0x001B777C
		// (remove) Token: 0x060047F5 RID: 18421 RVA: 0x001B93B4 File Offset: 0x001B77B4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFaceType;

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x060047F6 RID: 18422 RVA: 0x001B93EA File Offset: 0x001B77EA
		// (set) Token: 0x060047F7 RID: 18423 RVA: 0x001B93F7 File Offset: 0x001B77F7
		public bool updateCvsFaceType
		{
			get
			{
				return this._updateCvsFaceType.Value;
			}
			set
			{
				this._updateCvsFaceType.Value = value;
			}
		}

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x060047F8 RID: 18424 RVA: 0x001B9408 File Offset: 0x001B7808
		// (remove) Token: 0x060047F9 RID: 18425 RVA: 0x001B9440 File Offset: 0x001B7840
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFaceShapeWhole;

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x001B9476 File Offset: 0x001B7876
		// (set) Token: 0x060047FB RID: 18427 RVA: 0x001B9483 File Offset: 0x001B7883
		public bool updateCvsFaceShapeWhole
		{
			get
			{
				return this._updateCvsFaceShapeWhole.Value;
			}
			set
			{
				this._updateCvsFaceShapeWhole.Value = value;
			}
		}

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x060047FC RID: 18428 RVA: 0x001B9494 File Offset: 0x001B7894
		// (remove) Token: 0x060047FD RID: 18429 RVA: 0x001B94CC File Offset: 0x001B78CC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFaceShapeChin;

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x060047FE RID: 18430 RVA: 0x001B9502 File Offset: 0x001B7902
		// (set) Token: 0x060047FF RID: 18431 RVA: 0x001B950F File Offset: 0x001B790F
		public bool updateCvsFaceShapeChin
		{
			get
			{
				return this._updateCvsFaceShapeChin.Value;
			}
			set
			{
				this._updateCvsFaceShapeChin.Value = value;
			}
		}

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06004800 RID: 18432 RVA: 0x001B9520 File Offset: 0x001B7920
		// (remove) Token: 0x06004801 RID: 18433 RVA: 0x001B9558 File Offset: 0x001B7958
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFaceShapeCheek;

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06004802 RID: 18434 RVA: 0x001B958E File Offset: 0x001B798E
		// (set) Token: 0x06004803 RID: 18435 RVA: 0x001B959B File Offset: 0x001B799B
		public bool updateCvsFaceShapeCheek
		{
			get
			{
				return this._updateCvsFaceShapeCheek.Value;
			}
			set
			{
				this._updateCvsFaceShapeCheek.Value = value;
			}
		}

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06004804 RID: 18436 RVA: 0x001B95AC File Offset: 0x001B79AC
		// (remove) Token: 0x06004805 RID: 18437 RVA: 0x001B95E4 File Offset: 0x001B79E4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFaceShapeEyebrow;

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x001B961A File Offset: 0x001B7A1A
		// (set) Token: 0x06004807 RID: 18439 RVA: 0x001B9627 File Offset: 0x001B7A27
		public bool updateCvsFaceShapeEyebrow
		{
			get
			{
				return this._updateCvsFaceShapeEyebrow.Value;
			}
			set
			{
				this._updateCvsFaceShapeEyebrow.Value = value;
			}
		}

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06004808 RID: 18440 RVA: 0x001B9638 File Offset: 0x001B7A38
		// (remove) Token: 0x06004809 RID: 18441 RVA: 0x001B9670 File Offset: 0x001B7A70
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFaceShapeEyes;

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x0600480A RID: 18442 RVA: 0x001B96A6 File Offset: 0x001B7AA6
		// (set) Token: 0x0600480B RID: 18443 RVA: 0x001B96B3 File Offset: 0x001B7AB3
		public bool updateCvsFaceShapeEyes
		{
			get
			{
				return this._updateCvsFaceShapeEyes.Value;
			}
			set
			{
				this._updateCvsFaceShapeEyes.Value = value;
			}
		}

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x0600480C RID: 18444 RVA: 0x001B96C4 File Offset: 0x001B7AC4
		// (remove) Token: 0x0600480D RID: 18445 RVA: 0x001B96FC File Offset: 0x001B7AFC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFaceShapeNose;

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x0600480E RID: 18446 RVA: 0x001B9732 File Offset: 0x001B7B32
		// (set) Token: 0x0600480F RID: 18447 RVA: 0x001B973F File Offset: 0x001B7B3F
		public bool updateCvsFaceShapeNose
		{
			get
			{
				return this._updateCvsFaceShapeNose.Value;
			}
			set
			{
				this._updateCvsFaceShapeNose.Value = value;
			}
		}

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06004810 RID: 18448 RVA: 0x001B9750 File Offset: 0x001B7B50
		// (remove) Token: 0x06004811 RID: 18449 RVA: 0x001B9788 File Offset: 0x001B7B88
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFaceShapeMouth;

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x001B97BE File Offset: 0x001B7BBE
		// (set) Token: 0x06004813 RID: 18451 RVA: 0x001B97CB File Offset: 0x001B7BCB
		public bool updateCvsFaceShapeMouth
		{
			get
			{
				return this._updateCvsFaceShapeMouth.Value;
			}
			set
			{
				this._updateCvsFaceShapeMouth.Value = value;
			}
		}

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x06004814 RID: 18452 RVA: 0x001B97DC File Offset: 0x001B7BDC
		// (remove) Token: 0x06004815 RID: 18453 RVA: 0x001B9814 File Offset: 0x001B7C14
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFaceShapeEar;

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06004816 RID: 18454 RVA: 0x001B984A File Offset: 0x001B7C4A
		// (set) Token: 0x06004817 RID: 18455 RVA: 0x001B9857 File Offset: 0x001B7C57
		public bool updateCvsFaceShapeEar
		{
			get
			{
				return this._updateCvsFaceShapeEar.Value;
			}
			set
			{
				this._updateCvsFaceShapeEar.Value = value;
			}
		}

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x06004818 RID: 18456 RVA: 0x001B9868 File Offset: 0x001B7C68
		// (remove) Token: 0x06004819 RID: 18457 RVA: 0x001B98A0 File Offset: 0x001B7CA0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsMole;

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x0600481A RID: 18458 RVA: 0x001B98D6 File Offset: 0x001B7CD6
		// (set) Token: 0x0600481B RID: 18459 RVA: 0x001B98E3 File Offset: 0x001B7CE3
		public bool updateCvsMole
		{
			get
			{
				return this._updateCvsMole.Value;
			}
			set
			{
				this._updateCvsMole.Value = value;
			}
		}

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x0600481C RID: 18460 RVA: 0x001B98F4 File Offset: 0x001B7CF4
		// (remove) Token: 0x0600481D RID: 18461 RVA: 0x001B992C File Offset: 0x001B7D2C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsEyeLR;

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x0600481E RID: 18462 RVA: 0x001B9962 File Offset: 0x001B7D62
		// (set) Token: 0x0600481F RID: 18463 RVA: 0x001B996F File Offset: 0x001B7D6F
		public bool updateCvsEyeLR
		{
			get
			{
				return this._updateCvsEyeLR.Value;
			}
			set
			{
				this._updateCvsEyeLR.Value = value;
			}
		}

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x06004820 RID: 18464 RVA: 0x001B9980 File Offset: 0x001B7D80
		// (remove) Token: 0x06004821 RID: 18465 RVA: 0x001B99B8 File Offset: 0x001B7DB8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsEyeEtc;

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x001B99EE File Offset: 0x001B7DEE
		// (set) Token: 0x06004823 RID: 18467 RVA: 0x001B99FB File Offset: 0x001B7DFB
		public bool updateCvsEyeEtc
		{
			get
			{
				return this._updateCvsEyeEtc.Value;
			}
			set
			{
				this._updateCvsEyeEtc.Value = value;
			}
		}

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x06004824 RID: 18468 RVA: 0x001B9A0C File Offset: 0x001B7E0C
		// (remove) Token: 0x06004825 RID: 18469 RVA: 0x001B9A44 File Offset: 0x001B7E44
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsEyeHL;

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06004826 RID: 18470 RVA: 0x001B9A7A File Offset: 0x001B7E7A
		// (set) Token: 0x06004827 RID: 18471 RVA: 0x001B9A87 File Offset: 0x001B7E87
		public bool updateCvsEyeHL
		{
			get
			{
				return this._updateCvsEyeHL.Value;
			}
			set
			{
				this._updateCvsEyeHL.Value = value;
			}
		}

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x06004828 RID: 18472 RVA: 0x001B9A98 File Offset: 0x001B7E98
		// (remove) Token: 0x06004829 RID: 18473 RVA: 0x001B9AD0 File Offset: 0x001B7ED0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsEyebrow;

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x0600482A RID: 18474 RVA: 0x001B9B06 File Offset: 0x001B7F06
		// (set) Token: 0x0600482B RID: 18475 RVA: 0x001B9B13 File Offset: 0x001B7F13
		public bool updateCvsEyebrow
		{
			get
			{
				return this._updateCvsEyebrow.Value;
			}
			set
			{
				this._updateCvsEyebrow.Value = value;
			}
		}

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x0600482C RID: 18476 RVA: 0x001B9B24 File Offset: 0x001B7F24
		// (remove) Token: 0x0600482D RID: 18477 RVA: 0x001B9B5C File Offset: 0x001B7F5C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsEyelashes;

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x0600482E RID: 18478 RVA: 0x001B9B92 File Offset: 0x001B7F92
		// (set) Token: 0x0600482F RID: 18479 RVA: 0x001B9B9F File Offset: 0x001B7F9F
		public bool updateCvsEyelashes
		{
			get
			{
				return this._updateCvsEyelashes.Value;
			}
			set
			{
				this._updateCvsEyelashes.Value = value;
			}
		}

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06004830 RID: 18480 RVA: 0x001B9BB0 File Offset: 0x001B7FB0
		// (remove) Token: 0x06004831 RID: 18481 RVA: 0x001B9BE8 File Offset: 0x001B7FE8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsEyeshadow;

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06004832 RID: 18482 RVA: 0x001B9C1E File Offset: 0x001B801E
		// (set) Token: 0x06004833 RID: 18483 RVA: 0x001B9C2B File Offset: 0x001B802B
		public bool updateCvsEyeshadow
		{
			get
			{
				return this._updateCvsEyeshadow.Value;
			}
			set
			{
				this._updateCvsEyeshadow.Value = value;
			}
		}

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x06004834 RID: 18484 RVA: 0x001B9C3C File Offset: 0x001B803C
		// (remove) Token: 0x06004835 RID: 18485 RVA: 0x001B9C74 File Offset: 0x001B8074
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsCheek;

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06004836 RID: 18486 RVA: 0x001B9CAA File Offset: 0x001B80AA
		// (set) Token: 0x06004837 RID: 18487 RVA: 0x001B9CB7 File Offset: 0x001B80B7
		public bool updateCvsCheek
		{
			get
			{
				return this._updateCvsCheek.Value;
			}
			set
			{
				this._updateCvsCheek.Value = value;
			}
		}

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x06004838 RID: 18488 RVA: 0x001B9CC8 File Offset: 0x001B80C8
		// (remove) Token: 0x06004839 RID: 18489 RVA: 0x001B9D00 File Offset: 0x001B8100
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsLip;

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x0600483A RID: 18490 RVA: 0x001B9D36 File Offset: 0x001B8136
		// (set) Token: 0x0600483B RID: 18491 RVA: 0x001B9D43 File Offset: 0x001B8143
		public bool updateCvsLip
		{
			get
			{
				return this._updateCvsLip.Value;
			}
			set
			{
				this._updateCvsLip.Value = value;
			}
		}

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x0600483C RID: 18492 RVA: 0x001B9D54 File Offset: 0x001B8154
		// (remove) Token: 0x0600483D RID: 18493 RVA: 0x001B9D8C File Offset: 0x001B818C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFacePaint;

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600483E RID: 18494 RVA: 0x001B9DC2 File Offset: 0x001B81C2
		// (set) Token: 0x0600483F RID: 18495 RVA: 0x001B9DCF File Offset: 0x001B81CF
		public bool updateCvsFacePaint
		{
			get
			{
				return this._updateCvsFacePaint.Value;
			}
			set
			{
				this._updateCvsFacePaint.Value = value;
			}
		}

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06004840 RID: 18496 RVA: 0x001B9DE0 File Offset: 0x001B81E0
		// (remove) Token: 0x06004841 RID: 18497 RVA: 0x001B9E18 File Offset: 0x001B8218
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsBeard;

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06004842 RID: 18498 RVA: 0x001B9E4E File Offset: 0x001B824E
		// (set) Token: 0x06004843 RID: 18499 RVA: 0x001B9E5B File Offset: 0x001B825B
		public bool updateCvsBeard
		{
			get
			{
				return this._updateCvsBeard.Value;
			}
			set
			{
				this._updateCvsBeard.Value = value;
			}
		}

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06004844 RID: 18500 RVA: 0x001B9E6C File Offset: 0x001B826C
		// (remove) Token: 0x06004845 RID: 18501 RVA: 0x001B9EA4 File Offset: 0x001B82A4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsBodyShapeWhole;

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06004846 RID: 18502 RVA: 0x001B9EDA File Offset: 0x001B82DA
		// (set) Token: 0x06004847 RID: 18503 RVA: 0x001B9EE7 File Offset: 0x001B82E7
		public bool updateCvsBodyShapeWhole
		{
			get
			{
				return this._updateCvsBodyShapeWhole.Value;
			}
			set
			{
				this._updateCvsBodyShapeWhole.Value = value;
			}
		}

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06004848 RID: 18504 RVA: 0x001B9EF8 File Offset: 0x001B82F8
		// (remove) Token: 0x06004849 RID: 18505 RVA: 0x001B9F30 File Offset: 0x001B8330
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsBodyShapeBreast;

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x0600484A RID: 18506 RVA: 0x001B9F66 File Offset: 0x001B8366
		// (set) Token: 0x0600484B RID: 18507 RVA: 0x001B9F73 File Offset: 0x001B8373
		public bool updateCvsBodyShapeBreast
		{
			get
			{
				return this._updateCvsBodyShapeBreast.Value;
			}
			set
			{
				this._updateCvsBodyShapeBreast.Value = value;
			}
		}

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x0600484C RID: 18508 RVA: 0x001B9F84 File Offset: 0x001B8384
		// (remove) Token: 0x0600484D RID: 18509 RVA: 0x001B9FBC File Offset: 0x001B83BC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsBodyShapeUpper;

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x0600484E RID: 18510 RVA: 0x001B9FF2 File Offset: 0x001B83F2
		// (set) Token: 0x0600484F RID: 18511 RVA: 0x001B9FFF File Offset: 0x001B83FF
		public bool updateCvsBodyShapeUpper
		{
			get
			{
				return this._updateCvsBodyShapeUpper.Value;
			}
			set
			{
				this._updateCvsBodyShapeUpper.Value = value;
			}
		}

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x06004850 RID: 18512 RVA: 0x001BA010 File Offset: 0x001B8410
		// (remove) Token: 0x06004851 RID: 18513 RVA: 0x001BA048 File Offset: 0x001B8448
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsBodyShapeLower;

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06004852 RID: 18514 RVA: 0x001BA07E File Offset: 0x001B847E
		// (set) Token: 0x06004853 RID: 18515 RVA: 0x001BA08B File Offset: 0x001B848B
		public bool updateCvsBodyShapeLower
		{
			get
			{
				return this._updateCvsBodyShapeLower.Value;
			}
			set
			{
				this._updateCvsBodyShapeLower.Value = value;
			}
		}

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x06004854 RID: 18516 RVA: 0x001BA09C File Offset: 0x001B849C
		// (remove) Token: 0x06004855 RID: 18517 RVA: 0x001BA0D4 File Offset: 0x001B84D4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsBodyShapeArm;

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06004856 RID: 18518 RVA: 0x001BA10A File Offset: 0x001B850A
		// (set) Token: 0x06004857 RID: 18519 RVA: 0x001BA117 File Offset: 0x001B8517
		public bool updateCvsBodyShapeArm
		{
			get
			{
				return this._updateCvsBodyShapeArm.Value;
			}
			set
			{
				this._updateCvsBodyShapeArm.Value = value;
			}
		}

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x06004858 RID: 18520 RVA: 0x001BA128 File Offset: 0x001B8528
		// (remove) Token: 0x06004859 RID: 18521 RVA: 0x001BA160 File Offset: 0x001B8560
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsBodyShapeLeg;

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x001BA196 File Offset: 0x001B8596
		// (set) Token: 0x0600485B RID: 18523 RVA: 0x001BA1A3 File Offset: 0x001B85A3
		public bool updateCvsBodyShapeLeg
		{
			get
			{
				return this._updateCvsBodyShapeLeg.Value;
			}
			set
			{
				this._updateCvsBodyShapeLeg.Value = value;
			}
		}

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x0600485C RID: 18524 RVA: 0x001BA1B4 File Offset: 0x001B85B4
		// (remove) Token: 0x0600485D RID: 18525 RVA: 0x001BA1EC File Offset: 0x001B85EC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsBodySkinType;

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x001BA222 File Offset: 0x001B8622
		// (set) Token: 0x0600485F RID: 18527 RVA: 0x001BA22F File Offset: 0x001B862F
		public bool updateCvsBodySkinType
		{
			get
			{
				return this._updateCvsBodySkinType.Value;
			}
			set
			{
				this._updateCvsBodySkinType.Value = value;
			}
		}

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x06004860 RID: 18528 RVA: 0x001BA240 File Offset: 0x001B8640
		// (remove) Token: 0x06004861 RID: 18529 RVA: 0x001BA278 File Offset: 0x001B8678
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsSunburn;

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06004862 RID: 18530 RVA: 0x001BA2AE File Offset: 0x001B86AE
		// (set) Token: 0x06004863 RID: 18531 RVA: 0x001BA2BB File Offset: 0x001B86BB
		public bool updateCvsSunburn
		{
			get
			{
				return this._updateCvsSunburn.Value;
			}
			set
			{
				this._updateCvsSunburn.Value = value;
			}
		}

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x06004864 RID: 18532 RVA: 0x001BA2CC File Offset: 0x001B86CC
		// (remove) Token: 0x06004865 RID: 18533 RVA: 0x001BA304 File Offset: 0x001B8704
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsNip;

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06004866 RID: 18534 RVA: 0x001BA33A File Offset: 0x001B873A
		// (set) Token: 0x06004867 RID: 18535 RVA: 0x001BA347 File Offset: 0x001B8747
		public bool updateCvsNip
		{
			get
			{
				return this._updateCvsNip.Value;
			}
			set
			{
				this._updateCvsNip.Value = value;
			}
		}

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x06004868 RID: 18536 RVA: 0x001BA358 File Offset: 0x001B8758
		// (remove) Token: 0x06004869 RID: 18537 RVA: 0x001BA390 File Offset: 0x001B8790
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsUnderhair;

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x0600486A RID: 18538 RVA: 0x001BA3C6 File Offset: 0x001B87C6
		// (set) Token: 0x0600486B RID: 18539 RVA: 0x001BA3D3 File Offset: 0x001B87D3
		public bool updateCvsUnderhair
		{
			get
			{
				return this._updateCvsUnderhair.Value;
			}
			set
			{
				this._updateCvsUnderhair.Value = value;
			}
		}

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x0600486C RID: 18540 RVA: 0x001BA3E4 File Offset: 0x001B87E4
		// (remove) Token: 0x0600486D RID: 18541 RVA: 0x001BA41C File Offset: 0x001B881C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsNail;

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x0600486E RID: 18542 RVA: 0x001BA452 File Offset: 0x001B8852
		// (set) Token: 0x0600486F RID: 18543 RVA: 0x001BA45F File Offset: 0x001B885F
		public bool updateCvsNail
		{
			get
			{
				return this._updateCvsNail.Value;
			}
			set
			{
				this._updateCvsNail.Value = value;
			}
		}

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x06004870 RID: 18544 RVA: 0x001BA470 File Offset: 0x001B8870
		// (remove) Token: 0x06004871 RID: 18545 RVA: 0x001BA4A8 File Offset: 0x001B88A8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsBodyPaint;

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06004872 RID: 18546 RVA: 0x001BA4DE File Offset: 0x001B88DE
		// (set) Token: 0x06004873 RID: 18547 RVA: 0x001BA4EB File Offset: 0x001B88EB
		public bool updateCvsBodyPaint
		{
			get
			{
				return this._updateCvsBodyPaint.Value;
			}
			set
			{
				this._updateCvsBodyPaint.Value = value;
			}
		}

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06004874 RID: 18548 RVA: 0x001BA4FC File Offset: 0x001B88FC
		// (remove) Token: 0x06004875 RID: 18549 RVA: 0x001BA534 File Offset: 0x001B8934
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFutanari;

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06004876 RID: 18550 RVA: 0x001BA56A File Offset: 0x001B896A
		// (set) Token: 0x06004877 RID: 18551 RVA: 0x001BA577 File Offset: 0x001B8977
		public bool updateCvsFutanari
		{
			get
			{
				return this._updateCvsFutanari.Value;
			}
			set
			{
				this._updateCvsFutanari.Value = value;
			}
		}

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x06004878 RID: 18552 RVA: 0x001BA588 File Offset: 0x001B8988
		// (remove) Token: 0x06004879 RID: 18553 RVA: 0x001BA5C0 File Offset: 0x001B89C0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsHair;

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x0600487A RID: 18554 RVA: 0x001BA5F6 File Offset: 0x001B89F6
		// (set) Token: 0x0600487B RID: 18555 RVA: 0x001BA603 File Offset: 0x001B8A03
		public bool updateCvsHair
		{
			get
			{
				return this._updateCvsHair.Value;
			}
			set
			{
				this._updateCvsHair.Value = value;
			}
		}

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x0600487C RID: 18556 RVA: 0x001BA614 File Offset: 0x001B8A14
		// (remove) Token: 0x0600487D RID: 18557 RVA: 0x001BA64C File Offset: 0x001B8A4C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsClothes;

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x0600487E RID: 18558 RVA: 0x001BA682 File Offset: 0x001B8A82
		// (set) Token: 0x0600487F RID: 18559 RVA: 0x001BA68F File Offset: 0x001B8A8F
		public bool updateCvsClothes
		{
			get
			{
				return this._updateCvsClothes.Value;
			}
			set
			{
				this._updateCvsClothes.Value = value;
			}
		}

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x06004880 RID: 18560 RVA: 0x001BA6A0 File Offset: 0x001B8AA0
		// (remove) Token: 0x06004881 RID: 18561 RVA: 0x001BA6D8 File Offset: 0x001B8AD8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsClothesSaveDelete;

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06004882 RID: 18562 RVA: 0x001BA70E File Offset: 0x001B8B0E
		// (set) Token: 0x06004883 RID: 18563 RVA: 0x001BA71B File Offset: 0x001B8B1B
		public bool updateCvsClothesSaveDelete
		{
			get
			{
				return this._updateCvsClothesSaveDelete.Value;
			}
			set
			{
				this._updateCvsClothesSaveDelete.Value = value;
			}
		}

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x06004884 RID: 18564 RVA: 0x001BA72C File Offset: 0x001B8B2C
		// (remove) Token: 0x06004885 RID: 18565 RVA: 0x001BA764 File Offset: 0x001B8B64
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsClothesLoad;

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06004886 RID: 18566 RVA: 0x001BA79A File Offset: 0x001B8B9A
		// (set) Token: 0x06004887 RID: 18567 RVA: 0x001BA7A7 File Offset: 0x001B8BA7
		public bool updateCvsClothesLoad
		{
			get
			{
				return this._updateCvsClothesLoad.Value;
			}
			set
			{
				this._updateCvsClothesLoad.Value = value;
			}
		}

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x06004888 RID: 18568 RVA: 0x001BA7B8 File Offset: 0x001B8BB8
		// (remove) Token: 0x06004889 RID: 18569 RVA: 0x001BA7F0 File Offset: 0x001B8BF0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsAccessory;

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x0600488A RID: 18570 RVA: 0x001BA826 File Offset: 0x001B8C26
		// (set) Token: 0x0600488B RID: 18571 RVA: 0x001BA833 File Offset: 0x001B8C33
		public bool updateCvsAccessory
		{
			get
			{
				return this._updateCvsAccessory.Value;
			}
			set
			{
				this._updateCvsAccessory.Value = value;
			}
		}

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x0600488C RID: 18572 RVA: 0x001BA844 File Offset: 0x001B8C44
		// (remove) Token: 0x0600488D RID: 18573 RVA: 0x001BA87C File Offset: 0x001B8C7C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsAcsCopy;

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x0600488E RID: 18574 RVA: 0x001BA8B2 File Offset: 0x001B8CB2
		// (set) Token: 0x0600488F RID: 18575 RVA: 0x001BA8BF File Offset: 0x001B8CBF
		public bool updateCvsAcsCopy
		{
			get
			{
				return this._updateCvsAcsCopy.Value;
			}
			set
			{
				this._updateCvsAcsCopy.Value = value;
			}
		}

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06004890 RID: 18576 RVA: 0x001BA8D0 File Offset: 0x001B8CD0
		// (remove) Token: 0x06004891 RID: 18577 RVA: 0x001BA908 File Offset: 0x001B8D08
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsChara;

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x001BA93E File Offset: 0x001B8D3E
		// (set) Token: 0x06004893 RID: 18579 RVA: 0x001BA94B File Offset: 0x001B8D4B
		public bool updateCvsChara
		{
			get
			{
				return this._updateCvsChara.Value;
			}
			set
			{
				this._updateCvsChara.Value = value;
			}
		}

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06004894 RID: 18580 RVA: 0x001BA95C File Offset: 0x001B8D5C
		// (remove) Token: 0x06004895 RID: 18581 RVA: 0x001BA994 File Offset: 0x001B8D94
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsType;

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06004896 RID: 18582 RVA: 0x001BA9CA File Offset: 0x001B8DCA
		// (set) Token: 0x06004897 RID: 18583 RVA: 0x001BA9D7 File Offset: 0x001B8DD7
		public bool updateCvsType
		{
			get
			{
				return this._updateCvsType.Value;
			}
			set
			{
				this._updateCvsType.Value = value;
			}
		}

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06004898 RID: 18584 RVA: 0x001BA9E8 File Offset: 0x001B8DE8
		// (remove) Token: 0x06004899 RID: 18585 RVA: 0x001BAA20 File Offset: 0x001B8E20
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsStatus;

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x0600489A RID: 18586 RVA: 0x001BAA56 File Offset: 0x001B8E56
		// (set) Token: 0x0600489B RID: 18587 RVA: 0x001BAA63 File Offset: 0x001B8E63
		public bool updateCvsStatus
		{
			get
			{
				return this._updateCvsStatus.Value;
			}
			set
			{
				this._updateCvsStatus.Value = value;
			}
		}

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x0600489C RID: 18588 RVA: 0x001BAA74 File Offset: 0x001B8E74
		// (remove) Token: 0x0600489D RID: 18589 RVA: 0x001BAAAC File Offset: 0x001B8EAC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsCharaSaveDelete;

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x0600489E RID: 18590 RVA: 0x001BAAE2 File Offset: 0x001B8EE2
		// (set) Token: 0x0600489F RID: 18591 RVA: 0x001BAAEF File Offset: 0x001B8EEF
		public bool updateCvsCharaSaveDelete
		{
			get
			{
				return this._updateCvsCharaSaveDelete.Value;
			}
			set
			{
				this._updateCvsCharaSaveDelete.Value = value;
			}
		}

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x060048A0 RID: 18592 RVA: 0x001BAB00 File Offset: 0x001B8F00
		// (remove) Token: 0x060048A1 RID: 18593 RVA: 0x001BAB38 File Offset: 0x001B8F38
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsCharaLoad;

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060048A2 RID: 18594 RVA: 0x001BAB6E File Offset: 0x001B8F6E
		// (set) Token: 0x060048A3 RID: 18595 RVA: 0x001BAB7B File Offset: 0x001B8F7B
		public bool updateCvsCharaLoad
		{
			get
			{
				return this._updateCvsCharaLoad.Value;
			}
			set
			{
				this._updateCvsCharaLoad.Value = value;
			}
		}

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x060048A4 RID: 18596 RVA: 0x001BAB8C File Offset: 0x001B8F8C
		// (remove) Token: 0x060048A5 RID: 18597 RVA: 0x001BABC4 File Offset: 0x001B8FC4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action actUpdateCvsFusion;

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060048A6 RID: 18598 RVA: 0x001BABFA File Offset: 0x001B8FFA
		// (set) Token: 0x060048A7 RID: 18599 RVA: 0x001BAC07 File Offset: 0x001B9007
		public bool updateCvsFusion
		{
			get
			{
				return this._updateCvsFusion.Value;
			}
			set
			{
				this._updateCvsFusion.Value = value;
			}
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x001BAC15 File Offset: 0x001B9015
		public void ChangeCharaData()
		{
			this.RestrictSubMenu();
			this.ChangeAcsSlotName(-1);
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x001BAC24 File Offset: 0x001B9024
		public void RestrictSubMenu()
		{
			if (null == this.chaCtrl)
			{
				return;
			}
			if (this.chaCtrl.cmpClothes == null)
			{
				return;
			}
			bool interactable = true;
			bool interactable2 = true;
			ListInfoBase listInfoBase = this.chaCtrl.infoClothes[0];
			if (listInfoBase != null)
			{
				interactable = ("0" == listInfoBase.GetInfo(ChaListDefine.KeyType.Coordinate));
			}
			listInfoBase = this.chaCtrl.infoClothes[2];
			if (listInfoBase != null)
			{
				interactable2 = ("0" == listInfoBase.GetInfo(ChaListDefine.KeyType.Coordinate));
			}
			if (this.subMenuBot)
			{
				this.subMenuBot.interactable = interactable;
			}
			if (this.subMenuInnerDown)
			{
				this.subMenuInnerDown.interactable = interactable2;
			}
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x001BACE0 File Offset: 0x001B90E0
		public void ChangeAcsSlotName(int slotNo = -1)
		{
			for (int i = 0; i < this.acsSlotText.Length; i++)
			{
				if (slotNo == -1 || i == slotNo)
				{
					if (!(null == this.acsSlotText[i]))
					{
						int type = this.chaCtrl.nowCoordinate.accessory.parts[i].type;
						if (type == 350)
						{
							this.acsSlotText[i].text = (i + 1).ToString("00");
						}
						else
						{
							ListInfoBase listInfo = this.chaCtrl.lstCtrl.GetListInfo((ChaListDefine.CategoryNo)type, this.chaCtrl.nowCoordinate.accessory.parts[i].id);
							this.acsSlotText[i].text = string.Format("{0:00} {1}", i + 1, listInfo.Name);
						}
					}
				}
			}
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x001BADD0 File Offset: 0x001B91D0
		public void ChangeAcsSlotColor(int slotNo)
		{
			for (int i = 0; i < this.acsSlotText.Length; i++)
			{
				if (!(null == this.acsSlotText[i]))
				{
					if (i != slotNo)
					{
						this.acsSlotText[i].color = new Color32(235, 226, 215, byte.MaxValue);
					}
					else
					{
						this.acsSlotText[i].color = new Color32(204, 197, 59, byte.MaxValue);
					}
				}
			}
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x001BAE6D File Offset: 0x001B926D
		public void ChangeClothesStateAuto(int stateNo)
		{
			this.autoClothesStateNo = (int)((byte)stateNo);
			if (this.autoClothesState)
			{
				this.ChangeClothesState(0);
			}
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x001BAE8C File Offset: 0x001B928C
		public void ChangeClothesState(int stateNo)
		{
			byte[,] array = new byte[,]
			{
				{
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				},
				{
					2,
					2,
					0,
					0,
					0,
					0,
					0,
					2
				},
				{
					2,
					2,
					2,
					2,
					2,
					2,
					2,
					2
				}
			};
			if (stateNo != -1)
			{
				if (stateNo == 0)
				{
					this.autoClothesState = true;
					this.clothesStateNo = this.autoClothesStateNo;
				}
				else
				{
					this.autoClothesState = false;
					this.clothesStateNo = stateNo - 1;
				}
			}
			if (this.chaCtrl)
			{
				int num = Enum.GetNames(typeof(ChaFileDefine.ClothesKind)).Length;
				for (int i = 0; i < num; i++)
				{
					this.chaCtrl.SetClothesState(i, array[this.clothesStateNo, i], true);
				}
			}
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x001BAF34 File Offset: 0x001B9334
		public void ChangeAnimationNext(int next)
		{
			if (null == this.chaCtrl)
			{
				return;
			}
			ChaListDefine.CategoryNo type = (this.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.custom_pose_f : ChaListDefine.CategoryNo.custom_pose_m;
			Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(type);
			int[] array = categoryInfo.Keys.ToArray<int>();
			if (next == 0)
			{
				int num = (this.poseNo + array.Length - 1) % array.Length;
				if (num == 0)
				{
					num = array.Length - 1;
				}
				this.poseNo = num;
			}
			else
			{
				int num2 = (this.poseNo + 1) % array.Length;
				if (num2 == 0)
				{
					num2++;
				}
				this.poseNo = num2;
			}
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x001BAFE4 File Offset: 0x001B93E4
		public void ChangeAnimationNo(int no, bool mannequin = false)
		{
			if (null == this.chaCtrl)
			{
				return;
			}
			ChaListDefine.CategoryNo type = (this.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.custom_pose_f : ChaListDefine.CategoryNo.custom_pose_m;
			Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(type);
			int[] array = categoryInfo.Keys.ToArray<int>();
			if (!mannequin && no < 1)
			{
				no = 1;
			}
			if (no >= array.Length)
			{
				no = array.Length - 1;
			}
			this.poseNo = no;
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x001BB068 File Offset: 0x001B9468
		public bool ChangeAnimation()
		{
			if (null == this.chaCtrl)
			{
				return false;
			}
			ChaListDefine.CategoryNo type = (this.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.custom_pose_f : ChaListDefine.CategoryNo.custom_pose_m;
			Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(type);
			int[] array = categoryInfo.Keys.ToArray<int>();
			if (this.poseNo >= array.Length || this.poseNo < 0)
			{
				return false;
			}
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string abName = string.Empty;
			string assetName = string.Empty;
			ListInfoBase listInfoBase;
			if (categoryInfo.TryGetValue(array[this.poseNo], out listInfoBase))
			{
				text = listInfoBase.GetInfo(ChaListDefine.KeyType.MainManifest);
				text2 = listInfoBase.GetInfo(ChaListDefine.KeyType.MainAB);
				text3 = listInfoBase.GetInfo(ChaListDefine.KeyType.MainData);
				text4 = listInfoBase.GetInfo(ChaListDefine.KeyType.Clip);
				abName = listInfoBase.GetInfo(ChaListDefine.KeyType.IKAB);
				assetName = listInfoBase.GetInfo(ChaListDefine.KeyType.IKData);
				bool flag = true;
				ListInfoBase listInfoBase2;
				if (0 <= this.backPoseNo && categoryInfo.TryGetValue(array[this.backPoseNo], out listInfoBase2) && listInfoBase2.GetInfo(ChaListDefine.KeyType.MainManifest) == text && listInfoBase2.GetInfo(ChaListDefine.KeyType.MainAB) == text2 && listInfoBase2.GetInfo(ChaListDefine.KeyType.MainData) == text3)
				{
					flag = false;
				}
				if (flag)
				{
					this.chaCtrl.LoadAnimation(text2, text3, text);
				}
				if (this.customMotionIK != null)
				{
					this.customMotionIK.LoadData(abName, assetName, false);
				}
				if (0f > this.animationPos)
				{
					this.chaCtrl.AnimPlay(text4);
				}
				else
				{
					this.chaCtrl.syncPlay(text4, 0, this.animationPos);
				}
				this.animationPos = -1f;
				if (this.customMotionIK != null)
				{
					this.customMotionIK.Calc(text4);
				}
				this.chaCtrl.resetDynamicBoneAll = true;
				this.animeStateName = text4;
				this.backPoseNo = this.poseNo;
				return true;
			}
			return false;
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x001BB280 File Offset: 0x001B9680
		public void ChangeEyebrowPtnNext(int next)
		{
			ChaListDefine.CategoryNo type = (this.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.custom_eyebrow_f : ChaListDefine.CategoryNo.custom_eyebrow_m;
			Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(type);
			int[] array = categoryInfo.Keys.ToArray<int>();
			if (next == -1)
			{
				this.eyebrowPtn = 0;
			}
			else if (next == 0)
			{
				this.eyebrowPtn = (this.eyebrowPtn + array.Length - 1) % array.Length;
			}
			else
			{
				this.eyebrowPtn = (this.eyebrowPtn + 1) % array.Length;
			}
			this.chaCtrl.ChangeEyebrowPtn(array[this.eyebrowPtn], true);
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x001BB324 File Offset: 0x001B9724
		public void ChangeEyebrowPtnNo(int no)
		{
			ChaListDefine.CategoryNo type = (this.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.custom_eyebrow_f : ChaListDefine.CategoryNo.custom_eyebrow_m;
			Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(type);
			int[] array = categoryInfo.Keys.ToArray<int>();
			this.eyebrowPtn = Mathf.Clamp(no - 1, 0, array.Length - 1);
			this.chaCtrl.ChangeEyebrowPtn(array[this.eyebrowPtn], true);
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x001BB398 File Offset: 0x001B9798
		public void ChangeEyePtnNext(int next)
		{
			ChaListDefine.CategoryNo type = (this.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.custom_eye_f : ChaListDefine.CategoryNo.custom_eye_m;
			Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(type);
			int[] array = categoryInfo.Keys.ToArray<int>();
			if (next == -1)
			{
				this.eyePtn = 0;
			}
			else if (next == 0)
			{
				this.eyePtn = (this.eyePtn + array.Length - 1) % array.Length;
			}
			else
			{
				this.eyePtn = (this.eyePtn + 1) % array.Length;
			}
			this.chaCtrl.ChangeEyesPtn(array[this.eyePtn], true);
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x001BB43C File Offset: 0x001B983C
		public void ChangeEyePtnNo(int no)
		{
			ChaListDefine.CategoryNo type = (this.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.custom_eye_f : ChaListDefine.CategoryNo.custom_eye_m;
			Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(type);
			int[] array = categoryInfo.Keys.ToArray<int>();
			this.eyePtn = Mathf.Clamp(no - 1, 0, array.Length - 1);
			this.chaCtrl.ChangeEyesPtn(array[this.eyePtn], true);
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x001BB4B0 File Offset: 0x001B98B0
		public void ChangeMouthPtnNext(int next)
		{
			ChaListDefine.CategoryNo type = (this.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.custom_mouth_f : ChaListDefine.CategoryNo.custom_mouth_m;
			Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(type);
			int[] array = categoryInfo.Keys.ToArray<int>();
			if (next == -1)
			{
				this.mouthPtn = 0;
			}
			else if (next == 0)
			{
				this.mouthPtn = (this.mouthPtn + array.Length - 1) % array.Length;
			}
			else
			{
				this.mouthPtn = (this.mouthPtn + 1) % array.Length;
			}
			this.chaCtrl.ChangeMouthPtn(array[this.mouthPtn], true);
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x001BB554 File Offset: 0x001B9954
		public void ChangeMouthPtnNo(int no)
		{
			ChaListDefine.CategoryNo type = (this.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.custom_mouth_f : ChaListDefine.CategoryNo.custom_mouth_m;
			Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(type);
			int[] array = categoryInfo.Keys.ToArray<int>();
			this.mouthPtn = Mathf.Clamp(no - 1, 0, array.Length - 1);
			this.chaCtrl.ChangeMouthPtn(array[this.mouthPtn], true);
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x001BB5C6 File Offset: 0x001B99C6
		public void UpdateIKCalc()
		{
			if (this.customMotionIK != null)
			{
				this.customMotionIK.Calc(this.animeStateName);
			}
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x001BB5E4 File Offset: 0x001B99E4
		public bool IsInputFocused()
		{
			foreach (InputField inputField in this.lstInputField)
			{
				if (!(null == inputField))
				{
					if (inputField.isFocused)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x001BB660 File Offset: 0x001B9A60
		public static string ConvertTextFromRate(int min, int max, float value)
		{
			return Mathf.RoundToInt(Mathf.Lerp((float)min, (float)max, value)).ToString();
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x001BB68C File Offset: 0x001B9A8C
		public static float ConvertRateFromText(int min, int max, string buf)
		{
			if (buf.IsNullOrEmpty())
			{
				return 0f;
			}
			int num;
			if (!int.TryParse(buf, out num))
			{
				return 0f;
			}
			return Mathf.InverseLerp((float)min, (float)max, (float)num);
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x001BB6C8 File Offset: 0x001B9AC8
		public static float ConvertValueFromTextLimit(float min, float max, int digit, string buf)
		{
			if (buf.IsNullOrEmpty())
			{
				return 0f;
			}
			if (!MathfEx.RangeEqualOn<int>(0, digit, 4))
			{
				return 0f;
			}
			float value = 0f;
			float.TryParse(buf, out value);
			string[] array = new string[]
			{
				"f0",
				"f1",
				"f2",
				"f3",
				"f4"
			};
			value = float.Parse(value.ToString(array[digit]));
			return Mathf.Clamp(value, min, max);
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x001BB750 File Offset: 0x001B9B50
		public void SetUpdateToggleSetting()
		{
			if (this.tglEyesSameSetting)
			{
				this.tglEyesSameSetting.SetIsOnWithoutCallback(this.chaCtrl.fileFace.pupilSameSetting);
			}
			if (this.tglHairSameSetting)
			{
				this.tglHairSameSetting.SetIsOnWithoutCallback(this.chaCtrl.fileHair.sameSetting);
			}
			if (this.tglHairAutoSetting)
			{
				this.tglHairAutoSetting.SetIsOnWithoutCallback(this.chaCtrl.fileHair.autoSetting);
			}
			this.autoHairColor = this.chaCtrl.fileHair.autoSetting;
			if (this.tglHairControlTogether)
			{
				this.tglHairControlTogether.SetIsOnWithoutCallback(this.chaCtrl.fileHair.ctrlTogether);
			}
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x001BB820 File Offset: 0x001B9C20
		public void ResetLightSetting()
		{
			this.lightCustom.transform.localEulerAngles = new Vector3(8f, -20f, 0f);
			this.lightCustom.color = new Color(0.951f, 0.906f, 0.876f);
			this.lightCustom.intensity = 1f;
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x001BB880 File Offset: 0x001B9C80
		protected override void Awake()
		{
			this.lstInputField.Clear();
			this.customSettingSave.Load();
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x001BB898 File Offset: 0x001B9C98
		private void Start()
		{
			this.sliderControlWheel = this.customSettingSave.sliderWheel;
			this.tglSliderWheel.SetIsOnWithoutCallback(this.customSettingSave.sliderWheel);
			this._drawSaveFrameTop.Subscribe(delegate(bool draw)
			{
				if (null != this.saveFrameAssist)
				{
					this.saveFrameAssist.SetActiveSaveFrameTop(draw);
				}
			});
			this._forceBackFrameHide.Subscribe(delegate(bool hide)
			{
				if (null != this.saveFrameAssist)
				{
					this.saveFrameAssist.forceBackFrameHide = hide;
				}
			});
			this._drawSaveFrameBack.Subscribe(delegate(bool draw)
			{
				if (null != this.saveFrameAssist)
				{
					this.saveFrameAssist.backFrameDraw = draw;
				}
			});
			this._drawSaveFrameFront.Subscribe(delegate(bool draw)
			{
				if (null != this.saveFrameAssist)
				{
					this.saveFrameAssist.frontFrameDraw = draw;
				}
			});
			this.SetUpdateToggleSetting();
			if (this.tglEyesSameSetting)
			{
				this.tglEyesSameSetting.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					if (this.chaCtrl)
					{
						this.chaCtrl.fileFace.pupilSameSetting = isOn;
					}
				});
			}
			if (this.tglHairSameSetting)
			{
				this.tglHairSameSetting.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					if (this.chaCtrl)
					{
						this.chaCtrl.fileHair.sameSetting = isOn;
					}
				});
			}
			if (this.tglHairAutoSetting)
			{
				this.tglHairAutoSetting.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					if (this.chaCtrl)
					{
						this.chaCtrl.fileHair.autoSetting = isOn;
					}
					this.autoHairColor = isOn;
				});
			}
			if (this.tglHairControlTogether)
			{
				this.tglHairControlTogether.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					if (this.chaCtrl)
					{
						this.chaCtrl.fileHair.ctrlTogether = isOn;
					}
				});
			}
			(from f in this._changeCharaName
			where f
			select f).Subscribe(delegate(bool f)
			{
				this.customCtrl.UpdateCharaNameText();
				this.changeCharaName = false;
			});
			(from f in this._updateCvsFaceType
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsFaceType != null)
				{
					this.actUpdateCvsFaceType();
				}
				this.updateCvsFaceType = false;
			});
			(from f in this._updateCvsFaceShapeWhole
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsFaceShapeWhole != null)
				{
					this.actUpdateCvsFaceShapeWhole();
				}
				this.updateCvsFaceShapeWhole = false;
			});
			(from f in this._updateCvsFaceShapeChin
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsFaceShapeChin != null)
				{
					this.actUpdateCvsFaceShapeChin();
				}
				this.updateCvsFaceShapeChin = false;
			});
			(from f in this._updateCvsFaceShapeCheek
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsFaceShapeCheek != null)
				{
					this.actUpdateCvsFaceShapeCheek();
				}
				this.updateCvsFaceShapeCheek = false;
			});
			(from f in this._updateCvsFaceShapeEyebrow
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsFaceShapeEyebrow != null)
				{
					this.actUpdateCvsFaceShapeEyebrow();
				}
				this.updateCvsFaceShapeEyebrow = false;
			});
			(from f in this._updateCvsFaceShapeEyes
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsFaceShapeEyes != null)
				{
					this.actUpdateCvsFaceShapeEyes();
				}
				this.updateCvsFaceShapeEyes = false;
			});
			(from f in this._updateCvsFaceShapeNose
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsFaceShapeNose != null)
				{
					this.actUpdateCvsFaceShapeNose();
				}
				this.updateCvsFaceShapeNose = false;
			});
			(from f in this._updateCvsFaceShapeMouth
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsFaceShapeMouth != null)
				{
					this.actUpdateCvsFaceShapeMouth();
				}
				this.updateCvsFaceShapeMouth = false;
			});
			(from f in this._updateCvsFaceShapeEar
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsFaceShapeEar != null)
				{
					this.actUpdateCvsFaceShapeEar();
				}
				this.updateCvsFaceShapeEar = false;
			});
			(from f in this._updateCvsMole
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsMole != null)
				{
					this.actUpdateCvsMole();
				}
				this.updateCvsMole = false;
			});
			(from f in this._updateCvsEyeLR
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsEyeLR != null)
				{
					this.actUpdateCvsEyeLR();
				}
				this.updateCvsEyeLR = false;
			});
			(from f in this._updateCvsEyeEtc
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsEyeEtc != null)
				{
					this.actUpdateCvsEyeEtc();
				}
				this.updateCvsEyeEtc = false;
			});
			(from f in this._updateCvsEyeHL
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsEyeHL != null)
				{
					this.actUpdateCvsEyeHL();
				}
				this.updateCvsEyeHL = false;
			});
			(from f in this._updateCvsEyebrow
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsEyebrow != null)
				{
					this.actUpdateCvsEyebrow();
				}
				this.updateCvsEyebrow = false;
			});
			(from f in this._updateCvsEyelashes
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsEyelashes != null)
				{
					this.actUpdateCvsEyelashes();
				}
				this.updateCvsEyelashes = false;
			});
			(from f in this._updateCvsEyeshadow
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsEyeshadow != null)
				{
					this.actUpdateCvsEyeshadow();
				}
				this.updateCvsEyeshadow = false;
			});
			(from f in this._updateCvsCheek
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsCheek != null)
				{
					this.actUpdateCvsCheek();
				}
				this.updateCvsCheek = false;
			});
			(from f in this._updateCvsLip
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsLip != null)
				{
					this.actUpdateCvsLip();
				}
				this.updateCvsLip = false;
			});
			(from f in this._updateCvsFacePaint
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsFacePaint != null)
				{
					this.actUpdateCvsFacePaint();
				}
				this.updateCvsFacePaint = false;
			});
			(from f in this._updateCvsBeard
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsBeard != null)
				{
					this.actUpdateCvsBeard();
				}
				this.updateCvsBeard = false;
			});
			(from f in this._updateCvsBodyShapeWhole
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsBodyShapeWhole != null)
				{
					this.actUpdateCvsBodyShapeWhole();
				}
				this.updateCvsBodyShapeWhole = false;
			});
			(from f in this._updateCvsBodyShapeBreast
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsBodyShapeBreast != null)
				{
					this.actUpdateCvsBodyShapeBreast();
				}
				this.updateCvsBodyShapeBreast = false;
			});
			(from f in this._updateCvsBodyShapeUpper
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsBodyShapeUpper != null)
				{
					this.actUpdateCvsBodyShapeUpper();
				}
				this.updateCvsBodyShapeUpper = false;
			});
			(from f in this._updateCvsBodyShapeLower
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsBodyShapeLower != null)
				{
					this.actUpdateCvsBodyShapeLower();
				}
				this.updateCvsBodyShapeLower = false;
			});
			(from f in this._updateCvsBodyShapeArm
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsBodyShapeArm != null)
				{
					this.actUpdateCvsBodyShapeArm();
				}
				this.updateCvsBodyShapeArm = false;
			});
			(from f in this._updateCvsBodyShapeLeg
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsBodyShapeLeg != null)
				{
					this.actUpdateCvsBodyShapeLeg();
				}
				this.updateCvsBodyShapeLeg = false;
			});
			(from f in this._updateCvsBodySkinType
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsBodySkinType != null)
				{
					this.actUpdateCvsBodySkinType();
				}
				this.updateCvsBodySkinType = false;
			});
			(from f in this._updateCvsSunburn
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsSunburn != null)
				{
					this.actUpdateCvsSunburn();
				}
				this.updateCvsSunburn = false;
			});
			(from f in this._updateCvsNip
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsNip != null)
				{
					this.actUpdateCvsNip();
				}
				this.updateCvsNip = false;
			});
			(from f in this._updateCvsUnderhair
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsUnderhair != null)
				{
					this.actUpdateCvsUnderhair();
				}
				this.updateCvsUnderhair = false;
			});
			(from f in this._updateCvsNail
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsNail != null)
				{
					this.actUpdateCvsNail();
				}
				this.updateCvsNail = false;
			});
			(from f in this._updateCvsBodyPaint
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsBodyPaint != null)
				{
					this.actUpdateCvsBodyPaint();
				}
				this.updateCvsBodyPaint = false;
			});
			(from f in this._updateCvsFutanari
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsFutanari != null)
				{
					this.actUpdateCvsFutanari();
				}
				this.updateCvsFutanari = false;
			});
			(from f in this._updateCvsHair
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsHair != null)
				{
					this.actUpdateCvsHair();
				}
				this.updateCvsHair = false;
			});
			(from f in this._updateCvsClothes
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsClothes != null)
				{
					this.actUpdateCvsClothes();
				}
				this.updateCvsClothes = false;
			});
			(from f in this._updateCvsClothesSaveDelete
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsClothesSaveDelete != null)
				{
					this.actUpdateCvsClothesSaveDelete();
				}
				this.updateCvsClothesSaveDelete = false;
			});
			(from f in this._updateCvsClothesLoad
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsClothesLoad != null)
				{
					this.actUpdateCvsClothesLoad();
				}
				this.updateCvsClothesLoad = false;
			});
			(from f in this._updateCvsAccessory
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsAccessory != null)
				{
					this.actUpdateCvsAccessory();
				}
				this.updateCvsAccessory = false;
			});
			(from f in this._updateCvsAcsCopy
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsAcsCopy != null)
				{
					this.actUpdateCvsAcsCopy();
				}
				this.updateCvsAcsCopy = false;
			});
			(from f in this._updateCvsChara
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsChara != null)
				{
					this.actUpdateCvsChara();
				}
				this.updateCvsChara = false;
			});
			(from f in this._updateCvsType
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsType != null)
				{
					this.actUpdateCvsType();
				}
				this.updateCvsType = false;
			});
			(from f in this._updateCvsStatus
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsStatus != null)
				{
					this.actUpdateCvsStatus();
				}
				this.updateCvsStatus = false;
			});
			(from f in this._updateCvsCharaSaveDelete
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsCharaSaveDelete != null)
				{
					this.actUpdateCvsCharaSaveDelete();
				}
				this.updateCvsCharaSaveDelete = false;
			});
			(from f in this._updateCvsCharaLoad
			where f
			select f).Subscribe(delegate(bool f)
			{
				if (this.actUpdateCvsCharaLoad != null)
				{
					this.actUpdateCvsCharaLoad();
				}
				this.updateCvsCharaLoad = false;
			});
			this._accessoryDraw.Subscribe(delegate(bool f)
			{
				this.chaCtrl.SetAccessoryStateAll(f);
			});
			this._poseNo.Subscribe(delegate(int _)
			{
				this.ChangeAnimation();
			});
			this._eyelook.Subscribe(delegate(int v)
			{
				this.chaCtrl.ChangeLookEyesPtn((v != 0) ? 0 : 1);
			});
			this._necklook.Subscribe(delegate(int v)
			{
				this.chaCtrl.ChangeLookNeckPtn((v != 0) ? 3 : 1, 1f);
			});
			this._sliderControlWheel.Subscribe(delegate(bool f)
			{
				if (this.customCtrl.sliderScrollRaycast != null)
				{
					this.customCtrl.sliderScrollRaycast.ChangeActiveRaycast(!f);
				}
			});
			this.tglSliderWheel.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
			{
				this.customSettingSave.sliderWheel = isOn;
				this.sliderControlWheel = isOn;
			});
			this._centerDraw.Subscribe(delegate(bool f)
			{
				this.customCtrl.camCtrl.isOutsideTargetTex = f;
			});
			this._autoHairColor.Subscribe(delegate(bool f)
			{
				if (this.hairUICondition.objTopColorSet)
				{
					this.hairUICondition.objTopColorSet.SetActiveIfDifferent(!f && this.drawTopHairColor);
				}
				if (this.hairUICondition.objUnderColorSet)
				{
					this.hairUICondition.objUnderColorSet.SetActiveIfDifferent(!f && this.drawUnderHairColor);
				}
				if (this.hairUICondition.objGlossColorSet)
				{
					this.hairUICondition.objGlossColorSet.SetActiveIfDifferent(!f);
				}
			});
			this._drawTopHairColor.Subscribe(delegate(bool f)
			{
				if (this.hairUICondition.objTopColorSet)
				{
					this.hairUICondition.objTopColorSet.SetActiveIfDifferent(!this.autoHairColor && f);
				}
			});
			this._drawUnderHairColor.Subscribe(delegate(bool f)
			{
				if (this.hairUICondition.objUnderColorSet)
				{
					this.hairUICondition.objUnderColorSet.SetActiveIfDifferent(!this.autoHairColor && f);
				}
			});
			this._playPoseAnime.Subscribe(delegate(bool f)
			{
				this.chaCtrl.animBody.speed = ((!f) ? 0f : 1f);
			});
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x001BC528 File Offset: 0x001BA928
		public void Update()
		{
			this.lstShow.Clear();
			this.lstShow.Add(this.showAcsControllerAll);
			this.lstShow.Add(this.showAcsController[0]);
			this.lstShow.Add(this.customSettingSave.acsCtrlSetting.correctSetting[0].draw);
			bool active = YS_Assist.CheckFlagsList(this.lstShow);
			if (this.objAcs01ControllerTop)
			{
				this.objAcs01ControllerTop.SetActiveIfDifferent(active);
			}
			this.lstShow.Clear();
			this.lstShow.Add(this.showAcsControllerAll);
			this.lstShow.Add(this.showAcsController[1]);
			this.lstShow.Add(this.customSettingSave.acsCtrlSetting.correctSetting[1].draw);
			active = YS_Assist.CheckFlagsList(this.lstShow);
			if (this.objAcs02ControllerTop)
			{
				this.objAcs02ControllerTop.SetActiveIfDifferent(active);
			}
			this.lstShow.Clear();
			this.lstShow.Add(this.showHairController);
			this.lstShow.Add(this.customSettingSave.hairCtrlSetting.drawController);
			active = YS_Assist.CheckFlagsList(this.lstShow);
			if (this.objHairControllerTop)
			{
				this.objHairControllerTop.SetActiveIfDifferent(active);
			}
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x001BC68B File Offset: 0x001BAA8B
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.customSettingSave.Save();
		}

		// Token: 0x040042EB RID: 17131
		[Header("-----------------------------------------")]
		private BoolReactiveProperty _sliderControlWheel = new BoolReactiveProperty(true);

		// Token: 0x040042EE RID: 17134
		public CustomColorCtrl customColorCtrl;

		// Token: 0x040042EF RID: 17135
		public CvsCaptureMenu cvsCapMenu;

		// Token: 0x040042F0 RID: 17136
		public CustomDrawMenu drawMenu;

		// Token: 0x040042F1 RID: 17137
		public SaveFrameAssist saveFrameAssist;

		// Token: 0x040042F2 RID: 17138
		public Light lightCustom;

		// Token: 0x040042F3 RID: 17139
		public GameObject objAcs01ControllerTop;

		// Token: 0x040042F4 RID: 17140
		public GameObject objAcs02ControllerTop;

		// Token: 0x040042F5 RID: 17141
		public GameObject objHairControllerTop;

		// Token: 0x040042F6 RID: 17142
		[SerializeField]
		private Toggle tglEyesSameSetting;

		// Token: 0x040042F7 RID: 17143
		[SerializeField]
		private Toggle tglHairSameSetting;

		// Token: 0x040042F8 RID: 17144
		[SerializeField]
		private Toggle tglHairAutoSetting;

		// Token: 0x040042F9 RID: 17145
		[SerializeField]
		private Toggle tglHairControlTogether;

		// Token: 0x040042FA RID: 17146
		[SerializeField]
		private Toggle tglSliderWheel;

		// Token: 0x040042FB RID: 17147
		[SerializeField]
		private Toggle tglCenterDraw;

		// Token: 0x040042FC RID: 17148
		[SerializeField]
		private Slider sldBGMVol;

		// Token: 0x040042FD RID: 17149
		[SerializeField]
		private Slider sldSEVol;

		// Token: 0x040042FE RID: 17150
		[SerializeField]
		private UI_ButtonEx subMenuBot;

		// Token: 0x040042FF RID: 17151
		[SerializeField]
		private UI_ButtonEx subMenuInnerDown;

		// Token: 0x04004300 RID: 17152
		public CustomBase.PlayVoiceBackup playVoiceBackup = new CustomBase.PlayVoiceBackup();

		// Token: 0x04004301 RID: 17153
		[Header("髪の毛関連の表示 ------------------------")]
		[SerializeField]
		private CustomBase.HairUICondition hairUICondition;

		// Token: 0x04004302 RID: 17154
		[Header("アクセサリのスロット名 ------------------")]
		[SerializeField]
		private Text[] acsSlotText;

		// Token: 0x04004303 RID: 17155
		public CustomBase.CustomSettingSave customSettingSave = new CustomBase.CustomSettingSave();

		// Token: 0x04004304 RID: 17156
		public ChaFileControl defChaCtrl = new ChaFileControl();

		// Token: 0x04004309 RID: 17161
		private string animeStateName = string.Empty;

		// Token: 0x0400430D RID: 17165
		private List<bool> lstShow = new List<bool>();

		// Token: 0x0400430F RID: 17167
		public bool[] showAcsController = new bool[2];

		// Token: 0x04004311 RID: 17169
		public CustomControl customCtrl;

		// Token: 0x04004312 RID: 17170
		public Dictionary<int, string> dictPersonality = new Dictionary<int, string>();

		// Token: 0x04004313 RID: 17171
		public List<InputField> lstInputField = new List<InputField>();

		// Token: 0x04004317 RID: 17175
		private BoolReactiveProperty _centerDraw = new BoolReactiveProperty(true);

		// Token: 0x04004318 RID: 17176
		private FloatReactiveProperty _bgmVol = new FloatReactiveProperty(0.3f);

		// Token: 0x04004319 RID: 17177
		private FloatReactiveProperty _seVol = new FloatReactiveProperty(0.5f);

		// Token: 0x0400431A RID: 17178
		private BoolReactiveProperty _drawSaveFrameTop = new BoolReactiveProperty(false);

		// Token: 0x0400431B RID: 17179
		private BoolReactiveProperty _forceBackFrameHide = new BoolReactiveProperty(false);

		// Token: 0x0400431C RID: 17180
		private BoolReactiveProperty _drawSaveFrameBack = new BoolReactiveProperty(false);

		// Token: 0x0400431D RID: 17181
		private BoolReactiveProperty _drawSaveFrameFront = new BoolReactiveProperty(false);

		// Token: 0x0400431E RID: 17182
		private BoolReactiveProperty _changeCharaName = new BoolReactiveProperty(false);

		// Token: 0x0400431F RID: 17183
		private BoolReactiveProperty _drawTopHairColor = new BoolReactiveProperty(false);

		// Token: 0x04004320 RID: 17184
		private BoolReactiveProperty _drawUnderHairColor = new BoolReactiveProperty(false);

		// Token: 0x04004321 RID: 17185
		private BoolReactiveProperty _autoHairColor = new BoolReactiveProperty(false);

		// Token: 0x04004322 RID: 17186
		private BoolReactiveProperty _playPoseAnime = new BoolReactiveProperty(true);

		// Token: 0x04004323 RID: 17187
		private BoolReactiveProperty _cursorDraw = new BoolReactiveProperty(true);

		// Token: 0x04004325 RID: 17189
		private BoolReactiveProperty _accessoryDraw = new BoolReactiveProperty(true);

		// Token: 0x04004326 RID: 17190
		public int backPoseNo = -1;

		// Token: 0x04004327 RID: 17191
		private IntReactiveProperty _poseNo = new IntReactiveProperty(-1);

		// Token: 0x04004328 RID: 17192
		public float animationPos = -1f;

		// Token: 0x04004329 RID: 17193
		private IntReactiveProperty _eyelook = new IntReactiveProperty(0);

		// Token: 0x0400432A RID: 17194
		private IntReactiveProperty _necklook = new IntReactiveProperty(1);

		// Token: 0x0400432C RID: 17196
		private BoolReactiveProperty _updateCvsFaceType = new BoolReactiveProperty(false);

		// Token: 0x0400432E RID: 17198
		private BoolReactiveProperty _updateCvsFaceShapeWhole = new BoolReactiveProperty(false);

		// Token: 0x04004330 RID: 17200
		private BoolReactiveProperty _updateCvsFaceShapeChin = new BoolReactiveProperty(false);

		// Token: 0x04004332 RID: 17202
		private BoolReactiveProperty _updateCvsFaceShapeCheek = new BoolReactiveProperty(false);

		// Token: 0x04004334 RID: 17204
		private BoolReactiveProperty _updateCvsFaceShapeEyebrow = new BoolReactiveProperty(false);

		// Token: 0x04004336 RID: 17206
		private BoolReactiveProperty _updateCvsFaceShapeEyes = new BoolReactiveProperty(false);

		// Token: 0x04004338 RID: 17208
		private BoolReactiveProperty _updateCvsFaceShapeNose = new BoolReactiveProperty(false);

		// Token: 0x0400433A RID: 17210
		private BoolReactiveProperty _updateCvsFaceShapeMouth = new BoolReactiveProperty(false);

		// Token: 0x0400433C RID: 17212
		private BoolReactiveProperty _updateCvsFaceShapeEar = new BoolReactiveProperty(false);

		// Token: 0x0400433E RID: 17214
		private BoolReactiveProperty _updateCvsMole = new BoolReactiveProperty(false);

		// Token: 0x04004340 RID: 17216
		private BoolReactiveProperty _updateCvsEyeLR = new BoolReactiveProperty(false);

		// Token: 0x04004342 RID: 17218
		private BoolReactiveProperty _updateCvsEyeEtc = new BoolReactiveProperty(false);

		// Token: 0x04004344 RID: 17220
		private BoolReactiveProperty _updateCvsEyeHL = new BoolReactiveProperty(false);

		// Token: 0x04004346 RID: 17222
		private BoolReactiveProperty _updateCvsEyebrow = new BoolReactiveProperty(false);

		// Token: 0x04004348 RID: 17224
		private BoolReactiveProperty _updateCvsEyelashes = new BoolReactiveProperty(false);

		// Token: 0x0400434A RID: 17226
		private BoolReactiveProperty _updateCvsEyeshadow = new BoolReactiveProperty(false);

		// Token: 0x0400434C RID: 17228
		private BoolReactiveProperty _updateCvsCheek = new BoolReactiveProperty(false);

		// Token: 0x0400434E RID: 17230
		private BoolReactiveProperty _updateCvsLip = new BoolReactiveProperty(false);

		// Token: 0x04004350 RID: 17232
		private BoolReactiveProperty _updateCvsFacePaint = new BoolReactiveProperty(false);

		// Token: 0x04004352 RID: 17234
		private BoolReactiveProperty _updateCvsBeard = new BoolReactiveProperty(false);

		// Token: 0x04004354 RID: 17236
		private BoolReactiveProperty _updateCvsBodyShapeWhole = new BoolReactiveProperty(false);

		// Token: 0x04004356 RID: 17238
		private BoolReactiveProperty _updateCvsBodyShapeBreast = new BoolReactiveProperty(false);

		// Token: 0x04004358 RID: 17240
		private BoolReactiveProperty _updateCvsBodyShapeUpper = new BoolReactiveProperty(false);

		// Token: 0x0400435A RID: 17242
		private BoolReactiveProperty _updateCvsBodyShapeLower = new BoolReactiveProperty(false);

		// Token: 0x0400435C RID: 17244
		private BoolReactiveProperty _updateCvsBodyShapeArm = new BoolReactiveProperty(false);

		// Token: 0x0400435E RID: 17246
		private BoolReactiveProperty _updateCvsBodyShapeLeg = new BoolReactiveProperty(false);

		// Token: 0x04004360 RID: 17248
		private BoolReactiveProperty _updateCvsBodySkinType = new BoolReactiveProperty(false);

		// Token: 0x04004362 RID: 17250
		private BoolReactiveProperty _updateCvsSunburn = new BoolReactiveProperty(false);

		// Token: 0x04004364 RID: 17252
		private BoolReactiveProperty _updateCvsNip = new BoolReactiveProperty(false);

		// Token: 0x04004366 RID: 17254
		private BoolReactiveProperty _updateCvsUnderhair = new BoolReactiveProperty(false);

		// Token: 0x04004368 RID: 17256
		private BoolReactiveProperty _updateCvsNail = new BoolReactiveProperty(false);

		// Token: 0x0400436A RID: 17258
		private BoolReactiveProperty _updateCvsBodyPaint = new BoolReactiveProperty(false);

		// Token: 0x0400436C RID: 17260
		private BoolReactiveProperty _updateCvsFutanari = new BoolReactiveProperty(false);

		// Token: 0x0400436E RID: 17262
		private BoolReactiveProperty _updateCvsHair = new BoolReactiveProperty(false);

		// Token: 0x04004370 RID: 17264
		private BoolReactiveProperty _updateCvsClothes = new BoolReactiveProperty(false);

		// Token: 0x04004372 RID: 17266
		private BoolReactiveProperty _updateCvsClothesSaveDelete = new BoolReactiveProperty(false);

		// Token: 0x04004374 RID: 17268
		private BoolReactiveProperty _updateCvsClothesLoad = new BoolReactiveProperty(false);

		// Token: 0x04004376 RID: 17270
		private BoolReactiveProperty _updateCvsAccessory = new BoolReactiveProperty(false);

		// Token: 0x04004377 RID: 17271
		public bool forceUpdateAcsList;

		// Token: 0x04004379 RID: 17273
		private BoolReactiveProperty _updateCvsAcsCopy = new BoolReactiveProperty(false);

		// Token: 0x0400437B RID: 17275
		private BoolReactiveProperty _updateCvsChara = new BoolReactiveProperty(false);

		// Token: 0x0400437D RID: 17277
		private BoolReactiveProperty _updateCvsType = new BoolReactiveProperty(false);

		// Token: 0x0400437F RID: 17279
		private BoolReactiveProperty _updateCvsStatus = new BoolReactiveProperty(false);

		// Token: 0x04004381 RID: 17281
		private BoolReactiveProperty _updateCvsCharaSaveDelete = new BoolReactiveProperty(false);

		// Token: 0x04004383 RID: 17283
		private BoolReactiveProperty _updateCvsCharaLoad = new BoolReactiveProperty(false);

		// Token: 0x04004385 RID: 17285
		private BoolReactiveProperty _updateCvsFusion = new BoolReactiveProperty(false);

		// Token: 0x020009C1 RID: 2497
		public class CustomSettingSave
		{
			// Token: 0x06004930 RID: 18736 RVA: 0x001BD098 File Offset: 0x001BB498
			public void ResetWinLayout()
			{
				this.winSubLayout = new Vector2(1444f, -8f);
				this.winDrawLayout = new Vector2(1536f, -568f);
				this.winColorLayout = new Vector2(1536f, -768f);
				this.winPatternLayout = new Vector2(1176f, -8f);
			}

			// Token: 0x06004931 RID: 18737 RVA: 0x001BD0FC File Offset: 0x001BB4FC
			public void Save()
			{
				string path = UserData.Path + "custom/customscene.dat";
				string directoryName = Path.GetDirectoryName(path);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						binaryWriter.Write(CharaCustomDefine.CustomSettingVersion.ToString());
						binaryWriter.Write(this.backColor.r);
						binaryWriter.Write(this.backColor.g);
						binaryWriter.Write(this.backColor.b);
						binaryWriter.Write(this.bgmOn);
						binaryWriter.Write(this.hairCtrlSetting.drawController);
						binaryWriter.Write(this.hairCtrlSetting.controllerType);
						for (int i = 0; i < 2; i++)
						{
							binaryWriter.Write(this.acsCtrlSetting.correctSetting[i].posRate);
							binaryWriter.Write(this.acsCtrlSetting.correctSetting[i].rotRate);
							binaryWriter.Write(this.acsCtrlSetting.correctSetting[i].sclRate);
							binaryWriter.Write(this.acsCtrlSetting.correctSetting[i].draw);
							binaryWriter.Write(this.acsCtrlSetting.correctSetting[i].type);
							binaryWriter.Write(this.acsCtrlSetting.correctSetting[i].speed);
							binaryWriter.Write(this.acsCtrlSetting.correctSetting[i].scale);
						}
						binaryWriter.Write(this.sliderWheel);
						binaryWriter.Write(this.centerDraw);
						binaryWriter.Write(this.bgmVol);
						binaryWriter.Write(this.seVol);
						binaryWriter.Write(this.winSubLayout.x);
						binaryWriter.Write(this.winSubLayout.y);
						binaryWriter.Write(this.winDrawLayout.x);
						binaryWriter.Write(this.winDrawLayout.y);
						binaryWriter.Write(this.winColorLayout.x);
						binaryWriter.Write(this.winColorLayout.y);
						binaryWriter.Write(this.winPatternLayout.x);
						binaryWriter.Write(this.winPatternLayout.y);
						binaryWriter.Write(this.hairCtrlSetting.controllerSpeed);
						binaryWriter.Write(this.hairCtrlSetting.controllerScale);
					}
				}
			}

			// Token: 0x06004932 RID: 18738 RVA: 0x001BD3AC File Offset: 0x001BB7AC
			public void Load()
			{
				string path = UserData.Path + "custom/customscene.dat";
				if (!File.Exists(path))
				{
					return;
				}
				using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						this.version = new Version(binaryReader.ReadString());
						this.backColor.r = binaryReader.ReadSingle();
						this.backColor.g = binaryReader.ReadSingle();
						this.backColor.b = binaryReader.ReadSingle();
						this.bgmOn = binaryReader.ReadBoolean();
						this.hairCtrlSetting.drawController = binaryReader.ReadBoolean();
						this.hairCtrlSetting.controllerType = binaryReader.ReadInt32();
						for (int i = 0; i < 2; i++)
						{
							this.acsCtrlSetting.correctSetting[i].posRate = binaryReader.ReadInt32();
							this.acsCtrlSetting.correctSetting[i].rotRate = binaryReader.ReadInt32();
							this.acsCtrlSetting.correctSetting[i].sclRate = binaryReader.ReadInt32();
							this.acsCtrlSetting.correctSetting[i].draw = binaryReader.ReadBoolean();
							this.acsCtrlSetting.correctSetting[i].type = binaryReader.ReadInt32();
							this.acsCtrlSetting.correctSetting[i].speed = binaryReader.ReadSingle();
							this.acsCtrlSetting.correctSetting[i].scale = binaryReader.ReadSingle();
						}
						if (!(this.version < new Version("0.0.1")))
						{
							this.sliderWheel = binaryReader.ReadBoolean();
							this.centerDraw = binaryReader.ReadBoolean();
							this.bgmVol = binaryReader.ReadSingle();
							this.seVol = binaryReader.ReadSingle();
							if (!(this.version < new Version("0.0.2")))
							{
								this.winSubLayout.x = binaryReader.ReadSingle();
								this.winSubLayout.y = binaryReader.ReadSingle();
								this.winDrawLayout.x = binaryReader.ReadSingle();
								this.winDrawLayout.y = binaryReader.ReadSingle();
								this.winColorLayout.x = binaryReader.ReadSingle();
								this.winColorLayout.y = binaryReader.ReadSingle();
								this.winPatternLayout.x = binaryReader.ReadSingle();
								this.winPatternLayout.y = binaryReader.ReadSingle();
								if (!(this.version < new Version("0.0.3")))
								{
									this.hairCtrlSetting.controllerSpeed = binaryReader.ReadSingle();
									this.hairCtrlSetting.controllerScale = binaryReader.ReadSingle();
								}
							}
						}
					}
				}
			}

			// Token: 0x040043B3 RID: 17331
			public Version version = CharaCustomDefine.CustomSettingVersion;

			// Token: 0x040043B4 RID: 17332
			public Color backColor = Color.gray;

			// Token: 0x040043B5 RID: 17333
			public bool bgmOn = true;

			// Token: 0x040043B6 RID: 17334
			public bool sliderWheel = true;

			// Token: 0x040043B7 RID: 17335
			public bool centerDraw = true;

			// Token: 0x040043B8 RID: 17336
			public float bgmVol = 0.3f;

			// Token: 0x040043B9 RID: 17337
			public float seVol = 0.5f;

			// Token: 0x040043BA RID: 17338
			public CustomBase.CustomSettingSave.HairCtrlSetting hairCtrlSetting = new CustomBase.CustomSettingSave.HairCtrlSetting();

			// Token: 0x040043BB RID: 17339
			public CustomBase.CustomSettingSave.AcsCtrlSetting acsCtrlSetting = new CustomBase.CustomSettingSave.AcsCtrlSetting();

			// Token: 0x040043BC RID: 17340
			public Vector2 winSubLayout = new Vector2(1444f, -8f);

			// Token: 0x040043BD RID: 17341
			public Vector2 winDrawLayout = new Vector2(1536f, -568f);

			// Token: 0x040043BE RID: 17342
			public Vector2 winColorLayout = new Vector2(1536f, -768f);

			// Token: 0x040043BF RID: 17343
			public Vector2 winPatternLayout = new Vector2(1176f, -8f);

			// Token: 0x020009C2 RID: 2498
			public class HairCtrlSetting
			{
				// Token: 0x040043C0 RID: 17344
				public bool drawController;

				// Token: 0x040043C1 RID: 17345
				public int controllerType;

				// Token: 0x040043C2 RID: 17346
				public float controllerSpeed = 0.3f;

				// Token: 0x040043C3 RID: 17347
				public float controllerScale = 0.4f;
			}

			// Token: 0x020009C3 RID: 2499
			public class AcsCtrlSetting
			{
				// Token: 0x06004934 RID: 18740 RVA: 0x001BD6C0 File Offset: 0x001BBAC0
				public AcsCtrlSetting()
				{
					for (int i = 0; i < this.correctSetting.Length; i++)
					{
						this.correctSetting[i] = new CustomBase.CustomSettingSave.AcsCtrlSetting.CorrectSetting();
					}
				}

				// Token: 0x040043C4 RID: 17348
				public CustomBase.CustomSettingSave.AcsCtrlSetting.CorrectSetting[] correctSetting = new CustomBase.CustomSettingSave.AcsCtrlSetting.CorrectSetting[2];

				// Token: 0x020009C4 RID: 2500
				public class CorrectSetting
				{
					// Token: 0x040043C5 RID: 17349
					public int posRate;

					// Token: 0x040043C6 RID: 17350
					public int rotRate;

					// Token: 0x040043C7 RID: 17351
					public int sclRate;

					// Token: 0x040043C8 RID: 17352
					public bool draw;

					// Token: 0x040043C9 RID: 17353
					public int type;

					// Token: 0x040043CA RID: 17354
					public float speed = 0.3f;

					// Token: 0x040043CB RID: 17355
					public float scale = 1.5f;
				}
			}
		}

		// Token: 0x020009C5 RID: 2501
		public class PlayVoiceBackup
		{
			// Token: 0x040043CC RID: 17356
			public bool playSampleVoice;

			// Token: 0x040043CD RID: 17357
			public int backEyebrowPtn;

			// Token: 0x040043CE RID: 17358
			public int backEyesPtn;

			// Token: 0x040043CF RID: 17359
			public bool backBlink = true;

			// Token: 0x040043D0 RID: 17360
			public float backEyesOpen = 1f;

			// Token: 0x040043D1 RID: 17361
			public int backMouthPtn;

			// Token: 0x040043D2 RID: 17362
			public bool backMouthFix = true;

			// Token: 0x040043D3 RID: 17363
			public float backMouthOpen;
		}

		// Token: 0x020009C6 RID: 2502
		[Serializable]
		public class HairUICondition
		{
			// Token: 0x040043D4 RID: 17364
			public GameObject objTopColorSet;

			// Token: 0x040043D5 RID: 17365
			public GameObject objUnderColorSet;

			// Token: 0x040043D6 RID: 17366
			public GameObject objGlossColorSet;
		}
	}
}
