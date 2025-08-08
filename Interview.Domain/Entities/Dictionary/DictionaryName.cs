using Interview.Domain.Entities.Base;

namespace Interview.Domain.Entities.Dictionary
{
    /// <summary>
    /// Dictionary ცხრილების კონფიგურაციის entity
    /// გამოიყენება dictionary სქემის ცხრილების მართვისთვის
    /// </summary>
    public class DictionaryName : BaseEntity
    {
        /// <summary>
        /// ცხრილის სახელი ბაზაში
        /// </summary>
        public string TableName { get; set; } = string.Empty;
        
        /// <summary>
        /// Entity კლასის სახელი
        /// </summary>
        public string EntityTypeName { get; set; } = string.Empty;
    }
}
