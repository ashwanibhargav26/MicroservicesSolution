﻿namespace FileUploadServiceApi.Application.DTOs
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
    }
}