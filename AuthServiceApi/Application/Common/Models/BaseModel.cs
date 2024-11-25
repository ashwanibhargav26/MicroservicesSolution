namespace AuthServiceApi.Application.Common.Models
{
    public abstract class BaseModel
    {
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }=DateTime.Now;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
