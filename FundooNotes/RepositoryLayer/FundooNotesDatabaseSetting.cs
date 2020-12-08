using System;

namespace RepositoryLayer
{
    public class FundooNotesDatabaseSetting : IFundooNotesDatabaseSettings
    {
        public string FundooCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }

    public interface IFundooNotesDatabaseSettings
    {
        string FundooCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
