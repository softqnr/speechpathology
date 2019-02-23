namespace SpeechPathology.Converters
{
    public sealed class InverseBoolConverter : BoolConverter<bool>
    {
        public InverseBoolConverter() :
            base(false, true)
        { }
    }
}
