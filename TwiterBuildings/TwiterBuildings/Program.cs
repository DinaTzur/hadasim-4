//Prints that repeat themselves in several places or can be used in the future for more things
const string getActionErrorMessage = "Wrong input !!!\n";
const string getActionMessage = "Select Action\n" +
    "1. Rectangle\n" +
    "2. Triangle\n" +
    "3. Exit";
const string getTriangleActionMessage = "Select Action\n" +
    "1. Calculate perimeter\n" +
    "2. Print triangle";

//Reference to further calculation according to the user's choice of shape
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
//Getting shape dimensions parameters
int GetMetric(string metric)
{
    Console.WriteLine($"Input {metric}:");
    string? input = Console.ReadLine();
    int res;
    int.TryParse(input, out res);   
    return res;
}

//Receiving variables, choosing a desired continuation and printing the extent or sending it to the triangle printing function.
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
            Console.WriteLine("Can't print triangle, Invalid dimensions for printing.");
        } 
        else
        {
            PrintTriangleToScreen(width, height);
        }
    }
}
//The triangle print
void PrintTriangleToScreen(double width, double height)
{

    double groupNum = Math.Max(Math.Floor((width - 2) / 2), 1);
    double groupSize = Math.Floor((height - 2) / groupNum);
    double firstGroupSize = Math.Max(height - (groupSize * groupNum + 2) + groupSize,1);

    for (int i = 1; i <= height; i++)
    {
        int group = GetGroupNum(i, firstGroupSize, groupSize);
        double numOfStars = Math.Min(group * 2 - 1, width); // Adjust for extreme case
        Console.WriteLine("".PadLeft(Convert.ToInt32(numOfStars), '*').PadLeft(Convert.ToInt32(Math.Floor(width - numOfStars) / 2) + Convert.ToInt32(numOfStars), ' '));
    }

    
}

//An auxiliary function for calculating the number of asterisks needed in the row of the triangle
int GetGroupNum(int i, double firstGroupSize, double groupSize)
{
    if (i == 1)
        return 1;
    else if (i <= firstGroupSize + 1)
        return 2;
    else
        return Convert.ToInt32(Math.Ceiling((i - 1 - firstGroupSize) / groupSize) + 2);
}

//Accepting variables, and printing the perimeter or area of a rectangle according to an internal calculation
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
//Shape selection by the user
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
//Continued selection by the user on triangle data
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