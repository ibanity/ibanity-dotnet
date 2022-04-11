namespace Ibanity.Apis.Client.Models
{
    /// <summary>
    /// Filter results when using <c>List</c> methods.
    /// </summary>
    public class Filter
    {
        private readonly string _field;
        private readonly FilterOperator _operator;
        private readonly string _value;

        /// <summary>
        /// Build a new instance.
        /// </summary>
        /// <param name="field">Resource field to check</param>
        /// <param name="operator">Comparison type</param>
        /// <param name="value">Value to check for</param>
        public Filter(string field, FilterOperator @operator, string value)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new System.ArgumentException($"'{nameof(field)}' cannot be null or whitespace.", nameof(field));

            if (string.IsNullOrWhiteSpace(value))
                throw new System.ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

            _field = field;
            _operator = @operator;
            _value = value;
        }

        /// <summary>
        /// String representation of the filter.
        /// </summary>
        /// <returns>Filter to be used in query string</returns>
        public override string ToString() =>
            $"filter[{_field}][{_operator.ToString("g").ToLower()}]={_value}";
    }

    /// <summary>
    /// Comparison types.
    /// </summary>
    public enum FilterOperator
    {
        /// <summary>
        /// Equals.
        /// </summary>
        Eq,
        /// <summary>
        /// Fuzzy match.
        /// </summary>
        Like,
        /// <summary>
        /// Contains.
        /// </summary>
        Contains,
        /// <summary>
        /// In.
        /// </summary>
        In
    }
}
