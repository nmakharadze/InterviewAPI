namespace Interview.Application.Helpers
{
    /// <summary>
    /// ფაილის გაფართოებების მენეჯმენტისთვის განკუთვნილი Helper კლასი
    /// </summary>
    public static class FileExtensionHelper
    {
        /// <summary>
        /// Content-Type-ის მიხედვით ფაილის გაფართოების დადგენა
        /// </summary>
        /// <param name="contentType">ფაილის Content-Type</param>
        /// <returns>ფაილის გაფართოება წერტილით</returns>
        public static string GetFileExtension(string contentType) =>
            contentType switch
            {
                "image/jpeg" => ".jpg",
                "image/png" => ".png",
                "image/gif" => ".gif",
                _ => ".bin"
            };

        /// <summary>
        /// ფაილის გაფართოების მიხედვით Content-Type-ის დადგენა
        /// </summary>
        /// <param name="extension">ფაილის გაფართოება წერტილით</param>
        /// <returns>ფაილის Content-Type</returns>
        public static string GetContentType(string extension) =>
            extension.ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
    }
}
