using Microsoft.ML.Data;

namespace AzvsErPred.Model
{
    public class ModelInput
    {
        [ColumnName("coutry_tag"), LoadColumn(0)]
        public string Coutry_tag { get; set; }


        [ColumnName("header"), LoadColumn(1)]
        public string Header { get; set; }


        [ColumnName("body"), LoadColumn(2)]
        public string Body { get; set; }


        [ColumnName("class"), LoadColumn(3)]
        public string Class { get; set; }


    }
}
