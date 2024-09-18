namespace Interface.Repository
{
    public interface IAutoCodeRepository
    {
        Task<string> GetAccHeadCodeAsync(long groupId, long? parentId);
        Task<long> GetAccHeadGroupCode(long groupId, long? parentId);
        Task<string> GetAccLedgerCode(long headId);
        Task<long> GetAccLedgerGroupCode(long? headId);
        Task<string> GetVoucherAutoNo(string prefix = null, DateTime? vcDate = null);
        Task<int> GetMaxAutoCode(string tableName);
        Task<long> GetNextSeqValue(string sequenceName);
        Task<string> GetMaxAutoCode(string tableName, string columnName = null, string prefix = null, int? howManyDigit = null, string suffix = null, bool? isPrefixFilter = false);
        Task<string> GetInvoiceMaxAutoCode(string tableName, string columnName = null, string prefix = null, int? howManyDigit = null, string suffix = null, bool? isPrefixFilter = false);
    }
}
