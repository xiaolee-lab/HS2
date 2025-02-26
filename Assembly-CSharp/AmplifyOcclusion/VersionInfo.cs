using System;
using UnityEngine;

namespace AmplifyOcclusion
{
	// Token: 0x020004D7 RID: 1239
	[Serializable]
	public class VersionInfo
	{
		// Token: 0x060016E3 RID: 5859 RVA: 0x0008E4C5 File Offset: 0x0008C8C5
		private VersionInfo()
		{
			this.m_major = 2;
			this.m_minor = 0;
			this.m_release = 5;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0008E4E2 File Offset: 0x0008C8E2
		private VersionInfo(byte major, byte minor, byte release)
		{
			this.m_major = (int)major;
			this.m_minor = (int)minor;
			this.m_release = (int)release;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0008E4FF File Offset: 0x0008C8FF
		public static string StaticToString()
		{
			return string.Format("{0}.{1}.{2}", 2, 0, 5) + string.Empty;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0008E527 File Offset: 0x0008C927
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}", this.m_major, this.m_minor, this.m_release) + string.Empty;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0008E55E File Offset: 0x0008C95E
		public int Number
		{
			get
			{
				return this.m_major * 100 + this.m_minor * 10 + this.m_release;
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0008E57A File Offset: 0x0008C97A
		public static VersionInfo Current()
		{
			return new VersionInfo(2, 0, 5);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0008E584 File Offset: 0x0008C984
		public static bool Matches(VersionInfo version)
		{
			return version.m_major == 2 && version.m_minor == 0 && 5 == version.m_release;
		}

		// Token: 0x04001A26 RID: 6694
		public const byte Major = 2;

		// Token: 0x04001A27 RID: 6695
		public const byte Minor = 0;

		// Token: 0x04001A28 RID: 6696
		public const byte Release = 5;

		// Token: 0x04001A29 RID: 6697
		public const byte Revision = 0;

		// Token: 0x04001A2A RID: 6698
		[SerializeField]
		private int m_major;

		// Token: 0x04001A2B RID: 6699
		[SerializeField]
		private int m_minor;

		// Token: 0x04001A2C RID: 6700
		[SerializeField]
		private int m_release;
	}
}
