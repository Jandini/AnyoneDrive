namespace AnyoneDrive
{
    public class OneDriveFileInfo : OneDriveItemInfo
    {
        internal OneDriveFileInfo(OneDriveItem item)
            : base(item)
        {
            Url = new Uri(item.ContentDownloadUrl);
            Size = item.Size;
        }

        public long Size { get; internal set; }
    }
}
