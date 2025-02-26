using System;

namespace Studio
{
	// Token: 0x020011F6 RID: 4598
	public static class AnimeCommand
	{
		// Token: 0x020011F7 RID: 4599
		public class SpeedCommand : ICommand
		{
			// Token: 0x060096F8 RID: 38648 RVA: 0x003E7ADD File Offset: 0x003E5EDD
			public SpeedCommand(int[] _arrayKey, float _speed, float[] _oldSpeed)
			{
				this.arrayKey = _arrayKey;
				this.speed = _speed;
				this.oldSpeed = _oldSpeed;
			}

			// Token: 0x060096F9 RID: 38649 RVA: 0x003E7AFC File Offset: 0x003E5EFC
			public void Do()
			{
				for (int i = 0; i < this.arrayKey.Length; i++)
				{
					ObjectCtrlInfo ctrlInfo = Studio.GetCtrlInfo(this.arrayKey[i]);
					if (ctrlInfo != null)
					{
						ctrlInfo.animeSpeed = this.speed;
					}
				}
			}

			// Token: 0x060096FA RID: 38650 RVA: 0x003E7B47 File Offset: 0x003E5F47
			public void Redo()
			{
				this.Do();
			}

			// Token: 0x060096FB RID: 38651 RVA: 0x003E7B50 File Offset: 0x003E5F50
			public void Undo()
			{
				for (int i = 0; i < this.arrayKey.Length; i++)
				{
					ObjectCtrlInfo ctrlInfo = Studio.GetCtrlInfo(this.arrayKey[i]);
					if (ctrlInfo != null)
					{
						ctrlInfo.animeSpeed = this.oldSpeed[i];
					}
				}
			}

			// Token: 0x04007939 RID: 31033
			private int[] arrayKey;

			// Token: 0x0400793A RID: 31034
			private float speed;

			// Token: 0x0400793B RID: 31035
			private float[] oldSpeed;
		}

		// Token: 0x020011F8 RID: 4600
		public class PatternCommand : ICommand
		{
			// Token: 0x060096FC RID: 38652 RVA: 0x003E7B9D File Offset: 0x003E5F9D
			public PatternCommand(int[] _arrayKey, float _value, float[] _oldvalue)
			{
				this.arrayKey = _arrayKey;
				this.value = _value;
				this.oldvalue = _oldvalue;
			}

			// Token: 0x060096FD RID: 38653 RVA: 0x003E7BBC File Offset: 0x003E5FBC
			public void Do()
			{
				for (int i = 0; i < this.arrayKey.Length; i++)
				{
					OCIChar ocichar = Studio.GetCtrlInfo(this.arrayKey[i]) as OCIChar;
					if (ocichar != null)
					{
						ocichar.animePattern = this.value;
					}
				}
			}

			// Token: 0x060096FE RID: 38654 RVA: 0x003E7C0C File Offset: 0x003E600C
			public void Redo()
			{
				this.Do();
			}

			// Token: 0x060096FF RID: 38655 RVA: 0x003E7C14 File Offset: 0x003E6014
			public void Undo()
			{
				for (int i = 0; i < this.arrayKey.Length; i++)
				{
					OCIChar ocichar = Studio.GetCtrlInfo(this.arrayKey[i]) as OCIChar;
					if (ocichar != null)
					{
						ocichar.animePattern = this.oldvalue[i];
					}
				}
			}

			// Token: 0x0400793C RID: 31036
			private int[] arrayKey;

			// Token: 0x0400793D RID: 31037
			private float value;

			// Token: 0x0400793E RID: 31038
			private float[] oldvalue;
		}

		// Token: 0x020011F9 RID: 4601
		public class OptionParamCommand : ICommand
		{
			// Token: 0x06009700 RID: 38656 RVA: 0x003E7C66 File Offset: 0x003E6066
			public OptionParamCommand(int[] _arrayKey, float _value, float[] _oldvalue, int _kind)
			{
				this.arrayKey = _arrayKey;
				this.value = _value;
				this.oldvalue = _oldvalue;
				this.kind = _kind;
			}

			// Token: 0x06009701 RID: 38657 RVA: 0x003E7C8C File Offset: 0x003E608C
			public void Do()
			{
				for (int i = 0; i < this.arrayKey.Length; i++)
				{
					OCIChar ocichar = Studio.GetCtrlInfo(this.arrayKey[i]) as OCIChar;
					if (ocichar != null)
					{
						if (this.kind == 0)
						{
							ocichar.animeOptionParam1 = this.value;
						}
						else
						{
							ocichar.animeOptionParam2 = this.value;
						}
					}
				}
			}

			// Token: 0x06009702 RID: 38658 RVA: 0x003E7CF8 File Offset: 0x003E60F8
			public void Redo()
			{
				this.Do();
			}

			// Token: 0x06009703 RID: 38659 RVA: 0x003E7D00 File Offset: 0x003E6100
			public void Undo()
			{
				for (int i = 0; i < this.arrayKey.Length; i++)
				{
					OCIChar ocichar = Studio.GetCtrlInfo(this.arrayKey[i]) as OCIChar;
					if (ocichar != null)
					{
						if (this.kind == 0)
						{
							ocichar.animeOptionParam1 = this.oldvalue[i];
						}
						else
						{
							ocichar.animeOptionParam2 = this.oldvalue[i];
						}
					}
				}
			}

			// Token: 0x0400793F RID: 31039
			private int[] arrayKey;

			// Token: 0x04007940 RID: 31040
			private float value;

			// Token: 0x04007941 RID: 31041
			private float[] oldvalue;

			// Token: 0x04007942 RID: 31042
			private int kind;
		}
	}
}
