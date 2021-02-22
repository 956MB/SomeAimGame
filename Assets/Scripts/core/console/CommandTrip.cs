namespace SomeAimGame.Console {
    public class CommandTrip<T, K, V> {
        public CommandTrip() { }

        public CommandTrip(T type, K key, V value) {
            this.Type  = type;
            this.Key   = key;
            this.Value = value;
        }

        public T Type  { get; set; }
        public K Key   { get; set; }
        public V Value { get; set; }
    };
}