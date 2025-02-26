using System;
using AIProject.Definitions;

namespace AIProject
{
	// Token: 0x02000C1F RID: 3103
	public static class EnumExtensions
	{
		// Token: 0x06005FBD RID: 24509 RVA: 0x00285C8D File Offset: 0x0028408D
		public static bool Contains(this TimeZone source, TimeZone zone)
		{
			return source == (source | zone) && zone != (TimeZone)0;
		}

		// Token: 0x06005FBE RID: 24510 RVA: 0x00285CA2 File Offset: 0x002840A2
		public static bool Contains(this Temperature source, Temperature temperature)
		{
			return source == (source | temperature) && temperature != (Temperature)0;
		}

		// Token: 0x06005FBF RID: 24511 RVA: 0x00285CB7 File Offset: 0x002840B7
		public static bool Contains(this Rarelity source, Rarelity shape)
		{
			return source == (source | shape) && shape != Rarelity.None;
		}

		// Token: 0x06005FC0 RID: 24512 RVA: 0x00285CCC File Offset: 0x002840CC
		public static bool Contains(this Desire.Type source, Desire.Type desire)
		{
			return source == (source | desire) && desire != Desire.Type.None;
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x00285CE1 File Offset: 0x002840E1
		public static bool Contains(this EventType source, EventType type)
		{
			return source == (source | type) && type != (EventType)0;
		}

		// Token: 0x06005FC2 RID: 24514 RVA: 0x00285CF6 File Offset: 0x002840F6
		public static bool Contains(this ObjectLayer source, ObjectLayer layer)
		{
			return source == (source | layer) && layer != (ObjectLayer)0;
		}

		// Token: 0x06005FC3 RID: 24515 RVA: 0x00285D0B File Offset: 0x0028410B
		public static bool Contains(this PoseType source, PoseType poseType)
		{
			return source == (source | poseType) && poseType != (PoseType)0;
		}
	}
}
