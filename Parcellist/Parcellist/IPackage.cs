namespace Parcellist
{
    public interface IPackage
    {
        int Width { get; }
        int Height { get; }
        int Breadth { get; }

        string Name { get; }
        decimal Cost { get; }

        bool Fits(Parcel parcel);
    }
}