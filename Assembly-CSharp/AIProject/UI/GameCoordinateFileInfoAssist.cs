using System;
using System.Collections.Generic;
using System.IO;
using AIChara;

namespace AIProject.UI
{
	// Token: 0x02000E5B RID: 3675
	public class GameCoordinateFileInfoAssist
	{
		// Token: 0x0600744C RID: 29772 RVA: 0x00317604 File Offset: 0x00315A04
		private static void AddList(List<GameCoordinateFileInfo> list, List<string> coordList, string path, ref int idx)
		{
			List<string> list2 = ListPool<string>.Get();
			if (!coordList.IsNullOrEmpty<string>())
			{
				foreach (string item in coordList)
				{
					if (!list2.Contains(item))
					{
						list2.Add(item);
					}
				}
			}
			FolderAssist folderAssist = new FolderAssist();
			folderAssist.CreateFolderInfoEx(path, GameCoordinateFileInfoAssist._searchPatterns, true);
			int fileCount = folderAssist.GetFileCount();
			for (int i = 0; i < fileCount; i++)
			{
				ChaFileCoordinate chaFileCoordinate = new ChaFileCoordinate();
				FolderAssist.FileInfo fileInfo = folderAssist.lstFile[i];
				if (!chaFileCoordinate.LoadFile(fileInfo.FullPath))
				{
					int lastErrorCode = chaFileCoordinate.GetLastErrorCode();
				}
				else
				{
					bool flag = list2.Contains(Path.GetFileNameWithoutExtension(chaFileCoordinate.coordinateFileName));
					if (!flag)
					{
						list.Add(new GameCoordinateFileInfo
						{
							Index = idx++,
							FullPath = fileInfo.FullPath,
							FileName = fileInfo.FileName,
							Time = fileInfo.time
						});
					}
				}
			}
			ListPool<string>.Release(list2);
		}

		// Token: 0x0600744D RID: 29773 RVA: 0x00317754 File Offset: 0x00315B54
		private static void AddFileNameList(List<GameCoordinateFileInfo> list, List<string> fileNameList, string path, ref int idx)
		{
			if (fileNameList.IsNullOrEmpty<string>())
			{
				return;
			}
			int count = fileNameList.Count;
			for (int i = 0; i < count; i++)
			{
				ChaFileCoordinate chaFileCoordinate = new ChaFileCoordinate();
				string text = fileNameList[i];
				string text2 = string.Format("{0}{1}.png", path, text);
				if (!chaFileCoordinate.LoadFile(text2))
				{
					int lastErrorCode = chaFileCoordinate.GetLastErrorCode();
				}
				else
				{
					list.Add(new GameCoordinateFileInfo
					{
						Index = idx++,
						FullPath = text2,
						FileName = text,
						Time = File.GetLastWriteTime(text2)
					});
				}
			}
		}

		// Token: 0x0600744E RID: 29774 RVA: 0x003177FC File Offset: 0x00315BFC
		public static List<GameCoordinateFileInfo> CreateCoordinateFileInfoList(bool useMale, bool useFemale, List<string> filterList = null)
		{
			List<GameCoordinateFileInfo> list = new List<GameCoordinateFileInfo>();
			int num = 0;
			if (useMale)
			{
				string path = string.Format("{0}{1}", UserData.Path, "coordinate/male/");
				GameCoordinateFileInfoAssist.AddList(list, filterList, path, ref num);
			}
			if (useFemale)
			{
				string path2 = string.Format("{0}{1}", UserData.Path, "coordinate/female/");
				GameCoordinateFileInfoAssist.AddList(list, filterList, path2, ref num);
			}
			return list;
		}

		// Token: 0x0600744F RID: 29775 RVA: 0x0031785C File Offset: 0x00315C5C
		public static List<GameCoordinateFileInfo> CreateCoordinateFileInfoQueryList(List<string> fileNames)
		{
			List<GameCoordinateFileInfo> list = new List<GameCoordinateFileInfo>();
			int num = 0;
			string path = string.Format("{0}{1}", UserData.Path, "coordinate/female/");
			GameCoordinateFileInfoAssist.AddFileNameList(list, fileNames, path, ref num);
			return list;
		}

		// Token: 0x04005F0C RID: 24332
		private static readonly string[] _searchPatterns = new string[]
		{
			"*.png"
		};
	}
}
