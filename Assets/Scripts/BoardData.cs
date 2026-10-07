public class BoardData<T> where T : IItem
{
    private T[] _array;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public BoardData(int width, int height)
    {
        Width = width;
        Height = height;

        _array = new T[width * height];
    }

    public T this[int x, int y]
    {
        get
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height) return default(T);
            return _array[y * Width + x];
        }
        set
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                _array[y * Width + x] = value;
            }
        }
    }
}
