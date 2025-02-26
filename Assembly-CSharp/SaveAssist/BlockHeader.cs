using System;
using System.IO;
using System.Text;

namespace SaveAssist
{
	// Token: 0x0200083F RID: 2111
	public class BlockHeader
	{
		// Token: 0x060035EF RID: 13807 RVA: 0x0013DF43 File Offset: 0x0013C343
		public void SetHeader(string _tagName, int _version, long _pos, long _size)
		{
			this.tagName = _tagName;
			this.tag = BlockHeader.ChangeStringToByte(this.tagName);
			this.version = _version;
			this.pos = _pos;
			this.size = _size;
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x0013DF73 File Offset: 0x0013C373
		public bool SaveHeader(BinaryWriter writer)
		{
			if (this.tag == null)
			{
				return false;
			}
			writer.Write(this.tag);
			writer.Write(this.version);
			writer.Write(this.pos);
			writer.Write(this.size);
			return true;
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x0013DFB4 File Offset: 0x0013C3B4
		public bool LoadHeader(BinaryReader reader)
		{
			this.tag = reader.ReadBytes(128);
			this.tagName = BlockHeader.ChangeByteToString(this.tag);
			this.version = reader.ReadInt32();
			this.pos = reader.ReadInt64();
			this.size = reader.ReadInt64();
			return true;
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x0013E008 File Offset: 0x0013C408
		public static int GetBlockHeaderSize()
		{
			return 148;
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x0013E010 File Offset: 0x0013C410
		public static byte[] ChangeStringToByte(string _tagName)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(_tagName);
			if (bytes.GetLength(0) > 128)
			{
				return null;
			}
			byte[] array = new byte[128];
			Buffer.BlockCopy(bytes, 0, array, 0, bytes.GetLength(0));
			return array;
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x0013E058 File Offset: 0x0013C458
		public static string ChangeByteToString(byte[] _tag)
		{
			return Encoding.UTF8.GetString(_tag);
		}

		// Token: 0x04003638 RID: 13880
		private const int tagSize = 128;

		// Token: 0x04003639 RID: 13881
		public string tagName = string.Empty;

		// Token: 0x0400363A RID: 13882
		public byte[] tag;

		// Token: 0x0400363B RID: 13883
		public int version;

		// Token: 0x0400363C RID: 13884
		public long pos;

		// Token: 0x0400363D RID: 13885
		public long size;
	}
}
