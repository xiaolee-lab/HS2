using System;
using System.IO;
using NLayer;
using uAudio.uAudio_backend;
using uAudioDemo.Mp3StreamingDemo;
using UnityEngine;

// Token: 0x0200113A RID: 4410
internal class mp3AudioClip
{
	// Token: 0x17001F68 RID: 8040
	// (get) Token: 0x06009210 RID: 37392 RVA: 0x003C9F05 File Offset: 0x003C8305
	// (set) Token: 0x06009211 RID: 37393 RVA: 0x003C9F0C File Offset: 0x003C830C
	public static string szErrorMs { get; set; }

	// Token: 0x06009212 RID: 37394 RVA: 0x003C9F14 File Offset: 0x003C8314
	public static AudioClip LoadMp3(string targetFile, long SongLength)
	{
		mp3AudioClip.SongReadLoop = new AudioClip.PCMReaderCallback(mp3AudioClip.Song_Stream_Loop);
		Stream sourceStream = File.OpenRead(targetFile);
		if (mp3AudioClip.readFullyStream != null)
		{
			mp3AudioClip.readFullyStream.Dispose();
		}
		mp3AudioClip.readFullyStream = new ReadFullyStream(sourceStream);
		mp3AudioClip.readFullyStream.stream_CanSeek = true;
		byte[] array = new byte[1024];
		MpegFile mpegFile = new MpegFile(mp3AudioClip.readFullyStream, true);
		mpegFile.ReadSamples(array, 0, array.Length);
		mp3AudioClip.playbackDevice = mpegFile;
		int lengthSamples;
		if (SongLength > 2147483647L)
		{
			UnityEngine.Debug.LogWarning("uAudioPlayer - Song size over size on int #4sgh54h45h45");
			lengthSamples = int.MaxValue;
		}
		else
		{
			lengthSamples = (int)SongLength;
		}
		return AudioClip.Create("uAudio_song", lengthSamples, mpegFile.WaveFormat.Channels, mpegFile.WaveFormat.SampleRate, true, mp3AudioClip.SongReadLoop);
	}

	// Token: 0x06009213 RID: 37395 RVA: 0x003C9FDC File Offset: 0x003C83DC
	private static void Song_Stream_Loop(float[] data)
	{
		try
		{
			if (!mp3AudioClip.SongDone)
			{
				int num = mp3AudioClip._uAudio.uwa.audioPlayback.inputStream.Read(data, 0, data.Length);
				if (num <= 0)
				{
					mp3AudioClip.SongDone = true;
				}
			}
			else
			{
				mp3AudioClip.flare_SongEnd = true;
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("#trg56hgtyhty" + ex.Message);
			UnityEngine.Debug.LogError("Decode Error #8f76s8dsvsd");
			mp3AudioClip.szErrorMs = "Failed Play Sound\0";
		}
	}

	// Token: 0x04007636 RID: 30262
	private static AudioClip.PCMReaderCallback SongReadLoop;

	// Token: 0x04007637 RID: 30263
	private static ReadFullyStream readFullyStream;

	// Token: 0x04007638 RID: 30264
	private static MpegFile playbackDevice;

	// Token: 0x04007639 RID: 30265
	public static uAudio _uAudio;

	// Token: 0x0400763A RID: 30266
	public static bool SongDone;

	// Token: 0x0400763B RID: 30267
	public static bool flare_SongEnd;
}
