using System;
using System.IO;
using uAudio.uAudio_backend;
using UnityEngine;

// Token: 0x02000844 RID: 2116
public class CustomSound : MonoBehaviour
{
	// Token: 0x170009BD RID: 2493
	// (get) Token: 0x06003604 RID: 13828 RVA: 0x0013E767 File Offset: 0x0013CB67
	// (set) Token: 0x06003605 RID: 13829 RVA: 0x0013E76F File Offset: 0x0013CB6F
	public Action<PlayBackState> sendPlaybackState
	{
		get
		{
			return this._sendPlaybackState;
		}
		set
		{
			this._sendPlaybackState = value;
		}
	}

	// Token: 0x170009BE RID: 2494
	// (get) Token: 0x06003606 RID: 13830 RVA: 0x0013E778 File Offset: 0x0013CB78
	public uAudio UAudio
	{
		get
		{
			if (CustomSound._uAudio == null)
			{
				CustomSound._uAudio = new uAudio();
				CustomSound._uAudio.SetAudioFile(this.targetFile);
				CustomSound._uAudio.Volume = 1f;
				CustomSound._uAudio.sendPlaybackState = delegate(PlayBackState c)
				{
					this._sendPlaybackState(c);
				};
			}
			return CustomSound._uAudio;
		}
	}

	// Token: 0x170009BF RID: 2495
	// (get) Token: 0x06003607 RID: 13831 RVA: 0x0013E7D3 File Offset: 0x0013CBD3
	// (set) Token: 0x06003608 RID: 13832 RVA: 0x0013E7FB File Offset: 0x0013CBFB
	public TimeSpan CurrentTime
	{
		get
		{
			if (CustomSound._uAudio != null && this.State != PlayBackState.Stopped)
			{
				return CustomSound._uAudio.CurrentTime;
			}
			return this.endSongTime;
		}
		set
		{
			if (CustomSound._uAudio != null)
			{
				CustomSound._uAudio.CurrentTime = value;
			}
		}
	}

	// Token: 0x06003609 RID: 13833 RVA: 0x0013E812 File Offset: 0x0013CC12
	private void Start()
	{
		this.szListFileName = contents.AudioFileDirectory + "/BGMList.dat";
	}

	// Token: 0x0600360A RID: 13834 RVA: 0x0013E829 File Offset: 0x0013CC29
	private void Update()
	{
		if (mp3AudioClip.flare_SongEnd)
		{
			mp3AudioClip.flare_SongEnd = false;
			this.SongEnd();
			mp3AudioClip.flare_SongEnd = false;
		}
	}

	// Token: 0x0600360B RID: 13835 RVA: 0x0013E847 File Offset: 0x0013CC47
	public void LoadFile(string szTargetName)
	{
		if (szTargetName != this.targetFile)
		{
			this.SongEnd();
		}
		this.targetFile = szTargetName;
		ExternalAudioClip.LoadFile(szTargetName, this.targetFile, ref this._loadedTarget, this.UAudio);
	}

	// Token: 0x0600360C RID: 13836 RVA: 0x0013E880 File Offset: 0x0013CC80
	public void SongPlay()
	{
		if (this.State != PlayBackState.Playing)
		{
			this.State = PlayBackState.Playing;
			try
			{
				mp3AudioClip.SongDone = false;
				mp3AudioClip.flare_SongEnd = false;
				this.UAudio.targetFile = this.targetFile;
				if (this.myAudioSource.clip == null)
				{
					if (this.UAudio.LoadMainOutputStream())
					{
						long num = (long)this.UAudio.SongLength;
						string text = null;
						this.myAudioSource.clip = ExternalAudioClip.Load(this.targetFile, (long)this.UAudio.SongLength, this.UAudio, ref text);
						this.CurrentTime = TimeSpan.Zero;
						try
						{
							if (this.sendPlaybackState != null)
							{
								this.sendPlaybackState(PlayBackState.Playing);
							}
						}
						catch
						{
							UnityEngine.Debug.LogWarning("theAudioStream_sendStartLoopPump #32fw46hw465h45h");
						}
					}
					else
					{
						this.myAudioSource.clip = null;
					}
				}
				if (this.myAudioSource.clip != null)
				{
					if (!this.myAudioSource.isPlaying)
					{
						this.myAudioSource.Play();
					}
				}
				else
				{
					this.State = PlayBackState.Stopped;
				}
			}
			catch (Exception message)
			{
				this.State = PlayBackState.Stopped;
				UnityEngine.Debug.LogWarning("uAudioPlayer - Play #j356j536j356j56j");
				UnityEngine.Debug.LogWarning(message);
			}
		}
	}

