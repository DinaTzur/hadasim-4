namespace CoronaAPI.Models
{
    public class StringValidateModel
    {
        public string FieldName { get; set; }
        public string? Value { get; set; }
        public bool Required { get; set; }
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public string Pattern { get; set; }

        public StringValidateModel(string fieldName, string? value, bool required, int? minLength, int? maxLength, string pattern)
        {
            Value = value;
            Required = required;
            MinLength = minLength;
            MaxLength = maxLength;
            Pattern = pattern;
            FieldName = fieldName;
        }
    }
}
