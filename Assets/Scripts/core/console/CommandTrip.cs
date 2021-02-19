namespace SomeAimGame.Console {
    public class CommandTrip<T, R, I> {
        public CommandTrip() { }

        public CommandTrip(T type, R key, I value) {
            this.Type  = type;
            this.Key   = key;
            this.Value = value;
        }

        public T Type  { get; set; }
        public R Key   { get; set; }
        public I Value { get; set; }
    };
}