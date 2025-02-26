using System;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x02000479 RID: 1145
public class UsCmd
{
	// Token: 0x06001532 RID: 5426 RVA: 0x00083BF1 File Offset: 0x00081FF1
	public UsCmd()
	{
		this._buffer = new byte[16384];
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x00083C09 File Offset: 0x00082009
	public UsCmd(byte[] given)
	{
		this._buffer = given;
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x06001534 RID: 5428 RVA: 0x00083C18 File Offset: 0x00082018
	public byte[] Buffer
	{
		get
		{
			return this._buffer;
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x06001535 RID: 5429 RVA: 0x00083C20 File Offset: 0x00082020
	public int WrittenLen
	{
		get
		{
			return this._writeOffset;
		}
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x00083C28 File Offset: 0x00082028
	public object ReadPrimitive<T>()
	{
		if (typeof(T) == typeof(string))
		{
			throw new UsCmdIOError(UsCmdIOErrorCode.TypeMismatched);
		}
		if (this._readOffset + Marshal.SizeOf(typeof(T)) > this._buffer.Length)
		{
			throw new UsCmdIOError(UsCmdIOErrorCode.ReadOverflow);
		}
		object result = UsGeneric.Convert<T>(this._buffer, this._readOffset);
		this._readOffset += Marshal.SizeOf(typeof(T));
		return result;
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x00083CB4 File Offset: 0x000820B4
	public string ReadString()
	{
		short num = this.ReadInt16();
		if (num == 0)
		{
			return string.Empty;
		}
		if (this._readOffset + (int)num > this._buffer.Length)
		{
			throw new UsCmdIOError(UsCmdIOErrorCode.ReadOverflow);
		}
		string @string = Encoding.Default.GetString(this._buffer, this._readOffset, (int)num);
		this._readOffset += (int)num;
		return @string;
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x00083D18 File Offset: 0x00082118
	public void WritePrimitive<T>(T value)
	{
		if (typeof(T) == typeof(string))
		{
			throw new UsCmdIOError(UsCmdIOErrorCode.TypeMismatched);
		}
		if (this._writeOffset + Marshal.SizeOf(typeof(T)) > this._buffer.Length)
		{
			throw new UsCmdIOError(UsCmdIOErrorCode.WriteOverflow);
		}
		byte[] array = UsGeneric.Convert<T>(value);
		if (array == null)
		{
			throw new UsCmdIOError(UsCmdIOErrorCode.TypeMismatched);
		}
		array.CopyTo(this._buffer, this._writeOffset);
		this._writeOffset += Marshal.SizeOf(typeof(T));
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x00083DB8 File Offset: 0x000821B8
	public void WriteStringStripped(string value, short stripLen)
	{
		if (string.IsNullOrEmpty(value))
		{
			this.WritePrimitive<short>(0);
		}
		else
		{
			string s = (value.Length <= (int)stripLen) ? value : value.Substring(0, (int)stripLen);
			byte[] bytes = Encoding.Default.GetBytes(s);
			this.WritePrimitive<short>((short)bytes.Length);
			bytes.CopyTo(this._buffer, this._writeOffset);
			this._writeOffset += bytes.Length;
		}
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x00083E2F File Offset: 0x0008222F
	public eNetCmd ReadNetCmd()
	{
		return (eNetCmd)this.ReadInt16();
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x00083E37 File Offset: 0x00082237
	public short ReadInt16()
	{
		return (short)this.ReadPrimitive<short>();
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x00083E44 File Offset: 0x00082244
	public int ReadInt32()
	{
		return (int)this.ReadPrimitive<int>();
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x00083E51 File Offset: 0x00082251
	public float ReadFloat()
	{
		return (float)this.ReadPrimitive<float>();
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x00083E5E File Offset: 0x0008225E
	public void WriteNetCmd(eNetCmd cmd)
	{
		this.WritePrimitive<short>((short)cmd);
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x00083E68 File Offset: 0x00082268
	public void WriteInt16(short value)
	{
		this.WritePrimitive<short>(value);
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x00083E71 File Offset: 0x00082271
	public void WriteInt32(int value)
	{
		this.WritePrimitive<int>(value);
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x00083E7A File Offset: 0x0008227A
	public void WriteFloat(float value)
	{
		this.WritePrimitive<float>(value);
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x00083E83 File Offset: 0x00082283
	public void WriteStringStripped(string value)
	{
		this.WriteStringStripped(value, 64);
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x00083E8E File Offset: 0x0008228E
	public void WriteString(string value)
	{
		this.WriteStringStripped(value, short.MaxValue);
	}

	// Token: 0x04001835 RID: 6197
	public const int STRIP_NAME_MAX_LEN = 64;

	// Token: 0x04001836 RID: 6198
	public const int BUFFER_SIZE = 16384;

	// Token: 0x04001837 RID: 6199
	private int _writeOffset;

	// Token: 0x04001838 RID: 6200
	private int _readOffset;

	// Token: 0x04001839 RID: 6201
	private byte[] _buffer;
}
