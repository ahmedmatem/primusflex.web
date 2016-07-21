namespace PrimusFlex.Data.Models
{
    using PrimusFlex.Data.Common.Models;

    public class StorageAccount : BaseModel<int>
    {
        public string AccountName { get; set; }

        public string AccountKey { get; set; }
    }
}
