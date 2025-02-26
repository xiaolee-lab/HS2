using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x0200082B RID: 2091
public class FolderAssist
{
	// Token: 0x06003537 RID: 13623 RVA: 0x0013A3F7 File Offset: 0x001387F7
	public int GetFileCount()
	{
		return this.lstFile.Count;
	}

	// Token: 0x06003538 RID: 13624 RVA: 0x0013A404 File Offset: 0x00138804
	public bool CreateFolderInfo(string folder, string searchPattern, bool getFiles = true, bool clear = true)
	{
		if (clear)
		{
			this.lstFile.Clear();
		}
		if (!Directory.Exists(folder))
		{
			return false;
		}
		string[] array;
		if (getFiles)
		{
			array = Directory.GetFiles(folder, searchPattern);
		}
		else
		{
			array = Directory.GetDirectories(folder);
		}
		if (array.Length == 0)
		{
			return false;
		}
		foreach (string text in array)
		{
			FolderAssist.FileInfo fileInfo = new FolderAssist.FileInfo();
			fileInfo.FullPath = text;
			if (getFiles)
			{
				fileInfo.FileName = Path.GetFileNameWithoutExtension(text);
			}
			fileInfo.time = File.GetLastWriteTime(text);
			this.lstFile.Add(fileInfo);
		}
		return true;
	}

	// Token: 0x06003539 RID: 13625 RVA: 0x0013A4B0 File Offset: 0x001388B0
	public bool CreateFolderInfoEx(string folder, string[] searchPattern, bool clear = true)
	{
		if (clear)
		{
			this.lstFile.Clear();
		}
		if (!Directory.Exists(folder))
		{
			return false;
		}
		List<string> list = new List<string>();
		foreach (string searchPattern2 in searchPattern)
		{
			list.AddRange(Directory.GetFiles(folder, searchPattern2));
		}
		string[] array = list.ToArray();
		if (array.Length == 0)
		{
			return false;
		}
		foreach (string text in array)
		{
			FolderAssist.FileInfo fileInfo = new FolderAssist.FileInfo();
			fileInfo.FullPath = text;
			fileInfo.FileName = Path.GetFileNameWithoutExtension(text);
			fileInfo.time = File.GetLastWriteTime(text);
			this.lstFile.Add(fileInfo);
		}
		return true;
	}

	// Token: 0x0600353A RID: 13626 RVA: 0x0013A57C File Offset: 0x0013897C
	public int GetIndexFromFileName(string filename)
	{
		int num = 0;
		foreach (FolderAssist.FileInfo fileInfo in this.lstFile)
		{
			if (fileInfo.FileName == filename)
			{
				return num;
			}
			num++;
		}
		return -1;
	}

	// Token: 0x0600353B RID: 13627 RVA: 0x0013A5F4 File Offset: 0x001389F4
	public void SortName(bool ascend = true)
	{
		if (this.lstFile.Count == 0)
		{
			return;
		}
		if (ascend)
		{
			this.lstFile.Sort((FolderAssist.FileInfo a, FolderAssist.FileInfo b) => a.FileName.CompareTo(b.FileName));
		}
		else
		{
			this.lstFile.Sort((FolderAssist.FileInfo a, FolderAssist.FileInfo b) => b.FileName.CompareTo(a.FileName));
		}
	}

	// Token: 0x0600353C RID: 13628 RVA: 0x0013A670 File Offset: 0x00138A70
	public void SortDate(bool ascend = true)
	{
		if (this.lstFile.Count == 0)
		{
			return;
		}
		if (ascend)
		{
			this.lstFile.Sort((FolderAssist.FileInfo a, FolderAssist.FileInfo b) => a.time.CompareTo(b.time));
		}
		else
		{
			this.lstFile.Sort((FolderAssist.FileInfo a, FolderAssist.FileInfo b) => b.time.CompareTo(a.time));
		}
	}

	// Token: 0x040035C8 RID: 13768
	public List<FolderAssist.FileInfo> lstFile = new List<FolderAssist.FileInfo>();

	// Token: 0x0200082C RID: 2092
	public class FileInfo
	{
		// Token: 0x06003541 RID: 13633 RVA: 0x0013A735 File Offset: 0x00138B35
		public FileInfo()
		{
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x0013A754 File Offset: 0x00138B54
		public FileInfo(FolderAssist.FileInfo src)
		{
			this.FullPath = src.FullPath;
			this.FileName = src.FileName;
			this.time = src.time;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x0013A7A1 File Offset: 0x00138BA1
		public void Copy(FolderAssist.FileInfo src)
		{
			this.FullPath = src.FullPath;
			this.FileName = src.FileName;
			this.time = src.time;
		}

		// Token: 0x040035CD RID: 13773
		public string FullPath = string.Empty;

		// Token: 0x040035CE RID: 13774
		public string FileName = string.Empty;

		// Token: 0x040035CF RID: 13775
		public DateTime time;
	}
}
