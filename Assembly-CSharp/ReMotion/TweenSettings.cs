using System;

namespace ReMotion
{
	// Token: 0x020004FE RID: 1278
	public sealed class TweenSettings
	{
		// Token: 0x0600181A RID: 6170 RVA: 0x00095B54 File Offset: 0x00093F54
		public TweenSettings(LoopType loopType = LoopType.None, bool ignoreTimeScale = false, EasingFunction defaultEasing = null)
		{
			this.LoopType = loopType;
			this.IsIgnoreTimeScale = ignoreTimeScale;
			this.DefaultEasing = (defaultEasing ?? EasingFunctions.EaseOutQuad);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00095B7D File Offset: 0x00093F7D
		public static void SetDefault(TweenSettings settings)
		{
			TweenSettings.Default = settings;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x00095B85 File Offset: 0x00093F85
		// (set) Token: 0x0600181D RID: 6173 RVA: 0x00095B8D File Offset: 0x00093F8D
		public LoopType LoopType { get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x00095B96 File Offset: 0x00093F96
		// (set) Token: 0x0600181F RID: 6175 RVA: 0x00095B9E File Offset: 0x00093F9E
		public bool IsIgnoreTimeScale { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x00095BA7 File Offset: 0x00093FA7
		// (set) Token: 0x06001821 RID: 6177 RVA: 0x00095BAF File Offset: 0x00093FAF
		public EasingFunction DefaultEasing { get; private set; }

		// Token: 0x04001B34 RID: 6964
		public static TweenSettings Default = new TweenSettings(LoopType.None, false, null);

		// Token: 0x04001B35 RID: 6965
		public static readonly TweenSettings Cycle = new TweenSettings(LoopType.None, false, null)
		{
			LoopType = LoopType.Cycle
		};

		// Token: 0x04001B36 RID: 6966
		public static readonly TweenSettings Restart = new TweenSettings(LoopType.None, false, null)
		{
			LoopType = LoopType.Restart
		};

		// Token: 0x04001B37 RID: 6967
		public static readonly TweenSettings CycleOnce = new TweenSettings(LoopType.None, false, null)
		{
			LoopType = LoopType.CycleOnce
		};

		// Token: 0x04001B38 RID: 6968
		public static readonly TweenSettings IgnoreTimeScale = new TweenSettings(LoopType.None, false, null)
		{
			IsIgnoreTimeScale = true
		};

		// Token: 0x04001B39 RID: 6969
		public static readonly TweenSettings IgnoreTimeScaleCycle = new TweenSettings(LoopType.None, false, null)
		{
			IsIgnoreTimeScale = true,
			LoopType = LoopType.Cycle
		};

		// Token: 0x04001B3A RID: 6970
		public static readonly TweenSettings IgnoreTimeScaleRestart = new TweenSettings(LoopType.None, false, null)
		{
			IsIgnoreTimeScale = true,
			LoopType = LoopType.Restart
		};

		// Token: 0x04001B3B RID: 6971
		public static readonly TweenSettings IgnoreTimeScaleCycleOnce = new TweenSettings(LoopType.None, false, null)
		{
			IsIgnoreTimeScale = true,
			LoopType = LoopType.CycleOnce
		};
	}
}
