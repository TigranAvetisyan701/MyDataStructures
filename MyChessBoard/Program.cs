using MyQueueLib;

MatrixMain(8);

MatrixAux(8);

Console.WriteLine(RookCheck([1, 8], [1, 8]));

Console.WriteLine(KnightCheck([1, 8], [2, 6]));

Console.WriteLine(KnightMinPath([3, 4], [7, 7]));

Console.WriteLine(BishopCheck([1, 1], [4, 3]));

Console.WriteLine(BishopWithBarrier([1, 1], [5, 5], [3, 4]));

List<Cell> arr = BishopWithBarrierPossibles(new Cell (3, 3), new Cell (6, 6));
foreach (Cell v in arr)
    Console.WriteLine($"{v.X}, {v.Y}");
void MatrixMain(int size)
{
    for (int i = 0; i < size; i++)
    {
        for (int j = 0; j < size; j++)
        {
            if (i == j)
                Console.Write('#');
            else
                Console.Write('*');
        }
        Console.Write('\n');
    }
}

void MatrixAux(int size)
{
    for (int i = 0; i < size; i++)
    {
        for (int j = 0; j < size; j++)
        {
            if (i + j == size - 1)
                Console.Write('#');
            else
                Console.Write('*');
        }
        Console.Write('\n');
    }
}

bool RookCheck(int[] cell1, int[] cell2)
{
    if ((cell1[0] == cell2[0] || cell1[1] == cell2[1]) && !(cell1[0] == cell2[0] && cell1[1] == cell2[1]))
        return true;
    return false;
}

bool KnightCheck(int[] cell1, int[] cell2)
{
    int x1 = cell1[0];
    int y1 = cell1[1];

    int x2 = cell2[0];
    int y2 = cell2[1];

    if (!(x1 == x2 && y1 == y2) &&
         ((x1 - 1 == x2 || x1 + 1 == x2) && (y1 + 2 == y2 || y1 - 2 == y2)) ||
         ((x1 - 2 == x2 || x1 + 2 == x2) && (y1 - 1 == y2 || y1 + 1 == y2)))
        return true;
    return false;
}

int KnightMinPath(int[] startCell, int[] endCell)
{
    if (startCell[0] == endCell[0] && startCell[1] == endCell[1]) return 0;

    int[,] distance = new int[8, 8];
    for (int i = 0; i < 8; i++)
        for (int j = 0; j < 8; j++)
            distance[i, j] = -1;

    MyQueue<int[]> queue = new MyQueue<int[]>();

    distance[startCell[0], startCell[1]] = 0;
    queue.Enqueue(startCell);

    while (queue.Count > 0)
    {
        int[] current = queue.Dequeue();
        int x = current[0];
        int y = current[1];

        if (x == endCell[0] && y == endCell[1])
            return distance[x, y];

        for (int nextX = 0; nextX < 8; nextX++)
        {
            for (int nextY = 0; nextY < 8; nextY++)
            {
                int[] nextCell = { nextX, nextY };

                if (distance[nextX, nextY] == -1 && KnightCheck(current, nextCell))
                {
                    distance[nextX, nextY] = distance[x, y] + 1;
                    queue.Enqueue(nextCell);
                }
            }
        }
    }

    return distance[endCell[0], endCell[1]];
}

bool BishopCheck(int[] cell1, int[] cell2)
{
    int x = cell1[0] - cell2[0];
    int y = cell1[1] - cell2[1];

    x *= x;
    y *= y;

    if (x == y)
        return true;
    return false;
}

bool BishopWithBarrier(int[] cell1, int[] cell2, int[] BarrierCell)
{
    bool noBarrier = BishopCheck(cell1, cell2);

    if (noBarrier)
        return !(BishopCheck(cell1, BarrierCell) && BishopCheck(BarrierCell, cell2));
    return false;
}

List<Cell> BishopWithBarrierPossibles(Cell cell1, Cell BarrierCell)
{
    List<Cell> arr = new List<Cell>();

    int[][] directions = new int[][]
    {
        new int[] { 1, 1 },
        new int[] { 1, -1 },
        new int[] { -1, 1 },
        new int[] { -1, -1 }
    };

    foreach (var dir in directions)
    {
        int currentX = cell1.X + dir[0];
        int currentY = cell1.Y + dir[1];

        while (currentX >= 1 && currentX <= 8 && currentY >= 1 && currentY <= 8)
        {
            if (currentX == BarrierCell.X && currentY == BarrierCell.Y)
            {
                break;
            }

            arr.Add(new Cell(currentX, currentY));

            currentX += dir[0];
            currentY += dir[1];
        }
    }

    return arr;
}

public struct Cell
{
    public int X { get; set; }
    public int Y { get; set; }

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }
}