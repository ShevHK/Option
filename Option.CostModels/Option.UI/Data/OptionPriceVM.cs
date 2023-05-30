namespace Option.UI.Data
{
    public class OptionPriceVM
    {

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public double S { get; set; }
        public double K { get; set; }
        public DateTime T { get; set; }
        public double r { get; set; }
        public double sigma { get; set; }
        public ModelResult? BSMModel { get; set; }
        public ModelResult? CRRModel { get; set; }
        public ModelResult? LevyModel { get; set; }
    }

    public class ModelResult
    {
        public double CallPrice { get; set; }
        public double PutPrice { get; set; }
        public double Delta { get; set; }
        public double Gamma { get; set; }
        public double Vega { get; set; }
        public double Theta { get; set; }
        public double Rho { get; set; }

    }

}
