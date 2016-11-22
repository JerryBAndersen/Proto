namespace Proto.Storables {
    internal interface Feedable {
        void Feed();
        bool Starve();
    }
}