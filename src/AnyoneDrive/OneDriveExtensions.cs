using System.Text.Json;
using System.Text.RegularExpressions;

namespace AnyoneDrive
{
    public static class OneDriveExtensions
    {
        /// <summary>
        /// Retrieves a collection of OneDrive items from a shared folder based on the provided folder information.
        /// </summary>
        /// <param name="folderInfo">Information about the shared folder, including its URL.</param>
        /// <param name="httpClient">The HttpClient instance used to make the API request.</param>
        /// <param name="cancellationToken">A CancellationToken for canceling the asynchronous operation.</param>
        /// <returns>A collection of OneDrive items representing the contents of the shared folder.</returns>
        private static async Task<OneDriveCollection<OneDriveItem>> GetSharesAsync(OneDriveFolderInfo folderInfo, HttpClient httpClient, CancellationToken cancellationToken)
        {
            var match = Regex.Match(folderInfo.Url.ToString(), @"https:\/\/1drv\.ms\/f\/(.*?)(\?e=|$)");

            if (match.Success && match.Groups.Count <= 1)
                throw new Exception($"Cannot extract share id from {folderInfo.Url}.");

            string apiUrl = $"https://api.onedrive.com/v1.0/shares/{match.Groups[1].Value}/root/children";
           
            using var response = await httpClient.GetAsync(apiUrl, cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<OneDriveCollection<OneDriveItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }


        /// <summary>
        /// Retrieves a collection of OneDrive items within the specified folder.
        /// </summary>
        /// <param name="folderInfo">Information about the OneDrive folder to retrieve items from.</param>
        /// <param name="httpClient">The HttpClient instance used to make the API request.</param>
        /// <param name="cancellationToken">A CancellationToken for canceling the asynchronous operation.</param>
        /// <returns>An array of OneDriveItemInfo representing the items within the folder.</returns>
        public static async Task<OneDriveItemInfo[]> GetItemsAsync(this OneDriveFolderInfo folderInfo, HttpClient httpClient, CancellationToken cancellationToken) => (await GetSharesAsync(folderInfo, httpClient, cancellationToken)).Collection.Select(item => item.Folder != null ? new OneDriveFolderInfo(item) : (OneDriveItemInfo)new OneDriveFileInfo(item)).ToArray();



        /// <summary>
        /// Retrieves a collection of OneDrive files within the specified folder.
        /// </summary>
        /// <param name="folderInfo">Information about the OneDrive folder to retrieve files from.</param>
        /// <param name="httpClient">The HttpClient instance used to make the API request.</param>
        /// <param name="cancellationToken">A CancellationToken for canceling the asynchronous operation.</param>
        /// <returns>An array of OneDriveFileInfo representing the files within the folder.</returns>
        public static async Task<OneDriveFileInfo[]> GetFilesAsync(this OneDriveFolderInfo folderInfo, HttpClient httpClient, CancellationToken cancellationToken) => (await GetSharesAsync(folderInfo, httpClient, cancellationToken)).Collection.Where(item => item.File != null).Select(item => new OneDriveFileInfo(item)).ToArray();



        /// <summary>
        /// Retrieves a collection of OneDrive folders within the specified folder.
        /// </summary>
        /// <param name="folderInfo">Information about the OneDrive folder to retrieve folders from.</param>
        /// <param name="httpClient">The HttpClient instance used to make the API request.</param>
        /// <param name="cancellationToken">A CancellationToken for canceling the asynchronous operation.</param>
        /// <returns>An array of OneDriveFolderInfo representing the folders within the folder.</returns>
        public static async Task<OneDriveFolderInfo[]> GetFoldersAsync(this OneDriveFolderInfo folderInfo, HttpClient httpClient, CancellationToken cancellationToken) => (await GetSharesAsync(folderInfo, httpClient, cancellationToken)).Collection.Where(item => item.Folder != null).Select(item => new OneDriveFolderInfo(item)).ToArray();



        /// <summary>
        /// Retrieves a stream containing the content of the specified OneDrive file.
        /// </summary>
        /// <param name="fileInfo">Information about the OneDrive file to retrieve the content of.</param>
        /// <param name="httpClient">The HttpClient instance used to make the API request.</param>
        /// <param name="cancellationToken">A CancellationToken for canceling the asynchronous operation.</param>
        /// <returns>
        /// A stream containing the content of the OneDrive file. 
        /// The stream should be read in chunks to efficiently process the file content.
        /// </returns>
        public static async Task<Stream> GetStreamAsync(this OneDriveFileInfo fileInfo, HttpClient httpClient, CancellationToken cancellationToken)
        {
            var response = await httpClient.GetAsync(fileInfo.Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync();
        }



        /// <summary>
        /// Reads a block of bytes from the provided stream asynchronously.
        /// </summary>
        /// <param name="stream">The input stream to read data from.</param>
        /// <param name="blockSize">The size of the block to read from the stream.</param>
        /// <returns>
        /// An array of bytes containing the read block of data.
        /// If the end of the stream is reached or no data is available, an empty byte array is returned.
        /// </returns>
        public static async Task<byte[]> ReadBlockAsync(this Stream stream, int blockSize)
        {
            // Create a buffer to hold the block of data.
            byte[] buffer = new byte[blockSize];

            int blockBytes = 0;
            int bytes;

            // Read data from the stream in chunks until the block size is reached.
            while ((bytes = await stream.ReadAsync(buffer.AsMemory(blockBytes, blockSize - blockBytes))) > 0)
            {
                blockBytes += bytes;

                // If the block size is reached, return the buffer.
                if (blockBytes == blockSize)
                    return buffer;
            }

            // If there are remaining bytes, return a trimmed array.
            if (blockBytes > 0)
                return buffer.AsSpan()[..blockBytes].ToArray();

            // If no data is available or the end of the stream is reached, return an empty byte array.
            return Array.Empty<byte>();
        }
    }
}
