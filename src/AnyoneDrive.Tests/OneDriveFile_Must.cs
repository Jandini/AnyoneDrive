namespace AnyoneDrive.Tests
{
    public class OneDriveFile_Must
    {


      
        [Fact]
        public async void Get_Stream()
        {
            var httpClient = new HttpClient();
            var root = new OneDriveFolderInfo("https://1drv.ms/f/s!AuveTnis1UC4ylwrkuYqhdobhUGy?e=aq9M7h");
            var folders = await root.GetFoldersAsync(httpClient, CancellationToken.None);

            Assert.NotNull(folders);
            
            var files = await folders.First().GetFilesAsync(httpClient, CancellationToken.None);
            Assert.Equal(5, files.Length);

            using var stream = await files[0].GetStreamAsync(httpClient, CancellationToken.None);           
        }


        [Fact]
        public async void Read_Stream()
        {
            var httpClient = new HttpClient();
            var root = new OneDriveFolderInfo("https://1drv.ms/f/s!AuveTnis1UC4ylwrkuYqhdobhUGy?e=aq9M7h");
            var folders = await root.GetFoldersAsync(httpClient, CancellationToken.None);

            Assert.NotNull(folders);

            var files = await folders.First().GetFilesAsync(httpClient, CancellationToken.None);
                      
            using var stream = await files[0].GetStreamAsync(httpClient, CancellationToken.None);

            byte[] buffer = await stream.ReadBlockAsync(65536);
            Assert.Equal(5959, buffer.Length);
            
        }
    }
}