using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using Azure;

namespace User_API.Models
{
    // <!-- Microsoft Learn, 2024 [A] -->
    // <!-- ITableEntity is taken and used from Microsoft Learn -->
    public class User : ITableEntity
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}


// <!-- REFERENCE LIST -->
// ---------------------------
// <!-- Microsoft Learn. 2024 [A]. ITableEntity Interface, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/azure.data.tables.itableentity?view=azure-dotnet> [Accessed 8 August 2026]. -->



