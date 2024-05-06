namespace ConfigurableWarning.Options.Untyped {
    internal class UntypedTextOption(TextOption inner) : IUntypedOption {
        public string GetName() => inner.GetName();
        public object GetValue() => inner.GetValue();
        public void SetValue(object value) => inner.SetValue((string)value);
    }
}