	// Token: 0x0600360D RID: 13837 RVA: 0x0013E9FC File Offset: 0x0013CDFC
	public void SongEnd()
	{
		if (this.State == PlayBackState.Stopped)
		{
			return;
		}
		try
		{
			this.endSongTime = this.CurrentTime;
			this.myAudioSource.Stop();
			CustomSound._uAudio.Stop();
			this.myAudioSource.clip = null;
			this._loadedTarget = false;
			this.State = PlayBackState.Stopped;
			try
			{
				if (this.sendPlaybackState != null)
				{
					this.sendPlaybackState(PlayBackState.Stopped);
				}
			}
			catch
			{
				UnityEngine.Debug.LogWarning("sendPlaybackState #897j8h2432a1q");
			}
		}
		catch
		{
			throw new Exception("Song end #7cgf87dcf7sd8csd");
		}
	}

	// Token: 0x0600360E RID: 13838 RVA: 0x0013EAAC File Offset: 0x0013CEAC
	public void BGMListToFile(string[] SongPath)
	{
		this.szListFileName = contents.AudioFileDirectory + "/BGMList.dat";
		this.szListFileName.Remove(0, Application.dataPath.ToString().Length - "Assets".Length);
		FileInfo fileInfo = new FileInfo(this.szListFileName);
		try
		{
			StreamWriter streamWriter = fileInfo.CreateText();
			for (int i = 0; i < SongPath.Length; i++)
			{
				streamWriter.WriteLine(SongPath[i]);
			}
			streamWriter.Flush();
			streamWriter.Close();
		}
		catch
		{
			UnityEngine.Debug.LogWarning("Failed CreateText");
		}
	}

	// Token: 0x0600360F RID: 13839 RVA: 0x0013EB58 File Offset: 0x0013CF58
	public void FileToBGMList(string[] SongPath)
	{
		this.szListFileName = contents.AudioFileDirectory + "/BGMList.dat";
		this.szListFileName.Remove(0, Application.dataPath.ToString().Length - "Assets".Length);
		FileInfo fileInfo = new FileInfo(this.szListFileName);
		try
		{
			StreamReader streamReader = fileInfo.OpenText();
			for (int i = 0; i < SongPath.Length; i++)
			{
				SongPath[i] = streamReader.ReadLine();
				if (SongPath[i] == null)
				{
					SongPath[i] = "Empty";
				}
			}
			streamReader.Close();
		}
		catch
		{
			UnityEngine.Debug.LogWarning("Failed FileOpen Error");
		}
	}

	// Token: 0x06003610 RID: 13840 RVA: 0x0013EC0C File Offset: 0x0013D00C
	private void OnApplicationQuit()
	{
		this.Dispose();
	}

	// Token: 0x06003611 RID: 13841 RVA: 0x0013EC14 File Offset: 0x0013D014
	public void Dispose()
	{
		if (CustomSound._uAudio != null)
		{
			CustomSound._uAudio.Dispose();
			CustomSound._uAudio = null;
		}
		this._loadedTarget = false;
	}

	// Token: 0x06003612 RID: 13842 RVA: 0x0013EC37 File Offset: 0x0013D037
	public void Resume()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0400364F RID: 13903
	public AudioSource myAudioSource;

	// Token: 0x04003650 RID: 13904
	public PlayBackState State;

	// Token: 0x04003651 RID: 13905
	public static uAudio _uAudio;

	// Token: 0x04003652 RID: 13906
	public string targetFile;

	// Token: 0x04003653 RID: 13907
	private string szListFileName;

	// Token: 0x04003654 RID: 13908
	private Action<PlayBackState> _sendPlaybackState;

	// Token: 0x04003655 RID: 13909
	private bool _loadedTarget;

	// Token: 0x04003656 RID: 13910
	private TimeSpan endSongTime = TimeSpan.Zero;
}
