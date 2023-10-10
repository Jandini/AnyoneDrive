namespace AnyoneDrive
{
    /// <summary>
    /// Represents information about a file or folder in OneDrive.
    /// </summary>
    public class OneDriveItemInfo
    {
        /// <summary>
        /// Gets or sets the name of the file or folder.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets or sets the URI to the content of the file or directory.
        /// </summary>
        public Uri Url { get; internal set; }

        /// <summary>
        /// Gets or sets the date and time when the file or folder was created.
        /// </summary>
        public DateTime CreatedDateTime { get; internal set; }

        /// <summary>
        /// Gets or sets the date and time when the file or folder was last updated.
        /// </summary>
        public DateTime LastUpdatedDateTime { get; internal set; }


        internal OneDriveItemInfo()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneDriveItemInfo"/> class based on a OneDriveItem object.
        /// </summary>
        /// <param name="item">The OneDriveItem object from which to extract information.</param>
        internal OneDriveItemInfo(OneDriveItem item)
        {          
            Name = item.Name;
            CreatedDateTime = item.CreatedDateTime;
            LastUpdatedDateTime = item.LastModifiedDateTime;
        }
    }
}