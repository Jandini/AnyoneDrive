namespace AnyoneDrive.Tests
{
    public class OneDriveFile_Must
    {

        const string TEST_SHARE = "https://1drv.ms/f/s!AuveTnis1UC4ylzSgCoulSxnQGB9";


        [Fact]
        public async void Get_Stream()
        {
            var httpClient = new HttpClient();
            var root = new OneDriveFolderInfo(TEST_SHARE);
            var folders = await root.GetFoldersAsync(httpClient);

            Assert.NotNull(folders);
            
            var files = await folders.First().GetFilesAsync(httpClient);
            Assert.Equal(5, files.Length);

            using var stream = await files[0].GetStreamAsync(httpClient);
        }


        [Fact]
        public async void Read_Stream()
        {
            var httpClient = new HttpClient();
            var root = new OneDriveFolderInfo(TEST_SHARE);
            var folders = await root.GetFoldersAsync(httpClient);

            Assert.NotNull(folders);

            var files = await folders.First().GetFilesAsync(httpClient);
                      
            using var stream = await files[0].GetStreamAsync(httpClient);

            byte[] buffer = await stream.ReadBlockAsync(65536);
            Assert.Equal(5959, buffer.Length);
            
        }
    }
}