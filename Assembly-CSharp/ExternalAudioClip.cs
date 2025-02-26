using System;
using System.IO;
using System.Linq;
using uAudio.uAudio_backend;
using UnityEngine;

// Token: 0x02001138 RID: 4408
public class ExternalAudioClip
{
	// Token: 0x060091E4 RID: 37348 RVA: 0x003C9894 File Offset: 0x003C7C94
	public static AudioClip Load(string path, long SongLength, uAudio uAudio, ref string szErrorMs)
	{
		string extension = Path.GetExtension(path);
		if (extension == ".wav" || extension == ".WAV")
		{
			WaveHeader waveHeader = WaveHeader.ReadWaveHeader(path);
			float[] array = ExternalAudioClip.CreateRangedRawData(path, waveHeader.TrueWavBufIndex, waveHeader.TrueSamples, (int)waveHeader.Channels, (int)waveHeader.BitPerSample);
			if (array.Length == 0)
			{
				return null;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
			return ExternalAudioClip.CreateClip(fileNameWithoutExtension, array, waveHeader.TrueSamples, (int)waveHeader.Channels, waveHeader.Frequency);
		}
		else
		{
			if (extension == ".mp3" || extension == ".MP3")
			{
				mp3AudioClip._uAudio = uAudio;
				AudioClip result = mp3AudioClip.LoadMp3(path, SongLength);
				szErrorMs = mp3AudioClip.szErrorMs;
				return result;
			}
			return null;
		}
	}

	// Token: 0x060091E5 RID: 37349 RVA: 0x003C9954 File Offset: 0x003C7D54
	public static AudioClip CreateClip(string name, float[] rawData, int lengthSamples, int channels, int frequency)
	{
		AudioClip audioClip = AudioClip.Create(name, lengthSamples, channels, frequency, false);
		audioClip.SetData(rawData, 0);
		return audioClip;
	}

	// Token: 0x060091E6 RID: 37350 RVA: 0x003C9978 File Offset: 0x003C7D78
	public static float[] CreateRangedRawData(string path, int wavBufIndex, int samples, int channels, int bitPerSample)
	{
		byte[] array = File.ReadAllBytes(path);
		if (array.Length == 0)
		{
			return null;
		}
		return ExternalAudioClip.CreateRangedRawData(array, wavBufIndex, samples, channels, bitPerSample);
	}

	// Token: 0x060091E7 RID: 37351 RVA: 0x003C99A4 File Offset: 0x003C7DA4
	public static float[] CreateRangedRawData(byte[] data, int wavBufIndex, int samples, int channels, int bitPerSample)
	{
		float[] array = new float[samples * channels];
		int num = bitPerSample / 8;
		int num2 = wavBufIndex;
		try
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ExternalAudioClip.ByteToFloat(data, num2, bitPerSample);
				num2 += num;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString() + ": 対応してない音声ファイル");
			return Enumerable.Empty<float>().ToArray<float>();
		}
		return array;
	}

	// Token: 0x060091E8 RID: 37352 RVA: 0x003C9A28 File Offset: 0x003C7E28
	private static float ByteToFloat(byte[] data, int index, int bitPerSample)
	{
		float result;
		if (bitPerSample != 8)
		{
			if (bitPerSample != 16)
			{
				throw new Exception(bitPerSample + "bit is not supported.");
			}
			short num = BitConverter.ToInt16(data, index);
			result = (float)num * ExternalAudioClip.RangeValue16Bit;
		}
		else
		{
			result = (float)(data[index] - 128) * ExternalAudioClip.RangeValue8Bit;
		}
		return result;
	}

	// Token: 0x060091E9 RID: 37353 RVA: 0x003C9A92 File Offset: 0x003C7E92
	public static void LoadFile(string targetFileIN, string targetFile, ref bool _loadedTarget, uAudio uAudio)
	{
		if (!_loadedTarget || uAudio.targetFile != targetFile)
		{
			_loadedTarget = true;
			uAudio.LoadFile(targetFileIN);
		}
	}

	// Token: 0x04007621 RID: 30241
	public static readonly float RangeValue8Bit = 1f / Mathf.Pow(2f, 7f);

	// Token: 0x04007622 RID: 30242
	public static readonly float RangeValue16Bit = 1f / Mathf.Pow(2f, 15f);

	// Token: 0x04007623 RID: 30243
	public static readonly float RangeValue24Bit = 1f / Mathf.Pow(2f, 23f);

	// Token: 0x04007624 RID: 30244
	public static readonly float RangeValue32Bit = 1f / Mathf.Pow(2f, 31f);

	// Token: 0x04007625 RID: 30245
	public const int BaseConvertSamples = 20480;
}
