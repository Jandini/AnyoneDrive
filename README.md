# AnyoneDrive

[![Build](https://github.com/Jandini/AnyoneDrive/actions/workflows/build.yml/badge.svg)](https://github.com/Jandini/AnyoneDrive/actions/workflows/build.yml)
[![NuGet](https://github.com/Jandini/AnyoneDrive/actions/workflows/nuget.yml/badge.svg)](https://github.com/Jandini/AnyoneDrive/actions/workflows/nuget.yml)
---

AnyoneDrive provides access to OneDrive's public shares using a familiar approach inspired by FileInfo and DirectoryInfo.
The library offers the simplest form of read only access, requiring no authentication, to OneDrive's publicly available content.


```c#
var httpClient = new HttpClient();

var root = new OneDriveFolderInfo("https://1drv.ms/f/s!AuveTnis1UC4ylzSgCoulSxnQGB9");
var files = await root.GetFilesAsync(httpClient);
```



Please note that the link to the public folder must be accessible through public shares, and the link should include a share ID that begins with 's!'.


Created from [JandaBox](https://github.com/Jandini/JandaBox)
Box icon created by [Freepik - Flaticon](https://www.flaticon.com/free-icons/box)
