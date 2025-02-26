using System;
using System.Collections;
using System.Threading;
using Ionic.Zip;
using UniRx;

// Token: 0x0200100B RID: 4107
public class FileZip
{
	// Token: 0x06008A12 RID: 35346 RVA: 0x003A1A84 File Offset: 0x0039FE84
	public void ZipSaveProgress(object sender, SaveProgressEventArgs e)
	{
		if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry)
		{
		}
	}

	// Token: 0x06008A13 RID: 35347 RVA: 0x003A1A94 File Offset: 0x0039FE94
	public IEnumerator FileZipCor(IObserver<byte[]> observer, byte[] srcBytes, string entryName)
	{
		byte[] data = null;
		yield return Observable.Start<byte[]>(() => this.zipAssist.SaveZipBytes(srcBytes, entryName, new EventHandler<SaveProgressEventArgs>(this.ZipSaveProgress))).StartAsCoroutine(delegate(byte[] x)
		{
			data = x;
		}, default(CancellationToken));
		if (data == null)
		{
			observer.OnError(new Exception("圧縮処理に失敗しました。"));
			yield break;
		}
		observer.OnNext(data);
		observer.OnCompleted();
		yield break;
	}

	// Token: 0x06008A14 RID: 35348 RVA: 0x003A1AC4 File Offset: 0x0039FEC4
	public IEnumerator FileUnzipCor(IObserver<byte[]> observer, byte[] srcBytes)
	{
		byte[] data = null;
		yield return Observable.Start<byte[]>(() => this.zipAssist.SaveUnzipFile(srcBytes, new EventHandler<SaveProgressEventArgs>(this.ZipSaveProgress))).StartAsCoroutine(delegate(byte[] x)
		{
			data = x;
		}, default(CancellationToken));
		if (data == null)
		{
			observer.OnError(new Exception("解凍処理に失敗しました。"));
			yield break;
		}
		observer.OnNext(data);
		observer.OnCompleted();
		yield break;
	}

	// Token: 0x04007068 RID: 28776
	private ZipAssist zipAssist = new ZipAssist();
}
