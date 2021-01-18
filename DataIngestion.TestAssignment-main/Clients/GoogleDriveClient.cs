using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment.Clients
{
    /// <summary>
    /// Deprecated. Explored a GoogleDrive API approach, but didn't use it
    /// </summary>
    public class GoogleDriveClient
    {
        public async Task<byte[]> DownloadFile(DriveService service, string fileId)
        {
            byte[] saveTo = null;
            var request = service.Files.Get(fileId);

            using (var stream = new MemoryStream())
            {
                // Add a handler which will be notified on progress changes.
                // It will notify on each chunk download and when the
                // download is completed or failed.
                request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case Google.Apis.Download.DownloadStatus.Downloading:
                            {
                                Console.WriteLine(progress.BytesDownloaded);
                                break;
                            }
                        case Google.Apis.Download.DownloadStatus.Completed:
                            {
                                Console.WriteLine("Download complete.");

                                saveTo = stream.ToArray();
                                break;
                            }
                        case Google.Apis.Download.DownloadStatus.Failed:
                            {
                                Console.WriteLine("Download failed.");
                                break;
                            }
                    }
                };
                request.Download(stream);
            }

            return saveTo;
        }
    }
}
