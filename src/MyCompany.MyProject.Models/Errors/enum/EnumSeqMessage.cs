namespace MyCompany.MyProject.Models
{
    public enum EnumSeqMessage
{
        //public SeqMessage
        Cant_Be_Null = 9999,
        Type_Is_Wrong = 9998,
        Length_Is_Wrong = 9997,
        Is_Duplicated = 9996,
        Is_Invalid = 9995,
        Value_Formate_Is_wrong=9994,
        //System Error(For developer) 9000-9099
        Request_Is_Null = 9000,
        Token_Is_Invalid = 9001,
        Token_Is_Required = 9002,
        Exception_Problem =9003,
        Update_DB_Failed=9004,
        Data_Is_Not_Existed =9005,
    }
}
