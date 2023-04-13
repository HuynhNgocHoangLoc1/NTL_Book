﻿namespace NTL_Book.Helpers
{
    public class FileUploadHelper
    {
        public static string BookImageBaseDirectory { get; } = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "books");

        public static string BookImageBaseHref { get; } = @"\img\books\";
    }
}
