namespace AnyoneDrive
{
    public class OneDriveFolderInfo : OneDriveItemInfo
    {
        internal OneDriveFolderInfo(OneDriveItem item)
            : base(item)
        {
            Url = new Uri(item.WebUrl);
        }

        public OneDriveFolderInfo(string shareUrl)            
        {
            Url = new Uri(shareUrl);
        }

        public OneDriveFolderInfo(Uri shareUrl)
        {
            Url = shareUrl;
        }
    }
}
