class JulesBattleships {

    static int NumberOfAttempts, NumberOfHits;

    public static void Main(string[] args) {

        // Plan:
        // Create Battleships fr

        var SizeOfBoard = 4;

        GreetUser();
        PlayGame(SizeOfBoard);
    }


    static void GreetUser() {

        Console.Clear();

        Cyan();
        Console.WriteLine("Ahoy matey, wanna play a game o' Battleships?");
        White();
        Console.WriteLine("(Y/n)");
        Console.Write("> ");


        char? UserInput = null;
        try {
            UserInput = Convert.ToChar(Console.ReadLine().ToLower());
        }
        catch(Exception ex) {
            Console.WriteLine($"This is the error fr: \n {ex.Message}");
            Console.WriteLine("press any key to retry");
            Console.ReadKey();
            GreetUser();
        }

        // If user wants to play, return to Main method
        if(UserInput == 'y') 
            return;

        // This code will only get run if user says no
        Red();
        Console.WriteLine("Wrong answer me hearty, wants t' reckon again?");
        Console.WriteLine("I shall give ye another try, or I shall throw ye to the sharks!");
        Console.WriteLine("I shall pretend that ne'er happened...");
        White();
        Console.WriteLine("Press Any Key To Try Again");

        Console.ReadKey();
        GreetUser();

    }


    // Create Battleship Boards + returns them
    static ( char[,], char[,] ) CreateBoards(int Size) {
        char[,] BlankBoard = new char[Size,Size];
        char[,] BattleshipsBoard = new char[Size,Size];
        char EmptyCharacter = '.';

        for(var i = 0; i < Size; i++)
            for(var j = 0; j < Size; j++) {
                BlankBoard[i,j] = EmptyCharacter;
                BattleshipsBoard[i,j] = ' ';
            }

        return ( BlankBoard, BattleshipsBoard );
    }



    static void PlayGame(int BoardSize) {
        Console.Clear();
        Yellow();
        Console.Write("Wise choice me bucko, prepare t' ");
        Red();
        Console.WriteLine("suffer!");
        Yellow();
        Console.WriteLine("Let the game commence!");
        Console.WriteLine();

        char[,] BlankBoard = new char[BoardSize,BoardSize];
        char[,] BattleshipsBoard = new char[BoardSize,BoardSize];
        ( BlankBoard, BattleshipsBoard ) = CreateBoards(BoardSize);


        CreateBattleship(BoardSize, BattleshipsBoard);
        
        do {
            bool GuessResult;
            int x, y;
            ( GuessResult, x, y ) = Guess(BoardSize, BlankBoard, BattleshipsBoard);
            if(GuessResult)  {
                Green();
                Console.WriteLine("Tis a Hit!");
                BlankBoard[y, x] = 'x';
                NumberOfAttempts++;
                NumberOfHits++;
                White();
                Console.WriteLine("Press Any Key To Try Again!");
                Console.ReadKey();
            }
            else {

                Red();
                Console.WriteLine("'Tis a Miss! Better luck next time... ");
                BlankBoard[y, x] = 'o';
                NumberOfAttempts++;
                White();
                Console.WriteLine("Press Any Key To Try Again!");
                Console.ReadKey();
            }
        } while(NumberOfHits < 2);

        Console.WriteLine("You Won!");
        GameWin(BoardSize, BlankBoard);
    }


    static void DisplayBoard(int BoardSize, char[,] Board) {
        
        for(int i = 0; i < BoardSize; i++) {
            for(int j = 0; j < BoardSize; j++) {
               Console.Write($"{Board[i,j]} ");
            }
            Console.WriteLine();
        }

    }
    

    static ( bool, int x, int y ) Guess(int BoardSize, char[,] Board, char[,] BattleshipBoard) {

        Console.Clear();

        DisplayBoard(BoardSize, Board);

        Console.WriteLine($"Enter yer guess fer the X coordinate (0-{BoardSize})");
        int GuessX = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"Enter yer guess fer the Y coordinate (0-{BoardSize})");
        int GuessY = Convert.ToInt32(Console.ReadLine());

        if(BattleshipBoard[GuessY, GuessX] == 'x') {
            return ( true, GuessX, GuessY );
        }
        else return ( false, GuessX, GuessY );
    }



    static char[,] CreateBattleship(int BoardSize, char[,] BattleshipBoard) {

        // Challenge, Implent multiple battleships with different sizes


        var random = new Random();
        var X = random.Next(BoardSize - 2) + 1;
        var Y = random.Next(BoardSize - 2) + 1;

        var Direction = random.Next(3);

        int SecondPointX = 0;
        int SecondPointY = 0;


        switch(Direction) {
            case 0:
                SecondPointX = X;
                SecondPointY = Y + 1;
                break;
            case 1:
                SecondPointX = X + 1;
                SecondPointY = Y;
                break;
            case 2:
                SecondPointX = X - 1;
                SecondPointY = Y;
                break;
            case 3:
                SecondPointX = X;
                SecondPointY = Y - 1;
                break;
            default:
                SecondPointX = -1;
                SecondPointY = -1;
                break;
        }
        
        BattleshipBoard[X,Y] = 'x';
        BattleshipBoard[SecondPointX,SecondPointY] = 'x';

        return BattleshipBoard;
    }

    static void GameWin(int Size, char[,] Board) {
        Console.Clear();
        DisplayBoard(Size, Board);
        Console.WriteLine($"Congratulations matey, ye sank me ship in {NumberOfAttempts} Attempts!");
        Console.ReadKey();
    }

    static void Cyan() => Console.ForegroundColor = ConsoleColor.Cyan;
    static void Red() => Console.ForegroundColor = ConsoleColor.Red;
    static void White() => Console.ForegroundColor = ConsoleColor.White;
    static void Yellow() => Console.ForegroundColor = ConsoleColor.DarkYellow;
    static void Green() => Console.ForegroundColor = ConsoleColor.Green;
}
