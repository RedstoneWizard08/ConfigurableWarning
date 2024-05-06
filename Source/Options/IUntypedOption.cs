namespace ConfigurableWarning.Options {
    public interface IUntypedOption {
        string GetName();
        object GetValue();
        void SetValue(object value);
    }
}
