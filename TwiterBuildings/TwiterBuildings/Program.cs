
const string getActionErrorMessage = "Wrong input !!!\n";
const string getActionMessage = "Select Action\n" +
    "1. Rectangle\n" +
    "2. Triangle\n" +
    "3. Exit";
const string getTriangleActionMessage = "Select Action\n" +
    "1. Calculate perimeter\n" +
    "2. Print triangle";


int action;
do
{
    action = GetActionFromUser();

    switch (action)
    {
        case 1:
            HandleRectangle();
            break;
        case 2:
            HandleTriangle();
            break;
    }
} while (action != 3);

int GetMetric(string metric)
{
    Console.WriteLine($"Input {metric}:");
    string? input = Console.ReadLine();
    int res;
    int.TryParse(input, out res);   
    return res;
}


void HandleTriangle()
{
    int height = GetMetric("height");
    int width = GetMetric("width");

    int action=GetTriangleActionFromUser();

    if (action == 1)
    {
        double vertice = Math.Sqrt(Math.Pow(height,2) + Math.Pow(width/2, 2));
        Console.WriteLine(vertice * 2 + width);
    }
    else
    {
        if(width%2 == 0 || height*2<width) 
        {
            Console.WriteLine("Can't print triangle");
        } 
        else
        {
            PrintTriangleToScreen(width, height);
        }
    }
}

void PrintTriangleToScreen(double width, double height)
{
    double groupNum = Math.Floor((width - 2) / 2);
    double groupSize = Math.Floor((height - 2) / groupNum);
    double firstGroupSize = height - (groupSize * groupNum + 2) + groupSize;

    //option 1
    for (int i = 1; i <= height; i++)
    {
        int group = GetGroupNum(i, firstGroupSize, groupSize);
        int numOfStars = group * 2 - 1;
        Console.WriteLine("".PadLeft(numOfStars, '*').PadLeft(Convert.ToInt32(Math.Floor(width - numOfStars) / 2) + numOfStars, ' '));
    }

    //option 2
    double curGroupSize = firstGroupSize;
    int numberOfStars = 3;
    Console.WriteLine("".PadLeft(1, '*').PadLeft(Convert.ToInt32(Math.Floor(width - 1) / 2) + 1, ' '));

    for (int i = 1; i <= groupNum; i++)
    {

        for (int j = 0; j < curGroupSize; j++)
        {
            Console.WriteLine("".PadLeft(numberOfStars, '*').PadLeft(Convert.ToInt32(Math.Floor(width - numberOfStars) / 2) + numberOfStars, ' '));
        }
        curGroupSize = groupSize;
        numberOfStars += 2;
    }

    Console.WriteLine("".PadLeft(Convert.ToInt32(width), '*'));
}




int GetGroupNum(int i, double firstGroupSize, double groupSize)
{
    if (i == 1)
        return 1;
    else if (i <= firstGroupSize + 1)
        return 2;
    else
        return Convert.ToInt32(Math.Ceiling((i - 1 - firstGroupSize) / groupSize) + 2);
}

void HandleRectangle()
{
    int height = GetMetric("height");
    int width = GetMetric("width");

    if ( height == width || Math.Abs(height - width) > 5)
    {
        Console.WriteLine(height * width);
    }
    else
    {
        Console.WriteLine(height*2 + width*2);
    }
}

int GetActionFromUser()
{   
    do
    {
        Console.WriteLine(getActionMessage);
        string? input=Console.ReadLine();

        switch (input)
        {
            case "1":
                return 1;
            case "2":
                return 2;
            case "3":
                return 3;
            default:
                Console.WriteLine(getActionErrorMessage);
                break;
        }
    } while (true);
}

int GetTriangleActionFromUser()
{
    do
    {
        Console.WriteLine(getTriangleActionMessage);
        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                return 1;
            case "2":
                return 2;
            default:
                Console.WriteLine(getActionErrorMessage);
                break;
        }
    } while (true);
}