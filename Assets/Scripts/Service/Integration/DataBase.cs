using Service.Integration.Table;
namespace Service.Integration {
public sealed class DataBase : BaseDataBase {
    public static DataBase instance {
        get;
        private set;
    }
    public static DataBase GetInstance() {
        if (null == DataBase.instance) {
            DataBase.instance = new DataBase();
        }
        return DataBase.instance as DataBase;
    }
    protected override void Register() {
        this.daoList.Add("TScoreTable", new Dao<TScoreTable>());
        this.daoList.Add("TSummaryTable", new Dao<TSummaryTable>());
        this.daoList.Add("TLoadingTable", new Dao<TLoadingTable>());
        this.daoList.Add("MUserTable", new Dao<MUserTable>());
        this.daoList.Add("MCorporateTable", new Dao<MCorporateTable>());
        this.daoList.Add("MItemTable", new Dao<MItemTable>());
        this.daoList.Add("TItemTable", new Dao<TItemTable>());
        return;
    }
}
}
