using System.Text.Json.Serialization;

namespace AnyoneDrive
{
    /// <summary>
    /// Represents a collection of items returned from OneDrive API.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    internal class OneDriveCollection<T>
    {
        /// <summary>
        /// Gets or sets the count of items in the collection.
        /// </summary>
        [JsonPropertyName("@odata.count")]
        public int OdataCount { get; set; }

        /// <summary>
        /// Gets or sets the actual collection of items.
        /// </summary>
        [JsonPropertyName("Value")]
        public T[] Collection { get; set; }
    }


    /// <summary>
    /// Represents a single item in OneDrive.
    /// </summary>
    internal class OneDriveItem
    {
        /// <summary>
        /// Gets or sets the URL for downloading content.
        /// </summary>
        [JsonPropertyName("@content.downloadUrl")]
        public string ContentDownloadUrl { get; set; }

        /// <summary>
        /// Gets or sets information about the creator.
        /// </summary>
        public OneDriveCreatedBy CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time of creation.
        /// </summary>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the change tag.
        /// </summary>
        public string CTag { get; set; }

        /// <summary>
        /// Gets or sets the entity tag for caching.
        /// </summary>
        public string ETag { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets information about the last modifier.
        /// </summary>
        public OneDriveLastModifiedBy LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the last modification.
        /// </summary>
        public DateTime LastModifiedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the reference to the parent item.
        /// </summary>
        public OneDriveParentReference ParentReference { get; set; }

        /// <summary>
        /// Gets or sets the size of the item.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the URL for accessing the item on the web.
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        /// Gets or sets information about the file (if it's a file).
        /// </summary>
        public OneDriveFileFacet File { get; set; }

        /// <summary>
        /// Gets or sets information about the folder (if it's a folder).
        /// </summary>
        public OneDriveFolderFacet Folder { get; set; }

        /// <summary>
        /// Gets or sets file system information.
        /// </summary>
        public OneDriveFileSystemInfo FileSystemInfo { get; set; }

        /// <summary>
        /// Gets or sets reactions to the item.
        /// </summary>
        public OneDriveReactions Reactions { get; set; }

        /// <summary>
        /// Gets or sets information about sharing settings.
        /// </summary>
        public OneDriveShared Shared { get; set; }
    }


    internal class OneDriveCreatedBy
    {
        public OneDriveApplication Application { get; set; }
        public OneDriveDevice Device { get; set; }
        public OneDriveUser User { get; set; }
    }

    internal class OneDriveApplication
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }
    }

    internal class OneDriveDevice
    {
        public string Id { get; set; }
    }

    internal class OneDriveUser
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }
    }

    internal class OneDriveLastModifiedBy
    {
        public OneDriveUser User { get; set; }
    }

    internal class OneDriveParentReference
    {
        public string DriveId { get; set; }
        public string DriveType { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string ShareId { get; set; }
    }

    internal class OneDriveFileFacet
    {
        public OneDriveHashes Hashes { get; set; }
        public string MimeType { get; set; }
    }

    internal class OneDriveHashes
    {
        public string QuickXorHash { get; set; }
        public string Sha1Hash { get; set; }
        public string Sha256Hash { get; set; }
    }

    internal class OneDriveFileSystemInfo
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
    }

    internal class OneDriveReactions
    {
        public int CommentCount { get; set; }
    }

    internal class OneDriveFolderFacet
    {
        public int ChildCount { get; set; }
        public OneDriveFolderView FolderView { get; set; }
        public string FolderType { get; set; }
    }

    internal class OneDriveFolderView
    {
        public string ViewType { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }

    }

    internal class OneDriveShared
    {
        public List<string> EffectiveRoles { get; set; }
        public OneDriveOwner Owner { get; set; }
        public string Scope { get; set; }
    }

    internal class OneDriveOwner
    {
        public OneDriveUser User { get; set; }
    }
}
