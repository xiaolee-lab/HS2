using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001204 RID: 4612
	public static class MapCommand
	{
		// Token: 0x02001205 RID: 4613
		public class EqualsInfo
		{
			// Token: 0x04007950 RID: 31056
			public Vector3 oldValue;

			// Token: 0x04007951 RID: 31057
			public Vector3 newValue;
		}

		// Token: 0x02001206 RID: 4614
		public class MoveEqualsCommand : ICommand
		{
			// Token: 0x06009722 RID: 38690 RVA: 0x003E81E3 File Offset: 0x003E65E3
			public MoveEqualsCommand(MapCommand.EqualsInfo _changeAmountInfo)
			{
				this.changeAmountInfo = _changeAmountInfo;
			}

			// Token: 0x06009723 RID: 38691 RVA: 0x003E81F2 File Offset: 0x003E65F2
			public void Do()
			{
				Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.pos = this.changeAmountInfo.newValue;
			}

			// Token: 0x06009724 RID: 38692 RVA: 0x003E8218 File Offset: 0x003E6618
			public void Redo()
			{
				this.Do();
			}

			// Token: 0x06009725 RID: 38693 RVA: 0x003E8220 File Offset: 0x003E6620
			public void Undo()
			{
				Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.pos = this.changeAmountInfo.oldValue;
			}

			// Token: 0x04007952 RID: 31058
			private MapCommand.EqualsInfo changeAmountInfo;
		}

		// Token: 0x02001207 RID: 4615
		public class RotationEqualsCommand : ICommand
		{
			// Token: 0x06009726 RID: 38694 RVA: 0x003E8246 File Offset: 0x003E6646
			public RotationEqualsCommand(MapCommand.EqualsInfo _changeAmountInfo)
			{
				this.changeAmountInfo = _changeAmountInfo;
			}

			// Token: 0x06009727 RID: 38695 RVA: 0x003E8255 File Offset: 0x003E6655
			public void Do()
			{
				Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot = this.changeAmountInfo.newValue;
			}

			// Token: 0x06009728 RID: 38696 RVA: 0x003E827B File Offset: 0x003E667B
			public void Redo()
			{
				this.Do();
			}

			// Token: 0x06009729 RID: 38697 RVA: 0x003E8283 File Offset: 0x003E6683
			public void Undo()
			{
				Singleton<Studio>.Instance.sceneInfo.mapInfo.ca.rot = this.changeAmountInfo.oldValue;
			}

			// Token: 0x04007953 RID: 31059
			private MapCommand.EqualsInfo changeAmountInfo;
		}
	}
}
