namespace AnyoneDrive.Tests
{
    public class OneDriveFolder_Must
    {

        const string TEST_SHARE = "https://1drv.ms/f/s!AuveTnis1UC4ylwrkuYqhdobhUGy?e=aq9M7h";

        [Fact]
        public async void Get_Two_Items()
        {

            var root = new OneDriveFolderInfo(TEST_SHARE);
            var items = await root.GetItemsAsync(new HttpClient(), CancellationToken.None);

            Assert.Equal(2, items.Length);
        }

        [Fact]
        public async void Get_One_File()
        {

            var root = new OneDriveFolderInfo(TEST_SHARE);
            var items = await root.GetFilesAsync(new HttpClient(), CancellationToken.None);

            Assert.Single(items);
        }


        [Fact]
        public async void Get_One_Folder()
        {

            var root = new OneDriveFolderInfo(TEST_SHARE);
            var items = await root.GetFoldersAsync(new HttpClient(), CancellationToken.None);

            Assert.Single(items);
        }


        [Fact]
        public async void Get_SubFolder_Files()
        {
            var httpClient = new HttpClient();
            var root = new OneDriveFolderInfo(TEST_SHARE);
            var folders = await root.GetFoldersAsync(httpClient, CancellationToken.None);

            Assert.NotNull(folders);
            Assert.Single(folders);
            Assert.Equal("Single folder", folders[0].Name);

            var files = await folders.First().GetFilesAsync(httpClient, CancellationToken.None);

            Assert.Equal(5, files.Length);
        }



        [Fact]
        public async void Read_File()
        {
            var httpClient = new HttpClient();
            var root = new OneDriveFolderInfo(TEST_SHARE);
            var folders = await root.GetFoldersAsync(httpClient, CancellationToken.None);

            Assert.NotNull(folders);
            
            var files = await folders.First().GetFilesAsync(httpClient, CancellationToken.None);

            Assert.Equal(5, files.Length);

            using var stream = await files[0].GetStreamAsync(httpClient, CancellationToken.None);
        }
    }
}