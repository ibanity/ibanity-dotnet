namespace Ibanity.Apis.Client.Models
{
    public class Filter
    {
        private readonly string _field;
        private readonly FilterOperator _operator;
        private readonly string _value;

        public Filter(string field, FilterOperator @operator, string value)
        {
            _field = field;
            _operator = @operator;
            _value = value;
        }

        public override string ToString() =>
            $"filter[{_field}][{_operator.ToString("g").ToLower()}]={_value}";
    }

    public enum FilterOperator
    {
        Eq,
        Like,
        Contains,
        In
    }
}
